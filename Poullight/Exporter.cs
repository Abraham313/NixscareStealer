using System;

// Token: 0x02000025 RID: 37
internal class Exporter
{
	// Token: 0x0600005D RID: 93 RVA: 0x00005D80 File Offset: 0x00003F80
	public static string Export(string start, string end, string data)
	{
		string result;
		try
		{
			if (data != null && data.Contains(start) && data.Contains(end))
			{
				result = data.Split(new string[]
				{
					start
				}, StringSplitOptions.None)[1].Split(new string[]
				{
					end
				}, StringSplitOptions.None)[0];
			}
			else
			{
				result = null;
			}
		}
		catch
		{
			result = null;
		}
		return result;
	}
}
