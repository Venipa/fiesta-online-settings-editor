using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fiesta_Resolution_Changer.Settings;
using Fiesta_Resolution_Changer.Classes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace Fiesta_Resolution_Changer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Exception ex = (Exception)e.ExceptionObject;
                MessageBox.Show($"Error :: {ex.Message}", "Error occurred");
#if DEBUG
                Debug.WriteLine(ex);
#else
                Application.Current.Shutdown();
#endif
            };
            Check().Wait();
            M().Wait();
        }
        private async Task Check()
        {
            if(!File.Exists(Config.OptionsPath))
            {
                MessageBoxResult ms = MessageBox.Show("Could not find Options.mco, do you want to search for it?", "Options.mco not Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(ms == MessageBoxResult.Yes)
                {
                    OpenFileDialog fl = new OpenFileDialog()
                    {
                        Filter = "Option.mco (Option.mco)|Option.mco",
                        Title = "Select Option.mco",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Multiselect = false
                    };
                    if(fl.ShowDialog() == true)
                    {
                        Config.OptionsPath = fl.FileName;
                    } else
                    {
                        Close();
                    }
                } else
                {
                    Close();
                }
            }
        }
        private async Task M()
        {
            cmbRatio.loadResx();
            MCOReader.Read();
            switch (Options.CharacterShadow)
            {
                case 0:
                    slCharShadow.Value = 0;
                    break;
                case 1:
                    slCharShadow.Value = 1;
                    break;
                case 10:
                    slCharShadow.Value = 2;
                    break;
                case 50:
                    slCharShadow.Value = 3;
                    break;
            }
            chkCharEffect.IsChecked = Options.CharacterEffect;
            chkCharGlowEffect.IsChecked = Convert.ToBoolean(Options.GlowEffect);
            chkCharOutlineEffect.IsChecked = Convert.ToBoolean(Options.CharacterOutline);
            chkVibrationEffect.IsChecked = Options.ScreenVibrationEffect;
            chkWindowMode.IsChecked = Options.WindowMode;
            chkAntialiasing.IsChecked = Convert.ToBoolean(Options.Antialiasing);
            tCurrentRes.Text = $"Current Resolution: {Options.X}x{Options.Y}";
            foreach(ComboBoxItem it in cmbResolution.Items)
            {
                if (it.Content == $"{Options.X}x{Options.Y}") it.IsSelected = true;
            }

            //Disable those who dont work.
            chkAntialiasing.IsEnabled = false;
            chkShowInterface.IsEnabled = false;
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(MouseButtonState.Pressed == e.LeftButton)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void cmbRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem m = ((ComboBoxItem)((ComboBox)sender).SelectedItem);
            cmbResolution.loadRes((Ratio)int.Parse(m.Tag.ToString()));
            
        }

        private void cmbResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem m = ((ComboBoxItem)((ComboBox)sender).SelectedItem);
            tCurrentRes.Text = $"Current Resolution: {m.Content}";
            string[] res = m.Content.ToString().Split('x');
            Options.X = short.TryParse(res[0], out short X) ? X : (short)800;
            Options.Y = short.TryParse(res[1], out short Y) ? Y : (short)600;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Options.CharacterEffect = chkCharEffect.IsChecked == true;
            Options.GlowEffect = Convert.ToInt16(chkCharGlowEffect.IsChecked == true);
            Options.CharacterOutline = Convert.ToInt16(chkCharOutlineEffect.IsChecked == true);
            Options.ScreenVibrationEffect = chkVibrationEffect.IsChecked == true;
            Options.WindowMode = chkWindowMode.IsChecked == true;
            Options.ShowInterface = Convert.ToInt16(chkShowInterface.IsChecked == true);
            MCOWriter.Write();
        }

        private void slCharShadow_SizeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider s = (Slider)sender;
            switch (s.Value)
            {
                case 0:
                    Options.CharacterShadow = 0;
                    break;
                case 1:
                    Options.CharacterShadow = 1;
                    break;
                case 2:
                    Options.CharacterShadow = 10;
                    break;
                case 3:
                    Options.CharacterShadow = 50;
                    break;
            }
            tCharShadow.Text = $"x{s.Value+1}";
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            new Info().ShowDialog();
        }
    }
}
