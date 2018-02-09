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

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{
			WindowImageSource = scanner.CaptureBitmap(0);
			WindowImage.Source = WindowImageSource.GetSource();
		}

		private void WindowButtonBinarize_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private Bitmap WindowImageSource = null;

		private void WindowButtonRefresh_Click(object sender, RoutedEventArgs e) =>
			WindowImage.Source = WindowImageSource.GetSource();

		private FingerprintScanner scanner;
	}
}
