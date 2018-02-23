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
		public RegisterWindow() => InitializeComponent();

		/// <summary>
		/// Initializes a new instance of the <see cref="RegisterWindow"/> class.
		/// </summary>
		/// <param name="userbase">The userbase.</param>
		/// <param name="scanner">The scanner.</param>
		/// 
		public RegisterWindow(ref Userbase userbase, FingerprintScanner scanner)
		{
			InitializeComponent();
			this._Userbase = userbase;
			this._Scanner = scanner;
		}

		private void WindowButtonRegister_Click(object sender, RoutedEventArgs e)
		{
			//if (Contract.OnCondition(IsUsernameNull, () => WindowErrorLabel.Content = ErrorUsernameInvalid) &&
			//	Contract.OnCondition(IsPasswordNull, () => WindowErrorLabel.Content = ErrorPasswordInvalid) &&
			//	Contract.OnCondition(IsFingerprintNull, () => WindowErrorLabel.Content = ErrorFingerprintInvalid) &&
			//	Contract.OnCondition(UsernameExists(WindowBoxRegisterUsername), () => WindowErrorLabel.Content = ThisUsernameExists))

			// Tmp something in contracts is funny
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
			//if (this.IsFingerprintNull)
			//{
			//	return;
			//}

			ValidRegister();
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e) => 
			this._TempFingerprint = _Scanner.CaptureFingerprintData();

		private void ValidRegister()
		{
			this._Userbase.Add(new Record<Key, User, ShortDescription<string>>(
					0, new User(
					this.WindowBoxRegisterUsername.Text,
					this.WindowBoxRegisterPassword.Text,
					this._TempFingerprint
				)));

			this.WindowErrorLabel.Content = "Success " + this._Userbase.GetLength();
		}

		private bool IsUsernameNull => this.WindowBoxRegisterUsername.Text == null;
		private bool IsPasswordNull => this.WindowBoxRegisterPassword.Text == null;
		private bool IsFingerprintNull => this._TempFingerprint == null;

		private bool UsernameExists(TextBox box) => this._Userbase.Find(i => i.GetElement().Username.CompareTo(box.Text) == 0).Count != 0;

		private const string ErrorUsernameInvalid = "Type in valid username";
		private const string ErrorPasswordInvalid = "Type in valid password";
		
		private const string ThisUsernameExists = "This username already exists";

		private Userbase _Userbase;
		private FingerprintScanner _Scanner;

		private Fingerprint _TempFingerprint;
	}
}
