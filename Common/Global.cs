using Common.Utils;
using MongoDB.Driver;
using Config.Net;

namespace Common
{
    public static class Global
    {
        public static readonly IConfig config = new ConfigurationBuilder<IConfig>().UseJsonFile("config.json").Build();
        public static readonly Logger c = new("Global");
        public static readonly MongoClient MongoClient = new(config.DatabaseUri);
        public static readonly IMongoDatabase db = MongoClient.GetDatabase("BengBeng");

        public static long GetUnixInSeconds() => ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
        public static uint GetRandomSeed() => (uint)(GetUnixInSeconds() * new Random().Next(1, 10) / 100);

        public static byte[] HexToBuffer(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        internal static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

    }

    public interface IConfig
    {
        [Option(DefaultValue = VerboseLevel.Normal)]
        VerboseLevel VerboseLevel { get; set; }

        [Option(DefaultValue = "mongodb://127.0.0.1:27017/BengBeng")]
        string DatabaseUri { get; set; }

        [Option]
        IGameServer GameServer { get; set; }

        [Option]
        IHttp Http { get; set; }

        public interface IGameServer
        {
            [Option(DefaultValue = "127.0.0.1")]
            public string Host { get; set; }

            [Option(DefaultValue = (uint)(15004))]
            public uint Port { get; set; }
            
            [Option(DefaultValue = (uint)(26001))]
            public uint SecondaryPort { get; set; }
        }

        public interface IHttp
        {

            [Option(DefaultValue = (uint)(80))]
            public uint HttpPort { get; set; }

            [Option(DefaultValue = (uint)(443))]
            public uint HttpsPort { get; set; }
        }
    }

    public enum VerboseLevel
    {
        Silent = 0,
        Normal = 1,
        Debug = 2
    }
}
