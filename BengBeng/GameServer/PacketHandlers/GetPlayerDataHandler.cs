using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.Packet;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_GET_PLAER_DATA_REQ)]
    internal class GetPlayerDataHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            PlayerDataWriter playerData = new();
            playerData.WritePlayerLevel(80);
            playerData.WritePlayerExp(0);
            playerData.WriteHcoin(0);
            playerData.WriteScoin(0);
            playerData.WriteStamina(20);
            playerData.WriteFriendPoints(0);
            playerData.WriteLastStaminaTime(6);
            playerData.WriteMaxFriends(20);
            playerData.WriteEquips(1004, 1001, 1002, 0, 0, 0, 0, 1003, 0);
            playerData.WriteAdPoints(0);
            playerData.WriteLoginCount(0);
            playerData.WriteAppstoreRating(0);
            /*playerData.WriteStoryProgress(0);
            playerData.WriteTutorialProgress(0);*/
            playerData.WriteCurElp(0);
            playerData.WriteTipsStat(0, 0, 0, 0);
            playerData.WriteHasRealName(false);
            playerData.WriteStSaved(0);
            playerData.WriteAssistLeft(0);
            playerData.WriteEmblemId(0);
            WriteStream writeStream = new();
            QueryPlayerDataWritePacket playerDataWritePacket = new(100, playerData.ToVarDataString());
            playerDataWritePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_GET_PLAER_DATA_RSP));
        }
    }
}
