using System.Collections.Generic;
using System.IO;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class SkillExperienceConverter
    {
        public static void CreateSql(List<SkillExperience> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("SkillPointEXP_Table_New") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("SkillPointEXP_Table_New");
                qb.Add("ID", sb.Count);
                qb.Add("Index", entry.Index);
                qb.Add("RequireEXP", entry.RequireExp);
                qb.Add("RequireSkillExpLimit", entry.RequireSkillExpLimit);
                qb.Add("AquiredPoint", entry.AquiredPoint);
                qb.Add("QuestRequireExpLimit", entry.QuestRequireExpLimit);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "SkillPointEXP_Table_New.sql"), sb);
        }
    }
}
