using System;
using System.Collections.Generic;
using System.Linq;

using FDB.Database.Interface;

namespace FDB.Database.Generic
{
	/// <summary>
	///		Represents a database table, that is, a collection of items <typeparamref name="TElement"/> data accessed via <typeparamref name="TKey"/> key 
	///		and with metadata of type <typeparamref name="TMeta"/>. 
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TElement">The type of the element.</typeparam>
	/// <typeparam name="TMeta">The type of the metadata.</typeparam>
	/// <seealso cref="Record{TKey, TElement, TMeta}"/>
	/// <seealso cref="Interface.ITable{TKey, TElement, TMeta}" />
	/// <seealso cref="Interface.IKey{TKey}"/>
	/// <seealso cref="Interface.IElement{TElement}"/>
	/// <seealso cref="Interface.IRecord{TElement, TMeta}"/>
	/// 
	public class Table<TKey, TElement, TMeta> : ITable<TKey, TElement, TMeta>
		where TKey : struct, IKey<TKey>, IComparable<TKey>, IEquatable<TKey>
		where TElement : class, IElement<TElement>, IComparable<TElement>
		where TMeta : class, IComparable<TMeta>, IFormattable, new()
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="Table{TKey, TElement, TMeta}"/> class.
		/// </summary>
		/// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
		/// 
		public Table(bool isEncrypted = false) : this(null, isEncrypted) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="Table{TKey, TElement, TMeta}"/> class.
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="isEncrypted">if set to <c>true</c> [is encrypted].</param>
		/// 
		public Table(IDictionary<TKey, IRecord<TElement, TMeta>> table = null, bool isEncrypted = false)
		{
			this._DataTable = table as SortedDictionary<TKey, Record<TKey, TElement, TMeta>>;
			this._IsEncrypted = isEncrypted;
		}

		/// <summary>
		///		Gets the <see cref="IRecord{TElement, TMeta}"/> with the specified key.
		/// </summary>
		/// <value>
		///		The <see cref="IRecord{TElement, TMeta}"/>.
		/// </value>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		/// 
		public IRecord<TElement, TMeta> this[TKey key] => this._DataTable[key];

		/// <summary>
		///		Gets the <see cref="ICollection{IRecord{TElement, TMeta}}"/> with the specified keys.
		/// </summary>
		/// <value>
		///		The <see cref="ICollection{IRecord{TElement, TMeta}}"/>.
		/// </value>
		/// <param name="keys">The keys.</param>
		/// <returns></returns>
		/// 
		public ICollection<IRecord<TElement, TMeta>> this[params TKey[] keys] =>
			Convert<TKey, IRecord<TElement, TMeta>>(keys, i => this._DataTable[i]);

		/// <summary>
		///		Finds the specified first record by given <paramref name="element"/>.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns></returns>
		/// 
		public IRecord<TElement, TMeta> Find(TElement element) =>
			this._DataTable.First(i => i.Value.GetElement().CompareTo(element) == 0).Value;

		/// <summary>
		///		Finds the specified first record by given <paramref name="meta"/>data.
		/// </summary>
		/// <param name="meta">The meta.</param>
		/// <returns></returns>
		/// 
		public IRecord<TElement, TMeta> Find(TMeta meta) =>
			this._DataTable.First(i => i.Value.GetMeta().CompareTo(meta) == 0).Value;

		/// <summary>
		///		Finds the specified first records by given <paramref name="elements"/>.
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns></returns>
		/// 
		public ICollection<IRecord<TElement, TMeta>> Find(params TElement[] elements) =>
			elements.Select(i => this._DataTable.First(j => j.Value.GetElement().CompareTo(i) == 0).Value) as ICollection<IRecord<TElement, TMeta>>;

		/// <summary>
		///		Finds the specified record by given <paramref name="metadata"/>.
		/// </summary>
		/// <param name="metadata">The metadata.</param>
		/// <returns></returns>
		/// 
		public ICollection<IRecord<TElement, TMeta>> Find(params TMeta[] metadata) =>
			metadata.Select(i => this._DataTable.First(j => j.Value.GetMeta().CompareTo(i) == 0).Value) as ICollection<IRecord<TElement, TMeta>>;

		/// <summary>
		///		Finds all records matching given predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		/// 
		public ICollection<IRecord<TElement, TMeta>> Find(Predicate<IRecord<TElement, TMeta>> predicate) =>
			this._DataTable.Where(i => predicate(i.Value)).ToList() as ICollection<IRecord<TElement, TMeta>>;

