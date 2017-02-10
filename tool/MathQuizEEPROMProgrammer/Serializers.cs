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

    class QuestionsSerializer : BaseSerializer
    {
        byte _stageCt;
        byte _qsPerStage;

        class QData
        {
            public UInt16 Num1 { get; set; }
            public UInt16 Num2 { get; set; }
            public char Op { get; set; }
        }

        List<QData> _qs = new List<QData>();

        public QuestionsSerializer(string filepath) :
            base(filepath)
        {
            _stageCt = byte.Parse(_lines[0]);
            _qsPerStage = byte.Parse(_lines[1]);

            for (int i = 0; i < _stageCt * _qsPerStage; i++)  // watch overflow here
            {
                var tokens = _lines[i + 2].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                _qs.Add(new QData
                {
                    Num1 = UInt16.Parse(tokens[0]),
                    Num2 = UInt16.Parse(tokens[2]),
                    Op = tokens[1][0]
                });
            }
        }

        public List<byte> Serialize()
        {
            var data = new List<byte>();

            data.Add(_stageCt);
            data.Add(_qsPerStage);

            foreach (var q in _qs)
            {
                addUInt16(data, q.Num1);
                addUInt16(data, q.Num2);

                data.Add((byte)(q.Op));
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

        public SoundSerializer(string filepath)
            : base(filepath)
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

    class LockKeySerializer : BaseSerializer
    {
        private List<byte> _keys = new List<byte>();

        public LockKeySerializer(string filepath)
            : base(filepath)
        {
            _keys = _lines[0].Take(4).Select(c => (byte)c).ToList();
        }

        public List<byte> Serialize()
        {
            return _keys;
        }
    }
}
