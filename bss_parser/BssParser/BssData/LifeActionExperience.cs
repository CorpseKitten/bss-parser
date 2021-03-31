using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("lifeactionexperience")]
    public class LifeActionExperience : IBssEntry
    {
        public int ItemKey { get; set; }
        public byte LifeActionType0 { get; set; }
        public byte LifeActionType1 { get; set; }
        public long Exp0 { get; set; }
        public long Exp1 { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            br.ReadInt32();
            return new LifeActionExperience
            {
                ItemKey = br.ReadInt32(),
                LifeActionType0 = br.ReadByte(),
                LifeActionType1 = br.ReadByte(),
                Exp0 = br.ReadInt64(),
                Exp1 = br.ReadInt64()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (LifeActionExperience)Read(br)).ToList();
            entries = entries.OrderBy(x => x.ItemKey).ToList();
            LifeActionExperienceConverter.CreateXml(entries);
            LifeActionExperienceConverter.CreateSql(entries);
        }
    }
}
