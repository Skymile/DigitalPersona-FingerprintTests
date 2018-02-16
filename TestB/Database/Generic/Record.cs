using System;

using FDB.Database.Interface;

namespace FDB.Database.Generic
{
	/// <summary>
	///		Represents one single record in database.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TElement">The type of the element.</typeparam>
	/// <typeparam name="TMeta">The type of the meta.</typeparam>
	/// <seealso cref="Interface.IRecord{TElement, TMeta}" />
	/// 
	public class Record<TKey, TElement, TMeta> : IRecord<TElement, TMeta>
		where TKey : struct, IKey<TKey>, IComparable<TKey>, IEquatable<TKey>
		where TElement : class, IElement<TElement>, IComparable<TElement>
		where TMeta : class, IComparable<TMeta>, IFormattable, new()
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="Record{TKey, TElement, TMeta}"/> class.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="data">The data.</param>
		/// <param name="meta">The meta.</param>
		/// 
		public Record(TKey key, TElement data, TMeta meta = null)
		{
			this.Key = key;
			this.Data = data;
			this.Meta = meta ?? default(TMeta);
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="Record{TKey, TElement, TMeta}"/> class.
		/// </summary>
		/// <param name="record">The record.</param>
		/// 
		public Record(Record<TKey, TElement, TMeta> record)
		{
			this.Key = record.Key;
			this.Data = record.Data;
			this.Meta = record.Meta;
		}

		/// <summary>
		///		Gets the key.
		/// </summary>
		/// 
		public TKey GetKey() => this.Key;

		///	<summary>
		///		Gets the metadata.
		/// </summary>
		/// 
		public TMeta GetMeta() => this.Meta;

		/// <summary>
		///		Gets the element.
		/// </summary>
		/// 
		public TElement GetElement() => this.Data;

		/// <summary>
		///		Modifies copy of this record.
		/// </summary>
		/// <param name="modify">The modify.</param>
		/// 
		public IRecord<TElement, TMeta> Modify(Func<IRecord<TElement, TMeta>, IRecord<TElement, TMeta>> modify) => 
			modify(new Record<TKey, TElement, TMeta>(this));

		/// <summary>
		///		Updates this record.
		/// </summary>
		/// <param name="meta">The meta.</param>
		/// <returns></returns>
		public IRecord<TElement, TMeta> Update(TMeta meta)
		{
			this.Meta = meta;
			return this;
		}

		private readonly TKey Key;
		private readonly TElement Data;
		private TMeta Meta;
	}
}
