using System;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace R6Lighting
{
    public struct MemData
    {
        public static readonly int[] LocalPlayer = { 0x1B8, 0x48, 0x80 };
        public static readonly int[] health = { 0x3E8, 0x12C };
        public static readonly int[] ammo = { 0x3D8, 0x160 };
        public static readonly int[] SecGadget = { 0x460, 0x64C };
        public static readonly int[][] AllOffsets = { health, ammo, SecGadget };
    }

    public class ReadMem
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern int ReadProcessMemory(IntPtr hProcess, IntPtr BaseAddress, byte[] Buffer, int size, int BytesRead);

        public static IntPtr OpenProc(string procName)
        {
            try
            {
                Process proc = Process.GetProcessesByName(procName)[0]; // Get process
                IntPtr handle = OpenProcess(0x0010, false, proc.Id); // Get games handle
                return handle;
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        public static IntPtr PlayerBase(int baseOffset, IntPtr handle)
        {
            try
            {
                Process proc = Process.GetProcessesByName("RainbowSix")[0];
                IntPtr baseadd = proc.MainModule.BaseAddress; // Get games' entry point address in memory
                byte[] buffer = new byte[8];
                IntPtr pointer = IntPtr.Add(baseadd, baseOffset); // Add up the games' entry point address and the offset to get the playerbase
                ReadProcessMemory(handle, pointer, buffer, 8, 0); // Read the playerbase
                Int64 pBaseInt = BitConverter.ToInt64(buffer, 0);
                IntPtr pBase = new IntPtr(pBaseInt);
                return pBase;
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        public static int[] DataValues(IntPtr PlayerBase, IntPtr handle) // Has to be constanly re-read due to constant variable address changes
        {
            IntPtr CurPointer = PlayerBase;
            int value = 0;
            int valueHp = 0;
            int valueAmmo = 0;
            int valueGadget = 0;
            byte[] buffer = new byte[8];
            for (int i = 0; i<MemData.LocalPlayer.Length; i++) // Read The LoacalPlayer address
            {
                int CurOffset = MemData.LocalPlayer[i];
                IntPtr pointer = IntPtr.Add(CurPointer, CurOffset);
                ReadProcessMemory(handle, pointer, buffer, 8, 0);
                Int64 NextPointer = BitConverter.ToInt64(buffer, 0);
                CurPointer = new IntPtr(NextPointer);
            }
            IntPtr hp = IntPtr.Add(CurPointer, MemData.health[0]); // Adds the first offset for the specific item
            IntPtr ammo = IntPtr.Add(CurPointer, MemData.ammo[0]);
            IntPtr gadget = IntPtr.Add(CurPointer, MemData.SecGadget[0]);
            IntPtr[] pointers = { hp, ammo, gadget };
            for (int i = 0; i<pointers.Length; i++) // Reads the pointers, adds the final offset, reads again and return's the value for all items
            {
                ReadProcessMemory(handle, pointers[i], buffer, 8, 0);
                Int64 NextPointer = BitConverter.ToInt64(buffer, 0);
                IntPtr pointer = new IntPtr(NextPointer+MemData.AllOffsets[i][1]);
                ReadProcessMemory(handle, pointer, buffer, 8, 0);
                value = BitConverter.ToInt32(buffer, 0);
                switch (i)
                {
                    case 0:
                        valueHp = value;
                        break;
                    case 1:
                        valueAmmo = value;
                        break;
                    case 2:
                        valueGadget = value;
                        break;
                }
            }
            int[] AllValues = {valueHp, valueAmmo, valueGadget};
            return AllValues;
        }
    }
}
