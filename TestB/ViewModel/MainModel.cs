using System;
using System.Drawing;
using System.Collections.Generic;

using WindowControls = System.Windows.Controls;

using FDB.Biometrics;
using FDB.Networking;
using FDB.Networking.Log;
using FDB.Networking.Users;
using FDB.Database.Interface;
using FDB.Database.Generic;

namespace FDB.ViewModel
{
	using UserbaseTable = SortedDictionary<Key, IRecord<User, ShortDescription<string>>>;

	public class MainModel
	{
		public MainModel()
		{
			try
			{
				this.scanner = new FingerprintScanner();
			}
			catch (TypeInitializationException)
			{
				this.scanner = null;
				new View.Message(View.Message.Error.DeviceNotFound);
			}
		}

		public void FingerprintCapture(ref WindowControls.Image image, ref Bitmap imageBitmap)
		{
			imageBitmap = this.scanner.CaptureBitmap(0);
			image.Source = imageBitmap.GetSource();
		}

		public void Set() => 
			MainFingerprint = scanner.CaptureFingerprintData();

		public void Listen(ref WindowControls.Label label) => this.server.ListenAsync(label);

		public void Verify(ref WindowControls.Label label) => 
			label.Content = (this.scanner.CaptureFingerprintData() == this.MainFingerprint).ToString();

		public void Login()
		{
			if (this.userbase.GetLength().GetValueOrDefault() != 0)
			{
				this.login = new View.LoginWindow(ref userbase, scanner);
				this.login.ShowDialog();
			}
			else
			{
				this.message = new View.Message("You have to register first. " + this.userbase.GetLength().ToString());
				this.message.ShowDialog();
			}
		}

		public void Register(ref WindowControls.Label users)
		{
			this.register = new View.RegisterWindow(ref userbase, ref users, scanner);
			this.register.ShowDialog();
		}

		private View.Message message = new View.Message("You have to register first.");
		private View.Message success = new View.Message("Success");

		private View.RegisterWindow register;
		private View.LoginWindow login;

		private Fingerprint MainFingerprint;
		private FingerprintScanner scanner;

		private TcpServer server = new TcpServer();
		private Logger logger = new Logger();
		private Userbase userbase = new Userbase(new UserbaseTable());
	}
}
