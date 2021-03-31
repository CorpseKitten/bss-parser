using System.IO;
using System.Linq;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("title")]
    public class Title : IBssEntry
    {
        public int Key { get; set; }
        public string Titlee { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Category { get; set; }
        public int Parameter1 { get; set; }
        public int Parameter2 { get; set; }
        public int Parameter3 { get; set; }
        public long AquiredExperiationDate { get; set; }
        public long ApplyExpirationDate { get; set; }
        public string TitleCondition { get; set; }
        public string EffectName { get; set; }
        public string ApplyColor { get; set; }
        
        public IBssEntry Read(BinaryReader br)
        {
            var s = Enumerable.Range(0, br.ReadInt32()).Select(x => br.ReadInt32()).ToList();
            var tt = new Title();
            tt.Key = br.ReadInt32();
            tt.Titlee = br.ReadUnicodeStringBD();
            tt.Description = br.ReadUnicodeStringBD();
            tt.Type = br.ReadInt32();
            tt.Category = br.ReadInt32();
            tt.Parameter1 = br.ReadInt32();
            tt.Parameter2 = br.ReadInt32();
            tt.Parameter3 = br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            tt.AquiredExperiationDate = br.ReadInt64();
            tt.ApplyExpirationDate = br.ReadInt64();
            tt.TitleCondition = br.ReadUnicodeStringBD();
            tt.EffectName = br.ReadAsciiStringBD();
            tt.ApplyColor = br.ReadAsciiStringBD();
            br.ReadInt32();
            return tt;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (Title) Read(br)).ToList();
            //throw new System.NotImplementedException();
        }
    }
}
