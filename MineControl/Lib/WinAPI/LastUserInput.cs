using System;
using System.Runtime.InteropServices;

namespace MineControl.Lib.WinAPI
{
    public static class LastUserInput
    {
        // From https://www.pinvoke.net/default.aspx/user32.GetLastInputInfo
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LastInputInfo plii);

        [StructLayout(LayoutKind.Sequential)]
        struct LastInputInfo
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LastInputInfo));

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }

        public static uint GetLastInputTimeInSecs()
        {
            uint idleTime = 0;
            LastInputInfo lastInputInfo = new LastInputInfo();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            uint envTicks = (uint)Environment.TickCount;

            if (GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = lastInputInfo.dwTime;
                idleTime = envTicks - lastInputTick;
            }

            return (idleTime > 0) ? (idleTime / 1000) : 0;
        }
    }
}
