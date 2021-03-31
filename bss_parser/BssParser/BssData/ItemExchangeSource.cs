using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("itemexchangesource")]
    public class ItemExchangeSource : IBssEntry
    {
        public ushort MapKeyIndex { get; set; }
        public int Index { get; set; }
        public byte NeedObjectSkillNo { get; set; }
        public byte ExchangeType { get; set; }
        public Tuple<int, long> CraftingZoneCheck { get; set; }
        public Tuple<int, long> CraftingPublic { get; set; }
        public Tuple<int, long> CraftingPrivate { get; set; }
        public List<Tuple<int, long>> ItemList { get; set; }
        public Tuple<int, long> MainItem { get; set; }
        public byte ProductDataType { get; set; }
        public long CraftingTime { get; set; }
        public ushort ItemDropID { get; set; }
        public ushort NpcWorkerDropIDForLuck { get; set; }
        public ushort CraftCharacterKey { get; set; }
        public byte WorkAniType { get; set; }
        public List<ushort> ItemExchangeRepeatGroup { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var src = new ItemExchangeSource();
            src.MapKeyIndex = br.ReadUInt16();
            src.Index = br.ReadInt32();
            src.NeedObjectSkillNo = br.ReadByte();
            src.ExchangeType = br.ReadByte();
            src.CraftingZoneCheck = ReadItemMacro(br);
            src.CraftingPublic = ReadItemMacro(br);
            src.CraftingPrivate = ReadItemMacro(br);
            src.ItemList = Enumerable.Range(0, br.ReadInt32()).Select(x => ReadItemMacro(br)).ToList();
            src.MainItem = ReadItemMacro(br);
            src.ProductDataType = br.ReadByte();
            br.ReadInt32();
            br.ReadInt64();
            src.CraftingTime = br.ReadInt64();
            br.ReadUInt16();
            src.ItemDropID = br.ReadUInt16();
            src.NpcWorkerDropIDForLuck = br.ReadUInt16();
            src.CraftCharacterKey = br.ReadUInt16();
            br.ReadUnicodeStringBD();
            br.ReadUnicodeStringBD();
            br.ReadUnicodeStringBD();
            br.ReadBytes(sizeof(int) * br.ReadInt32());
            src.WorkAniType = br.ReadByte();
            br.ReadInt32();
            src.ItemExchangeRepeatGroup = Enumerable.Range(0, br.ReadInt32()).Select(x => br.ReadUInt16()).ToList();
            br.ReadInt32();
            br.ReadInt32();
            return src;
        }

        private Tuple<int, long> ReadItemMacro(BinaryReader br)
        {
            return new Tuple<int, long>(br.ReadInt32(), br.ReadInt64());
        }

        public void ReadTable(BinaryReader br)
        {
            var table = new List<ItemExchangeSource>();
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x =>
            {
                table.Add((ItemExchangeSource)Read(br));
            });

            ItemExchangeSourceConverter.CreateSql(table);
            ItemExchangeSourceConverter.CreateXml(table);
        }
    }
}
