using BengBeng.Common;
using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.Packet;
using BengBeng.GameServer;

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
            
            session.Send(Packet.Create(writeStream, CommandType.CMD_LOGIN));

            WriteStream writeStream2 = new();
            SyncPackDataPacket syncPackData2 = new();
            syncPackData2.PopulateDummy();
            syncPackData2.Init();
            syncPackData2.Serialize(ref writeStream2);

            session.Send(Packet.Create(writeStream2, CommandType.CMD_SYNC_PACK_DATA));
        }
    }
}
