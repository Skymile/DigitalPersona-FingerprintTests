using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;

using DPUruNet;

namespace TestB
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
			catch (TypeInitializationException ex)
			{
				throw;
			}
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{
			WindowImageSource = scanner.CaptureBitmap(0);
			WindowImage.Source = WindowImageSource.GetSource();
		}

		private void WindowButtonBinarize_Click(object sender, RoutedEventArgs e)
		{
			int[] sharpener = new[] {
				-1, -1, -1,
				-1,  8, -1,
				-1, -1, -1
			};

			BitmapData data = WindowImageSource.LockBits(ImageLockMode.ReadWrite);

			byte[] input = new byte[data.Stride * data.Height];
			byte[] output = new byte[data.Stride * data.Height];

			Marshal.Copy(data.Scan0, input, 0, data.Stride * data.Height);

			int bytesPerPixel = data.Stride / data.Width;

			for (int i = 1; i < data.Width - 1; i++)
				for (int j = 1; j < data.Height - 1; j++)
				{
					int sum = 0;

					for (int k = 0; k < 9; k++)
						sum += input[(i - 1 + (k % 3)) * bytesPerPixel + (j - 1 + (k / 3)) * data.Stride] * sharpener[k];

					sum = sum > 128 ? 255 : 0;

					output[j * data.Stride + i * bytesPerPixel + 0] =
						output[j * data.Stride + i * bytesPerPixel + 1] =
						output[j * data.Stride + i * bytesPerPixel + 2] = (byte)sum;
				}

			Marshal.Copy(output, 0, data.Scan0, input.Length);
			WindowImageSource.UnlockBits(data);
			WindowImage.Source = WindowImageSource.GetSource();
		}

		private Bitmap WindowImageSource = null;

		private void WindowButtonRefresh_Click(object sender, RoutedEventArgs e) =>
			WindowImage.Source = WindowImageSource.GetSource();

		private FingerprintScanner scanner;

		private void WindowButtonIdentify_Click(object sender, RoutedEventArgs e)
		{
			Fid result = scanner.CaptureFingerprintData();
			WindowLabelMatching.Content = Comparison.Compare(
				FeatureExtraction.CreateFmdFromFid(MainFingerprint, Constants.Formats.Fmd.ANSI).Data,
				0,
				FeatureExtraction.CreateFmdFromFid(result, Constants.Formats.Fmd.ANSI).Data,
				0
			).Score;
		}

		private void WindowButtonSet_Click(object sender, RoutedEventArgs e) =>
			MainFingerprint = scanner.CaptureFingerprintData();

		private Fid MainFingerprint;
	}
}
