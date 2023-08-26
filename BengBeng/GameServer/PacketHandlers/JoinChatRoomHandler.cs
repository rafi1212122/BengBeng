using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.ReadPacket;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_JOIN_CHATROOM_REQ)]
    internal class JoinChatRoomHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ReadStream readStream = new(packet.data, (uint)packet.data.Length);

            JoinChatRoomReadPacket readPacket = new();
            readPacket.Serialize(readStream);

            WriteStream writeStream = new WriteStream();
            JoinChatRoomWritePacket writePacket = new(0, readPacket.GetRoomId());
            writePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_JOIN_CHATROOM_RSP));
        }
    }
}
