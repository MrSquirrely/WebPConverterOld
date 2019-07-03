using System;
using System.IO;
using ImageMagick;

namespace WebPConverter.Code {
    internal class Converter {
        private const MagickFormat Webp = MagickFormat.WebP;

        internal static void Convert() {

            foreach (ImageFile imageFile in Reference.ImageCollection) {
                string image = $"{imageFile.FileLocation}\\{imageFile.FileName}";
                string imageExt = $"{image}{imageFile.FileType}";

                try {
                    if (imageFile.FileType != ".webp") {
                        MagickImage magickImage = new MagickImage(imageExt);
                        magickImage.Settings.SetDefine(Webp, "-lossless", Settings.Lossless); // bool
                        magickImage.Settings.SetDefine(Webp, "-emulate-jpeg-size", Settings.EmulateJpeg);// bool
                        magickImage.Settings.SetDefine(Webp, "-alpha", Settings.RemoveAlpha);// bool
                        magickImage.Settings.SetDefine(Webp, "-quality", Settings.Quality);// string
                        magickImage.Format = Webp;
                        magickImage.Write($"{image}.webp");
                    }
                    RemoveImage($"{imageFile.FileName}{imageFile.FileType}", imageFile.FileLocation);
                }
                catch (Exception ex) {
                    Toaster.FailedToConvertImage(ex);
                }
            }
        }

        private static void RemoveImage(string image, string imageFileLocation) {
            if (Settings.DeleteImage) {
                File.Delete($"{imageFileLocation}\\{image}");
            }
        }
    }
}
