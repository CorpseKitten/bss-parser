using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("itempuzzle")]
    public class ItemPuzzle : IBssEntry
    {
        public short PuzzleKey { get; set; }
        public List<int[]> PuzzleData { get; set; }
        public int BaseItemKey { get; set; }
        public int Unk1 { get; set; }
        public short Unk2 { get; set; }
        public short Unk3 { get; set; }
        public int MakeItemKey { get; set; }
        public short Unk4 { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new ItemPuzzle
            {
                PuzzleKey = br.ReadInt16(),
                PuzzleData = Enumerable.Range(0, 5).Select(x => new int[]
                {
                    br.ReadInt32(), // Puzzle0
                    br.ReadInt32(), // Puzzle1
                    br.ReadInt32(), // Puzzle2
                    br.ReadInt32(), // Puzzle3
                    br.ReadInt32(), // Puzzle4
                }).ToList(),
                BaseItemKey = br.ReadInt32(),
                Unk1 = br.ReadInt32(),
                Unk2 = br.ReadInt16(),
                Unk3 = br.ReadInt16(),
                MakeItemKey = br.ReadInt32(),
                Unk4 = br.ReadInt16()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (ItemPuzzle) Read(br)).ToList();
            entries = entries.OrderBy(x => x.BaseItemKey).ToList();
            ItemPuzzleConverter.CreateSql(entries);
            ItemPuzzleConverter.CreateXml(entries);
        }
    }
}
