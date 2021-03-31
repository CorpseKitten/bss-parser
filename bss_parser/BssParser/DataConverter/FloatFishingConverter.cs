using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class FloatFishingConverter
    {
        public static void CreateSql(List<FloatFishing> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("FloatFishing_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("FloatFishing_Table");
                qb.Add("ID", sb.Count);
                qb.Add("FishingGroupKey", entry.FishingGroupKey);

                // Drop Rate 1
                {
                    if (entry.DropRate[0] > 0)
                        qb.Add("DropID1", entry.DropID[0]);
                    qb.Add("DropRate1", entry.DropRate[0]);
                }

                // Drop Rate 2
                {
                    if (entry.DropRate[1] > 0)
                        qb.Add("DropID2", entry.DropID[1]);
                    qb.Add("DropRate2", entry.DropRate[1]);
                }

                // Drop Rate 3
                {
                    if (entry.DropRate[2] > 0)
                        qb.Add("DropID3", entry.DropID[2]);
                    qb.Add("DropRate3", entry.DropRate[2]);
                }

                // Drop Rate 4
                {
                    if (entry.DropRate[3] > 0)
                        qb.Add("DropID4", entry.DropID[3]);
                    qb.Add("DropRate4", entry.DropRate[3]);
                }

                // Drop Rate 5
                {
                    if (entry.DropRate[4] > 0)
                        qb.Add("DropID5", entry.DropID[4]);
                    qb.Add("DropRate5", entry.DropRate[4]);
                }
                qb.Add("MinWaitTime", entry.MinWaitTime);
                qb.Add("MaxWaitTime", entry.MaxWaitTime);
                qb.Add("PointRemainTime", entry.PointRemainTime);
                qb.Add("MinFishCount", entry.MinFishCount);
                qb.Add("MaxFishCount", entry.MaxFishCount);
                qb.Add("AvailableFishingLevel", entry.AvailableFishingLevel);
                qb.Add("ObserveFishingLevel", entry.ObserveFishingLevel);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "FloatFishing_Table.sql"), sb);
        }
        public static void CreateXml(List<FloatFishing> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "FloatFishing_Table"),
                from entry in Entries
                select
                    new XElement("data", 
                        new XAttribute("FishingGroupKey", entry.FishingGroupKey),
                        new XAttribute("DropRate1", entry.DropRate[0]),
                        new XAttribute("DropID1", entry.DropID[0]),
                        new XAttribute("DropRate2", entry.DropRate[1]),
                        new XAttribute("DropID2", entry.DropID[1]),
                        new XAttribute("DropRate3", entry.DropRate[2]),
                        new XAttribute("DropID3", entry.DropID[2]),
                        new XAttribute("DropRate4", entry.DropRate[3]),
                        new XAttribute("DropID4", entry.DropID[3]),
                        new XAttribute("DropRate5", entry.DropRate[4]),
                        new XAttribute("DropID5", entry.DropID[4]),
                        new XAttribute("MinWaitTime", entry.MinWaitTime),
                        new XAttribute("MaxWaitTime", entry.MaxWaitTime),
                        new XAttribute("PointRemainTime", entry.PointRemainTime),
                        new XAttribute("MinFishCount", entry.MinFishCount),
                        new XAttribute("MaxFishCount", entry.MaxFishCount),
                        new XAttribute("AvailableFishingLevel", entry.AvailableFishingLevel),
                        new XAttribute("ObserveFishingLevel", entry.ObserveFishingLevel)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "FloatFishing_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
