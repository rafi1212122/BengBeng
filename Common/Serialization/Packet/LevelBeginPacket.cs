namespace BengBeng.Common.Serialization.Packet
{
    public class LevelBeginPacket : IBasePacket
    {
        private const byte MAX_LIST_COUNT = 10;
        public byte[] rareDropType;
        public short[] rareDropId;
        public byte[] rareDropLevel;
        public byte retCode;
        public short id;
        public byte useSavedStamina;
        public byte equipCount;
        public int[] equipTmpIds;
        public byte eventId;
        public byte count;
        public byte progress;
        public int randomSeed;
        public byte starItemCount;
        public byte encounterCount;
        public object[] encounterList;
        public byte propCount;
        public short[] propList;
        public short insidePassCount;
        public byte giftItemDropCount;
        public short[] giftItemID;
        public byte[] giftItemCount;
        public byte levelGrade;
        public int attackPlayerId;
        public int defendPlayerId;

        public LevelBeginPacket()
        {
            rareDropType = Array.Empty<byte>();
            rareDropId = Array.Empty<short>();
            rareDropLevel = Array.Empty<byte>();
            retCode = 0;
            id = 0;
            useSavedStamina = 0;
            equipCount = 0;
            equipTmpIds= Array.Empty<int>();
            eventId = 0;
            count = 0;
            progress = 1;
            randomSeed = (int)Global.GetRandomSeed();
            starItemCount = 0;
            encounterCount = 0;
            encounterList = Array.Empty<object>();
            propCount = 0;
            propList = Array.Empty<short>();
            insidePassCount = 0;
            giftItemDropCount = 0;
            giftItemID = Array.Empty<short>();
            giftItemCount = Array.Empty<byte>();
            levelGrade = 0;
            attackPlayerId = 0;
            defendPlayerId = 0;
        }

        public void Serialize(ReadStream readStream)
        {
            attackPlayerId = readStream.Serialize(attackPlayerId);
            defendPlayerId = readStream.Serialize(defendPlayerId);
            id = readStream.Serialize(id);
            useSavedStamina = readStream.Serialize(useSavedStamina);
            equipCount = readStream.Serialize(equipCount);
            equipTmpIds = new int[equipCount];
            for (int i = 0; i < equipCount; i++)
            {
                equipTmpIds[i] = readStream.Serialize(equipTmpIds[i]);
            }
            levelGrade = readStream.Serialize(levelGrade);
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(retCode);
            writeStream.Serialize(id);
            writeStream.Serialize(eventId);
            writeStream.Serialize(progress);
            writeStream.Serialize(randomSeed);
            writeStream.Serialize(useSavedStamina);
            writeStream.Serialize(count, MAX_LIST_COUNT);
            for (int i = 0; i < count; i++)
            {
                writeStream.Serialize(rareDropType[i]);
                writeStream.Serialize(rareDropId[i]);
                writeStream.Serialize(rareDropLevel[i]);
            }
            writeStream.Serialize(starItemCount);
            writeStream.Serialize(encounterCount);
            for (int i = 0; i < encounterCount; i++)
            {
                // TODO: Implement encounter data
            }
            writeStream.Serialize(propCount);
            for (int i = 0; i < propCount; i++)
            {
                writeStream.Serialize(propList[i]);
            }
            writeStream.Serialize(insidePassCount);
            writeStream.Serialize(giftItemDropCount);
            for (int i = 0; i < giftItemDropCount; i++)
            {
                writeStream.Serialize(giftItemID[i]);
                writeStream.Serialize(giftItemCount[i]);
            }
            writeStream.Serialize(levelGrade);
        }
    }
}
