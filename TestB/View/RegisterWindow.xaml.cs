using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FDB.Biometrics;
using FDB.Database;
using FDB.Database.Generic;
using FDB.Networking.Users;

namespace FDB.View
{
	/// <summary>
	/// Interaction logic for RegisterWindow.xaml
	/// </summary>
	public partial class RegisterWindow : Window
	{
		public RegisterWindow()
		{
			InitializeComponent();
		}

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

			if (IsUsernameNull)
			{
				WindowErrorLabel.Content = ErrorUsernameInvalid;
				return;
			}
			if (IsPasswordNull)
			{
				WindowErrorLabel.Content = ErrorPasswordInvalid;
				return;
			}
			if (IsFingerprintNull)
			{
				WindowErrorLabel.Content = ErrorFingerprintInvalid;
				return;
			}
			if (UsernameExists(WindowBoxRegisterUsername))
			{
				WindowErrorLabel.Content = ThisUsernameExists;
				return;
			}
			ValidRegister();
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{
			this._TempFingerprint = _Scanner.CaptureFingerprintData();
		}

		private void ValidRegister()
		{
			this._Userbase.Add(new Record<Database.Generic.Key, User, ShortDescription<string>>(
					0, new User(
					this.WindowBoxRegisterUsername.Text,
					this.WindowBoxRegisterPassword.Text,
					this._TempFingerprint
				)));

			WindowErrorLabel.Content = "Success " + this._Userbase.GetLength();
		}

		private bool IsUsernameNull => this.WindowBoxRegisterUsername.Text == null;
		private bool IsPasswordNull => this.WindowBoxRegisterPassword.Text == null;
		private bool IsFingerprintNull => this._TempFingerprint == null;

		private bool UsernameExists(TextBox box) => this._Userbase.Find(i => i.GetElement().Username.CompareTo(box.Text) == 0).Count != 0;

		private const string ErrorUsernameInvalid = "Type in valid username";
		private const string ErrorPasswordInvalid = "Type in valid password";
		private const string ErrorFingerprintInvalid = "Fingerprint needed";

		private const string ThisUsernameExists = "This username already exists";

		private Userbase _Userbase;
		private FingerprintScanner _Scanner;

		private Fingerprint _TempFingerprint;
	}
}
