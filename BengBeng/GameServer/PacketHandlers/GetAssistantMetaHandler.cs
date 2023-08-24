using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_PICK_ASSISTANT_DATA)]
    internal class GetAssistantMetaHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetAssistantMetaWritePacket writePacket = new();
            WriteStream writeStream = new();
            writePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_PICK_ASSISTANT_DATA));
        }
    }
}
