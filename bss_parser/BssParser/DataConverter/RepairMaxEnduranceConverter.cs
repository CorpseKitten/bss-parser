using System.Collections.Generic;
using System.IO;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class RepairMaxEnduranceConverter
    {
        public static void CreateSql(List<RepairMaxEndurance> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("RepairMaxEndurance") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("RepairMaxEndurance");
                qb.Add("ID", sb.Count + 1);
                qb.Add("BaseItem", entry.BaseItem);
                //qb.Add("EnchantLevel", entry.EnchantLevel);
                qb.Add("OnlyNeedMoney", 0);
                qb.Add("OnlyMoneyRepairCount", 0);

                var itemNum = 1;
                foreach (var item in entry.RepairItems)
                {
                    if (itemNum > 10)
                        break;
                    qb.Add($"Item{itemNum}", item.ItemId);
                    qb.Add($"RepairCount{itemNum}", item.RepairCount);
                    qb.Add($"NeedMoney{itemNum}", item.NeedMoney);
                    ++itemNum;
                }
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "RepairMaxEndurance.sql"), sb);
        }
    }
}
