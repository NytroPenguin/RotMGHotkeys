using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lib_K_Relay;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Utilities;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Networking.Packets.DataObjects;
namespace RotMGHotkeys
{
    public partial class Settings : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private RotMGHotkeys _hk;
        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        public Settings(RotMGHotkeys hk)
        {
            _hk = hk;
            InitializeComponent();
            f1.Text = Config.f1();
            f2.Text = Config.f2();
            f3.Text = Config.f3();
            f4.Text = Config.f4();
            f5.Text = Config.f5();
            f6.Text = Config.f6();
            f7.Text = Config.f7();
            f8.Text = Config.f8();
            //enableHotSwap();
            //enableTextHotkeys();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (getActiveProcessName().Contains("flashplayer"))
            {
                if (m.Msg == 0x0312)
                {
                    int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                    _hk.processHotkey(id);
                }
            }
        }

        private string getActiveProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        public void enableTextHotkeys()
        {
            //text hotkeys
            RegisterHotKey(this.Handle, 1, (int)KeyModifier.None, Keys.F1.GetHashCode());
            RegisterHotKey(this.Handle, 2, (int)KeyModifier.None, Keys.F2.GetHashCode());
            RegisterHotKey(this.Handle, 3, (int)KeyModifier.None, Keys.F3.GetHashCode());
            RegisterHotKey(this.Handle, 4, (int)KeyModifier.None, Keys.F4.GetHashCode());
            RegisterHotKey(this.Handle, 5, (int)KeyModifier.None, Keys.F5.GetHashCode());
            RegisterHotKey(this.Handle, 6, (int)KeyModifier.None, Keys.F6.GetHashCode());
            RegisterHotKey(this.Handle, 7, (int)KeyModifier.None, Keys.F7.GetHashCode());
            RegisterHotKey(this.Handle, 8, (int)KeyModifier.None, Keys.F8.GetHashCode());
        }

        public void disableTextHotkeys()
        {
            UnregisterHotKey(this.Handle, 1);
            UnregisterHotKey(this.Handle, 2);
            UnregisterHotKey(this.Handle, 3);
            UnregisterHotKey(this.Handle, 4);
            UnregisterHotKey(this.Handle, 5);
            UnregisterHotKey(this.Handle, 6);
            UnregisterHotKey(this.Handle, 7);
            UnregisterHotKey(this.Handle, 8);
        }

        public void enableHotSwap()
        {
            //equip swap hotkeys
            RegisterHotKey(this.Handle, 9, (int)KeyModifier.None, Keys.D1.GetHashCode());
            RegisterHotKey(this.Handle, 10, (int)KeyModifier.None, Keys.D2.GetHashCode());
            RegisterHotKey(this.Handle, 11, (int)KeyModifier.None, Keys.D3.GetHashCode());
            RegisterHotKey(this.Handle, 12, (int)KeyModifier.None, Keys.D4.GetHashCode());
            RegisterHotKey(this.Handle, 13, (int)KeyModifier.None, Keys.D5.GetHashCode());
            RegisterHotKey(this.Handle, 14, (int)KeyModifier.None, Keys.D6.GetHashCode());
            RegisterHotKey(this.Handle, 15, (int)KeyModifier.None, Keys.D7.GetHashCode());
            RegisterHotKey(this.Handle, 16, (int)KeyModifier.None, Keys.D8.GetHashCode());
        }

        public void disableHotSwap()
        {
            UnregisterHotKey(this.Handle, 9);
            UnregisterHotKey(this.Handle, 10);
            UnregisterHotKey(this.Handle, 11);
            UnregisterHotKey(this.Handle, 12);
            UnregisterHotKey(this.Handle, 13);
            UnregisterHotKey(this.Handle, 14);
            UnregisterHotKey(this.Handle, 15);
            UnregisterHotKey(this.Handle, 16);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Config.updateConfig(f1.Text, f2.Text, f3.Text, f4.Text, f5.Text, f6.Text, f7.Text, f8.Text);
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            disableHotSwap();
            disableTextHotkeys();
        }

        private void Settings_Shown(object sender, EventArgs e)
        {
            enableHotSwap();
            enableTextHotkeys();
        }
    }
}
