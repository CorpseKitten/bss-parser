using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class SkillsConverter
    {
        public static void CreateSql(List<Skill> Entries)
        {
            /*
            var sb = new List<string> { QueryBuilder.Prepare("Skill_Table_New") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Skill_Table_New");
                qb.Add("ID", sb.Count + 1);
                qb.Add("SkillNo", entry.SkillNo1);
                qb.Add("SkillLevel", entry.SkillLevel1);
                qb.Add("ActionName", entry.ActionName);
                qb.Add("IsPrompt_ForLearning", entry.IsPromptForLearning);
                qb.Add("SkillPoint_ForLearning", entry.SkillPointForLearning);
                qb.Add("PcLevel_ForLearning", entry.PcLevelForLearning);
                qb.Add("NeedMoney_ForLearning", entry.NeedMoneyForLearning);
                qb.Add("NeedItemID_ForLearning", entry.NeedItemIdForLearning);
                qb.Add("NeedItemCount_ForLearning", entry.NeedItemCountForLearning);
                qb.Add("NeedSkillNo0_ForLearning", entry.NeedSkillNo0ForLearning);
                qb.Add("NeedSkillLevelNo0_ForLearning", entry.NeedSkillLevelNo0ForLearning);
                qb.Add("NeedSkillNo1_ForLearning", entry.NeedSkillNo1ForLearning);
                qb.Add("NeedSkillLevelNo1_ForLearning", entry.NeedSkillLevelNo1ForLearning);
                qb.Add("RequireHP", entry.RequireHP);
                qb.Add("RequireMP", entry.RequireMP);
                qb.Add("RequireSP", entry.RequireSP);
                qb.Add("RequireSubResourcePoint", entry.RequireResourcePoint);
                qb.Add("NeedItemID", entry.NeedItemId);
                qb.Add("NeedItemCount", entry.NeedItemCount);
                qb.Add("IsGlobalCycle", 0); // TODO
                qb.Add("ReuseGroup", entry.ReuseGroup);
                qb.Add("ReuseCycle", entry.ReuseCycle);
                qb.Add("isExpiredInOffline", entry.IsExpiredInOffline);
                qb.Add("ApplyNumber", entry.ApplyNumber);
                qb.Add("DoCheckHit", entry.DoCheckHit);
                qb.Add("VariedHit", entry.VariedHit);
                qb.Add("BuffApplyRate", entry.BuffApplyRate);
                qb.Add("StunValue", 0); // TODO
                qb.Add("Buff0", entry.BuffData[0]);
                qb.Add("Buff1", entry.BuffData[1]);
                qb.Add("Buff2", entry.BuffData[2]);
                qb.Add("Buff3", entry.BuffData[3]);
                qb.Add("Buff4", entry.BuffData[4]);
                qb.Add("Buff5", entry.BuffData[5]);
                qb.Add("Buff6", entry.BuffData[6]);
                qb.Add("Buff7", entry.BuffData[7]);
                qb.Add("Buff8", entry.BuffData[8]);
                qb.Add("Buff9", entry.BuffData[9]);
                qb.Add("Desc", entry.Description);
                qb.Add("DDD", entry.OffensiveStats[0].Item1);
                qb.Add("DHIT", entry.OffensiveStats[0].Item2);
                qb.Add("DDV", entry.DefensiveStats[0][0]);
                qb.Add("HDDV", entry.DefensiveStats[0][1]);
                qb.Add("DPV", entry.DefensiveStats[0][2]);
                qb.Add("HDPV", entry.DefensiveStats[0][3]);
                qb.Add("RDD", entry.OffensiveStats[1].Item1);
                qb.Add("RHIT", entry.OffensiveStats[1].Item2);
                qb.Add("RDV", entry.DefensiveStats[1][0]);
                qb.Add("HRDV", entry.DefensiveStats[1][1]);
                qb.Add("RPV", entry.DefensiveStats[1][2]);
                qb.Add("HRPV", entry.DefensiveStats[1][3]);
                qb.Add("MDD", entry.OffensiveStats[2].Item1);
                qb.Add("MHIT", entry.OffensiveStats[2].Item2);
                qb.Add("MDV", entry.DefensiveStats[2][0]);
                qb.Add("HMDV", entry.DefensiveStats[2][1]);
                qb.Add("MPV", entry.DefensiveStats[2][2]);
                qb.Add("HMPV", entry.DefensiveStats[2][3]);
                qb.Add("UsableInCoolTime", entry.UsableInCooltime);
                qb.Add("ContentsGroupKey", 0);
                sb.Add(qb.Combine());
            }*/

            var sb = new List<string> { };
            foreach (var entry in Entries)
            {
                var qb = new UpdateQueryBuilder("Skill_Table_New");
                qb.Add("RequireHP", entry.RequireHP);
                qb.Add("RequireMP", entry.RequireMP);
                qb.Add("RequireSP", entry.RequireSP);
                qb.Add("RequireSubResourcePoint", entry.RequireResourcePoint);
                //qb.Add("NeedItemID", entry.NeedItemId);
                //qb.Add("NeedItemCount", entry.NeedItemCount);
                qb.Add("ReuseGroup", entry.ReuseGroup);
                qb.Add("ReuseCycle", entry.ReuseCycle);
                //qb.Add("isExpiredInOffline", entry.IsExpiredInOffline);
                qb.Add("ApplyNumber", entry.ApplyNumber);
                qb.Add("DoCheckHit", entry.DoCheckHit);
                qb.Add("VariedHit", entry.VariedHit);
                qb.Add("BuffApplyRate", entry.BuffApplyRate);
                for (var i = 0; i < 10; ++i)
                    if (entry.BuffData[i] > 0)
                        qb.Add("Buff" + i, entry.BuffData[i]);
                    else
                        qb.Add("Buff" + i, null);
                qb.Add("DDD", entry.OffensiveStats[0].Item1);
                qb.Add("DHIT", entry.OffensiveStats[0].Item2);
                qb.Add("DDV", entry.DefensiveStats[0][0]);
                qb.Add("HDDV", entry.DefensiveStats[0][1]);
                qb.Add("DPV", entry.DefensiveStats[0][2]);
                qb.Add("HDPV", entry.DefensiveStats[0][3]);
                qb.Add("RDD", entry.OffensiveStats[1].Item1);
                qb.Add("RHIT", entry.OffensiveStats[1].Item2);
                qb.Add("RDV", entry.DefensiveStats[1][0]);
                qb.Add("HRDV", entry.DefensiveStats[1][1]);
                qb.Add("RPV", entry.DefensiveStats[1][2]);
                qb.Add("HRPV", entry.DefensiveStats[1][3]);
                qb.Add("MDD", entry.OffensiveStats[2].Item1);
                qb.Add("MHIT", entry.OffensiveStats[2].Item2);
                qb.Add("MDV", entry.DefensiveStats[2][0]);
                qb.Add("HMDV", entry.DefensiveStats[2][1]);
                qb.Add("MPV", entry.DefensiveStats[2][2]);
                qb.Add("HMPV", entry.DefensiveStats[2][3]);
                //qb.Add("UsableInCoolTime", entry.UsableInCooltime);
                //qb.Add("ContentsGroupKey", 0);
                sb.Add(qb.Combine("SkillNo", entry.SkillNo1.ToString()));
            }

            File.WriteAllLines(Path.Combine("output.sql", "Skill_Table_New.sql"), sb);
        }

        public static void CreateXml(List<Skill> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "Skill_Table_New"),
                    from entry in Entries.Select(x => (Skill)x)
                    select
                    new XElement("data",
                        new XAttribute("SkillNo", entry.SkillNo1),
                        new XAttribute("SkillLevel", entry.SkillLevel1),
                        new XAttribute("ActionName", entry.ActionName),
                        new XAttribute("IsPrompt_ForLearning", entry.IsPromptForLearning),
                        new XAttribute("SkillPoint_ForLearning", entry.SkillPointForLearning),
                        new XAttribute("PcLevel_ForLearning", entry.PcLevelForLearning),
                        new XAttribute("NeedMoney_ForLearning", entry.NeedMoneyForLearning),
                        new XAttribute("NeedItemID_ForLearning", entry.NeedItemIdForLearning),
                        new XAttribute("NeedItemCount_ForLearning", entry.NeedItemCountForLearning),
                        new XAttribute("NeedSkillNo0_ForLearning", entry.NeedSkillNo0ForLearning),
                        new XAttribute("NeedSkillLevelNo0_ForLearning", entry.NeedSkillLevelNo0ForLearning),
                        new XAttribute("NeedSkillNo1_ForLearning", entry.NeedSkillNo1ForLearning),
                        new XAttribute("NeedSkillLevelNo1_ForLearning", entry.NeedSkillLevelNo1ForLearning),
                        new XAttribute("RequireHP", entry.RequireHP),
                        new XAttribute("RequireMP", entry.RequireMP),
                        new XAttribute("RequireSP", entry.RequireSP),
                        new XAttribute("RequireSubResourcePoint", entry.RequireResourcePoint),
                        new XAttribute("NeedItemID", entry.NeedItemId),
                        new XAttribute("NeedItemCount", entry.NeedItemCount),
                        new XAttribute("IsGlobalCycle", 0), // TODO
                        new XAttribute("ReuseGroup", entry.ReuseGroup),
                        new XAttribute("ReuseCycle", entry.ReuseCycle),
                        new XAttribute("isExpiredInOffline", entry.IsExpiredInOffline),
                        new XAttribute("ApplyNumber", entry.ApplyNumber),
                        new XAttribute("DoCheckHit", entry.DoCheckHit),
                        new XAttribute("VariedHit", entry.VariedHit),
                        new XAttribute("BuffApplyRate", entry.BuffApplyRate),
                        new XAttribute("StunValue", 0), // TODO
                        new XAttribute("Buff0", entry.BuffData[0]),
                        new XAttribute("Buff1", entry.BuffData[1]),
                        new XAttribute("Buff2", entry.BuffData[2]),
                        new XAttribute("Buff3", entry.BuffData[3]),
                        new XAttribute("Buff4", entry.BuffData[4]),
                        new XAttribute("Buff5", entry.BuffData[5]),
                        new XAttribute("Buff6", entry.BuffData[6]),
                        new XAttribute("Buff7", entry.BuffData[7]),
                        new XAttribute("Buff8", entry.BuffData[8]),
                        new XAttribute("Buff9", entry.BuffData[9]),
                        new XAttribute("Desc", entry.Description),
                        new XAttribute("DDD", entry.OffensiveStats[0].Item1),
                        new XAttribute("DHIT", entry.OffensiveStats[0].Item2),
                        new XAttribute("DDV", entry.DefensiveStats[0][0]),
                        new XAttribute("HDDV", entry.DefensiveStats[0][1]),
                        new XAttribute("DPV", entry.DefensiveStats[0][2]),
                        new XAttribute("HDPV", entry.DefensiveStats[0][3]),
                        new XAttribute("RDD", entry.OffensiveStats[1].Item1),
                        new XAttribute("RHIT", entry.OffensiveStats[1].Item2),
                        new XAttribute("RDV", entry.DefensiveStats[1][0]),
                        new XAttribute("HRDV", entry.DefensiveStats[1][1]),
                        new XAttribute("RPV", entry.DefensiveStats[1][2]),
                        new XAttribute("HRPV", entry.DefensiveStats[1][3]),
                        new XAttribute("MDD", entry.OffensiveStats[2].Item1),
                        new XAttribute("MHIT", entry.OffensiveStats[2].Item2),
                        new XAttribute("MDV", entry.DefensiveStats[2][0]),
                        new XAttribute("HMDV", entry.DefensiveStats[2][1]),
                        new XAttribute("MPV", entry.DefensiveStats[2][2]),
                        new XAttribute("HMPV", entry.DefensiveStats[2][3]),
                        new XAttribute("UsableInCoolTime", entry.UsableInCooltime),
                        new XAttribute("ContentsGroupKey", 0)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Skill_Table_New.xml"), xws))
                document.Save(xw);
        }
    }
}
