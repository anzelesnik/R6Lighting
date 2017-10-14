using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;

namespace R6Lighting
{
    public partial class Form1 : Form
    {
        IntPtr PlayerBase;
        IntPtr handle;
        int[] AllValues;

        public Form1(string[] args)
        {
            InitializeComponent();
            if (Process.GetProcessesByName("RainbowSix").Length > 0) // Checks if the game is running
            {
                label3.Text = "Running";
                label3.ForeColor = Color.Green;
                if (args.Length > 0) // If a handle was passed in when starting this program use that one (HLeaker)
                {
                    label7.Text = "Bypassed";
                    label7.ForeColor = Color.Green;
                    int handleInt = Convert.ToInt32(args[0]);
                    handle = new IntPtr(handleInt);
                }
                else 
                {
                    ServiceController sc = new ServiceController();
                    sc.ServiceName = "BEService";
                    if (sc.Status == ServiceControllerStatus.Running || sc.Status == ServiceControllerStatus.Paused) // Check if the Battleye service is running, if it is, stop it
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                        label7.Text = "Stopped";
                        label7.ForeColor = Color.Green;
                    }
                    else if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        label7.Text = "Stopped";
                        label7.ForeColor = Color.Green;
                    }
                    else
                    {
                        label7.Text = "Error";
                        label7.ForeColor = Color.Red;
                    } 
                    handle = ReadMem.OpenProc("RainbowSix"); // Get the games handle
                }
                string statusmsg = LightingCtrl.Initialize(); // Initialize CUE.NET
                label4.Text = statusmsg;
                StatusMsg(statusmsg); // Change the UI text according to the response from CUE.NET
                if (statusmsg != "Keyboard not found")
                {
                    PlayerBase = ReadMem.PlayerBase(0x046DFCA0, handle); // Get the playerbase of the game
                    if (handle != IntPtr.Zero && PlayerBase != IntPtr.Zero)
                    {
                        timer1.Enabled = true; // Enable the timer which reads the values
                    }
                    else
                    {
                        label3.Text = "Error";
                        label3.ForeColor = Color.Red;
                    }
                    LightingCtrl.BaseLighting(); // background keyboard lighting
                }
            }
            else
            {
                    label3.Text = "Not running";
                    label3.ForeColor = Color.Red;
                    label7.Text = "Not running";
                    label7.ForeColor = Color.Red;
            }
        }

        private void StatusMsg(string status)
        {
            if (status == "Connected")
            {
                label4.ForeColor = Color.Green;
                button1.Enabled = false;
            }
            else
            {
                label4.ForeColor = Color.Red;
                button1.Enabled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string statusmsg = LightingCtrl.Initialize();
            StatusMsg(statusmsg);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            AllValues = ReadMem.DataValues(PlayerBase, handle); // Read all values (0 = hp, 1 = ammo, 2 = gadget)
            LightingCtrl.HpLighting(AllValues[0]); // Change the effects accordingly to the read values
            LightingCtrl.ReloadLighting(AllValues[1], AllValues[0]);
            LightingCtrl.GadgetLighting(AllValues[2]);
        }
    }
}
