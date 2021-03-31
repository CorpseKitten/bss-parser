using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class BuffConverter
    {
        public static void CreateSql(List<Buff> Entries)
        {
            var sb = new List<string>();
            foreach (var entry in Entries)
            {
                var qb = new UpdateQueryBuilder("Buff_Table");
                qb.Add("Index", entry.BuffId);
                //qb.Add("BuffName", entry.BuffName);
                qb.Add("Level", entry.Level);
                //qb.Add("Group", entry.Group);
                //qb.Add("ConditionType", entry.ConditionType);
                //qb.Add("ModuleType", entry.ModuleType);
                //qb.Add("BuffType", entry.BuffType);
                qb.Add("ApplyRate", entry.ApplyRate);
                qb.Add("ValidityTime", entry.ValidityTime);
                qb.Add("RepeatTime", entry.RepeatTime);
                qb.Add("NeedEquipType", entry.NeedEquipType);
                for (var i = 0; i < 10; ++i)
                    //if (entry.UsedParameters[i])
                        qb.Add("Param" + i, entry.Parameters[i]);
                    //else
                    //    qb.Add("Param" + i, null);
                //qb.Add("BuffEffect", entry.BuffEffect);
                //qb.Add("IsDisplay", entry.IsDisplay);
                sb.Add(qb.Combine("Index", entry.BuffId.ToString()));
            }

            File.WriteAllLines(Path.Combine("output.sql", "Buff_Table.sql"), sb);
            CreateSql2(Entries);
        }

        public static void CreateSql2(List<Buff> Entries)
        {
            var sb = new List<string>();
            foreach (var entry in Entries)
            {
                var qb = new UpdateQueryBuilder("Buff_Table");
                qb.Add("ApplyToGroup", entry.BuffType == 0 ? 1 : 0);
                sb.Add(qb.Combine("Index", entry.BuffId.ToString()));
            }

            File.WriteAllLines(Path.Combine("output.sql", "Buff_Table_Test.sql"), sb);
        }

        public static void CreateXml(List<Buff> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("table", "Buff_Table"),
                    new XElement("keys", "Index"),
                from entry in Entries.Select(x => (Buff)x)
                select
                    new XElement("data",
                        new XAttribute("Index", entry.BuffId),
                        new XAttribute("BuffName", entry.BuffName),
                        new XAttribute("Level", entry.Level),
                        new XAttribute("Group", entry.Group),
                        new XAttribute("ConditionType", entry.ConditionType),
                        new XAttribute("ModuleType", entry.ModuleType),
                        new XAttribute("BuffType", entry.BuffType),
                        new XAttribute("ApplyRate", entry.ApplyRate),
                        new XAttribute("ValidityTime", entry.ValidityTime),
                        new XAttribute("RepeatTime", entry.RepeatTime),
                        new XAttribute("NeedEquipType", entry.NeedEquipType),
                        new XAttribute("Param0", entry.Parameters[0]),
                        new XAttribute("Param1", entry.Parameters[1]),
                        new XAttribute("Param2", entry.Parameters[2]),
                        new XAttribute("Param3", entry.Parameters[3]),
                        new XAttribute("Param4", entry.Parameters[4]),
                        new XAttribute("Param5", entry.Parameters[5]),
                        new XAttribute("Param6", entry.Parameters[6]),
                        new XAttribute("Param7", entry.Parameters[7]),
                        new XAttribute("Param8", entry.Parameters[8]),
                        new XAttribute("Param9", entry.Parameters[9]),
                        new XAttribute("BuffEffect", entry.BuffEffect),
                        new XAttribute("IsDisplay", entry.IsDisplay)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Buff_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
