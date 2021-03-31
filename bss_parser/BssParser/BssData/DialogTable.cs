using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BssParser.DataConverter;
using BssParser.Utils;

namespace BssParser.BssData
{
    // IMPORTANT:
    // When importing, base dialog must be first.
    // Advanced dialog will add remaining data.
    public class DialogTable
    {
        [BssTable("base_dialog")]
        public class BaseDialog : IBssEntry
        {
            public ushort Npc { get; set; }
            public ushort DialogIndex { get; set; }
            public string Name { get; set; }
            public List<Tuple<int, string>> BubbleList { get; set; }
            public IBssEntry Read(BinaryReader br)
            {
                return new BaseDialog
                {
                    Npc = br.ReadUInt16(),
                    DialogIndex = br.ReadUInt16(),
                    Name = br.ReadUnicodeStringBD(),
                    BubbleList = Enumerable.Range(0, br.ReadInt32()).Select(x => new Tuple<int, string>(x + 1, br.ReadUnicodeStringBD())).ToList()
                };
            }

            public void ReadTable(BinaryReader br)
            {
                var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (BaseDialog) Read(br)).ToList();
                //XmlConvert.DialogTable.CreateBase(entries);
            }
        }

        [BssTable("detail_dialog")]
        public class DetailDialog : IBssEntry
        {
            public ushort Key { get; set; }
            public ushort Npc { get; set; }
            public ushort DialogIndex { get; set; }
            public string MainScene { get; set; }
            public string Name { get; set; }
            public string MainDialog { get; set; }
            public List<FuncDialogData> FuncDialogDatas { get; set; }
            public List<DialogData> DialogDatas { get; set; }
            public List<ButtonData> ButtonDatas { get; set; }
            public List<DefaultVoiceData> DefaultVoiceDatas { get; set; }
            public IBssEntry Read(BinaryReader br)
            {
                var dd = new DetailDialog();
                dd.Key = br.ReadUInt16();
                br.ReadUInt16();
                dd.Npc = br.ReadUInt16();
                dd.DialogIndex = br.ReadUInt16();
                dd.MainScene = br.ReadAsciiStringBD();
                dd.Name = br.ReadAsciiStringBD();
                dd.MainDialog = br.ReadUnicodeStringBD();
                dd.FuncDialogDatas = Enumerable.Range(0, br.ReadInt32()).Select(x => FuncDialogData.Read(br)).ToList();
                dd.DialogDatas = Enumerable.Range(0, br.ReadInt32()).Select(x => DialogData.Read(br)).ToList();
                dd.ButtonDatas = Enumerable.Range(0, br.ReadInt32()).Select(x => ButtonData.Read(br)).ToList();
                dd.DefaultVoiceDatas = Enumerable.Range(0, (int) br.ReadInt64()).Select(x => DefaultVoiceData.Read(br)).ToList();
                return dd;
            }

            public void ReadTable(BinaryReader br)
            {
                var entries = Enumerable.Range(0, br.ReadInt32()).Select(x => (DetailDialog)Read(br)).ToList();
                entries = entries.OrderBy(x => x.Key).ThenBy(x => x.Npc).ToList();
                DialogConverter.CreateSql(entries);
            }

            public class ButtonData
            {
                public string Condition { get; set; }
                public string Button { get; set; }
                public ushort DialogNo { get; set; }

                public static ButtonData Read(BinaryReader br)
                {
                    return new ButtonData
                    {
                        Condition = br.ReadUnicodeStringBD(),
                        Button = br.ReadUnicodeStringBD(),
                        DialogNo = br.ReadUInt16()
                    };
                }
            }

            public class DialogData
            {
                public string Condition { get; set; }
                public string Button { get; set; }
                public uint ButtonType { get; set; }
                public string Dialog { get; set; }
                public string Function { get; set; }
                public ushort DialogNo { get; set; }

                public static DialogData Read(BinaryReader br)
                {
                    return new DialogData
                    {
                        Condition = br.ReadUnicodeStringBD(),
                        Button = br.ReadUnicodeStringBD(),
                        ButtonType = br.ReadUInt32(),
                        Dialog = br.ReadUnicodeStringBD(),
                        Function = br.ReadUnicodeStringBD(),
                        DialogNo = br.ReadUInt16()
                    };
                }
            }

            public class FuncDialogData
            {
                public string FuncDialog { get; set; }

                public static FuncDialogData Read(BinaryReader b)
                {
                    return new FuncDialogData
                    {
                        FuncDialog = b.ReadUnicodeStringBD()
                    };
                }
            }

            public class DefaultVoiceData
            {
                public ushort VoiceId { get; set; }

                public static DefaultVoiceData Read(BinaryReader br)
                {
                    return new DefaultVoiceData
                    {
                        VoiceId = br.ReadUInt16()
                    };
                }
            }
        }
    }
}
