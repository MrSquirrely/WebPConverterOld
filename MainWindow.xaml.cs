using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using HandyControl.Controls;
using Squirrel_Sizer;
using WebPConverter.Code;

namespace WebPConverter {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private ThreadStart ConvertThreadStart { get; set; }
        private Thread ConvertThread { get; set; }

        public MainWindow() {
            InitializeComponent();
            ImageListView.ItemsSource = Reference.ImageCollection;
            Settings.Load();
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;

            //todo: Adding a github thing to submit bugs.
            //! https://gist.github.com/MrSquirrely/85f2fb8cfd95fd5691be64cadf08bb89
        }

        private void CloseSelected_OnClick(object sender, RoutedEventArgs e) {
            List<ImageFile> selectedFiles = ImageListView.SelectedItems.Cast<ImageFile>().ToList();

            foreach (ImageFile selectedFile in selectedFiles) {
                Reference.ImageCollection.Remove(selectedFile);
            }

            UpdateNumber();
        }

        private void CloseAll_OnClick(object sender, RoutedEventArgs e) {
            Reference.ImageCollection.Clear();
            UpdateNumber();
        }

        private void MenuSettings_OnClick(object sender, RoutedEventArgs e) {
            if (IsOpened()) return;
            Reference.IsSettingsOpen = true;
            Dialog.Show(new Views.SettingsView());
        }

        private void MenuBug_OnClick(object sender, RoutedEventArgs e) {
            if (IsOpened()) return;
            Reference.IsBugsOpen = true;
            Dialog.Show(new Views.BugsView());
        }

        private void MenuFeatures_OnClick(object sender, RoutedEventArgs e) {
            if (IsOpened()) return;
            Reference.IsFeaturesOpen = true;
            Dialog.Show(new Views.FeaturesView());
        }

        private static bool IsOpened() => Reference.IsSettingsOpen || Reference.IsBugsOpen || Reference.IsFeaturesOpen;

        private void ImageListView_OnDrop(object sender, DragEventArgs e) {
            Reference.ImageCollection.Clear();
            if (e.Data == null) return;
            string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
            switch (data) {
                case null:
                    break;
                default: {
                    foreach (string file in data) {
                        FileInfo info = new FileInfo(file);
                        if (!Reference.ImageTypes.Contains(info.Extension.ToLower())) continue;
                        Reference.ImageCollection.Add(new ImageFile(){
                            FileName = Path.GetFileNameWithoutExtension(file),
                            FileType = info.Extension,
                            FileSize = Sizer.SizeSuffix(info.Length),
                            FileLocation = info.DirectoryName
                        });
                    }
                    break;
                }
            }
            UpdateNumber();
        }

        private void UpdateNumber() => NumberOfItemsLabel.Header = $"Images Loaded: {Reference.ImageCollection.Count}";

        private void ConvertButton_OnClick(object sender, RoutedEventArgs e) {
            ConvertThreadStart = Converter.Convert;
            ConvertThreadStart += () => {
                Toaster.FinishedConversion();
                switch (Settings.SoundToPlay) {
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
                    default:
                        Sound.PlaySound(Sound.HitSoundPlayer);
                        break;
                }
            };
            ConvertThread = new Thread(ConvertThreadStart);
            ConvertThread.Start();
        }

        private static void OnProcessExit(object sender, EventArgs e) => Reference.logger.Dispose();
    }
}
