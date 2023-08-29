namespace BengBeng.Common.Serialization.ReadPacket
{
    public class TutorialProgressReadPacket : IPacket
    {
        private short _progress;

        public void Serialize(ReadStream readStream)
        {
            _progress = readStream.Serialize(_progress);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            throw new NotImplementedException();
        }

        public short GetProgress()
        {
            return _progress;
        }
    }
}
