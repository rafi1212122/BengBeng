using System.Runtime.InteropServices;
using System.Text;

namespace BengBeng.Common.Serialization
{
    public class DATA_STRING
    {
        public int SIZE;
		public short m_uLen;
        public byte[] m_pData;

        public DATA_STRING(int size)
        {
            SIZE = size;
            m_uLen = 0;
            m_pData = new byte[size];
        }

        public void SetData(byte[] pData, short uLen)
        {
            Array.Copy(pData, 0, m_pData, 0, uLen);
            m_uLen = uLen;
        }

        public int GetSize()
        {
            return Marshal.SizeOf(typeof(short)) + m_uLen;
        }

        public int GetLength()
        {
            return m_uLen;
        }

        public byte[] GetData()
        {
            return m_pData;
        }

        public string GetUtf8()
        {
            return new UTF8Encoding().GetString(m_pData);
        }

        public override string ToString()
        {
            return GetUtf8();
        }
    }
}
