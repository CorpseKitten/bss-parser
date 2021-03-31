using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class KnowledgeLearningConverter
    {
        public static void CreateXml(List<KnowledgeLearning> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "KnowledgeLearning_Table"),
                from entry in Entries
                select
                    new XElement("data",
                        new XAttribute("Key", entry.Key),
                        new XAttribute("LearningType", entry.LearningType),
                        new XAttribute("SelectRate", entry.SelectRate),
                        new XAttribute("KnowledgeIndex", entry.KnowledgeIndex)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "KnowledgeLearning_Table.xml"), xws))
                document.Save(xw);
        }
        public static void CreateSql(List<KnowledgeLearning> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("KnowledgeLearning_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("KnowledgeLearning_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("Key", entry.Key);
                qb.Add("LearningType", entry.LearningType);
                qb.Add("SelectRate", entry.SelectRate);
                qb.Add("KnowledgeIndex", entry.KnowledgeIndex);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "KnowledgeLearning_Table.sql"), sb);
        }
    }
}
