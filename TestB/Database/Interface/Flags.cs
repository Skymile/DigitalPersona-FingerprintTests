using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDB.Database.Interface
{
	public static class Flags
	{
		public enum Status
		{
			Success,
			Failure,
			NotFound,
			Obstacle,
			Unknown
		}
	}
}
