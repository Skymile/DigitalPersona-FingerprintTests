using System.Windows;

using FDB.Networking;
using FDB.Support.Constants;

namespace FDB.View
{
	/// <summary>
	///		Interaction logic for SendWindow.xaml
	/// </summary>
	public partial class SendWindow : Window
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="SendWindow"/> class.
		/// </summary>
		/// 
		public SendWindow() => InitializeComponent();

		private TcpServer server = new TcpServer();

		private void WindowButtonCancel_Click(object sender, RoutedEventArgs e) => this.Close();

		private void WindowButtonSend_Click(object sender, RoutedEventArgs e)
		{
			this.WindowLabelInfo.Content = Text.Initialization;
			if (this.WindowLabelAddress.Text == null)
				this.WindowLabelInfo.Content = Text.Address.Invalid;
			else if (this.WindowLabelPassword.Text == null)
				this.WindowLabelInfo.Content = Text.Password.Invalid;
			else
			{
				this.server.Send(this.WindowLabelAddress.Text, this.WindowLabelPassword.Text);
				this.WindowLabelInfo.Content = Text.TCP.DataSend;
			}
		}
	}
}
