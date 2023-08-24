using System.Net.Sockets;
using System.Net;
using BengBeng.Common;
using BengBeng.Common.Utils;

namespace BengBeng.GameServer
{
    public class Server
    {
        public static readonly Logger c = new("TCP", ConsoleColor.Blue);
        public readonly Dictionary<string, Session> Sessions = new();
        private static Server? Instance;

        public static Server GetInstance()
        {
            return Instance ??= new Server();
        }

        public Server()
        {
            Task.Run(() => Start((int)Global.config.GameServer.Port));
            Thread.Sleep(100);
            Task.Run(() => Start((int)Global.config.GameServer.SecondaryPort));
        }

        public void Start(int port)
        {
            TcpListener Listener = new(IPAddress.Parse("0.0.0.0"), port);

            while (true)
            {
                try
                {
                    Listener.Start();
                    c.Log($"TCP server started on port {port}");

                    while (true)
                    {
                        TcpClient Client = Listener.AcceptTcpClient();
                        string Id = Client.Client.RemoteEndPoint!.ToString()!;

                        c.Warn($"{Id} connected");
                        Sessions.Add(Id, new Session(Id, Client));
                        LogClients();
                    }
                }
                catch (Exception ex)
                {
                    c.Error("TCP server error: " + ex.Message);
                    Thread.Sleep(3000);
                }
            }
        }

        public void LogClients()
        {
            c.Log($"Connected clients: {Sessions.Count}");
        }
    }
}
