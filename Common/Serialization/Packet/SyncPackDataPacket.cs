﻿namespace BengBeng.Common.Serialization.Packet
{
    public class SyncPackDataPacket : IBasePacket
    {
        private const short MAX_LIST_COUNT = 400;
        private const int MAX_SKILL_PER_ITEM = 5;
        private byte[] type;
        private int[] id;
        private int[] tmpId;
        private short[] level;
        private int[] exp;
        private byte[] protection;
        private byte[] star;
        private byte[] skillCount;
        private int[,] skillIdMap;
        private int[,] skillLevelMap;
        private short maxSize;
        private byte opType;
        private short count;

        public SyncPackDataPacket()
        {
            opType = 0;
            maxSize = MAX_LIST_COUNT;
            count = 0;
            type = Array.Empty<byte>();
            id = Array.Empty<int>();
            tmpId = Array.Empty<int>();
            level = Array.Empty<short>();
            exp = Array.Empty<int>();
            protection = Array.Empty<byte>();
            star = Array.Empty<byte>();
            skillCount = Array.Empty<byte>();
            skillIdMap = new int[0, MAX_SKILL_PER_ITEM];
            skillLevelMap = new int[0, MAX_SKILL_PER_ITEM];
        }

        public void Init()
        {
            opType = 1;
        }

        public void PopulateDummy()
        {
            opType = 3;
            count = 0;
            type = new byte[] { 0 };
            id = new int[] { 0 };
            tmpId = new int[] { 1 };
            level = new short[] { 0 };
            exp = new int[] { 0 };
            protection = new byte[] { 0 };
            star = new byte[] { 1 };
            skillCount = new byte[] { MAX_SKILL_PER_ITEM };
            skillIdMap = new int[1, MAX_SKILL_PER_ITEM];
            skillLevelMap = new int[1, MAX_SKILL_PER_ITEM];
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(opType);
            writeStream.Serialize(maxSize);
            writeStream.Serialize(count);
            for (int i = 0; i < count; i++)
            {
                writeStream.Serialize(type[i]);
                writeStream.Serialize(id[i]);
                writeStream.Serialize(tmpId[i]);
                writeStream.Serialize(level[i]);
                writeStream.Serialize(exp[i]);
                writeStream.Serialize(protection[i]);
                writeStream.Serialize(star[i]);
                writeStream.Serialize(skillCount[i]);

                for (int j = 0; j < skillCount[i]; j++)
                {
                    writeStream.Serialize(skillIdMap[i, j]);
                    writeStream.Serialize(skillLevelMap[i, j]);
                }
            }
        }

        public void Serialize(ReadStream readStream)
        {
            opType = readStream.Serialize(opType);
            maxSize = readStream.Serialize(maxSize);
            count = readStream.Serialize(count, MAX_LIST_COUNT);
            type = new byte[count];
            id = new int[count];
            tmpId = new int[count];
            level = new short[count];
            exp = new int[count];
            protection = new byte[count];
            star = new byte[count];
            skillCount = new byte[count];
            skillIdMap = new int[count, MAX_SKILL_PER_ITEM];
            skillLevelMap = new int[count, MAX_SKILL_PER_ITEM];
            for (int i = 0; i < count; i++)
            {
                type[i] = readStream.Serialize(type[i]);
                id[i] = readStream.Serialize(id[i]);
                tmpId[i] = readStream.Serialize(tmpId[i]);
                level[i] = readStream.Serialize(level[i]);
                exp[i] = readStream.Serialize(exp[i]);
                protection[i] = readStream.Serialize(protection[i]);
                star[i] = readStream.Serialize(star[i]);
                skillCount[i] = readStream.Serialize(skillCount[i]);
                int num = 0;
                byte b = 0;
                for (int j = 0; j < skillCount[i]; j++)
                {
                    num = readStream.Serialize(num);
                    b = readStream.Serialize(b);
                    skillIdMap[i, j] = num;
                    skillLevelMap[i, j] = b;
                }
            }
        }

        public enum OP_TYPE
        {
            SPOP_INIT = 1,
            SPOP_ADD = 2,
            SPOP_UPDATE = 3,
            SPOP_DEL = 4
        }
    }
}