		/// <summary>
		///		Finds all records matching specified parameter.
		/// </summary>
		/// <typeparam name="TSeeker">The type of the parameter to be matched.</typeparam>
		/// <param name="seek">The parameter to be matched</param>
		/// <param name="comparer">The comparer of given parameter and given element from table</param>
		/// <returns></returns>
		/// 
		public ICollection<IRecord<TElement, TMeta>> Find<TSeeker>(
			TSeeker seek,
			Func<TSeeker, IRecord<TElement, TMeta>, IRecord<TElement, TMeta>> comparer
		) => 
			this._DataTable.Select(i => comparer(seek, i.Value)).ToList();

		/// <summary>
		///		Adds the specified record.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="record">The record.</param>
		/// <returns></returns>
		/// 
		public Flags.Status Add(TKey key, IRecord<TElement, TMeta> record) => 
			Add(key, record.GetElement(), record.GetMeta());

		/// <summary>
		///		Adds the specified record.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="meta">The meta.</param>
		/// <returns></returns>
		/// 
		public Flags.Status Add(TElement element, TMeta meta = null) => 
			Add(this._DataTable.Keys.Max().GetNext(), element, meta);

		/// <summary>
		/// Adds the specified record.
		/// </summary>
		/// <param name="record">The record.</param>
		/// <returns></returns>
		public Flags.Status Add(IRecord<TElement, TMeta> record) => 
			Add(this._DataTable.Keys.Max().GetNext(), record.GetElement(), record.GetMeta());

		/// <summary>
		///		Adds the specified record.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="element">The element.</param>
		/// <param name="meta">The meta.</param>
		/// <returns></returns>
		/// 
		public Flags.Status Add(TKey key, TElement element, TMeta meta = null)
		{
			Contract.Requires<NullReferenceException>(this._DataTable != null);

			if (this[key] != null)
				return Flags.Status.Obstacle;

			int prevLength = this.GetLength().Value;

			this._DataTable.Add(key, new Record<TKey, TElement, TMeta>(key, element, meta));

			return prevLength == this.GetLength().Value ? Flags.Status.Failure : Flags.Status.Success;
		}

		/// <summary>
		///		Adds the specified record without any checks.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="element">The element.</param>
		/// <param name="meta">The meta.</param>
		/// 
		public void UnsafeAdd(TKey key, TElement element, TMeta meta) => 
			this._DataTable.Add(key, new Record<TKey, TElement, TMeta>(key, element, meta));

		/// <summary>
		///		Adds the specified record without any checks.
		/// </summary>
		/// <param name="record">Record to be added</param>
		/// 
		public void UnsafeAdd(Record<TKey, TElement, TMeta> record) =>
			this._DataTable.Add(record.GetKey(), record);

		/// <summary>
		///		Removes the record by given key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///		<see cref="Flags.Status.NotFound"/>
		///		<see cref="Flags.Status.Failure" />
		///		<see cref="Flags.Status.Success" />
		/// </returns>
		/// 
		public Flags.Status Remove(TKey key)
		{
			Contract.Requires<NullReferenceException>(this._DataTable != null);

			if (this[key] == null)
				return Flags.Status.NotFound;

			int prevLength = this.GetLength().Value;

			this._DataTable.Remove(key);

			return prevLength != this.GetLength().Value ? Flags.Status.Failure : Flags.Status.Success;
		}

		/// <summary>
		///		Removes the records with given element.
		///		Tries to remove "<paramref name="count"/>" number of occurences.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="count">Number of occurences to delete until it empties the table.</param>
		/// <returns>
		///		<see cref="Flags.Status.NotFound"/>
		///		<see cref="Flags.Status.Failure" />
		///		<see cref="Flags.Status.Success" />
		/// </returns>
		/// 
		public Flags.Status Remove(TElement element, int count = 1) => 
			Remove(element, (i, j) => i.CompareTo(j.GetElement()) == 0, count);

		/// <summary>
		///		Removes the records with given metadata.
		///		Tries to remove "<paramref name="count"/>" number of occurences.
		/// </summary>
		/// <param name="meta">The meta.</param>
		/// <param name="count">Number of occurences to delete until it empties the table.</param>
		/// <returns>
		///		<see cref="Flags.Status.NotFound"/>
		///		<see cref="Flags.Status.Failure" />
		///		<see cref="Flags.Status.Success" />
		/// </returns>
		/// 
		public Flags.Status Remove(TMeta meta, int count = 1) =>
			Remove(meta, (i, j) => i.CompareTo(j.GetMeta()) == 0, count);

		/// <summary>
		///		Removes the records.
		///		Tries to remove "<paramref name="count"/>" number of occurences.
		/// </summary>
		/// <param name="record">The record.</param>
		/// <param name="count">Number of occurences to delete until it empties the table.</param>
		/// <returns></returns>
		/// 
		public Flags.Status Remove(IRecord<TElement, TMeta> record, int count = 1) =>
			Remove(record, (i, j) => i.GetElement().CompareTo(j.GetElement()) == 0 && i.GetMeta().CompareTo(j.GetMeta()) == 0, count);

