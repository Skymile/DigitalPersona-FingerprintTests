using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDB.Database
{
	public class Table<TKey, TElement>
		where TElement : IDescription<string[], Description>
	{
		public Table(bool isEncrypted = false, uint capacity = uint.MaxValue)
		{

		}

		public SortedDictionary<TKey, TElement> TableData;
	}
}
