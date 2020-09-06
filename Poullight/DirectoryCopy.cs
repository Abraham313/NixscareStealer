using System;
using System.IO;

// Token: 0x02000023 RID: 35
internal class DirectoryCopy
{
	// Token: 0x0600005A RID: 90 RVA: 0x00005C20 File Offset: 0x00003E20
	public DirectoryCopy(string sourceDirectoryName, string destDirectoryName)
	{
		if (sourceDirectoryName.ToLower() == destDirectoryName.ToLower())
		{
			return;
		}
		if (!Directory.Exists(destDirectoryName))
		{
			Directory.CreateDirectory(destDirectoryName);
		}
		string[] directories = Directory.GetDirectories(sourceDirectoryName);
		if (directories.Length != 0)
		{
			foreach (string text in directories)
			{
				new DirectoryCopy(text, destDirectoryName + "\\" + Path.GetFileName(text));
			}
		}
		string[] files = Directory.GetFiles(sourceDirectoryName);
		if (files.Length != 0)
		{
			foreach (string text2 in files)
			{
				File.Copy(text2, destDirectoryName + "\\" + Path.GetFileName(text2));
			}
		}
	}
}
