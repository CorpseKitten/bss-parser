using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("plantworker")]
    public class PlantWorker : IBssEntry
    {
        public ushort CharacterKey { get; set; }
        public ushort UpgradeCharacterKey { get; set; }
        public ushort DefaultSkillKey { get; set; }
        public ushort LopExchangeKey { get; set; }
        public ushort PestControlExchangeKey { get; set; }
        public int MovementSpeed { get; set; }
        public int ActionPoint { get; set; }
        public int Luck { get; set; }
        public byte Supply { get; set; }
        public List<long> SellPrice { get; set; }
        public List<int> WorkerEfficiency { get; set; }
        public List<long> Level { get; set; }
        public string CharacterFaceIcon { get; set; }
        public List<int> Group { get; set; }
        public List<int> Upgrade { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var entry = new PlantWorker();
            br.ReadInt16();
            entry.CharacterKey = br.ReadUInt16();
            entry.UpgradeCharacterKey = br.ReadUInt16();
            entry.DefaultSkillKey = br.ReadUInt16();
            entry.LopExchangeKey = br.ReadUInt16();
            entry.PestControlExchangeKey = br.ReadUInt16();
            entry.MovementSpeed = br.ReadInt32();
            entry.ActionPoint = br.ReadInt32();
            entry.Luck = br.ReadInt32();
            entry.Supply = br.ReadByte();
            entry.SellPrice = Enumerable.Range(0, 31).Select(x => br.ReadInt64()).ToList();
            entry.WorkerEfficiency = Enumerable.Range(0, 9).Select(x => br.ReadInt32()).ToList();
            entry.Level = Enumerable.Range(0, 31).Select(x => br.ReadInt64()).ToList();
            entry.CharacterFaceIcon = br.ReadAsciiStringBD();
            entry.Group = Enumerable.Range(0, 6).Select(x => br.ReadInt32()).ToList();
            entry.Upgrade = Enumerable.Range(0, 31).Select(x => br.ReadInt32()).ToList();
            return entry;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (PlantWorker) Read(br)).ToList();
            entries = entries.OrderBy(x => x.CharacterKey).ToList();
            PlantWorkerConverter.CreateSql(entries);
        }
    }
}
