using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("characterstatic")]
    public class CharacterStatic : IBssEntry
    {
        public ushort Index { get; set; }
        public byte IsHiddenName { get; set; }
        public byte IsHiddenHp { get; set; }
        public byte IsRenderShadow { get; set; }
        public byte UseDefaultAudio { get; set; }
        public string ActiveCondition { get; set; }
        public string InterActiveCondition { get; set; }
        public ushort DropId { get; set; }
        public ushort CharacterGroup { get; set; }
        public byte CharKind { get; set; }
        public byte GradeType { get; set; }
        public int VariedTendencyOnDead { get; set; }
        public float[] BoxSize { get; set; }
        public float Health { get; set; }
        public int Mana { get; set; }
        public int MaxCriticalRate { get; set; }
        public int MaxFishingLuck { get; set; }
        public int MaxCollectItemLuck { get; set; }
        public int MaxDropItemLuck { get; set; }
        public long PossessableWeight { get; set; }
        public int Suspension { get; set; }
        public int MaxAttackSpeed { get; set; }
        public int MaxMoveSpeed { get; set; }
        public int MaxCastSpeed { get; set; }
        public byte IsFixed { get; set; }
        public byte IsCachedAble { get; set; }
        public byte VehicleType { get; set; }
        public byte VehicleSeatCount { get; set; }
        public byte WorldMapDisplayType { get; set; }
        public int TribeType { get; set; }
        public byte InventoryMax { get; set; }
        public int Level { get; set; }
        public long VanishTime { get; set; }
        public float BodySize { get; set; }
        public float BodyHeight { get; set; }
        public int Weight { get; set; }
        public int ObstacleType { get; set; }
        public float BodyHeight2 { get; set; }
        public string AiScriptClassName { get; set; }
        public string ActionScriptFilePrefix { get; set; }
        public string CharacterDisplayName { get; set; }
        public string CharacterName { get; set; }
        public string CharcaterTitle { get; set; }
        public string VoiceBankFileName { get; set; }
        public List<ushort> ItemSubGroupData { get; set; }
        public IBssEntry Read(BinaryReader br)
        {
            var st = new CharacterStatic();
            st.Index = br.ReadUInt16();
            st.IsHiddenName = br.ReadByte();
            st.IsHiddenHp = br.ReadByte();
            br.ReadInt16();
            br.ReadByte();
            st.IsRenderShadow = br.ReadByte();
            st.UseDefaultAudio = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            st.ActiveCondition = br.ReadUnicodeStringBD();
            st.InterActiveCondition = br.ReadUnicodeStringBD();
            br.ReadByte();
            st.DropId = br.ReadUInt16();
            st.CharacterGroup = br.ReadUInt16();
            st.CharKind = br.ReadByte();
            st.GradeType = br.ReadByte();
            st.VariedTendencyOnDead = br.ReadInt32();
            st.BoxSize = Enumerable.Range(0, 3).Select(x => br.ReadSingle()).ToArray();
            br.ReadInt32();
            br.ReadInt16(); // collect drop id
            st.Health = br.ReadSingle();
            st.Mana = br.ReadInt32();
            br.ReadInt16();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            st.MaxCriticalRate = br.ReadInt32();
            st.MaxFishingLuck = br.ReadInt32();
            st.MaxCollectItemLuck = br.ReadInt32();
            st.MaxDropItemLuck = br.ReadInt32();
            st.PossessableWeight = br.ReadInt64();
            var kk = br.ReadSingle();
            st.Suspension = br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            st.MaxAttackSpeed = br.ReadInt32();
            st.MaxMoveSpeed = br.ReadInt32();
            st.MaxCastSpeed = br.ReadInt32();
            st.IsFixed = br.ReadByte();
            st.IsCachedAble = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadInt32();
            br.ReadByte();
            st.VehicleType = br.ReadByte();
            st.VehicleSeatCount = br.ReadByte();
            br.ReadByte();
            st.WorldMapDisplayType = br.ReadByte();
            st.TribeType = br.ReadInt32();
            st.InventoryMax = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadBytes(10); // #2 = isPushable
            st.Level = br.ReadInt32();
            br.ReadInt32();
            st.VanishTime = br.ReadInt64();
            st.BodySize = br.ReadSingle();
            st.BodyHeight = br.ReadSingle();
            st.Weight = br.ReadInt32();
            st.ObstacleType = br.ReadInt32();
            br.ReadInt32();
            st.BodyHeight2 = br.ReadSingle();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt16();
            br.ReadInt16();
            br.ReadInt32();
            br.ReadByte();
            br.ReadInt32();
            br.ReadByte();
            br.ReadInt16();
            br.ReadInt16();
            st.AiScriptClassName = br.ReadAsciiStringBD();
            st.ActionScriptFilePrefix = br.ReadAsciiStringBD();
            st.CharacterDisplayName = br.ReadUnicodeStringBD();
            st.CharacterName = br.ReadUnicodeStringBD();
            st.CharcaterTitle = br.ReadUnicodeStringBD();
            st.VoiceBankFileName = br.ReadAsciiStringBD();
            br.ReadByte();
            br.ReadInt32();
            br.ReadInt16();
            br.ReadByte();
            br.ReadUnicodeStringBD();
            br.ReadUnicodeStringBD();
            br.ReadInt16();
            br.ReadInt32();
            st.ItemSubGroupData = Enumerable.Range(0, br.ReadInt32()).Select(x => br.ReadUInt16()).ToList();
            br.ReadInt32();

            br.ReadSingle();
            br.ReadSingle();
            br.ReadSingle();
            br.ReadInt32();
            br.ReadInt32();
            br.ReadInt16();
            br.ReadInt32();
            return st;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (CharacterStatic) Read(br)).ToList();
            entries = entries.OrderBy(x => x.Index).ToList();
            CharacterStaticConverter.CreateXml(entries);
            CharacterStaticConverter.CreateSql(entries);
        }
    }
}
