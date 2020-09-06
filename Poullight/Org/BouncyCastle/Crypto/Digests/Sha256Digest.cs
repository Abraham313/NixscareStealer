﻿using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200003E RID: 62
	public class Sha256Digest : GeneralDigest
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000076C3 File Offset: 0x000058C3
		public Sha256Digest()
		{
			this.initHs();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000076DE File Offset: 0x000058DE
		public override string AlgorithmName
		{
			get
			{
				return "SHA-256";
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000076E5 File Offset: 0x000058E5
		public override int GetDigestSize()
		{
			return 32;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000076EC File Offset: 0x000058EC
		internal override void ProcessWord(byte[] input, int inOff)
		{
			this.X[this.xOff] = Pack.BE_To_UInt32(input, inOff);
			int num = this.xOff + 1;
			this.xOff = num;
			if (num == 16)
			{
				this.ProcessBlock();
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007728 File Offset: 0x00005928
		internal override void ProcessLength(long bitLength)
		{
			if (this.xOff > 14)
			{
				this.ProcessBlock();
			}
			this.X[14] = (uint)((ulong)bitLength >> 32);
			this.X[15] = (uint)bitLength;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007754 File Offset: 0x00005954
		public override int DoFinal(byte[] output, int outOff)
		{
			base.Finish();
			Pack.UInt32_To_BE(this.H1, output, outOff);
			Pack.UInt32_To_BE(this.H2, output, outOff + 4);
			Pack.UInt32_To_BE(this.H3, output, outOff + 8);
			Pack.UInt32_To_BE(this.H4, output, outOff + 12);
			Pack.UInt32_To_BE(this.H5, output, outOff + 16);
			Pack.UInt32_To_BE(this.H6, output, outOff + 20);
			Pack.UInt32_To_BE(this.H7, output, outOff + 24);
			Pack.UInt32_To_BE(this.H8, output, outOff + 28);
			this.Reset();
			return 32;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000077EA File Offset: 0x000059EA
		public override void Reset()
		{
			base.Reset();
			this.initHs();
			this.xOff = 0;
			Array.Clear(this.X, 0, this.X.Length);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007814 File Offset: 0x00005A14
		private void initHs()
		{
			this.H1 = 1779033703U;
			this.H2 = 3144134277U;
			this.H3 = 1013904242U;
			this.H4 = 2773480762U;
			this.H5 = 1359893119U;
			this.H6 = 2600822924U;
			this.H7 = 528734635U;
			this.H8 = 1541459225U;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000787C File Offset: 0x00005A7C
		internal override void ProcessBlock()
		{
			for (int i = 16; i <= 63; i++)
			{
				this.X[i] = Sha256Digest.Theta1(this.X[i - 2]) + this.X[i - 7] + Sha256Digest.Theta0(this.X[i - 15]) + this.X[i - 16];
			}
			uint num = this.H1;
			uint num2 = this.H2;
			uint num3 = this.H3;
			uint num4 = this.H4;
			uint num5 = this.H5;
			uint num6 = this.H6;
			uint num7 = this.H7;
			uint num8 = this.H8;
			int num9 = 0;
			for (int j = 0; j < 8; j++)
			{
				num8 += Sha256Digest.Sum1Ch(num5, num6, num7) + Sha256Digest.K[num9] + this.X[num9];
				num4 += num8;
				num8 += Sha256Digest.Sum0Maj(num, num2, num3);
				num9++;
				num7 += Sha256Digest.Sum1Ch(num4, num5, num6) + Sha256Digest.K[num9] + this.X[num9];
				num3 += num7;
				num7 += Sha256Digest.Sum0Maj(num8, num, num2);
				num9++;
				num6 += Sha256Digest.Sum1Ch(num3, num4, num5) + Sha256Digest.K[num9] + this.X[num9];
				num2 += num6;
				num6 += Sha256Digest.Sum0Maj(num7, num8, num);
				num9++;
				num5 += Sha256Digest.Sum1Ch(num2, num3, num4) + Sha256Digest.K[num9] + this.X[num9];
				num += num5;
				num5 += Sha256Digest.Sum0Maj(num6, num7, num8);
				num9++;
				num4 += Sha256Digest.Sum1Ch(num, num2, num3) + Sha256Digest.K[num9] + this.X[num9];
				num8 += num4;
				num4 += Sha256Digest.Sum0Maj(num5, num6, num7);
				num9++;
				num3 += Sha256Digest.Sum1Ch(num8, num, num2) + Sha256Digest.K[num9] + this.X[num9];
				num7 += num3;
				num3 += Sha256Digest.Sum0Maj(num4, num5, num6);
				num9++;
				num2 += Sha256Digest.Sum1Ch(num7, num8, num) + Sha256Digest.K[num9] + this.X[num9];
				num6 += num2;
				num2 += Sha256Digest.Sum0Maj(num3, num4, num5);
				num9++;
				num += Sha256Digest.Sum1Ch(num6, num7, num8) + Sha256Digest.K[num9] + this.X[num9];
				num5 += num;
				num += Sha256Digest.Sum0Maj(num2, num3, num4);
				num9++;
			}
			this.H1 += num;
			this.H2 += num2;
			this.H3 += num3;
			this.H4 += num4;
			this.H5 += num5;
			this.H6 += num6;
			this.H7 += num7;
			this.H8 += num8;
			this.xOff = 0;
			Array.Clear(this.X, 0, 16);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007B89 File Offset: 0x00005D89
		private static uint Sum1Ch(uint x, uint y, uint z)
		{
			return ((x >> 6 | x << 26) ^ (x >> 11 | x << 21) ^ (x >> 25 | x << 7)) + (z ^ (x & (y ^ z)));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007BAE File Offset: 0x00005DAE
		private static uint Sum0Maj(uint x, uint y, uint z)
		{
			return ((x >> 2 | x << 30) ^ (x >> 13 | x << 19) ^ (x >> 22 | x << 10)) + ((x & y) | (z & (x ^ y)));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007BD6 File Offset: 0x00005DD6
		private static uint Theta0(uint x)
		{
			return (x >> 7 | x << 25) ^ (x >> 18 | x << 14) ^ x >> 3;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007BEE File Offset: 0x00005DEE
		private static uint Theta1(uint x)
		{
			return (x >> 17 | x << 15) ^ (x >> 19 | x << 13) ^ x >> 10;
		}

		// Token: 0x04000045 RID: 69
		private const int DigestLength = 32;

		// Token: 0x04000046 RID: 70
		private uint H1;

		// Token: 0x04000047 RID: 71
		private uint H2;

		// Token: 0x04000048 RID: 72
		private uint H3;

		// Token: 0x04000049 RID: 73
		private uint H4;

		// Token: 0x0400004A RID: 74
		private uint H5;

		// Token: 0x0400004B RID: 75
		private uint H6;

		// Token: 0x0400004C RID: 76
		private uint H7;

		// Token: 0x0400004D RID: 77
		private uint H8;

		// Token: 0x0400004E RID: 78
		private uint[] X = new uint[64];

		// Token: 0x0400004F RID: 79
		private int xOff;

		// Token: 0x04000050 RID: 80
		private static readonly uint[] K = new uint[]
		{
			1116352408U,
			1899447441U,
			3049323471U,
			3921009573U,
			961987163U,
			1508970993U,
			2453635748U,
			2870763221U,
			3624381080U,
			310598401U,
			607225278U,
			1426881987U,
			1925078388U,
			2162078206U,
			2614888103U,
			3248222580U,
			3835390401U,
			4022224774U,
			264347078U,
			604807628U,
			770255983U,
			1249150122U,
			1555081692U,
			1996064986U,
			2554220882U,
			2821834349U,
			2952996808U,
			3210313671U,
			3336571891U,
			3584528711U,
			113926993U,
			338241895U,
			666307205U,
			773529912U,
			1294757372U,
			1396182291U,
			1695183700U,
			1986661051U,
			2177026350U,
			2456956037U,
			2730485921U,
			2820302411U,
			3259730800U,
			3345764771U,
			3516065817U,
			3600352804U,
			4094571909U,
			275423344U,
			430227734U,
			506948616U,
			659060556U,
			883997877U,
			958139571U,
			1322822218U,
			1537002063U,
			1747873779U,
			1955562222U,
			2024104815U,
			2227730452U,
			2361852424U,
			2428436474U,
			2756734187U,
			3204031479U,
			3329325298U
		};
	}
}
