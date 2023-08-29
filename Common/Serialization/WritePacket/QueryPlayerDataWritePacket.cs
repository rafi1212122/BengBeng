using System.Text;

namespace BengBeng.Common.Serialization.WritePacket
{
    public class QueryPlayerDataWritePacket : IPacket
    {
        internal const int MAX_NICK_NAME_LENGTH = 25;
        private byte requestType;
        private VAR_DATA_STRING playerData;

        public QueryPlayerDataWritePacket(byte _requestType, VAR_DATA_STRING _playerData)
        {
            requestType = _requestType;
            playerData = _playerData;
        }

        public void Serialize(ReadStream readStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(requestType);
            writeStream.Serialize(playerData);
        }

        // EXP_LEVEL = ushort
        // EXP_2_NEXTLEVEL = uint
        // H_COINS = uint
        // S_COINS = uint
        // STAMINA = ushort
        // FRIEND_POINTS = uint
        // NICK_NAME = byte[25] (utf8 encoded)
        // LAST_STAMINA_TIME = uint
        // MAX_FRIENDS = ushort
        // EQUIPED_ITEMS = byte, uint[] (first byte is array length and for each item is uint of ids)
        // CMS_NAME = byte[45] (utf8 encoded)
        // AD_POINTS = ushort
        // SUM_LOGIN_COUNT = byte
        // APPSTORE_RATING_FLAG = byte
        // STORY_PROGRESS = ushort
        // TUTORIAL_PROGRESS = ushort
        // CUR_ELP = uint
        // MAX_ELP = uint
        // PDT_PHONE = long
        // WEEK_MAX_ELP = uint
        // PDT_TIPS_STAT = byte, long[] (first byte is array length and for each item is long)
        // PDT_HAS_REAL_NAME = byte (bool)
        // PDT_ST_SAVED = ushort
        // PDT_BYN_ASSIS_TIME_LEFT = byte
        // PDT_CUR_EQ_INDEX = byte
        // PDT_EMBLEM_ID = ushort
        // PDT_CUR_INSIDE_ELP = uint
        // PDT_MAX_INSIDE_ELP = uint
        // PDT_BH_DUST = uint
        // PDT_PLAYER_BACK = byte
        public enum DATA_TYPE
        {
            PDT_BEGIN = 0,
            EXP_LEVEL = 1,
            EXP_2_NEXTLEVEL = 2,
            H_COINS = 3,
            S_COINS = 4,
            STAMINA = 5,
            FRIEND_POINTS = 6,
            NICK_NAME = 7,
            LAST_STAMINA_TIME = 8,
            MAX_FRIENDS = 9,
            EQUIPED_ITEMS = 10,
            CMS_NAME = 11,
            AD_POINTS = 12,
            SUM_LOGIN_COUNT = 13,
            APPSTORE_RATING_FLAG = 14,
            STORY_PROGRESS = 15,
            TUTORIAL_PROGRESS = 16,
            CUR_ELP = 17,
            MAX_ELP = 18,
            PDT_PHONE = 19,
            WEEK_MAX_ELP = 20,
            PDT_TIPS_STAT = 21,
            PDT_HAS_REAL_NAME = 22,
            PDT_ST_SAVED = 23,
            PDT_BYN_ASSIS_TIME_LEFT = 24,
            PDT_CUR_EQ_INDEX = 25,
            PDT_EMBLEM_ID = 26,
            PDT_CUR_INSIDE_ELP = 27,
            PDT_MAX_INSIDE_ELP = 28,
            PDT_BH_DUST = 29,
            PDT_PLAYER_BACK = 30,
            PDT_PINK_COIN = 31,
            PDT_WHITE_COIN = 32,
            PDT_END = 33
        }
    }

    // TODO: Full implementation
    public class PlayerDataWriter : WriteStreamLE
    {
        private void WriteType(QueryPlayerDataWritePacket.DATA_TYPE type)
        {
            Serialize((byte)type);
        }

        public VAR_DATA_STRING ToVarDataString()
        {
            VAR_DATA_STRING data = new();
            data.SetData(GetOccupied(), (short)GetOccupied().Length);
            return data;
        }

        public void WriteHasRealName(bool realName)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.PDT_HAS_REAL_NAME);
            Serialize(realName ? (byte)1 : (byte)0);
        }

        public void WriteAssistLeft(byte left)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.PDT_BYN_ASSIS_TIME_LEFT);
            Serialize(left);
        }

        public void WriteLoginCount(byte days)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.SUM_LOGIN_COUNT);
            Serialize(days);
        }

        public void WriteAppstoreRating(byte rating)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.APPSTORE_RATING_FLAG);
            Serialize(rating);
        }

        public void WriteStSaved(ushort st)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.PDT_ST_SAVED);
            Serialize(st);
        }

        public void WriteEmblemId(ushort num)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.PDT_EMBLEM_ID);
            Serialize(num);
        }

        public void WriteMaxFriends(ushort num)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.MAX_FRIENDS);
            Serialize(num);
        }

        public void WriteAdPoints(ushort point)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.AD_POINTS);
            Serialize(point);
        }

        public void WriteStoryProgress(ushort progress)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.STORY_PROGRESS);
            Serialize(progress);
        }

        public void WriteTutorialProgress(ushort tutorial)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.TUTORIAL_PROGRESS);
            Serialize(tutorial);
        }

        public void WritePlayerLevel(ushort level)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.EXP_LEVEL);
            Serialize(level);
        }

        public void WriteStamina(ushort stamina)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.STAMINA);
            Serialize(stamina);
        }

        public void WritePlayerExp(uint exp)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.EXP_2_NEXTLEVEL);
            Serialize(exp);
        }

        public void WriteLastStaminaTime(uint time)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.LAST_STAMINA_TIME);
            Serialize(time);
        }

        public void WriteFriendPoints(uint fp)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.FRIEND_POINTS);
            Serialize(fp);
        }

        public void WriteCurElp(uint elp)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.CUR_ELP);
            Serialize(elp);
        }

        public void WriteHcoin(uint hcoin)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.H_COINS);
            Serialize(hcoin);
        }

        public void WriteScoin(uint scoin)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.S_COINS);
            Serialize(scoin);
        }

        public void WriteNick(string nick)
        {
            UTF8Encoding utf8Encoding = new();
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.NICK_NAME);
            byte[] nickBytes = utf8Encoding.GetBytes(nick);
            byte[] setBytes = new byte[QueryPlayerDataWritePacket.MAX_NICK_NAME_LENGTH];
            Array.Copy(nickBytes, setBytes, nickBytes.Length);
            Serialize(setBytes);
        }

        public void WriteCms(string cmsName)
        {
            UTF8Encoding utf8Encoding = new();
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.CMS_NAME);
            byte[] nickBytes = utf8Encoding.GetBytes(cmsName);
            byte[] setBytes = new byte[45];
            Array.Copy(nickBytes, setBytes, nickBytes.Length);
            Serialize(setBytes);
        }
        
        public void WriteEquips(params uint[] items)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.EQUIPED_ITEMS);
            Serialize((byte)items.Length);
            foreach (uint item in items)
            {
                Serialize(item);
            }
        }
        
        public void WriteTipsStat(params long[] stats)
        {
            WriteType(QueryPlayerDataWritePacket.DATA_TYPE.PDT_TIPS_STAT);
            Serialize((byte)stats.Length);
            foreach (long item in stats)
            {
                Serialize(item);
            }
        }
    }
}
