using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Poullight.Properties
{
	// Token: 0x0200002F RID: 47
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00002180 File Offset: 0x00000380
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00006DB6 File Offset: 0x00004FB6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Poullight.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00006DE2 File Offset: 0x00004FE2
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00006DE9 File Offset: 0x00004FE9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00006DF1 File Offset: 0x00004FF1
		internal static byte[] cpp
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("cpp", Resources.resourceCulture);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00006E0C File Offset: 0x0000500C
		internal static byte[] SteamCfg
		{
			get
			{
				return (byte[])Resources.ResourceManager.GetObject("SteamCfg", Resources.resourceCulture);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00006E27 File Offset: 0x00005027
		internal static string String0
		{
			get
			{
				return Resources.ResourceManager.GetString("String0", Resources.resourceCulture);
			}
		}

		// Token: 0x04000033 RID: 51
		private static ResourceManager resourceMan;

		// Token: 0x04000034 RID: 52
		private static CultureInfo resourceCulture;
	}
}
