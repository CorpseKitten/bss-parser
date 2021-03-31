using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class PersonalityConverter
    {
        public static void CreateXml(List<NpcPersonality> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "Personality_Table"),
                from entry in Entries
                select
                    new XElement("data",
                        new XAttribute("Npc", entry.Npc),
                        new XAttribute("Theme1", entry.Themes.ElementAtOrDefault(0) != null ? entry.Themes[0].Theme : 0),
                        new XAttribute("NeedCount1", entry.Themes.ElementAtOrDefault(0) != null ? entry.Themes[0].NeedCount : 0),
                        new XAttribute("Theme2", entry.Themes.ElementAtOrDefault(1) != null ? entry.Themes[1].Theme : 0),
                        new XAttribute("NeedCount2", entry.Themes.ElementAtOrDefault(1) != null ? entry.Themes[1].NeedCount : 0),
                        new XAttribute("Theme3", entry.Themes.ElementAtOrDefault(2) != null ? entry.Themes[2].Theme : 0),
                        new XAttribute("NeedCount3", entry.Themes.ElementAtOrDefault(2) != null ? entry.Themes[2].NeedCount : 0),
                        new XAttribute("MinPv", entry.MinPv),
                        new XAttribute("MaxPv", entry.MaxPv),
                        new XAttribute("MinDv", entry.MinDv),
                        new XAttribute("MaxDv", entry.MaxDv),
                        new XAttribute("ZodiacSignIndexKey", entry.ZodiacSignIndexKey)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Personality_Table.xml"), xws))
                document.Save(xw);
        }
        public static void CreateSql(List<NpcPersonality> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("Personality_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Personality_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("Npc", entry.Npc);
                qb.Add("Theme1", entry.Themes.ElementAtOrDefault(0) != null ? entry.Themes[0].Theme : 0);
                qb.Add("NeedCount1", entry.Themes.ElementAtOrDefault(0) != null ? entry.Themes[0].NeedCount : 0);
                qb.Add("Theme2", entry.Themes.ElementAtOrDefault(1) != null ? entry.Themes[1].Theme : 0);
                qb.Add("NeedCount2", entry.Themes.ElementAtOrDefault(1) != null ? entry.Themes[1].NeedCount : 0);
                qb.Add("Theme3", entry.Themes.ElementAtOrDefault(2) != null ? entry.Themes[2].Theme : 0);
                qb.Add("NeedCount3", entry.Themes.ElementAtOrDefault(2) != null ? entry.Themes[2].NeedCount : 0);
                qb.Add("MinPv", entry.MinPv);
                qb.Add("MaxPv", entry.MaxPv);
                qb.Add("MinDv", entry.MinDv);
                qb.Add("MaxDv", entry.MaxDv);
                qb.Add("ZodiacSignIndexKey", entry.ZodiacSignIndexKey);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "Personality_Table.sql"), sb);
        }
    }
}
