namespace BengBeng.Common.Serialization.Packet
{
    // TODO: Make proper packet!
    public class LevelFinishPacket : IPacket
    {
        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(new byte[340]);
            /*writeStream.Serialize(0);
            writeStream.Serialize(0);
            writeStream.Serialize(0);
            writeStream.Serialize((short)0);
            writeStream.Serialize(0);
            writeStream.Serialize((short)0);
            writeStream.Serialize(0);
            writeStream.Serialize(0);
            writeStream.Serialize(0);
            writeStream.Serialize(0);
            writeStream.Serialize((short)0);
            writeStream.Serialize(0);
            writeStream.Serialize(0);
            writeStream.Serialize((short)0);*/
        }
    }
}
