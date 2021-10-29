using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace MineControl.Lib
{
    public static class SysTrayIcon
    {
        public static Color GPUColor { get; set; } = Color.Transparent;
        public static Color CPUColor { get; set; } = Color.Transparent;
        public static int GPUPowerStep { get; set; } = -1;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        /// <summary>
        /// Updates icon, but only if it's different from before
        /// </summary>        
        /// <returns>True if icon was updated</returns>
        [SupportedOSPlatform("windows")]
        public static bool UpdateTextIcon(NotifyIcon notifyIcon, bool forceRedraw, MinerState gpuState, MinerState cpuState, int gpuPowerStep, SysTrayIconTextMode iconDisplayMode)
        {
            Color gpuColor = GetColorFromMinerState(gpuState);
            Color cpuColor = GetColorFromMinerState(cpuState);

            // only update the icon if it ***needs*** to be redrawn (this is to save resources)
            switch (iconDisplayMode)
            {
                // old code for different display info
                /*case SysTrayIconTextMode.MinerActiveStatus:
                    if ((forceRedraw) || (gpuColor != GPUColor) || (cpuColor != CPUColor))
                    {
                        GPUColor = gpuColor;
                        CPUColor = cpuColor;                        
                        SetTextIcon(
                            notifyIcon,
                            'G',
                            'C',
                            gpuColor,
                            cpuColor,
                            Color.Black,
                            Color.Black);
                        return true;
                    }
                    break;*/

                case SysTrayIconTextMode.MinerActiveStatus:
                case SysTrayIconTextMode.GPUPowerStep:
                    if (forceRedraw || (GPUPowerStep != gpuPowerStep) || (gpuColor != GPUColor) || (cpuColor != CPUColor))
                    {
                        GPUColor = gpuColor;
                        CPUColor = cpuColor;
                        GPUPowerStep = gpuPowerStep;
                        SetTextIcon(
                            notifyIcon,
                            GPUPowerStep.ToString()[0],
                            null,
                            Color.White,
                            Color.Transparent,
                            gpuColor,
                            cpuColor);
                        return true;
                    }
                    break;
            }

            return false;
        }

        private static Color GetColorFromMinerState(MinerState minerState)
        {
            return minerState switch
            {
                MinerState.Uninitialized => Color.Black,
                MinerState.Running => Color.LimeGreen,
                MinerState.DisabledByUser => Color.Black,
                MinerState.DisabledBySchedule => Color.DarkGray,
                MinerState.DisabledByUnknownTemp => Color.Orange,
                MinerState.DisabledByOverheating => Color.Orange,
                MinerState.DisabledByUserActivity => Color.Yellow,
                MinerState.DisabledUnknownError => Color.Red,
                MinerState.DisabledClosing => Color.Black,
                _ => Color.Red,
            };
        }

        /// <summary>
        /// Creates an image using the provided chars and colors, and applies it to the provided NotifyIcon.
        /// If only one of the chars is present, that char is drawn in the center at a larger font.
        /// Provides option for colored bars on left and right as well.
        /// </summary>        
        [SupportedOSPlatform("windows")]
        private static void SetTextIcon(NotifyIcon notifyIcon, char? char1, char? char2, Color char1Color, Color char2Color, Color barColorLeft, Color barColorRight)
        {
            using Brush brush1 = new SolidBrush(char1Color);
            using Brush brush2 = new SolidBrush(char2Color);
            using Brush brushBarLeft = new SolidBrush(barColorLeft);
            using Brush brushBarRight = new SolidBrush(barColorRight);
            using Bitmap bitmap = new(16, 16);
            using Graphics g = Graphics.FromImage(bitmap);

            // fill background and bars
            g.Clear(Color.Transparent);
            g.FillRectangle(brushBarLeft, new Rectangle(0, 0, 8, 16));
            g.FillRectangle(brushBarRight, new Rectangle(8, 0, 8, 16));
            g.FillRectangle(Brushes.Black, new Rectangle(3, 0, 10, 16));

            // draw chars if we have any
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            if (char1 != null && char2 != null)
            {
                using Font font = new("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);
                g.DrawString(char1.ToString(), font, brush1, -1, 1);
                g.DrawString(char2.ToString(), font, brush2, 7, 1);
            }
            else if (char1 != null || char2 != null)
            {
                using Font font = new("Tahoma", 15, FontStyle.Bold, GraphicsUnit.Pixel);
                g.DrawString(
                    char1 != null ? char1.ToString() : char2.ToString(),
                    font,
                    char1 != null ? brush1 : brush2,
                    0,
                    -1);
            }

            // set the new icon and dispose of the old one at the end to avoid memory leaks
            IntPtr hIcon = bitmap.GetHicon();
            Icon previousIcon = notifyIcon.Icon;
            notifyIcon.Icon = Icon.FromHandle(hIcon);
            previousIcon.Dispose();
            DestroyIcon(hIcon);
        }
    }
}
