using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ItemMainGroupConverter
    {
        public static void CreateSql(List<ItemMainGroup> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("ItemMainGroup_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("ItemMainGroup_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("ItemMainGroupKey", entry.ItemMainGroupKey);
                qb.Add("DoSelectOnlyOne", entry.DoSelectOnlyOne);
                qb.Add("RefreshStartHour", entry.RefreshStartHour);
                qb.Add("RefreshInterval", entry.RefreshStartHour);
                qb.Add("PlantCraftResultCount", entry.PlantCraftResultCount);

                if (entry.SubGroups.ElementAtOrDefault(0) != null)
                {
                    qb.Add("ItemSubGroupKey0", entry.SubGroups[0].ItemSubGroupKey);
                    qb.Add("Condition0", entry.SubGroups[0].Condition);
                    qb.Add("SelectRate0", entry.SubGroups[0].SelectRate);
                }

                if (entry.SubGroups.ElementAtOrDefault(1) != null)
                {
                    qb.Add("ItemSubGroupKey1", entry.SubGroups[1].ItemSubGroupKey);
                    qb.Add("Condition1", entry.SubGroups[1].Condition);
                    qb.Add("SelectRate1", entry.SubGroups[1].SelectRate);
                }

                if (entry.SubGroups.ElementAtOrDefault(2) != null)
                {
                    qb.Add("ItemSubGroupKey2", entry.SubGroups[2].ItemSubGroupKey);
                    qb.Add("Condition2", entry.SubGroups[2].Condition);
                    qb.Add("SelectRate2", entry.SubGroups[2].SelectRate);
                }

                if (entry.SubGroups.ElementAtOrDefault(3) != null)
                {
                    qb.Add("ItemSubGroupKey3", entry.SubGroups[3].ItemSubGroupKey);
                    qb.Add("Condition3", entry.SubGroups[3].Condition);
                    qb.Add("SelectRate3", entry.SubGroups[3].SelectRate);
                }

                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "ItemMainGroup_Table.sql"), sb);
        }
        public static void CreateXml(List<ItemMainGroup> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "ItemMainGroup_Table"),
                new XElement("keys", "ItemMainGroupKey"),
                from entry in Entries.Select(x => (ItemMainGroup)x)
                select
                    new XElement("data",
                        new XAttribute("ItemMainGroupKey", entry.ItemMainGroupKey),

                        new XAttribute("DoSelectOnlyOne", entry.DoSelectOnlyOne),
                        new XAttribute("RefreshStartHour", entry.RefreshStartHour),
                        new XAttribute("RefreshInterval", entry.RefreshStartHour),
                        new XAttribute("PlantCraftResultCount", entry.PlantCraftResultCount),

                        entry.SubGroups.ElementAtOrDefault(0) != null ? new XAttribute("ItemSubGroupKey0", entry.SubGroups[0].ItemSubGroupKey) : null,
                        entry.SubGroups.ElementAtOrDefault(0) != null ? new XAttribute("Condition0", entry.SubGroups[0].Condition) : null,
                        entry.SubGroups.ElementAtOrDefault(0) != null ? new XAttribute("SelectRate0", entry.SubGroups[0].SelectRate) : null,

                        entry.SubGroups.ElementAtOrDefault(1) != null ? new XAttribute("ItemSubGroupKey1", entry.SubGroups[1].ItemSubGroupKey) : null,
                        entry.SubGroups.ElementAtOrDefault(1) != null ? new XAttribute("Condition1",  entry.SubGroups[1].Condition) : null,
                        entry.SubGroups.ElementAtOrDefault(1) != null ? new XAttribute("SelectRate1", entry.SubGroups[1].SelectRate) : null,

                        entry.SubGroups.ElementAtOrDefault(2) != null ? new XAttribute("ItemSubGroupKey2", entry.SubGroups[2].ItemSubGroupKey) : null,
                        entry.SubGroups.ElementAtOrDefault(2) != null ? new XAttribute("Condition2", entry.SubGroups[2].Condition) : null,
                        entry.SubGroups.ElementAtOrDefault(2) != null ? new XAttribute("SelectRate2", entry.SubGroups[2].SelectRate) : null,

                        entry.SubGroups.ElementAtOrDefault(3) != null ? new XAttribute("ItemSubGroupKey3", entry.SubGroups[3].ItemSubGroupKey) : null,
                        entry.SubGroups.ElementAtOrDefault(3) != null ? new XAttribute("Condition3", entry.SubGroups[3].Condition) : null,
                        entry.SubGroups.ElementAtOrDefault(3) != null ? new XAttribute("SelectRate3", entry.SubGroups[3].SelectRate) : null
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "ItemMainGroup_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
