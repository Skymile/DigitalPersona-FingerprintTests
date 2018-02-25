using FDB.Biometrics;
using FDB.Networking.Users;

namespace FDB.Database.Generic
{
	public sealed class UserRecord : Record<Key, User, ShortDescription<string>>
	{
		public UserRecord(Record<Key, User, ShortDescription<string>> record) : base(record) {}

		public UserRecord(Key key, User data, ShortDescription<string> meta = null) : base(key, data, meta) {}

		public string Username => GetElement().Username;
		public string Password => GetElement().Password;
		public Fingerprint Fingerprint => GetElement().Finger;
	
		public Key Key => GetKey();
		public string Description => GetMeta().ToString();
	}
}
