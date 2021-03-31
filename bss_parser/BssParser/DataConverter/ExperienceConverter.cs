using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ExperienceConverter
    {
        public static void CreateSql(Experience Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("Pc_Table") };
            foreach (var entry in Entries.Levels)
                foreach (var level in entry.Value)
                {
                    var qb = new QueryBuilder("Pc_Table");
                    qb.Add("ID", sb.Count);
                    qb.Add("Class", level.ClassType);
                    qb.Add("Level", level.Level);
                    qb.Add("RequireEXP", level.RequireEXP);
                    qb.Add("RequireExpLimit", level.RequireEXPLimit);
                    qb.Add("ContentsRequireExpLimit", level.ContentsRequireEXPLimit);
                    qb.Add("HP", level.CharacterStats.HP);
                    qb.Add("MP", level.CharacterStats.MP);
                    qb.Add("HPRegen", level.CharacterStats.HPRegen);
                    qb.Add("MPRegen", level.CharacterStats.MPRegen);
                    qb.Add("ProductionHit", level.CharacterStats.ProductionHit);
                    qb.Add("possessableWeight", level.CharacterStats.PossessableWeight);
                    qb.Add("CriticalRate", level.CharacterStats.CriticalRate);
                    qb.Add("VariedAttackSpeedRate", level.CharacterStats.VariedAttackSpeedRate);
                    qb.Add("VariedMoveSpeedRate", level.CharacterStats.VariedMoveSpeedRate);
                    qb.Add("VariedCastingSpeedRate", level.CharacterStats.VariedCastingSpeedRate);
                    qb.Add("DDD" , level.AttackerStats.OffensiveStats[0].Damage);
                    qb.Add("DHIT", level.AttackerStats.OffensiveStats[0].HIT);
                    qb.Add("DDV" , level.AttackerStats.DefensiveStats[0].DV);
                    qb.Add("HDDV", level.AttackerStats.DefensiveStats[0].HDV);
                    qb.Add("DPV" , level.AttackerStats.DefensiveStats[0].PV);
                    qb.Add("HDPV", level.AttackerStats.DefensiveStats[0].HPV);
                    qb.Add("RDD" , level.AttackerStats.OffensiveStats[1].Damage);
                    qb.Add("RHIT", level.AttackerStats.OffensiveStats[1].HIT);
                    qb.Add("RDV" , level.AttackerStats.DefensiveStats[1].DV);
                    qb.Add("HRDV", level.AttackerStats.DefensiveStats[1].HDV);
                    qb.Add("RPV" , level.AttackerStats.DefensiveStats[1].PV);
                    qb.Add("HRPV", level.AttackerStats.DefensiveStats[1].HPV);
                    qb.Add("MDD" , level.AttackerStats.OffensiveStats[2].Damage);
                    qb.Add("MHIT", level.AttackerStats.OffensiveStats[2].HIT);
                    qb.Add("MDV" , level.AttackerStats.DefensiveStats[2].DV);
                    qb.Add("HMDV", level.AttackerStats.DefensiveStats[2].HDV);
                    qb.Add("MPV" , level.AttackerStats.DefensiveStats[2].PV);
                    qb.Add("HMPV", level.AttackerStats.DefensiveStats[2].HPV);
                    qb.Add("VariedMaxHP", level.VariedStats.unk1);
                    qb.Add("VariedMaxMP", level.VariedStats.unk2);
                    qb.Add("VariedDirectHit", level.VariedStats.unk3[0]);
                    qb.Add("VariedDirectDV", level.VariedStats.unk3[1]);
                    qb.Add("VariedDirectPV", level.VariedStats.unk3[2]);
                    qb.Add("VariedRangeHit", level.VariedStats.unk4[0]);
                    qb.Add("VariedRangeDV", level.VariedStats.unk4[1]);
                    qb.Add("VariedRangePV", level.VariedStats.unk4[2]);
                    qb.Add("VariedMagicalHit", level.VariedStats.unk5[0]);
                    qb.Add("VariedMagicalDV", level.VariedStats.unk5[1]);
                    qb.Add("VariedMagicalPV", level.VariedStats.unk5[2]);
                    qb.Add("MaxCriticalRate", level.AttackerStats.CriticalRate);
                    qb.Add("MaxAttackSpeed", level.CharacterStats.MaxAttackSpeedRate);
                    qb.Add("MaxMoveSpeed", level.CharacterStats.MaxMoveSpeedRate);
                    qb.Add("MaxCastingSpeed", level.CharacterStats.MaxCastingSpeedRate);
                    qb.Add("MaxFishingLuck", level.CharacterStats.MaxLuck[0]);
                    qb.Add("MaxCollectionLuck", level.CharacterStats.MaxLuck[1]);
                    qb.Add("MaxDropItemLuck", level.CharacterStats.MaxLuck[2]);

                    // fix below..
                    qb.Add("StunGauge", level.Level > 0 ? 100 + level.Level * 2 : 0);
                    qb.Add("Suspension", level.Level > 0 ? 100 + level.Level * 2 : 0);
                    qb.Add("SubResourcePoint", 30); // static
                    qb.Add("RegenSubResourcePoint", 0);
                    qb.Add("ActiveRegenSubResourcePoint", 0);
                    qb.Add("DDamageResistance", 0);
                    qb.Add("RDamageResistance", 0);
                    qb.Add("MDamageResistance", 0);
                    sb.Add(qb.Combine());
                }

            File.WriteAllLines(Path.Combine("output.sql", "Pc_Table.sql"), sb);
        }
    }
}
