using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ItemEnchantConverter
    {
        [DllImport("msvcrt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int sscanf(string buffer, string format, ref int arg0, ref int arg1, ref int arg2);

        private static Tuple<int, int, int> ParseDice(string dice)
        {
            var n = 0;
            var sides = 0;
            var constant = 0;
            sscanf(dice, "%dD%d%d", ref n, ref sides, ref constant);
            return new Tuple<int, int, int>(n, sides, constant);
        }

        public static string Merge(string arg0, string arg1)
        {
            var dice1 = ParseDice(arg0);
            var dice2 = ParseDice(arg1);

            return dice1.Item3 == -1 || dice1.Item3 == 0 
                && dice2.Item3 == -1 || dice2.Item3 == 0
                ? arg0 
                : $"{dice1.Item1}D{dice1.Item2}{(dice1.Item3 + dice2.Item3):+0;-#}";
        }


        public static void CreateSql(List<ItemEnchant> Entries, string table)
        {
            var sb = new List<string>();

            // Group everything by item id and then enchant level.
            var filteredEntries = Entries
                .Where(x => x.OffensiveStats != null && x.DefensiveStats != null)
                .OrderBy(x => x.ItemId).ThenBy(x => x.EnchantLevel);
            
            // Select maximum enchant level possible.
            foreach (var entry in filteredEntries)
            {
                var qb = new UpdateQueryBuilder(table);
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
                sb.Add(qb.MultiCombine(new []
                {
                    "Index", "Enchant"
                }, new object[]
                {
                    entry.ItemId, entry.EnchantLevel
                }));
            }

            foreach (var entry in Entries)
            {
                var qb = new UpdateQueryBuilder(table);
                qb.Add("VariedPossessableWeight", entry.VariedPossessableWeight);
                qb.Add("VariedSuspension", entry.VariedSuspension);
                qb.Add("VariedCriticalRate", entry.VariedCriticalRate);
                qb.Add("VariedMoveSpeedRate", entry.VariedMoveSpeedRate);
                qb.Add("VariedAttackSpeedRate", entry.VariedAttackSpeedRate);
                qb.Add("VariedCastingSpeedRate", entry.VariedCastingSpeedRate);
                qb.Add("VariedCollectionSpeedRate", entry.VariedCollectionSpeedRate);
                qb.Add("VariedFishingSpeedRate", entry.VariedFishingSpeedRate);
                qb.Add("VariedDropItemRate", entry.VariedDropItemRate);


                if (entry.EnchantSkillNo > 0)
                    qb.Add("SkillNo", entry.EnchantSkillNo);
                else
                    qb.Add("SkillNo", null);
                sb.Add(qb.MultiCombine(new[] { "Index", "Enchant" }, new object[] { entry.ItemId, entry.EnchantLevel }));
            }

            File.WriteAllLines(Path.Combine("output.sql", table + ".sql"), sb);
        }

        public static void CreateSql2(List<ItemEnchant> Entries, string table)
        {
            var sb = new List<string>();
            foreach (var entry in Entries)
            {
                var qb = new UpdateQueryBuilder(table);
                if (entry.SkillIds.FirstOrDefault() > 0)
                    qb.Add("SkillNo", entry.SkillIds.FirstOrDefault());
                else
                    qb.Add("SkillNo", null);
                sb.Add(qb.Combine("Index", entry.ItemId.ToString()));
            }

            File.WriteAllLines(Path.Combine("output.sql", table + ".sql"), sb);
        }

        public static void CreateXml(List<ItemEnchant> Entries, string table)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("table", table),
                new XElement("keys", "Index,Enchant"),
                from entry in Entries.Where(x => x.OffensiveStats != null && x.DefensiveStats != null)
                select
                    new XElement("data",
                        new XAttribute("Index", entry.ItemId),
                        new XAttribute("Enchant", entry.EnchantLevel),
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
                        new XAttribute("HMPV", entry.DefensiveStats[2][3])
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", table + ".xml"), xws))
                document.Save(xw);
        }
    }
}
