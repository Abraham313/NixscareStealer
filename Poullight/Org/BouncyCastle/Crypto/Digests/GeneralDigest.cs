using System;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x0200003D RID: 61
	public abstract class GeneralDigest : IDigest
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000752E File Offset: 0x0000572E
		internal GeneralDigest()
		{
			this.xBuf = new byte[4];
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00007544 File Offset: 0x00005744
		public void Update(byte input)
		{
			byte[] array = this.xBuf;
			int num = this.xBufOff;
			this.xBufOff = num + 1;
			array[num] = input;
			if (this.xBufOff == this.xBuf.Length)
			{
				this.ProcessWord(this.xBuf, 0);
				this.xBufOff = 0;
			}
			this.byteCount += 1L;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000075A0 File Offset: 0x000057A0
		public void BlockUpdate(byte[] input, int inOff, int length)
		{
			length = Math.Max(0, length);
			int i = 0;
			if (this.xBufOff != 0)
			{
				while (i < length)
				{
					byte[] array = this.xBuf;
					int num = this.xBufOff;
					this.xBufOff = num + 1;
					array[num] = input[inOff + i++];
					if (this.xBufOff == 4)
					{
						this.ProcessWord(this.xBuf, 0);
						this.xBufOff = 0;
						break;
					}
				}
			}
			int num2 = (length - i & -4) + i;
			while (i < num2)
			{
				this.ProcessWord(input, inOff + i);
				i += 4;
			}
			while (i < length)
			{
				byte[] array2 = this.xBuf;
				int num = this.xBufOff;
				this.xBufOff = num + 1;
				array2[num] = input[inOff + i++];
			}
			this.byteCount += (long)length;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007659 File Offset: 0x00005859
		public virtual void Reset()
		{
			this.byteCount = 0L;
			this.xBufOff = 0;
			Array.Clear(this.xBuf, 0, this.xBuf.Length);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000767E File Offset: 0x0000587E
		public int GetByteLength()
		{
			return 64;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000C6 RID: 198
		public abstract string AlgorithmName { get; }

		// Token: 0x060000C7 RID: 199
		public abstract int GetDigestSize();

		// Token: 0x060000C8 RID: 200
		public abstract int DoFinal(byte[] output, int outOff);

		// Token: 0x060000C9 RID: 201 RVA: 0x00007684 File Offset: 0x00005884
		public void Finish()
		{
			long bitLength = this.byteCount << 3;
			this.Update(128);
			while (this.xBufOff != 0)
			{
				this.Update(0);
			}
			this.ProcessLength(bitLength);
			this.ProcessBlock();
		}

		// Token: 0x060000CA RID: 202
		internal abstract void ProcessWord(byte[] input, int inOff);

		// Token: 0x060000CB RID: 203
		internal abstract void ProcessLength(long bitLength);

		// Token: 0x060000CC RID: 204
		internal abstract void ProcessBlock();

		// Token: 0x04000041 RID: 65
		private const int BYTE_LENGTH = 64;

		// Token: 0x04000042 RID: 66
		private readonly byte[] xBuf;

		// Token: 0x04000043 RID: 67
		private long byteCount;

		// Token: 0x04000044 RID: 68
		private int xBufOff;
	}
}
