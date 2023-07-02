using System;
using System.Text;

namespace Common.Serialization.Packet
{
    public class PlayerLoginPacket : IBasePacket
    {
        private const int STR_MAX_LEN = 50;
        public DATA_STRING strUUID;
        public DATA_STRING strDeviceModel;
        public byte deviceType;
        public short gameVersion;
        public int tokenTimestamp;
        public DATA_STRING strDeviceUUID;
        private DATA_STRING sendUUID;

        public PlayerLoginPacket()
        {
            strUUID = new(STR_MAX_LEN);
            strDeviceModel = new(STR_MAX_LEN);
            deviceType = 0;
            gameVersion = 0;
            tokenTimestamp = 0;
            strDeviceUUID = new(STR_MAX_LEN);
            sendUUID = new(STR_MAX_LEN);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            sendUUID = strDeviceUUID;
            writeStream.Serialize(sendUUID);
        }

        public void Serialize(ReadStream readStream)
        {
            strUUID = readStream.Serialize(strUUID);
            deviceType = readStream.Serialize(deviceType);
            gameVersion = readStream.Serialize(gameVersion);
            strDeviceModel = readStream.Serialize(strDeviceModel);
            tokenTimestamp = readStream.Serialize(tokenTimestamp);
            strDeviceUUID = readStream.Serialize(strDeviceUUID);
        }
    }
}
