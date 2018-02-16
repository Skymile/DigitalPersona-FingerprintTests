using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDB.Database.Interface
{
	public interface IKey<TKey>
		where TKey : struct, IComparable<TKey>, IEquatable<TKey>
	{
		TKey GetNext();
	}
}
