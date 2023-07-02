using Common.Serialization;
using Common.Serialization.Packet;
using BengBeng.GameServer;
using Common;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_KEEP_LIVE)]
    internal class KeepLiveHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            session.LastKeepAlive = Global.GetUnixInSeconds();
            WriteStream writeStream = new();
            KeepLivePacket keepLivePacket = new();
            keepLivePacket.Serialize(ref writeStream);
            session.Send(Packet.Create(writeStream, CommandType.CMD_KEEP_LIVE));
        }
    }
}
