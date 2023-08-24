namespace BengBeng.Common.Serialization.ReadPacket
{
    public class GetPlayerMetaReadPacket : IBasePacket
    {
        private int uid;

        public void Serialize(ReadStream readStream)
        {
            uid = readStream.Serialize(uid);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            throw new NotImplementedException();
        }

        public int GetUid()
        {
            return uid;
        }
    }
}
