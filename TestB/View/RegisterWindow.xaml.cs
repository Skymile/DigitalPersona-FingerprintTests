using System;
using System.Windows;
using System.Windows.Controls;

using FDB.Biometrics;
using FDB.Database.Generic;
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
		public RegisterWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterWindow"/> class.
		/// </summary>
		/// <param name="userbase">The userbase.</param>
		/// <param name="scanner">The scanner.</param>
		/// 
		public RegisterWindow(ref Userbase userbase, ref Label usersInfo, FingerprintScanner scanner)
		{
			InitializeComponent();
			this._Userbase = userbase;
			this._Scanner = scanner;
			this.userInfo = usersInfo;
		}

		private void WindowButtonRegister_Click(object sender, RoutedEventArgs e)
		{
			if (this.IsUsernameNull)
			{
				this.WindowErrorLabel.Content = ErrorUsernameInvalid;
				return;
			}
			if (UsernameExists(WindowBoxRegisterUsername))
			{
				this.WindowErrorLabel.Content = ThisUsernameExists;
				return;
			}

			if (this.IsPasswordNull)
			{
				this.WindowErrorLabel.Content = ErrorPasswordInvalid;
				return;
			}

			// TODO fingerprint check prev
			var user = new User(this.WindowBoxRegisterUsername.Text, this.WindowBoxRegisterPassword.Text, _TempFingerprint);

			if (this._Userbase.Find(user) != null)
				this.WindowErrorLabel.Content = "This username already exists";
			else
				ValidRegister(user);
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e) => 
			this._TempFingerprint = _Scanner.CaptureFingerprintData();

		private void ValidRegister(User user)
		{
			this._Userbase.Add(user);

			this.userInfo.Content = String.Join("\n", this._Userbase.Select(i => $"{i.GetElement().Username} {i.GetElement().Password}"));

			this.WindowErrorLabel.Content = "Success " + this._Userbase.GetLength();
		}

		private bool IsUsernameNull => this.WindowBoxRegisterUsername.Text == null;
		private bool IsPasswordNull => this.WindowBoxRegisterPassword.Text == null;
		private bool IsFingerprintNull => this._TempFingerprint == null;

		private bool UsernameExists(TextBox box) => 
			this._Userbase.Find(i => i.GetElement().Username.CompareTo(box.Text) == 0) == null;

		private const string ErrorUsernameInvalid = "Type in valid username";
		private const string ErrorPasswordInvalid = "Type in valid password";
		
		private const string ThisUsernameExists = "This username already exists";

		private Userbase _Userbase;
		private FingerprintScanner _Scanner;

		private Fingerprint _TempFingerprint;

		private Label userInfo;
	}
}
