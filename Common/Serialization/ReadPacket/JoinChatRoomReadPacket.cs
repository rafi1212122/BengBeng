namespace BengBeng.Common.Serialization.ReadPacket
{
    public class JoinChatRoomReadPacket : IBasePacket
    {
        private int _room_id;

        public void Serialize(ReadStream readStream)
        {
            _room_id = readStream.Serialize(_room_id);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            throw new NotImplementedException();
        }

        public int GetRoomId() { return _room_id; }
    }
}
