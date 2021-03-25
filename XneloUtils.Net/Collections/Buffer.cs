#region Copyright (c) 2021 Spencer Hoffa
// \file Buffer.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2021 Spencer Hoffa 
#endregion

using System;

namespace XneloUtils.Net.Collections
{
	public class Buffer<T>
	{
		private object m_Lock = new object();
		private T[] m_Buffer = null;
		private int m_BufferSize;
		private int m_DataInBuffer;


		public Buffer(int size)
		{
			if (size < 1) throw new ArgumentException("Cannot pass a size less than 1");
			lock (m_Lock)
			{
				m_BufferSize = size;
				m_Buffer = new T[m_BufferSize];
				m_DataInBuffer = 0;
			}
		}

		public void AddData(T[] data, int amtToAdd)
		{
			lock (m_Buffer)
			{
				if (m_DataInBuffer + amtToAdd > m_BufferSize)
				{
					throw new System.InsufficientMemoryException("The buffer is full.");
				}

				Array.Copy(data, 0, m_Buffer, m_DataInBuffer, amtToAdd);
				m_DataInBuffer += amtToAdd;
			}
		}

		public int DataInBuffer
		{
			get
			{
				return m_DataInBuffer;
			}
		}

		public bool HasData()
		{
			return DataInBuffer > 0;
		}

		public bool HasData(int amt)
		{
			return DataInBuffer >= amt;
		}

		public T Peek(int index)
		{
			T retVal;
			lock (m_Buffer)
			{
				if (index < 0 || index >= m_DataInBuffer)
				{
					throw new IndexOutOfRangeException($"Index must be between 0 and {m_DataInBuffer}.");
				}

				retVal = m_Buffer[index];
			}

			return retVal;
		}

		public T[] GetData(int amount)
		{
			T[] retVal = new T[amount];
			lock (m_Buffer)
			{
				if (amount <= 0) throw new ArgumentException("Must request an amount < 0");
				if (amount > m_DataInBuffer) throw new ArgumentException($"Must request an amount < {m_DataInBuffer}.");

				Array.Copy(m_Buffer, 0, retVal, 0, amount);
				// remove copied data
				/*if (m_DataInBuffer - amount == 0)
				{
					m_DataInBuffer = 0;
				}
				else
				{
					T[] newBuffer = new T[m_BufferSize];
					Array.Copy(m_Buffer, amount, newBuffer, 0, m_DataInBuffer - amount);
					m_Buffer = newBuffer;
					m_DataInBuffer -= amount;
				}*/
				Remove(amount);
			}

			return retVal;
		}

		public void Remove(int amount)
		{
			if (m_DataInBuffer - amount == 0)
			{
				m_DataInBuffer = 0;
			}
			else
			{
				T[] newBuffer = new T[m_BufferSize];
				Array.Copy(m_Buffer, amount, newBuffer, 0, m_DataInBuffer - amount);
				m_Buffer = newBuffer;
				m_DataInBuffer -= amount;
			}
		}
	}
}
