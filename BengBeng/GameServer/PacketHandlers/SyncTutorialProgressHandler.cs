using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.ReadPacket;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_SYNC_TUTORIAL_PROGRESS)]
    internal class SyncTutorialProgressHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ReadStream readStream = new(packet.data, (uint)packet.data.Length);

            TutorialProgressReadPacket readPacket = new();
            readPacket.Serialize(readStream);

            PlayerDataWriter playerData = new();
            playerData.WriteTutorialProgress((ushort)readPacket.GetProgress());
            WriteStream writeStream = new();
            QueryPlayerDataWritePacket playerDataWritePacket = new(16, playerData.ToVarDataString());
            playerDataWritePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_GET_PLAER_DATA_RSP));
        }
    }
}
