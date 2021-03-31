using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("fishinginfo")]
    public class FishingInfo : IBssEntry
    {
        public byte B1 { get; set; }
        public byte G1 { get; set; }
        public byte R1 { get; set; }
        public byte A1 { get; set; }
        public byte B2 { get; set; }
        public byte G2 { get; set; }
        public byte R2 { get; set; }
        public byte A2 { get; set; }
        public short DropId { get; set; }
        public short DropIdHarpoon { get; set; }
        public short DropIdNet { get; set; }
        public int MinWaitTime { get; set; }
        public int MaxWaitTime { get; set; }
        public short DropId1 { get; set; }
        public short DropId2 { get; set; }
        public short DropId3 { get; set; }
        public short DropId4 { get; set; }
        public short DropId5 { get; set; }
        public int DropRate1 { get; set; }
        public int DropRate2 { get; set; }
        public int DropRate3 { get; set; }
        public int DropRate4 { get; set; }
        public int DropRate5 { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new FishingInfo
            {
                B1 = br.ReadByte(),
                G1 = br.ReadByte(),
                R1 = br.ReadByte(),
                A1 = br.ReadByte(),
                B2 = br.ReadByte(),
                G2 = br.ReadByte(),
                R2 = br.ReadByte(),
                A2 = br.ReadByte(),
                DropId = br.ReadInt16(),
                DropIdHarpoon = br.ReadInt16(),
                DropIdNet = br.ReadInt16(),
                MinWaitTime = br.ReadInt32(),
                MaxWaitTime = br.ReadInt32(),
                DropId1 = br.ReadInt16(),
                DropId2 = br.ReadInt16(),
                DropId3 = br.ReadInt16(),
                DropId4 = br.ReadInt16(),
                DropId5 = br.ReadInt16(),
                DropRate1 = br.ReadInt32(),
                DropRate2 = br.ReadInt32(),
                DropRate3 = br.ReadInt32(),
                DropRate4 = br.ReadInt32(),
                DropRate5 = br.ReadInt32()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = new List<FishingInfo>();
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x =>
            {
                entries.Add((FishingInfo) Read(br));
            });

            entries = entries.OrderBy(x => x.A1).ThenBy(x => x.B1).ThenBy(x => x.G1).ThenBy(x => x.R1).ToList();
            FishingConverter.CreateXml(entries);
            FishingConverter.CreateSql(entries);
        }
    }
}
