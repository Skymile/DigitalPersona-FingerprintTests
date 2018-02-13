using System;
using System.Runtime.Serialization;

namespace FDB
{
	[Serializable]
	internal class UnhandledExceptionException : Exception
	{
		public UnhandledExceptionException()
		{
		}

		public UnhandledExceptionException(string message) : base(message)
		{
		}

		public UnhandledExceptionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected UnhandledExceptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}