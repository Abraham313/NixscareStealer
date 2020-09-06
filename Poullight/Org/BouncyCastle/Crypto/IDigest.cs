using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200003B RID: 59
	public interface IDigest
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000B0 RID: 176
		string AlgorithmName { get; }

		// Token: 0x060000B1 RID: 177
		int GetDigestSize();

		// Token: 0x060000B2 RID: 178
		int GetByteLength();

		// Token: 0x060000B3 RID: 179
		void Update(byte input);

		// Token: 0x060000B4 RID: 180
		void BlockUpdate(byte[] input, int inOff, int length);

		// Token: 0x060000B5 RID: 181
		int DoFinal(byte[] output, int outOff);

		// Token: 0x060000B6 RID: 182
		void Reset();
	}
}
