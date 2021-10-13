//-------------------------------------------------------------------------------------
// CRC16 support class
// Based on various examples found on the web
// Copyright (C) 2014 Vincenzo Mennella (see license.txt)
// History
//  0.1.0 31/05/2014:   First public code release
//  0.1.1 17/12/2014:   Minor revision and commented code
//  0.1.2 06/06/2019:   Fix reflect routine for 16 bit data
//                      Added ModBus and Mcrf4XX inline functions
//
// License
// "MIT Open Source Software License":
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in the
// Software without restriction, including without limitation the rights to use, copy,
// modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
// and to permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//-------------------------------------------------------------------------------------

// CSharpified by Spencer Hoffa
// 10/13/2021 disabled CS0675 warning in code.

namespace XneloUtils.Net.Data
{
	public class Crc16
	{
		public const string VERSION = "0.1.2";

		//Crc parameters
		private readonly ushort m_MsbMask;
		private readonly ushort m_Mask;
		private readonly ushort m_XorIn;
		private readonly ushort m_XorOut;
		private readonly ushort m_Polynomial;
		private readonly bool m_ReflectIn;
		private readonly bool m_ReflectOut;

		//Crc value
		private ushort m_Crc;

		/// <summary>
		/// Reflects bit in a byte
		/// </summary>
		/// <param name="data">byte value to reflect</param>
		/// <returns>reflected byte</returns>
		private static byte Reflect(byte data)
		{
			const byte bits = 8;
			ulong reflection = 0x00000000;
			// Reflect the data about the center bit.
			for (byte bit = 0; bit < bits; bit++)
			{
				// If the LSB bit is set, set the reflection of it.
				if ((data & 0x01) != 0)
				{
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
					reflection |= (ulong)(1 << ((bits - 1) - bit));
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand
				}
				data = (byte)(data >> 1);
			}

			return (byte)reflection;
		}

		/// <summary>
		/// Reflects bit in a ushort
		/// </summary>
		/// <param name="data">The ushort to reflect</param>
		/// <returns>Reflected ushort value.</returns>
		private static ushort Reflect(ushort data)
		{
			const byte bits = 16;
			ulong reflection = 0x00000000;
			// Reflect the data about the center bit.
			for (byte bit = 0; bit < bits; bit++)
			{
				// If the LSB bit is set, set the reflection of it.
				if ((data & 0x01) != 0)
				{
#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
					reflection |= (ulong)(1 << ((bits - 1) - bit));
#pragma warning restore CS0675 // Bitwise-or operator used on a sign-extended operand

				}

				data = (ushort)(data >> 1);
			}

			return (ushort)reflection;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public Crc16()
		{
			//Default to XModem parameters
			m_ReflectIn = false;
			m_ReflectOut = false;
			m_Polynomial = 0x1021;
			m_XorIn = 0x0000;
			m_XorOut = 0x0000;
			m_MsbMask = 0x8000;
			m_Mask = 0xFFFF;
			m_Crc = m_XorIn;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reflectIn"></param>
		/// <param name="reflectOut"></param>
		/// <param name="polynomial"></param>
		/// <param name="xorIn"></param>
		/// <param name="xorOut"></param>
		/// <param name="msbMask"></param>
		/// <param name="mask"></param>
		public Crc16(bool reflectIn, bool reflectOut, ushort polynomial, ushort xorIn, ushort xorOut, ushort msbMask, ushort mask)
		{
			m_ReflectIn = reflectIn;
			m_ReflectOut = reflectOut;
			m_Polynomial = polynomial;
			m_XorIn = xorIn;
			m_XorOut = xorOut;
			m_MsbMask = msbMask;
			m_Mask = mask;
			m_Crc = m_XorIn;
		}

		/// <summary>
		/// Initialize crc calculation
		/// </summary>
		public void clearCrc()
		{
			m_Crc = m_XorIn;
		}

		/// <summary>
		/// Update crc with new data
		/// </summary>
		/// <param name="data">The byte of new data to add to the crc value.</param>
		public void updateCrc(byte data)
		{
			if (m_ReflectIn)
			{
				data = (byte)Reflect(data);
			}

			int j = 0x80;

			while (j > 0)
			{
				ushort bit = (ushort)(m_Crc & m_MsbMask);

				m_Crc <<= 1;

				if ((data & j) != 0)
				{
					bit = (ushort)(bit ^ m_MsbMask);
				}

				if (bit != 0)
				{
					m_Crc ^= m_Polynomial;
				}

				j >>= 1;
			}
		}

		/// <summary>
		/// Get final crc value
		/// </summary>
		/// <returns>A ushort crc value.</returns>
		public ushort getCrc()
		{
			if (m_ReflectOut)
			{
				m_Crc = (ushort)((Reflect(m_Crc) ^ m_XorOut) & m_Mask);
			}

			return m_Crc;
		}

		//---------------------------------------------------
		// Calculate generic crc code on data array
		// Examples of crc 16:
		// Kermit: 		width=16 poly=0x1021 init=0x0000 refin=true  refout=true  xorout=0x0000 check=0x2189
		// Modbus: 		width=16 poly=0x8005 init=0xffff refin=true  refout=true  xorout=0x0000 check=0x4b37
		// XModem: 		width=16 poly=0x1021 init=0x0000 refin=false refout=false xorout=0x0000 check=0x31c3
		// CCITT-False:	width=16 poly=0x1021 init=0xffff refin=false refout=false xorout=0x0000 check=0x29b1
		//---------------------------------------------------
		public static ushort fastCrc(byte[] data, ushort start, ushort length, bool reflectIn, bool reflectOut, ushort polynomial, ushort xorIn, ushort xorOut, ushort msbMask, ushort mask)
		{
			ushort crc = xorIn;

			int j;
			byte c;
			uint bit;

			if (length == 0) return crc;

			for (int i = start; i < (start + length); i++)
			{
				c = data[i];

				if (reflectIn)
				{
					c = (byte)Reflect(c);
				}

				j = 0x80;

				while (j > 0)
				{
					bit = (uint)(crc & msbMask);
					crc <<= 1;

					if ((c & j) != 0)
					{
						bit = (uint)(bit ^ msbMask);
					}

					if (bit != 0)
					{
						crc ^= polynomial;
					}

					j >>= 1;
				}
			}

			if (reflectOut)
			{
				crc = (ushort)((Reflect((ushort)crc) ^ xorOut) & mask);
			}

			return crc;
		}

		public static ushort XModemCrc(byte[] data, ushort start, ushort length)
		{
			//  XModem parameters: poly=0x1021 init=0x0000 refin=false refout=false xorout=0x0000
			return fastCrc(data, start, length,
				false,
				false,
				0x1021, 0x0000, 0x0000, 0x8000, 0xffff);
		}

		public static ushort Mcrf4XX(byte[] data, ushort start, ushort length)
		{
			return fastCrc(data, start, length,
				true,
				true,
				0x1021, 0xffff, 0x0000, 0x8000, 0xffff);
		}

		public static ushort Modbus(byte[] data, ushort start, ushort length)
		{
			return fastCrc(data, start, length,
				true,
				true,
				0x8005, 0xffff, 0x0000, 0x8000, 0xffff);
		}
	}
}
