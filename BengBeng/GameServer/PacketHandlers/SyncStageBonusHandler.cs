using Common.Serialization;
using Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_SYNC_STAGE_AWARD_REQ)]
    internal class SyncStageBonusHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SyncStageBonusWritePacket writePacket = new();
            WriteStream writeStream = new();
            writePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_SYNC_STAGE_AWARD_RSP));
        }
    }
}
