using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.Packet;
using Newtonsoft.Json;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_SYNC_EQUIPED_ITEM_EX)]
    internal class SyncEquipedItemExHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            SyncEquipedItemExPacket syncEquipedItemEx = new();
            ReadStream readStream = new(packet.data, (uint)packet.data.Length);
            syncEquipedItemEx.Serialize(readStream);
            session.c.Debug(JsonConvert.SerializeObject(syncEquipedItemEx));

            WriteStream writeStream = new();
            syncEquipedItemEx.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_SYNC_EQUIPED_ITEM_EX));
        }
    }
}
