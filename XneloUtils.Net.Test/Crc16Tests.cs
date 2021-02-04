#region Copyright (c) 2021 Spencer Hoffa
/// \file Crc16Tests.cs
/// \author Spencer Hoffa
/// \copyright \link LICENSE.md MIT License\endlink 2021 Spencer Hoffa 
#endregion

using NUnit.Framework;
using XneloUtils.Net.Data;

namespace XneloUtils.Net.Test
{
	public class Tests
	{
		[Test]
		public void XModemCrcTest1()
		{
			byte[] data = new byte[3];
			data[0] = 0xAB;
			data[1] = 0xCD;
			data[2] = 0xEF;
			Assert.AreEqual(0x21A4, Crc16.XModemCrc(data, 0, 3));
			//ABCDEF12345678
		}

		[Test]
		public void XModemCrcTest2()
		{
			byte[] data = new byte[7];
			data[0] = 0xAB;
			data[1] = 0xCD;
			data[2] = 0xEF;
			data[3] = 0x12;
			data[4] = 0x34;
			data[5] = 0x56;
			data[6] = 0x78;
			Assert.AreEqual(0x948A, Crc16.XModemCrc(data, 0, 7));
		}

		[Test]
		public void XModemCrcTest3()
		{
			byte[] data = new byte[11];
			data[0] = (byte)'H';
			data[1] = (byte)'e';
			data[2] = (byte)'l';
			data[3] = (byte)'l';
			data[4] = (byte)'o';
			data[5] = (byte)' ';
			data[6] = (byte)'W';
			data[7] = (byte)'o';
			data[8] = (byte)'r';
			data[9] = (byte)'l';
			data[10] = (byte)'d';
			Assert.AreEqual(0x992A, Crc16.XModemCrc(data, 0, 11));
		}
	}
}