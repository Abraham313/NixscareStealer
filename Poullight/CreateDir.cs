using System;
using System.IO;

// Token: 0x02000021 RID: 33
internal class CreateDir
{
	// Token: 0x06000055 RID: 85 RVA: 0x00005AF0 File Offset: 0x00003CF0
	public static string create(string dir1, string dir2 = null, string dir3 = null)
	{
		string text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + dir1;
		try
		{
			Sqlite.SqliteFile();
			if (Directory.Exists(text))
			{
				Directory.Delete(text, true);
			}
			Directory.CreateDirectory(text);
		}
		catch
		{
			try
			{
				text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ((dir2 == null) ? dir1 : dir2);
				if (Directory.Exists(text))
				{
					Directory.Delete(text, true);
				}
				Directory.CreateDirectory(text);
			}
			catch
			{
				try
				{
					text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + ((dir3 == null) ? dir1 : dir3);
					if (Directory.Exists(text))
					{
						Directory.Delete(text, true);
					}
					Directory.CreateDirectory(text);
				}
				catch
				{
					Environment.Exit(0);
				}
			}
		}
		return text;
	}
}
