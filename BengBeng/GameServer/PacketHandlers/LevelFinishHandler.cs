using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.Packet;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_LEVEL_FINISH)]
    internal class LevelFinishHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            WriteStream writeStream = new();
            LevelFinishPacket finishPacket = new();
            finishPacket.Serialize(ref writeStream);

            PlayerDataWriter playerData = new();
            playerData.WriteStoryProgress(2);
            WriteStream writeStream2 = new();
            QueryPlayerDataWritePacket playerDataWritePacket = new(15, playerData.ToVarDataString());
            playerDataWritePacket.Serialize(ref writeStream2);

            session.Send(Packet.Create(writeStream, CommandType.CMD_LEVEL_FINISH));
            // session.Send(Packet.Create(writeStream2, CommandType.CMD_GET_PLAER_DATA_RSP));
        }
    }
}
