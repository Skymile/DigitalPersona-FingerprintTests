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

		public RegisterWindow(Userbase userbase)
		{
			InitializeComponent();
			this._Userbase = userbase;
		}

		private void WindowButtonRegister_Click(object sender, RoutedEventArgs e)
		{
			Contract.Conditionally(ValidRegister,
				Contract.OnCondition(IsUsernameNull, () => WindowErrorLabel.Content = ErrorUsernameInvalid),
				Contract.OnCondition(IsPasswordNull, () => WindowErrorLabel.Content = ErrorPasswordInvalid),
				Contract.OnCondition(IsFingerprintNull, () => WindowErrorLabel.Content = ErrorFingerprintInvalid),
				Contract.OnCondition(UsernameExists(WindowBoxRegisterUsername), () => WindowErrorLabel.Content = ThisUsernameExists)
			);
		}

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ValidRegister() =>
			this._Userbase.Add(new Record<int, User, ShortDescription<string>>(0, new User(
				this.WindowBoxRegisterUsername.Text,
				this.WindowBoxRegisterPassword.Text,
				this.TempFingerprint
			)));

		private bool IsUsernameNull => this.WindowBoxRegisterUsername.Text == null;
		private bool IsPasswordNull => this.WindowBoxRegisterPassword.Text == null;
		private bool IsFingerprintNull => this.TempFingerprint == null;

		private bool UsernameExists(TextBox box) => this._Userbase.Find(box.Text, (i, j) => j.GetElement().Username.CompareTo(i) == 0);

		private const string ErrorUsernameInvalid = "Type in valid username";
		private const string ErrorPasswordInvalid = "Type in valid password";
		private const string ErrorFingerprintInvalid = "Fingerprint needed";

		private const string ThisUsernameExists = "This username already exists";

		private Userbase _Userbase;
		private Fingerprint TempFingerprint;
	}
}
