using System.Net;
using System.Runtime.InteropServices;

namespace BengBeng.Common.Serialization
{
    public class WriteStream
    {
        protected byte[] _mpBuffer;
        protected uint _miBufferPos;

        public WriteStream(byte[] buf)
        {
            _mpBuffer = buf;
            _miBufferPos = 0;
        }

        public WriteStream()
        {
            byte[] buf = new byte[1 << 16];
            _mpBuffer = buf;
            _miBufferPos = 0;
        }

        public void Reset()
        {
            _miBufferPos = 0;
        }

        public byte[] GetBuf()
        {
            return _mpBuffer;
        }

        public uint GetLen()
        {
            return _miBufferPos;
        }

        public byte[] GetOccupied()
        {
            return _mpBuffer[..(int)_miBufferPos];
        }

        private void Write8(byte auValue)
        {
            _mpBuffer[(int)(UIntPtr)_miBufferPos] = auValue;
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(byte auValue)
        {
            Write8(auValue);
        }

        public void Serialize(byte auValue, byte auLimit)
        {
            if (auValue > auLimit)
            {
                auValue = auLimit;
            }
            Write8(auValue);
        }

        private void Write16(short auValue)
        {
            BitConverter.GetBytes(auValue).CopyTo(_mpBuffer, (long)(ulong)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(short auValue)
        {
            Write16(IPAddress.HostToNetworkOrder(auValue));
        }

        public void Serialize(ushort auValue)
        {
            Write16(IPAddress.HostToNetworkOrder((short)auValue));
        }

        public void Serialize(short auValue, short auLimit)
        {
            if (auValue > auLimit)
            {
                auValue = auLimit;
            }
            Write16(IPAddress.HostToNetworkOrder(auValue));
        }

        private void Write32(int auValue)
        {
            BitConverter.GetBytes(auValue).CopyTo(_mpBuffer, (long)(ulong)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(int auValue)
        {
            Write32(IPAddress.HostToNetworkOrder(auValue));
        }

        public void Serialize(int auValue, int auLimit)
        {
            if (auValue > auLimit)
            {
                auValue = auLimit;
            }
            Write32(IPAddress.HostToNetworkOrder(auValue));
        }

        private void Write64(long auValue)
        {
            BitConverter.GetBytes(auValue).CopyTo(_mpBuffer, (long)(ulong)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(long auValue)
        {
            Write64(IPAddress.HostToNetworkOrder(auValue));
        }

        public void Serialize(DATA_STRING aoDataStr)
        {
            Write16(IPAddress.HostToNetworkOrder(aoDataStr.m_uLen));
            Array.Copy(aoDataStr.m_pData, 0L, _mpBuffer, (long)(ulong)_miBufferPos, aoDataStr.m_uLen);
            _miBufferPos += (uint)aoDataStr.m_uLen;
        }

        public void Serialize(VAR_DATA_STRING aoDataStr)
        {
            Write16(IPAddress.HostToNetworkOrder(aoDataStr.m_uLen));
            if (aoDataStr.m_uLen == 0)
                return;
            Array.Copy(aoDataStr.m_pData, 0L, _mpBuffer, (long)(ulong)_miBufferPos, aoDataStr.m_uLen);
            _miBufferPos += (uint)aoDataStr.m_uLen;
        }

        public void Serialize(byte[] byteData)
        {
            Array.Copy(byteData, 0L, _mpBuffer, (long)(ulong)_miBufferPos, byteData.Length);
            _miBufferPos += (uint)byteData.Length;
        }

        public static uint GetNetStrLen(byte[] lpStr)
        {
            if (lpStr == null)
                return 0U;

            return (uint)(lpStr.Length + Marshal.SizeOf(typeof(short)) + 1);
        }
    }

    public class WriteStreamLE
    {
        protected byte[] _mpBuffer;
        protected uint _miBufferPos;

        public WriteStreamLE(byte[] buf)
        {
            _mpBuffer = buf;
            _miBufferPos = 0;
        }

        public WriteStreamLE()
        {
            byte[] buf = new byte[1 << 16];
            _mpBuffer = buf;
            _miBufferPos = 0;
        }

        public void Reset()
        {
            _miBufferPos = 0;
        }

        public byte[] GetBuf()
        {
            return _mpBuffer;
        }

        public uint GetLen()
        {
            return _miBufferPos;
        }

        public byte[] GetOccupied()
        {
            return _mpBuffer[..(int)_miBufferPos];
        }

        private void Write8(byte auValue)
        {
            _mpBuffer[(int)(UIntPtr)_miBufferPos] = auValue;
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(byte auValue)
        {
            Write8(auValue);
        }

        public void Serialize(byte auValue, byte auLimit)
        {
            if (auValue > auLimit)
            {
                auValue = auLimit;
            }
            Write8(auValue);
        }

        private void Write16(short auValue)
        {
            BitConverter.GetBytes(auValue).CopyTo(_mpBuffer, (long)(ulong)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(short auValue)
        {
            Write16(auValue);
        }

        public void Serialize(ushort auValue)
        {
            Write16((short)auValue);
        }

        public void Serialize(short auValue, short auLimit)
        {
            if (auValue > auLimit)
            {
                auValue = auLimit;
            }
            Write16(auValue);
        }

        private void Write32(int auValue)
        {
            BitConverter.GetBytes(auValue).CopyTo(_mpBuffer, (long)(ulong)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(int auValue)
        {
            Write32(auValue);
        }

        public void Serialize(uint auValue)
        {
            Write32((int)auValue);
        }

        public void Serialize(int auValue, int auLimit)
        {
            if (auValue > auLimit)
            {
                auValue = auLimit;
            }
            Write32(auValue);
        }

        private void Write64(long auValue)
        {
            BitConverter.GetBytes(auValue).CopyTo(_mpBuffer, (long)(ulong)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(auValue);
        }

        public void Serialize(long auValue)
        {
            Write64(auValue);
        }

        public void Serialize(DATA_STRING aoDataStr)
        {
            Write16(aoDataStr.m_uLen);
            Array.Copy(aoDataStr.m_pData, 0L, _mpBuffer, (long)(ulong)_miBufferPos, aoDataStr.m_uLen);
            _miBufferPos += (uint)aoDataStr.m_uLen;
        }

        public void Serialize(VAR_DATA_STRING aoDataStr)
        {
            Write16(aoDataStr.m_uLen);
            if (aoDataStr.m_uLen == 0)
                return;
            Array.Copy(aoDataStr.m_pData, 0L, _mpBuffer, (long)(ulong)_miBufferPos, aoDataStr.m_uLen);
            _miBufferPos += (uint)aoDataStr.m_uLen;
        }

        public void Serialize(byte[] byteData)
        {
            Array.Copy(byteData, 0L, _mpBuffer, (long)(ulong)_miBufferPos, byteData.Length);
            _miBufferPos += (uint)byteData.Length;
        }

        public static uint GetNetStrLen(byte[] lpStr)
        {
            if (lpStr == null)
                return 0U;

            return (uint)(lpStr.Length + Marshal.SizeOf(typeof(short)) + 1);
        }
    }
}
