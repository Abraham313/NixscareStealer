using System;
using System.IO;

// Token: 0x0200001A RID: 26
internal class AntiReplaySender
{
	// Token: 0x06000040 RID: 64 RVA: 0x000053DC File Offset: 0x000035DC
	public static bool CheckReplayStart()
	{
		bool result;
		try
		{
			string path = string.Format("{0}{1}", global::Buffer.path_t, Exporter.Export("<mutex>", "</mutex>", Starter.FileData).ToLower());
			if (File.Exists(path))
			{
				result = false;
			}
			else
			{
				File.WriteAllText(path, GetRandom.String(null, -1));
				result = false;
			}
		}
		catch
		{
			result = false;
		}
		return result;
	}
}
