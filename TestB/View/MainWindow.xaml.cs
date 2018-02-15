using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows;

using FDB.Biometrics;
using FDB.Networking;
using FDB.Networking.Log;
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
			WindowLabelMatching.Content = "Init";
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

		private void WindowButtonRegister_Click(object sender, RoutedEventArgs e)
		{

		}

		private Bitmap WindowImageSource = null;
		private MainModel mainModel = new MainModel();
	}
}
