using System;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("skillexperience")]
    public class SkillExperience : IBssEntry
    {
        public ushort Index { get; set; }
        public ushort AquiredPoint { get; set; }
        public uint RequireExp { get; set; }
        public uint RequireSkillExpLimit { get; set; }
        public uint QuestRequireExpLimit { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new SkillExperience
            {
                Index = br.ReadUInt16(),
                AquiredPoint = br.ReadUInt16(),
                RequireExp = br.ReadUInt32(),
                RequireSkillExpLimit = br.ReadUInt32(),
                QuestRequireExpLimit = br.ReadUInt32()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (SkillExperience) Read(br)).ToList();
            SkillExperienceConverter.CreateSql(entries);
        }
    }
}
