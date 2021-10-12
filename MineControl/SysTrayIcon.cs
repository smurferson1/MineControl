using System;
using System.Drawing;
using System.Windows.Forms;

namespace MineControl
{
    public static class SysTrayIcon
    {
        public static Color GPUColor { get; set; } = Color.Transparent;
        public static Color CPUColor { get; set; } = Color.Transparent;
        public static int GPUPowerStep { get; set; } = -1;

        // TODO: this is duplicated
        private static Properties.Settings settings = Properties.Settings.Default;

        /// <summary>
        /// Updates icon, but only if it's different from before
        /// </summary>
        /// <param name="notifyIcon"></param>
        /// <param name="forceRedraw"></param>
        /// <param name="gpuState"></param>
        /// <param name="cpuState"></param>
        /// <returns>True if icon was updated</returns>
        public static bool UpdateTextIcon(NotifyIcon notifyIcon, bool forceRedraw, MinerState gpuState, MinerState cpuState)
        {
            Color gpuColor = GetColorFromMinerState(gpuState);
            Color cpuColor = GetColorFromMinerState(cpuState);

            // only update the icon if it ***needs*** to be redrawn (this is to save resources)
            switch ((SysTrayIconTextMode)settings.generalSysTrayDisplayMode)
            {
                case SysTrayIconTextMode.MinerActiveStatus:
                    if ((forceRedraw) || (gpuColor != SysTrayIcon.GPUColor) || (cpuColor != SysTrayIcon.CPUColor))
                    {
                        SysTrayIcon.GPUColor = gpuColor;
                        SysTrayIcon.CPUColor = cpuColor;
                        SysTrayIcon.SetTextIcon(
                            notifyIcon,
                            "G",
                            "C",
                            gpuColor,
                            cpuColor,
                            Color.Transparent,
                            Color.Transparent,
                            Color.Green);
                        return true;
                    }
                    break;

                case SysTrayIconTextMode.GPUPowerStep:
                    if ((forceRedraw) || (SysTrayIcon.GPUPowerStep != settings.tempSpeedStep) || (gpuColor != SysTrayIcon.GPUColor) || (cpuColor != SysTrayIcon.CPUColor))
                    {
                        SysTrayIcon.GPUColor = gpuColor;
                        SysTrayIcon.CPUColor = cpuColor;
                        SysTrayIcon.GPUPowerStep = settings.tempSpeedStep;
                        SysTrayIcon.SetTextIcon(
                            notifyIcon,
                            SysTrayIcon.GPUPowerStep.ToString(),
                            "",
                            Color.White,
                            Color.Transparent,
                            gpuColor,
                            cpuColor,
                            Color.White);
                        return true;
                    }
                    break;
            }

            return false;
        }

        private static Color GetColorFromMinerState(MinerState minerState)
        {
            switch (minerState)
            {
                case MinerState.Uninitialized:
                    return Color.Black;
                case MinerState.Running:
                    return Color.LimeGreen;
                case MinerState.DisabledByUser:
                    return Color.Black;
                case MinerState.DisabledBySchedule:
                    return Color.DarkGray;
                case MinerState.DisabledByUnknownTemp:
                    return Color.Orange;
                case MinerState.DisabledByOverheating:
                    return Color.Orange;
                case MinerState.DisabledByUserActivity:
                    return Color.Yellow;
                case MinerState.DisabledUnknownError:
                    return Color.Red;
                case MinerState.DisabledClosing:
                    return Color.Black;
                default:
                    return Color.Red;
            }
        }

        // modified example from: https://stackoverflow.com/questions/36379547/writing-text-to-the-system-tray-instead-of-an-icon
        private static void SetTextIcon(NotifyIcon notifyIcon, string char1, string char2, Color char1Color, Color char2Color, Color backgroundColorLeft, Color backgroundColorRight, Color borderColor)
        {
            Brush brushTextShadow = new SolidBrush(Color.Black);
            Brush brush1 = new SolidBrush(char1Color);
            Brush brush2 = new SolidBrush(char2Color);
            Brush brushBackgroundLeft = new SolidBrush(backgroundColorLeft);
            Brush brushBackgroundRight = new SolidBrush(backgroundColorRight);
            Bitmap bitmapText = new Bitmap(16, 16);
            Graphics g = System.Drawing.Graphics.FromImage(bitmapText);
            Pen p = new Pen(borderColor, 1);

            try
            {
                IntPtr hIcon;
                g.Clear(Color.Transparent);
                g.FillRectangle(brushBackgroundLeft, new Rectangle(0, 0, 8, 16));
                g.FillRectangle(brushBackgroundRight, new Rectangle(8, 0, 8, 16));
                g.FillRectangle(Brushes.Black, new Rectangle(3, 0, 10, 16));
                //g.DrawRectangle(p, new Rectangle(0, 0, 15, 15));
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                if ((char1.Length > 0) && (char2.Length > 0))
                {
                    Font fontToUse = new Font("Tahoma", 11, FontStyle.Bold, GraphicsUnit.Pixel);
                    //Size sizeOfText = TextRenderer.MeasureText(char1, fontToUse);                    
                    //g.FillRectangle(Brushes.Black, new Rectangle(new Point(-1, 1), sizeOfText));
                    
                    //g.DrawString(char1[0].ToString(), fontToUse, brushTextShadow, -1, 0);
                    g.DrawString(char1[0].ToString(), fontToUse, brush1, -1, 1);
                    //g.DrawString(char2[0].ToString(), fontToUse, brushTextShadow, 7, 0);
                    g.DrawString(char2[0].ToString(), fontToUse, brush2, 7, 1);
                }
                else if ((char1.Length) > 0)
                {
                    Font fontToUse = new Font("Tahoma", 15, FontStyle.Bold, GraphicsUnit.Pixel);
                    //Size sizeOfText = TextRenderer.MeasureText(char1, fontToUse);
                    //g.FillRectangle(Brushes.Black, new Rectangle(new Point(0, -1), sizeOfText));
                    
                    //g.DrawString(char1[0].ToString(), fontToUse, brushTextShadow, -1, -1);
                    //g.DrawString(char1[0].ToString(), fontToUse, brushTextShadow, 1, -1);
                    //g.DrawString(char1[0].ToString(), fontToUse, brushTextShadow, -1, 0);
                    //g.DrawString(char1[0].ToString(), fontToUse, brushTextShadow, 1, 0);
                    g.DrawString(char1[0].ToString(), fontToUse, brush1, 0, -1);
                }
                else if ((char2.Length) > 0)
                {
                    Font fontToUse = new Font("Tahoma", 15, FontStyle.Bold, GraphicsUnit.Pixel);
                    //Size sizeOfText = TextRenderer.MeasureText(char2, fontToUse);
                    //g.FillRectangle(Brushes.Black, new Rectangle(new Point(0, -1), sizeOfText));
                    
                    //g.DrawString(char2[0].ToString(), fontToUse, brushTextShadow, -1, -1);
                    //g.DrawString(char2[0].ToString(), fontToUse, brushTextShadow, 1, -1);
                    //g.DrawString(char2[0].ToString(), fontToUse, brushTextShadow, -1, 0);
                    //g.DrawString(char2[0].ToString(), fontToUse, brushTextShadow, 1, 0);
                    g.DrawString(char2[0].ToString(), fontToUse, brush2, 0, -1);
                }
                hIcon = (bitmapText.GetHicon());
                notifyIcon.Icon = System.Drawing.Icon.FromHandle(hIcon);
            }
            finally
            {
                g.Dispose();
            }
        }
    }
}
