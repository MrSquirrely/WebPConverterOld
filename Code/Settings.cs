using System.IO;
using System.Text.Json.Serialization;

namespace WebPConverter.Code {
    internal class Settings {
        #region WebP Settings
        internal static bool Lossless { get; set; } = true;
        internal static bool RemoveAlpha { get; set; }
        internal static bool EmulateJpeg { get; set; }
        internal static string Quality { get; set; } = "80";
        #endregion

        #region Software Settings
        internal static bool DeleteImage { get; set; } = true;
        internal static bool PlaySound { get; set; } = true;
        internal static int SoundToPlay { get; set; }
        #endregion


        internal static void Save() {
            if (File.Exists("webp.settings")) File.Delete("webp.settings");
            JSettings jSettings = new JSettings{
                Lossless = Lossless, 
                RemoveAlpha = RemoveAlpha, 
                EmulateJpeg = EmulateJpeg, 
                Quality = Quality, 
                DeleteImage = DeleteImage,
                PlaySound = PlaySound,
                SoundToPlay = SoundToPlay
            };
            string json = JsonSerializer.ToString(jSettings);
            StreamWriter writer = new StreamWriter("webp.settings");
            writer.Write(json);
            writer.Close();
        }

        internal static void Load() {
            if (!File.Exists("webp.settings")) return;
            StreamReader reader = new StreamReader("webp.settings");
            string json = reader.ReadToEnd();
            reader.Close();
            JSettings jSettings = JsonSerializer.Parse<JSettings>(json);

            Lossless = jSettings.Lossless;
            RemoveAlpha = jSettings.RemoveAlpha;
            EmulateJpeg = jSettings.EmulateJpeg;
            Quality = jSettings.Quality;
            DeleteImage = jSettings.DeleteImage;
            PlaySound = jSettings.PlaySound;
            SoundToPlay = jSettings.SoundToPlay;
        }

    }
}
