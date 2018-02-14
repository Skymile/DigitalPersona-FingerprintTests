using System;

namespace FDB.Database.Interface
{
	public interface IRecord<TElement, TDescription>
		where TElement : IElement
		where TDescription : class, IComparable<TDescription>, IFormattable, new()
	{
		TDescription GetDescription();

		TElement GetElement();

		bool Update(TDescription description);
	}
}
