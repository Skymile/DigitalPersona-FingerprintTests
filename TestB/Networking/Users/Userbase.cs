using System.Collections.Generic;

using FDB.Database.Generic;
using FDB.Database.Interface;

namespace FDB.Networking.Users
{
	public class Userbase : Table<Key, User, ShortDescription<string>>
	{
		public Userbase(IDictionary<Key, IRecord<User, ShortDescription<string>>> table = null, bool isEncrypted = false) : base(table, isEncrypted)
		{
		}
	}
}
