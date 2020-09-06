using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000037 RID: 55
	internal class Check
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x000073D7 File Offset: 0x000055D7
		internal static void DataLength(byte[] buf, int off, int len, string msg)
		{
			if (off > buf.Length - len)
			{
				throw new CryptoException(msg);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000073D7 File Offset: 0x000055D7
		internal static void OutputLength(byte[] buf, int off, int len, string msg)
		{
			if (off > buf.Length - len)
			{
				throw new CryptoException(msg);
			}
		}
	}
}
