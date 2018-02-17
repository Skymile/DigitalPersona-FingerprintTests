using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDB.Database.Interface
{
	/// <summary>
	///		Defines a unique, not null, primary key
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// 
	public interface IKey<TKey>
		where TKey : struct, IComparable<TKey>, IEquatable<TKey>
	{
		TKey GetNext();
	}
}
