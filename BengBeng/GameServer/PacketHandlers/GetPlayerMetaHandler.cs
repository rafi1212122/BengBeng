using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.ReadPacket;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_FETCH_FRIEND_USERBIN_REQ)]
    internal class GetPlayerMetaHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ReadStream readStream = new(packet.data, (uint)packet.data.Length);
            WriteStream writeStream = new();

            GetPlayerMetaReadPacket readPacket = new();
            readPacket.Serialize(readStream);

            GetPlayerMetaWritePacket writePacket = new(0);
            writePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_FETCH_FRIEND_USERBIN_RSP));
        }
    }
}
