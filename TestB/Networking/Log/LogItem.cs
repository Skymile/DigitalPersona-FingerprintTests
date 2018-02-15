using System;

using FDB.Database.Interface;
using FDB.Networking.Users;

namespace FDB.Networking.Log
{
	public struct LogItem : IElement
	{
		public readonly User User;
		public readonly DateTime Time;

		public int GetSize()
		{
			throw new NotImplementedException();
		}
	}
}
