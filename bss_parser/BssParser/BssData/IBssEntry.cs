using System.IO;

namespace BssParser.BssData
{
    public interface IBssEntry
    {
        IBssEntry Read(BinaryReader br);
        void ReadTable(BinaryReader br);
    }
}
