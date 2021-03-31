using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class AlchemyConverter
    {
        public static void CreateSql(List<AlchemyStatus> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("Alchemy_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Alchemy_Table");
                qb.Add("ID", sb.Count);
                qb.Add("AlchemyType", entry.AlchemyType);
                qb.Add("KeyItem", entry.KeyItem);
                for (var i = 0; i < 10; ++i)
                {
                    if (entry.Materials.ElementAtOrDefault(i) == null)
                        continue;

                    qb.Add("MaterialItem" + (i + 1), entry.Materials[i].MaterialItem);
                    qb.Add("MaterialCount" + (i + 1), entry.Materials[i].MaterialCount);
                }
                qb.Add("ResultDropGroup", entry.ResultDropGroup);

                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "Alchemy_Table.sql"), sb);
        }
        public static void CreateXml(List<AlchemyStatus> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "Alchemy_Table"),
                from entry in Entries.Select(x => (AlchemyStatus)x)
                select
                    new XElement("data",
                        new XAttribute("AlchemyType", entry.AlchemyType),
                        new XAttribute("KeyItem", entry.KeyItem),
                        from i in Enumerable.Range(1, 10).Select(x => entry.Materials.ElementAtOrDefault(x - 1) != null ? x : 0)
                        select
                            i > 0 ? new XAttribute("MaterialCount" + i, entry.Materials[i - 1].MaterialCount) : null,

                        from i in Enumerable.Range(1, 10).Select(x => entry.Materials.ElementAtOrDefault(x - 1) != null ? x : 0)
                        select
                            i > 0 ? new XAttribute("MaterialItem" + i, entry.Materials[i - 1].MaterialCount) : null,
                        new XAttribute("ResultDropGroup", entry.ResultDropGroup)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Alchemy_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
