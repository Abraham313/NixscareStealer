using System;
using System.Text;

// Token: 0x02000026 RID: 38
internal class GetRandom
{
	// Token: 0x0600005F RID: 95 RVA: 0x00005DE4 File Offset: 0x00003FE4
	public static string String(string Alphabet = null, int Length = -1)
	{
		Random random = new Random();
		if (Length == -1)
		{
			Length = random.Next(8, 32);
		}
		if (Alphabet == null)
		{
			Alphabet = "qwertyuiopasdfghjklzxcvbnm1234567890-_";
		}
		StringBuilder stringBuilder = new StringBuilder(Length - 1);
		for (int i = 0; i < Length; i++)
		{
			int index = random.Next(0, Alphabet.Length - 1);
			stringBuilder.Append(Alphabet[index]);
		}
		return stringBuilder.ToString();
	}
}
