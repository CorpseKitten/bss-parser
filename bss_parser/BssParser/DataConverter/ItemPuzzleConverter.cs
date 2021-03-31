using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ItemPuzzleConverter
    {
        public static void CreateSql(List<ItemPuzzle> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("ItemPuzzle_Table") };
            foreach (var entry in Entries)
                foreach (var puzzleEntry in entry.PuzzleData)
                {
                    var qb = new QueryBuilder("ItemPuzzle_Table");
                    qb.Add("ID", sb.Count + 1);
                    qb.Add("PuzzleKey", entry.PuzzleKey);
                    qb.Add("Puzzle0", puzzleEntry[0]);
                    qb.Add("Puzzle1", puzzleEntry[1]);
                    qb.Add("Puzzle2", puzzleEntry[2]);
                    qb.Add("Puzzle3", puzzleEntry[3]);
                    qb.Add("Puzzle4", puzzleEntry[4]);
                    qb.Add("MakeItemKey", entry.MakeItemKey);
                    qb.Add("MakeItemEnchantLevel", 0);
                    sb.Add(qb.Combine());
                }

            File.WriteAllLines(Path.Combine("output.sql", "ItemPuzzle_Table.sql"), sb);
        }
        public static void CreateXml(List<ItemPuzzle> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "ItemPuzzle_Table"),
                from entry in Entries.Select(x => (ItemPuzzle)x)
                from puzzleEntry in entry.PuzzleData
                select
                    new XElement("data",
                        new XAttribute("PuzzleKey", entry.PuzzleKey),
                        new XAttribute("Puzzle0", puzzleEntry[0]),
                        new XAttribute("Puzzle1", puzzleEntry[1]),
                        new XAttribute("Puzzle2", puzzleEntry[2]),
                        new XAttribute("Puzzle3", puzzleEntry[3]),
                        new XAttribute("Puzzle4", puzzleEntry[4]),
                        new XAttribute("MakeItemKey", entry.MakeItemKey),
                        new XAttribute("MakeItemEnchantLevel", 0) // TODO
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "ItemPuzzle_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
