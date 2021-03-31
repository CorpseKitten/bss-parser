using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("itemsubgroup")]
    public class ItemSubGroup : IBssEntry
    {
        public ushort ItemSubGroupKey { get; set; }
        public string UnkString { get; set; }
        public int TotalSelectRate { get; set; }
        public List<SubGroup> SubGroups { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var xz = new ItemSubGroup
            {
                ItemSubGroupKey = br.ReadUInt16(),
                UnkString = br.ReadAsciiStringBD(),
                TotalSelectRate = br.ReadInt32(),
                SubGroups = Enumerable.Range(0, br.ReadInt32()).ToList().Select(x => SubGroup.Read(br)).ToList()
            };
            return xz;
        }

        public void ReadTable(BinaryReader br)
        {
            var table = new List<ItemSubGroup>();
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x =>
            {
                table.Add((ItemSubGroup) Read(br));
            });
            ItemSubGroupConverter.CreateSql(table);
            ItemSubGroupConverter.CreateSql_Filler(table);
        }

        public class SubGroup
        {
            public ushort ItemKey { get; set; }
            public ushort EnchantLevel { get; set; }
            public uint SelectRate { get; set; }
            public string SelectCondition { get; set; }
            public byte DoPetAddDrop { get; set; }
            public long MinCount { get; set; }
            public long MaxCount { get; set; }
            public int IntimacyVariation { get; set; }
            public int ApplyRandomPrice { get; set; }
            public long TradeManagerMinPrice { get; set; }
            public long TradeManagerMaxPrice { get; set; }
            public int TradeManagerBargainRate { get; set; }

            public static SubGroup Read(BinaryReader br)
            {
                var sb = new SubGroup();
                sb.ItemKey = br.ReadUInt16();
                sb.EnchantLevel = br.ReadUInt16();
                sb.SelectRate = br.ReadUInt32();
                sb.SelectCondition = br.ReadUnicodeStringBD();
                sb.DoPetAddDrop = br.ReadByte();
                sb.MinCount = br.ReadInt64();
                sb.MaxCount = br.ReadInt64();
                sb.IntimacyVariation = br.ReadInt32();
                br.ReadInt64();
                br.ReadByte();
                br.ReadInt32();
                sb.ApplyRandomPrice = br.ReadInt32();
                br.ReadByte();
                br.ReadByte();
                br.ReadByte();
                br.ReadByte();
                br.ReadInt64();
                sb.TradeManagerMinPrice = br.ReadInt64();
                sb.TradeManagerMaxPrice = br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                br.ReadInt64();
                sb.TradeManagerBargainRate = br.ReadInt32();

                return sb;
            }
        }
    }
}
