using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("fieldboss")]
    public class FieldBoss : IBssEntry
    {
        public class DropData
        {
            public uint Rank { get; set; }
            public ushort DropGroupKey { get; set; }

            public static DropData Read(BinaryReader br)
            {
                return new DropData
                {
                    Rank = br.ReadUInt32(),
                    DropGroupKey = br.ReadUInt16()
                };
            }
        }

        public ushort MapKey { get; set; }
        public byte TypeKey { get; set; }
        public ushort CharacterKey { get; set; }
        public List<DropData> Type1Drops { get; set; }
        public List<DropData> Type2Drops { get; set; }
        public List<DropData> Type3Drops { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var fb = new FieldBoss();
            fb.MapKey = br.ReadUInt16();
            fb.TypeKey = br.ReadByte();
            fb.CharacterKey = br.ReadUInt16();
            fb.Type1Drops = Enumerable.Range(0, br.ReadInt32()).Select(x => DropData.Read(br)).ToList();
            fb.Type2Drops = Enumerable.Range(0, br.ReadInt32()).Select(x => DropData.Read(br)).ToList();
            fb.Type3Drops = Enumerable.Range(0, br.ReadInt32()).Select(x => DropData.Read(br)).ToList();
            return fb;
        }

        public void ReadTable(BinaryReader br)
        {
            var fieldBosses = Enumerable.Range(0, br.ReadInt32()).Select(x => (FieldBoss) Read(br)).ToList();
            FieldBossConverter.CreateSql(fieldBosses);
        }
    }
}
