using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("knowledgelearning")]
    public class KnowledgeLearning : IBssEntry
    {
        public int Key { get; set; }
        public int LearningType { get; set; }
        public int SelectRate { get; set; }
        public int KnowledgeIndex { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            br.ReadInt32();
            return new KnowledgeLearning
            {
                Key = br.ReadInt32(),
                LearningType = br.ReadInt32(),
                SelectRate = br.ReadInt32(),
                KnowledgeIndex = br.ReadInt32()
            };
        }

        public void ReadTable(BinaryReader br)
        {
            var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (KnowledgeLearning) Read(br)).ToList();
            entries = entries.OrderBy(x => x.Key).ThenBy(x => x.KnowledgeIndex).ToList();
            KnowledgeLearningConverter.CreateXml(entries);
            KnowledgeLearningConverter.CreateSql(entries);
        }
    }
}
