using System.Collections.Generic;

using FDB.Database.Generic;
using FDB.Database.Interface;

namespace FDB.Networking.Log
{
	public class Logger : Table<Key, LogItem, ShortDescription<string>>
	{
		public Logger(IDictionary<Key, IRecord<LogItem, ShortDescription<string>>> table = null, bool isEncrypted = false) : base(table, isEncrypted)
		{
		}
	}
}
