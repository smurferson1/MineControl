﻿using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace MineControl.Lib.WinAPI
{
    public static class SysTrayTooltipReader
    {
        // Modified code originally from: https://stackoverflow.com/questions/51887744/trouble-implementing-code-example-using-pinvoke-declarations/51892012?noredirect=1#comment90762185_51892012
        public static string GetAllSysTrayToolbarText()
        {
            StringBuilder sb = new();

            var handle = GetSystemTrayHandle();
            if (handle == IntPtr.Zero)
                return "";

            var count = SendMessage(handle, TB_BUTTONCOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32();
            if (count == 0)
                return "";

            GetWindowThreadProcessId(handle, out var pid);
            var hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, pid);
            if (hProcess == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var size = (IntPtr)Marshal.SizeOf<TBButtonInfoW>();
            var buffer = VirtualAllocEx(hProcess, IntPtr.Zero, size, MEM_COMMIT, PAGE_READWRITE);
            if (buffer == IntPtr.Zero)
            {
                CloseHandle(hProcess);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            for (int i = 0; i < count; i++)
            {
                var btn = new TBButtonInfoW();
                btn.cbSize = size.ToInt32();
                btn.dwMask = TBIF_BYINDEX | TBIF_COMMAND;
                if (WriteProcessMemory(hProcess, buffer, ref btn, size, out var written))
                {
                    // we want the identifier
                    var res = SendMessage(handle, TB_GETBUTTONINFOW, (IntPtr)i, buffer);
                    if (res.ToInt32() >= 0 && ReadProcessMemory(hProcess, buffer, ref btn, size, out var read))
                    {
                        // now get display text using the identifier
                        // first pass we ask for size
                        var textSize = SendMessage(handle, TB_GETBUTTONTEXTW, (IntPtr)btn.idCommand, IntPtr.Zero);
                        if (textSize.ToInt32() != -1)
                        {
                            // we need to allocate for the terminating zero and unicode
                            var utextSize = (IntPtr)((1 + textSize.ToInt32()) * 2);
                            var textBuffer = VirtualAllocEx(hProcess, IntPtr.Zero, utextSize, MEM_COMMIT, PAGE_READWRITE);
                            if (textBuffer != IntPtr.Zero)
                            {
                                res = SendMessage(handle, TB_GETBUTTONTEXTW, (IntPtr)btn.idCommand, textBuffer);
                                if (res == textSize)
                                {
                                    var localBuffer = Marshal.AllocHGlobal(utextSize.ToInt32());
                                    if (ReadProcessMemory(hProcess, textBuffer, localBuffer, utextSize, out read))
                                    {
                                        var text = Marshal.PtrToStringUni(localBuffer);
                                        sb.AppendLine(text);
                                    }
                                    Marshal.FreeHGlobal(localBuffer);
                                }
                                VirtualFreeEx(hProcess, textBuffer, IntPtr.Zero, MEM_RELEASE);
                            }
                        }
                    }
                }
            }

            VirtualFreeEx(hProcess, buffer, IntPtr.Zero, MEM_RELEASE);
            CloseHandle(hProcess);

            return sb.ToString();
        }

        private static IntPtr GetSystemTrayHandle()
        {
            var hwnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", null);
            hwnd = FindWindowEx(hwnd, IntPtr.Zero, "TrayNotifyWnd", null);
            hwnd = FindWindowEx(hwnd, IntPtr.Zero, "SysPager", null);
            return FindWindowEx(hwnd, IntPtr.Zero, "ToolbarWindow32", null);
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref TBButtonInfoW lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, ref TBButtonInfoW lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, IntPtr nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("user32", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("kernel32", SetLastError = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, int flAllocationType, int flProtect);

        [DllImport("kernel32", SetLastError = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, int dwFreeType);

        [DllImport("user32")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string lpClassName, string lpWindowName);

        private const int TBIF_BYINDEX = unchecked((int)0x80000000); // this specifies that the wparam in Get/SetButtonInfo is an index, not id
        private const int TBIF_COMMAND = 0x20;
        private const int MEM_COMMIT = 0x1000;
        private const int MEM_RELEASE = 0x8000;
        private const int PAGE_READWRITE = 0x4;
        private const int TB_GETBUTTONINFOW = 1087;
        private const int TB_GETBUTTONTEXTW = 1099;
        private const int TB_BUTTONCOUNT = 1048;

        private static bool IsWindowsVistaOrAbove() => Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6;
        private static int PROCESS_ALL_ACCESS => IsWindowsVistaOrAbove() ? 0x001FFFFF : 0x001F0FFF;

        [StructLayout(LayoutKind.Sequential)]
        private struct TBButtonInfoW
        {
            public int cbSize;
            public int dwMask;
            public int idCommand;
            public int iImage;
            public byte fsState;
            public byte fsStyle;
            public short cx;
            public IntPtr lParam;
            public IntPtr pszText;
            public int cchText;
        }
    }
}
