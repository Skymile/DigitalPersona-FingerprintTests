using System;

namespace FDB.Database
{
	public class ShortDescription<TText> : IComparable<ShortDescription<TText>>, IFormattable
		where TText : class, IComparable<TText>
	{
		public ShortDescription() : this(null) { }

		public ShortDescription(TText desc)
		{
			this.Desc = desc;
		}

		public int CompareTo(ShortDescription<TText> other) => this.Desc.CompareTo(other.Desc);

		public string ToString(string format, IFormatProvider formatProvider) => 
			this.Desc.ToString();

		private readonly TText Desc;
	}
}
