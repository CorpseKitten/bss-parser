using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("quest")]
    public class Quest : IBssEntry
    {
        public ushort OrderNpc { get; set; }
        public ushort OrderNpcDialogIndex { get; set; }
        public ushort CompleteNpc { get; set; }
        public ushort CompleteNpcDialogIndex { get; set; }
        public string CompleteDisplay { get; set; }
        public uint RecommendedLevel { get; set; }
        public string AcceptCondition { get; set; }
        public string CompleteCondition { get; set; }
        public string CompleteConditionText { get; set; }
        public string DoGuide { get; set; }
        public byte IsEscortQuest { get; set; }
        public Dictionary<int, List<Reward>> Rewards { get; set; }
        public long ExpectedPrice { get; set; }
        public int Unk { get; set; }
        public short RemoveQuestGroup { get; set; }
        public short RemoveQuestId { get; set; }
        public byte GuideAutoErase { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<float[]> PositionAndRadiusData { get; set; }
        public string QuestIcon { get; set; }
        public string AcceptDialogButton { get; set; }
        public string UnkStr { get; set; }
        public string CompleteDialog { get; set; }
        public string ProgressDialog { get; set; }
        public int RepeatTime { get; set; }
        public byte IsUserBaseQuest { get; set; }
        public int Region { get; set; }
        public int QuestType { get; set; }
        public int SelectType { get; set; }
        public short QuestGroup { get; set; }
        public short QuestId { get; set; }
        public byte GuideForce { get; set; }

        public class Reward
        {
            public int RewardType { get; set; } // 3
            public long Experience { get; set; } // 0
            public int Unk2 { get; set; } // 0
            public int Unk3 { get; set; } // -1
            public byte Unk4 { get; set; } // 15
            public int Unk5 { get; set; }
            public ushort ItemId { get; set; }
            public ushort ItemEnchantLevel { get; set; }
            public long ItemCount { get; set; }
            public short Unk6 { get; set; }
            public int Unk7 { get; set; }
            public int Unk8 { get; set; }
            public short Unk9 { get; set; }
        }

        public IBssEntry Read(BinaryReader br)
        {
            var quest = new Quest();
            quest.OrderNpc = br.ReadUInt16();
            quest.OrderNpcDialogIndex = br.ReadUInt16();
            quest.CompleteNpc = br.ReadUInt16();
            quest.CompleteNpcDialogIndex = br.ReadUInt16();
            quest.CompleteDisplay = br.ReadUnicodeStringBD();
            quest.RecommendedLevel = br.ReadUInt32();
            quest.AcceptCondition = br.ReadUnicodeStringBD();
            quest.CompleteCondition = br.ReadUnicodeStringBD();
            quest.CompleteConditionText = br.ReadUnicodeStringBD();
            quest.DoGuide = br.ReadUnicodeStringBD();
            quest.IsEscortQuest = br.ReadByte();
            quest.Rewards = new Dictionary<int, List<Reward>>();
            {
                var l = new List<Reward>();
                var c = br.ReadInt32();
                for (var i = 0; i < c; ++i)
                {
                    var reward = new Reward
                    {
                        RewardType = br.ReadInt32(),
                        Experience = br.ReadInt64(),
                        Unk2 = br.ReadInt32(),
                        Unk3 = br.ReadInt32(),
                        Unk4 = br.ReadByte(),
                        Unk5 = br.ReadInt32(),
                        ItemId = br.ReadUInt16(),
                        ItemEnchantLevel = br.ReadUInt16(),
                        ItemCount = br.ReadInt64(),
                        Unk6 = br.ReadInt16(),
                        Unk7 = br.ReadInt32(),
                        Unk8 = br.ReadInt32(),
                        Unk9 = br.ReadInt16()
                    };
                    l.Add(reward);
                }
                quest.Rewards.Add(0, l);
                l.Clear();
                c = br.ReadInt32();
                for (var i = 0; i < c; ++i)
                {
                    var reward = new Reward
                    {
                        RewardType = br.ReadInt32(),
                        Experience = br.ReadInt64(),
                        Unk2 = br.ReadInt32(),
                        Unk3 = br.ReadInt32(),
                        Unk4 = br.ReadByte(),
                        Unk5 = br.ReadInt32(),
                        ItemId = br.ReadUInt16(),
                        ItemEnchantLevel = br.ReadUInt16(),
                        ItemCount = br.ReadInt64(),
                        Unk6 = br.ReadInt16(),
                        Unk7 = br.ReadInt32(),
                        Unk8 = br.ReadInt32(),
                        Unk9 = br.ReadInt16()
                    };
                    l.Add(reward);
                }
                br.ReadInt64();
                quest.Rewards.Add(1, l);
            }
            quest.ExpectedPrice = br.ReadInt64();
            quest.Unk = br.ReadInt32();
            quest.RemoveQuestGroup = br.ReadInt16();
            quest.RemoveQuestId = br.ReadInt16();
            quest.GuideAutoErase = br.ReadByte();
            quest.Title = br.ReadUnicodeStringBD();
            quest.Description = br.ReadUnicodeStringBD();
            quest.PositionAndRadiusData = Enumerable.Range(0, br.ReadInt32())
                .Select(x => new []
                {
                    br.ReadSingle(), // X
                    br.ReadSingle(), // Z
                    br.ReadSingle(), // Y
                    br.ReadSingle()  // Radius
                }).ToList();
            quest.QuestIcon = br.ReadUnicodeStringBD();
            quest.AcceptDialogButton = br.ReadUnicodeStringBD();
            quest.UnkStr = br.ReadUnicodeStringBD();
            quest.CompleteDialog = br.ReadUnicodeStringBD();
            quest.ProgressDialog = br.ReadUnicodeStringBD();
            quest.RepeatTime = br.ReadInt32();
            quest.IsUserBaseQuest = br.ReadByte();
            quest.Region = br.ReadInt32();
            quest.QuestType = br.ReadInt32();
            quest.SelectType = br.ReadInt32();
            quest.QuestGroup = br.ReadInt16();
            quest.QuestId = br.ReadInt16();
            quest.GuideForce = br.ReadByte();
            return quest;
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = new List<Quest>();
            var x = br.ReadInt32();
            for (var i = 0; i < x; ++i)
                entries.Add((Quest) Read(br));
            QuestConverter.CreateXml(entries);
            QuestConverter.CreateSql(entries);
        }
    }
}
