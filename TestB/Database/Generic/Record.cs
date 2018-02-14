using FDB.Database.Interface;
using System;

namespace FDB.Database
{
	public class Record<TKey, TElement, TDescription> : IRecord<TElement, TDescription>
		where TKey : struct, IComparable<TKey>, IEquatable<TKey>
		where TElement : IElement
		where TDescription : class, IComparable<TDescription>, IFormattable, new()
	{
		public Record(TKey key, TElement data, TDescription description = null)
		{
			this.Key = key;
			this.Data = data;
			this.Description = description ?? default(TDescription);
		}

		public TKey GetKey() => this.Key;
		public TDescription GetDescription() => this.Description;
		public TElement GetElement() => this.Data;

		public bool Update(TDescription description) 
		{
			try
			{
				this.Description = description;
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private readonly TKey Key;
		private readonly TElement Data;
		private TDescription Description;
	}
}
