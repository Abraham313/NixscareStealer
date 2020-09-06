using System;
using System.Threading;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Prng;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000033 RID: 51
	public class SecureRandom : Random
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00006FA8 File Offset: 0x000051A8
		public SecureRandom() : this(SecureRandom.CreatePrng("SHA256", true))
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00006FBB File Offset: 0x000051BB
		public SecureRandom(IRandomGenerator generator) : base(0)
		{
			this.generator = generator;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00006FCB File Offset: 0x000051CB
		private static SecureRandom Master { get; } = new SecureRandom(new CryptoApiRandomGenerator());

		// Token: 0x06000087 RID: 135 RVA: 0x00006FD2 File Offset: 0x000051D2
		private static long NextCounterValue()
		{
			return Interlocked.Increment(ref SecureRandom.counter);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00006FE0 File Offset: 0x000051E0
		private static DigestRandomGenerator CreatePrng(string digestName, bool autoSeed)
		{
			IDigest digest = DigestUtilities.GetDigest(digestName);
			if (digest == null)
			{
				return null;
			}
			DigestRandomGenerator digestRandomGenerator = new DigestRandomGenerator(digest);
			if (autoSeed)
			{
				digestRandomGenerator.AddSeedMaterial(SecureRandom.NextCounterValue());
				digestRandomGenerator.AddSeedMaterial(SecureRandom.GetNextBytes(SecureRandom.Master, digest.GetDigestSize()));
			}
			return digestRandomGenerator;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007028 File Offset: 0x00005228
		public static byte[] GetNextBytes(SecureRandom secureRandom, int length)
		{
			byte[] array = new byte[length];
			secureRandom.NextBytes(array);
			return array;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00007044 File Offset: 0x00005244
		public override int Next()
		{
			return this.NextInt() & int.MaxValue;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00007054 File Offset: 0x00005254
		public override int Next(int maxValue)
		{
			if (maxValue < 2)
			{
				if (maxValue < 0)
				{
					throw new ArgumentOutOfRangeException("maxValue", "cannot be negative");
				}
				return 0;
			}
			else
			{
				int num;
				if ((maxValue & maxValue - 1) == 0)
				{
					num = (this.NextInt() & int.MaxValue);
					return (int)((long)num * (long)maxValue >> 31);
				}
				int num2;
				do
				{
					num = (this.NextInt() & int.MaxValue);
					num2 = num % maxValue;
				}
				while (num - num2 + (maxValue - 1) < 0);
				return num2;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000070B8 File Offset: 0x000052B8
		public override int Next(int minValue, int maxValue)
		{
			if (maxValue <= minValue)
			{
				if (maxValue == minValue)
				{
					return minValue;
				}
				throw new ArgumentException("maxValue cannot be less than minValue");
			}
			else
			{
				int num = maxValue - minValue;
				if (num > 0)
				{
					return minValue + this.Next(num);
				}
				int num2;
				do
				{
					num2 = this.NextInt();
				}
				while (num2 < minValue || num2 >= maxValue);
				return num2;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000070FC File Offset: 0x000052FC
		public override void NextBytes(byte[] buf)
		{
			this.generator.NextBytes(buf);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000710A File Offset: 0x0000530A
		public override double NextDouble()
		{
			return Convert.ToDouble((ulong)this.NextLong()) / SecureRandom.DoubleScale;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00007120 File Offset: 0x00005320
		public virtual int NextInt()
		{
			byte[] array = new byte[4];
			this.NextBytes(array);
			return (((int)array[0] << 8 | (int)array[1]) << 8 | (int)array[2]) << 8 | (int)array[3];
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00007150 File Offset: 0x00005350
		public virtual long NextLong()
		{
			return (long)((ulong)this.NextInt() << 32 | (ulong)this.NextInt());
		}

		// Token: 0x0400003D RID: 61
		private static long counter = DateTime.UtcNow.Ticks * 100L;

		// Token: 0x0400003E RID: 62
		private static readonly double DoubleScale = Math.Pow(2.0, 64.0);

		// Token: 0x0400003F RID: 63
		protected readonly IRandomGenerator generator;
	}
}
