using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Camera
{
    public partial class CameraForm
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        private AppSettings settings = new AppSettings();

        private const int HOTKEY_ENABLE_PATHING = 1;
        private const int HOTKEY_STOP_PATHING = 2;
        private const int HOTKEY_ADD_POINT = 3;
        private const int HOTKEY_ROLL_INC = 4;
        private const int HOTKEY_ROLL_DEC = 5;
        private const int HOTKEY_FOV_INC = 6;
        private const int HOTKEY_FOV_DEC = 7;

        private void InitializeHotKeyTimer()
        {
            focusCheckTimer = new Timer();
            focusCheckTimer.Interval = 1000;
            focusCheckTimer.Tick += FocusCheckTimer_Tick;
            focusCheckTimer.Start();
        }

        private void InitializeHotkeys()
        {
            RegisterHotKey(this.Handle, HOTKEY_ENABLE_PATHING, 0, (uint)settings.EnablePathingHotkey);
            RegisterHotKey(this.Handle, HOTKEY_STOP_PATHING, 0, (uint)settings.StopPathingHotkey);
            RegisterHotKey(this.Handle, HOTKEY_ADD_POINT, 0, (uint)settings.AddPointHotkey);
            RegisterHotKey(this.Handle, HOTKEY_ROLL_INC, 0, (uint)settings.RollIncHotkey);
            RegisterHotKey(this.Handle, HOTKEY_ROLL_DEC, 0, (uint)settings.RollDecHotkey);
            RegisterHotKey(this.Handle, HOTKEY_FOV_INC, 0, (uint)settings.FovIncHotkey);
            RegisterHotKey(this.Handle, HOTKEY_FOV_DEC, 0, (uint)settings.FovDecHotkey);
        }

        private string GetFocusedProcessName()
        {
            IntPtr foregroundWindowHandle = GetForegroundWindow();
            GetWindowThreadProcessId(foregroundWindowHandle, out uint processId);
            Process process = Process.GetProcessById((int)processId);
            return process.ProcessName;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;

            switch (m.Msg)
            {
                case WM_HOTKEY:
                    int id = m.WParam.ToInt32();

                    if (IsDesiredProcessFocused())
                    {
                        HandleHotkey(id);
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private void HandleHotkey(int id)
        {
            switch (id)
            {
                case HOTKEY_ENABLE_PATHING:
                    enablePathing.Checked = true;
                    break;
                case HOTKEY_STOP_PATHING:
                    enablePathing.Checked = false;
                    break;
                case HOTKEY_ADD_POINT:
                    //createKeyframe();
                    break;
                case HOTKEY_ROLL_INC:
                    increaseRoll();
                    break;
                case HOTKEY_ROLL_DEC:
                    decreaseRoll();
                    break;
                case HOTKEY_FOV_INC:
                    increaseFOV();
                    break;
                case HOTKEY_FOV_DEC:
                    decreaseFOV();
                    break;
            }
        }

        private void FocusCheckTimer_Tick(object sender, EventArgs e)
        {
            if (IsDesiredProcessFocused())
            {
                InitializeHotkeys();
            }
            else
            {
                UnregisterHotkeys();
            }
        }

        private bool IsDesiredProcessFocused()
        {
            string desiredProcessName = selectedProcessName;
            string focusedProcessName = GetFocusedProcessName();

            return string.Equals(focusedProcessName, desiredProcessName, StringComparison.OrdinalIgnoreCase);
        }

        private void UnregisterHotkeys()
        {
            // Unregister hotkeys
            UnregisterHotKey(this.Handle, HOTKEY_ENABLE_PATHING);
            UnregisterHotKey(this.Handle, HOTKEY_STOP_PATHING);
            UnregisterHotKey(this.Handle, HOTKEY_ADD_POINT);
            UnregisterHotKey(this.Handle, HOTKEY_ROLL_INC);
            UnregisterHotKey(this.Handle, HOTKEY_ROLL_DEC);
            UnregisterHotKey(this.Handle, HOTKEY_FOV_INC);
            UnregisterHotKey(this.Handle, HOTKEY_FOV_DEC);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            UnregisterHotkeys();

            focusCheckTimer.Stop();
            focusCheckTimer.Dispose();

            base.OnFormClosing(e);
        }
    }

    public class AppSettings
    {
        public Keys EnablePathingHotkey { get; set; } = Keys.M;
        public Keys StopPathingHotkey { get; set; } = Keys.N;
        public Keys AddPointHotkey { get; set; } = Keys.K;
        public Keys RollIncHotkey { get; set; } = Keys.P;
        public Keys RollDecHotkey { get; set; } = Keys.O;
        public Keys FovIncHotkey { get; set; } = Keys.I;
        public Keys FovDecHotkey { get; set; } = Keys.U;
    }
}