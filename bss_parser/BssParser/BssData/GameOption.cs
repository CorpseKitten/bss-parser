using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Configuration;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    [BssTable("gameoption")]
    public class GameOption : IBssEntry
    {
        public Dictionary<int, List<float>> ClassBalance { get; set; }

        public IBssEntry Read(BinaryReader br)
        {
            var option = new GameOption
            {
                ClassBalance = new Dictionary<int, List<float>>()
            }; // 9261
            br.BaseStream.Position = 9257; // 9265 on EU/NA

            for (var i = 0; i < 32; ++i)
            {
                var data = new List<float>();
                for (var j = 0; j < 32; ++j)
                    data.Add(br.ReadSingle());
                option.ClassBalance.Add(i, data);                                
            }

            return option;
        }

        public void ReadTable(BinaryReader br)
        {
            var option = (GameOption) Read(br);
            GameOptionConverter.CreateSql(option);
            GameOptionConverter.CreateXml(option);
        }
    }
}
