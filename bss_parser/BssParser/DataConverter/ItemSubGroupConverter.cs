using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ItemSubGroupConverter
    {
        public static void CreateSql(List<ItemSubGroup> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("ItemSubGroup_Table") };
            foreach (var entry in Entries)
            {
                foreach (var subGroup in entry.SubGroups)
                {
                    var qb = new QueryBuilder("ItemSubGroup_Table");
                    qb.Add("ItemSubGroupKey", entry.ItemSubGroupKey);
                    qb.Add("ItemKey", subGroup.ItemKey);
                    qb.Add("EnchantLevel", subGroup.EnchantLevel);
                    qb.Add("SelectRate_0", subGroup.SelectRate);
                    qb.Add("MinCount_0", subGroup.MinCount);
                    qb.Add("MaxCount_0", subGroup.MaxCount);
                    if (subGroup.IntimacyVariation != 0)
                        qb.Add("IntimacyVariation", subGroup.IntimacyVariation);
                    sb.Add(qb.Combine());
                }
            }

            File.WriteAllLines(Path.Combine("output.sql", "ItemSubGroup_Table.sql"), sb);
        }
        public static void CreateSql_Filler(List<ItemSubGroup> Entries)
        {
            var sb = new List<string> { };
            foreach (var entry in Entries)
            {
                foreach (var subGroup in entry.SubGroups.Where(x => x.IntimacyVariation != 0))
                {
                    var qb = new UpdateQueryBuilder("ItemSubGroup_Table");
                    qb.Add("IntimacyVariation", subGroup.IntimacyVariation);
                    //qb.Add("SelectRate_0", subGroup.SelectRate);
                    //qb.Add("MinCount_0", subGroup.MinCount);
                    //qb.Add("MaxCount_0", subGroup.MaxCount);
                    sb.Add(qb.MultiCombine(new []
                    {
                        "ItemSubGroupKey", "ItemKey"
                    }, new object[]
                    {
                        entry.ItemSubGroupKey, subGroup.ItemKey
                    }));
                }
            }

            File.WriteAllLines(Path.Combine("output.sql", "ItemSubGroup_Table_Filler.sql"), sb);
        }
        public static void CreateXml(List<ItemSubGroup> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("table", "ItemSubGroup_Table"),
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                from entry in Entries
                select
                    from subGroup in entry.SubGroups //.Where(x => x.MinCount == -1)
                    select
                        new XElement("data",
                            new XAttribute("ItemSubGroupKey", entry.ItemSubGroupKey),
                            new XAttribute("ItemKey", subGroup.ItemKey),
                            new XAttribute("EnchantLevel", subGroup.EnchantLevel),

                            new XAttribute("SelectRate_0", subGroup.SelectRate),
                            new XAttribute("MinCount_0", subGroup.MinCount),
                            new XAttribute("MaxCount_0", subGroup.MaxCount)
                        )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "ItemSubGroup_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
