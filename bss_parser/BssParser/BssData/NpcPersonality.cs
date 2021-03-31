using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("npcpersonality")]
    public class NpcPersonality : IBssEntry
    {
        public ushort Npc { get; set; }
        public List<ThemeEntry> Themes { get; set; }
        public float MinDv { get; set; }
        public float MaxDv { get; set; }
        public float MinPv { get; set; }
        public float MaxPv { get; set; }
        public ushort ZodiacSignIndexKey { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var pers = new NpcPersonality();
            pers.Npc = br.ReadUInt16();
            pers.Themes = Enumerable.Range(0, 3)
                .Select(x => new ThemeEntry
                {
                    Theme = br.ReadUInt16(),
                    NeedCount = br.ReadUInt16()
                }).ToList();
            br.ReadInt16();
            pers.MinDv = br.ReadSingle();
            pers.MaxDv = br.ReadSingle();
            pers.MinPv = br.ReadSingle();
            pers.MaxPv = br.ReadSingle();
            pers.ZodiacSignIndexKey = br.ReadUInt16();
            return pers;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (NpcPersonality)Read(br)).ToList();
            entries = entries.OrderBy(x => x.Npc).ToList();
            PersonalityConverter.CreateXml(entries);
            PersonalityConverter.CreateSql(entries);
        }

        public class ThemeEntry
        {
            public ushort Theme { get; set; }
            public ushort NeedCount { get; set; }
        }
    }
}
