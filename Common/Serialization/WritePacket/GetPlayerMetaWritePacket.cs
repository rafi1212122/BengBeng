namespace BengBeng.Common.Serialization.WritePacket
{
    public class GetPlayerMetaWritePacket : IBasePacket
    {
        private byte flag;

        public GetPlayerMetaWritePacket(byte flag)
        {
            this.flag = flag;
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(flag);
            writeStream.Serialize((byte)0);
        }
    }
}
