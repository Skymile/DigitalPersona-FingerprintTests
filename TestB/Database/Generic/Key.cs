using System;

using FDB.Database.Interface;

namespace FDB.Database.Generic
{
	/// <summary>
	///		Represents unique primary key used in databases.
	/// </summary>
	/// <see cref="Table{TKey, TElement, TMeta}"/>
	/// <seealso cref="Interface.IKey{Key}" />
	/// <seealso cref="System.IComparable{Key}" />
	/// <seealso cref="System.IEquatable{Key}" />
	/// 
	public struct Key : IKey<Key>, IComparable<Key>, IEquatable<Key>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="Key"/> struct.
		/// </summary>
		/// <param name="value">The value.</param>
		/// 
		public Key(int value) => this.Value = value;

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
		public int CompareTo(Key other) => this.Value.CompareTo(other.Value);

		/// <summary>
		///		Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; 
		///   otherwise, <see langword="false" />.
		/// </returns>
		/// 
		public bool Equals(Key other) => this.Value.Equals(other.Value);

		/// <summary>
		///		Gets the next.
		/// </summary>
		/// 
		public Key GetNext() => new Key(this.Value + 1);

		/// <summary>
		///		Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="Key"/>.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		///		The result of the conversion.
		/// </returns>
		public static implicit operator Key(int value) => new Key(value);

		/// <summary>
		///		Held <see cref="Int32"/> value.
		/// </summary>
		public readonly int Value;
	}
}
