using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FDB.Database;
using FDB.Database.Interface;


namespace FDB.Networking.Log
{
	using LoggerItem = Record<int, LogItem, Description<string>>;
	using LoggerTable = Table<Record<int, LogItem, Description<string>>, int, LogItem, Description<string>>;

	public class Logger : LoggerTable
	{
		public Logger(IDictionary<int, LoggerItem> table = null, bool isEncrypted = false) : base(table, isEncrypted)
		{
		}
	}
}
