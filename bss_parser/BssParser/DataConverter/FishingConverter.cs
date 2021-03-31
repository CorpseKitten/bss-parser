using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class FishingConverter
    {
        public static void CreateSql(List<FishingInfo> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("Fishing_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Fishing_Table");
                qb.Add("ID", sb.Count);
                qb.Add("R", entry.R1);
                qb.Add("G", entry.G1);
                qb.Add("B", entry.B1);
                if (entry.DropId > 0)
                   qb.Add("DropID", entry.DropId);
                if (entry.DropIdHarpoon > 0)
                    qb.Add("DropIDHarpoon", entry.DropIdHarpoon);
                if (entry.DropIdNet > 0)
                    qb.Add("DropIDNet", entry.DropIdNet);
                qb.Add("DropRate1", entry.DropRate1);
                if (entry.DropId1 > 0)
                    qb.Add("DropID1", entry.DropId1);
                qb.Add("DropRate2", entry.DropRate2);
                if (entry.DropId2 > 0)
                    qb.Add("DropID2", entry.DropId2);
                qb.Add("DropRate3", entry.DropRate3);
                if (entry.DropId3 > 0)
                    qb.Add("DropID3", entry.DropId3);
                qb.Add("DropRate4", entry.DropRate4);
                if (entry.DropId4 > 0)
                    qb.Add("DropID4", entry.DropId4);
                qb.Add("DropRate5", entry.DropRate5);
                if (entry.DropId5 > 0)
                    qb.Add("DropID5", entry.DropId5);
                qb.Add("MinWaitTime", entry.MinWaitTime);
                qb.Add("MaxWaitTime", entry.MaxWaitTime);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "Fishing_Table.sql"), sb);
        }
        public static void CreateXml(List<FishingInfo> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "Fishing_Table"),
                from entry in Entries
                select
                    new XElement("data",
                        new XAttribute("R", entry.R1),
                        new XAttribute("G", entry.G1),
                        new XAttribute("B", entry.B1),
                        new XAttribute("DropID", entry.DropId),
                        new XAttribute("DropIDHarpoon", entry.DropIdHarpoon),
                        new XAttribute("DropIDNet", entry.DropIdNet),
                        new XAttribute("DropRate1", entry.DropRate1),
                        new XAttribute("DropID1", entry.DropId1),
                        new XAttribute("DropRate2", entry.DropRate2),
                        new XAttribute("DropID2", entry.DropId2),
                        new XAttribute("DropRate3", entry.DropRate3),
                        new XAttribute("DropID3", entry.DropId3),
                        new XAttribute("DropRate4", entry.DropRate4),
                        new XAttribute("DropID4", entry.DropId4),
                        new XAttribute("DropRate5", entry.DropRate5),
                        new XAttribute("DropID5", entry.DropId5),
                        new XAttribute("MinWaitTime", entry.MinWaitTime),
                        new XAttribute("MaxWaitTime", entry.MaxWaitTime)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Fishing_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
