using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQuizEEPROMWriter
{
    class BaseSerializer
    {
        protected List<string> _lines = new List<string>();

        protected BaseSerializer(string inputText)
        {
            foreach (var line in inputText.Split(new string[] { "\r\n" },  StringSplitOptions.RemoveEmptyEntries))
            {
                var copy = line;
                int pound = copy.IndexOf('#');
                if (pound >= 0)
                {
                    copy = line.Substring(0, pound);
                }

                copy = copy.Trim();

                if (copy.Length > 0)
                {
                    _lines.Add(copy);
                }
            }
        }

        public static void addUInt16(List<byte> bytes, UInt16 n)
        {
            bytes.Add((byte)(n >> 8));
            bytes.Add((byte)(n));
        }
    }

    class UserDataSerializer : BaseSerializer
    {
        List<UserData> _users = new List<UserData>();

        class UserData
        {
            public string Name { get; set; }
            public byte Stage { get; set; }
            public string SecretKey { get; set; }
        }

        public byte UserCt { get; private set; }

        public UserDataSerializer(string inputTest) :
            base(inputTest)
        {
            UserCt = byte.Parse(_lines[0]);

            for (int i = 0; i < UserCt; i++)
            {
                _users.Add(new UserData
                {
                    Name = _lines[1 + i * 3].PadRight(10, ' ').Substring(0, 10),
                    Stage = byte.Parse(_lines[1 + i * 3 + 1]),
                    SecretKey = _lines[1 + i * 3 + 2].PadRight(10, ' ').Substring(0, 10)
                });
            }
        }

        public List<byte> Serialize()
        {
            var data = new List<byte>();

            data.Add(UserCt);

            foreach (var user in _users)
            {
                data.AddRange(Encoding.UTF8.GetBytes(user.Name));
                data.Add(user.Stage);
                data.AddRange(Encoding.UTF8.GetBytes(user.SecretKey));
            }

            return data;
        }
    }

    class QuestionsSerializer : BaseSerializer
    {
        class QData
        {
            public UInt16 Num1 { get; set; }
            public UInt16 Num2 { get; set; }
            public char Op { get; set; }
        }

        class QuestionsPerUser
        {
            public byte StageCt { get; set; }
            public byte QuestionsPerStage { get; set; }
            public List<QData> Questions { get; } = new List<QData>();

            public int TotalNumberOfQuestions => StageCt * QuestionsPerStage;

            public List<byte> Serialize()
            {
                var data = new List<byte>();

                data.Add(StageCt);
                data.Add(QuestionsPerStage);

                foreach (var q in Questions)
                {
                    addUInt16(data, q.Num1);
                    addUInt16(data, q.Num2);

                    data.Add((byte)(q.Op));
                }

                return data;
            }
        }

        List<QuestionsPerUser> _qData = new List<QuestionsPerUser>();

        public QuestionsSerializer(string inputText, int userCt) :
            base(inputText)
        {
            int curLine = 0;

            for (int i = 0; i < userCt; i++)
            {
                var qPerUser = new QuestionsPerUser
                {
                    StageCt = byte.Parse(_lines[curLine++]),
                    QuestionsPerStage = byte.Parse(_lines[curLine++])
                };

                for (int j = 0; j < qPerUser.TotalNumberOfQuestions; j++)
                {
                    var tokens = _lines[curLine++].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    qPerUser.Questions.Add(new QData
                    {
                        Num1 = UInt16.Parse(tokens[0]),
                        Num2 = UInt16.Parse(tokens[2]),
                        Op = tokens[1][0]
                    });
                }

                _qData.Add(qPerUser);
            }
        }

        public List<byte> Serialize()
        {
            var addressTableLen = 2 * _qData.Count;
            var data = new List<byte>(Enumerable.Repeat((byte)0, addressTableLen));

            var curAddress = addressTableLen;

            for (int i = 0; i < _qData.Count; i++)
            {
                data[i * 2] = (byte)(curAddress >> 8);
                data[i * 2 + 1] = (byte)(curAddress);

                var perUserData = _qData[i].Serialize();
                curAddress += perUserData.Count;
                data.AddRange(perUserData);
            }

            return data;
        }
    }

    class SoundSerializer : BaseSerializer
    {
        class Note
        {
            public char Tone { get; private set; }
            public byte Scale { get; private set; }
            public byte LenEnu { get; private set; }
            public byte LenDen { get; private set; }

            public Note(char tone, byte scale, byte lenEnu, byte lenDen)
            {
                Tone = tone;
                Scale = scale;
                LenEnu = lenEnu;
                LenDen = lenDen;
            }
        }

        class Comp
        {
            public List<Note> Notes { get; private set; }
            public byte Speed { get; private set; }

            public Comp(byte speed)
            {
                Speed = speed;
                Notes = new List<Note>();
            }
        }

        private List<Comp> _comps = new List<Comp>();

        public SoundSerializer(string inputText)
            : base(inputText)
        {
            Comp comp = null;

            foreach (var line in _lines)
            {
                if (line.StartsWith("[START]"))
                {
                    comp = new Comp(byte.Parse(line.Substring("[START]".Length)));
                }
                else if (line == "[END]")
                {
                    _comps.Add(comp);
                    comp = null;
                }
                else
                {
                    comp.Notes.Add(parseNote(line));
                }
            }
        }

        public List<byte> Serialize()
        {
            var data = new List<byte>();

            byte compCount = (byte)_comps.Count;
            UInt16 addrDataBlock = (UInt16)(1 + 2 * compCount);

            data.Add((byte)compCount);

            addUInt16(data, addrDataBlock);

            for (int i = 0; i < _comps.Count - 1; i++)
            {
                addrDataBlock += (UInt16)(_comps[i].Notes.Count * 4 + 2); // + 2 for speed(Uint8) and # of notes(Uint8)
                addUInt16(data, addrDataBlock);
            }

            foreach (var comp in _comps)
            {
                data.Add(comp.Speed);
                data.Add((byte)(comp.Notes.Count));

                foreach (var note in comp.Notes)
                {
                    data.Add((byte)(note.Tone));
                    data.Add(note.Scale);
                    data.Add(note.LenEnu);
                    data.Add(note.LenDen);
                }
            }

            return data;
        }

        private Note parseNote(string line)
        {
            var tokens = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            char tone = tokens[0][1];
            byte scale = byte.Parse(tokens[1]);
            byte lenEnu = byte.Parse(tokens[2]);
            byte lenDen = byte.Parse(tokens[3]);

            return new Note(tone, scale, lenEnu, lenDen);
        }

    }
}
