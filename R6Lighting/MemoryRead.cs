using System;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace R6Lighting
{
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

        public static int FinalAddress(IntPtr PlayerBase, int[] offsets, IntPtr handle) // Has to be constanly re-read due to constant variable address changes
        {
            IntPtr CurPointer = PlayerBase;
            int value = 0;
            for (int i = 0; i<offsets.Length; i++) // Read multilevel pointer
            {
                int CurOffset = offsets[i];
                IntPtr pointer = IntPtr.Add(CurPointer, CurOffset);
                byte[] buffer = new byte[8];
                ReadProcessMemory(handle, pointer, buffer, 8, 0);
                if (i == 4)
                {
                    value = buffer[0];
                }
                else
                {
                    Int64 NextPointer = BitConverter.ToInt64(buffer, 0);
                    CurPointer = new IntPtr(NextPointer);
                }
            }
            return value;
        }
    }
}
