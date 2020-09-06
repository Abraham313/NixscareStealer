using System;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x02000035 RID: 53
	internal abstract class Enums
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000733C File Offset: 0x0000553C
		internal static Enum GetEnumValue(Type enumType, string s)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("Not an enumeration type", "enumType");
			}
			if (s.Length > 0 && char.IsLetter(s[0]) && s.IndexOf(',') < 0)
			{
				s = s.Replace('-', '_');
				s = s.Replace('/', '_');
				return (Enum)Enum.Parse(enumType, s, false);
			}
			throw new ArgumentException();
		}
	}
}
