using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            if (!File.Exists(Config.OptionsPath))
            {
                MessageBoxResult ms = MessageBox.Show("Could not find Option.mco, do you want to search for it?", "Option.mco not Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ms == MessageBoxResult.Yes)
                {
                    OpenFileDialog fl = new OpenFileDialog()
                    {
                        Filter = "Option.mco (Option.mco)|Option.mco",
                        Title = "Select Option.mco",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Multiselect = false
                    };
                    if (fl.ShowDialog() == true)
                    {
                        Config.OptionsPath = fl.FileName;
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Close();
                }
            }
            if (!File.Exists(Config.SoundOptionsPath))
            {
                MessageBoxResult ms = MessageBox.Show("Could not find OptionSound.mco, do you want to search for it?", "OptionSound.mco not Found", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ms == MessageBoxResult.Yes)
                {
                    OpenFileDialog fl = new OpenFileDialog()
                    {
                        Filter = "OptionSound.mco (OptionSound.mco)|OptionSound.mco",
                        Title = "Select OptionSound.mco",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        Multiselect = false
                    };
                    if (fl.ShowDialog() == true)
                    {
                        Config.SoundOptionsPath = fl.FileName;
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    Close();
                }
            }
        }
        private async Task M()
        {
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
            slSoundMaster.Value = OptionSound.masVol.Limit(0, 100);
            slSoundBGM.Value = OptionSound.bgmVol.Limit(0, 100);
            slSoundSFX.Value = OptionSound.sfxVol.Limit(0, 100);
            slSoundENV.Value = OptionSound.envVol.Limit(0, 100);
            cmbResolution.LoadRes();
            tCurrentRes.Text = $"Current Resolution: {Options.X}x{Options.Y}";

            //Disable those who dont work.
            chkAntialiasing.IsEnabled = false;
            chkShowInterface.IsEnabled = false;
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CmbRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem m = ((ComboBoxItem)((ComboBox)sender).SelectedItem);
            cmbResolution.LoadRes((Ratio)int.Parse(m.Tag.ToString()));

        }

        private void CmbResolution_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem m = ((ComboBoxItem)((ComboBox)sender).SelectedItem);
            tCurrentRes.Text = $"Current Resolution: {m.Content}";
            string[] res = m.Tag.ToString().Split('x');
            Options.X = short.TryParse(res[0], out short X) ? X : (short)800;
            Options.Y = short.TryParse(res[1], out short Y) ? Y : (short)600;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Options.CharacterEffect = chkCharEffect.IsChecked == true;
            Options.GlowEffect = Convert.ToInt16(chkCharGlowEffect.IsChecked == true);
            Options.CharacterOutline = Convert.ToInt16(chkCharOutlineEffect.IsChecked == true);
            Options.ScreenVibrationEffect = chkVibrationEffect.IsChecked == true;
            Options.WindowMode = chkWindowMode.IsChecked == true;
            Options.ShowInterface = Convert.ToInt16(chkShowInterface.IsChecked == true);
            OptionSound.masVol = Convert.ToInt16(slSoundMaster.Value).Limit(0, 100);
            OptionSound.bgmVol = Convert.ToInt16(slSoundBGM.Value).Limit(0, 100);
            OptionSound.sfxVol = Convert.ToInt16(slSoundSFX.Value).Limit(0, 100);
            OptionSound.envVol = Convert.ToInt16(slSoundENV.Value).Limit(0, 100);
            MCOWriter.Write();
        }

        private void SlCharShadow_SizeChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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
            tCharShadow.Text = $"x{s.Value + 1}";
        }

        private void BtnInfo_Click(object sender, RoutedEventArgs e)
        {
            new Info().ShowDialog();
        }
    }
}
