using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;
namespace BssParser.BssData
{
    [BssTable("itemenchant")]
    public class ItemEnchant : IBssEntry
    {
        public ushort ItemId { get; set; }
        public short EnchantLevel { get; set; }
        public string ItemName { get; set; }
        public byte ItemType { get; set; }
        public byte ItemClassify { get; set; }
        public byte GradeType { get; set; }
        public byte EquipType { get; set; }
        public byte[] OccupiedEquipType { get; set; }
        public byte[] OccupiedEquipNo { get; set; }
        public byte ItemMaterial { get; set; }
        public int Weight { get; set; }
        public int ExpirationPeriod { get; set; }
        public byte VestedType { get; set; }
        public byte[] ItemTableAllowedClasses { get; set; }
        public byte[] ItemTableAllowedPets { get; set; }
        public byte ItemAccessLevel { get; set; }
        public long OriginalPrice { get; set; }
        public long SellPriceToNpc { get; set; }
        public int[] OriginalPriceRate { get; set; }
        public int[] PriceRate { get; set; }
        public long RepairPrice { get; set; }
        public long RepairTime { get; set; }
        public int MaxestPercentForItemMarket { get; set; }
        public int MinestPercentForItemMarket { get; set; }
        public long BasePriceForItemMarket { get; set; }
        public short ContentsEventType { get; set; }
        public int ContentsEventParam1 { get; set; }
        public int ContentsEventParam2 { get; set; }
        public byte DoLogging { get; set; }
        public short CharacterKey { get; set; }
        public string UseCondition { get; set; }
        public string IconImageFile { get; set; }
        public string Description { get; set; }
        public short EnduranceLimit { get; set; }
        public List<Tuple<string, float>> OffensiveStats { get; set; }
        public List<float[]> DefensiveStats { get; set; }
        public int UnkDefensiveRate { get; set; }
        public List<ushort> SkillIds { get; set; }
        public ushort EnchantSkillNo { get; set; }
        
        public float VariedRecovMP { get; set; }
        public int VariedMaxMP { get; set; }
        public float VariedRecovHP { get; set; }
        public int VariedMaxHP { get; set; }
        public int VariedCriticalRate { get; set; }
        public int VariedDropItemRate { get; set; }
        public int VariedFishingSpeedRate { get; set; }
        public int VariedCollectionSpeedRate { get; set; }
        public int VariedMoveSpeedRate { get; set; }
        public int VariedAttackSpeedRate { get; set; }
        public int VariedCastingSpeedRate { get; set; }
        public long VariedPossessableWeight { get; set; }
        public int VariedSuspension { get; set; }
        
