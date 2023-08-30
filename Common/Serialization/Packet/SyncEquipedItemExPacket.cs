namespace BengBeng.Common.Serialization.Packet
{
    public class SyncEquipedItemExPacket : IPacket
    {
        public const byte LIST_COUNT_CHANGE = 10;
        public const byte LIST_COUNT_EQUIPS = 10;
        public byte cur_index;
        public byte count_change;
        public byte[] index;
        public byte[] count_equip;
        public int[,] uid;

        public SyncEquipedItemExPacket()
        {
            cur_index = 0;
            count_change = 0;
            index = new byte[10];
            count_equip = new byte[10];
            uid = new int[10, 10];
        }

        public void Serialize(ReadStream readStream)
        {
            cur_index = readStream.Serialize(cur_index);
            count_change = readStream.Serialize(count_change);

            for (int i = 0; i < count_change; i++)
            {
                index[i] = readStream.Serialize(index[i]);
                count_equip[i] = readStream.Serialize(count_equip[i]);

                for (int j = 0; j < count_equip[i]; j++)
                {
                    uid[i, j] = readStream.Serialize(uid[i, j]);
                }
            }
        }

        public void Serialize(ref WriteStream writeStream)
        {
            writeStream.Serialize(cur_index);
            writeStream.Serialize(count_change);

            for (int i = 0; i < count_change; i++)
			{
                writeStream.Serialize(index[i]);
                writeStream.Serialize(count_equip[i]);
                for (int j = 0; j < count_equip[i]; j++)
                {
                    writeStream.Serialize(uid[i, j]);
                }
            }
        }
    }
}
