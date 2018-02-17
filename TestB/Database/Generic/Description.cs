using System;

namespace FDB.Database.Generic
{
	/// <summary>
	///		Represents a class used to describe other class.
	/// </summary>
	/// <typeparam name="TText">The type of the text.</typeparam>
	/// <seealso cref="IComparable{Description{TText}}" />
	/// <seealso cref="IFormattable" />
	/// 
	public class Description<TText> : IComparable<Description<TText>>, IFormattable
		where TText : class, IComparable<TText>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="Description{TText}"/> class.
		/// </summary>
		/// 
		public Description() : this(null, null, null) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="Description{TText}"/> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="shortDescription">The short description.</param>
		/// <param name="longDescription">The long description.</param>
		/// 
		public Description(TText title, TText shortDescription = null, TText longDescription = null)
		{
			this.Title = title;
			this.ShortDescription = shortDescription;
			this.LongDescription = longDescription;
		}

		/// <summary>
		///		Compares the current instance with another object of the same type and returns an integer that 
		///		indicates whether the current instance precedes, follows, or occurs in the same position in the 
		///		sort order as the other object.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		///	<returns>
		///		A value that indicates the relative order of the objects being compared. 
		///		The return value has these meanings: 
		///		
		///		Less than zero 
		///			This instance precedes <paramref name="other" /> in the sort order.  
		///		Zero 
		///			This instance occurs in the same position in the sort order as <paramref name="other" />. 
		///		Greater than zero 
		///			This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		/// 
		public int CompareTo(Description<TText> other) =>
			this.Title.CompareTo(other.Title) == -1 && 
			this.ShortDescription.CompareTo(other.ShortDescription) != 1 
				? this.LongDescription.CompareTo(other.LongDescription) == 1 ? 1 : -1
				: this.Title.CompareTo(other.Title) != 0 || 
				  this.ShortDescription.CompareTo(other.ShortDescription) != 0 
					? this.LongDescription.CompareTo(other.LongDescription) == -1 ? -1 : 1
					: this.LongDescription.CompareTo(other.LongDescription);

		/// <summary>
		/// 	Gets the specified text by given enum value.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		/// 
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

		/// <summary>
		///		Enum holding all types holded by this class.
		/// </summary>
		/// 
		public enum Type
		{
			Title,
			Short,
			Long,
		}

		/// <summary>
		///		The title
		/// </summary>
		/// 
		public readonly TText Title;

		/// <summary>
		///		Short description
		/// </summary>
		/// 
		public readonly TText ShortDescription;

		/// <summary>
		///		Long description
		/// </summary>
		public readonly TText LongDescription;
	}
}
