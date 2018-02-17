using System;

namespace FDB.Database.Interface
{
	/// <summary>
	///		Defines one record from database's table.
	/// </summary>
	/// <typeparam name="TElement">The type of the element.</typeparam>
	/// <typeparam name="TMeta">The type of the meta.</typeparam>
	/// 
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
