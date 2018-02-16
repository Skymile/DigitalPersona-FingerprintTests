using System;

namespace FDB.Database.Interface
{
	public interface IRecord<TElement, TMeta>
		where TElement : class, IElement<TElement>, IComparable<TElement>
		where TMeta : class, IComparable<TMeta>, IFormattable, new()
	{
		TMeta GetMeta();

		TElement GetElement();

		IRecord<TElement, TMeta> Update(TMeta meta);

		IRecord<TElement, TMeta> Modify(Func<IRecord<TElement, TMeta>, IRecord<TElement, TMeta>> modify);
	}
}
