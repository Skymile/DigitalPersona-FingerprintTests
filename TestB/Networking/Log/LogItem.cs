using System;

using FDB.Database.Interface;
using FDB.Networking.Users;

namespace FDB.Networking.Log
{
	public class LogItem : IElement<LogItem>, IComparable<LogItem>, IEquatable<LogItem>
	{
		public readonly User User;
		public readonly DateTime Time;

		public int CompareTo(LogItem other) => throw new NotImplementedException();

		public bool Equals(LogItem other) => throw new NotImplementedException();
	}
}
