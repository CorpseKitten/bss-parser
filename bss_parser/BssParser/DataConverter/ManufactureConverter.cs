using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ManufactureConverter
    {
        public static void CreateXml(List<Manufacture> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "Manufacture_Table"),
                from entry in Entries
                select
                    new XElement("data",
                        entry.Materials.ElementAtOrDefault(0) != null ? new XAttribute("MaterialItem1",  entry.Materials[0].MaterialItem) : null,
                        entry.Materials.ElementAtOrDefault(0) != null ? new XAttribute("MaterialItemCount1", entry.Materials[0].MaterialCount) : null,
                        entry.Materials.ElementAtOrDefault(1) != null ? new XAttribute("MaterialItem2"     , entry.Materials[1].MaterialItem) : null,
                        entry.Materials.ElementAtOrDefault(1) != null ? new XAttribute("MaterialItemCount2", entry.Materials[1].MaterialCount) : null,
                        entry.Materials.ElementAtOrDefault(2) != null ? new XAttribute("MaterialItem3"     , entry.Materials[2].MaterialItem) : null,
                        entry.Materials.ElementAtOrDefault(2) != null ? new XAttribute("MaterialItemCount3", entry.Materials[2].MaterialCount) : null,
                        entry.Materials.ElementAtOrDefault(3) != null ? new XAttribute("MaterialItem4"     , entry.Materials[3].MaterialItem) : null,
                        entry.Materials.ElementAtOrDefault(3) != null ? new XAttribute("MaterialItemCount4", entry.Materials[3].MaterialCount) : null,
                        entry.Materials.ElementAtOrDefault(4) != null ? new XAttribute("MaterialItem5"     , entry.Materials[4].MaterialItem) : null,
                        entry.Materials.ElementAtOrDefault(4) != null ? new XAttribute("MaterialItemCount5", entry.Materials[4].MaterialCount) : null,
                        new XAttribute("ResultDropGroup", entry.ResultDropGroup),
                        new XAttribute("CheckTime", entry.CheckTime),
                        new XAttribute("SuccessPercent", entry.SuccessPercent),
                        new XAttribute("ActionName", entry.ActionName)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Manufacture_Table.xml"), xws))
                document.Save(xw);
        }
        public static void CreateSql(List<Manufacture> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("Manufacture_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Manufacture_Table");
                qb.Add("ID", sb.Count + 1);
                if (entry.Materials.ElementAtOrDefault(0) != null)
                {
                    qb.Add("MaterialItem1", entry.Materials[0].MaterialItem);
                    qb.Add("MaterialItemCount1", entry.Materials[0].MaterialCount);
                }
                if (entry.Materials.ElementAtOrDefault(1) != null)
                {
                    qb.Add("MaterialItem2", entry.Materials[1].MaterialItem);
                    qb.Add("MaterialItemCount2", entry.Materials[1].MaterialCount);
                }
                if (entry.Materials.ElementAtOrDefault(2) != null)
                {
                    qb.Add("MaterialItem3", entry.Materials[2].MaterialItem);
                    qb.Add("MaterialItemCount3", entry.Materials[2].MaterialCount);
                }
                if (entry.Materials.ElementAtOrDefault(3) != null)
                {
                    qb.Add("MaterialItem4", entry.Materials[3].MaterialItem);
                    qb.Add("MaterialItemCount4", entry.Materials[3].MaterialCount);
                }
                if (entry.Materials.ElementAtOrDefault(4) != null)
                {
                    qb.Add("MaterialItem5", entry.Materials[4].MaterialItem);
                    qb.Add("MaterialItemCount5", entry.Materials[4].MaterialCount);
                }
                qb.Add("ResultDropGroup", entry.ResultDropGroup);
                qb.Add("CheckTime", entry.CheckTime);
                qb.Add("SuccessPercent", entry.SuccessPercent);
                qb.Add("ActionName", entry.ActionName);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "Manufacture_Table.sql"), sb);
        }
    }
}
