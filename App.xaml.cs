using Microsoft.Gaming.XboxGameBar;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Media.Capture;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TelegramXGB {
    sealed partial class App : Application {
        private XboxGameBarWidget widget1 = null;
        public App() {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        public enum Device {
            Works,
            NoPermission,
            DoesNotWork
        }

        internal static async Task<Device> CheckDevice(StreamingCaptureMode device) {
            try {
                await new MediaCapture().InitializeAsync(new MediaCaptureInitializationSettings {
                    StreamingCaptureMode = device
                });
                return Device.Works;
            }
            catch (UnauthorizedAccessException) {
                return Device.NoPermission;
            }
            catch (Exception) {
                return Device.DoesNotWork;
            }
        }

        public static async Task Message(string title, string message) {
            MessageDialog msg = new MessageDialog(message, title);
            msg.Commands.Add(new UICommand("ОК", new UICommandInvokedHandler((command) => { })));
            msg.DefaultCommandIndex = 0;
            await msg.ShowAsync();
        }
        protected override void OnActivated(IActivatedEventArgs args) {
            XboxGameBarWidgetActivatedEventArgs widgetArgs = null;
            if (args.Kind == ActivationKind.Protocol) {
                var protocolArgs = args as IProtocolActivatedEventArgs;
                string scheme = protocolArgs.Uri.Scheme;
                if (scheme.Equals("ms-gamebarwidget")) {
                    widgetArgs = args as XboxGameBarWidgetActivatedEventArgs;
                }
            }
            if (widgetArgs != null) {
                if (widgetArgs.IsLaunchActivation) {
                    var rootFrame = new Frame();
                    rootFrame.NavigationFailed += OnNavigationFailed;
                    Window.Current.Content = rootFrame;
                    widget1 = new XboxGameBarWidget(
                        widgetArgs,
                        Window.Current.CoreWindow,
                        rootFrame);
                    rootFrame.Navigate(typeof(Telegram));
                    Window.Current.Closed += Widget1Window_Closed;
                    Window.Current.Activate();
                }
            }
        }

        private void Widget1Window_Closed(object sender, Windows.UI.Core.CoreWindowEventArgs e) {
            widget1 = null;
            Window.Current.Closed -= Widget1Window_Closed;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null) {
                rootFrame = new Frame();
                rootFrame.NavigationFailed += OnNavigationFailed;
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false) {
                if (rootFrame.Content == null) {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e) {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        private void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();
            widget1 = null;
            deferral.Complete();
        }
    }
}