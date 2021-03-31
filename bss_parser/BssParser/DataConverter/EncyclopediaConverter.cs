using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class EncyclopediaConverter
    {
        public static void CreateXml(List<Encyclopedia> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "Encyclopedia_Table"),
                    from entry in Entries
                    select
                    new XElement("data",
                        new XAttribute("Key", entry.Key),
                        new XAttribute("Type", entry.Type),
                        new XAttribute("TypeKey", entry.TypeKey),
                        new XAttribute("Category", entry.Category),
                        new XAttribute("Rareness", entry.Rareness),
                        new XAttribute("BaseSize", entry.BaseSize),
                        new XAttribute("VarySize_0",
                            $"{entry.VarySize[0][0]}D{entry.VarySize[0][1]}{(entry.VarySize[0][2] > 0 ? $"+{entry.VarySize[0][2]}" : $"{entry.VarySize[0][2]}")}"),
                        new XAttribute("VarySize_1",
                            $"{entry.VarySize[1][0]}D{entry.VarySize[1][1]}{(entry.VarySize[1][2] > 0 ? $"+{entry.VarySize[1][2]}" : $"{entry.VarySize[1][2]}")}"),
                        new XAttribute("VarySize_2",
                            $"{entry.VarySize[2][0]}D{entry.VarySize[2][1]}{(entry.VarySize[2][2] > 0 ? $"+{entry.VarySize[2][2]}" : $"{entry.VarySize[2][2]}")}"),
                        new XAttribute("VarySize_3",
                            $"{entry.VarySize[3][0]}D{entry.VarySize[3][1]}{(entry.VarySize[3][2] > 0 ? $"+{entry.VarySize[3][2]}" : $"{entry.VarySize[3][2]}")}"),
                        new XAttribute("VarySize_4",
                            $"{entry.VarySize[4][0]}D{entry.VarySize[4][1]}{(entry.VarySize[4][2] > 0 ? $"+{entry.VarySize[4][2]}" : $"{entry.VarySize[4][2]}")}"),
                        new XAttribute("FishExpType", entry.FishExpType),
                        new XAttribute("FishExpRate", entry.FishExpRate),
                        new XAttribute("Image", entry.Image),
                        new XAttribute("Desc", entry.Description)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Encyclopedia_Table.xml"), xws))
                document.Save(xw);
        }

        public static void CreateSql(List<Encyclopedia> Entries)
        {
            var sb = new List<string> {QueryBuilder.Prepare("Encyclopedia_Table")};
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Encyclopedia_Table");
                qb.Add("ID", sb.Count);
                qb.Add("Key", entry.Key);
                qb.Add("Type", entry.Type);
                qb.Add("TypeKey", entry.TypeKey);
                qb.Add("Category", entry.Category);
                qb.Add("Rareness", entry.Rareness);
                qb.Add("BaseSize", entry.BaseSize);
                qb.Add("VarySize_0",
                    $"{entry.VarySize[0][0]}D{entry.VarySize[0][1]}{(entry.VarySize[0][2] > 0 ? $"+{entry.VarySize[0][2]}" : $"{entry.VarySize[0][2]}")}");
                qb.Add("VarySize_1",
                    $"{entry.VarySize[1][0]}D{entry.VarySize[1][1]}{(entry.VarySize[1][2] > 0 ? $"+{entry.VarySize[1][2]}" : $"{entry.VarySize[1][2]}")}");
                qb.Add("VarySize_2",
                    $"{entry.VarySize[2][0]}D{entry.VarySize[2][1]}{(entry.VarySize[2][2] > 0 ? $"+{entry.VarySize[2][2]}" : $"{entry.VarySize[2][2]}")}");
                qb.Add("VarySize_3",
                    $"{entry.VarySize[3][0]}D{entry.VarySize[3][1]}{(entry.VarySize[3][2] > 0 ? $"+{entry.VarySize[3][2]}" : $"{entry.VarySize[3][2]}")}");
                qb.Add("VarySize_4",
                    $"{entry.VarySize[4][0]}D{entry.VarySize[4][1]}{(entry.VarySize[4][2] > 0 ? $"+{entry.VarySize[4][2]}" : $"{entry.VarySize[4][2]}")}");
                qb.Add("FishExpType", entry.FishExpType);
                qb.Add("FishExpRate", entry.FishExpRate);
                //qb.Add("Image", entry.Image);
                //qb.Add("Desc", entry.Description);

                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "Encyclopedia_Table.sql"), sb);
        }
    }
}