using System;

namespace FDB
{
	public static class Contract
	{
		public static void Requires<TException>(bool condition, string name = null, string optionalDescription = null)
			where TException : Exception, new()
		{
			if (!condition)
				Requires<TException>(condition, name, new[] { optionalDescription });
		}

		public static void Requires<TException>(bool condition, string name = null, params string[] optionalDescription)
			where TException : Exception, new()
		{
			if (!condition)
			{
				string optional = String.Join("\n", optionalDescription);

#if CSHARP_SEVEN
				TException exception = default;
#else
				TException exception = default(TException);
#endif
				try
				{
					exception = Activator.CreateInstance(typeof(TException), Format(exception, name, optional)) as TException;
				}
				catch (MissingMethodException)
				{
					throw;
				}
				throw exception;
			}
		}

		public static void Assure(Action function)
		{
			try
			{
				function();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static void Assure<TException>(Action function)
			where TException : Exception
		{
			try
			{
				function();
			}
			catch (TException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				throw new UnhandledExceptionException(ex.Message);
			}
		}

		private static string Format(Exception exception, string name, string optional)
		{
#if CSHARP_SEVEN
			switch (exception)
			{
				case ArgumentNullException ex:
					return $"{name} cannot be null. " + (optional ?? String.Empty);
				default:
					return "Undefined error occured.";
			}
#else
			if (exception is ArgumentNullException)
				return $"{name} cannot be null. " + (optional ?? String.Empty);
			else
				return "Undefined error occured.";
#endif
		}
	}
}