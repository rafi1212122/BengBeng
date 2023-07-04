using Common.Serialization;
using Common.Serialization.Packet;
using Newtonsoft.Json;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_LEVEL_BEGIN)]
    internal class LevelBeginHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            ReadStream readStream = new(packet.data, (uint)packet.data.Length);
            WriteStream writeStream = new();
            LevelBeginPacket levelBeginPacket = new();
            levelBeginPacket.Serialize(readStream);
            levelBeginPacket.id = 1;
            levelBeginPacket.attackPlayerId = 4001;
            levelBeginPacket.levelGrade = 3;
            levelBeginPacket.Serialize(ref writeStream);
            session.c.Debug(JsonConvert.SerializeObject(levelBeginPacket));

            session.Send(Packet.Create(writeStream, CommandType.CMD_LEVEL_BEGIN));
        }
    }
}
