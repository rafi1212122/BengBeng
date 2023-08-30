using BengBeng.Common.Data;

namespace BengBeng.Common.Serialization.WritePacket
{
    // TODO: Implement more full fields
    public class GetAssistantMetaWritePacket : IPacket
    {
        private const short MAX_LIST_COUNT = 100;
        private byte count;
        private int[] uid;
        private short[] level;
        private byte[] viplevel;
        private int[] lastLoginTime;
        private int[] establishedTime;
        private short[] emblemId;
        private string[] nickName;
        private byte[] eqCount;
        protected PetItem[] petItem;
        private List<int> eqMID;
        private List<byte> eqLevel;
        private List<byte> eqStar;
        protected List<byte> eqIsPresonated;
        protected List<byte> eqIntimacy;
        private List<byte> eqSkillCount;
        private List<List<int>> eqSkillID;
        private List<List<byte>> eqSkillLevel;

        public GetAssistantMetaWritePacket()
        {
            count = 0;
            uid = Array.Empty<int>();
            level = Array.Empty<short>();
            viplevel = Array.Empty<byte>();
            lastLoginTime = Array.Empty<int>();
            establishedTime = Array.Empty<int>();
            emblemId = Array.Empty<short>();
            nickName = Array.Empty<string>();
            eqCount = Array.Empty<byte>();
            petItem = Array.Empty<PetItem>();
            eqMID = new();
            eqLevel = new();
            eqStar = new();
            eqIsPresonated = new();
            eqIntimacy = new();
            eqSkillCount = new();
            eqSkillID = new();
            eqSkillLevel = new();
        }

        public void PopulateDummy()
        {
            count = 1;
            uid = new int[] { 69 };
            level = new short[] { 69 };
            viplevel = new byte[] { 0 };
            lastLoginTime = new int[] { 0 };
            establishedTime = new int[] { 0 };
            emblemId = new short[] { 1 };
            nickName = new string[] { "OG" };
            eqCount = new byte[] { 0 };
            petItem = new PetItem[] { new(7003, 1, 0, 0, 0, 0, 1, 1, 1, 0) };
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(count);

            for (int i = 0; i < count; i++)
            {
                writeStream.Serialize(uid[i]);
                writeStream.Serialize(level[i]);
                writeStream.Serialize(viplevel[i]);
                writeStream.Serialize(lastLoginTime[i]);
                writeStream.Serialize(establishedTime[i]);
                writeStream.Serialize(emblemId[i]);

                DATA_STRING nick = new(nickName[i].Length);
                nick.SetData(System.Text.Encoding.UTF8.GetBytes(nickName[i]), (short)nickName[i].Length);
                writeStream.Serialize(nick);

                writeStream.Serialize(eqCount[i]);

                petItem[i].SerializeToStream(ref writeStream);

                /*writeStream.Serialize(eqMID[i]);
                writeStream.Serialize(eqLevel[i]);
                writeStream.Serialize(eqStar[i]);
                writeStream.Serialize(eqIsPresonated[i]);
                writeStream.Serialize(eqIntimacy[i]);
                writeStream.Serialize(eqSkillCount[i]);

                for (int j = 0; j < eqSkillCount[i]; j++)
                {
                    writeStream.Serialize(eqSkillID[i][j]);
                    writeStream.Serialize(eqSkillLevel[i][j]);
                }*/
            }
        }
    }
}
