using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class GuildSkillPointExperienceConverter
    {

        public static void CreateXml(List<GuildSkillPointExperience> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "GuildSkillPointEXP_Table"),
                    from entry in Entries
                    select
                    new XElement("data",
                        new XAttribute("Index", entry.Index),
                        new XAttribute("RequireEXP", entry.RequireEXP),
                        new XAttribute("RequireSkillExpLimit", entry.RequireSkillEXPLimit),
                        new XAttribute("AquiredPoint", entry.AquiredPoint)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "GuildSkillPointEXP_Table.xml"), xws))
                document.Save(xw);
        }

        public static void CreateSql(List<GuildSkillPointExperience> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("GuildSkillPointEXP_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("GuildSkillPointEXP_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("Index", entry.Index);
                qb.Add("RequireEXP", entry.RequireEXP);
                qb.Add("RequireSkillExpLimit", entry.RequireSkillEXPLimit);
                qb.Add("AquiredPoint", entry.AquiredPoint);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "GuildSkillPointEXP_Table.sql"), sb);
        }
    }
}
