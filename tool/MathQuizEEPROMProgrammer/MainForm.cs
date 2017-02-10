using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuizEEPROMWriter
{
    public partial class MainForm : Form
    {
        private const string _sampleQDataFilepath = "Sample Data/SampleQuestionData.txt";
        private const string _sampleSdDataFilepath = "Sample Data/SampleSoundData.txt";

        private const string _helpQDataFilepath = "Help/HelpQData.txt";
        private const string _helpSdDataFilepath = "Help/HelpSdData.txt";
        private const string _helpEEPROMDataFilepath = "Help/HelpEEPROM.txt";

        private const string _dlgFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

        private string _qDataFilepath;
        private string _sdDataFilepath;

        private Timer _timer;

        private List<byte> _eepromData;

        public MainForm()
        {
            InitializeComponent();

            btnQDataSave.Enabled = btnSdDataSave.Enabled = false;

            _timer = new Timer { Interval = 1000 };
            _timer.Tick += timerTick;
            _timer.Enabled = true;

        }

        private void timerTick(object sender, EventArgs e)
        {
            btnGenerateEEPROMData.Enabled = tbQData.TextLength > 0 && tbSdData.TextLength > 0 && tbSecretKey.TextLength > 0;

            btnSaveEEPROMDataAsBinary.Enabled = btnProgoramEEPROM.Enabled = _eepromData?.Count > 0;
        }

        private void OnClickQDataButton(object sender, EventArgs e)
        {
            if (sender == btnQDataOpen)
            {
                var dlg = new OpenFileDialog { Filter = _dlgFilter };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbQDataPath.Text = _qDataFilepath = dlg.FileName;
                    tbQData.Text = File.ReadAllText(_qDataFilepath);

                    btnQDataSave.Enabled = btnQDataSaveAs.Enabled = true;
                }
            }
            else if (sender == btnQDataSave)
            {
                File.WriteAllText(_qDataFilepath, tbQData.Text);
            }
            else if (sender == btnQDataSaveAs)
            {
                var dlg = new SaveFileDialog { Filter = _dlgFilter };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbQDataPath.Text = _qDataFilepath = dlg.FileName;
                    File.WriteAllText(_qDataFilepath, tbQData.Text);
                }
            }
            else if (sender == btnQDataLoadSample)
            {
                tbQData.Text = File.ReadAllText(_sampleQDataFilepath);
            }
            else if (sender == btnQDataHelp)
            {
                new HelpDialog(File.ReadAllText(_helpQDataFilepath)).ShowDialog();
            }
        }

        private void OnClickSdDataButton(object sender, EventArgs e)
        {
            if (sender == btnSdDataOpen)
            {
                var dlg = new OpenFileDialog { Filter = _dlgFilter };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbSdDataPath.Text = _sdDataFilepath = dlg.FileName;
                    tbSdData.Text = File.ReadAllText(_sdDataFilepath);

                    btnSdDataSave.Enabled = btnSdDataSaveAs.Enabled = true;
                }
            }
            else if (sender == btnSdDataSave)
            {
                File.WriteAllText(_sdDataFilepath, tbSdData.Text);
            }
            else if (sender == btnSdDataSaveAs)
            {
                var dlg = new SaveFileDialog { Filter = _dlgFilter };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tbSdDataPath.Text = _sdDataFilepath = dlg.FileName;
                    File.WriteAllText(_sdDataFilepath, tbSdData.Text);
                }
            }
            else if (sender == btnSdDataLoadSample)
            {
                tbSdData.Text = File.ReadAllText(_sampleSdDataFilepath);
            }
            else if (sender == btnSdDataHelp)
            {
                new HelpDialog(File.ReadAllText(_helpSdDataFilepath)).ShowDialog();
            }
        }

        private void OnClickGenerateEEPROMData(object sender, EventArgs e)
        {
            _eepromData = MainSerializer.Serialize(tbQData.Text, tbSdData.Text, tbSecretKey.Text);

            tbEEPROMData.Text = formatMemory(0, _eepromData, _eepromData.Count);
        }

        private void OnClickProgramEEPROM(object sender, EventArgs e)
        {
            new ProgramEEPROMDialog(_eepromData.ToArray()).ShowDialog();
        }

        private string formatMemory(UInt16 addr, List<byte> data, int len)
        {
            var sb = new StringBuilder();

            bool done = false;

            for (int p = 0; !done; p++)
            {
                for (int i = 0; i < 4 & !done; i++)
                {
                    sb.AppendFormat("0x{0}  ", (addr + p * 32 + i * 8).ToString("x4"));

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
    }
}
