namespace Common.Serialization.WritePacket
{
    public class QueryPlayerDataWritePacket : IBasePacket
    {
        private const int MAX_NICK_NAME_LENGTH = 25;
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
        // EQUIPED_ITEMS = byte[] (first byte is array length and for each item is uint of ids)
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
        // PDT_TIPS_STAT = byte[] (first byte is array length and for each item is long)
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
            PDT_BEGIN,
            EXP_LEVEL,
            EXP_2_NEXTLEVEL,
            H_COINS,
            S_COINS,
            STAMINA,
            FRIEND_POINTS,
            NICK_NAME,
            LAST_STAMINA_TIME,
            MAX_FRIENDS,
            EQUIPED_ITEMS,
            CMS_NAME,
            AD_POINTS,
            SUM_LOGIN_COUNT,
            APPSTORE_RATING_FLAG,
            STORY_PROGRESS,
            TUTORIAL_PROGRESS,
            CUR_ELP,
            MAX_ELP,
            PDT_PHONE,
            WEEK_MAX_ELP,
            PDT_TIPS_STAT,
            PDT_HAS_REAL_NAME,
            PDT_ST_SAVED,
            PDT_BYN_ASSIS_TIME_LEFT,
            PDT_CUR_EQ_INDEX,
            PDT_EMBLEM_ID,
            PDT_CUR_INSIDE_ELP,
            PDT_MAX_INSIDE_ELP,
            PDT_BH_DUST,
            PDT_PLAYER_BACK,
            PDT_END
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
    }
}
