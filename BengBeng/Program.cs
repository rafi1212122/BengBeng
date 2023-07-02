using System.Net.NetworkInformation;
using BengBeng.GameServer;
using Common;

namespace BengBeng
{
    internal class Program
    {
        static void Main()
        {
#if DEBUG
            Global.config.VerboseLevel = VerboseLevel.Debug;
            /*Packet packet = new(Global.HexToBuffer("000000480001461600000000000000010000000000000000000000000000000000002E84246100206230623636316639653035646339323863633033316265396335326432366566"));
            Global.c.Debug(packet.IsValid().ToString());*/
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