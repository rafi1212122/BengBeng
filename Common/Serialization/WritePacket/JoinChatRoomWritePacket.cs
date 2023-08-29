namespace BengBeng.Common.Serialization.WritePacket
{
    public class JoinChatRoomWritePacket : IPacket
    {
        private byte _retcode;
        private int _room_id;

        public JoinChatRoomWritePacket(byte retcode, int room_id)
        {
            _retcode = retcode;
            _room_id = room_id;
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(_retcode);
            writeStream.Serialize(_room_id);
        }
    }
}
