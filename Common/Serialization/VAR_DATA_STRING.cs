using System.Runtime.InteropServices;

namespace BengBeng.Common.Serialization
{
    public class VAR_DATA_STRING
    {
        public VAR_DATA_STRING()
        {
            m_uLen = 0;
            m_pData = Array.Empty<byte>();
        }

        public void SetData(byte[] pData, short uLen)
        {
            m_pData = pData;
            m_uLen = uLen;
        }

        public int GetSize()
        {
            return Marshal.SizeOf(typeof(short)) + (int)m_uLen;
        }

        public byte[] GetData()
        {
            return m_pData;
        }

        public short GetLength()
        {
            return m_uLen;
        }

        public short m_uLen;
        public byte[] m_pData;
    }
}
