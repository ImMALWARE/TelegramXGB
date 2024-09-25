using System;
using System.Collections.Generic;
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TelegramXGB {

    public sealed partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
            RefreshTexts();
        }

        private async void RefreshTexts() {
            Dictionary<App.Device, string> statuses = new Dictionary<App.Device, string> {
                    {App.Device.Works, "It works!" },
                    {App.Device.NoPermission, "I don't have permission!" },
                    {App.Device.DoesNotWork, "It doesn't work!" }
            };
            MicrophoneText.Text = statuses[await App.CheckDevice(StreamingCaptureMode.Audio)];
            CameraText.Text = statuses[await App.CheckDevice(StreamingCaptureMode.Video)];
        }
        private async void RequestMicrophone(object sender, RoutedEventArgs e) {
            switch (await App.CheckDevice(StreamingCaptureMode.Audio)) {
                case App.Device.NoPermission:
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-microphone"));
                    await App.Message("Telegram XGB", "I have opened Settings, please allow TelegramXGB to use microphone, so Telegram can access it");
                    return;
                case App.Device.Works:
                    RefreshTexts();
                    return;
                default:
                    await App.Message("Your microphone doesn't work", "Do other apps see the microphone?");
                    break;
            }
        }

        private async void RequestCamera(object sender, RoutedEventArgs e) {
            switch (await App.CheckDevice(StreamingCaptureMode.Video)) {
                case App.Device.NoPermission:
                    await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
                    await App.Message("TelegramXGB", "I have opened Settings, please allow TelegramXGB to use camera, so Telegram can access it");
                    return;
                case App.Device.Works:
                    RefreshTexts();
                    return;
                default:
                    await App.Message("Your camera doesn't work", "Do other apps see the camera?");
                    break;
            }
        }
    }
}