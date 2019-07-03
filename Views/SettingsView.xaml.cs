using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using HandyControl.Interactivity;
using WebPConverter.Code;

namespace WebPConverter.Views {
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView {
        private static readonly Regex Regex = new Regex("[^0-9]+");
        private bool _firstOpen = true;
        public SettingsView() {
            InitializeComponent();

            LosslessCheck.IsChecked = Settings.Lossless;
            RemoveAlphaCheck.IsChecked = Settings.RemoveAlpha;
            EmulateJpegCheck.IsChecked = Settings.EmulateJpeg;
            QualityTextBox.Text = Settings.Quality;
            DeleteImageCheck.IsChecked = Settings.DeleteImage;
            PlaySoundCheck.IsChecked = Settings.PlaySound;
            SoundCombo.SelectedIndex = Settings.SoundToPlay;

        }
        
        private void ResetButton_OnClick(object sender, RoutedEventArgs e) {
            LosslessCheck.IsChecked = true;
            RemoveAlphaCheck.IsChecked = false;
            EmulateJpegCheck.IsChecked = false;
            QualityTextBox.Text = "80";
            DeleteImageCheck.IsChecked = true;
            PlaySoundCheck.IsChecked = true;
            SoundCombo.SelectedIndex = 0;
            Toaster.SettingsReset();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e) {
            if (LosslessCheck.IsChecked != null) Settings.Lossless = LosslessCheck.IsChecked.Value;
            if (RemoveAlphaCheck.IsChecked != null) Settings.RemoveAlpha = RemoveAlphaCheck.IsChecked.Value;
            if (EmulateJpegCheck.IsChecked != null) Settings.EmulateJpeg = EmulateJpegCheck.IsChecked.Value;
            Settings.Quality = QualityTextBox.Text;
            if (DeleteImageCheck.IsChecked != null) Settings.DeleteImage = DeleteImageCheck.IsChecked.Value;
            if (PlaySoundCheck.IsChecked != null) Settings.PlaySound = PlaySoundCheck.IsChecked.Value;
            Settings.SoundToPlay = SoundCombo.SelectedIndex;
            Settings.Save();
            Toaster.SettingsSaved();
            SetClosed();
            SaveButton.Command.Execute(ControlCommands.Close);
        }
        
        private void QualityTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e) {
            if (e != null) e.Handled = Regex.IsMatch(e.Text);
        }

        private void QualityTextBox_OnPasting(object sender, DataObjectPastingEventArgs e) {
            // ReSharper disable once UseNullPropagation
            if (e != null) e.CancelCommand();
        }

        private void QualityTextBox_OnLostFocus(object sender, RoutedEventArgs e) => IsOver();
        private void QualityTextBox_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) => IsOver();

        private void IsOver() {
            if (int.Parse(QualityTextBox.Text) > 100) {
                QualityTextBox.Text = "100";
            }
        }

        private void SoundCombo_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (!_firstOpen) {
                switch (SoundCombo.SelectedIndex) {
                    case 0:
                        Sound.PlaySound(Sound.HitSoundPlayer);
                        break;
                    case 1:
                        Sound.PlaySound(Sound.PizzaSoundPlayer);
                        break;
                    case 2:
                        Sound.PlaySound(Sound.SaxSoundPlayer);
                        break;
                    case 3:
                        Sound.PlaySound(Sound.SteelSoundPlayer);
                        break;
                }
            }
            else {
                _firstOpen = false;
            }
        }
        private void CancelButton_OnClick(object sender, RoutedEventArgs e) {
            SetClosed();
            CancelButton.Command.Execute(ControlCommands.Close);
        }

        private static void SetClosed() => Reference.IsSettingsOpen = false;
    }
}
