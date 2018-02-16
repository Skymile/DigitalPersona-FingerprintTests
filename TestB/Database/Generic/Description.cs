using System;

namespace FDB.Database.Generic
{
	public class Description<TText> : IComparable<Description<TText>>, IFormattable
		where TText : class, IComparable<TText>
	{
		public Description() : this(null, null, null) { }

		public Description(TText title, TText shortDescription = null, TText longDescription = null)
		{
			this.Title = title;
			this.ShortDescription = shortDescription;
			this.LongDescription = longDescription;
		}

		public int CompareTo(Description<TText> other) =>
			this.Title.CompareTo(other.Title) == -1 && 
			this.ShortDescription.CompareTo(other.ShortDescription) != 1 
				? this.LongDescription.CompareTo(other.LongDescription) == 1 ? 1 : -1
				: this.Title.CompareTo(other.Title) != 0 || 
				  this.ShortDescription.CompareTo(other.ShortDescription) != 0 
					? this.LongDescription.CompareTo(other.LongDescription) == -1 ? -1 : 1
					: this.LongDescription.CompareTo(other.LongDescription);

		public TText Get(Type type)
		{
			switch (type)
			{
				case Type.Title: return this.Title;
				case Type.Short: return this.ShortDescription;
				case Type.Long: return this.LongDescription;
				default: throw new NotImplementedException();
			}
		}

		public string ToString(string format, IFormatProvider formatProvider)
		{
			throw new NotImplementedException();
		}

		public enum Type
		{
			Title,
			Short,
			Long,
		}

		public readonly TText Title;
		public readonly TText ShortDescription;
		public readonly TText LongDescription;
	}
}
