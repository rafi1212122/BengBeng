using Common;
using Common.Serialization;
using Common.Serialization.Packet;
using BengBeng.GameServer;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_LOGIN)]
    internal class PlayerLoginHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            PlayerLoginPacket loginPacket = new();
            loginPacket.Serialize(new(packet.data, (uint)packet.data.Length));

            WriteStream writeStream = new();
            loginPacket.Serialize(ref writeStream);
            // session.Send(new Packet(Global.HexToBuffer("0000008300014616FFFFFFFFFFFFFFFF000000000000000000000000000000000000356F1FE7002062306236363166396530356463393238636330333162653963353264323665667F4616001073616D73756E6720534D2D47393838300000000000206230623636316639653035646339323863633033316265396335326432366566")));
            session.Send(Packet.Create(writeStream, CommandType.CMD_LOGIN));
        }
    }
}
