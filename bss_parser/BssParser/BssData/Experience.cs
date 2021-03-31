using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    public class CharacterStats
    {
        public float HP { get; set; }
        public int MP { get; set; }
        public float HPRegen { get; set; }
        public int MPRegen { get; set; }
        public int CriticalRate { get; set; }
        public int VariedAttackSpeedRate { get; set; }
        public int VariedMoveSpeedRate { get; set; }
        public int VariedCastingSpeedRate { get; set; }
        public int MaxAttackSpeedRate { get; set; }
        public int MaxMoveSpeedRate { get; set; }
        public int MaxCastingSpeedRate { get; set; }
        public long PossessableWeight { get; set; }
        public float ProductionHit { get; set; }
        public int Suspension { get; set; }
        public List<int> MaxLuck { get; set; }

        public static CharacterStats Read(BinaryReader br)
        {
            var ret = new CharacterStats();
            ret.HP = br.ReadSingle();
            ret.MP = br.ReadInt32();
            br.ReadInt16();
            br.ReadBytes(0x14);
            ret.HPRegen = br.ReadSingle();
            ret.MPRegen = br.ReadInt32();
            ret.CriticalRate = br.ReadInt32();
            ret.VariedAttackSpeedRate = br.ReadInt32();
            ret.VariedMoveSpeedRate = br.ReadInt32();
            ret.VariedCastingSpeedRate = br.ReadInt32();
            ret.MaxAttackSpeedRate = br.ReadInt32();
            ret.MaxMoveSpeedRate = br.ReadInt32();
            ret.MaxCastingSpeedRate = br.ReadInt32();
            ret.PossessableWeight = br.ReadInt64();
            ret.ProductionHit = br.ReadSingle();
            br.ReadInt32(); // suspension
            br.ReadBytes(4 * 3);
            br.ReadBytes(4 * 3);
            ret.MaxLuck = Enumerable.Range(0, 3).Select(x => br.ReadInt32()).ToList();
            return ret;
        }
    }

    public class AttackerStats
    {
        public class OffseniveStat
        {
            public string Damage { get; set; }
            public float HIT { get; set; }

            public static OffseniveStat Read(BinaryReader br)
            {
                var ret = new OffseniveStat();
                ret.Damage = br.ReadUnicodeStringBD();
                ret.HIT = br.ReadSingle();
                return ret;
            }
        }

        public class DefensiveStat
        {
            public float DV { get; set; }
            public float HDV { get; set; }
            public float PV { get; set; }
            public float HPV { get; set; }

            public static DefensiveStat Read(BinaryReader br)
            {
                var ret = new DefensiveStat();
                ret.DV = br.ReadSingle();
                ret.HDV = br.ReadSingle();
                ret.PV = br.ReadSingle();
                ret.HPV = br.ReadSingle();
                return ret;
            }
        }

        public List<OffseniveStat> OffensiveStats { get; set; }
        public List<DefensiveStat> DefensiveStats { get; set; }
        public int CriticalRate { get; set; }
        public static AttackerStats Read(BinaryReader br)
        {
            var ret = new AttackerStats();
            ret.OffensiveStats = Enumerable.Range(0, 3).Select(x => OffseniveStat.Read(br)).ToList();
            ret.DefensiveStats = Enumerable.Range(0, 3).Select(x => DefensiveStat.Read(br)).ToList();
            ret.CriticalRate = br.ReadInt32();
            return ret;
        }
    }

    public class VariedStats
    {
        public string unk1 { get; set; }
        public string unk2 { get; set; }
        public List<string> unk3 { get; set; }
        public List<string> unk4 { get; set; }
        public List<string> unk5 { get; set; }
        public static VariedStats Read(BinaryReader br)
        {
            var ret = new VariedStats();
            ret.unk1 = br.ReadUnicodeStringBD();
            ret.unk2 = br.ReadUnicodeStringBD();
            ret.unk3 = Enumerable.Range(0, 3).Select(x => br.ReadUnicodeStringBD()).ToList();
            ret.unk4 = Enumerable.Range(0, 3).Select(x => br.ReadUnicodeStringBD()).ToList();
            ret.unk5 = Enumerable.Range(0, 3).Select(x => br.ReadUnicodeStringBD()).ToList();
            return ret;
        }
    }

    [BssTable("experience")]
    public class Experience : IBssEntry
    {
        public Dictionary<int, List<ExperienceEntry>> Levels { get; set; }

        public class ExperienceEntry
        {
            public byte ClassType { get; set; }
            public int Level { get; set; }
            public long RequireEXP { get; set; }
            public long RequireEXPLimit { get; set; }
            public long ContentsRequireEXPLimit { get; set; }
            public CharacterStats CharacterStats { get; set; }
            public AttackerStats AttackerStats { get; set; }
            public VariedStats VariedStats { get; set; }
        }

        public IBssEntry Read(BinaryReader br)
        {
            return null;
        }

        public void ReadTable(BinaryReader br)
        {
            // Dictionary<int, List<ExperienceEntry>>
            var entries = new Experience {Levels = new Dictionary<int, List<ExperienceEntry>>()};
            for (var i = 0; i < 32; ++i)
            {
                var levelCount = br.ReadInt32();
                var levels = new List<ExperienceEntry>();
                for (var k = 0; k < levelCount; ++k)
                {
                    var level = new ExperienceEntry();
                    level.ClassType = br.ReadByte();
                    br.ReadByte();
                    br.ReadInt16();
                    level.Level = br.ReadInt32();
                    level.RequireEXP = br.ReadInt64();
                    level.RequireEXPLimit = br.ReadInt64();
                    level.ContentsRequireEXPLimit = br.ReadInt64();
                    level.CharacterStats = CharacterStats.Read(br);
                    level.AttackerStats = AttackerStats.Read(br);
                    level.VariedStats = VariedStats.Read(br);
                    levels.Add(level);
                }
                entries.Levels.Add(i, levels);
            }
            ExperienceConverter.CreateSql(entries);
        }
    }
}
