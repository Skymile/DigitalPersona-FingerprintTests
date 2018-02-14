using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FDB.Database;
using FDB.Database.Interface;


namespace FDB.Networking
{
	using LoggerItem = Record<int, LogItem, Description<string>>;
	using ILogger = ITable<Record<int, LogItem, Description<string>>, int, LogItem, Description<string>>;
	using LoggerTable = Table<Record<int, LogItem, Description<string>>, int, LogItem, Description<string>>;
	using System.Collections;

	public class Logger : ILogger
	{
		public Logger(LoggerTable loggerData)
		{
			this.LoggerData = loggerData;
		}

		public Flags.Status Add(LoggerItem item ) =>
			this.LoggerData.Add(item);

		public Flags.Status Add(int key, LoggerItem record)
		{
			throw new NotImplementedException();
		}

		public Flags.Status Remove(LoggerItem record)
		{
			throw new NotImplementedException();
		}

		public Flags.Status Remove(int record)
		{
			throw new NotImplementedException();
		}

		public ILogger Select(Predicate<LoggerItem> predicate)
		{
			throw new NotImplementedException();
		}

		public ICollection<TSelect> Select<TSelect>(Func<Predicate<LoggerItem>, TSelect> func)
		{
			throw new NotImplementedException();
		}

		public ILogger Where(Predicate<LoggerItem> predicate)
		{
			throw new NotImplementedException();
		}

		public ILogger Join(ILogger other)
		{
			throw new NotImplementedException();
		}

		public ILogger Split(int startIndex, int endIndex)
		{
			throw new NotImplementedException();
		}

		public int? GetLength()
		{
			throw new NotImplementedException();
		}

		public int? GetSize()
		{
			throw new NotImplementedException();
		}

		public IDictionary<int, LoggerItem> GetTable()
		{
			throw new NotImplementedException();
		}

		public int GetNextKey()
		{
			throw new NotImplementedException();
		}

		public IEnumerator<LoggerItem> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public LoggerTable LoggerData;

		public ICollection<LoggerItem> this[params LogItem[] key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public LoggerItem this[LogItem key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ICollection<LoggerItem> this[params Description<string>[] key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public LoggerItem this[Description<string> key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ICollection<LoggerItem> this[params int[] key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public LoggerItem this[int key]
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
