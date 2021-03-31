using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class CharacterStaticConverter
    {
        public static void CreateSql(List<CharacterStatic> Entries)
        {
            var sb = new List<string> { };
            foreach (var entry in Entries)
            {
                var qb = new UpdateQueryBuilder("Character_Table");
                qb.Add("HP", entry.Health);
                qb.Add("MP", entry.Mana);
                //qb.Add("HPRegen", entry.Health);
                if (entry.CharKind < 3)
                    qb.Add("DropID", entry.DropId);
                else
                    qb.Add("DropID", null);
                qb.Add("variedTendencyOnDead", entry.VariedTendencyOnDead);
                sb.Add(qb.Combine("Index", entry.Index.ToString()));
            }

            File.WriteAllLines(Path.Combine("output.sql", "Character_Table.sql"), sb);
        }
        public static void CreateXml(List<CharacterStatic> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("table", "Character_Table"),
                    new XElement("keys", "Index"),
                from entry in Entries.Select(x => (CharacterStatic)x)
                select
                    new XElement("data",
                        new XAttribute("Index", entry.Index),
                        new XAttribute("HP", entry.Health),
                        new XAttribute("MP", entry.Mana)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Character_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
