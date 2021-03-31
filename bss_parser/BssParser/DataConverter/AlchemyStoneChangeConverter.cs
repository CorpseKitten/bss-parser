using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class AlchemyStoneChangeConverter
    {
        public static void CreateSql(List<AlchemyStoneChange> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("AlchemyStoneChange_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("AlchemyStoneChange_Table");
                qb.Add("ID", sb.Count);
                qb.Add("ItemKey", entry.ItemKey);
                qb.Add("NeedItemKey", entry.NeedItemKey);
                qb.Add("NeedItemCount", entry.NeedItemCount);
                qb.Add("MainGroup", entry.MainGroupKey);
                qb.Add("BreakRate", entry.BreakRate);
                qb.Add("DownRate", entry.DownRate);
                qb.Add("DownGroup", entry.DownGroup);
                qb.Add("Condition", entry.Condition);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "AlchemyStoneChange_Table.sql"), sb);
        }
        public static void CreateXml(List<AlchemyStoneChange> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "AlchemyStoneChange_Table"),
                    from entry in Entries.Select(x => (AlchemyStoneChange)x)
                    select
                    new XElement("data",
                        new XAttribute("ItemKey", entry.ItemKey),
                        new XAttribute("NeedItemKey", entry.NeedItemKey),
                        new XAttribute("NeedItemCount", entry.NeedItemCount),
                        new XAttribute("MainGroup", entry.MainGroupKey),
                        new XAttribute("BreakRate", entry.BreakRate),
                        new XAttribute("DownRate", entry.DownRate),
                        new XAttribute("DownGroup", entry.DownGroup),
                        new XAttribute("Condition", entry.Condition)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "AlchemyStoneChange_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
