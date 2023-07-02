using System.Text;

namespace Common.Serialization.WritePacket
{
    public class QueryEggMachineTxtWritePacket : IBasePacket
    {
        private byte count;
        private List<string> textList;
        private const byte MAX_LIST_COUNT = 10;
        private const short LIMIT_EGGMACHINE_TXT_LENGTH = 150;

        public QueryEggMachineTxtWritePacket()
        {
            count = 0;
            textList = new List<string>();
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            if (count > MAX_LIST_COUNT)
                throw new InvalidDataException();
            writeStream.Serialize(count);
            UTF8Encoding utf8Encoding = new();
            for (int i = 0; i < count; i++)
            {
                DATA_STRING data = new(LIMIT_EGGMACHINE_TXT_LENGTH);
                byte[] stringBytes = utf8Encoding.GetBytes(textList[i]);
                data.SetData(stringBytes, LIMIT_EGGMACHINE_TXT_LENGTH);
                writeStream.Serialize(data);
            }
        }
    }
}
