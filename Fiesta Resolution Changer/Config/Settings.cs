using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fiesta_Resolution_Changer.Settings
{
    public class Config
    {
        public static string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string OptionsPath = BaseDir + "\\ressystem\\Option.mco";
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
            List<string> resolutions = new List<string>();
            string r = string.Empty;
            switch(rat)
            {
                case Ratio.UltraWideScreen:
                    {
                        r = Properties.Resources._16x10;
                        break;
                    }
                case Ratio.BlockScreen:
                    {
                        r = Properties.Resources._4x3;
                        break;
                    }
                case Ratio.WideScreen:
                default:
                    {
                        r = Properties.Resources._16x9;
                        break;
                    }
            }
            resolutions = r.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            cmd.Items.Clear();
            foreach(string res in resolutions)
            {
                cmd.Items.Add(new ComboBoxItem()
                {
                    Content = res
                });
            }
        }
    }
}
