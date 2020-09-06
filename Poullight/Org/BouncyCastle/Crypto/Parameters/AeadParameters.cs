using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000049 RID: 73
	public class AeadParameters : ICipherParameters
	{
		// Token: 0x06000131 RID: 305 RVA: 0x0000A748 File Offset: 0x00008948
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText)
		{
			this.Key = key;
			this.nonce = nonce;
			this.MacSize = macSize;
			this.associatedText = associatedText;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000A76D File Offset: 0x0000896D
		public virtual KeyParameter Key { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000A775 File Offset: 0x00008975
		public virtual int MacSize { get; }

		// Token: 0x06000134 RID: 308 RVA: 0x0000A77D File Offset: 0x0000897D
		public virtual byte[] GetAssociatedText()
		{
			return this.associatedText;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A785 File Offset: 0x00008985
		public virtual byte[] GetNonce()
		{
			return this.nonce;
		}

		// Token: 0x0400008A RID: 138
		private readonly byte[] associatedText;

		// Token: 0x0400008B RID: 139
		private readonly byte[] nonce;
	}
}
