using System;

namespace Fiesta_Resolution_Changer.Classes
{
    class Options
    {
        public static Int16 X { get; set; }
        public static Int16 Y { get; set; }
        public static Int16 CharacterShadow { get; set; }
        public static Int16 CharacterOutline { get; set; }
        public static bool WindowMode { get; set; }
        public static Int16 Antialiasing { get; set; }
        public static Int16 GlowEffect { get; set; }
        public static bool ScreenVibrationEffect { get; set; }
        public static bool CharacterEffect { get; set; }
        public static Int16 ShowInterface { get; set; }
    }
    class OptionSound
    {
        public static Int16 masVol { get; set; }
        public static Int16 bgmVol { get; set; }
        public static Int16 sfxVol { get; set; }
        public static Int16 envVol { get; set; }
    }
}
