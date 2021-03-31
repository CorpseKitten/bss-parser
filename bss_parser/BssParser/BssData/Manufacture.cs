using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("manufacture")]
    public class Manufacture : IBssEntry
    {
        public int MaterialItem { get; set; }
        public long CheckTime { get; set; }
        public string ActionName { get; set; }
        public int SuccessPercent { get; set; }
        public byte UsingInstallationType { get; set; }
        public ushort ResultDropGroup { get; set; }
        public List<Item> Materials { get; set; }

        public class Item
        {
            public int MaterialItem { get; set; }
            public long MaterialCount { get; set; }
        }

        public IBssEntry Read(BinaryReader br)
        {
            var man = new Manufacture();
            man.MaterialItem = br.ReadInt32();
            man.CheckTime = br.ReadInt64();
            man.ActionName = br.ReadAsciiStringBD();
            man.SuccessPercent = br.ReadInt32();
            br.ReadInt16();
            br.ReadInt16();
            man.UsingInstallationType = br.ReadByte();
            man.ResultDropGroup = br.ReadUInt16();
            man.Materials = Enumerable.Range(0, br.ReadInt32())
                .Select(x => new Item
                {
                    MaterialItem = br.ReadInt32(),
                    MaterialCount = br.ReadInt64()
                }).ToList();
            return man;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (Manufacture)Read(br)).ToList();
            entries = entries.OrderBy(x => x.MaterialItem).ToList();
            ManufactureConverter.CreateXml(entries);
            ManufactureConverter.CreateSql(entries);
        }
    }
}
