using System;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000041 RID: 65
	public interface IGcmExponentiator
	{
		// Token: 0x06000106 RID: 262
		void Init(byte[] x);

		// Token: 0x06000107 RID: 263
		void ExponentiateX(long pow, byte[] output);
	}
}
