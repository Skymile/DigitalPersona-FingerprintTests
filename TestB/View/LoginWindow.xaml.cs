using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using FDB.Biometrics;
using FDB.Database.Generic;
using FDB.Database.Interface;
using FDB.Networking.Users;

namespace FDB.View
{
	/// <summary>
	///		Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow(ref Userbase userbase, FingerprintScanner scanner)
		{
			InitializeComponent();
			this._Userbase = userbase;
			this._Scanner = scanner;
		}

		private void WindowButtonLogin_Click(object sender, RoutedEventArgs e)
		{
			if (this.IsUsernameNull || !UsernameExists(this.WindowBoxLoginUsername))
				this.WindowErrorLabel.Content = Properties.Resources.UsernameInvalid;
			else
				ValidLogin(this.WindowBoxLoginUsername.Text, this.WindowBoxLoginPassword.Text);
		}

		private bool IsUsernameNull => this.WindowBoxLoginUsername.Text == null;
		private bool IsPasswordNull => this.WindowBoxLoginPassword.Text == null;
		private bool IsFingerprintNull => this._Fingerprint == null;

		private bool UsernameExists(TextBox box) =>
			this._Userbase.Find(i => i.GetElement().Username.CompareTo(box.Text) == 0) != null;

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{
			if (_Scanner == null)
				new Message(Message.Error.DeviceNotFound);
			else
				this._Fingerprint = _Scanner.CaptureFingerprintData();
		}

		private IRecord<User, ShortDescription<string>> ValidLogin(string username, string password)
		{
			var match = this._Userbase.Find(i => i.GetElement().Username == username).ElementAtOrDefault(0);
			
			if (match != null)
				return Check(match, IsFingerprintNull ? match.GetElement().Password == password 
													  : this._Fingerprint.CompareFmd(match.GetElement().Finger) < 32768);
			this.WindowErrorLabel.Content = "mismatch";
			return null;

			IRecord<User, ShortDescription<string>> Check(in IRecord<User, ShortDescription<string>> record, bool condition)
			{
				if (condition)
				{
					this.WindowErrorLabel.Content = "match";
					return record;
				}
				this.WindowErrorLabel.Content = "mismatch";
				return null;
			}
		}

		private Userbase _Userbase;
		private FingerprintScanner _Scanner;

		private Fingerprint _Fingerprint;
	}
}
