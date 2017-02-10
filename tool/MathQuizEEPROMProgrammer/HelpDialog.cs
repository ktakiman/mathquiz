using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuizEEPROMWriter
{
    public partial class HelpDialog : Form
    {
        public HelpDialog()
        {
            InitializeComponent();
        }

        public HelpDialog(string helpText) : this()
        {
            HelpText = helpText;
        }

        public string HelpText
        {
            get { return tbHelpText.Text; }
            set { tbHelpText.Text = value; tbHelpText.Select(0, 0); }
        }
    }
}
