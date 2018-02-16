using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FDB.Networking
{
	class TcpServer
	{
        private bool isListening = false;

        public void Stop()
        {
            isListening = false;
            listener.Stop();
        }

		public async Task<TcpServer> ListenAsync(Label notify)
		{
            isListening = true;
			listener.Start();

            while (isListening)
            {
                var other = await listener.AcceptTcpClientAsync();

                var reader = new BinaryReader(other.GetStream());
                var writer = new BinaryWriter(other.GetStream());

                var urlLen = (int)BitConverter.ToUInt32(reader.ReadBytes(4), 0);
                var url = Encoding.UTF8.GetString(reader.ReadBytes(urlLen));

                var username = "pawel";
                var password = "tajnehaslo";                

                writer.Write(username.Length);
                writer.Write(password.Length);
                writer.Write(Encoding.UTF8.GetBytes(username));
                writer.Write(Encoding.UTF8.GetBytes(password));

                notify.Content += "; Server stopped";
            }

			return this;
		}

		private TcpListener listener = new TcpListener(IPAddress.Any, 8656);
	}
}
