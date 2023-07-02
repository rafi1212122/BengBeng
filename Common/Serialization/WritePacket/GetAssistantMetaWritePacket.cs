namespace Common.Serialization.WritePacket
{
    // TODO: Implement more full fields
    public class GetAssistantMetaWritePacket : IBasePacket
    {
        private const short MAX_LIST_COUNT = 100;
        private byte count;

        public GetAssistantMetaWritePacket()
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
