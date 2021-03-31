using System.Collections.Generic;
using System.IO;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class VehicleSkillConverter
    {
        public static void CreateSql(List<VehicleSkill> skill, List<VehicleSkill.Owner> owner)
        {
            var sb = new List<string>();
            foreach (var entry in owner)
            {
                var qb = new UpdateQueryBuilder("VehicleSkillOwner_Table");

                var x = 0;
                foreach (var able in entry.IsAbleToLearnSkill)
                {
                    if (x > 52)
                        break;
                    ++x;
                    if (x == 1)
                        continue;
                    qb.Add("Able_" + (x - 1), able);
                }

                x = 0;
                foreach (var exp in entry.AddedExpForLearningSkill)
                {
                    if (x > 52)
                        break;
                    ++x;
                    if (x == 1)
                        continue;
                    qb.Add("AddExp_" + (x - 1), exp);
                }

                x = 0;
                foreach (var learn in entry.SkillCanBeLearntInfo)
                {
                    if (x > 52)
                        break;
                    ++x;
                    if (x == 1)
                        continue;
                    qb.Add("IsLearn_" + (x - 1), learn);
                }

                sb.Add(qb.Combine("CharacterKey", entry.CharacterKey.ToString()));
            }
            File.WriteAllLines(Path.Combine("output.sql", "VehicleSkillOwner_Table.sql"), sb);
        }
    }
}
