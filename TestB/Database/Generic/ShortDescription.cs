using System;

namespace FDB.Database.Generic
{
	/// <summary>
	///		Represents a containter for one string-like datatype.
	/// </summary>
	/// <typeparam name="TText">The type of the text.</typeparam>
	/// <seealso cref="System.IComparable{FDB.Database.Generic.ShortDescription{TText}}" />
	/// <seealso cref="System.IFormattable" />
	public class ShortDescription<TText> : IComparable<ShortDescription<TText>>, IFormattable
		where TText : class, IComparable<TText>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ShortDescription{TText}"/> class.
		/// </summary>
		/// 
		public ShortDescription() : this(null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="ShortDescription{TText}"/> class.
		/// </summary>
		/// <param name="desc">The description</param>
		/// 
		public ShortDescription(TText desc) => this.Desc = desc;

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates 
		/// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>
		///		A value that indicates the relative order of the objects being compared.
		///		
		///		The return value has these meanings: 
		///		Less than zero 
		///			This instance precedes <paramref name="other" /> in the sort order.  
		///		Zero 
		///			This instance occurs in the same position in the sort order as <paramref name="other" />. 
		///		Greater than zero 
		///			This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		/// 
		public int CompareTo(ShortDescription<TText> other) => this.Desc.CompareTo(other.Desc);

		/// <summary>
		///		Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>
		///		A <see cref="System.String" /> that represents this instance.
		/// </returns>
		/// 
		public string ToString(string format, IFormatProvider formatProvider) => 
			this.Desc.ToString();

		private readonly TText Desc;
	}
}
