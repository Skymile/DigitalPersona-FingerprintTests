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

namespace FDB.View
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void WindowButtonLogin_Click(object sender, RoutedEventArgs e)
		{

		}


		private const string ErrorUsernameInvalid = "Type in valid username";
		private const string ErrorPasswordInvalid = "Type in valid password";
		private const string ErrorFingerprintInvalid = "Fingerprint needed";

		private void WindowButtonCapture_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
