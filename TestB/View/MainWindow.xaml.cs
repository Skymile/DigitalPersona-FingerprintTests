using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows;

using FDB.Biometrics;
using FDB.Networking;

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
			try
			{
				scanner = new FingerprintScanner();
			}
			catch (TypeInitializationException)
			{
				throw;
			}

			WindowLabelMatching.Content = "Init";
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{
			WindowImageSource = scanner.CaptureBitmap(0);
			WindowImage.Source = WindowImageSource.GetSource();
		}

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

		private void WindowButtonIdentify_Click(object sender, RoutedEventArgs e) =>
			WindowLabelMatching.Content = (scanner.CaptureFingerprintData() == MainFingerprint).ToString();

		private void WindowButtonSet_Click(object sender, RoutedEventArgs e) =>
			MainFingerprint = scanner.CaptureFingerprintData();

		private void WindowButtonListen_Click(object sender, RoutedEventArgs e) => 
			server.Listen(WindowLabelMatching);

		private Bitmap WindowImageSource = null;

		private Fingerprint MainFingerprint;
		private FingerprintScanner scanner;

		private TcpServer server;
	}
}
