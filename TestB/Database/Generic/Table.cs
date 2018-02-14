using FDB.Database.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FDB.Database
{
	public class Table<TRecord, TKey, TElement, TDescription> : ITable<TRecord, TKey, TElement, TDescription>
		where TRecord : class, IRecord<TElement, TDescription>
		where TKey : struct, IComparable<TKey>, IEquatable<TKey>
		where TElement : IElement
		where TDescription : class, IComparable<TDescription>, IFormattable, new()
	{

		public Table(IDictionary<TKey, TRecord> table = null, bool isEncrypted = false)
		{
			this.DataTable = table;
		}

		private IDictionary<TKey, TRecord> DataTable;

		public ICollection<TRecord> this[params TElement[] keys]
		{
			get
			{
				List<TRecord> output = new List<TRecord>();
				foreach (var data in this.DataTable)
					foreach (var key in keys)
						if (data.Value.GetElement().Equals(key))
							output.Add(data.Value);
				return output;
			}
		}

		public TRecord this[TElement key]
		{
			get
			{
				foreach (var i in this.DataTable)
					if (i.Value.GetElement().Equals(key))
						return i.Value;
				return null;
			}
		}

		public ICollection<TRecord> this[params TDescription[] descriptions]
		{
			get
			{
				List<TRecord> output = new List<TRecord>();
				foreach (var data in this.DataTable)
					foreach (var description in descriptions)
						if (data.Value.GetDescription().Equals(description))
							output.Add(data.Value);
				return output;
			}
		}

		public TRecord this[TDescription description]
		{
			get
			{
				foreach (var data in this.DataTable)
					if (data.Value.GetDescription().Equals(description))
						return data.Value;
				return null;
			}
		}

		public ICollection<TRecord> this[params TKey[] keys]
		{
			get
			{
				return keys.Select(i => this.DataTable[i]).ToList();
			}
		}

		public TRecord this[TKey key]
		{
			get
			{
				return this.DataTable[key];
			}
		}

		public ITable<TRecord, TKey, TElement, TDescription> Select(Predicate<TRecord> predicate)
		{
			throw new NotImplementedException();
		}

		public ICollection<TSelect> Select<TSelect>(Func<Predicate<TRecord>, TSelect> func)
		{
			throw new NotImplementedException();
		}

		public ITable<TRecord, TKey, TElement, TDescription> Where(Predicate<TRecord> predicate)
		{
			throw new NotImplementedException();
		}

		public ITable<TRecord, TKey, TElement, TDescription> Join(ITable<TRecord, TKey, TElement, TDescription> other)
		{
			foreach (var data in other.GetTable())
				this.DataTable.Add(data.Key, data.Value);
			return this;
		}

		public ITable<TRecord, TKey, TElement, TDescription> Split(TKey startIndex, TKey endIndex)
		{
			var table = new Table<TRecord, TKey, TElement, TDescription>();

			foreach (var data in this.DataTable)
				if (data.Key.CompareTo(startIndex) != -1 && data.Key.CompareTo(endIndex) == 1)
					table.Add(data.Value);

			return table;
		}

		public int? GetLength() => this.DataTable?.Count;

		public int? GetSize() => this.DataTable?.Sum(i => i.Value.GetElement().GetSize());

		public IDictionary<TKey, TRecord> GetTable() => this.DataTable;

		public IEnumerator<TRecord> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public Flags.Status Add(TRecord record) => Add(GetNextKey(), record);

		public Flags.Status Add(TKey key, TRecord record)
		{
			try
			{
				int length = this.DataTable.Count;
				this.DataTable.Add(key, record);

				return this.DataTable.Count == length ? Flags.Status.Success : Flags.Status.Obstacle;
			}
			catch (NullReferenceException)
			{
				return Flags.Status.NotFound;
			}
			catch (ArgumentException)
			{
				return Flags.Status.Failure;
			}
		}

		public Flags.Status Remove(TRecord record)
		{

			throw new NotImplementedException();
		}

		public Flags.Status Remove(TKey record)
		{
			throw new NotImplementedException();
		}

		public TKey GetNextKey()
		{
			throw new NotImplementedException();
		}
	}
}
