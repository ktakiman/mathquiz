using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuizEEPROMWriter
{
    public partial class ProgramEEPROMDialog : Form
    {
        private const int CMD_STATUS = 0x10;
        private const int CMD_WRITE = 0x20;
        private const int CMD_READ = 0x30;

        private const byte STATUS_IDLE = 0xa0;
        private const int STATUS_WRITE = 0xa1;
        private const int STATUS_READ = 0xa2;

        private const int INPUT_BUF_SIZE = 4096;
        private const int PAGE_SIZE = 64;

        private const byte NOTIFY_IDLE = 0xd0;
        private const byte NOTIFY_WRITE_COMPLETE = 0xd1;

        private byte[] _writeHeader = new byte[] { CMD_WRITE, 0x00, 0x00, 0x00 };
        private byte[] _readHeader = new byte[] { CMD_READ, 0x00, 0x00, 0x00 };

        private byte[] _outputBuf = new byte[PAGE_SIZE];

        private enum EStatus
        {
            Inactive,
            Pending,
            Success,
            Failure
        }

        private byte[] _newData;


        public ProgramEEPROMDialog(byte[] newData)
        {
            InitializeComponent();

            _newData = newData;

            setStatus(lblCheckResult, lblCheck, EStatus.Inactive);
            setStatus(lblReadResult, lblRead, EStatus.Inactive);
            setStatus(lblWriteResult, lblWrite, EStatus.Inactive);
            setStatus(lblVerifyResult, lblVerify, EStatus.Inactive);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            fillWithComPortsDescriptiveNames(cbComPort);
        }

        public void OnClickEEPROMActionButton(object sender, EventArgs e)
        {
            var com = cbComPort.SelectedValue as string;

            bool isCheck = sender == btnCheck;
            bool isRead = sender == btnRead;
            bool isWrite = sender == btnWrite;

            setStatus(lblCheckResult, lblCheck, EStatus.Pending);

            if (isRead)
            {
                setStatus(lblReadResult, lblRead, EStatus.Pending);
                setStatus(lblWriteResult, lblWrite, EStatus.Inactive);
                setStatus(lblVerifyResult, lblVerify, EStatus.Inactive);
            }
            else if (isWrite)
            {
                setStatus(lblReadResult, lblRead, EStatus.Inactive);
                setStatus(lblWriteResult, lblWrite, EStatus.Pending);
                setStatus(lblVerifyResult, lblVerify, EStatus.Pending);
            }
            else
            {
                setStatus(lblReadResult, lblRead, EStatus.Inactive);
                setStatus(lblWriteResult, lblWrite, EStatus.Inactive);
                setStatus(lblVerifyResult, lblVerify, EStatus.Inactive);
            }

            try
            {
                using (var port = new SerialPort(com, 14400))
                {
                    port.Open();

                    if (checkProgrammerStatus(port))
                    {
                        setStatus(lblCheckResult, lblCheck, EStatus.Success);

                        if (isRead)
                        {
                            byte[] outBuf;
                            var readSuccess = readEEPROM(port, out outBuf);

                            setStatus(lblReadResult, lblRead, readSuccess ? EStatus.Success : EStatus.Failure);

                        }
                        else if (isWrite)
                        {
                            var writeSuccess = writeEEPROM(port);

                            setStatus(lblWriteResult, lblWrite, writeSuccess ? EStatus.Success : EStatus.Failure);

                            if (writeSuccess)
                            {
                                byte[] outBuf;
                                var readSuccess = readEEPROM(port, out outBuf);
                                var verifySuccess = readSuccess && outBuf.SequenceEqual(_newData);

                                setStatus(lblVerifyResult, lblVerify, verifySuccess ? EStatus.Success : EStatus.Failure);

                            }
                        }
                    }
                    else
                    {
                        setStatus(lblCheckResult, lblCheck, EStatus.Failure);

                    }

                    port.Close();
                }
            }
            catch (Exception ex)
            {
                tbSerialOut.AppendText("#### Exception thrown ####\r\n\r\n" + ex.ToString());
            }
        }

        private bool checkProgrammerStatus(SerialPort port)
        {
            var txBuf = new byte[] { CMD_STATUS };
            var rxBuf = new byte[1];

            writeTx(port, txBuf, 0, 1);
            readRx(port, rxBuf, 0, 1, 1000);

            return rxBuf[0] == NOTIFY_IDLE;
        }

        private bool readEEPROM(SerialPort port, out byte[] outBuf)
        {
            var tempBuf = new byte[_newData.Length];
            var totalReadLen = 0;

            var result = pageOp((pos, len, highAddr, lowAddr) =>
            {
                var txBuf = new byte[] { CMD_READ, highAddr, lowAddr, len };

                writeTx(port, txBuf, 0, txBuf.Length);

                var read = readRx(port, tempBuf, totalReadLen, len, 1000);

                totalReadLen += read;

                return read == len;
            });

            outBuf = tempBuf;

            return result;
        }

        private bool writeEEPROM(SerialPort port)
        {
            return pageOp((pos, len, highAddr, lowAddr) =>
            {
                var txBuf = new byte[] { CMD_WRITE, highAddr, lowAddr, len };

                writeTx(port, txBuf, 0, txBuf.Length);

                writeTx(port, _newData, pos, len);

                var tempBuf = new byte[1];

                readRx(port, tempBuf, 0, 1, 1000);

                return tempBuf[0] == NOTIFY_WRITE_COMPLETE;
            });
        }

        private bool pageOp(Func<int, byte, byte, byte, bool> op) // callback takes the position in _newData array, data length in a page, high address, low address
        {
            int bytesLeft = _newData.Length;

            UInt16 addr = 0;
            bool success = true;

            while (bytesLeft > 0)
            {
                byte len = (byte)(bytesLeft >= PAGE_SIZE ? PAGE_SIZE : bytesLeft);

                byte highAddr = (byte)(addr >> 8);
                byte lowAddr = (byte)addr;

                if (!op(_newData.Length - bytesLeft, len, highAddr, lowAddr))
                {
                    success = false;
                }

                bytesLeft -= len;
                addr += len;
            }

            return success;
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

        private void writeTx(SerialPort port, Byte[] buf, int start, int size)
        {
            port.Write(buf, start, size);

            tbSerialOut.AppendText(hex(buf, start, size, true) + "\r\n");
        }

        private int readRx(SerialPort port, Byte[] buf, int start, int size, int timeOutInMs)
        {
            var waitUntil = DateTime.Now.AddSeconds(5);

            var sw = new Stopwatch();
            sw.Start();

            int bytesArrived = 0;
            while (bytesArrived < size && sw.ElapsedMilliseconds < timeOutInMs)
            {
                bytesArrived += port.Read(buf, start + bytesArrived, size - bytesArrived);
            }

            tbSerialIn.AppendText(hex(buf, start, bytesArrived, true) + "\r\n");

            return bytesArrived;
        }

        private string hex(Byte b, bool prepend0x)
        {
            return (prepend0x ? "0x" : "") + string.Format("{0:x}", b).PadLeft(2, '0');
        }

        private string hex(Byte[] buf, int start, int size, bool prepend0X)
        {
            return string.Join(" ", Enumerable.Range(start, size).Select(p => hex(buf[p], prepend0X)));
        }

        private void setStatus(Label lblCheck, Label lblText, EStatus status)
        {
            if (status == EStatus.Inactive)
            {
                lblCheck.Visible = lblText.Visible  = false;
            }
            else
            {
                lblCheck.Visible = lblText.Visible = true;
                lblText.ForeColor = Color.Black;

                if (status == EStatus.Pending)
                {
                    lblCheck.ForeColor = Color.Gray;
                    lblCheck.Text = "_";
                }
                else if (status == EStatus.Success)
                {
                    lblCheck.ForeColor = Color.Green;
                    lblCheck.Text = "✔";
                }
                else if (status == EStatus.Failure)
                {
                    lblCheck.ForeColor = Color.Red;
                    lblCheck.Text = "X";
                }
            }
        }

        private void OnClickClearText(object sender, EventArgs e)
        {
            tbSerialIn.Clear();
            tbSerialOut.Clear();
        }
    }
}
