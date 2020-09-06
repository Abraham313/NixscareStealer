using System;

namespace Org.BouncyCastle.Crypto.Utilities
{
	// Token: 0x0200003C RID: 60
	internal sealed class Pack
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00002180 File Offset: 0x00000380
		private Pack()
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00007403 File Offset: 0x00005603
		internal static void UInt32_To_BE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[off + 1] = (byte)(n >> 16);
			bs[off + 2] = (byte)(n >> 8);
			bs[off + 3] = (byte)n;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007428 File Offset: 0x00005628
		internal static void UInt32_To_BE(uint[] ns, byte[] bs, int off)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				Pack.UInt32_To_BE(ns[i], bs, off);
				off += 4;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007452 File Offset: 0x00005652
		internal static uint BE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] << 24 | (int)bs[off + 1] << 16 | (int)bs[off + 2] << 8 | (int)bs[off + 3]);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007474 File Offset: 0x00005674
		internal static void BE_To_UInt32(byte[] bs, int off, uint[] ns)
		{
			for (int i = 0; i < ns.Length; i++)
			{
				ns[i] = Pack.BE_To_UInt32(bs, off);
				off += 4;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000749E File Offset: 0x0000569E
		internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs, off);
			Pack.UInt32_To_BE((uint)n, bs, off + 4);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000074B7 File Offset: 0x000056B7
		internal static void UInt32_To_LE(uint n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
			bs[2] = (byte)(n >> 16);
			bs[3] = (byte)(n >> 24);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000074D5 File Offset: 0x000056D5
		internal static void UInt32_To_LE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[off + 1] = (byte)(n >> 8);
			bs[off + 2] = (byte)(n >> 16);
			bs[off + 3] = (byte)(n >> 24);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000074F9 File Offset: 0x000056F9
		internal static uint LE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((int)bs[off] | (int)bs[off + 1] << 8 | (int)bs[off + 2] << 16 | (int)bs[off + 3] << 24);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00007518 File Offset: 0x00005718
		internal static void UInt64_To_LE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_LE((uint)n, bs);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, 4);
		}
	}
}
