using System.Collections.Generic;
using System.IO;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class FieldBossConverter
    {
        public static void CreateSql(List<FieldBoss> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("FieldBoss_Table") };
            int i = 1;
            foreach (var entry in Entries)
            {
                foreach (var type1 in entry.Type1Drops)
                {
                    var qb = new QueryBuilder("FieldBoss_Table");
                    qb.Add("ID", i++);
                    qb.Add("CharacterKey", entry.CharacterKey);
                    qb.Add("Type", 0);
                    qb.Add("Rank", type1.Rank);
                    qb.Add("DropGroupKey", type1.DropGroupKey);
                    sb.Add(qb.Combine());
                }

                foreach (var type1 in entry.Type2Drops)
                {
                    var qb = new QueryBuilder("FieldBoss_Table");
                    qb.Add("ID", i++);
                    qb.Add("CharacterKey", entry.CharacterKey);
                    qb.Add("Type", 1);
                    qb.Add("Rank", type1.Rank);
                    qb.Add("DropGroupKey", type1.DropGroupKey);
                    sb.Add(qb.Combine());
                }

                foreach (var type1 in entry.Type3Drops)
                {
                    var qb = new QueryBuilder("FieldBoss_Table");
                    qb.Add("ID", i++);
                    qb.Add("CharacterKey", entry.CharacterKey);
                    qb.Add("Type", 2);
                    qb.Add("Rank", type1.Rank);
                    qb.Add("DropGroupKey", type1.DropGroupKey);
                    sb.Add(qb.Combine());
                }
            }

            File.WriteAllLines(Path.Combine("output.sql", "FieldBoss_Table.sql"), sb);
        }
    }
}
