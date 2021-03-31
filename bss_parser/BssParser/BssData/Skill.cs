using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("skill")]
    public class Skill : IBssEntry
    {
        public ushort SkillLevel1 { get; set; }
        public ushort SkillNo1 { get; set; }
        public ushort SkillLevel2 { get; set; }
        public ushort SkillNo2 { get; set; }
        public byte IsPromptForLearning { get; set; }
        public string ActionName { get; set; }
        public uint ActionHash { get; set; }
        public byte Unk1 { get; set; }
        public ushort SkillPointForLearning { get; set; }
        public int PcLevelForLearning { get; set; }
        public long NeedMoneyForLearning { get; set; }
        public int NeedItemIdForLearning { get; set; }
        public long NeedItemCountForLearning { get; set; }
        public ushort NeedSkillLevelNo0ForLearning { get; set; }
        public ushort NeedSkillNo0ForLearning { get; set; }
        public ushort NeedSkillLevelNo1ForLearning { get; set; }
        public ushort NeedSkillNo1ForLearning { get; set; }
        public byte Unk2 { get; set; }
        public int RequireHP { get; set; }
        public int RequireMP { get; set; }
        public int RequireResourcePoint { get; set; }
        public int RequireSP { get; set; }
        public int NeedItemId { get; set; }
        public long NeedItemCount { get; set; }
        public byte Unk3 { get; set; }
        public int Unk4 { get; set; }
        public short ReuseGroup { get; set; }
        public int ReuseCycle { get; set; }
        public byte DoCheckHit { get; set; }
        public float VariedHit { get; set; }
        public List<Tuple<string, float>> OffensiveStats { get; set; }
        public List<float[]> DefensiveStats { get; set; }
        public int UnkDefensiveRate { get; set; }
        public byte Unk5 { get; set; }
        public short ApplyNumber { get; set; }
        public int BuffApplyRate { get; set; }
        public List<ushort> BuffData { get; set; }
        public string Description { get; set; }
        public short FFFF { get; set; }
        public short Unk6 { get; set; }
        public byte UsableInCooltime { get; set; }
        public byte Unk7 { get; set; }
        public byte IsExpiredInOffline { get; set; }
        public ushort Unk8 { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var sk = new Skill();
            sk.SkillLevel1 = br.ReadUInt16();
            sk.SkillNo1 = br.ReadUInt16();
            sk.SkillLevel2 = br.ReadUInt16();
            sk.SkillNo2 = br.ReadUInt16();
            sk.IsPromptForLearning = br.ReadByte();
            sk.ActionName = br.ReadAsciiStringBD();
            sk.ActionHash = br.ReadUInt32();
            sk.Unk1 = br.ReadByte();
            sk.SkillPointForLearning = br.ReadUInt16();
            sk.PcLevelForLearning = br.ReadInt32();
            sk.NeedMoneyForLearning = br.ReadInt64();
            sk.NeedItemIdForLearning = br.ReadInt32();
            sk.NeedItemCountForLearning = br.ReadInt64();
            sk.NeedSkillLevelNo0ForLearning = br.ReadUInt16();
            sk.NeedSkillNo0ForLearning = br.ReadUInt16();
            sk.NeedSkillLevelNo1ForLearning = br.ReadUInt16();
            sk.NeedSkillNo1ForLearning = br.ReadUInt16();
            sk.Unk2 = br.ReadByte();
            sk.RequireHP = br.ReadInt32();
            sk.RequireMP = br.ReadInt32();
            sk.RequireResourcePoint = br.ReadInt32();
            sk.RequireSP = br.ReadInt32();
            sk.NeedItemId = br.ReadInt32();
            sk.NeedItemCount = br.ReadInt64();
            sk.Unk3 = br.ReadByte();
            sk.Unk4 = br.ReadInt32();
            sk.ReuseGroup = br.ReadInt16();
            sk.ReuseCycle = br.ReadInt32();
            sk.DoCheckHit = br.ReadByte();
            sk.VariedHit = br.ReadSingle();
            sk.OffensiveStats =
                Enumerable.Range(0, br.ReadInt32())
                    .Select(x => new Tuple<string, float>(br.ReadUnicodeStringBD(), br.ReadSingle()))
                    .ToList();
            sk.DefensiveStats = Enumerable.Range(0, br.ReadInt32()).Select(x =>
                new[]
                {
                    br.ReadSingle(), // DV
                    br.ReadSingle(), // HDV
                    br.ReadSingle(), // PV
                    br.ReadSingle() // HPV
                }).ToList();
            sk.UnkDefensiveRate = br.ReadInt32();
            sk.Unk5 = br.ReadByte();
            sk.ApplyNumber = br.ReadInt16();
            sk.BuffApplyRate = br.ReadInt32();
            sk.BuffData = Enumerable.Range(0, 10).Select(x => br.ReadUInt16()).ToList();
            sk.Description = br.ReadUnicodeStringBD();
            sk.FFFF = br.ReadInt16();
            sk.Unk6 = br.ReadInt16();
            sk.UsableInCooltime = br.ReadByte();
            sk.Unk7 = br.ReadByte();
            sk.IsExpiredInOffline = br.ReadByte();
            sk.Unk8 = br.ReadUInt16();
            return sk;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (Skill) Read(br)).ToList();
            entries = entries.OrderBy(x => x.SkillNo1).ToList();
            SkillsConverter.CreateXml(entries);
            SkillsConverter.CreateSql(entries);
        }
    }
}
