namespace Common.Serialization.WritePacket
{
    public class QueryNotificationListWritePacket : IBasePacket
    {
        private const short MAX_LIST_COUNT = 100;
        private byte count;

        public QueryNotificationListWritePacket()
        {
            count = 0;
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(count, MAX_LIST_COUNT);
        }
    }
}
