using System;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200003F RID: 63
	public class GcmBlockCipher
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00007C21 File Offset: 0x00005E21
		public GcmBlockCipher(IBlockCipher c) : this(c, null)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007C2C File Offset: 0x00005E2C
		public GcmBlockCipher(IBlockCipher c, IGcmMultiplier m)
		{
			if (c.GetBlockSize() != 16)
			{
				throw new ArgumentException("cipher required with a block size of " + 16 + ".");
			}
			if (m == null)
			{
				m = new Tables8kGcmMultiplier();
			}
			this.cipher = c;
			this.multiplier = m;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00007C7D File Offset: 0x00005E7D
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCM";
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007C94 File Offset: 0x00005E94
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007C9C File Offset: 0x00005E9C
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			this.macBlock = null;
			this.initialised = true;
			if (!(parameters is AeadParameters))
			{
				throw new ArgumentException("invalid parameters passed to GCM");
			}
			AeadParameters aeadParameters = (AeadParameters)parameters;
			byte[] array = aeadParameters.GetNonce();
			this.initialAssociatedText = aeadParameters.GetAssociatedText();
			int num = aeadParameters.MacSize;
			if (num < 32 || num > 128 || num % 8 != 0)
			{
				throw new ArgumentException("Invalid value for MAC size: " + num);
			}
			this.macSize = num / 8;
			KeyParameter key = aeadParameters.Key;
			int num2 = forEncryption ? 16 : (16 + this.macSize);
			this.bufBlock = new byte[num2];
			if (array == null || array.Length < 1)
			{
				throw new ArgumentException("IV must be at least 1 byte");
			}
			if (forEncryption && this.nonce != null && Arrays.AreEqual(this.nonce, array))
			{
				if (key == null)
				{
					throw new ArgumentException("cannot reuse nonce for GCM encryption");
				}
				if (this.lastKey != null && Arrays.AreEqual(this.lastKey, key.GetKey()))
				{
					throw new ArgumentException("cannot reuse nonce for GCM encryption");
				}
			}
			this.nonce = array;
			if (key != null)
			{
				this.lastKey = key.GetKey();
			}
			if (key != null)
			{
				this.cipher.Init(true, key);
				this.H = new byte[16];
				this.cipher.ProcessBlock(this.H, 0, this.H, 0);
				this.multiplier.Init(this.H);
				this.exp = null;
			}
			else if (this.H == null)
			{
				throw new ArgumentException("Key must be specified in initial init");
			}
			this.J0 = new byte[16];
			if (this.nonce.Length == 12)
			{
				Array.Copy(this.nonce, 0, this.J0, 0, this.nonce.Length);
				this.J0[15] = 1;
			}
			else
			{
				this.gHASH(this.J0, this.nonce, this.nonce.Length);
				byte[] array2 = new byte[16];
				Pack.UInt64_To_BE((ulong)((long)this.nonce.Length * 8L), array2, 8);
				this.gHASHBlock(this.J0, array2);
			}
			this.S = new byte[16];
			this.S_at = new byte[16];
			this.S_atPre = new byte[16];
			this.atBlock = new byte[16];
			this.atBlockPos = 0;
			this.atLength = 0UL;
			this.atLengthPre = 0UL;
			this.counter = Arrays.Clone(this.J0);
			this.blocksRemaining = 4294967294U;
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007F45 File Offset: 0x00006145
		public virtual byte[] GetMac()
		{
			if (this.macBlock != null)
			{
				return Arrays.Clone(this.macBlock);
			}
			return new byte[this.macSize];
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007F68 File Offset: 0x00006168
		public virtual int GetOutputSize(int len)
		{
			int num = len + this.bufOff;
			if (this.forEncryption)
			{
				return num + this.macSize;
			}
			if (num >= this.macSize)
			{
				return num - this.macSize;
			}
			return 0;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007FA4 File Offset: 0x000061A4
		public virtual int GetUpdateOutputSize(int len)
		{
			int num = len + this.bufOff;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					return 0;
				}
				num -= this.macSize;
			}
			return num - num % 16;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007FDC File Offset: 0x000061DC
		public virtual void ProcessAadByte(byte input)
		{
			this.CheckStatus();
			this.atBlock[this.atBlockPos] = input;
			int num = this.atBlockPos + 1;
			this.atBlockPos = num;
			if (num == 16)
			{
				this.gHASHBlock(this.S_at, this.atBlock);
				this.atBlockPos = 0;
				this.atLength += 16UL;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000803C File Offset: 0x0000623C
		public virtual void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			this.CheckStatus();
			for (int i = 0; i < len; i++)
			{
				this.atBlock[this.atBlockPos] = inBytes[inOff + i];
				int num = this.atBlockPos + 1;
				this.atBlockPos = num;
				if (num == 16)
				{
					this.gHASHBlock(this.S_at, this.atBlock);
					this.atBlockPos = 0;
					this.atLength += 16UL;
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000080AC File Offset: 0x000062AC
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.CheckStatus();
			this.bufBlock[this.bufOff] = input;
			int num = this.bufOff + 1;
			this.bufOff = num;
			if (num == this.bufBlock.Length)
			{
				this.ProcessBlock(this.bufBlock, 0, output, outOff);
				if (this.forEncryption)
				{
					this.bufOff = 0;
				}
				else
				{
					Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
					this.bufOff = this.macSize;
				}
				return 16;
			}
			return 0;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008134 File Offset: 0x00006334
		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			this.CheckStatus();
			Check.DataLength(input, inOff, len, "input buffer too short");
			int num = 0;
			if (this.forEncryption)
			{
				if (this.bufOff != 0)
				{
					while (len > 0)
					{
						len--;
						this.bufBlock[this.bufOff] = input[inOff++];
						int num2 = this.bufOff + 1;
						this.bufOff = num2;
						if (num2 == 16)
						{
							this.ProcessBlock(this.bufBlock, 0, output, outOff);
							this.bufOff = 0;
							num += 16;
							break;
						}
					}
				}
				while (len >= 16)
				{
					this.ProcessBlock(input, inOff, output, outOff + num);
					inOff += 16;
					len -= 16;
					num += 16;
				}
				if (len > 0)
				{
					Array.Copy(input, inOff, this.bufBlock, 0, len);
					this.bufOff = len;
				}
			}
			else
			{
				for (int i = 0; i < len; i++)
				{
					this.bufBlock[this.bufOff] = input[inOff + i];
					int num2 = this.bufOff + 1;
					this.bufOff = num2;
					if (num2 == this.bufBlock.Length)
					{
						this.ProcessBlock(this.bufBlock, 0, output, outOff + num);
						Array.Copy(this.bufBlock, 16, this.bufBlock, 0, this.macSize);
						this.bufOff = this.macSize;
						num += 16;
					}
				}
			}
			return num;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000827C File Offset: 0x0000647C
		public int DoFinal(byte[] output, int outOff)
		{
			this.CheckStatus();
			if (this.totalLength == 0UL)
			{
				this.InitCipher();
			}
			int num = this.bufOff;
			if (this.forEncryption)
			{
				Check.OutputLength(output, outOff, num + this.macSize, "Output buffer too short");
			}
			else
			{
				if (num < this.macSize)
				{
					throw new CryptoException("data too short");
				}
				num -= this.macSize;
				Check.OutputLength(output, outOff, num, "Output buffer too short");
			}
			if (num > 0)
			{
				this.ProcessPartial(this.bufBlock, 0, num, output, outOff);
			}
			this.atLength += (ulong)this.atBlockPos;
			if (this.atLength > this.atLengthPre)
			{
				if (this.atBlockPos > 0)
				{
					this.gHASHPartial(this.S_at, this.atBlock, 0, this.atBlockPos);
				}
				if (this.atLengthPre > 0UL)
				{
					GcmUtilities.Xor(this.S_at, this.S_atPre);
				}
				long pow = (long)(this.totalLength * 8UL + 127UL >> 7);
				byte[] array = new byte[16];
				if (this.exp == null)
				{
					this.exp = new Tables1kGcmExponentiator();
					this.exp.Init(this.H);
				}
				this.exp.ExponentiateX(pow, array);
				GcmUtilities.Multiply(this.S_at, array);
				GcmUtilities.Xor(this.S, this.S_at);
			}
			byte[] array2 = new byte[16];
			Pack.UInt64_To_BE(this.atLength * 8UL, array2, 0);
			Pack.UInt64_To_BE(this.totalLength * 8UL, array2, 8);
			this.gHASHBlock(this.S, array2);
			byte[] array3 = new byte[16];
			this.cipher.ProcessBlock(this.J0, 0, array3, 0);
			GcmUtilities.Xor(array3, this.S);
			int num2 = num;
			this.macBlock = new byte[this.macSize];
			Array.Copy(array3, 0, this.macBlock, 0, this.macSize);
			if (this.forEncryption)
			{
				Array.Copy(this.macBlock, 0, output, outOff + this.bufOff, this.macSize);
				num2 += this.macSize;
			}
			else
			{
				byte[] array4 = new byte[this.macSize];
				Array.Copy(this.bufBlock, num, array4, 0, this.macSize);
				if (!Arrays.ConstantTimeAreEqual(this.macBlock, array4))
				{
					throw new CryptoException("mac check in GCM failed");
				}
			}
			this.Reset(false);
			return num2;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000084C4 File Offset: 0x000066C4
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000084D0 File Offset: 0x000066D0
		private void InitCipher()
		{
			if (this.atLength > 0UL)
			{
				Array.Copy(this.S_at, 0, this.S_atPre, 0, 16);
				this.atLengthPre = this.atLength;
			}
			if (this.atBlockPos > 0)
			{
				this.gHASHPartial(this.S_atPre, this.atBlock, 0, this.atBlockPos);
				this.atLengthPre += (ulong)this.atBlockPos;
			}
			if (this.atLengthPre > 0UL)
			{
				Array.Copy(this.S_atPre, 0, this.S, 0, 16);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008560 File Offset: 0x00006760
		private void Reset(bool clearMac)
		{
			this.cipher.Reset();
			this.S = new byte[16];
			this.S_at = new byte[16];
			this.S_atPre = new byte[16];
			this.atBlock = new byte[16];
			this.atBlockPos = 0;
			this.atLength = 0UL;
			this.atLengthPre = 0UL;
			this.counter = Arrays.Clone(this.J0);
			this.blocksRemaining = 4294967294U;
			this.bufOff = 0;
			this.totalLength = 0UL;
			if (this.bufBlock != null)
			{
				Arrays.Fill(this.bufBlock, 0);
			}
			if (clearMac)
			{
				this.macBlock = null;
			}
			if (this.forEncryption)
			{
				this.initialised = false;
				return;
			}
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008638 File Offset: 0x00006838
		private void ProcessBlock(byte[] buf, int bufOff, byte[] output, int outOff)
		{
			Check.OutputLength(output, outOff, 16, "Output buffer too short");
			if (this.totalLength == 0UL)
			{
				this.InitCipher();
			}
			byte[] array = new byte[16];
			this.GetNextCtrBlock(array);
			if (this.forEncryption)
			{
				GcmUtilities.Xor(array, buf, bufOff);
				this.gHASHBlock(this.S, array);
				Array.Copy(array, 0, output, outOff, 16);
			}
			else
			{
				this.gHASHBlock(this.S, buf, bufOff);
				GcmUtilities.Xor(array, 0, buf, bufOff, output, outOff);
			}
			this.totalLength += 16UL;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000086C8 File Offset: 0x000068C8
		private void ProcessPartial(byte[] buf, int off, int len, byte[] output, int outOff)
		{
			byte[] array = new byte[16];
			this.GetNextCtrBlock(array);
			if (this.forEncryption)
			{
				GcmUtilities.Xor(buf, off, array, 0, len);
				this.gHASHPartial(this.S, buf, off, len);
			}
			else
			{
				this.gHASHPartial(this.S, buf, off, len);
				GcmUtilities.Xor(buf, off, array, 0, len);
			}
			Array.Copy(buf, off, output, outOff, len);
			this.totalLength += (ulong)len;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000873C File Offset: 0x0000693C
		private void gHASH(byte[] Y, byte[] b, int len)
		{
			for (int i = 0; i < len; i += 16)
			{
				int len2 = Math.Min(len - i, 16);
				this.gHASHPartial(Y, b, i, len2);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000876B File Offset: 0x0000696B
		private void gHASHBlock(byte[] Y, byte[] b)
		{
			GcmUtilities.Xor(Y, b);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00008780 File Offset: 0x00006980
		private void gHASHBlock(byte[] Y, byte[] b, int off)
		{
			GcmUtilities.Xor(Y, b, off);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008796 File Offset: 0x00006996
		private void gHASHPartial(byte[] Y, byte[] b, int off, int len)
		{
			GcmUtilities.Xor(Y, b, off, len);
			this.multiplier.MultiplyH(Y);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000087B0 File Offset: 0x000069B0
		private void GetNextCtrBlock(byte[] block)
		{
			if (this.blocksRemaining == 0U)
			{
				throw new InvalidOperationException("Attempt to process too many blocks");
			}
			this.blocksRemaining -= 1U;
			uint num = 1U;
			num += (uint)this.counter[15];
			this.counter[15] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[14];
			this.counter[14] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[13];
			this.counter[13] = (byte)num;
			num >>= 8;
			num += (uint)this.counter[12];
			this.counter[12] = (byte)num;
			this.cipher.ProcessBlock(this.counter, 0, block, 0);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000885D File Offset: 0x00006A5D
		private void CheckStatus()
		{
			if (this.initialised)
			{
				return;
			}
			if (this.forEncryption)
			{
				throw new InvalidOperationException("GCM cipher cannot be reused for encryption");
			}
			throw new InvalidOperationException("GCM cipher needs to be initialised");
		}

		// Token: 0x04000051 RID: 81
		private const int BlockSize = 16;

		// Token: 0x04000052 RID: 82
		private readonly IBlockCipher cipher;

		// Token: 0x04000053 RID: 83
		private readonly IGcmMultiplier multiplier;

		// Token: 0x04000054 RID: 84
		private byte[] atBlock;

		// Token: 0x04000055 RID: 85
		private int atBlockPos;

		// Token: 0x04000056 RID: 86
		private ulong atLength;

		// Token: 0x04000057 RID: 87
		private ulong atLengthPre;

		// Token: 0x04000058 RID: 88
		private uint blocksRemaining;

		// Token: 0x04000059 RID: 89
		private byte[] bufBlock;

		// Token: 0x0400005A RID: 90
		private int bufOff;

		// Token: 0x0400005B RID: 91
		private byte[] counter;

		// Token: 0x0400005C RID: 92
		private IGcmExponentiator exp;

		// Token: 0x0400005D RID: 93
		private bool forEncryption;

		// Token: 0x0400005E RID: 94
		private byte[] H;

		// Token: 0x0400005F RID: 95
		private byte[] initialAssociatedText;

		// Token: 0x04000060 RID: 96
		private bool initialised;

		// Token: 0x04000061 RID: 97
		private byte[] J0;

		// Token: 0x04000062 RID: 98
		private byte[] lastKey;

		// Token: 0x04000063 RID: 99
		private byte[] macBlock;

		// Token: 0x04000064 RID: 100
		private int macSize;

		// Token: 0x04000065 RID: 101
		private byte[] nonce;

		// Token: 0x04000066 RID: 102
		private byte[] S;

		// Token: 0x04000067 RID: 103
		private byte[] S_at;

		// Token: 0x04000068 RID: 104
		private byte[] S_atPre;

		// Token: 0x04000069 RID: 105
		private ulong totalLength;
	}
}
