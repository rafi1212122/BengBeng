using BengBeng.Common.Serialization;
using BengBeng.Common.Utils;
using System.Reflection;
using System.Buffers.Binary;
using BengBeng.Common.Serialization.Lib;

namespace BengBeng.GameServer
{
    public class Packet
    {
        public static readonly Logger c = new("Packet", ConsoleColor.Magenta);
        public byte[] buf;
        public int length;
        public short cmdId;
        public short ver;
        public long uid;
        public long sid;
        public short gwId;
        public short nsId;
        public short workType;
        public long reference;
        public byte[] data;

        // 0000006A 0006 4616 FFFFFFFFFFFFFFFF 0000000000000000 0000 0000 0000 DC2A4E00B386EBC2 0020666236313837373961623130643632303638383831643963366366366161633500203861383832643035393439653633393632316634383931316534376231623437
        public Packet(byte[] bytes)
        {
            ReadStream readStream = new(bytes, (uint)bytes.Length);
            buf = bytes;
            length = readStream.Serialize(length);
            cmdId = readStream.Serialize(cmdId);
            ver = readStream.Serialize(ver);
            uid = readStream.Serialize(uid);
            sid = readStream.Serialize(sid);
            gwId = readStream.Serialize(gwId);
            nsId = readStream.Serialize(nsId);
            workType = readStream.Serialize(workType);
            reference = readStream.Serialize(reference);

            data = bytes[38..];
            string? PacketName = Enum.GetName(typeof(CommandType), cmdId);

            if (PacketName == null)
            {
                c.Warn($"CmdId {cmdId} not recognized!");
            }
        }

        public static Packet Create(WriteStream writeStream, CommandType command)
        {
            byte[] streamBytes = writeStream.GetOccupied();
            byte[] packet = new byte[streamBytes.Length + 38];
            short cmdId = (short)command;
            Array.Copy(streamBytes, 0, packet, 38, streamBytes.Length);
            BinaryPrimitives.WriteInt32BigEndian(packet, packet.Length);
            BinaryPrimitives.WriteInt16BigEndian(packet.AsSpan()[4..], cmdId);
            BinaryPrimitives.WriteUInt16BigEndian(packet.AsSpan()[6..], 25110);
            BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[8..], 69420);
            // BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[16..], 1);
            // BinaryPrimitives.WriteInt16BigEndian(packet.AsSpan()[28..], 0x0400);
            if (cmdId == 6 || cmdId == 17)
                BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[30..], Convert.ToInt64(Crc32.Compute(packet)));
            else
                BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[30..], 0L);
            // BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[30..], (long)(((ulong)(DateTime.Now.Ticks / 10000L) / 1000UL) << 32 | Convert.ToUInt64(Crc32.Compute(packet))));
            return new Packet(packet);
        }

        public static Packet Create(CommandType command)
        {
            byte[] packet = new byte[38];
            short cmdId = (short)command;
            BinaryPrimitives.WriteInt32BigEndian(packet, packet.Length);
            BinaryPrimitives.WriteInt16BigEndian(packet.AsSpan()[4..], cmdId);
            BinaryPrimitives.WriteUInt16BigEndian(packet.AsSpan()[6..], 25110);
            BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[8..], 69420);
            // BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[16..], 1);
            // BinaryPrimitives.WriteInt16BigEndian(packet.AsSpan()[28..], 0x0400);
            if (cmdId == 6 || cmdId == 17)
                BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[30..], Convert.ToInt64(Crc32.Compute(packet)));
            else
                BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[30..], 0L);
            // BinaryPrimitives.WriteInt64BigEndian(packet.AsSpan()[30..], (long)(((ulong)(DateTime.Now.Ticks / 10000L) / 1000UL) << 32 | Convert.ToUInt64(Crc32.Compute(packet))));
            return new Packet(packet);
        }

        public bool IsValid()
        {
            byte[] compareHash = new byte[4];
            Array.Copy(buf, 34, compareHash, 0, 4);
            Console.WriteLine(GetHash());

            return GetHash() == BinaryPrimitives.ReverseEndianness(BitConverter.ToUInt32(compareHash));
        }

        public uint GetHash()
        {
            byte[] tmpBuf = new byte[buf.Length];
            Array.Copy(buf, tmpBuf, buf.Length);
            byte[] hash = new byte[8];
            Array.Copy(hash, 0, tmpBuf, 30, 8);

            return (cmdId == 6 || cmdId == 17) ? Crc32.Compute(tmpBuf) : 0;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PacketCmdId : Attribute
    {
        public CommandType Id { get; }

        public PacketCmdId(CommandType id)
        {
            Id = id;
        }
    }

    public interface IPacketHandler
    {
        public void Handle(Session session, Packet packet);
    }

    public static class PacketFactory
    {
        public static readonly Dictionary<CommandType, IPacketHandler> Handlers = new();
        static readonly Logger c = new("Factory", ConsoleColor.Yellow);

        public static void LoadPacketHandlers()
        {
            c.Log("Loading Packet Handlers...");

            IEnumerable<Type> classes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                        select t;

            foreach ((Type t, PacketCmdId attr) in from Type? t in classes.ToList()
                                                   let attrs = (Attribute[])t.GetCustomAttributes(typeof(PacketCmdId), false)
                                                   where attrs.Length > 0
                                                   let attr = (PacketCmdId)attrs[0]
                                                   where !Handlers.ContainsKey(attr.Id)
                                                   select (t, attr))
            {
                Handlers.Add(attr.Id, (IPacketHandler)Activator.CreateInstance(t)!);
#if DEBUG
                c.Log($"Loaded PacketHandler {t.Name} for Packet Type {attr.Id}");
#endif
            }

            c.Log("Finished Loading Packet Handlers");
        }

        public static IPacketHandler? GetPacketHandler(CommandType cmdId)
        {
            Handlers.TryGetValue(cmdId, out IPacketHandler? handler);
            return handler;
        }
    }
}
