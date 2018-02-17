using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FDB.Networking
{
	/// <summary>
	///		A networking class used to communicate with application's web part.
	/// </summary>
	/// 
	public class TcpServer
	{
        private bool isListening = false;

		/// <summary>
		///		Stops the listening.
		/// </summary>
		/// 
		public void Stop()
        {
            isListening = false;
            listener.Stop();
        }

		/// <summary>
		///		Asynchronous listening.
		/// </summary>
		/// <param name="notify">The notify label.</param>
		/// 
		public async Task<TcpServer> ListenAsync(Label notify)
		{
            isListening = true;
			listener.Start();
			bool isLogged = true;

			notify.Content = "Init";
            while (this.isListening)
            {
				if (isLogged)
				{
					var other = await listener.AcceptTcpClientAsync();

					var reader = new BinaryReader(other.GetStream());

					var urlLen = (int)BitConverter.ToUInt32(reader.ReadBytes(4), 0);
					var url = Encoding.UTF8.GetString(reader.ReadBytes(urlLen));

					var data = Resolve(url);
					var username = data[0].User;
					var password = data[0].Pass;

					var writer = new BinaryWriter(other.GetStream());

					writer.Write(username.Length);
					writer.Write(password.Length);
					writer.Write(Encoding.UTF8.GetBytes(username));
					writer.Write(Encoding.UTF8.GetBytes(password));
				}
				else
				{// tmp !DRY
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
            }
			notify.Content = "Stop";

			return this;
		}

		public TcpServer Send(string address, string password)
		{
			// TODO 
			return this;
		}

		private bool IpCheck(string address) => true;

		private Data[] Resolve(string address)
		{
			return new Data[] {
				new Data("pawel", "tajnehaslo")
			};
		}

		// placeholder
		public struct Data
		{
			public Data(string user, string pass)
			{
				this.User = user;
				this.Pass = pass;
			}

			public string User;
			public string Pass;
		}

		private TcpListener listener = new TcpListener(IPAddress.Any, 8656);
	}
}
