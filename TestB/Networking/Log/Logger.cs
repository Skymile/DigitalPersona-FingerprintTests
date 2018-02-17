using System.Collections.Generic;

using FDB.Database.Generic;
using FDB.Database.Interface;

namespace FDB.Networking.Log
{
	/// <summary>
	///		Represents a database's table holding all logs
	/// </summary>
	/// <seealso cref="Database.Generic.Table{Key, LogItem, ShortDescription{System.String}}" />
	/// 
	public class Logger : Table<Key, LogItem, ShortDescription<string>>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="Logger"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
		/// 
		public Logger(IDictionary<Key, IRecord<LogItem, ShortDescription<string>>> table = null, bool isEncrypted = false) : base(table, isEncrypted)
		{
		}
	}
}
