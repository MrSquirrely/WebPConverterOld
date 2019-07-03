using System.Collections.Generic;
using System.Collections.ObjectModel;
using Squirrel_Logger;

namespace WebPConverter.Code {
    internal class Reference {
        internal static Logger logger = new Logger("WebP");
        internal static ObservableCollection<ImageFile> ImageCollection { get; } = new ObservableCollection<ImageFile>();
        internal static readonly List<string> ImageTypes = new List<string>() { ".png", ".jpeg", ".jpg", ".exif", ".tiff", ".bmp" };

        internal static bool IsSettingsOpen = false;
        internal static bool IsBugsOpen = false;
        internal static bool IsFeaturesOpen = false;

    }
}
