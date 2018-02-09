using System;
using System.Drawing;

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
		///		Initializes a new instance of <see cref="FingerprintScanner"/> class.
		/// </summary>
		public FingerprintScanner()
		{
			readerCollection = ReaderCollection.GetReaders();

			if (readerCollection.Count == 0)
				throw new DeviceNotFoundException();

			var reader = readerCollection[0];

			reader.Open(Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
			reader.Calibrate();
		}

		/// <summary>
		///		
		/// </summary>
		/// <param name="index">
		///		Index of <see cref="ReaderCollection"/> reader instance.
		/// </param>
		/// <returns>
		///		Captured instance of <see cref="Bitmap"/> class.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException"/>
		public Bitmap CaptureBitmap(int index)
		{
			if (index > readerCollection.Count)
				throw new IndexOutOfRangeException(nameof(index));

			var reader = this[index];

			CaptureResult captureResult = reader.Capture(
				Constants.Formats.Fid.ANSI,
				Constants.CaptureProcessing.DP_IMG_PROC_DEFAULT,
				-1,
				reader.Capabilities.Resolutions[0]
			);

			return captureResult.Data.Views[0].ToBitmap();
		}

		/// <summary>
		///		Gets the instance of <see cref="Reader"/> class.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public Reader this[int key] => readerCollection[key];

		/// <summary>
		///		Gets the count of available readers.
		/// </summary>
		public int Length => readerCollection.Count;

		/// <summary>
		///		Collection of fingerprint readers.
		/// </summary>
		private ReaderCollection readerCollection;
	}
}
