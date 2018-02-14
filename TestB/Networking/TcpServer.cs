using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDB.Networking
{
	class TcpServer
	{
		/*
		// Temp
		private TcpListener listener = new TcpListener(IPAddress.Any, 8656);

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
		*/
	}
}
