using System.Buffers.Binary;
using System.Net.Sockets;
using BengBeng.GameServer;
using BengBeng.Common;
using BengBeng.Common.Serialization;
using BengBeng.Common.Utils;

namespace BengBeng.GameServer
{
    public class Session
    {
        public readonly string Id;
        public readonly TcpClient Client;
        public readonly Logger c;
        public long LastKeepAlive;

        public Session(string id, TcpClient client)
        {
            Id = id;
            Client = client;
            c = new Logger(Id);
            LastKeepAlive = Global.GetUnixInSeconds();
            Task.Run(ClientLoop);
        }

        private async void ClientLoop()
        {
            NetworkStream stream = Client.GetStream();

            byte[] msg = new byte[1 << 16];

            while (Client.Connected)
            {
                try
                {
                    Array.Clear(msg, 0, msg.Length);
                    int len = stream.Read(msg, 0, msg.Length);

                    if (len > 0)
                    {
                        List<Packet> packets = new();

                        for (int offset = 0; offset < msg.Length;)
                        {
                            byte[] segment = msg[offset..];
                            int segmentLength = BinaryPrimitives.ReadInt32BigEndian(segment);

                            if (segmentLength < 1)
                                break;

                            packets.Add(new Packet(segment[..segmentLength]));
                            offset += len;
                        }

                        c.Debug($"Found {packets.Count} packet(s)");
                        foreach (Packet packet in packets)
                        {
                            if (!packet.IsValid())
                                c.Warn("Invalid CRC!");
                            ProcessPacket(packet, true);
                        }
                    }
                }
                catch (Exception)
                {
                    break;
                }
                await Task.Delay(10);
                if (Global.GetUnixInSeconds() > LastKeepAlive + 90)
                    break;
            }

            DisconnectProtocol();
        }

        public void ProcessPacket(Packet packet, bool log = false)
        {
            string PacketName = Enum.GetName(typeof(CommandType), packet.cmdId)!;
            c.Debug(BitConverter.ToString(packet.buf).Replace("-", ""));
            try
            {
                CommandType cmdId = (CommandType)Enum.ToObject(typeof(CommandType), packet.cmdId);
                IPacketHandler? handler = PacketFactory.GetPacketHandler(cmdId);

                if (handler == null)
                {
                    c.Warn($"{PacketName} not handled!");
                    /*if (PacketName.EndsWith("REQ"))
                        Send(Packet.Create((CommandType)Enum.ToObject(typeof(CommandType), packet.cmdId + 1)));
                    else
                        Send(Packet.Create(cmdId));*/
                    return;
                }

                if (log)
                    c.Log(PacketName);

                handler.Handle(this, packet);
            }
            catch (Exception ex)
            {
                if ((int)Global.config.VerboseLevel > 0)
                {
                    c.Error(ex.Message);
                }
            }
        }

        public void Send(params Packet[] packets)
        {
            foreach (Packet packet in packets)
            {
                string PacketName = Enum.GetName(typeof(CommandType), packet.cmdId)!;

                try
                {
                    c.Debug(BitConverter.ToString(packet.buf).Replace("-", ""));
                    Client.GetStream().Write(packet.buf, 0, packet.buf.Length);
                    c.Log(PacketName);
                }
                catch (ObjectDisposedException)
                {
                    DisconnectProtocol();
                }
                catch (Exception ex)
                {
                    c.Error($"Failed to send {PacketName}:" + ex.Message);
                }
            }
        }

        public void DisconnectProtocol()
        {
            if (Server.GetInstance().Sessions.GetValueOrDefault(Id) is null)
                return;

            Server.GetInstance().Sessions.Remove(Id);
            Server.GetInstance().LogClients();
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
