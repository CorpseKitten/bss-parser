using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class GameOptionConverter
    {
        public static void CreateSql(GameOption Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("ClassBalance") };

            foreach (var cls in Entries.ClassBalance)
            {
                var qb = new QueryBuilder("ClassBalance");
                qb.Add("ID", sb.Count);
                qb.Add("ClassKey", cls.Key);
                for (var i = 0; i < 32; ++i)
                    qb.Add("_" + i, cls.Value[i]);
                sb.Add(qb.Combine());
            }

            // sb.add qb.combine
            File.WriteAllLines(Path.Combine("output.sql", "ClassBalance.sql"), sb);
        }
        public static void CreateXml(GameOption Entries)
        {
            var x = 0;
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "ClassBalance"),
                    from entry in Entries.ClassBalance
                    select
                    new XElement("data",
                        new XAttribute("ClassKey", entry.Key),
                        new XAttribute("_0", entry.Value[0]),
                        new XAttribute("_1", entry.Value[1]),
                        new XAttribute("_2", entry.Value[2]),
                        new XAttribute("_3", entry.Value[3]),
                        new XAttribute("_4", entry.Value[4]),
                        new XAttribute("_5", entry.Value[5]),
                        new XAttribute("_6", entry.Value[6]),
                        new XAttribute("_7", entry.Value[7]),
                        new XAttribute("_8", entry.Value[8]),
                        new XAttribute("_9", entry.Value[9]),
                        new XAttribute("_10", entry.Value[10]),
                        new XAttribute("_11", entry.Value[11]),
                        new XAttribute("_12", entry.Value[12]),
                        new XAttribute("_13", entry.Value[13]),
                        new XAttribute("_14", entry.Value[14]),
                        new XAttribute("_15", entry.Value[15]),
                        new XAttribute("_16", entry.Value[16]),
                        new XAttribute("_17", entry.Value[17]),
                        new XAttribute("_18", entry.Value[18]),
                        new XAttribute("_19", entry.Value[19]),
                        new XAttribute("_20", entry.Value[20]),
                        new XAttribute("_21", entry.Value[21]),
                        new XAttribute("_22", entry.Value[22]),
                        new XAttribute("_23", entry.Value[23]),
                        new XAttribute("_24", entry.Value[24]),
                        new XAttribute("_25", entry.Value[25]),
                        new XAttribute("_26", entry.Value[26]),
                        new XAttribute("_27", entry.Value[27]),
                        new XAttribute("_28", entry.Value[28]),
                        new XAttribute("_29", entry.Value[29]),
                        new XAttribute("_30", entry.Value[30]),
                        new XAttribute("_31", entry.Value[31])
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Class_Balance.xml"), xws))
                document.Save(xw);
        }
    }
}
