using System;
using System.Collections.Generic;

namespace FDB.Database.Interface
{
	/// <summary>
	///		Defines the database's table.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TElement">The type of the element.</typeparam>
	/// <typeparam name="TMeta">The type of the meta.</typeparam>
	/// 
	public interface ITable<TKey, TElement, TMeta>
		where TKey     : struct, IKey<TKey>, IComparable<TKey>, IEquatable<TKey>
		where TElement : class,  IElement<TElement>, IComparable<TElement>
		where TMeta    : class,  IComparable<TMeta>, IFormattable, new()
	{
		IRecord<TElement, TMeta> this[TKey key] { get; }
		IRecord<TElement, TMeta> Find(TElement element);
		IRecord<TElement, TMeta> Find(TMeta meta);

		ICollection<IRecord<TElement, TMeta>> this[params TKey[] key] { get; }
		IEnumerable<IRecord<TElement, TMeta>> Find(params TElement[] element);
		IEnumerable<IRecord<TElement, TMeta>> Find(params TMeta[] meta);
		IEnumerable<IRecord<TElement, TMeta>> Find(Predicate<IRecord<TElement, TMeta>> predicate);
		IEnumerable<IRecord<TElement, TMeta>> Find<TSeeker>(TSeeker seek, Func<TSeeker, IRecord<TElement, TMeta>, IRecord<TElement, TMeta>> comparer);

		Flags.Status Add(TKey key, IRecord<TElement, TMeta> record);
		Flags.Status Add(TKey key, TElement element, TMeta meta = null);
		Flags.Status Add(TElement element, TMeta meta = null);
		Flags.Status Add(IRecord<TElement, TMeta> record);

		Flags.Status Remove(TKey key);
		Flags.Status Remove(TElement element, int count = 1);
		Flags.Status Remove(TMeta meta, int count = 1);
		Flags.Status Remove(IRecord<TElement, TMeta> record, int count = 1);
		Flags.Status Remove<TRemove>(TRemove remove, Func<TRemove, IRecord<TElement, TMeta>, bool> func, int count = 1);

		IEnumerable<TSelect> Select<TSelect>(Func<IRecord<TElement, TMeta>, TSelect> selection);

		ICollection<IRecord<TElement, TMeta>> Where(Predicate<IRecord<TElement, TMeta>> predicate);

		ITable<TKey, TElement, TMeta> Join(ITable<TKey, TElement, TMeta> other, Predicate<IRecord<TElement, TMeta>> predicate);

		ITable<TKey, TElement, TMeta> Split(TKey startIndex, TKey endIndex);

		ITable<TKey, TElement, TMeta> ForEach(Action<IRecord<TElement, TMeta>> action);

		ITable<TKey, TElement, TMeta> Cartesian(ITable<TKey, TElement, TMeta> other, Func<IRecord<TElement, TMeta>, IRecord<TElement, TMeta>> func);

		IDictionary<TKey, IRecord<TElement, TMeta>> GetTable();

		int? GetLength();
	}
}
