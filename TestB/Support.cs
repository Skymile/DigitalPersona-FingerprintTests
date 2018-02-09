using DPUruNet;
using System;
using System.Drawing;
using DrawImg = System.Drawing.Imaging;
using System.Runtime.InteropServices;

using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TestB
{
	public static class Support
	{
		public static Bitmap ToBitmap(this Fid.Fiv fingerprintData)
		{
			Bitmap bmp = new Bitmap(fingerprintData.Width, fingerprintData.Height, DrawImg.PixelFormat.Format24bppRgb);
			DrawImg.BitmapData data = bmp.LockBits(DrawImg.ImageLockMode.WriteOnly);

			try
			{
				byte[] inBytes = fingerprintData.RawImage,
					   rgbBytes = new byte[fingerprintData.RawImage.Length * 3];

				for (int i = 0; i < inBytes.Length; ++i)
				{
					rgbBytes[(i * 3) + 0] = inBytes[i];
					rgbBytes[(i * 3) + 1] = inBytes[i];
					rgbBytes[(i * 3) + 2] = inBytes[i];
				}

				for (int i = 0; i < bmp.Height; ++i)
					Marshal.Copy(rgbBytes, i * bmp.Width * 3, new IntPtr(data.Scan0.ToInt64() + data.Stride * i), bmp.Width * 3);
			}
			catch
			{
				return null;
			}
			finally
			{
				bmp.UnlockBits(data);
			}

			return bmp;
		}

		public static DrawImg.BitmapData LockBits(this Bitmap bitmap, DrawImg.ImageLockMode lockMode) => bitmap.LockBits(bitmap.GetRectangle(), lockMode, bitmap.PixelFormat);

		public static Rectangle GetRectangle(this Bitmap bitmap) => new Rectangle(0, 0, bitmap.Width, bitmap.Height);

		public static ImageSource GetSource(this Bitmap bitmap) => 
			Imaging.CreateBitmapSourceFromHBitmap(
				bitmap.GetHbitmap(),
				IntPtr.Zero,
				Int32Rect.Empty,
				BitmapSizeOptions.FromEmptyOptions()
			);
	}
}
