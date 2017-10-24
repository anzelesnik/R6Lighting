using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using HandleLeaker;

namespace Launcher
{
    public partial class Launcher : Form
    {
        public Launcher()
        {
            InitializeComponent();
            
            if (Process.GetProcessesByName("RainbowSix").Length == 0)
                button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"/Stuff/R6Lighting.exe";
            Process.Start(info);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CProcess CurrentProcess = new CProcess(), TargetProcess = new CProcess(Options.TargetProcess), ServProcess; //Use HLeaker to get a handle with needed privileges
            int counter = 0, maxCount = 1;
            List<Service.HANDLE_INFO> HandleList = new List<Service.HANDLE_INFO>();
            IntPtr hProcess = IntPtr.Zero;
            CurrentProcess.SetPrivilege("SeDebugPrivilege", true);
            CurrentProcess.SetPrivilege("SeTcbPrivilege", true);
            TargetProcess.Wait(Options.DelayToWait);
            if (TargetProcess.IsValidProcess())
            {
                HandleList = Service.ServiceEnumHandles(TargetProcess.GetPid(), Options.DesiredAccess);
                if (HandleList.Count > 0)
                {
                    foreach (Service.HANDLE_INFO enumerator in HandleList)
                    {
                        if (counter == maxCount)
                            break;
                        if (enumerator.Pid == Kernel32.GetCurrentProcessId()) continue;
                        ServProcess = new CProcess(enumerator.Pid);
                        if (Service.ServiceSetHandleStatus(ServProcess, (IntPtr)enumerator.hProcess, true, true) == true)
                        {
                            hProcess = Service.ServiceStartProcess(null, Directory.GetCurrentDirectory() + "\\Stuff\\" + Options.YourProcess + " " + enumerator.hProcess, null, true, ServProcess.GetHandle());
                            Service.ServiceSetHandleStatus(ServProcess, (IntPtr)enumerator.hProcess, false, false);
                            counter++;
                        }
                        if (hProcess != null)
                            Kernel32.CloseHandle(hProcess);
                        ServProcess.Close();
                    }
                }
                TargetProcess.Close();
            }
            CurrentProcess.SetPrivilege("SeDebugPrivilege", false);
            CurrentProcess.SetPrivilege("SeTcbPrivilege", false);
            this.Close();
        }
    }
}
