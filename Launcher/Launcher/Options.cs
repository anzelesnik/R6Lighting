using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandleLeaker
{
    class Options
    {
        /*const uint DELETE = 0x00010000;
        const uint READ_CONTROL = 0x00020000;
        const uint WRITE_DAC = 0x00040000;
        const uint WRITE_OWNER = 0x00080000;
        const uint SYNCHRONIZE = 0x00100000;
        const uint END = 0xFFF; 
        const uint PROCESS_ALL_ACCESS = (DELETE | READ_CONTROL | WRITE_DAC | WRITE_OWNER | SYNCHRONIZE | END);*/
        public static string TargetProcess = "RainbowSix";
        public static string YourProcess = "R6Lighting.exe";
        public static UInt32 DesiredAccess = 0x0010; 
        public static int DelayToWait = 10;
        public static uint ObjectTimeout = 1000;
        public static bool UseDuplicateHandle = false;
    }
}
