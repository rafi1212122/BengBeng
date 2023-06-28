using System.Net.NetworkInformation;
using BengBeng.GameServer;
using Common;
using Common.Serialization;
using Common.Serialization.ReadPacket;
using Common.Serialization.WritePacket;
using Newtonsoft.Json;
using PemukulPaku.GameServer;

namespace BengBeng
{
    internal class Program
    {
        static void Main()
        {
#if DEBUG
            Global.config.VerboseLevel = VerboseLevel.Debug;
#endif
            Global.c.Log("Starting...");

            if (Global.config.GameServer.Host == "127.0.0.1")
                Global.config.GameServer.Host = NetworkInterface.GetAllNetworkInterfaces().Where(i => i.NetworkInterfaceType != NetworkInterfaceType.Loopback && i.OperationalStatus == OperationalStatus.Up).First().GetIPProperties().UnicastAddresses.Where(a => a.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First().Address.ToString();

            PacketFactory.LoadPacketHandlers();
            new Thread(HttpServer.Program.Main).Start();
            _ = Server.GetInstance();
            _ = Console.ReadLine();
        }
    }
}