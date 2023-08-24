namespace BengBeng.Common.Serialization.ReadPacket
{
    public class GetConnSvrInfoReadPacket : IBasePacket
    {
        private const int LIMIT_MAX_TOKEN_LEN = 50;
        private DATA_STRING _strUUID;
        private DATA_STRING _strChkSum;

        public GetConnSvrInfoReadPacket()
        {
            _strUUID = new(LIMIT_MAX_TOKEN_LEN);
            _strChkSum = new(LIMIT_MAX_TOKEN_LEN);
        }

        public void Serialize(ReadStream readStream)
        {
            _strUUID = readStream.Serialize(_strUUID);
            _strChkSum = readStream.Serialize(_strChkSum);
        }

        public string GetUUID()
        {
            return _strUUID.GetUtf8();
        }

        public string GetChecksum()
        {
            return _strChkSum.GetUtf8();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            throw new NotImplementedException();
        }
    }
}
