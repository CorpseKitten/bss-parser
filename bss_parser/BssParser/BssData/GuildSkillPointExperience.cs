using System;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("guildskillexperience")]
    public class GuildSkillPointExperience : IBssEntry
    {
        public ushort Index { get; set; }
        public ushort SkillLevel { get; set; }
        public uint RequireEXP { get; set; }
        public uint RequireSkillEXPLimit { get; set; }
        public byte AquiredPoint { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new GuildSkillPointExperience
            {
                Index = br.ReadUInt16(),
                SkillLevel = br.ReadUInt16(),
                RequireEXP = br.ReadUInt32(),
                RequireSkillEXPLimit = br.ReadUInt32(),
                AquiredPoint = br.ReadByte()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (GuildSkillPointExperience) Read(br)).ToList();
            entries = entries.OrderBy(x => x.Index).ToList();

            GuildSkillPointExperienceConverter.CreateXml(entries);
            GuildSkillPointExperienceConverter.CreateSql(entries);
        }
    }
}
