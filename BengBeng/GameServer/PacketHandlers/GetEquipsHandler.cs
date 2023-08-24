using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.Packet;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_GET_EQUIPS)]
    internal class GetEquipsHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            WriteStream writeStream = new();
            SyncPackDataPacket syncPackData = new();
            syncPackData.Init();
            syncPackData.Serialize(ref writeStream);

            WriteStream writeStream2 = new();
            SyncPackDataPacket syncPackData2 = new();
            syncPackData2.PopulateDummy();
            syncPackData2.Init();
            syncPackData2.Serialize(ref writeStream2);

            // session.Send(Packet.Create(writeStream, CommandType.CMD_SYNC_PACK_DATA));
            session.Send(Packet.Create(writeStream2, CommandType.CMD_SYNC_PACK_DATA));
        }
    }
}
