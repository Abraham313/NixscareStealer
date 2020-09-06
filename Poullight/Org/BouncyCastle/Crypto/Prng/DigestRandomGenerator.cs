using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000046 RID: 70
	public class DigestRandomGenerator : IRandomGenerator
	{
		// Token: 0x06000114 RID: 276 RVA: 0x000090B4 File Offset: 0x000072B4
		public DigestRandomGenerator(IDigest digest)
		{
			this.digest = digest;
			this.seed = new byte[digest.GetDigestSize()];
			this.seedCounter = 1L;
			this.state = new byte[digest.GetDigestSize()];
			this.stateCounter = 1L;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009100 File Offset: 0x00007300
		public void AddSeedMaterial(byte[] inSeed)
		{
			lock (this)
			{
				this.DigestUpdate(inSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009154 File Offset: 0x00007354
		public void AddSeedMaterial(long rSeed)
		{
			lock (this)
			{
				this.DigestAddCounter(rSeed);
				this.DigestUpdate(this.seed);
				this.DigestDoFinal(this.seed);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000091A8 File Offset: 0x000073A8
		public void NextBytes(byte[] bytes)
		{
			this.NextBytes(bytes, 0, bytes.Length);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000091B8 File Offset: 0x000073B8
		public void NextBytes(byte[] bytes, int start, int len)
		{
			lock (this)
			{
				int num = 0;
				this.GenerateState();
				int num2 = start + len;
				for (int i = start; i < num2; i++)
				{
					if (num == this.state.Length)
					{
						this.GenerateState();
						num = 0;
					}
					bytes[i] = this.state[num++];
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000922C File Offset: 0x0000742C
		private void CycleSeed()
		{
			this.DigestUpdate(this.seed);
			long num = this.seedCounter;
			this.seedCounter = num + 1L;
			this.DigestAddCounter(num);
			this.DigestDoFinal(this.seed);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000926C File Offset: 0x0000746C
		private void GenerateState()
		{
			long num = this.stateCounter;
			this.stateCounter = num + 1L;
			this.DigestAddCounter(num);
			this.DigestUpdate(this.state);
			this.DigestUpdate(this.seed);
			this.DigestDoFinal(this.state);
			if (this.stateCounter % 10L == 0L)
			{
				this.CycleSeed();
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000092C8 File Offset: 0x000074C8
		private void DigestAddCounter(long seedVal)
		{
			byte[] array = new byte[8];
			Pack.UInt64_To_LE((ulong)seedVal, array);
			this.digest.BlockUpdate(array, 0, array.Length);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000092F3 File Offset: 0x000074F3
		private void DigestUpdate(byte[] inSeed)
		{
			this.digest.BlockUpdate(inSeed, 0, inSeed.Length);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009305 File Offset: 0x00007505
		private void DigestDoFinal(byte[] result)
		{
			this.digest.DoFinal(result, 0);
		}

		// Token: 0x04000071 RID: 113
		private const long CYCLE_COUNT = 10L;

		// Token: 0x04000072 RID: 114
		private long stateCounter;

		// Token: 0x04000073 RID: 115
		private long seedCounter;

		// Token: 0x04000074 RID: 116
		private IDigest digest;

		// Token: 0x04000075 RID: 117
		private byte[] state;

		// Token: 0x04000076 RID: 118
		private byte[] seed;
	}
}
