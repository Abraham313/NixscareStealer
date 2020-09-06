using System;
using System.Text;

// Token: 0x0200001D RID: 29
internal class Base64
{
	// Token: 0x06000049 RID: 73 RVA: 0x000056E6 File Offset: 0x000038E6
	public static string Encode(string text, Encoding encode = null)
	{
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		return Convert.ToBase64String(((encode == null) ? Encoding.UTF8 : encode).GetBytes(text));
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00005708 File Offset: 0x00003908
	public static string Decode(string text, Encoding encode = null)
	{
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		return ((encode == null) ? Encoding.UTF8 : encode).GetString(Convert.FromBase64String(text));
	}
}
