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
            /*Packet packet = new(Global.HexToBuffer("000000f70001ea6000000000043a97840000000000000000000000000000dc349e8bcec0f8dd0001420702002037393434623363316536656138633232376265663736663837393535393038397f0000001073616d73756e6720534d2d473938383064a2450e00203664343432343735653836356461373137363536323261396437663863646266000000000000093730313331323336300028663432383331343966646435356230633166353135393831323835633365353264306634643833330000000e002f416e64726f6964204f532039202f204150492d32382028505133412e3139303630352e3030332f3337393332363529"));
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