using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_EGG_MACHINE_TXT_REQ)]
    internal class EggMachineTxtHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            QueryEggMachineTxtWritePacket writePacket = new();
            WriteStream writeStream = new();
            writePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_EGG_MACHINE_TXT_RSP));
        }
    }
}
