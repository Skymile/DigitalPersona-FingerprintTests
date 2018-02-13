using System;

namespace FDB.Database
{
	public struct Description
	{
		public readonly uint Size;
		public string Get(Type type)
		{
			switch (type)
			{
				case Type.Title: return Title;
				case Type.Short: return ShortDescription;
				case Type.Long: return LongDescription;
				default: throw new NotImplementedException();
			}
		}

		readonly string Title;
		readonly string ShortDescription;
		readonly string LongDescription;

		public enum Type
		{
			Title,
			Short,
			Long,
		}
	}
}