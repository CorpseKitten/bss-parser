using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class PlantWorkerConverter
    {
        public static void CreateSql(List<PlantWorker> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("PlantWorker_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("PlantWorker_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("CharacterKey", entry.CharacterKey);
                qb.Add("MoveSpeed", entry.MovementSpeed);
                qb.Add("WorkerEfficiency", entry.WorkerEfficiency.FirstOrDefault(xq => xq > 0));
                qb.Add("ActionPoint", entry.ActionPoint);
                qb.Add("Luck", entry.Luck);
                qb.Add("DefaultSkillKey", entry.DefaultSkillKey);

                var x = 0;
                foreach(var lv in entry.Level)
                    qb.Add($"Lv{x++}", lv);

                x = 1;
                foreach(var group in entry.Group)
                    qb.Add($"Group{x++}", group);

                qb.Add("UpgradeCharacterKey", entry.UpgradeCharacterKey);

                x = 0;
                foreach(var upgrade in entry.Upgrade)
                    qb.Add($"Upgrade{x++}", upgrade);

                x = 0;
                foreach(var sellPrice in entry.SellPrice)
                    qb.Add($"SellPrice{x++}", sellPrice);

                qb.Add("LopExchangeKey", entry.LopExchangeKey);
                qb.Add("PestControlExchangeKey", entry.PestControlExchangeKey);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "PlantWorker_Table.sql"), sb);
        }
    }
}
