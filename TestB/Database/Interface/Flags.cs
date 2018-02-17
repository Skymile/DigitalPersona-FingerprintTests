using System;

namespace FDB.Database.Interface
{
	/// <summary>
	///		<see langword="static"/> class containing various enums.
	/// </summary>
	public static class Flags
	{
		/// <summary>
		///		Flags representing situations from operating on collections.
		/// </summary>
		[Flags]
		public enum Status
		{
			/// <summary>
			///		Occurs when operation is succesful; 
			///		Should not be used with other flags
			/// </summary>
			Success = 1,

			/// <summary>
			///		Generic failure.
			/// </summary>
			Failure = 1 << 1,

			/// <summary>
			///		Given item not found.
			/// </summary>
			NotFound = 1 << 2,

			/// <summary>
			///		Adding impossible; item already exists.
			/// </summary>
			Obstacle = 1 << 3,

			/// <summary>
			///		Unknown error has occured.
			/// </summary>
			Unknown = 1 << 4
		}
	}
}
