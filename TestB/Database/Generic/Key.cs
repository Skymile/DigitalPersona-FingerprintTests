using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDB.Database.Interface;

namespace FDB.Database.Generic
{
	public struct Key : IKey<Key>, IComparable<Key>, IEquatable<Key>
	{
		public Key(int value) => this.Value = value;

		public int CompareTo(Key other) => this.Value.CompareTo(other.Value);

		public bool Equals(Key other) => this.Value.Equals(other.Value);

		public Key GetNext() => new Key(this.Value + 1);

		public static implicit operator Key(int value) => new Key(value);

		public readonly int Value;
	}
}
