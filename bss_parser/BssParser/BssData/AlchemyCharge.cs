using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("alchemycharge")]
    public class AlchemyCharge : IBssEntry
    {
        public int ItemKey { get; set; }
        public int Type { get; set; }
        public int ItemKey2 { get; set; }
        public int ChargePoint { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            return new AlchemyCharge
            {
                ItemKey = br.ReadInt32(),
                Type = br.ReadInt32(),
                ItemKey2 = br.ReadInt32(),
                ChargePoint = br.ReadInt32()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = new List<AlchemyCharge>();

            var unkCount = br.ReadInt32();
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x => { entries.Add((AlchemyCharge)Read(br)); });
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x => { entries.Add((AlchemyCharge)Read(br)); });
            Enumerable.Range(0, br.ReadInt32()).ToList().ForEach(x => { entries.Add((AlchemyCharge)Read(br)); });

            //foreach(var entry in entries)
            //    Console.WriteLine("ItemKey: {0} Type: {1} ChargePoints: {2}", entry.ItemKey, entry.Type, entry.ChargePoint);
        }
    }
}
