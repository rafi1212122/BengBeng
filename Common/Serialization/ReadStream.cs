using System.Buffers.Binary;
using System.Net;
using System.Runtime.InteropServices;

namespace BengBeng.Common.Serialization
{
    public class ReadStream
    {
        protected byte[] _mpBuffer;
        protected uint _miBufferLen;
        protected uint _miBufferPos;

        public ReadStream(byte[] buf, uint alen)
        {
            _mpBuffer = buf;
            _miBufferLen = alen;
            _miBufferPos = 0U;
        }

        public void Reset(byte[] buf, uint alen)
        {
            _mpBuffer = buf;
            _miBufferLen = alen;
            _miBufferPos = 0U;
        }

        public byte Read8()
        {
            byte result = _mpBuffer[(int)(UIntPtr)_miBufferPos];
            _miBufferPos += (uint)Marshal.SizeOf(typeof(byte));
            return result;
        }

        public byte Serialize(byte uValue)
        {
            return Read8();
        }

        public byte Serialize(byte uValue, short auLimit)
        {
            uValue = Read8();
            if (uValue > auLimit)
                uValue = (byte)auLimit;
            return uValue;
        }

        public short Read16()
        {
            short result = (short)BitConverter.ToUInt16(_mpBuffer, (int)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(typeof(short));
            return result;
        }

        public short Serialize(short uValue)
        {
            return IPAddress.NetworkToHostOrder(Read16());
        }

        public ushort Serialize(ushort uValue)
        {
            return (ushort)IPAddress.NetworkToHostOrder(Read16());
        }

        public short Serialize(short uValue, int auLimit)
        {
            uValue = IPAddress.NetworkToHostOrder(Read16());
            if (uValue > auLimit)
            {
                uValue = (short)auLimit;
            }
            return uValue;
        }

        public int Read32()
        {
            int result = (int)BitConverter.ToUInt32(_mpBuffer, (int)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(typeof(int));
            return result;
        }

        public int Serialize(int uValue)
        {
            return IPAddress.NetworkToHostOrder(Read32());
        }

        public int Serialize(int uValue, int auLimit)
        {
            uValue = IPAddress.NetworkToHostOrder(Read32());
            if (uValue > auLimit)
            {
                uValue = auLimit;
            }
            return uValue;
        }

        public long Read64()
        {
            long result = (long)BitConverter.ToUInt64(_mpBuffer, (int)_miBufferPos);
            _miBufferPos += (uint)Marshal.SizeOf(typeof(long));
            return result;
        }

        public long Serialize(long uValue)
        {
            return IPAddress.NetworkToHostOrder(Read64());
        }

        public DATA_STRING Serialize(DATA_STRING aoString)
        {
            short num = IPAddress.NetworkToHostOrder(Read16());
            Array.Copy(_mpBuffer, (long)(ulong)_miBufferPos, aoString.m_pData, 0L, num);
            aoString.m_uLen = num;
            _miBufferPos += (uint)num;
            return aoString;
        }

        /*public VAR_DATA_STRING method_13(VAR_DATA_STRING var_DATA_STRING_0)
        {
            short num = Read16();
            byte[] array = new byte[(int)num];
            Array.Copy(_mpBuffer, (long)((ulong)_miBufferPos), array, 0L, (long)num);
            var_DATA_STRING_0.method_0(array, num);
            _miBufferPos += (uint)num;
            return var_DATA_STRING_0;
        }*/

        public uint Length()
        {
            return _miBufferLen;
        }

        public byte[] GetBuffer()
        {
            return _mpBuffer;
        }
    }

}
