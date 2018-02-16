using System;
using FDB.Biometrics;
using FDB.Database.Interface;

namespace FDB.Networking.Users
{
	public struct User : IElement
	{
        public User(string username, string password, Fingerprint finger)
        {
            this.Username = username;
            this.Password = password;
			this.Finger = finger;
        }

		public readonly string Username;
		public readonly string Password;
		public readonly Fingerprint Finger;

		public int GetSize()
		{
			throw new NotImplementedException();
		}
	}
}
