using System.Windows;
using DPUruNet;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TestA
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			using (var readerCollection = ReaderCollection.GetReaders())
			{
				if (readerCollection.Count == 0)
					throw new DeviceNotFoundException();

				var reader = readerCollection[0];

				reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);

				//				if (reader.GetStatus().HasFlag())
				//					throw new ReaderNotReadyException();

				CaptureResult captureResult = reader.Capture(
					Constants.Formats.Fid.ANSI,
					Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT,
					-1,
					reader.Capabilities.Resolutions[0]
				);

				Console.WriteLine(captureResult.Data.Bytes.Length);

				Console.WriteLine("Donee");

				Bitmap bitmap = new Bitmap(reader.Capabilities.Resolutions[0], reader.Capabilities.Resolutions[0]);

				BitmapData data = bitmap.LockBits(
					new Rectangle(0, 0, bitmap.Height, bitmap.Width), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb
				);

				byte[] captured = captureResult.Data.Bytes;

				Console.WriteLine(captured.Length);

				Marshal.Copy(captured, 0, data.Scan0, 88904);

				bitmap.UnlockBits(data);

				bitmap.Save("test.png");
				//*/
			}

			Console.ReadLine();
		}
	}
}
