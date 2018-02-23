using System;

using FDB.Database.Interface;
using FDB.Networking.Users;

namespace FDB.Networking.Log
{
	/// <summary>
	///		Represents one logging event.
	/// </summary>
	/// <seealso cref="FDB.Database.Interface.IElement{FDB.Networking.Log.LogItem}" />
	/// <seealso cref="System.IComparable{FDB.Networking.Log.LogItem}" />
	/// <seealso cref="System.IEquatable{FDB.Networking.Log.LogItem}" />
	/// 
	public class LogItem : IElement<LogItem>, IComparable<LogItem>, IEquatable<LogItem>
	{
		/// <summary>
		///		The user who logged
		/// </summary>
		public readonly User User;

		/// <summary>
		///		The time of this event.
		/// </summary>
		public readonly DateTime Time;

		public int CompareTo(LogItem other)
		{
			throw new NotImplementedException();
		}

		public bool Equals(LogItem other)
		{
			throw new NotImplementedException();
		}
	}
}