        public IBssEntry Read(BinaryReader br)
        {
            var item = new ItemEnchant();
            item.ItemId = br.ReadUInt16();
            item.EnchantLevel = br.ReadInt16();
            item.ItemName = br.ReadUnicodeStringBD();
            item.ItemType = br.ReadByte();
            item.ItemClassify = br.ReadByte();
            item.GradeType = br.ReadByte();
            item.EquipType = br.ReadByte();
            br.ReadBytes(8);
            item.OccupiedEquipType = br.ReadBytes(0x1F);
            item.OccupiedEquipNo = br.ReadBytes(0x1F);
            br.ReadByte();
            item.ItemMaterial = br.ReadByte();
            item.Weight = br.ReadInt32();
            br.ReadByte();
            br.ReadByte();
            item.ExpirationPeriod = br.ReadInt32();
            item.VestedType = br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            item.ItemTableAllowedClasses = br.ReadBytes(0x20);
            item.ItemTableAllowedPets = br.ReadBytes(0x13);
            br.ReadInt32();
            br.ReadInt32();
            br.ReadByte();
            br.ReadInt32();
            br.ReadByte();
            item.ItemAccessLevel = br.ReadByte();
            item.OriginalPrice = br.ReadInt64();
            item.SellPriceToNpc = br.ReadInt64();
            item.OriginalPriceRate = Enumerable.Range(0, 3).Select(x => br.ReadInt32()).ToArray();
            item.PriceRate = Enumerable.Range(0, 3).Select(x => br.ReadInt32()).ToArray();
            item.RepairPrice = br.ReadInt64();
            item.RepairTime = br.ReadInt64();
            br.ReadInt64();
            br.ReadInt64();
            br.ReadInt64();
            br.ReadInt64();
            item.MaxestPercentForItemMarket = br.ReadInt32();
            item.MinestPercentForItemMarket = br.ReadInt32();
            item.BasePriceForItemMarket = br.ReadInt64();
            br.ReadInt16();
            br.ReadByte();
            br.ReadBytes(16 * (int) br.ReadInt64());
            br.ReadByte();
            br.ReadByte();
            br.ReadInt32();
            br.ReadInt32();
            item.ContentsEventType = br.ReadInt16();
            item.ContentsEventParam1 = br.ReadInt32();
            item.ContentsEventParam2 = br.ReadInt32();
            br.ReadByte();
            br.ReadByte();
            br.ReadInt64();
            br.ReadInt16();
            br.ReadInt32();
            br.ReadByte();
            br.ReadInt16();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            item.DoLogging = br.ReadByte();
            item.CharacterKey = br.ReadInt16();
            br.ReadInt32();
            item.UseCondition = br.ReadUnicodeStringBD();
            br.ReadInt32();
            item.IconImageFile = br.ReadUnicodeStringBD();
            item.Description = br.ReadUnicodeStringBD();
            br.ReadUnicodeStringBD();
            br.ReadInt32();
            br.ReadUnicodeStringBD();
            br.ReadUnicodeStringBD();
            br.ReadByte();
            br.ReadBytes(16 * (int) br.ReadInt64());
            br.ReadBytes(2 * (int) br.ReadInt64());
            br.ReadBytes(4 * (int) br.ReadInt64());
            br.ReadByte();
            br.ReadInt16();
            br.ReadByte();
            br.ReadInt16();
            br.ReadInt16();
            br.ReadByte();
            br.ReadInt32();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadInt64();
            br.ReadInt64();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadByte();
            br.ReadInt32();
            br.ReadInt64();
            item.EnduranceLimit = br.ReadInt16();
            br.ReadByte();
            br.ReadInt32();
            br.ReadInt16();
            br.ReadInt16();

            br.ReadInt32();
            var hasStats = br.ReadByte(); // has intem enchant
            if (hasStats > 0)
            {
                br.ReadInt16(); // need enchant item
                br.ReadInt16(); // enchant
                br.ReadInt16(); // enchant item
                br.ReadInt16(); // echant
                br.ReadInt64(); // erfect item count
                br.ReadInt64(); // black count for market
                var blackCount = br.ReadInt64();
                br.ReadInt64(); // unk
                br.ReadInt32(); // success rate
                br.ReadInt32(); // add rate
                br.ReadInt32(); // unk
                br.ReadInt16(); // limit
                br.ReadInt16(); // reduce max at fail
                br.ReadInt16(); // recover max
                br.ReadInt16(); // recover max at perfect

                item.VariedRecovHP = br.ReadSingle(); // varied recov hp
                item.VariedMaxHP = br.ReadInt32(); // varied max hp
                item.VariedRecovHP = br.ReadSingle(); // varied recov hp
                item.VariedMaxHP = br.ReadInt32(); // varied max hp
                br.ReadBytes(0x28); // float array
                item.VariedCriticalRate = br.ReadInt32(); // varied crit
                item.VariedDropItemRate = br.ReadInt32(); // varied drop
                item.VariedFishingSpeedRate = br.ReadInt32(); // varied fishing
                item.VariedCollectionSpeedRate = br.ReadInt32(); // varied collection
                item.VariedMoveSpeedRate = br.ReadInt32();
                item.VariedAttackSpeedRate = br.ReadInt32();
                item.VariedCastingSpeedRate = br.ReadInt32();
                item.VariedPossessableWeight = br.ReadInt64(); // possessable weight
                item.VariedSuspension = br.ReadInt32();
                br.ReadInt32();
                br.ReadByte(); // socket
                br.ReadBytes(0xC);
                br.ReadInt32();
                br.ReadInt32();
                br.ReadBytes(0xC);
                br.ReadBytes(0xC);
                br.ReadBytes(0xC);
                br.ReadBytes(0xC);
                br.ReadBytes(0xC);
                br.ReadUInt16();
                item.EnchantSkillNo = br.ReadUInt16();
                item.Description = br.ReadUnicodeStringBD();
                br.ReadUnicodeStringBD();
                br.ReadByte();
                br.ReadByte();
                br.ReadInt32();
                br.ReadInt32();
                br.ReadInt16();
                br.ReadByte();
                item.OffensiveStats =
                    Enumerable.Range(0, 3)
                        .Select(x => new Tuple<string, float>(br.ReadUnicodeStringBD(), br.ReadSingle()))
                        .ToList();
                item.DefensiveStats = Enumerable.Range(0, 3).Select(x =>
                    new[]
                    {
                        br.ReadSingle(), // DV
                        br.ReadSingle(), // HDV
                        br.ReadSingle(), // PV
                        br.ReadSingle() // HPV
                    }).ToList();
                br.ReadInt32();
            }

            var hasMarket = br.ReadByte();
            if (hasMarket > 0)
            {
                item.SkillIds = new List<ushort>(); // 64 * 2
                for (var i = 0; i < 32; ++i)
                {
                    br.ReadInt16();
                    var num = br.ReadUInt16();
                    if (num > 0)
                        item.SkillIds.Add(num);
                }
                br.ReadBytes(32 * 2);
                br.ReadBytes(38);
                br.ReadBytes(76);
                br.ReadByte();
                br.ReadByte();
                br.ReadInt16();
            }
            else
                br.ReadByte();
            return item;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (ItemEnchant)Read(br)).ToList();
            entries = entries.OrderBy(x => x.ItemId).ThenBy(x => x.EnchantLevel).ToList();
            ItemEnchantConverter.CreateSql(entries, "1_LongSword");
            ItemEnchantConverter.CreateSql(entries, "2_Blunt");
            ItemEnchantConverter.CreateSql(entries, "3_TwoHandSword");
            ItemEnchantConverter.CreateSql(entries, "4_Bow");
            ItemEnchantConverter.CreateSql(entries, "5_Dagger");
            ItemEnchantConverter.CreateSql2(entries, "Item_Table");
        }
    }
}
