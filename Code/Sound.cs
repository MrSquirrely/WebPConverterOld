using System;
using System.Media;

namespace WebPConverter.Code {
    internal class Sound {
        internal static SoundPlayer HitSoundPlayer = new SoundPlayer($"{Environment.CurrentDirectory}\\Files\\Hit.wav");
        internal static SoundPlayer PizzaSoundPlayer = new SoundPlayer($"{Environment.CurrentDirectory}\\Files\\Pizza.wav");
        internal static SoundPlayer SaxSoundPlayer = new SoundPlayer($"{Environment.CurrentDirectory}\\Files\\Sax.wav");
        internal static SoundPlayer SteelSoundPlayer = new SoundPlayer($"{Environment.CurrentDirectory}\\Files\\Steel.wav");

        internal static void PlaySound(SoundPlayer soundPlayer) => soundPlayer?.Play();
    }
}
