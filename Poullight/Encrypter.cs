using System;
using System.Security.Cryptography;

// Token: 0x02000022 RID: 34
internal class Encrypter
{
	// Token: 0x06000057 RID: 87 RVA: 0x00005BC8 File Offset: 0x00003DC8
	public static byte[] AES_Decryptor(byte[] input)
	{
		RijndaelManaged rijndaelManaged = new RijndaelManaged();
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		rijndaelManaged.Key = md5CryptoServiceProvider.ComputeHash(Encrypter.key);
		rijndaelManaged.Mode = CipherMode.ECB;
		return rijndaelManaged.CreateDecryptor().TransformFinalBlock(input, 0, input.Length);
	}

	// Token: 0x04000022 RID: 34
	public static byte[] key = new byte[]
	{
		66,
		59,
		131,
		42,
		23,
		164,
		0,
		4
	};
}
