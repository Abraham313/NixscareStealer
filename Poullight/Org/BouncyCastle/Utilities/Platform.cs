using System;
using System.Collections;
using System.Globalization;

namespace Org.BouncyCastle.Utilities
{
	// Token: 0x02000036 RID: 54
	internal abstract class Platform
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000073AE File Offset: 0x000055AE
		internal static IList CreateArrayList(int capacity)
		{
			return new ArrayList(capacity);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000073B6 File Offset: 0x000055B6
		internal static IDictionary CreateHashtable()
		{
			return new Hashtable();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000073BD File Offset: 0x000055BD
		internal static string ToUpperInvariant(string s)
		{
			return s.ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000073CA File Offset: 0x000055CA
		internal static string GetTypeName(object obj)
		{
			return obj.GetType().FullName;
		}
	}
}
