using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public class CryptoException : Exception
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x000073E8 File Offset: 0x000055E8
		public CryptoException()
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000073F0 File Offset: 0x000055F0
		public CryptoException(string message) : base(message)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000073F9 File Offset: 0x000055F9
		public CryptoException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
