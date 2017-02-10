namespace MathQuizEEPROMWriter
{
    partial class MainFormOld
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnWrite = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.tbInputHex = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.tbInput = new System.Windows.Forms.TextBox();
            this.cmGenerateQuestions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miGenerateQuestions = new System.Windows.Forms.ToolStripMenuItem();
            this.miClearQuestions = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlOutput = new System.Windows.Forms.Panel();
            this.pnlOutBtm = new System.Windows.Forms.Panel();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.btnSerialize = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbInputFile = new System.Windows.Forms.ComboBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStatus = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.cmGenerateQuestions.SuspendLayout();
            this.pnlOutput.SuspendLayout();
            this.pnlOutBtm.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(418, 34);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 0;
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.OnWriteClick);
            // 
            // tbOutput
            // 
            this.tbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.Location = new System.Drawing.Point(0, 0);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutput.Size = new System.Drawing.Size(261, 542);
            this.tbOutput.TabIndex = 1;
            this.tbOutput.WordWrap = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlInput);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.pnlOutput);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(825, 542);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.tbInputHex);
            this.pnlInput.Controls.Add(this.splitter2);
            this.pnlInput.Controls.Add(this.tbInput);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInput.Location = new System.Drawing.Point(0, 0);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(561, 542);
            this.pnlInput.TabIndex = 0;
            // 
            // tbInputHex
            // 
            this.tbInputHex.AcceptsReturn = true;
            this.tbInputHex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInputHex.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInputHex.Location = new System.Drawing.Point(0, 266);
            this.tbInputHex.Multiline = true;
            this.tbInputHex.Name = "tbInputHex";
            this.tbInputHex.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInputHex.Size = new System.Drawing.Size(561, 276);
            this.tbInputHex.TabIndex = 4;
            this.tbInputHex.WordWrap = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 263);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(561, 3);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // tbInput
            // 
            this.tbInput.AcceptsReturn = true;
            this.tbInput.ContextMenuStrip = this.cmGenerateQuestions;
            this.tbInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbInput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInput.Location = new System.Drawing.Point(0, 0);
            this.tbInput.Multiline = true;
            this.tbInput.Name = "tbInput";
            this.tbInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbInput.Size = new System.Drawing.Size(561, 263);
            this.tbInput.TabIndex = 2;
            this.tbInput.WordWrap = false;
            // 
            // cmGenerateQuestions
            // 
            this.cmGenerateQuestions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miGenerateQuestions,
            this.miClearQuestions});
            this.cmGenerateQuestions.Name = "cmGenerateQuestions";
            this.cmGenerateQuestions.Size = new System.Drawing.Size(122, 48);
            this.cmGenerateQuestions.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnGenerateQuestionsClicked);
            // 
            // miGenerateQuestions
            // 
            this.miGenerateQuestions.Name = "miGenerateQuestions";
            this.miGenerateQuestions.Size = new System.Drawing.Size(121, 22);
            this.miGenerateQuestions.Tag = "0";
            this.miGenerateQuestions.Text = "Generate";
            // 
            // miClearQuestions
            // 
            this.miClearQuestions.Name = "miClearQuestions";
            this.miClearQuestions.Size = new System.Drawing.Size(121, 22);
            this.miClearQuestions.Tag = "1";
            this.miClearQuestions.Text = "Clear";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(561, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 542);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // pnlOutput
            // 
            this.pnlOutput.Controls.Add(this.tbOutput);
            this.pnlOutput.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlOutput.Location = new System.Drawing.Point(564, 0);
            this.pnlOutput.Name = "pnlOutput";
            this.pnlOutput.Size = new System.Drawing.Size(261, 542);
            this.pnlOutput.TabIndex = 1;
            // 
            // pnlOutBtm
            // 
            this.pnlOutBtm.Controls.Add(this.cbComPort);
            this.pnlOutBtm.Controls.Add(this.btnSerialize);
            this.pnlOutBtm.Controls.Add(this.button1);
            this.pnlOutBtm.Controls.Add(this.cbInputFile);
            this.pnlOutBtm.Controls.Add(this.btnRead);
            this.pnlOutBtm.Controls.Add(this.btnClear);
            this.pnlOutBtm.Controls.Add(this.btnStatus);
            this.pnlOutBtm.Controls.Add(this.btnWrite);
            this.pnlOutBtm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOutBtm.Location = new System.Drawing.Point(0, 542);
            this.pnlOutBtm.Name = "pnlOutBtm";
            this.pnlOutBtm.Size = new System.Drawing.Size(825, 70);
            this.pnlOutBtm.TabIndex = 0;
            // 
            // cbComPort
            // 
            this.cbComPort.FormattingEnabled = true;
            this.cbComPort.Location = new System.Drawing.Point(12, 6);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(320, 21);
            this.cbComPort.TabIndex = 7;
            // 
            // btnSerialize
            // 
            this.btnSerialize.Location = new System.Drawing.Point(337, 34);
            this.btnSerialize.Name = "btnSerialize";
            this.btnSerialize.Size = new System.Drawing.Size(75, 23);
            this.btnSerialize.TabIndex = 6;
            this.btnSerialize.Text = "Serialize";
            this.btnSerialize.UseVisualStyleBackColor = true;
            this.btnSerialize.Click += new System.EventHandler(this.OnSerializeClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(657, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Flush Rx";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnFlushClick);
            // 
            // cbInputFile
            // 
            this.cbInputFile.FormattingEnabled = true;
            this.cbInputFile.Items.AddRange(new object[] {
            "Questions",
            "Sound",
            "Light"});
            this.cbInputFile.Location = new System.Drawing.Point(12, 37);
            this.cbInputFile.Name = "cbInputFile";
            this.cbInputFile.Size = new System.Drawing.Size(320, 21);
            this.cbInputFile.TabIndex = 4;
            this.cbInputFile.SelectedIndexChanged += new System.EventHandler(this.OnInputFileSelect);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(499, 34);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.OnReadClick);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(738, 35);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.OnClearClick);
            // 
            // btnStatus
            // 
            this.btnStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatus.Location = new System.Drawing.Point(576, 35);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(75, 23);
            this.btnStatus.TabIndex = 1;
            this.btnStatus.Text = "Status";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.OnStatusClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 612);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlOutBtm);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEPROM Writer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.pnlMain.ResumeLayout(false);
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.cmGenerateQuestions.ResumeLayout(false);
            this.pnlOutput.ResumeLayout(false);
            this.pnlOutput.PerformLayout();
            this.pnlOutBtm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlOutput;
        private System.Windows.Forms.Panel pnlOutBtm;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.ComboBox cbInputFile;
        private System.Windows.Forms.TextBox tbInputHex;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSerialize;
        private System.Windows.Forms.ContextMenuStrip cmGenerateQuestions;
        private System.Windows.Forms.ToolStripMenuItem miGenerateQuestions;
        private System.Windows.Forms.ToolStripMenuItem miClearQuestions;
        private System.Windows.Forms.ComboBox cbComPort;
    }
}

