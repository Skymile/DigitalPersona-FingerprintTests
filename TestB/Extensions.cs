using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDB.ViewModel
{
	public static class Extensions
	{
		public static bool HasAnyItem<T>(T length)
			where T : class
		{
			return length == null || length.Equals(0) ? false : true;
		}
	}
}
