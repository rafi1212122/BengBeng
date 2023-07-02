using Common.Serialization;
using Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_GET_PLAER_DATA_REQ)]
    internal class GetPlayerDataHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            PlayerDataWriter playerData = new();
            playerData.WriteStoryProgress(0);
            // playerData.WriteTutorialProgress(0);
            playerData.WritePlayerLevel(1);
            playerData.WritePlayerExp(10);
            playerData.WriteStamina(10);
            playerData.WriteHcoin(69);
            playerData.WriteScoin(500);
            WriteStream writeStream = new();
            QueryPlayerDataWritePacket playerDataWritePacket = new(100, playerData.ToVarDataString());
            playerDataWritePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_GET_PLAER_DATA_RSP));
        }
    }
}
