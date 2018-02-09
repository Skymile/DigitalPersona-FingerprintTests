using DPUruNet;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TestB
{
	public static class Support
	{
		public static Bitmap ToBitmap(this Fid.Fiv fingerprintData)
		{
			byte[] inBytes = fingerprintData.RawImage,
				   rgbBytes = new byte[fingerprintData.RawImage.Length * 3];

			for (int i = 0; i < inBytes.Length; ++i)
			{
				rgbBytes[(i * 3) + 0] = inBytes[i];
				rgbBytes[(i * 3) + 1] = inBytes[i];
				rgbBytes[(i * 3) + 2] = inBytes[i];
			}

			Bitmap bmp = new Bitmap(fingerprintData.Width, fingerprintData.Height, PixelFormat.Format24bppRgb);

			BitmapData data = bmp.LockBits(ImageLockMode.WriteOnly);

			for (int i = 0; i < bmp.Height; ++i)
				Marshal.Copy(rgbBytes, i * bmp.Width * 3, new IntPtr(data.Scan0.ToInt64() + data.Stride * i), bmp.Width * 3);

			bmp.UnlockBits(data);

			return bmp;
		}

		public static BitmapData LockBits(this Bitmap bitmap, ImageLockMode lockMode) => bitmap.LockBits(bitmap.GetRectangle(), lockMode, bitmap.PixelFormat);
		public static Rectangle GetRectangle(this Bitmap bitmap) => new Rectangle(0, 0, bitmap.Width, bitmap.Height);
	}
}
