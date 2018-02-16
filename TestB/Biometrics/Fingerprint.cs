using System;

using DPUruNet;
using static DPUruNet.Constants;

namespace FDB.Biometrics
{
	public class Fingerprint
	{
		public Fingerprint(Fid fid)
		{
			this.Fid = fid;
		}

		/// <summary>
		///		Checks whether instances are the same by reference.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj) => obj == null || GetType() != obj.GetType() ? false : base.Equals(obj);

		public override int GetHashCode() => base.GetHashCode();

		public int CompareFmd(Fingerprint other)
		{
			if (other == null)
				return 999999;

			return Comparison.Compare(
				FeatureExtraction.CreateFmdFromFid(this.Fid, Formats.Fmd.ANSI).Data, 0,
				FeatureExtraction.CreateFmdFromFid(other.Fid, Formats.Fmd.ANSI).Data, 0
			).Score;
		}

		private Fid Fid;
	}
}
