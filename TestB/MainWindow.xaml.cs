using System.Drawing;
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
			scanner = new FingerprintScanner();
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e) =>
			WindowImage.Source = ReaderCaptureBitmap().GetSource();

		private Bitmap ReaderCaptureBitmap()
		{
			var reader = scanner[0];

			CaptureResult captureResult = reader.Capture(
				Constants.Formats.Fid.ANSI,
				Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT,
				-1,
				reader.Capabilities.Resolutions[0]
			);

			return captureResult.Data.Views[0].ToBitmap();
		}

		private FingerprintScanner scanner;
	}
}
