namespace Common.Serialization.ReadPacket
{
    public class StoryProgressReadPacket : IBasePacket
    {
        private short progress;

        public StoryProgressReadPacket()
        {
            progress = 0;
        }

        public void Serialize(ReadStream readStream)
        {
            progress = readStream.Serialize(progress);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            throw new NotImplementedException();
        }

        public short GetProgress()
        {
            return progress;
        }
    }
}
