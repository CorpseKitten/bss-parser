using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("pcgrowth")]
    public class PcGrowth : IBssEntry
    {
        public byte ClassType { get; set; }
        public ushort CharacterKey { get; set; }
        public ushort BackgroundCharacterKey { get; set; }
        public ushort AlteregoKey { get; set; }
        public ushort WorkerCharacterKey { get; set; }
        public List<int> DefaultItemList { get; set; }
        public List<float> StartPosition { get; set; }
        public int StartDirection { get; set; }
        public int WeaponEnduranceDecreaseRate { get; set; }
        public int ArmorEnduranceDecreaseRate { get; set; }
        public int ArmorEnduranceArrowDecreaseRate { get; set; }
        public short FieldNo { get; set; }
        public byte CombatResourceType { get; set; }
        public int StartWaypointNo { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public string ClassMovie { get; set; }
        public List<string> ResourceSaveEffect { get; set; }
        public List<float> ComboColor { get; set; }
        public List<float> AwakenComboColor { get; set; }
        public byte MainAttackType { get; set; }
        public int DefaultVisibleWeapon { get; set; }
        public int DefaultVisibleSubWeapon { get; set; }
        public int DefaultVisibleAwakenWeapon { get; set; }
        public byte IsOpen { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var gr = new PcGrowth();
            gr.ClassType = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            gr.CharacterKey = br.ReadUInt16();
            gr.BackgroundCharacterKey = br.ReadUInt16();
            gr.AlteregoKey = br.ReadUInt16();
            gr.WorkerCharacterKey = br.ReadUInt16();
            br.ReadByte(); // class count
            gr.DefaultItemList = Enumerable.Range(0, br.ReadInt32())
                .Select(x => br.ReadInt32()).ToList();
            gr.StartPosition = Enumerable.Range(0, 3).Select(x => br.ReadSingle()).ToList();
            gr.StartDirection = br.ReadInt32();
            gr.WeaponEnduranceDecreaseRate = br.ReadInt32();
            gr.ArmorEnduranceDecreaseRate = br.ReadInt32();
            gr.ArmorEnduranceArrowDecreaseRate = br.ReadInt32();
            gr.FieldNo = br.ReadInt16();
            gr.CombatResourceType = br.ReadByte();
            gr.StartWaypointNo = br.ReadInt32();
            gr.ClassName = br.ReadUnicodeStringBD();
            gr.ClassDescription = br.ReadUnicodeStringBD();
            gr.ClassMovie = br.ReadUnicodeStringBD();
            br.ReadByte();
            gr.ResourceSaveEffect = Enumerable.Range(0, 4).Select(x => br.ReadAsciiStringBD()).ToList();
            gr.ComboColor = Enumerable.Range(0, 3).Select(x => br.ReadSingle()).ToList();
            gr.AwakenComboColor = Enumerable.Range(0, 3).Select(x => br.ReadSingle()).ToList();
            gr.MainAttackType = br.ReadByte();
            gr.DefaultVisibleWeapon = br.ReadInt32();
            gr.DefaultVisibleSubWeapon = br.ReadInt32();
            gr.DefaultVisibleAwakenWeapon = br.ReadInt32();
            gr.IsOpen = br.ReadByte();
            return gr;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (PcGrowth)Read(br)).ToList();
            entries = entries.OrderBy(x => x.ClassType).ToList();
            PcGrowthConverter.CreateXml(entries);
            PcGrowthConverter.CreateSql(entries);
        }
    }
}
