using System;
using Fiesta_Resolution_Changer.Settings;
using System.IO;

namespace Fiesta_Resolution_Changer.Classes
{
    class MCOReader
    {
        public static void Read()
        {
            try
            {
                long lPos = 0;
                BinaryReader bReader = new BinaryReader((Stream)File.Open(Config.OptionsPath, FileMode.Open, FileAccess.Read));

                while (bReader.BaseStream.Position != bReader.BaseStream.Length)
                {
                    bReader.BaseStream.Seek(lPos, SeekOrigin.Begin);
                    switch (Enum.Parse(typeof(MCOEnum), lPos.ToString()))
                    {
                        case MCOEnum.WindowX:
                            Options.X = bReader.ReadInt16();
                            break;
                        case MCOEnum.WindowY:
                            Options.Y = bReader.ReadInt16();
                            break;
                        case MCOEnum.CharacterShadow:
                            Options.CharacterShadow = bReader.ReadInt16();
                            break;
                        case MCOEnum.CharacterOutline:
                            Options.CharacterOutline = bReader.ReadInt16();
                            break;
                        case MCOEnum.WindowMode:
                            Options.WindowMode = bReader.ReadBoolean();
                            break;
                        case MCOEnum.Antialiasing:
                            Options.Antialiasing = bReader.ReadInt16();
                            break;
                        case MCOEnum.GlowEffect:
                            Options.GlowEffect = bReader.ReadInt16();
                            break;
                        case MCOEnum.ScreenVibrationEffect:
                            Options.ScreenVibrationEffect = bReader.ReadBoolean();
                            break;
                        case MCOEnum.CharacterEffect:
                            Options.CharacterEffect = bReader.ReadBoolean();
                            break;
                        case MCOEnum.ShowInterface:
                            Options.ShowInterface = bReader.ReadInt16();
                            break;
                    }
                    lPos++;
                }

                bReader.Dispose();
                bReader.Close();

                //OptionSound.mco
                lPos = 0;
                bReader = new BinaryReader((Stream)File.Open(Config.OptionsPath, FileMode.Open, FileAccess.Read));
                while (bReader.BaseStream.Position != bReader.BaseStream.Length)
                {
                    bReader.BaseStream.Seek(lPos, SeekOrigin.Begin);
                    switch (Enum.Parse(typeof(MCOEnum), lPos.ToString()))
                    {
                        case MCOEnum.MasterVolume:
                            OptionSound.masVol = bReader.ReadInt16();
                            break;
                        case MCOEnum.BGMVolume:
                            OptionSound.bgmVol = bReader.ReadInt16();
                            break;
                        case MCOEnum.SFXVolume:
                            OptionSound.sfxVol = bReader.ReadInt16();
                            break;
                        case MCOEnum.EnvVolume:
                            OptionSound.envVol = bReader.ReadInt16();
                            break;
                    }
                    lPos++;
                }
                bReader.Dispose();
                bReader.Close();
            }
            catch (Exception ex)
            {
                Throw.Message(ex.Message);
            }
        }
    }
}
