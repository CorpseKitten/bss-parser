using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class QuestConverter
    {
        public static void CreateSql(List<Quest> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("Quest_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Quest_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("QuestGroup", entry.QuestGroup);
                qb.Add("QuestID", entry.QuestId);
                qb.Add("SelectType", entry.SelectType);
                qb.Add("QuestType", entry.QuestType);
                qb.Add("isUserBaseQuest", entry.IsUserBaseQuest);
                qb.Add("RemoveQuest", $"{entry.QuestGroup},{entry.QuestId}");
                qb.Add("UnableToGiveup", 0); // TODO: FIND ME!
                qb.Add("DoGuide", entry.DoGuide);
                qb.Add("Region", entry.Region);
                qb.Add("IsEscortQuest", entry.IsEscortQuest);
                qb.Add("Title", entry.Title);
                qb.Add("Desc", ""); //entry.Description too big ok
                qb.Add("RepeatTime", entry.RepeatTime);
                qb.Add("OrderNpc", entry.OrderNpc);
                qb.Add("OrderNpcDialogIndex", entry.OrderNpcDialogIndex);
                qb.Add("RecommendLevel", entry.RecommendedLevel);
                qb.Add("AcceptConditions", entry.AcceptCondition);
                qb.Add("AcceptPushItem", 0); // TODO: AcceptPushItem?
                qb.Add("CompleteNpc", entry.CompleteNpc);
                qb.Add("CompleteNpcDialogIndex", entry.CompleteNpcDialogIndex);
                qb.Add("CompleteDisplay", entry.CompleteDisplay);
                qb.Add("QuestIcon", entry.QuestIcon);
                qb.Add("GuideForce", entry.GuideForce);
                qb.Add("GuideAutoErase", entry.GuideAutoErase);
                qb.Add("PositionAndRadius", GetPosition(entry.PositionAndRadiusData));
                qb.Add("CompleteCondition", entry.CompleteCondition);
                qb.Add("CompleteConditionText", entry.CompleteConditionText);
                qb.Add("AddDrop", 0); // TODO: AddDrop?
                qb.Add("ExpectedPrice", entry.ExpectedPrice);
                qb.Add("BaseReward", GetReward(entry.Rewards[0], entry.QuestGroup, entry.QuestId));
                qb.Add("SelectReward", GetReward(entry.Rewards[1], entry.QuestGroup, entry.QuestId));
                qb.Add("AcceptDialog", 0); // TODO
                qb.Add("NotAcceptDialog", 0); // TODO
                qb.Add("AcceptButton", 0); // TODO
                qb.Add("AcceptDungeon", 0); // TODO
                qb.Add("ProgressDialog", entry.ProgressDialog);
                qb.Add("RetryButton", 0); // TODO
                qb.Add("CompleteDialog", entry.CompleteDialog);
                qb.Add("ContentsGroupKey", 0);
                sb.Add(qb.Combine());
            }

            File.WriteAllLines(Path.Combine("output.sql", "Quest_Table.sql"), sb);
        }

        public static void CreateXml(List<Quest> Entries)
        {
            var document = new XDocument(
                new XElement("datas",
                    new XAttribute("method", "insert"),
                    new XAttribute("table", "Quest_Table"),
                    from entry in Entries.Select(x => (Quest)x)
                    select
                    new XElement("data",
                        new XAttribute("QuestGroup", entry.QuestGroup),
                        new XAttribute("QuestID", entry.QuestId),
                        new XAttribute("SelectType", entry.SelectType),
                        new XAttribute("QuestType", entry.QuestType),
                        new XAttribute("isUserBaseQuest", entry.IsUserBaseQuest),
                        new XAttribute("RemoveQuest", $"{entry.QuestGroup},{entry.QuestId}"),
                        new XAttribute("UnableToGiveup", 0), // TODO: FIND ME!
                        new XAttribute("DoGuide", entry.DoGuide),
                        new XAttribute("Region", entry.Region),
                        new XAttribute("IsEscortQuest", entry.IsEscortQuest),
                        new XAttribute("Title", entry.Title),
                        new XAttribute("Desc", entry.Description),
                        new XAttribute("RepeatTime", entry.RepeatTime),
                        new XAttribute("OrderNpc", entry.OrderNpc),
                        new XAttribute("OrderNpcDialogIndex", entry.OrderNpcDialogIndex),
                        new XAttribute("RecommendLevel", entry.RecommendedLevel),
                        new XAttribute("AcceptConditions", entry.AcceptCondition),
                        new XAttribute("AcceptPushItem", 0), // TODO: AcceptPushItem?
                        new XAttribute("CompleteNpc", entry.CompleteNpc),
                        new XAttribute("CompleteNpcDialogIndex", entry.CompleteNpcDialogIndex),
                        new XAttribute("CompleteDisplay", entry.CompleteDisplay),
                        new XAttribute("QuestIcon", entry.QuestIcon),
                        new XAttribute("GuideForce", entry.GuideForce),
                        new XAttribute("GuideAutoErase", entry.GuideAutoErase),
                        new XAttribute("PositionAndRadius", GetPosition(entry.PositionAndRadiusData)),
                        new XAttribute("CompleteCondition", entry.CompleteCondition),
                        new XAttribute("CompleteConditionText", entry.CompleteConditionText),
                        new XAttribute("AddDrop", 0), // TODO: AddDrop?
                        new XAttribute("ExpectedPrice", entry.ExpectedPrice),
                        new XAttribute("BaseReward", GetReward(entry.Rewards[0], entry.QuestGroup, entry.QuestId)),
                        new XAttribute("SelectReward", GetReward(entry.Rewards[1], entry.QuestGroup, entry.QuestId)),
                        new XAttribute("AcceptDialog", 0), // TODO
                        new XAttribute("NotAcceptDialog", 0), // TODO
                        new XAttribute("AcceptButton", 0), // TODO
                        new XAttribute("AcceptDungeon", 0), // TODO
                        new XAttribute("ProgressDialog", entry.ProgressDialog),
                        new XAttribute("RetryButton", 0), // TODO
                        new XAttribute("CompleteDialog", entry.CompleteDialog),
                        new XAttribute("ContentsGroupKey", 0)
                    )
                )
            );

            var xws = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = true,
                IndentChars = "\t"

            };
            using (var xw = XmlWriter.Create(Path.Combine("output.xml", "Quest_Table.xml"), xws))
                document.Save(xw);
        }

        // (-61458.90, -3778.72, 29886.00,2500);
        public static string GetPosition(List<float[]> data)
        {
            var sb = new StringBuilder();
            foreach (var pos in data)
                sb.Append("(" + string.Join(",", pos) + ");");
            return sb.ToString();
        }

        // item(491,0,1);item(542,0,1);
        public static string GetReward(List<Quest.Reward> data, short qGroup, short qId)
        {
            /**
             * Types   Name         Command
             *  0:   Experience     exp(count);
             *  3:   Item           item(id,level,count);
             */

            var sb = new StringBuilder();
            foreach (var reward in data)
            {
                switch (reward.RewardType)
                {
                    case 0: sb.Append($"exp({reward.Experience});"); break;
                    case 3: sb.Append($"item({reward.ItemId},{reward.ItemEnchantLevel},{reward.ItemCount});"); break;
                    default:
                        Console.WriteLine("Unsupported type: {0} for G: {1} ID: {2}", reward.RewardType, qGroup, qId);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
