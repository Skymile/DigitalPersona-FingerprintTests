using System;
using System.Collections.Generic;

namespace FDB.Database.Interface
{
	public interface ITable<TRecord, TKey, TElement, TDescription> : IEnumerable<TRecord>
		where TRecord : IRecord<TElement, TDescription>
		where TKey : struct, IComparable<TKey>, IEquatable<TKey>
		where TElement : IElement
		where TDescription : class, IComparable<TDescription>, IFormattable, new()
	{
		TRecord this[TKey key] { get; }
		ICollection<TRecord> this[params TKey[] key] { get; }

		TRecord this[TDescription key] { get; }
		ICollection<TRecord> this[params TDescription[] key] { get; }

		TRecord this[TElement key] { get; }
		ICollection<TRecord> this[params TElement[] key] { get; }

		Flags.Status Add(TKey key, TRecord record);
		Flags.Status Add(TRecord record);

		Flags.Status Remove(TRecord record);
		Flags.Status Remove(TKey record);

		ITable<TRecord, TKey, TElement, TDescription> Select(Predicate<TRecord> predicate);
		ICollection<TSelect> Select<TSelect>(Func<Predicate<TRecord>, TSelect> func);

		ITable<TRecord, TKey, TElement, TDescription> Where(Predicate<TRecord> predicate);

		ITable<TRecord, TKey, TElement, TDescription> Join(ITable<TRecord, TKey, TElement, TDescription> other);

		ITable<TRecord, TKey, TElement, TDescription> Split(TKey startIndex, TKey endIndex);

		int? GetLength();
		int? GetSize();

		IDictionary<TKey, TRecord> GetTable();
		TKey GetNextKey();
	}
}
