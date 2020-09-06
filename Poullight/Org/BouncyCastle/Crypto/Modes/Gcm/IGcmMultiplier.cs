using System;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000042 RID: 66
	public interface IGcmMultiplier
	{
		// Token: 0x06000108 RID: 264
		void Init(byte[] H);

		// Token: 0x06000109 RID: 265
		void MultiplyH(byte[] x);
	}
}
