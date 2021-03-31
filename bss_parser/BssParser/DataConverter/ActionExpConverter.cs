using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ActionExpConverter
    {
        public static void CreateXml(List<ActionExp> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "ActionEXP_Table"),
                from entry in Entries
                select
                    new XElement("data",
                        new XAttribute("Class", entry.Class),
                        new XAttribute("Level", entry.Level),
                        new XAttribute("Quest", entry.Quest),
                        new XAttribute("Collect", entry.Collect),
                        new XAttribute("Manufacture", entry.Manufacture),
                        new XAttribute("Alchemy", entry.Alchemy),
                        new XAttribute("Fishing", entry.Fishing),
                        new XAttribute("HorseTaming", entry.HorseTaming),
                        new XAttribute("Study", entry.Study),
                        new XAttribute("Trade", entry.Trade),
                        new XAttribute("Farming", entry.Farming)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "ActionEXP_Table.xml"), xws))
                document.Save(xw);
        }
        public static void CreateSql(List<ActionExp> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("ActionEXP_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("ActionEXP_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("Class", entry.Class);
                qb.Add("Level", entry.Level);
                qb.Add("Quest", entry.Quest);
                qb.Add("Collect", entry.Collect);
                qb.Add("Manufacture", entry.Manufacture);
                qb.Add("Alchemy", entry.Alchemy);
                qb.Add("Fishing", entry.Fishing);
                qb.Add("HorseTaming", entry.HorseTaming);
                qb.Add("Study", entry.Study);
                qb.Add("Trade", entry.Trade);
                qb.Add("Farming", entry.Farming);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "ActionEXP_Table.sql"), sb);
        }
    }
}
