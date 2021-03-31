using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class ItemExchangeSourceConverter
    {
        public static void CreateSql(List<ItemExchangeSource> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("ItemExchangeSource") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("ItemExchangeSource");
                qb.Add("ID", sb.Count + 1);
                qb.Add("Index", entry.Index);
                qb.Add("ExchangeType", entry.ExchangeType);
                qb.Add("ProductDataType", entry.ProductDataType);
                qb.Add("CraftingTime", entry.CraftingTime);
                qb.Add("CraftingPublicNeedItemID", entry.CraftingPublic.Item1);
                qb.Add("CraftingPublicNeedCount", entry.CraftingPublic.Item2);
                qb.Add("CraftingPrivateNeedItemID", entry.CraftingPrivate.Item1);
                qb.Add("CraftingPrivateNeedCount", entry.CraftingPrivate.Item2);
                qb.Add("ItemIndex0", entry.MainItem.Item1);
                qb.Add("ItemCount0", entry.MainItem.Item2);
                for (var i = 1; i <= 5; ++i)
                {
                    if (entry.ItemList.ElementAtOrDefault(i) == null)
                        continue;
                    qb.Add("ItemIndex" + i, entry.ItemList.ElementAtOrDefault(i)?.Item1);
                    qb.Add("ItemCount" + i, entry.ItemList.ElementAtOrDefault(i)?.Item2);
                }
                qb.Add("ItemDropID", entry.ItemDropID);
                qb.Add("NpcWorkerDropIDForLuck", entry.NpcWorkerDropIDForLuck);
                qb.Add("CraftCharacterKey", entry.CraftCharacterKey);
                qb.Add("ItemExchangeRepatGroupKey", string.Join(",", entry.ItemExchangeRepeatGroup));
                qb.Add("WorkAniType", entry.WorkAniType);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "ItemExchangeSource.sql"), sb);
        }
        public static void CreateXml(List<ItemExchangeSource> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "ItemExchangeSource"),
                new XElement("keys", "Index"),
                from entry in Entries
                select
                    new XElement("data",
                        new XAttribute("Index", entry.Index),
                        new XAttribute("ExchangeType", entry.ExchangeType),
                        new XAttribute("ProductDataType", entry.ProductDataType),
                        new XAttribute("CraftingTime", entry.CraftingTime),
                        new XAttribute("CraftingPublicNeedItemID", entry.CraftingPublic.Item1),
                        new XAttribute("CraftingPublicNeedCount", entry.CraftingPublic.Item2),
                        new XAttribute("CraftingPrivateNeedItemID", entry.CraftingPrivate.Item1),
                        new XAttribute("CraftingPrivateNeedCount", entry.CraftingPrivate.Item2),
                        new XAttribute("ItemIndex0", entry.MainItem.Item1),
                        new XAttribute("ItemCount0", entry.MainItem.Item2),
                        entry.ItemList.ElementAtOrDefault(1) != null ? new XAttribute("ItemIndex1", entry.ItemList[1].Item1) : null,
                        entry.ItemList.ElementAtOrDefault(1) != null ? new XAttribute("ItemCount1", entry.ItemList[1].Item2) : null,
                        entry.ItemList.ElementAtOrDefault(2) != null ? new XAttribute("ItemIndex2", entry.ItemList[2].Item1) : null,
                        entry.ItemList.ElementAtOrDefault(2) != null ? new XAttribute("ItemCount2", entry.ItemList[2].Item2) : null,
                        entry.ItemList.ElementAtOrDefault(3) != null ? new XAttribute("ItemIndex3", entry.ItemList[3].Item1) : null,
                        entry.ItemList.ElementAtOrDefault(3) != null ? new XAttribute("ItemCount3", entry.ItemList[3].Item2) : null,
                        entry.ItemList.ElementAtOrDefault(4) != null ? new XAttribute("ItemIndex4", entry.ItemList[4].Item1) : null,
                        entry.ItemList.ElementAtOrDefault(4) != null ? new XAttribute("ItemCount4", entry.ItemList[4].Item2) : null,
                        entry.ItemList.ElementAtOrDefault(5) != null ? new XAttribute("ItemIndex5", entry.ItemList[5].Item1) : null,
                        entry.ItemList.ElementAtOrDefault(5) != null ? new XAttribute("ItemCount5", entry.ItemList[5].Item2) : null,
                        new XAttribute("ItemDropID", entry.ItemDropID),
                        new XAttribute("NpcWorkerDropIDForLuck", entry.NpcWorkerDropIDForLuck),
                        new XAttribute("CraftCharacterKey", entry.CraftCharacterKey),
                        new XAttribute("ItemExchangeRepatGroupKey", string.Join(",", entry.ItemExchangeRepeatGroup)),
                        new XAttribute("WorkAniType", entry.WorkAniType)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "ItemExchangeSource.xml"), xws))
                document.Save(xw);
        }
    }
}
