using System;
using HandyControl.Controls;
using HandyControl.Data;

namespace WebPConverter.Code {
    internal class Toaster {
        internal static void SettingsReset() => Growl.Info(GetGrowlInfo("Settings Reset! Make sure to save your settings!"));
        internal static void SettingsSaved() => Growl.Info(GetGrowlInfo("Settings Saved!"));
        internal static void FailedToConvertImage(Exception exception) => Growl.Warning(GetGrowlInfo($"Failed to convert an image! {exception.Message}"));
        internal static void FinishedConversion() => Growl.Success(GetGrowlInfo("Finished with the conversion!"));
        private static GrowlInfo GetGrowlInfo(string message) {
            return new GrowlInfo(){
                Message = message,
                ShowDateTime = false
            };
        }
        
    }
}
