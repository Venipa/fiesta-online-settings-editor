using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Fiesta_Resolution_Changer.Classes
{
    class Resolutions
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;

            public int dmFields;
            public short dmOrientation;
            public short dmPaperSize;
            public short dmPaperLength;

            public short dmPaperWidth;
            public short dmScale;
            public short dmCopies;
            public short dmDefaultSource;
            public short dmPrintQuality;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;
            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;

            public int dmPelsHeight;
            public int dmDisplayFlags;

            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;

            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }
        [DllImport("user32.dll", EntryPoint = "EnumDisplaySettingsA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        private static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
        [DllImport("user32.dll", EntryPoint = "ChangeDisplaySettingsExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        private static extern Int32 ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, Int32 hwnd, Int32 dwflags, Int32 lParam);

        public struct ScreenResolution
        {
            public string Total;
            public string Slim;
            public int height;
            public int width;
        }

        private List<ScreenResolution> ScreenRes = new List<ScreenResolution>();

        private string deviceName;
        public Resolutions(string devName)
        {
            deviceName = devName;
            DEVMODE dm = new DEVMODE
            {
                dmDeviceName = new string(new char[32]),
                dmFormName = new string(new char[32])
            };
            dm.dmSize = Convert.ToInt16(Marshal.SizeOf(dm));

            int counter = 0;
            do
            {
                if (EnumDisplaySettings(deviceName, counter, ref dm) != 0)
                {
                    ScreenResolution sr = new ScreenResolution
                    {
                        Total = dm.dmPelsWidth + " x " + dm.dmPelsHeight,
                        width = dm.dmPelsWidth,
                        height = dm.dmPelsHeight
                    };

                    sr.Slim = sr.width + "x" + sr.height;
                    if (!ScreenRes.Contains(sr))
                    {
                        ScreenRes.Add(sr);
                    }
                    sr.Total = null;
                    counter += 1;
                }
                else
                {
                    break;
                }
            } while (true);
        }

        public List<ScreenResolution> ScreenResolutions
        {
            get { return ScreenRes; }
        }
    }
}
