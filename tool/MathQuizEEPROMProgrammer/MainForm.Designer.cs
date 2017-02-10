namespace MathQuizEEPROMWriter
{
    partial class MainForm
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpQData = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbQData = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbQDataPath = new System.Windows.Forms.TextBox();
            this.btnQDataSaveAs = new System.Windows.Forms.Button();
            this.btnQDataHelp = new System.Windows.Forms.Button();
            this.btnQDataLoadSample = new System.Windows.Forms.Button();
            this.btnQDataSave = new System.Windows.Forms.Button();
            this.btnQDataOpen = new System.Windows.Forms.Button();
            this.tpSdData = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tbSdData = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSdDataPath = new System.Windows.Forms.TextBox();
            this.btnSdDataSaveAs = new System.Windows.Forms.Button();
            this.btnSdDataHelp = new System.Windows.Forms.Button();
            this.btnSdDataLoadSample = new System.Windows.Forms.Button();
            this.btnSdDataSave = new System.Windows.Forms.Button();
            this.btnSdDataOpen = new System.Windows.Forms.Button();
            this.tpEEPROM = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tbEEPROMData = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbSecretKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerateEEPROMData = new System.Windows.Forms.Button();
            this.btnSaveEEPROMDataAsBinary = new System.Windows.Forms.Button();
            this.btnProgoramEEPROM = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpQData.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tpSdData.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tpEEPROM.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpQData);
            this.tcMain.Controls.Add(this.tpSdData);
            this.tcMain.Controls.Add(this.tpEEPROM);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(866, 582);
            this.tcMain.TabIndex = 0;
            // 
            // tpQData
            // 
            this.tpQData.BackColor = System.Drawing.SystemColors.Control;
            this.tpQData.Controls.Add(this.panel4);
            this.tpQData.Controls.Add(this.panel2);
            this.tpQData.Location = new System.Drawing.Point(4, 22);
            this.tpQData.Name = "tpQData";
            this.tpQData.Padding = new System.Windows.Forms.Padding(3);
            this.tpQData.Size = new System.Drawing.Size(858, 556);
            this.tpQData.TabIndex = 0;
            this.tpQData.Text = "Question Data";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbQData);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(852, 480);
            this.panel4.TabIndex = 0;
            // 
            // tbQData
            // 
            this.tbQData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbQData.Location = new System.Drawing.Point(0, 0);
            this.tbQData.Multiline = true;
            this.tbQData.Name = "tbQData";
            this.tbQData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbQData.Size = new System.Drawing.Size(852, 480);
            this.tbQData.TabIndex = 0;
            this.tbQData.WordWrap = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbQDataPath);
            this.panel2.Controls.Add(this.btnQDataSaveAs);
            this.panel2.Controls.Add(this.btnQDataHelp);
            this.panel2.Controls.Add(this.btnQDataLoadSample);
            this.panel2.Controls.Add(this.btnQDataSave);
            this.panel2.Controls.Add(this.btnQDataOpen);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 483);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(852, 70);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Path:";
            // 
            // tbQDataPath
            // 
            this.tbQDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbQDataPath.Location = new System.Drawing.Point(55, 14);
            this.tbQDataPath.Name = "tbQDataPath";
            this.tbQDataPath.ReadOnly = true;
            this.tbQDataPath.Size = new System.Drawing.Size(506, 20);
            this.tbQDataPath.TabIndex = 2;
            // 
            // btnQDataSaveAs
            // 
            this.btnQDataSaveAs.Location = new System.Drawing.Point(770, 11);
            this.btnQDataSaveAs.Name = "btnQDataSaveAs";
            this.btnQDataSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnQDataSaveAs.TabIndex = 5;
            this.btnQDataSaveAs.Text = "Save &As";
            this.btnQDataSaveAs.UseVisualStyleBackColor = true;
            this.btnQDataSaveAs.Click += new System.EventHandler(this.OnClickQDataButton);
            // 
            // btnQDataHelp
            // 
            this.btnQDataHelp.Location = new System.Drawing.Point(770, 41);
            this.btnQDataHelp.Name = "btnQDataHelp";
            this.btnQDataHelp.Size = new System.Drawing.Size(75, 23);
            this.btnQDataHelp.TabIndex = 7;
            this.btnQDataHelp.Text = "&Help";
            this.btnQDataHelp.UseVisualStyleBackColor = true;
            this.btnQDataHelp.Click += new System.EventHandler(this.OnClickQDataButton);
            // 
            // btnQDataLoadSample
            // 
            this.btnQDataLoadSample.Location = new System.Drawing.Point(608, 41);
            this.btnQDataLoadSample.Name = "btnQDataLoadSample";
            this.btnQDataLoadSample.Size = new System.Drawing.Size(156, 23);
            this.btnQDataLoadSample.TabIndex = 6;
            this.btnQDataLoadSample.Text = "&Load Sample";
            this.btnQDataLoadSample.UseVisualStyleBackColor = true;
            this.btnQDataLoadSample.Click += new System.EventHandler(this.OnClickQDataButton);
            // 
            // btnQDataSave
            // 
            this.btnQDataSave.Location = new System.Drawing.Point(689, 12);
            this.btnQDataSave.Name = "btnQDataSave";
            this.btnQDataSave.Size = new System.Drawing.Size(75, 23);
            this.btnQDataSave.TabIndex = 4;
            this.btnQDataSave.Text = "&Save";
            this.btnQDataSave.UseVisualStyleBackColor = true;
            this.btnQDataSave.Click += new System.EventHandler(this.OnClickQDataButton);
            // 
            // btnQDataOpen
            // 
            this.btnQDataOpen.Location = new System.Drawing.Point(608, 12);
            this.btnQDataOpen.Name = "btnQDataOpen";
            this.btnQDataOpen.Size = new System.Drawing.Size(75, 23);
            this.btnQDataOpen.TabIndex = 3;
            this.btnQDataOpen.Text = "&Open";
            this.btnQDataOpen.UseVisualStyleBackColor = true;
            this.btnQDataOpen.Click += new System.EventHandler(this.OnClickQDataButton);
            // 
            // tpSdData
            // 
            this.tpSdData.BackColor = System.Drawing.SystemColors.Control;
            this.tpSdData.Controls.Add(this.panel3);
            this.tpSdData.Controls.Add(this.panel5);
            this.tpSdData.Location = new System.Drawing.Point(4, 22);
            this.tpSdData.Name = "tpSdData";
            this.tpSdData.Padding = new System.Windows.Forms.Padding(3);
            this.tpSdData.Size = new System.Drawing.Size(858, 556);
            this.tpSdData.TabIndex = 1;
            this.tpSdData.Text = "Sound Data";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbSdData);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(852, 480);
            this.panel3.TabIndex = 0;
            // 
            // tbSdData
            // 
            this.tbSdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSdData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSdData.Location = new System.Drawing.Point(0, 0);
            this.tbSdData.Multiline = true;
            this.tbSdData.Name = "tbSdData";
            this.tbSdData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSdData.Size = new System.Drawing.Size(852, 480);
            this.tbSdData.TabIndex = 0;
            this.tbSdData.WordWrap = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.tbSdDataPath);
            this.panel5.Controls.Add(this.btnSdDataSaveAs);
            this.panel5.Controls.Add(this.btnSdDataHelp);
            this.panel5.Controls.Add(this.btnSdDataLoadSample);
            this.panel5.Controls.Add(this.btnSdDataSave);
            this.panel5.Controls.Add(this.btnSdDataOpen);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 483);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(852, 70);
            this.panel5.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Path:";
            // 
            // tbSdDataPath
            // 
            this.tbSdDataPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSdDataPath.Location = new System.Drawing.Point(55, 14);
            this.tbSdDataPath.Name = "tbSdDataPath";
            this.tbSdDataPath.ReadOnly = true;
            this.tbSdDataPath.Size = new System.Drawing.Size(506, 20);
            this.tbSdDataPath.TabIndex = 2;
            // 
            // btnSdDataSaveAs
            // 
            this.btnSdDataSaveAs.Location = new System.Drawing.Point(770, 11);
            this.btnSdDataSaveAs.Name = "btnSdDataSaveAs";
            this.btnSdDataSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSdDataSaveAs.TabIndex = 5;
            this.btnSdDataSaveAs.Text = "Save &As";
            this.btnSdDataSaveAs.UseVisualStyleBackColor = true;
            this.btnSdDataSaveAs.Click += new System.EventHandler(this.OnClickSdDataButton);
            // 
            // btnSdDataHelp
            // 
            this.btnSdDataHelp.Location = new System.Drawing.Point(770, 41);
            this.btnSdDataHelp.Name = "btnSdDataHelp";
            this.btnSdDataHelp.Size = new System.Drawing.Size(75, 23);
            this.btnSdDataHelp.TabIndex = 7;
            this.btnSdDataHelp.Text = "&Help";
            this.btnSdDataHelp.UseVisualStyleBackColor = true;
            this.btnSdDataHelp.Click += new System.EventHandler(this.OnClickSdDataButton);
            // 
            // btnSdDataLoadSample
            // 
            this.btnSdDataLoadSample.Location = new System.Drawing.Point(608, 41);
            this.btnSdDataLoadSample.Name = "btnSdDataLoadSample";
            this.btnSdDataLoadSample.Size = new System.Drawing.Size(156, 23);
            this.btnSdDataLoadSample.TabIndex = 6;
            this.btnSdDataLoadSample.Text = "&Load Sample";
            this.btnSdDataLoadSample.UseVisualStyleBackColor = true;
            this.btnSdDataLoadSample.Click += new System.EventHandler(this.OnClickSdDataButton);
            // 
            // btnSdDataSave
            // 
            this.btnSdDataSave.Location = new System.Drawing.Point(689, 12);
            this.btnSdDataSave.Name = "btnSdDataSave";
            this.btnSdDataSave.Size = new System.Drawing.Size(75, 23);
            this.btnSdDataSave.TabIndex = 4;
            this.btnSdDataSave.Text = "&Save";
            this.btnSdDataSave.UseVisualStyleBackColor = true;
            this.btnSdDataSave.Click += new System.EventHandler(this.OnClickSdDataButton);
            // 
            // btnSdDataOpen
            // 
            this.btnSdDataOpen.Location = new System.Drawing.Point(608, 12);
            this.btnSdDataOpen.Name = "btnSdDataOpen";
            this.btnSdDataOpen.Size = new System.Drawing.Size(75, 23);
            this.btnSdDataOpen.TabIndex = 3;
            this.btnSdDataOpen.Text = "&Open";
            this.btnSdDataOpen.UseVisualStyleBackColor = true;
            this.btnSdDataOpen.Click += new System.EventHandler(this.OnClickSdDataButton);
            // 
            // tpEEPROM
            // 
            this.tpEEPROM.BackColor = System.Drawing.SystemColors.Control;
            this.tpEEPROM.Controls.Add(this.panel6);
            this.tpEEPROM.Controls.Add(this.panel1);
            this.tpEEPROM.Location = new System.Drawing.Point(4, 22);
            this.tpEEPROM.Name = "tpEEPROM";
            this.tpEEPROM.Size = new System.Drawing.Size(858, 556);
            this.tpEEPROM.TabIndex = 2;
            this.tpEEPROM.Text = "EEPROM data";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tbEEPROMData);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(858, 509);
            this.panel6.TabIndex = 0;
            // 
            // tbEEPROMData
            // 
            this.tbEEPROMData.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbEEPROMData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEEPROMData.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEEPROMData.Location = new System.Drawing.Point(0, 0);
            this.tbEEPROMData.Multiline = true;
            this.tbEEPROMData.Name = "tbEEPROMData";
            this.tbEEPROMData.ReadOnly = true;
            this.tbEEPROMData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbEEPROMData.Size = new System.Drawing.Size(858, 509);
            this.tbEEPROMData.TabIndex = 0;
            this.tbEEPROMData.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbSecretKey);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnGenerateEEPROMData);
            this.panel1.Controls.Add(this.btnSaveEEPROMDataAsBinary);
            this.panel1.Controls.Add(this.btnProgoramEEPROM);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 509);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 47);
            this.panel1.TabIndex = 1;
            // 
            // tbSecretKey
            // 
            this.tbSecretKey.Location = new System.Drawing.Point(76, 18);
            this.tbSecretKey.Name = "tbSecretKey";
            this.tbSecretKey.Size = new System.Drawing.Size(100, 20);
            this.tbSecretKey.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Secret Key:";
            // 
            // btnGenerateEEPROMData
            // 
            this.btnGenerateEEPROMData.Location = new System.Drawing.Point(323, 16);
            this.btnGenerateEEPROMData.Name = "btnGenerateEEPROMData";
            this.btnGenerateEEPROMData.Size = new System.Drawing.Size(178, 23);
            this.btnGenerateEEPROMData.TabIndex = 3;
            this.btnGenerateEEPROMData.Text = "&Generate EEPROM Data";
            this.btnGenerateEEPROMData.UseVisualStyleBackColor = true;
            this.btnGenerateEEPROMData.Click += new System.EventHandler(this.OnClickGenerateEEPROMData);
            // 
            // btnSaveEEPROMDataAsBinary
            // 
            this.btnSaveEEPROMDataAsBinary.Location = new System.Drawing.Point(539, 16);
            this.btnSaveEEPROMDataAsBinary.Name = "btnSaveEEPROMDataAsBinary";
            this.btnSaveEEPROMDataAsBinary.Size = new System.Drawing.Size(145, 23);
            this.btnSaveEEPROMDataAsBinary.TabIndex = 4;
            this.btnSaveEEPROMDataAsBinary.Text = "Save &As Binary File";
            this.btnSaveEEPROMDataAsBinary.UseVisualStyleBackColor = true;
            // 
            // btnProgoramEEPROM
            // 
            this.btnProgoramEEPROM.Location = new System.Drawing.Point(690, 16);
            this.btnProgoramEEPROM.Name = "btnProgoramEEPROM";
            this.btnProgoramEEPROM.Size = new System.Drawing.Size(160, 23);
            this.btnProgoramEEPROM.TabIndex = 5;
            this.btnProgoramEEPROM.Text = "&Program EEPROM";
            this.btnProgoramEEPROM.UseVisualStyleBackColor = true;
            this.btnProgoramEEPROM.Click += new System.EventHandler(this.OnClickProgramEEPROM);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 582);
            this.Controls.Add(this.tcMain);
            this.Name = "MainForm";
            this.Text = "Math Quiz Game EEPROM tool";
            this.tcMain.ResumeLayout(false);
            this.tpQData.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tpSdData.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tpEEPROM.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpQData;
        private System.Windows.Forms.TabPage tpSdData;
        private System.Windows.Forms.TabPage tpEEPROM;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbQData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbQDataPath;
        private System.Windows.Forms.Button btnQDataSaveAs;
        private System.Windows.Forms.Button btnQDataSave;
        private System.Windows.Forms.Button btnQDataOpen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSaveEEPROMDataAsBinary;
        private System.Windows.Forms.Button btnProgoramEEPROM;
        private System.Windows.Forms.Button btnQDataHelp;
        private System.Windows.Forms.Button btnQDataLoadSample;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSdDataPath;
        private System.Windows.Forms.Button btnSdDataSaveAs;
        private System.Windows.Forms.Button btnSdDataHelp;
        private System.Windows.Forms.Button btnSdDataLoadSample;
        private System.Windows.Forms.Button btnSdDataSave;
        private System.Windows.Forms.Button btnSdDataOpen;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox tbSdData;
        private System.Windows.Forms.Button btnGenerateEEPROMData;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox tbEEPROMData;
        private System.Windows.Forms.TextBox tbSecretKey;
        private System.Windows.Forms.Label label2;
    }
}