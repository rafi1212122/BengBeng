using System.Net;

namespace BengBeng.Common.Serialization.WritePacket
{
    public class GetConnSvrInfoWritePacket : IBasePacket
    {
        private const byte MAX_CM_COUNT = 8;
        private byte[] _uServerTypeList;
        private int[] _uIPList;
        private short[] _uPortList;
        private byte _uNum;

        public GetConnSvrInfoWritePacket()
        {
            _uServerTypeList = Array.Empty<byte>();
            _uIPList = Array.Empty<int>();
            _uPortList = Array.Empty<short>();
            _uNum = 0;
        }

        public void Serialize(ReadStream readStream)
        {
            _uNum = readStream.Serialize(_uNum, MAX_CM_COUNT);
            _uServerTypeList = new byte[_uNum];
            _uIPList = new int[_uNum];
            _uPortList = new short[_uNum];
            for (int i = 0; i < _uNum; i++)
            {
                _uServerTypeList[i] = readStream.Serialize(_uServerTypeList[i]);
                _uIPList[i] = readStream.Serialize(_uIPList[i]);
                _uPortList[i] = readStream.Serialize(_uPortList[i]);
            }
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize((byte)1);
            writeStream.Serialize((byte)0);
            writeStream.Serialize((int)IPAddress.Parse(Global.config.GameServer.Host).Address);
            writeStream.Serialize((short)Global.config.GameServer.SecondaryPort);
        }
    }
}
