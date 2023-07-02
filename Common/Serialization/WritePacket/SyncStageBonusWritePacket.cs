using Newtonsoft.Json.Linq;

namespace Common.Serialization.WritePacket
{
    public class SyncStageBonusWritePacket : IBasePacket
    {
        private const int MAX_LIST_COUNT = 100;
        private byte num;
        private byte[] stage;
        private byte[] status;

        public SyncStageBonusWritePacket()
        {
            num = 0;
            stage = Array.Empty<byte>();
            status = Array.Empty<byte>();
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(num);
            if (num > MAX_LIST_COUNT)
                throw new InvalidDataException();
            stage = new byte[num];
            status = new byte[num];
            for (int i = 0; i < num; i++)
            {
                writeStream.Serialize(stage[i]);
                writeStream.Serialize(status[i]);
            }
        }
    }
}
