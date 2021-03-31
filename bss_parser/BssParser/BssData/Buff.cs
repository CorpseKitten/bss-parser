using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("buff")]
    public class Buff : IBssEntry
    {
        public ushort BuffId { get; set; }
        public string BuffName { get; set; }
        public uint Level { get; set; }
        public ushort Group { get; set; }
        public ushort ConditionType { get; set; }
        public byte ModuleType { get; set; }
        public List<bool> UsedParameters { get; set; }
        public List<long> Parameters { get; set; }
        public byte BuffType { get; set; }
        public int ValidityTime { get; set; }
        public int RepeatTime { get; set; }
        public long MaxConditionApplyCount { get; set; }
        public byte NeedEquipType { get; set; }
        public byte NeedEquipSlot { get; set; }
        public string BuffEffect { get; set; }
        public byte IsDisplay { get; set; }
        public int ApplyRate { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var buff = new Buff();
            buff.BuffId = br.ReadUInt16();
            buff.BuffName = br.ReadUnicodeStringBD();
            buff.Level = br.ReadUInt32();
            buff.Group = br.ReadUInt16();
            buff.ConditionType = br.ReadUInt16();
            buff.ModuleType = br.ReadByte();
            buff.UsedParameters = Enumerable.Range(0, 10).Select(x => br.ReadBoolean()).ToList();
            buff.Parameters = Enumerable.Range(0, 10).Select(x => br.ReadInt64()).ToList();
            br.ReadUnicodeStringBD();
            buff.BuffType = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            buff.ValidityTime = br.ReadInt32();
            buff.RepeatTime = br.ReadInt32();
            buff.MaxConditionApplyCount = br.ReadInt64();
            br.ReadInt16();
            buff.NeedEquipType = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            buff.NeedEquipSlot = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            buff.BuffEffect = br.ReadUnicodeStringBD();
            br.ReadUnicodeStringBD();
            buff.IsDisplay = br.ReadByte();
            buff.ApplyRate = br.ReadInt32();
            br.ReadUnicodeStringBD();
            br.ReadByte();
            br.ReadByte(); // 485
            return buff;
        }

        public void ReadTable(BinaryReader br)
        {
            var table = new List<Buff>();
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x =>
            {
                table.Add((Buff) Read(br));
            });
            BuffConverter.CreateXml(table);
            BuffConverter.CreateSql(table);
        }
    }
}
