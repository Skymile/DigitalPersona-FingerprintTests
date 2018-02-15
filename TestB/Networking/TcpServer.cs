using System.Net;
using System.Net.Sockets;
using System.Windows.Controls;

namespace FDB.Networking
{
	class TcpServer
	{
		public TcpServer Listen(Label notify)
		{
			listener.Start();
			byte[] bytes = new byte[256];
			for (int cycles = 0; cycles < 1024; ++cycles)
			{
				TcpClient other = listener.AcceptTcpClient();
				NetworkStream stream = other.GetStream();

				if (stream.Read(bytes, 0, 256) != 0)
					notify.Content = stream.DataAvailable.ToString();
				else
					notify.Content = "No Connection";

				other.Close();
			}
			listener.Stop();
			notify.Content += "; Server stopped";
			return this;
		}

		private TcpListener listener = new TcpListener(IPAddress.Any, 8656);
	}
}
