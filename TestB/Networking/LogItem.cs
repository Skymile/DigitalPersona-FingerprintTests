using System;

using FDB.Database.Interface;

namespace FDB.Networking
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
