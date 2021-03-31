using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("lifeexp")]
    public class LifeExp : IBssEntry
    {
        public byte Type { get; set; }
        public int Level { get; set; }
        public long Experience { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new LifeExp
            {
                Type = br.ReadByte(),
                Level = br.ReadInt32(),
                Experience = br.ReadInt64()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var table = new List<IBssEntry>();

            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(col =>
            {
                Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(row =>
                {
                    table.Add(Read(br));
                });
            });
            
            // Do whatever you want with the table
        }
    }
}
