using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200004A RID: 74
	public class KeyParameter : ICipherParameters
	{
		// Token: 0x06000136 RID: 310 RVA: 0x0000A78D File Offset: 0x0000898D
		public KeyParameter(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = (byte[])key.Clone();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A7B4 File Offset: 0x000089B4
		public byte[] GetKey()
		{
			return (byte[])this.key.Clone();
		}

		// Token: 0x0400008E RID: 142
		private readonly byte[] key;
	}
}
