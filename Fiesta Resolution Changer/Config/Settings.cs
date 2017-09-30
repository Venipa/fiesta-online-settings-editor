using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Fiesta_Resolution_Changer.Classes;
using System.Diagnostics;

namespace Fiesta_Resolution_Changer.Settings
{
    public class Config
    {
        public static string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string OptionsPath = BaseDir + "\\ressystem\\Option.mco";
        public static string SoundOptionsPath = BaseDir + "\\ressystem\\OptionSound.mco";
    }
    public enum Ratio : int
    {
        WideScreen = 0,
        UltraWideScreen = 1,
        BlockScreen = 2
    }
    public static class Extensions
    {

        public static void loadResx(this ComboBox cmb)
        {
            List<string> words = Properties.Resources.ratios.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            cmb.Items.Clear();
            foreach (string word in words)
            {
                string[] k = word.Split('|');
                if(k.Length > 1)
                {
                    cmb.Items.Add(new ComboBoxItem()
                    {
                        Content = k[1],
                        Tag = k[0],
                        IsSelected = k[0] == "0"
                    });
                }
            }
        }
        public static void loadRes(this ComboBox cmd, Ratio rat = Ratio.WideScreen)
        {
            Resolutions sRes = new Resolutions(System.Windows.Forms.Screen.PrimaryScreen.DeviceName);
            cmd.Items.Clear();
            foreach(Resolutions.ScreenResolution res in sRes.ScreenResolutions)
            {
#if DEBUG
                Debug.WriteLine(res.Total.ToString());
#endif
                cmd.Items.Add(new ComboBoxItem()
                {
                    Content = res.Total.ToString()
                });
            }
        }
        public static int limit(this int i, int min, int max, int Default = 0)
        {
            return i > max || i < min ? Default : i;
        }
        public static short limit(this short i, short min, short max, short Default = 0)
        {
            return i > max || i < min ? Default : i;
        }
    }
}
