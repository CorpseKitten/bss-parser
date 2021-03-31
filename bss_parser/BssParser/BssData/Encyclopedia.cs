using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("encyclopedia")]
    public class Encyclopedia : IBssEntry
    {
        public short Key { get; set; }
        public int TypeKey { get; set; }
        public int Type { get; set; }
        public byte Category { get; set; }
        public byte Rareness { get; set; }
        public short BaseSize { get; set; }
        public long FishExpRate { get; set; }
        public int FishExpType { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<int[]> VarySize { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var enc = new Encyclopedia();
            enc.Key = br.ReadInt16();
            enc.TypeKey = br.ReadInt32();
            enc.Type = br.ReadInt32();
            enc.Category = br.ReadByte();
            enc.Rareness = br.ReadByte();
            enc.BaseSize = br.ReadInt16();
            enc.FishExpRate = br.ReadInt64();
            enc.FishExpType = br.ReadInt32();
            enc.Description = br.ReadUnicodeStringBD();
            enc.Image = br.ReadUnicodeStringBD();
            enc.VarySize = Enumerable.Range(0, 5)
                .Select(x => new []
                {
                    br.ReadInt32(),
                    br.ReadInt32(),
                    br.ReadInt32()
                }).ToList();
            return enc;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (Encyclopedia) Read(br)).ToList();
            entries = entries.OrderBy(x => x.Key).ToList();
            EncyclopediaConverter.CreateXml(entries);
            EncyclopediaConverter.CreateSql(entries);
        }
    }
}
