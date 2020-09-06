using System;
using Poullight.Properties;

// Token: 0x02000027 RID: 39
internal class HandlerParams
{
	// Token: 0x06000061 RID: 97 RVA: 0x00005E4C File Offset: 0x0000404C
	public static void Start()
	{
		try
		{
			string text = Exporter.Export("<settings>", "</settings>", Resources.String0).Replace("\0", "");
			if (text != null)
			{
				text = Base64.Decode(text, null);
				string text2 = Exporter.Export("<prog.params>", "</prog.params>", text);
				string[] array = null;
				if (!string.IsNullOrEmpty(text2))
				{
					array = text2.Split(new char[]
					{
						'|'
					});
				}
				if (array != null && array.Length != 0)
				{
					Starter.FileData = text;
					Starter.Params = array;
				}
			}
		}
		catch
		{
		}
	}
}
