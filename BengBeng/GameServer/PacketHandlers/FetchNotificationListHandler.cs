using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.WritePacket;
using BengBeng.GameServer;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_FETCH_NOTIFI_LIST_REQ)]
    internal class FetchNotificationListHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            WriteStream writeStream = new();
            QueryNotificationListWritePacket notificationPacket = new();
            notificationPacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_FETCH_NOTIFI_LIST_RSP));
        }
    }
}
