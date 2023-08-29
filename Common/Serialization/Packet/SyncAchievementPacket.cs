namespace BengBeng.Common.Serialization.Packet
{
    public class SyncAchievementPacket : IPacket
    {
        private const short MAX_LIST_COUNT = 1000;
        private short _count;
        private short[] _id;
        private byte[] _status;
        private int[] _progress;

        public SyncAchievementPacket(short count, short[] ids, byte[] statuses, int[] progresses)
        {
            _count = count;
            _id = ids;
            _status = statuses;
            _progress = progresses;
        }

        public SyncAchievementPacket()
        {
            _count = 0;
            _id = Array.Empty<short>();
            _status = Array.Empty<byte>();
            _progress = Array.Empty<int>();
        }

        public void Serialize(ReadStream readStream)
        {
            _count = readStream.Serialize(_count);
            _id = new short[_count];
            _status = new byte[_count];
            _progress = new int[_count];

            for (int i = 0; i < _count; i++)
			{
                _id[i] = readStream.Serialize(_id[i]);
                _status[i] = (byte)(readStream.Serialize(_status[i]) - 2);
                _progress[i] = readStream.Serialize(_progress[i]);
            }
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(_count);

            for (int i = 0; i < _count; i++)
            {
                writeStream.Serialize(_id[i]);
                writeStream.Serialize((byte)(_status[i] + 2));
                writeStream.Serialize(_progress[i]);
            }
        }
    }
}
