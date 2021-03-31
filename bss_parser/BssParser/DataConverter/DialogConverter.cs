using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.BssData;
using BssParser.Utils;

namespace BssParser.DataConverter
{
    public class DialogConverter
    {
        public static void CreateSql(List<DialogTable.DetailDialog> Entries)
        {
            var sb = new List<string> { QueryBuilder.Prepare("Dialog_Table") };
            foreach (var entry in Entries)
            {
                var qb = new QueryBuilder("Dialog_Table");
                qb.Add("ID", sb.Count + 1);
                qb.Add("Npc", entry.Npc);
                qb.Add("DialogIndex", entry.DialogIndex);
                qb.Add("Index", "x");
                qb.Add("Name", entry.Name);
                qb.Add("MainScene", entry.MainScene.Replace("\"", "").Replace("'", "").Replace("\n", ""));
                qb.Add("FuncDialog", entry.FuncDialogDatas.FirstOrDefault()?.FuncDialog.Replace("\"", "").Replace("'", "").Replace("\n", ""));
                qb.Add("FuncScene", "");
                qb.Add("DefaultVoice", string.Join(",", entry.DefaultVoiceDatas.Select(x => x.VoiceId)));
                qb.Add("MainDialog", entry.MainDialog.Replace("\"", "").Replace("'", "").Replace("\n", ""));
                qb.Add("Condition", "");
                qb.Add("Button", "");
                qb.Add("ButtonType", "");
                qb.Add("Dialog", "");
                qb.Add("Function", "");
                sb.Add(qb.Combine());

                foreach (var dialog in entry.DialogDatas)
                {
                    qb = new QueryBuilder("Dialog_Table");
                    qb.Add("ID", sb.Count + 1);
                    qb.Add("Npc", entry.Npc);
                    qb.Add("DialogIndex", entry.DialogIndex);
                    qb.Add("DialogNo", dialog.DialogNo);
                    qb.Add("Index", "");
                    qb.Add("Name", "");
                    qb.Add("MainScene", "");
                    qb.Add("FuncDialog", "");
                    qb.Add("FuncScene", "");
                    qb.Add("DefaultVoice", string.Join(",", entry.DefaultVoiceDatas.Select(x => x.VoiceId)));
                    qb.Add("MainDialog", "");
                    qb.Add("Condition", dialog.Condition);
                    qb.Add("Button", dialog.Button.Replace("\"", "").Replace("'", "").Replace("\n", ""));
                    qb.Add("ButtonType", dialog.ButtonType);
                    qb.Add("Dialog", dialog.Dialog.Replace("\"", "").Replace("'", "").Replace("\n", ""));
                    qb.Add("Function", dialog.Function);
                    sb.Add(qb.Combine());
                }
            }

            File.WriteAllLines(Path.Combine("output.sql", "Dialog_Table.sql"), sb);
        }
    }
}
