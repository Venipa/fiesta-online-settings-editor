using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiesta_Resolution_Changer.Settings;
using System.IO;

namespace Fiesta_Resolution_Changer.Classes
{
    class MCOWriter
    {
        public static void Write()
        {
            try
            {
                long lPos = 0;
                BinaryWriter bWriter = new BinaryWriter((Stream)File.Open(Config.OptionsPath, FileMode.Open, FileAccess.Write));

                while (bWriter.BaseStream.Position != bWriter.BaseStream.Length)
                {
                    bWriter.BaseStream.Seek(lPos, SeekOrigin.Begin);
                    switch (Enum.Parse(typeof(MCOEnum), lPos.ToString()))
                    {
                        case MCOEnum.WindowX:
                            bWriter.Write(Options.X);
                            break;
                        case MCOEnum.WindowY:
                            bWriter.Write(Options.Y);
                            break;
                        case MCOEnum.CharacterShadow:
                            bWriter.Write(Options.CharacterShadow);
                            break;
                        case MCOEnum.CharacterOutline:
                            bWriter.Write(Options.CharacterOutline);
                            break;
                        case MCOEnum.WindowMode:
                            bWriter.Write(Options.WindowMode);
                            break;
                        case MCOEnum.Antialiasing:
                            bWriter.Write(Options.Antialiasing);
                            break;
                        case MCOEnum.GlowEffect:
                            bWriter.Write(Options.GlowEffect);
                            break;
                        case MCOEnum.ScreenVibrationEffect:
                            bWriter.Write(Options.ScreenVibrationEffect);
                            break;
                        case MCOEnum.CharacterEffect:
                            bWriter.Write(Options.CharacterEffect);
                            break;
                        case MCOEnum.ShowInterface:
                            bWriter.Write(Options.ShowInterface);
                            break;
                    }
                    lPos++;
                }

                bWriter.Dispose();
                bWriter.Close();

                //OptionSound.mco
                lPos = 0;
                bWriter = new BinaryWriter((Stream)File.Open(Config.SoundOptionsPath, FileMode.Open, FileAccess.Write));
                while (bWriter.BaseStream.Position != bWriter.BaseStream.Length)
                {
                    bWriter.BaseStream.Seek(lPos, SeekOrigin.Begin);
                    switch (Enum.Parse(typeof(MCOEnum), lPos.ToString()))
                    {
                        case MCOEnum.MasterVolume:
                            bWriter.Write(OptionSound.masVol);
                            break;
                        case MCOEnum.BGMVolume:
                            bWriter.Write(OptionSound.bgmVol);
                            break;
                        case MCOEnum.SFXVolume:
                            bWriter.Write(OptionSound.sfxVol);
                            break;
                        case MCOEnum.EnvVolume:
                            bWriter.Write(OptionSound.envVol);
                            break;
                    }
                    lPos++;
                }
                bWriter.Dispose();
                bWriter.Close();
            }
            catch (Exception ex)
            {
                Throw.Message(ex.Message);
            }
        }
    }
}
