using System;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("collect")]
    public class Collect : IBssEntry
    {
        public ushort Index { get; set; }
        public byte ProductDataType { get; set; }
        public int ProductTime { get; set; }
        public float ProductAllHp { get; set; }
        public float ProductDecreaseHP { get; set; }
        public ushort ItemDropId { get; set; }
        public byte ToolType0 { get; set; }
        public byte ToolType1 { get; set; }
        public byte ToolType2 { get; set; }
        public ushort ItemDropId0 { get; set; }
        public ushort ItemDropId1 { get; set; }
        public ushort ItemDropId2 { get; set; }
        public int ActionPoint { get; set; }
        public int SpawnDelay { get; set; }
        public int SpawnVariableTime { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var collect = new Collect();
            collect.Index = br.ReadUInt16();
            br.ReadInt64();
            br.ReadInt16();
            collect.ProductDataType = br.ReadByte();
            collect.ProductTime = br.ReadInt32();
            collect.ProductAllHp = br.ReadSingle();
            collect.ProductDecreaseHP = br.ReadSingle();
            collect.ItemDropId = br.ReadUInt16();
            collect.ToolType0 = br.ReadByte();
            collect.ToolType1 = br.ReadByte();
            collect.ToolType2 = br.ReadByte();
            collect.ItemDropId0 = br.ReadUInt16();
            collect.ItemDropId1 = br.ReadUInt16();
            collect.ItemDropId2 = br.ReadUInt16();
            collect.ActionPoint = br.ReadInt32();
            collect.SpawnDelay = br.ReadInt32();
            collect.SpawnVariableTime = br.ReadInt32();
            return collect;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (Collect)Read(br)).ToList();
            entries = entries.OrderBy(x => x.Index).ToList();
            CollectConverter.CreateXml(entries);
            CollectConverter.CreateSql(entries);
        }
    }
}
