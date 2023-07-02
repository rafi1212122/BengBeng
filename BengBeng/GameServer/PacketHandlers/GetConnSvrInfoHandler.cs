using BengBeng.GameServer;
using Common.Serialization;
using Common.Serialization.ReadPacket;
using Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_GET_CONNSVRINFO)]
    internal class GetConnSvrInfoHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            GetConnSvrInfoReadPacket getConnSvrInfo = new();
            getConnSvrInfo.Serialize(new ReadStream(packet.data, (uint)packet.data.Length));
            // session.c.Debug($"{getConnSvrInfo.GetUUID()} - {getConnSvrInfo.GetChecksum()}");

            // Setting the game version to the client, idk why they do it this way tbh
            SPWritePacket sPPacket = new();
            WriteStream sPWriteStream = new();
            sPPacket.Serialize(ref sPWriteStream);
            session.Send(Packet.Create(sPWriteStream, CommandType.CMD_SP_STATUS));

            GetConnSvrInfoWritePacket writePacket = new();
            WriteStream writeStream = new();
            writePacket.Serialize(ref writeStream);
            session.Send(Packet.Create(writeStream, CommandType.CMD_GET_CONNSVRINFO));
        }
    }
}
