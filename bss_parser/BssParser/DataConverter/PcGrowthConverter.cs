using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class PcGrowthConverter
    {
        public static void CreateXml(List<PcGrowth> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("clearTable", true),
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "PC_Set_Table"),
                from entry in Entries
                select
                    new XElement("data",
                        new XAttribute("ClassType", entry.ClassType),
                        new XAttribute("ClassName", entry.ClassName),
                        new XAttribute("CharacterKey", entry.CharacterKey),
                        new XAttribute("BackgroundCharacterKey", entry.BackgroundCharacterKey),
                        new XAttribute("AlteregoKey", entry.AlteregoKey),
                        new XAttribute("WorkerCharacterKey", entry.WorkerCharacterKey),
                        new XAttribute("ParentsClass", ""),
                        new XAttribute("PossessableWeight", 300000000),
                        new XAttribute("DefaultItemList", string.Join(",", entry.DefaultItemList)),
                        new XAttribute("StartPosition", string.Join(",", entry.StartPosition)),
                        new XAttribute("StartDirection", entry.StartDirection),
                        new XAttribute("WeaponEnduranceDecreseRate", entry.WeaponEnduranceDecreaseRate),
                        new XAttribute("ArmorEnduranceDecreseRate", entry.ArmorEnduranceDecreaseRate),
                        new XAttribute("ArmorEnduranceArrowDecreseRate", entry.ArmorEnduranceArrowDecreaseRate),
                        new XAttribute("FieldNo", entry.FieldNo),
                        new XAttribute("StartWayPointNo", entry.StartWaypointNo),
                        new XAttribute("CombatResourceType", entry.CombatResourceType),
                        new XAttribute("ClassMovie", entry.ClassMovie),
                        new XAttribute("ClassDesc", entry.ClassDescription),
                        new XAttribute("ResourceSaveEffect_Lv1", entry.ResourceSaveEffect[0]),
                        new XAttribute("ResourceSaveEffect_Lv2", entry.ResourceSaveEffect[1]),
                        new XAttribute("ResourceSaveEffect_Lv3", entry.ResourceSaveEffect[2]),
                        new XAttribute("ResourceSaveEffect_Lv4", entry.ResourceSaveEffect[3]),
                        new XAttribute("ComboColor", string.Join(",", entry.ComboColor)),
                        new XAttribute("AwakenComboColor", string.Join(",", entry.AwakenComboColor)),
                        new XAttribute("MainAttackType", entry.MainAttackType),
                        new XAttribute("DefaultVisibleWeapon", entry.DefaultVisibleWeapon),
                        new XAttribute("DefaultVisibleSubWeapon", entry.DefaultVisibleSubWeapon),
                        new XAttribute("DefaultVisibleAwakenWeapon", entry.DefaultVisibleAwakenWeapon),
                        new XAttribute("IsOpen", entry.IsOpen)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "PC_Set_Table.xml"), xws))
                document.Save(xw);
        }
        public static void CreateSql(List<PcGrowth> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("PC_Set_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("PC_Set_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("ClassType", entry.ClassType);
                qb.Add("ClassName", entry.ClassName);
                qb.Add("CharacterKey", entry.CharacterKey);
                qb.Add("BackgroundCharacterKey", entry.BackgroundCharacterKey);
                qb.Add("AlteregoKey", entry.AlteregoKey);
                qb.Add("WorkerCharacterKey", entry.WorkerCharacterKey);
                qb.Add("ParentsClass", "");
                qb.Add("PossessableWeight", 300000000);
                qb.Add("DefaultItemList", string.Join(",", entry.DefaultItemList));
                qb.Add("StartPosition", string.Join(",", entry.StartPosition));
                qb.Add("StartDirection", entry.StartDirection);
                qb.Add("WeaponEnduranceDecreseRate", entry.WeaponEnduranceDecreaseRate);
                qb.Add("ArmorEnduranceDecreseRate", entry.ArmorEnduranceDecreaseRate);
                qb.Add("ArmorEnduranceArrowDecreseRate", entry.ArmorEnduranceArrowDecreaseRate);
                qb.Add("FieldNo", entry.FieldNo);
                qb.Add("StartWayPointNo", entry.StartWaypointNo);
                qb.Add("CombatResourceType", entry.CombatResourceType);
                qb.Add("ClassMovie", entry.ClassMovie);
                qb.Add("ClassDesc", entry.ClassDescription);
                qb.Add("ResourceSaveEffect_Lv1", entry.ResourceSaveEffect[0]);
                qb.Add("ResourceSaveEffect_Lv2", entry.ResourceSaveEffect[1]);
                qb.Add("ResourceSaveEffect_Lv3", entry.ResourceSaveEffect[2]);
                qb.Add("ResourceSaveEffect_Lv4", entry.ResourceSaveEffect[3]);
                qb.Add("ComboColor", string.Join(",", entry.ComboColor));
                qb.Add("AwakenComboColor", string.Join(",", entry.AwakenComboColor));
                qb.Add("MainAttackType", entry.MainAttackType);
                qb.Add("DefaultVisibleWeapon", entry.DefaultVisibleWeapon);
                qb.Add("DefaultVisibleSubWeapon", entry.DefaultVisibleSubWeapon);
                qb.Add("DefaultVisibleAwakenWeapon", entry.DefaultVisibleAwakenWeapon);
                qb.Add("IsOpen", entry.IsOpen);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "PC_Set_Table.sql"), sb);
        }
    }
}
