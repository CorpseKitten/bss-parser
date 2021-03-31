using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("vehicleskill")]
    public class VehicleSkill : IBssEntry
    {
        public byte Index { get; set; }
        public string Name { get; set; }
        public int MaxExp { get; set; }
        public int AddExpForCheck { get; set; }
        public long BasePrice { get; set; }
        public int AddMatingRate { get; set; }
        public byte IsLearnFromItem { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public List<ushort> HorseNpcIds { get; set; }
        public byte IsMarketFilter { get; set; }

        public class Owner
        {
            public ushort CharacterKey { get; set; }
            public List<int> IsAbleToLearnSkill { get; set; }
            public List<int> AddedExpForLearningSkill { get; set; }
            public List<byte> SkillCanBeLearntInfo { get; set; }
        }

        public IBssEntry Read(BinaryReader br)
        {
            var vs = new VehicleSkill();
            vs.Index = br.ReadByte();
            vs.Name = br.ReadUnicodeStringBD();
            vs.MaxExp = br.ReadInt32();
            vs.AddExpForCheck = br.ReadInt32();
            vs.BasePrice = br.ReadInt64();
            vs.AddMatingRate = br.ReadInt32();
            vs.IsLearnFromItem = br.ReadByte();
            vs.Icon = br.ReadUnicodeStringBD();
            vs.Description = br.ReadUnicodeStringBD();
            vs.HorseNpcIds = Enumerable.Range(0, br.ReadInt32()).Select(x => br.ReadUInt16()).ToList();
            vs.IsMarketFilter = br.ReadByte();
            return vs;
        }

        public Owner ReadOwner(BinaryReader br)
        {
            var owner = new Owner();
            br.ReadUInt16();
            owner.CharacterKey = br.ReadUInt16();
            owner.IsAbleToLearnSkill = Enumerable.Range(0, 55).Select(x => br.ReadInt32()).ToList();
            owner.AddedExpForLearningSkill = Enumerable.Range(0, 55).Select(x => br.ReadInt32()).ToList();
            owner.SkillCanBeLearntInfo = Enumerable.Range(0, 55).Select(x => br.ReadByte()).ToList();
            return owner;
        }

        public void ReadTable(BinaryReader br)
        {
            var vehicleOwner = Enumerable.Range(0, br.ReadByte()).Select(x => (VehicleSkill) Read(br)).ToList();
            br.ReadBytes(102);
            var vehicleSkill = Enumerable.Range(0, br.ReadInt32()).Select(x => (Owner) ReadOwner(br)).ToList();
            VehicleSkillConverter.CreateSql(vehicleOwner, vehicleSkill);
        }
    }
}
