using System;
using System.Collections;
using System.Collections.Generic;

namespace FDB.Database
{
	public interface IDescription<TCollection, TDescription>
		where TCollection : class, ICollection//, new() //<TDescription>
		where TDescription : struct//, IComparable
	{
		TDescription GetDescription();

		TCollection ToCombined(TDescription description);

		TDescription ToDescription(TCollection collection);

		bool Update(TDescription description);

		bool Equals(TCollection l, TCollection r, Func<TDescription, TDescription, bool> comparer);
	}
}
