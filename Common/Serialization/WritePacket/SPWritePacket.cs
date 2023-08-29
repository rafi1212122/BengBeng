namespace BengBeng.Common.Serialization.WritePacket
{
    public class SPWritePacket : IPacket
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
            writeStream.Serialize((byte)7);

            writeStream.Serialize((byte)2);
            writeStream.Serialize((ushort)25110);
            writeStream.Serialize((byte)4);
            writeStream.Serialize((ushort)1);
            writeStream.Serialize((byte)6);
            writeStream.Serialize((ushort)0);
            writeStream.Serialize((byte)7);
            writeStream.Serialize((ushort)1);
            writeStream.Serialize((byte)11);
            writeStream.Serialize((ushort)0);
            writeStream.Serialize((byte)18);
            writeStream.Serialize((ushort)1);
            writeStream.Serialize((byte)19);
            writeStream.Serialize((ushort)1);
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
