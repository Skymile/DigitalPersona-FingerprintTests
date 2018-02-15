using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDB.Database;

namespace FDB.Networking.Users
{
	using UserItem = Record<int, User, ShortDescription<string>>;
	using UserTable = Table<Record<int, User, ShortDescription<string>>, int, User, ShortDescription<string>>;

	public class Userbase : UserTable
	{
		public Userbase(IDictionary<int, UserItem> table = null, bool isEncrypted = false) : base(table, isEncrypted)
		{
		}

	}
}
