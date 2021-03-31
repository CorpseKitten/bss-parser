using System.IO;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("productexperience")]
    public class ProductExperience : IBssEntry
    {
        public ushort Index { get; set; }
        public uint ProductExperienceEXP { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var pr = new ProductExperience();
            pr.Index = br.ReadUInt16();
            pr.ProductExperienceEXP = br.ReadUInt32();
            br.ReadByte();
            return pr;
        }

        public void ReadTable(BinaryReader br)
        {
            //throw new System.NotImplementedException();
        }
    }
}
