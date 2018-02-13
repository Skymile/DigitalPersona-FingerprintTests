using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using DPUruNet;

namespace TestB
{
	/// <summary>
	///		A wrapper for <see cref="ReaderCollection"/> and other fingerprint classes from DPUruNet framework. <para/>
	///		Contains methods for fingerprint capture using EikonTouch fingerprint scanner device.
	/// </summary>
	public class FingerprintScanner
	{
		/// <summary>
		///		Initializes a static <see cref="ReaderCollection"/> of <see cref="FingerprintScanner"/> class.
		/// </summary>
		/// 
		static FingerprintScanner()
		{
			readerCollection = ReaderCollection.GetReaders();

			if (readerCollection.Count == 0)
				throw new DeviceNotFoundException();

			var reader = readerCollection[0];

			reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
			reader.Calibrate();
//			reader.ConfigureLed(Constants.LedId.ALL, Constants.LedMode.DEFAULT);
		}

		/// <summary>
		///		Captures <see cref="Bitmap"/> from fingerprint scanner device. 
		/// </summary>
		/// <param name="index"> Index of <see cref="ReaderCollection"/> reader instance. By default set to 0. </param>
		/// <param name="timeout"> Time after which scanner gives up, returns null if timed out. </param>
		/// <returns>
		///		Captured instance of <see cref="Bitmap"/> class.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException"> 
		///		Occurs when <paramref name="index"/> is bigger than the count of fingerprint devices. 
		///	</exception>
		/// <exception cref="DeviceNotFoundException"> 
		///		Occurs when device is unplugged. 
		///	</exception>
		/// 
		public Bitmap CaptureBitmap(int index = 0, int timeout = -1)
		{
			CaptureResult captureResult = Capture(index);
			return captureResult.Data.Views[0].ToBitmap();
		}

		public Fid CaptureFingerprintData(int index = 0, int timeout = -1)
		{
			CaptureResult captureResult = Capture(index);
			return captureResult.Data;
		}


		private CaptureResult Capture(int index = 0, int timeout = -1)
		{ // TODO Check and set configuration of indexes other than 0
			if (index > readerCollection.Count)
				throw new IndexOutOfRangeException(nameof(index));

			//readerCollection[index].UpdateLed(Constants.LedId.FINGER_DETECT, Constants.LedCommand.ON);

			//readerCollection[index].Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE

			Task<CaptureResult> task = new Task<CaptureResult>(
				() => readerCollection[index].Capture(
						Constants.Formats.Fid.ANSI,
						Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT,
						timeout,
						readerCollection[index].Capabilities.Resolutions[0]
					)
				);

			task.Start();

			CaptureResult captureResult = task.Result;

			//readerCollection[index].UpdateLed(Constants.LedId.FINGER_DETECT, Constants.LedCommand.OFF);

			if (captureResult == null)
				return null;
			else if (captureResult.Data == null)
				throw new DeviceNotFoundException(nameof(captureResult.Data));
			return captureResult;
		}

		/// <summary>
		///		Gets the instance of <see cref="Reader"/> class.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// 
		public Reader this[int key] => readerCollection[key];

		/// <summary>
		///		Gets the count of available readers.
		/// </summary>
		/// 
		public static int Length => readerCollection.Count;

		/// <summary>
		///		Collection of fingerprint readers.
		/// </summary>
		/// 
		private static ReaderCollection readerCollection;
	}
}
