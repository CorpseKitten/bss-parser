using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("itemmaingroup")]
    public class ItemMainGroup : IBssEntry
    {
        public ushort ItemMainGroupKey { get; set; }
        public byte DoSelectOnlyOne { get; set; }
        public long PlantCraftResultCount { get; set; }
        public int RefreshStartHour { get; set; }
        public long RefreshInterval { get; set; }
        public List<ItemSubGroup> SubGroups { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new ItemMainGroup
            {
                ItemMainGroupKey = br.ReadUInt16(),
                DoSelectOnlyOne = br.ReadByte(),
                PlantCraftResultCount = br.ReadInt64(),
                RefreshStartHour = br.ReadInt32(),
                RefreshInterval = br.ReadInt64(),
                SubGroups = Enumerable.Range(0, br.ReadInt32()).ToList().Select(x => ItemSubGroup.Read(br)).ToList()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var table = new List<ItemMainGroup>();
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(col =>
            {
                table.Add((ItemMainGroup) Read(br));
            });
            table = table.OrderBy(x => x.ItemMainGroupKey).ToList();
            ItemMainGroupConverter.CreateXml(table);
            ItemMainGroupConverter.CreateSql(table);
        }

        public class ItemSubGroup
        {
            public int SelectRate { get; set; }
            public string Condition { get; set; }
            public ushort ItemSubGroupKey { get; set; }

            public static ItemSubGroup Read(BinaryReader br)
            {
                return new ItemSubGroup
                {
                    SelectRate = br.ReadInt32(),
                    Condition = Encoding.Unicode.GetString(br.ReadBytes((int) br.ReadInt64() * 2)),
                    ItemSubGroupKey = br.ReadUInt16()
                };
            }
        }
    }
}
