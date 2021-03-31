using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class LifeActionExperienceConverter
    {
        public static void CreateXml(List<LifeActionExperience> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "LifeActionEXP_Table"),
                from entry in Entries
                select
                    new XElement("data",
                        new XAttribute("ItemKey", entry.ItemKey),
                        new XAttribute("LifeActionType0", entry.LifeActionType0),
                        new XAttribute("Exp0", entry.Exp0),
                        new XAttribute("LifeActionType1", entry.LifeActionType1),
                        new XAttribute("Exp1", entry.Exp1)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "LifeActionEXP_Table.xml"), xws))
                document.Save(xw);
        }
        public static void CreateSql(List<LifeActionExperience> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("LifeActionEXP_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("LifeActionEXP_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("ItemKey", entry.ItemKey);
                qb.Add("LifeActionType0", entry.LifeActionType0);
                qb.Add("Exp0", entry.Exp0);
                qb.Add("LifeActionType1", entry.LifeActionType1);
                qb.Add("Exp1", entry.Exp1);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "LifeActionEXP_Table.sql"), sb);
        }
    }
}
