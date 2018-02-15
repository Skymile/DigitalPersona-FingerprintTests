
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

		/// <summary>
		///		Checks whether instances are the same by value. (dissimilarity score)
		/// </summary>
		/// <param name="l"></param>
		/// <param name="r"></param>
		/// <returns></returns>
		public static bool operator==(Fingerprint l, Fingerprint r) =>
			Comparison.Compare(
				FeatureExtraction.CreateFmdFromFid(l.Fid, Formats.Fmd.ANSI).Data, 0, 
				FeatureExtraction.CreateFmdFromFid(r.Fid, Formats.Fmd.ANSI).Data, 0
			).ResultCode.HasFlag(ResultCode.DP_SUCCESS);

		public static bool operator!=(Fingerprint l, Fingerprint r) => !(l == r);

		private Fid Fid;
	}
}
