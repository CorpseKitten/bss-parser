using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("alchemystonechange")]
    public class AlchemyStoneChange : IBssEntry
    {
        public int ItemKey { get; set; }
        public int ItemKey2 { get; set; }
        public ushort MainGroupKey { get; set; }
        public int NeedItemKey { get; set; }
        public long NeedItemCount { get; set; }
        public int BreakRate { get; set; }
        public int DownRate { get; set; }
        public ushort DownGroup { get; set; }
        public string Condition { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new AlchemyStoneChange
            {
                ItemKey = br.ReadInt32(),
                ItemKey2 = br.ReadInt32(),
                MainGroupKey = br.ReadUInt16(),
                NeedItemKey = br.ReadInt32(),
                NeedItemCount = br.ReadInt64(),
                BreakRate = br.ReadInt32(),
                DownRate = br.ReadInt32(),
                DownGroup = br.ReadUInt16(),
                Condition = Encoding.Unicode.GetString(br.ReadBytes((int) br.ReadInt64() * 2))
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var table = new List<AlchemyStoneChange>();
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x =>
            {
                table.Add((AlchemyStoneChange) Read(br));
            });

            table = table.OrderBy(x => x.ItemKey).ToList();
            AlchemyStoneChangeConverter.CreateXml(table);
            AlchemyStoneChangeConverter.CreateSql(table);
        }
    }
}
