namespace BengBeng.Common.Serialization.ReadPacket
{
    public class QueryNotificationListReadPacket : IBasePacket
    {
        private int msgId;

        public QueryNotificationListReadPacket()
        {
            msgId = 0;
        }

        public void Serialize(ReadStream readStream)
        {
            msgId = readStream.Serialize(msgId);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            throw new NotImplementedException();
        }

        public int GetMsgId()
        {
            return msgId;
        }
    }
}
