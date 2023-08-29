namespace BengBeng.Common.Serialization.Packet
{
    public class KeepLivePacket : IPacket
    {
        public KeepLivePacket()
        {
            timestamp = Environment.TickCount / 1000;
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(timestamp);
        }

        public void Serialize(ReadStream readStream)
        {
            timestamp = readStream.Serialize(timestamp);
        }

        public int GetTimestamp()
        {
            return timestamp;
        }

        private int timestamp;
    }
}
