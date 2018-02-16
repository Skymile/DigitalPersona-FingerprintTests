using System.Windows;

using FDB.Networking;

namespace FDB.View
{
	/// <summary>
	/// Interaction logic for SendWindow.xaml
	/// </summary>
	public partial class SendWindow : Window
	{
		public SendWindow()
		{
			InitializeComponent();
		}

		private TcpServer server = new TcpServer();

		private void WindowButtonCancel_Click(object sender, RoutedEventArgs e) => this.Close();

		private void WindowButtonSend_Click(object sender, RoutedEventArgs e)
		{
			WindowLabelInfo.Content = "Starting";
			if (WindowLabelAddress.Text == null)
				WindowLabelInfo.Content = "Invalid address detected";
			else if (WindowLabelPassword.Text == null)
				WindowLabelInfo.Content = "Invalid password detected";
			else
			{
				server.Send(WindowLabelAddress.Text, WindowLabelPassword.Text);
				WindowLabelInfo.Content = "Data send to TCP Server";
			}
		}
	}
}
