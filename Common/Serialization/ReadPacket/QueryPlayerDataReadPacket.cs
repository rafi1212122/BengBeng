namespace Common.Serialization.ReadPacket
{
    public class QueryPlayerDataReadPacket : IBasePacket
    {
        private byte requestType;

        public QueryPlayerDataReadPacket()
        {
            requestType = 0;
        }

        public void Serialize(ReadStream readStream)
        {
            requestType = readStream.Serialize(requestType);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            throw new NotImplementedException();
        }

        public enum REQ_TYPE
        {
            MAIN_UI = 100
        }
    }
}
