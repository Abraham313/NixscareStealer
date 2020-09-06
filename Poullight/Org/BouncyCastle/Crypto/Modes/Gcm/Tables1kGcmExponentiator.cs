using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000043 RID: 67
	public class Tables1kGcmExponentiator : IGcmExponentiator
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00008CD4 File Offset: 0x00006ED4
		public void Init(byte[] x)
		{
			uint[] array = GcmUtilities.AsUints(x);
			if (this.lookupPowX2 != null && Arrays.AreEqual(array, (uint[])this.lookupPowX2[0]))
			{
				return;
			}
			this.lookupPowX2 = Platform.CreateArrayList(8);
			this.lookupPowX2.Add(array);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008D24 File Offset: 0x00006F24
		public void ExponentiateX(long pow, byte[] output)
		{
			uint[] x = GcmUtilities.OneAsUints();
			int num = 0;
			while (pow > 0L)
			{
				if ((pow & 1L) != 0L)
				{
					this.EnsureAvailable(num);
					GcmUtilities.Multiply(x, (uint[])this.lookupPowX2[num]);
				}
				num++;
				pow >>= 1;
			}
			GcmUtilities.AsBytes(x, output);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008D74 File Offset: 0x00006F74
		private void EnsureAvailable(int bit)
		{
			int num = this.lookupPowX2.Count;
			if (num <= bit)
			{
				uint[] array = (uint[])this.lookupPowX2[num - 1];
				do
				{
					array = Arrays.Clone(array);
					GcmUtilities.Multiply(array, array);
					this.lookupPowX2.Add(array);
				}
				while (++num <= bit);
			}
		}

		// Token: 0x0400006D RID: 109
		private IList lookupPowX2;
	}
}
