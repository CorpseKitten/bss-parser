using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("floatfishing")]
    public class FloatFishing : IBssEntry
    {
        public ushort FishingGroupKey { get; set; }
        public int MinWaitTime { get; set; }
        public int MaxWaitTime { get; set; }
        public int PointRemainTime { get; set; }
        public int MinFishCount { get; set; }
        public int MaxFishCount { get; set; }
        public int AvailableFishingLevel { get; set; }
        public int ObserveFishingLevel { get; set; }
        public List<ushort> DropID { get; set; }
        public List<uint> DropRate { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new FloatFishing
            {
                FishingGroupKey = br.ReadUInt16(),
                MinWaitTime = br.ReadInt32(),
                MaxWaitTime = br.ReadInt32(),
                PointRemainTime = br.ReadInt32(),
                MinFishCount = br.ReadInt32(),
                MaxFishCount = br.ReadInt32(),
                AvailableFishingLevel = br.ReadInt32(),
                ObserveFishingLevel = br.ReadInt32(),
                DropID = Enumerable.Range(0, 5).Select(x => br.ReadUInt16()).ToList(),
                DropRate = Enumerable.Range(0, 5).Select(x => br.ReadUInt32()).ToList()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (FloatFishing) Read(br)).ToList();
            entries = entries.OrderBy(x => x.FishingGroupKey).ToList();
            FloatFishingConverter.CreateSql(entries);
            FloatFishingConverter.CreateXml(entries);
        }
    }
}
