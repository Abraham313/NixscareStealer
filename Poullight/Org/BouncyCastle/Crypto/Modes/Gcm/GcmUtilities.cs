using System;
using Org.BouncyCastle.Crypto.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000040 RID: 64
	internal abstract class GcmUtilities
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00008888 File Offset: 0x00006A88
		private static uint[] GenerateLookup()
		{
			uint[] array = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				uint num = 0U;
				for (int j = 7; j >= 0; j--)
				{
					if ((i & 1 << j) != 0)
					{
						num ^= 3774873600U >> 7 - j;
					}
				}
				array[i] = num;
			}
			return array;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000088DC File Offset: 0x00006ADC
		internal static uint[] OneAsUints()
		{
			uint[] array = new uint[4];
			array[0] = 2147483648U;
			return array;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000088EC File Offset: 0x00006AEC
		internal static void AsBytes(uint[] x, byte[] z)
		{
			Pack.UInt32_To_BE(x, z, 0);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000088F8 File Offset: 0x00006AF8
		internal static uint[] AsUints(byte[] bs)
		{
			uint[] array = new uint[4];
			Pack.BE_To_UInt32(bs, 0, array);
			return array;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008918 File Offset: 0x00006B18
		internal static void Multiply(byte[] x, byte[] y)
		{
			uint[] x2 = GcmUtilities.AsUints(x);
			uint[] y2 = GcmUtilities.AsUints(y);
			GcmUtilities.Multiply(x2, y2);
			GcmUtilities.AsBytes(x2, x);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008940 File Offset: 0x00006B40
		internal static void Multiply(uint[] x, uint[] y)
		{
			uint num = x[0];
			uint num2 = x[1];
			uint num3 = x[2];
			uint num4 = x[3];
			uint num5 = 0U;
			uint num6 = 0U;
			uint num7 = 0U;
			uint num8 = 0U;
			for (int i = 0; i < 4; i++)
			{
				int num9 = (int)y[i];
				for (int j = 0; j < 32; j++)
				{
					uint num10 = (uint)(num9 >> 31);
					num9 <<= 1;
					num5 ^= (num & num10);
					num6 ^= (num2 & num10);
					num7 ^= (num3 & num10);
					num8 ^= (num4 & num10);
					uint num11 = (uint)((int)((int)num4 << 31) >> 8);
					num4 = (num4 >> 1 | num3 << 31);
					num3 = (num3 >> 1 | num2 << 31);
					num2 = (num2 >> 1 | num << 31);
					num = (num >> 1 ^ (num11 & 3774873600U));
				}
			}
			x[0] = num5;
			x[1] = num6;
			x[2] = num7;
			x[3] = num8;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008A08 File Offset: 0x00006C08
		internal static void MultiplyP(uint[] x)
		{
			uint num = (uint)((int)GcmUtilities.ShiftRight(x) >> 8);
			x[0] ^= (num & 3774873600U);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008A30 File Offset: 0x00006C30
		internal static void MultiplyP8(uint[] x)
		{
			uint num = GcmUtilities.ShiftRightN(x, 8);
			x[0] ^= GcmUtilities.LOOKUP[(int)(num >> 24)];
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008A5C File Offset: 0x00006C5C
		internal static uint ShiftRight(uint[] x)
		{
			uint num = x[0];
			x[0] = num >> 1;
			uint num2 = num << 31;
			num = x[1];
			x[1] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[2];
			x[2] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[3];
			x[3] = (num >> 1 | num2);
			return num << 31;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008AAC File Offset: 0x00006CAC
		internal static uint ShiftRightN(uint[] x, int n)
		{
			uint num = x[0];
			int num2 = 32 - n;
			x[0] = num >> n;
			uint num3 = num << num2;
			num = x[1];
			x[1] = (num >> n | num3);
			num3 = num << num2;
			num = x[2];
			x[2] = (num >> n | num3);
			num3 = num << num2;
			num = x[3];
			x[3] = (num >> n | num3);
			return num << num2;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008B14 File Offset: 0x00006D14
		internal static void Xor(byte[] x, byte[] y)
		{
			int num = 0;
			do
			{
				int num2 = num;
				x[num2] ^= y[num];
				num++;
				int num3 = num;
				x[num3] ^= y[num];
				num++;
				int num4 = num;
				x[num4] ^= y[num];
				num++;
				int num5 = num;
				x[num5] ^= y[num];
				num++;
			}
			while (num < 16);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008B74 File Offset: 0x00006D74
		internal static void Xor(byte[] x, byte[] y, int yOff)
		{
			int num = 0;
			do
			{
				int num2 = num;
				x[num2] ^= y[yOff + num];
				num++;
				int num3 = num;
				x[num3] ^= y[yOff + num];
				num++;
				int num4 = num;
				x[num4] ^= y[yOff + num];
				num++;
				int num5 = num;
				x[num5] ^= y[yOff + num];
				num++;
			}
			while (num < 16);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008BDC File Offset: 0x00006DDC
		internal static void Xor(byte[] x, int xOff, byte[] y, int yOff, byte[] z, int zOff)
		{
			int num = 0;
			do
			{
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
			}
			while (num < 16);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008C4C File Offset: 0x00006E4C
		internal static void Xor(byte[] x, byte[] y, int yOff, int yLen)
		{
			while (--yLen >= 0)
			{
				int num = yLen;
				x[num] ^= y[yOff + yLen];
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008C6A File Offset: 0x00006E6A
		internal static void Xor(byte[] x, int xOff, byte[] y, int yOff, int len)
		{
			while (--len >= 0)
			{
				int num = xOff + len;
				x[num] ^= y[yOff + len];
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008C8D File Offset: 0x00006E8D
		internal static void Xor(uint[] x, uint[] y)
		{
			x[0] ^= y[0];
			x[1] ^= y[1];
			x[2] ^= y[2];
			x[3] ^= y[3];
		}

		// Token: 0x0400006A RID: 106
		private const uint E1 = 3774873600U;

		// Token: 0x0400006B RID: 107
		private const ulong E1L = 16212958658533785600UL;

		// Token: 0x0400006C RID: 108
		private static readonly uint[] LOOKUP = GcmUtilities.GenerateLookup();
	}
}
