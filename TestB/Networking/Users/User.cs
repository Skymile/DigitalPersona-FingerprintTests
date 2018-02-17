using System;

using FDB.Biometrics;
using FDB.Database.Interface;

namespace FDB.Networking.Users
{
	/// <summary>
	///		A class representing one user.
	/// </summary>
	/// <seealso cref="FDB.Database.Interface.IElement{FDB.Networking.Users.User}" />
	/// <seealso cref="System.IComparable{FDB.Networking.Users.User}" />
	public class User : IElement<User>, IComparable<User>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <param name="finger">The finger.</param>
		/// 
		public User(string username, string password, Fingerprint finger)
        {
            this.Username = username;
            this.Password = password;
			this.Finger = finger;
        }

		/// <summary>
		///		The username
		/// </summary>
		/// 
		public readonly string Username;

		/// <summary>
		///		The password
		/// </summary>
		/// 
		public readonly string Password;

		/// <summary>
		///		The fingerprint data
		/// </summary>
		/// 
		public readonly Fingerprint Finger;

		/// <summary>
		///		Guest user.
		/// </summary>
		/// <value>
		///		The guest.
		/// </value>
		/// 
		public static User Guest => new User("Guest", "", null);

		/// <summary>
		///		Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, 
		///		follows, or occurs in the same position in the sort order as the other object.
		///		<para/>
		///		Comparison goes by <see cref="Username"/>.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>
		///		A value that indicates the relative order of the objects being compared. 
		///		The return value has these meanings: 
		///			Less than zero This instance precedes <paramref name="other" /> in the sort order.  
		///			Zero This instance occurs in the same position in the sort order as <paramref name="other" />. 
		///			Greater than zero This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		/// 
		public int CompareTo(User other) =>
			this.Username.CompareTo(other.Username);
	}
}
