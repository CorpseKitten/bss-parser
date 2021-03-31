using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("repairmaxendurance")]
    public class RepairMaxEndurance : IBssEntry
    {
        public class RepairItem
        {
            public ushort ItemId { get; set; }
            public short RepairCount { get; set; }
            public long NeedMoney { get; set; }

            public static RepairItem Read(BinaryReader br)
            {
                var ret = new RepairItem();
                ret.ItemId = br.ReadUInt16();
                br.ReadInt16();
                ret.RepairCount = br.ReadInt16();
                ret.NeedMoney = br.ReadInt64();
                return ret;
            }
        }

        public ushort BaseItem { get; set; }
        public ushort EnchantLevel { get; set; }
        public List<RepairItem> RepairItems { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var ret = new RepairMaxEndurance();
            br.ReadInt32();
            ret.BaseItem = br.ReadUInt16();
            ret.EnchantLevel = br.ReadUInt16();
            br.ReadInt64();
            br.ReadInt16();
            ret.RepairItems = Enumerable.Range(0, br.ReadInt32()).Select(x => RepairItem.Read(br)).ToList();
            return ret;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (RepairMaxEndurance) Read(br)).ToList();
            entries = entries.OrderBy(x => x.BaseItem).Where(x => x.EnchantLevel == 0).ToList();
            //entries = entries.OrderBy(x => x.BaseItem).ThenBy(x => x.EnchantLevel).ToList();
            RepairMaxEnduranceConverter.CreateSql(entries);
        }
    }
}
