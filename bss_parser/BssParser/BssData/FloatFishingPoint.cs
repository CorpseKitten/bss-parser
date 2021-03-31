using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("floatfishingpoint")]
    public class FloatFishingPoint : IBssEntry
    {
        public short PointKey { get; set; }
        public int PointSize { get; set; }
        public float StartPositionX { get; set; }
        public float StartPositionY { get; set; }
        public float StartPositionZ { get; set; }
        public float EndPositionX { get; set; }
        public float EndPositionY { get; set; }
        public float EndPositionZ { get; set; }
        public short FishGroupKey { get; set; }
        public int SpawnRate { get; set; }
        public short SpawnCharacterKey { get; set; }
        public int SpawnActionIndex { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new FloatFishingPoint
            {
                PointKey = br.ReadInt16(),
                PointSize = br.ReadInt32(),
                StartPositionX = br.ReadSingle(),
                StartPositionY = br.ReadSingle(),
                StartPositionZ = br.ReadSingle(),
                EndPositionX = br.ReadSingle(),
                EndPositionY = br.ReadSingle(),
                EndPositionZ = br.ReadSingle(),
                FishGroupKey = br.ReadInt16(),
                SpawnRate = br.ReadInt32(),
                SpawnCharacterKey = br.ReadInt16(),
                SpawnActionIndex = br.ReadInt32(),
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).ToList().Select(i => (FloatFishingPoint) Read(br)).ToList();
            FloatFishingPointConverter.CreateSql(entries);
            FloatFishingPointConverter.CreateXml(entries);
        }
    }
}
