﻿using BengBeng.Common.Serialization;
using BengBeng.Common.Serialization.ReadPacket;
using BengBeng.Common.Serialization.WritePacket;

namespace BengBeng.GameServer.PacketHandlers
{
    [PacketCmdId(CommandType.CMD_SYNC_STORY_PROGRESS)]
    internal class SyncStoryProgressHandler : IPacketHandler
    {
        public void Handle(Session session, Packet packet)
        {
            StoryProgressReadPacket storyProgressSync = new();
            ReadStream readStream = new(packet.data, (uint)packet.data.Length);
            storyProgressSync.Serialize(readStream);

            PlayerDataWriter playerData = new();
            playerData.WriteStoryProgress((ushort)storyProgressSync.GetProgress());
            WriteStream writeStream = new();
            QueryPlayerDataWritePacket playerDataWritePacket = new(15, playerData.ToVarDataString());
            playerDataWritePacket.Serialize(ref writeStream);

            session.Send(Packet.Create(writeStream, CommandType.CMD_GET_PLAER_DATA_RSP));
        }
    }
}
