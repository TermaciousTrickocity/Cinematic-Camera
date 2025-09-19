﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace modularDollyCam
{
    public partial class MainForm : Form
    {
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private const int VK_I = 0x49;
        private const int VK_K = 0x4B;
        private const int VK_M = 0x4D;
        private const int VK_N = 0x4E;
        private const int VK_O = 0x4F;
        private const int VK_P = 0x50;
        private const int VK_U = 0x55;
        private const int VK_Home = 0x24;

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                    switch (vkCode)
                    {
                        case VK_M:
                            pathStart_checkbox.Checked = true;
                            break;
                        case VK_N:
                            pathStart_checkbox.Checked = false;
                            break;
                        case VK_P:
                            memory.WriteMemory(rollAng, "float", (memory.ReadFloat(rollAng) + 0.1f).ToString());
                            break;
                        case VK_O:
                            memory.WriteMemory(rollAng, "float", (memory.ReadFloat(rollAng) - 0.1f).ToString());
                            break;
                        case VK_I:
                            memory.WriteMemory(playerFov, "float", (memory.ReadFloat(playerFov) + 1f >= 145 ? "145.0" : (memory.ReadFloat(playerFov) + 1f).ToString()));
                            break;
                        case VK_U:
                            memory.WriteMemory(playerFov, "float", (memory.ReadFloat(playerFov) - 1f <= 5 ? "5.0" : (memory.ReadFloat(playerFov) - 1f).ToString()));
                            break;
                        case VK_K:
                            AddKeyPointRow(memory.ReadFloat(xPos), memory.ReadFloat(yPos), memory.ReadFloat(zPos), memory.ReadFloat(yawAng), memory.ReadFloat(pitchAng), memory.ReadFloat(rollAng), memory.ReadFloat(playerFov), 1);
                            break;
                        case VK_Home:
                            Console.WriteLine("home!");
                            break;
                        default:
                            break;
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

    }
}
