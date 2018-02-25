using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using FDB.Biometrics;
using FDB.Networking.Users;
using FDB.ViewModel;

namespace FDB.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        public MainWindow()
        {
            InitializeComponent();
            WindowLabelMatching.Content = Properties.Resources.Initialization;
        }

        private void WindowButtonCapture_Click(object sender, RoutedEventArgs e) => mainModel.FingerprintCapture(ref WindowImage, ref WindowImageSource);

        private void WindowButtonBinarize_Click(object sender, RoutedEventArgs e)
        {
            WindowImageSource = WindowImageSource.ApplyEffect(new[] {
                -1, -1, -1,
                -1,  8, -1,
                -1, -1, -1
            });

            WindowImage.Source = WindowImageSource.GetSource();
        }

        private void WindowButtonRefresh_Click(object sender, RoutedEventArgs e) =>
            WindowImage.Source = WindowImageSource.GetSource();

        private void WindowButtonIdentify_Click(object sender, RoutedEventArgs e) => mainModel.Verify(ref WindowLabelMatching);

        private void WindowButtonSet_Click(object sender, RoutedEventArgs e) => mainModel.Set();

        private void WindowButtonListen_Click(object sender, RoutedEventArgs e) => mainModel.Listen(ref WindowLabelMatching);

        private void WindowButtonLogin_Click(object sender, RoutedEventArgs e) => mainModel.Login();

        private void WindowButtonRegister_Click(object sender, RoutedEventArgs e) => mainModel.Register(ref ListBoxPassword);
        
        private User CurrentUser = User.Guest;

        private Bitmap WindowImageSource = null;
        private MainModel mainModel = new MainModel();

        private void WindowSend_Click(object sender, RoutedEventArgs e)
        {
            SendWindow send = new SendWindow();
            send.ShowDialog();
        }

        private void WindowExit_Click(object sender, RoutedEventArgs e) =>
            Environment.Exit(0);

        private void WindowMaximize_Click(object sender, RoutedEventArgs e) => 
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void WindowMinimalize_Click(object sender, RoutedEventArgs e) => 
            this.WindowState = WindowState.Minimized;

        private void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && Mouse.GetPosition(this).Y < 32)
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                    Application.Current.MainWindow.Top = 3;
                }
                this.DragMove();
            }
        }
    }
}
