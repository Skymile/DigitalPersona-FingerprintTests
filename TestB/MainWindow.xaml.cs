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

			//			for (int i = 2; i < 88904 / 2; i++)
			//				if (88904 % i == 0)
			//					Console.Write($"{i} ");

			using (var readerCollection = ReaderCollection.GetReaders())
			{
				if (readerCollection.Count == 0)
					throw new DeviceNotFoundException();

				var reader = readerCollection[0];

				reader.Open(Constants.CapturePriority.DP_PRIORITY_EXCLUSIVE);

				reader.Calibrate();

				//				if (reader.GetStatus().HasFlag())
				//					throw new ReaderNotReadyException();

				CaptureResult captureResult = reader.Capture(
					Constants.Formats.Fid.ANSI,
					Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT,
					-1,
					reader.Capabilities.Resolutions[0]
				);

				Bitmap bitmap = captureResult.Data.Views[0].ToBitmap();

				bitmap.Save("test.png");
				//*/
			}

			Environment.Exit(0);
		}
	}
}
