using System;
using FDB.Biometrics;
using FDB.Database.Interface;

namespace FDB.Networking.Users
{
	public class User : IElement<User>, IComparable<User>
	{
        public User(string username, string password, Fingerprint finger)
        {
            this.Username = username;
            this.Password = password;
			this.Finger = finger;
        }

		public readonly string Username;
		public readonly string Password;
		public readonly Fingerprint Finger;

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
		///			Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  
		///			Zero This instance occurs in the same position in the sort order as <paramref name="other" />. 
		///			Greater than zero This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		/// 
		public int CompareTo(User other) =>
			this.Username.CompareTo(other.Username);
	}
}
