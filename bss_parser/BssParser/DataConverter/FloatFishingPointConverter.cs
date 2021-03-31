using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class FloatFishingPointConverter
    {
        public static void CreateSql(List<FloatFishingPoint> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("FloatFishingPoint_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("FloatFishingPoint_Table");
                qb.Add("ID", sb.Count);
                qb.Add("PointKey", entry.PointKey);
                qb.Add("PointSize", entry.PointSize);
                qb.Add("StartPositionX", entry.StartPositionX);
                qb.Add("StartPositionY", entry.StartPositionY);
                qb.Add("StartPositionZ", entry.StartPositionZ);
                qb.Add("EndPositionX", entry.EndPositionX);
                //qb.Add("EndPositionY", entry.EndPositionY);
                qb.Add("EndPositionZ", entry.EndPositionZ);
                qb.Add("FishingGroupKey", entry.FishGroupKey);
                qb.Add("SpawnRate", entry.SpawnRate);
                qb.Add("SpawnCharacterKey", entry.SpawnCharacterKey);
                qb.Add("SpawnActionIndex", entry.SpawnActionIndex);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "FloatFishingPoint_Table.sql"), sb);
        }
        public static void CreateXml(List<FloatFishingPoint> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "FloatFishingPoint_Table"),
                from entry in Entries.Select(x => (FloatFishingPoint)x)
                select
                    new XElement("data",
                        new XAttribute("PointKey", entry.PointKey),
                        new XAttribute("PointSize", entry.PointSize),
                        new XAttribute("StartPositionX", entry.StartPositionX),
                        new XAttribute("StartPositionY", entry.StartPositionY),
                        new XAttribute("StartPositionZ", entry.StartPositionZ),
                        new XAttribute("EndPositionX", entry.EndPositionX),
                        //new XAttribute("EndPositionY", entry.EndPositionY),
                        new XAttribute("EndPositionZ", entry.EndPositionZ),
                        new XAttribute("FishingGroupKey", entry.FishGroupKey),
                        new XAttribute("SpawnRate", entry.SpawnRate),
                        new XAttribute("SpawnCharacterKey", entry.SpawnCharacterKey),
                        new XAttribute("SpawnActionIndex", entry.SpawnActionIndex)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "FloatFishingPoint_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
