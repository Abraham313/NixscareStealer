using System;
using System.Security.Cryptography;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000045 RID: 69
	public class CryptoApiRandomGenerator : IRandomGenerator
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000908A File Offset: 0x0000728A
		public CryptoApiRandomGenerator() : this(RandomNumberGenerator.Create())
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009097 File Offset: 0x00007297
		public CryptoApiRandomGenerator(RandomNumberGenerator rng)
		{
			this.rndProv = rng;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000090A6 File Offset: 0x000072A6
		public virtual void NextBytes(byte[] bytes)
		{
			this.rndProv.GetBytes(bytes);
		}

		// Token: 0x04000070 RID: 112
		private readonly RandomNumberGenerator rndProv;
	}
}
