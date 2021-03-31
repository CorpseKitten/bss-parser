using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class CollectConverter
    {
        public static void CreateSql(List<Collect> Entries)
        {
            var sb = new List<string> { };
            foreach (var entry in Entries)
            {
                var qb = new UpdateQueryBuilder("Collect_Table");
                qb.Add("Index", entry.Index);
                qb.Add("ProductDataType", entry.ProductDataType);
                qb.Add("NeedObjectSkillNo", 61);
                qb.Add("ProductSkillLevel", 1);
                qb.Add("ProductTime", entry.ProductTime);
                qb.Add("ProductSkillPointExp", 0);
                qb.Add("ProductAllHP", entry.ProductAllHp);
                qb.Add("ProductDeceraseHP", entry.ProductDecreaseHP);
                qb.Add("ItemDropID", entry.ItemDropId);
                if (entry.ToolType0 != 11)
                    qb.Add("ToolType_0", entry.ToolType0);
                if (entry.ItemDropId0 > 0)
                    qb.Add("ItemDropID_0", entry.ItemDropId0);
                if (entry.ToolType1 != 11)
                    qb.Add("ToolType_1", entry.ToolType1);
                if (entry.ItemDropId1 > 0)
                    qb.Add("ItemDropID_1", entry.ItemDropId1);
                if (entry.ToolType2 != 11)
                    qb.Add("ToolType_2", entry.ToolType2);
                if (entry.ItemDropId2 > 0)
                    qb.Add("ItemDropID_2", entry.ItemDropId2);
                qb.Add("ActionPoint", 1);
                qb.Add("SpawnDelayTime", entry.SpawnDelay);
                qb.Add("SpawnVariableTime", entry.SpawnVariableTime);
                sb.Add(qb.Combine("Index", entry.Index.ToString()));
            }

            File.WriteAllLines(Path.Combine("output.sql", "Collect_Table.sql"), sb);
        }

        public static void CreateXml(List<Collect> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("table", "Collect_Table"),
                    new XElement("keys", "Index"),
                    from entry in Entries.Select(x => (Collect)x)
                    select
                    new XElement("data",
                        new XAttribute("Index", entry.Index),
                        new XAttribute("ProductDataType", entry.ProductDataType),
                        new XAttribute("NeedObjectSkillNo", 61),
                        new XAttribute("ProductSkillLevel", 1),
                        new XAttribute("ProductTime", entry.ProductTime),
                        new XAttribute("ProductSkillPointExp", 0),
                        new XAttribute("ProductAllHP", entry.ProductAllHp),
                        new XAttribute("ProductDeceraseHP", entry.ProductDecreaseHP),
                        new XAttribute("ItemDropID", entry.ItemDropId),
                        new XAttribute("ToolType_0", entry.ToolType0),
                        new XAttribute("ItemDropID_0", entry.ItemDropId0),
                        new XAttribute("ToolType_1", entry.ToolType1),
                        new XAttribute("ItemDropID_1", entry.ItemDropId1),
                        new XAttribute("ToolType_2", entry.ToolType2),
                        new XAttribute("ItemDropID_2", entry.ItemDropId2),
                        new XAttribute("ActionPoint", 1),
                        new XAttribute("SpawnDelayTime", entry.SpawnDelay),
                        new XAttribute("SpawnVariableTime", entry.SpawnVariableTime)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Collect_Table.xml"), xws))
                document.Save(xw);
        }
    }
}
