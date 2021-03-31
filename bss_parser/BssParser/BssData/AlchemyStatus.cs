using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("alchemystatus")]
    public class AlchemyStatus : IBssEntry
    {
        public int KeyItem { get; set; }
        public int AlchemyType { get; set; }
        public int KeyItem2 { get; set; }
        public short ResultDropGroup { get; set; }
        public List<MaterialEntry> Materials { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new AlchemyStatus
            {
                KeyItem = br.ReadInt32(),
                AlchemyType = br.ReadInt32(),
                KeyItem2 = br.ReadInt32(),
                ResultDropGroup = br.ReadInt16(),
                Materials = Enumerable.Range(0, br.ReadInt32()).ToList().Select(x => MaterialEntry.Read(br)).ToList()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var table = new List<AlchemyStatus>();

            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(col =>
            {
                table.Add((AlchemyStatus) Read(br));
            });

            table = table.OrderBy(x => x.AlchemyType).ThenBy(x => x.KeyItem).ToList();

            AlchemyConverter.CreateXml(table);
            AlchemyConverter.CreateSql(table);
        }

        public class MaterialEntry
        {
            public int MaterialItem { get; set; }
            public long MaterialCount { get; set; }

            public static MaterialEntry Read(BinaryReader br)
            {
                return new MaterialEntry
                {
                    MaterialItem = br.ReadInt32(),
                    MaterialCount = br.ReadInt64()
                };
            }
        }
    }
}
