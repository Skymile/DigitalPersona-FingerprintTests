using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDB.Database
{
	public class Record<T, TKey, TElement>
		where T : ILookup<TKey, TElement>
		where TElement : IDescription<string[], Description>
	{
		public Record(T data)
		{
			this.Data = data;
		}

		private readonly T Data;
	}
}
