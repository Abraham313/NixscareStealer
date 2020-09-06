using System;
using System.Runtime.InteropServices;

// Token: 0x0200002E RID: 46
internal class WinApi
{
	// Token: 0x06000072 RID: 114
	[DllImport("avicap32.dll")]
	public static extern IntPtr capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);

	// Token: 0x06000073 RID: 115
	[DllImport("user32")]
	public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

	// Token: 0x06000074 RID: 116
	[DllImport("Kernel32.dll")]
	public static extern IntPtr GetModuleHandle(string running);

	// Token: 0x04000031 RID: 49
	public const int WS_CHILD = 1073741824;

	// Token: 0x04000032 RID: 50
	public const int WS_POPUP = -2147483648;
}
