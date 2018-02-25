using System;
using System.Windows;
using System.Windows.Controls;

using FDB.Biometrics;
using FDB.Networking.Users;

namespace FDB.View
{
	/// <summary>
	/// Interaction logic for RegisterWindow.xaml
	/// </summary>
	public partial class RegisterWindow : Window
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterWindow"/> class.
		/// </summary>
		/// 
		public RegisterWindow() => InitializeComponent();

		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterWindow"/> class.
		/// </summary>
		/// <param name="userbase">The userbase.</param>
		/// <param name="scanner">The scanner.</param>
		/// 
		public RegisterWindow(ref Userbase userbase, ref ListBox userlist, FingerprintScanner scanner)
		{
			InitializeComponent();

			this._Userbase = userbase;
			this._Scanner = scanner;
			this._UserList = userlist;
		}

		private void WindowButtonRegister_Click(object sender, RoutedEventArgs e)
		{
			if (this.IsUsernameNull || this.IsPasswordNull)
				this.WindowErrorLabel.Content = Properties.Resources.UsernameInvalid;
			else if (UsernameExists(WindowBoxRegisterUsername))
				this.WindowErrorLabel.Content = Properties.Resources.UsernameExists;
			else
			{
				// TODO fingerprint check prev
				var user = new User(this.WindowBoxRegisterUsername.Text, this.WindowBoxRegisterPassword.Text, _TempFingerprint);

				if (this._Userbase.Find(user) != null)
					this.WindowErrorLabel.Content = Properties.Resources.UsernameExists;
				else
					ValidRegister(user);
			}
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{
			if (this._Scanner != null)
				this._TempFingerprint = _Scanner.CaptureFingerprintData();
			else
				new Message(Message.Error.DeviceNotFound);
		}

		private void ValidRegister(User user)
		{
			this._Userbase.Add(user);

			this._UserList.Items.Add(
				$"{user.Username.PadRight(24)} {user.Password.PadRight(24)}" 
				+ (user.Finger == null ? Properties.Resources.NotAvailable : Properties.Resources.Available)
			);

			this.WindowErrorLabel.Content = Properties.Resources.Success + " " + this._Userbase.GetLength();
		}

		private bool IsUsernameNull => this.WindowBoxRegisterUsername.Text == null;
		private bool IsPasswordNull => this.WindowBoxRegisterPassword.Text == null;
		private bool IsFingerprintNull => this._TempFingerprint == null;

		private bool UsernameExists(TextBox box) =>
			this._Userbase.Find(i => i.GetElement().Username.CompareTo(box.Text) == 0) == null;

		private Userbase _Userbase;
		private FingerprintScanner _Scanner;

		private Fingerprint _TempFingerprint;

		private ListBox _UserList;
	}
}
