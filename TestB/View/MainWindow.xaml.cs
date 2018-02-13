using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows;

using FDB.Biometrics;

namespace FDB.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Temp
		private TcpListener listener = new TcpListener(IPAddress.Any, 8656);

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

		private Bitmap WindowImageSource = null;

		private void WindowButtonRefresh_Click(object sender, RoutedEventArgs e) =>
			WindowImage.Source = WindowImageSource.GetSource();

		private FingerprintScanner scanner;

		private void WindowButtonIdentify_Click(object sender, RoutedEventArgs e) =>
			WindowLabelMatching.Content = (scanner.CaptureFingerprintData() == MainFingerprint).ToString();

		private void WindowButtonSet_Click(object sender, RoutedEventArgs e) =>
			MainFingerprint = scanner.CaptureFingerprintData();

		private Fingerprint MainFingerprint;

		private void WindowButtonListen_Click(object sender, RoutedEventArgs e)
		{
			listener.Start();
			byte[] bytes = new byte[256];
			for (int cycles = 0; cycles < 1024; ++cycles)
			{
				TcpClient other = listener.AcceptTcpClient();
				NetworkStream stream = other.GetStream();

				if (stream.Read(bytes, 0, 256) != 0)
					WindowLabelMatching.Content = stream.DataAvailable.ToString();
				else
					WindowLabelMatching.Content = "No Connection";

				other.Close();
			}
			listener.Stop();
			WindowLabelMatching.Content += "; Server stopped";
		}

		[Obsolete]
		private void WindowButtonStop_Click(object sender, RoutedEventArgs e) => listener.Stop();
	}
}
