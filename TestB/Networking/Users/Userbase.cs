using System.Collections.Generic;

using FDB.Database.Generic;
using FDB.Database.Interface;

namespace FDB.Networking.Users
{
	/// <summary>
	///		Represents database's table of all users
	/// </summary>
	/// <seealso cref="Database.Generic.Table{Key, User, ShortDescription{System.String}}" />
	/// 
	public class Userbase : Table<Key, User, ShortDescription<string>>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Userbase"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
		/// 
		public Userbase(IDictionary<Key, IRecord<User, ShortDescription<string>>> table = null, bool isEncrypted = false) : base(table, isEncrypted)
		{
		}
	}
}
