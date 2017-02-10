using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace MathQuizEEPROMWriter
{
    public partial class MainFormOld : Form
    {
        const string FILEDIR = "../../datafiles/v1/";
        const string QESTION_FILE = "questions.txt";
        const string SOUND_FILE = "sound.txt";
        const string LIGHT_FILE = "light.txt";
        const string LOCKKEY_FILE = "lockkey.txt";

        const int CMD_STATUS = 0x10;
        const int CMD_WRITE = 0x20;
        const int CMD_READ = 0x30;

        const int STATUS_IDLE = 0xa0;
        const int STATUS_WRITE = 0xa1;
        const int STATUS_READ = 0xa2;

        const int INPUT_BUF_SIZE = 4096;
        const int PAGE_SIZE = 64;

        byte[] _writeHeader = new byte[] { CMD_WRITE, 0x00, 0x00, 0x00 };
        byte[] _readHeader = new byte[] { CMD_READ, 0x00, 0x00, 0x00 };
        byte[] _statusHeader = new byte[] { CMD_STATUS };

        byte[] _inputBuf = new byte[INPUT_BUF_SIZE];
        byte[] _outputBuf = new byte[PAGE_SIZE];

        // Write request
        // 1. CMD_WRITE
        // 2. HIGH ADDRESS
        // 3. LOW ADDRESS
        // 4. Data Size (max = 64)
        // 5. Data

        // Read request
        // 1. CMD_READ
        // 2. HIGH ADDRESS
        // 3. LOW ADDRESS
        // 4. HIGH Data Size 
        // 5. LOW Data Size
        // 6. Data (Rx)


        public MainFormOld()
        {
            InitializeComponent();

            fillWithComPortsDescriptiveNames(cbComPort);
        }

        private void fillWithComPortsDescriptiveNames(ComboBox cb)
        {
            var source = new List<Tuple<string, string>>();
            var ports = new HashSet<string>(SerialPort.GetPortNames());

            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE PNPClass LIKE 'Ports'"))
            {
                foreach (var o in searcher.Get())
                {
                    var caption = o["Caption"].ToString();

                    var match = ports.FirstOrDefault(p => caption.ToLower().Contains(p.ToLower()));

                    if (match != null)
                    {
                        source.Add(Tuple.Create(match, caption));
                        ports.Remove(match);
                    }
                }
            }

            // ports wihtout match
            if (ports.Any())
            {
                source.AddRange(ports.Select(p => Tuple.Create(p, p)));
            }

            cb.DataSource = source;
            cb.DisplayMember = "Item2";
            cb.ValueMember = "Item1";

            if (source.Any())
            {
                cb.SelectedIndex = 0;
            }

            // *** Do 'cb.SelectedValue as string' to retrieve selected com port name ***
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void serial(Action<SerialPort> callback)
        {
            var com = cbComPort.SelectedValue as string;
            var port = new SerialPort(com, 14400);

            port.Open();

            callback(port);

            port.Close();
        }

        private void executeWrite(UInt16 addr, byte size, byte[] data, int startPos, SerialPort port)
        {
            _writeHeader[1] = (byte)(addr >> 8);
            _writeHeader[2] = (byte)addr;
            _writeHeader[3] = size;

            port.Write(_writeHeader, 0, 4);

            port.Write(data, startPos, size);
        }

        private void executeRead(UInt16 addr, byte size, SerialPort port)
        {
            _readHeader[1] = (byte)(addr >> 8);
            _readHeader[2] = (byte)addr;
            _readHeader[3] = (byte)size;

            port.Write(_readHeader, 0, 4);
        }

        private void onUIThread(Action action)
        {
            tbOutput.Invoke(action);
        }

        private string hex(byte b, bool prepend0x)
        {
            return (prepend0x ? "0x" : "") + string.Format("{0:x}", b).PadLeft(2, '0');
        }

        private void readRx(int size, SerialPort port)
        {
            var waitUntil = DateTime.Now.AddSeconds(5);

            int bytesArrived = 0;
            while (bytesArrived < size)
            {
                bytesArrived += port.Read(_inputBuf, bytesArrived, INPUT_BUF_SIZE - bytesArrived);
            }
        }

        private void OnWriteClick(object sender, EventArgs e)
        {
            serialize(false);
        }

        private void OnReadClick(object sender, EventArgs e)
        {
            serial(port =>
            {
                UInt16 addr = 0x0000;

                byte pgSz = 64; 

                for (int page = 0; page < 48; page++)
                {
                    addr = (UInt16)(page * pgSz);

                    executeRead(addr, pgSz, port);

                    readRx(pgSz, port);

                    tbOutput.AppendText(FormatMemory(addr, _inputBuf, pgSz));
                }

                tbOutput.AppendText("\r\n");
            });
        }

        private void OnStatusClick(object sender, EventArgs e)
        {
            serial(port =>
            {
                port.Write(_statusHeader, 0, 1);

                readRx(1, port);

                tbOutput.AppendText(string.Format("STATUS: {0}\r\n", _inputBuf[0].ToString("x2")));
            });
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            tbOutput.Clear();
        }

        private void OnInputFileSelect(object sender, EventArgs e)
        {
            string file = null;

            switch (cbInputFile.SelectedIndex)
            {
                case 0: file = QESTION_FILE; break;
                case 1: file = SOUND_FILE; break;
                case 2: file = LIGHT_FILE; break;
                default: break;
            }

            tbInput.Text = File.ReadAllText(FILEDIR + file);
        }

        private void OnFlushClick(object sender, EventArgs e)
        {
            serial(port =>
            {
                if (port.BytesToRead > 0)
                {
                    int read = port.Read(_inputBuf, 0, INPUT_BUF_SIZE);

                    for (int i = 0; i < read; i++)
                    {
                        tbOutput.AppendText(hex(_inputBuf[i], true) + "\r\n");
                    }
                }
                else
                {
                    tbOutput.AppendText("empty\r\n");
                }
            });
        }

        private void OnSerializeClick(object sender, EventArgs e)
        {
            serialize(true);
        }

        private void serialize(bool previewOnly)
        {
            var qSerializer = new QuestionsSerializer(File.ReadAllText(FILEDIR + QESTION_FILE));
            var sSerializer = new SoundSerializer(File.ReadAllText(FILEDIR + SOUND_FILE));
            var lKSerializer = new LockKeySerializer(File.ReadAllText(FILEDIR + LOCKKEY_FILE));

            var qData = qSerializer.Serialize();
            var sData = sSerializer.Serialize();
            var lkData = lKSerializer.Serialize();

            var data = new List<byte>();

            UInt16 qDataAddr = 0x06;
            UInt16 sDataAddr = (UInt16)(qDataAddr + qData.Count);
            UInt16 lkDataAddr = (UInt16)(sDataAddr + sData.Count);

            BaseSerializer.addUInt16(data, qDataAddr);
            BaseSerializer.addUInt16(data, sDataAddr);
            BaseSerializer.addUInt16(data, lkDataAddr);

            data.AddRange(qData);
            data.AddRange(sData);
            data.AddRange(lkData);

            var bytes = data.ToArray();
            var len = bytes.Length;
            var pageCt = len / PAGE_SIZE  + (len % PAGE_SIZE > 0 ? 1 : 0);

            tbInputHex.Text = string.Format("Total bytes: {0}, Pages: {1}\r\n\r\n", len, pageCt);

            tbInputHex.AppendText(FormatMemory((UInt16)0x0000, bytes, len));

            if (!previewOnly)
            {
                serial(port =>
                {
                    for (int page = 0; page < pageCt; page++)
                    {
                        var addr = (UInt16)(page * PAGE_SIZE);
                        var size = (byte)(page == pageCt - 1 ? len % PAGE_SIZE : PAGE_SIZE);

                        executeWrite(addr, size, bytes, addr, port);

                        tbOutput.AppendText(string.Format("writing to 0x{0} ... ", (page * PAGE_SIZE).ToString("x4")));

                        readRx(1, port);

                        tbOutput.AppendText(hex(_inputBuf[0], true) + "\r\n");
                    }
                });
            }
        }

        private string FormatMemory(UInt16 addr, byte[] data, int len)
        {
            var sb = new StringBuilder();

            bool done = false;

            for (int p = 0; !done; p++)
            {
                for (int i = 0; i < 4 & !done; i++)
                {
                    sb.AppendFormat("0x{0} ", (addr + p * 32 + i * 8).ToString("x4"));

                    for (int j = 0; j < 2 & !done; j++)
                    {
                        for (int k = 0; k < 4 & !done; k++)
                        {
                            var pos = p * 32 + i * 8 + j * 4 + k;
                            sb.Append(data[pos].ToString("x2"));

                            done = pos == len - 1;
                        }

                        sb.Append(" ");
                    }

                    sb.AppendLine();
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        #region Question Generation Helper

        private void OnGenerateQuestionsClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "miClearQuestions")
            {
                tbInput.Clear();
            }
            else if (e.ClickedItem.Name == "miGenerateQuestions")
            {
                if (tbInput.Lines.Length == 0)
                {
                    MessageBox.Show("1st line got to be '+', '-', '*', '/')");
                }
                else
                {
                    switch (tbInput.Lines[0])
                    {
                        case "+":
                            createAdditionQs();
                            break;
                        case "-":
                            MessageBox.Show("Not implemented yet");
                            break;
                        case "*":
                            MessageBox.Show("Not implemented yet");
                            break;
                        case "/":
                            MessageBox.Show("Not implemented yet");
                            break;
                        default:
                            MessageBox.Show("1st line got to be '+', '-', '*', '/')");
                            break;
                    }
                }
            }
        }

        private void createAdditionQs()
        {
            try
            {
                int count = int.Parse(tbInput.Lines[1]);
                int min = int.Parse(tbInput.Lines[2]);
                int max = int.Parse(tbInput.Lines[3]);
                bool carry = bool.Parse(tbInput.Lines[4]);

                tbInput.Text = string.Join("\r\n", Enumerable.Range(0, 5).Select(i => tbInput.Lines[i])) + "\r\n\r\n";

                tbInput.AppendText(QuestionGenerator.GenerateAddition(min, max, carry, count));
            }
            catch
            {
                MessageBox.Show("##### Format #####\r\nCount\r\nMin\r\nMax\r\nCarry(true/false)");
            }
        }

        #endregion
    }
}
