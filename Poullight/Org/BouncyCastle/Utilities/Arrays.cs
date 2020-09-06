using System;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x02000034 RID: 52
	public abstract class Arrays
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000071B2 File Offset: 0x000053B2
		public static bool AreEqual(byte[] a, byte[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000071CC File Offset: 0x000053CC
		public static bool ConstantTimeAreEqual(byte[] a, byte[] b)
		{
			if (a == null || b == null)
			{
				return false;
			}
			if (a == b)
			{
				return true;
			}
			int num = Math.Min(a.Length, b.Length);
			int num2 = a.Length ^ b.Length;
			for (int i = 0; i < num; i++)
			{
				num2 |= (int)(a[i] ^ b[i]);
			}
			for (int j = num; j < b.Length; j++)
			{
				num2 |= (int)(b[j] ^ ~(int)b[j]);
			}
			return num2 == 0;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000722E File Offset: 0x0000542E
		public static bool AreEqual(uint[] a, uint[] b)
		{
			return a == b || (a != null && b != null && Arrays.HaveSameContents(a, b));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007248 File Offset: 0x00005448
		private static bool HaveSameContents(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00007274 File Offset: 0x00005474
		private static bool HaveSameContents(uint[] a, uint[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000729F File Offset: 0x0000549F
		public static byte[] Clone(byte[] data)
		{
			if (data != null)
			{
				return (byte[])data.Clone();
			}
			return null;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000072B1 File Offset: 0x000054B1
		public static uint[] Clone(uint[] data)
		{
			if (data != null)
			{
				return (uint[])data.Clone();
			}
			return null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000072C4 File Offset: 0x000054C4
		public static void Fill(byte[] buf, byte b)
		{
			int i = buf.Length;
			while (i > 0)
			{
				buf[--i] = b;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000072E4 File Offset: 0x000054E4
		public static byte[] CopyOfRange(byte[] data, int from, int to)
		{
			int length = Arrays.GetLength(from, to);
			byte[] array = new byte[length];
			Array.Copy(data, from, array, 0, Math.Min(length, data.Length - from));
			return array;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007315 File Offset: 0x00005515
		private static int GetLength(int from, int to)
		{
			int num = to - from;
			if (num < 0)
			{
				throw new ArgumentException(from + " > " + to);
			}
			return num;
		}
	}
}
