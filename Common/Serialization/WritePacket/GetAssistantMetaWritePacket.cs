using BengBeng.Common.Data;

namespace BengBeng.Common.Serialization.WritePacket
{
    // TODO: Implement more full fields
    public class GetAssistantMetaWritePacket : IBasePacket
    {
        private const short MAX_LIST_COUNT = 100;
        private byte count;
        private new int[] uids;
		private new short[] levels;
		private new byte[] viplevels;
		private new int[] lastLogintimes;
		private new int[] establishedTimes;
		private new short[] emblemIds;
		private new string[] nickNames;
		private new byte[] eqCounts;
		protected new PetItem[] petItems;
		private new List<int> eqMids;
		private new List<byte> eqLevels;
		private new List<byte> eqStars;
		private new List<byte> eqSkillCounts;
		private new List<List<int>> eqSkillIds;
		private new List<List<byte>> eqSkillLevels;

        public GetAssistantMetaWritePacket()
        {
            count = 0;
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(count, MAX_LIST_COUNT);
        }
    }
}
