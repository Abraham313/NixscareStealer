using System;
using System.Collections;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000031 RID: 49
	public sealed class DigestUtilities
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00006F10 File Offset: 0x00005110
		static DigestUtilities()
		{
			DigestUtilities.algorithms["SHA256"] = "SHA-256";
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002180 File Offset: 0x00000380
		private DigestUtilities()
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00006F30 File Offset: 0x00005130
		public static IDigest GetDigest(string algorithm)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			string text2 = (string)DigestUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			try
			{
				if ((DigestUtilities.DigestAlgorithm)Enums.GetEnumValue(typeof(DigestUtilities.DigestAlgorithm), text2) == DigestUtilities.DigestAlgorithm.SHA_256)
				{
					return new Sha256Digest();
				}
			}
			catch (ArgumentException)
			{
			}
			throw new CryptoException("Digest " + text2 + " not recognised.");
		}

		// Token: 0x0400003A RID: 58
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x02000032 RID: 50
		private enum DigestAlgorithm
		{
			// Token: 0x0400003C RID: 60
			SHA_256
		}
	}
}
