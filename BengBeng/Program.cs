using System.Net.NetworkInformation;
using System.Text;
using BengBeng.GameServer;
using BengBeng.Common;
using BengBeng.Common.Serialization;

namespace BengBeng
{
    internal class Program
    {
        static void Main()
        {
#if DEBUG
            Global.config.VerboseLevel = VerboseLevel.Debug;
            /*Packet packet = new(Global.HexToBuffer("00000048000162167FFFFFFFFFFFFFFF00000000000000000000000000000000000027CC810A00203135383430393232656437623634356562333330366135306264323434613031"));
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