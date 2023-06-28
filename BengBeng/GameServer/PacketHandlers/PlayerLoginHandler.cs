using Common;
using Common.Serialization;
using Common.Serialization.Packet;
using PemukulPaku.GameServer;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_LOGIN)]
    internal class PlayerLoginHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            PlayerLoginPacket loginPacket = new();
            loginPacket.Serialize(new(packet.data, (uint)packet.data.Length));

            WriteStream writeStream = new();
            loginPacket.Serialize(ref writeStream);
            session.Send(Packet.FromStream(writeStream, CommandType.CMD_LOGIN));
        }
    }
}
