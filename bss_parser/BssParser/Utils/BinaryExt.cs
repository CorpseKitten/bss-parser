using System.IO;
using System.Text;

namespace BssParser.Utils
{
    public static class BinaryExt
    {
        public static string ReadUnicodeStringBD(this BinaryReader br)
        {
            var len = br.ReadInt64(); // for test..
            return Encoding.Unicode.GetString(br.ReadBytes((int) len * 2));
        }
        public static string ReadAsciiStringBD(this BinaryReader br)
        {
            return Encoding.ASCII.GetString(br.ReadBytes((int)br.ReadInt64()));
        }
    }
}
