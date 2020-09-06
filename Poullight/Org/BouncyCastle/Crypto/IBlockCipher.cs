using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000039 RID: 57
	public interface IBlockCipher
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000AA RID: 170
		string AlgorithmName { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000AB RID: 171
		bool IsPartialBlockOkay { get; }

		// Token: 0x060000AC RID: 172
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060000AD RID: 173
		int GetBlockSize();

		// Token: 0x060000AE RID: 174
		int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);

		// Token: 0x060000AF RID: 175
		void Reset();
	}
}
