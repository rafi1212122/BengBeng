using BengBeng.Common.Serialization;

namespace BengBeng.Common.Data
{
    public class PetItem
    {
        private const int PET_SKILL_NUM = 3;
        private int id;
        private int level;
        private int exp;
        private int curSyn;
        private int synLevel;
        private int ultraSkillType;
        private int ultraSkillLevel;
        private int autoSkillLevel;
        private int negativeSkillLevel;
        private int feedCountLeft;

        public PetItem(int id, int level, int exp, int curSyn, int synLevel, int ultraSkillType, int ultraSkillLevel, int autoSkillLevel, int negativeSkillLevel, int feedCountLeft)
        {
            this.id = id;
            this.level = level;
            this.exp = exp;
            this.curSyn = curSyn;
            this.synLevel = synLevel;
            this.ultraSkillType = ultraSkillType;
            this.ultraSkillLevel = ultraSkillLevel;
            this.autoSkillLevel = autoSkillLevel;
            this.negativeSkillLevel = negativeSkillLevel;
            this.feedCountLeft = feedCountLeft;
        }

        public int GetId()
        {
            return id;
        }

        public int GetLevel()
        {
            return level;
        }

        public int GetExp()
        {
            return exp;
        }

        public void SetCurSyn(int value)
        {
            curSyn = value;
        }

        public int GetCurSyn()
        {
            return curSyn;
        }

        public int GetSynLevel()
        {
            return synLevel;
        }

        public PetUltraSkillType GetUltraSkillType()
		{
			return (PetUltraSkillType)ultraSkillType;
        }

        public int GetUltraSkillLevel()
        {
            return ultraSkillLevel;
        }

        public int GetAutoSkillLevel()
        {
            return autoSkillLevel;
        }

        public int GetNegativeSkillLevel()
        {
            return negativeSkillLevel;
        }

        public int GetFeedCountLeft()
        {
            return feedCountLeft;
        }

        public static PetItem CreatePetItemByReadStream(ReadStream readStream)
        {
            short u = 0;
            short num = 0;
            int num2 = 0;
            short num3 = 0;
            short num4 = 0;
            byte b = 0;
            byte b2 = 0;
            byte b3 = 0;
            byte b4 = 0;
            short num5 = 0;
            u = readStream.Serialize(u);
            num = readStream.Serialize(num);
            num2 = readStream.Serialize(num2);
            num3 = readStream.Serialize(num3);
            num4 = readStream.Serialize(num4);
            b = readStream.Serialize(b);
            b2 = readStream.Serialize(b2);
            b3 = readStream.Serialize(b3);
            b4 = readStream.Serialize(b4);
            num5 = readStream.Serialize(num5);

            return new PetItem((int)u, (int)num, num2, (int)num3, (int)num4, (int)b, (int)b2, (int)b3, (int)b4, (int)num5);
        }

        public void SerializeToStream(ref WriteStream writeStream)
        {
            writeStream.Serialize((short)id);
            writeStream.Serialize((short)level);
            writeStream.Serialize(exp);
            writeStream.Serialize((short)curSyn);
            writeStream.Serialize((short)synLevel);
            writeStream.Serialize((byte)ultraSkillType);
            writeStream.Serialize((byte)ultraSkillLevel);
            writeStream.Serialize((byte)autoSkillLevel);
            writeStream.Serialize((byte)negativeSkillLevel);
            writeStream.Serialize((short)feedCountLeft);
        }
	}

    public enum PetUltraSkillType
    {
        ULTRA,
        HIDDEN
    }
}
