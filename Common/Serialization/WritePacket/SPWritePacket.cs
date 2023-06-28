namespace Common.Serialization.WritePacket
{
    public class SPWritePacket : IBasePacket
    {
        private const int MAX_NUM = 100;
        private byte num;
        private byte[] statusType;
        private short[] statusRes;

        public SPWritePacket()
        {
            num = 0;
            statusType = Array.Empty<byte>();
            statusRes = Array.Empty<short>();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            if (num > MAX_NUM)
                throw new InvalidDataException();
            writeStream.Serialize((byte)1);
            writeStream.Serialize((byte)2);
            writeStream.Serialize((ushort)17942);
        }

        public void Serialize(ReadStream readStream)
        {
            num = readStream.Serialize(num);
            if (num > MAX_NUM)
                throw new InvalidDataException();
            statusType = new byte[num];
            statusRes = new short[num];
            for (int i = 0; i < num; i++)
            {
                statusType[i] = readStream.Serialize(statusType[i]);
                statusRes[i] = readStream.Serialize(statusRes[i]);
            }
        }

        public byte[] GetSPType()
        {
            return statusType;
        }

        public short[] GetResult()
        {
            return statusRes;
        }
    }
}