		/// <summary>
		///		Removes the specified matching occurences with given <typeparamref name="TRemove"/> type element and given
		///		function returning bool.
		///		Tries to remove "<paramref name="count"/>" number of occurences.
		/// </summary>
		/// <typeparam name="TRemove">The type of the remove.</typeparam>
		/// <param name="remove">The remove.</param>
		/// <param name="func">The function.</param>
		/// <param name="count">Number of occurences to delete until it empties the table.</param>
		/// <returns></returns>
		/// 
		public Flags.Status Remove<TRemove>(TRemove remove, Func<TRemove, IRecord<TElement, TMeta>, bool> func, int count = 1)
		{
			Contract.Requires<NullReferenceException>(this._DataTable != null);

			int prevLength = GetLength().Value;

			if (prevLength < count)
				count = prevLength;

			IEnumerable<TKey> found = this._DataTable.Where(
				i => func(remove, i.Value) && count-- > 0
			).Select(i => i.Key);

			if (found == null)
				return Flags.Status.NotFound;

			foreach (var f in found)
				this._DataTable.Remove(f);

			return this.GetLength() == prevLength ? Flags.Status.Failure : Flags.Status.Success;
		}

		/// <summary>
		///		Removes given record without any checks.
		/// </summary>
		/// <param name="key">The key.</param>
		/// 
		public void UnsafeRemove(TKey key) =>
			this._DataTable.Remove(key);

		/// <summary>
		///		Projects every record into a record with new element of new form.
		/// </summary>
		/// <typeparam name="TSelect">The type of the select.</typeparam>
		/// <param name="selection">The selection.</param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException"/>
		/// 
		public ICollection<IRecord<TSelect, TMeta>> Select<TSelect>(Func<IRecord<TElement, TMeta>, TSelect> selection)
					where TSelect : class, IElement<TSelect>, IComparable<TSelect> 
			=> (ICollection<IRecord<TSelect, TMeta>>)
			this._DataTable.Select(i => 
				new Record<TKey, TSelect, TMeta>(i.Key, selection(i.Value), i.Value.GetMeta())
			).ToList();

		/// <summary>
		///		Filters the data based on given predicate
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns></returns>
		/// <exception cref="NullReferenceException"/>
		/// 
		public ICollection<IRecord<TElement, TMeta>> Where(Predicate<IRecord<TElement, TMeta>> predicate) => 
			this._DataTable.Where(i => predicate(i.Value)).
							Select(i => i.Value) as ICollection<IRecord<TElement, TMeta>>;

		/// <summary>
		///		Not implemented.
		/// </summary>
		/// <param name="other"></param>
		/// <param name="predicate"></param>
		/// <exception cref="NotImplementedException"></exception>
		/// 
		public ITable<TKey, TElement, TMeta> Join(ITable<TKey, TElement, TMeta> other, Predicate<IRecord<TElement, TMeta>> predicate) => 
			throw new NotImplementedException();

		/// <summary>
		///		Not implemented.
		/// </summary>
		/// <param name="startIndex">The starting index.</param>
		/// <param name="endIndex">The end index.</param>
		/// <exception cref="NotImplementedException"></exception>
		/// 
		public ITable<TKey, TElement, TMeta> Split(TKey startIndex, TKey endIndex) => 
			throw new NotImplementedException();

		/// <summary>
		///		Not implemented.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <exception cref="NotImplementedException"></exception>
		/// 
		public ITable<TKey, TElement, TMeta> ForEach(Action<IRecord<TElement, TMeta>> action) => 
			throw new NotImplementedException();

		/// <summary>
		///		Not implemented.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <param name="func">The function.</param>
		/// <exception cref="NotImplementedException"></exception>
		/// 
		public ITable<TKey, TElement, TMeta> Cartesian(
			ITable<TKey, TElement, TMeta> other, 
			Func<IRecord<TElement, TMeta>, IRecord<TElement, TMeta>> func
		) => 
			throw new NotImplementedException();

		/// <summary>
		///		Not implemented.
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
		/// 
		public IDictionary<TKey, IRecord<TElement, TMeta>> GetTable() => 
			throw new NotImplementedException();

		/// <summary>
		///		Gets the count of records saved into this table.
		/// </summary>
		/// <returns></returns>
		/// 
		public int? GetLength() => this._DataTable?.Count;

		private ICollection<K> Convert<T, K>(T[] array, Func<T, K> func) =>
			array.Select(i => func(i)) as ICollection<K>;

		private SortedDictionary<TKey, Record<TKey, TElement, TMeta>> _DataTable;
		private bool _IsEncrypted;
	}
}
