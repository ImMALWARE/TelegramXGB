using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using Windows.Media.Capture;
using Windows.UI.Xaml.Controls;

namespace TelegramXGB {
    public sealed partial class Telegram : Page {
        public Telegram() {
            InitializeComponent();
            Environment.SetEnvironmentVariable("WEBVIEW2_DEFAULT_BACKGROUND_COLOR", "151330");
            wv.NavigationCompleted += Wv_NavigationCompleted;
        }

        private void Wv_NavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args) {
            if (args.IsSuccess) {
                wv.CoreWebView2.PermissionRequested += async (s, pargs) => {
                    if (pargs.PermissionKind == CoreWebView2PermissionKind.Camera) {
                        switch (await App.CheckDevice(StreamingCaptureMode.Video)) {
                            case App.Device.NoPermission:
                                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
                                await App.Message("Telegram needs access to your camera", "I have opened Settings, please allow TelegramXGB to use camera, so Telegram can access it");
                                return;
                            case App.Device.Works:
                                pargs.State = CoreWebView2PermissionState.Allow;
                                return;
                            default:
                                await App.Message("Your camera doesn't work", "Do other apps see the camera?");
                                return;
                        }
                    }
                    else if (pargs.PermissionKind == CoreWebView2PermissionKind.Microphone) {
                        switch (await App.CheckDevice(StreamingCaptureMode.Audio)) {
                            case App.Device.NoPermission:
                                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-webcam"));
                                await App.Message("Telegram needs access to your microphone", "I have opened Settings, please allow TelegramXGB to use microphone, so Telegram can access it");
                                return;
                            case App.Device.Works:
                                pargs.State = CoreWebView2PermissionState.Allow;
                                return;
                            default:
                                await App.Message("Your microphone doesn't work", "Do other apps see the microphone?");
                                return;
                        }
                    }
                    else if (pargs.PermissionKind == CoreWebView2PermissionKind.Notifications) {
                        pargs.State = CoreWebView2PermissionState.Deny;
                    }
                };
            }
        }
    }
}