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
            playerData.WriteLoginCount(0);
            playerData.WriteMaxFriends(50);
            playerData.WriteFriendPoints(0);
            playerData.WriteStoryProgress(0);
            // playerData.WriteTutorialProgress(0);
            playerData.WriteEquips(new int[] { 0, 0, 0, 0, 0, 0, 0, 0 });
            playerData.WriteNick("KOK");
            playerData.WriteCms("BengBeng");
            playerData.WritePlayerLevel(1);
            playerData.WritePlayerExp(10);
            playerData.WriteStamina(200);
            playerData.WriteHcoin(69);
            playerData.WriteScoin(500);
            WriteStream writeStream = new();
            QueryPlayerDataWritePacket playerDataWritePacket = new(100, playerData.ToVarDataString());
            playerDataWritePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_GET_PLAER_DATA_RSP));
        }
    }
}
