using System;
using System.Windows;

namespace FDB.View
{
	/// <summary>
	///		Interaction logic for Message.xaml
	/// </summary>
	public partial class Message : Window
	{
		public Message()
		{
			InitializeComponent();
		}

		public Message(string message, bool show = false)
		{
			InitializeComponent();
			this.WindowErrorMessage.Content = message;
			if (show)
				this.ShowDialog();
		}

		public Message(Error error)
		{
			InitializeComponent();
			this.WindowErrorMessage.Content = Resolve(error);
			this.ShowDialog();
		}

		public void SetMessage(string message) => WindowErrorMessage.Content = message ?? "Unknown error";

		private void WindowButtonOk_Click(object sender, RoutedEventArgs e) => this.Close();

		private string Resolve(Error error)
		{
			switch (error)
			{
				case Error.DeviceNotFound:    return "No fingerprint scanner found, plug in device";
				case Error.MustRegisterFirst: return "You have to register first";
				case Error.UsernameNotFound:  return "This username doesn't exist";
				default: return "Unknown error has occured";
			}
		}

		[Flags]
		public enum Error
		{
			DeviceNotFound,
			MustRegisterFirst,
			UsernameNotFound
		}
	}
}
