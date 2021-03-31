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
    [BssTable("actionexp")]
    public class ActionExp : IBssEntry
    {
        public int Level { get; set; }
        public byte Class { get; set; }
        public long Quest { get; set; }
        public long Collect { get; set; }
        public long Manufacture { get; set; }
        public long Alchemy { get; set; }
        public long Fishing { get; set; }
        public long HorseTaming { get; set; }
        public long Study { get; set; }
        public long Trade { get; set; }
        public long Farming { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            br.ReadInt32(); // Level
            return new ActionExp
            {
                Class = br.ReadByte(),
                Level = br.ReadInt32(),
                Quest = br.ReadInt64(),
                Collect = br.ReadInt64(),
                Manufacture = br.ReadInt64(),
                Alchemy = br.ReadInt64(),
                Fishing = br.ReadInt64(),
                HorseTaming = br.ReadInt64(),
                Study = br.ReadInt64(),
                Trade = br.ReadInt64(),
                Farming = br.ReadInt64()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = new List<ActionExp>();
            var classCount = br.ReadInt32();
            for (var i = 0; i < classCount; ++i)
                entries.AddRange(Enumerable.Range(0, br.ReadInt32()).Select(x => (ActionExp)Read(br)).ToList());
            entries = entries.OrderBy(x => x.Class).ThenBy(x => x.Level).ToList();
            ActionExpConverter.CreateXml(entries);
            ActionExpConverter.CreateSql(entries);
        }
    }
}
