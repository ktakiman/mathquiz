namespace MathQuizEEPROMWriter
{
    partial class ProgramEEPROMDialog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblCheck = new System.Windows.Forms.Label();
            this.lblVerify = new System.Windows.Forms.Label();
            this.lblCheckResult = new System.Windows.Forms.Label();
            this.lblWrite = new System.Windows.Forms.Label();
            this.lblReadResult = new System.Windows.Forms.Label();
            this.lblRead = new System.Windows.Forms.Label();
            this.lblWriteResult = new System.Windows.Forms.Label();
            this.lblVerifyResult = new System.Windows.Forms.Label();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbComPort = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tbSerialIn = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbSerialOut = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClearText = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.btnWrite);
            this.panel1.Controls.Add(this.btnRead);
            this.panel1.Controls.Add(this.btnCheck);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbComPort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 276);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Rx";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Tx";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(12, 251);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(520, 2);
            this.panel7.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "  Status  ";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblCheck);
            this.panel6.Controls.Add(this.lblVerify);
            this.panel6.Controls.Add(this.lblCheckResult);
            this.panel6.Controls.Add(this.lblWrite);
            this.panel6.Controls.Add(this.lblReadResult);
            this.panel6.Controls.Add(this.lblRead);
            this.panel6.Controls.Add(this.lblWriteResult);
            this.panel6.Controls.Add(this.lblVerifyResult);
            this.panel6.Location = new System.Drawing.Point(99, 49);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(338, 118);
            this.panel6.TabIndex = 12;
            // 
            // lblCheck
            // 
            this.lblCheck.AutoSize = true;
            this.lblCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheck.Location = new System.Drawing.Point(40, 11);
            this.lblCheck.Name = "lblCheck";
            this.lblCheck.Size = new System.Drawing.Size(208, 13);
            this.lblCheck.TabIndex = 11;
            this.lblCheck.Text = "Connected to EEPROM programmer";
            // 
            // lblVerify
            // 
            this.lblVerify.AutoSize = true;
            this.lblVerify.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerify.Location = new System.Drawing.Point(40, 88);
            this.lblVerify.Name = "lblVerify";
            this.lblVerify.Size = new System.Drawing.Size(148, 13);
            this.lblVerify.TabIndex = 11;
            this.lblVerify.Text = "Verify EEPROM contents";
            // 
            // lblCheckResult
            // 
            this.lblCheckResult.AutoSize = true;
            this.lblCheckResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckResult.Location = new System.Drawing.Point(17, 11);
            this.lblCheckResult.Name = "lblCheckResult";
            this.lblCheckResult.Size = new System.Drawing.Size(17, 13);
            this.lblCheckResult.TabIndex = 11;
            this.lblCheckResult.Text = "✔";
            // 
            // lblWrite
            // 
            this.lblWrite.AutoSize = true;
            this.lblWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWrite.Location = new System.Drawing.Point(40, 62);
            this.lblWrite.Name = "lblWrite";
            this.lblWrite.Size = new System.Drawing.Size(146, 13);
            this.lblWrite.TabIndex = 11;
            this.lblWrite.Text = "Write EEPROM contents";
            // 
            // lblReadResult
            // 
            this.lblReadResult.AutoSize = true;
            this.lblReadResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReadResult.Location = new System.Drawing.Point(17, 37);
            this.lblReadResult.Name = "lblReadResult";
            this.lblReadResult.Size = new System.Drawing.Size(17, 13);
            this.lblReadResult.TabIndex = 11;
            this.lblReadResult.Text = "✔";
            // 
            // lblRead
            // 
            this.lblRead.AutoSize = true;
            this.lblRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRead.Location = new System.Drawing.Point(40, 37);
            this.lblRead.Name = "lblRead";
            this.lblRead.Size = new System.Drawing.Size(146, 13);
            this.lblRead.TabIndex = 11;
            this.lblRead.Text = "Read EEPROM contents";
            // 
            // lblWriteResult
            // 
            this.lblWriteResult.AutoSize = true;
            this.lblWriteResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWriteResult.Location = new System.Drawing.Point(17, 62);
            this.lblWriteResult.Name = "lblWriteResult";
            this.lblWriteResult.Size = new System.Drawing.Size(17, 13);
            this.lblWriteResult.TabIndex = 11;
            this.lblWriteResult.Text = "✔";
            // 
            // lblVerifyResult
            // 
            this.lblVerifyResult.AutoSize = true;
            this.lblVerifyResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerifyResult.Location = new System.Drawing.Point(17, 88);
            this.lblVerifyResult.Name = "lblVerifyResult";
            this.lblVerifyResult.Size = new System.Drawing.Size(17, 13);
            this.lblVerifyResult.TabIndex = 11;
            this.lblVerifyResult.Text = "✔";
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(447, 187);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(90, 23);
            this.btnWrite.TabIndex = 10;
            this.btnWrite.Text = "&Write EEPROM";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.OnClickEEPROMActionButton);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(330, 187);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(107, 23);
            this.btnRead.TabIndex = 10;
            this.btnRead.Text = "&Read EEPROM";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.OnClickEEPROMActionButton);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(184, 187);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(124, 23);
            this.btnCheck.TabIndex = 10;
            this.btnCheck.Text = "&Check Connection";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.OnClickEEPROMActionButton);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "COM Ports:";
            // 
            // cbComPort
            // 
            this.cbComPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbComPort.FormattingEnabled = true;
            this.cbComPort.Location = new System.Drawing.Point(99, 12);
            this.cbComPort.Name = "cbComPort";
            this.cbComPort.Size = new System.Drawing.Size(438, 21);
            this.cbComPort.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 276);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(549, 256);
            this.panel2.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tbSerialIn);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(280, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(269, 256);
            this.panel5.TabIndex = 2;
            // 
            // tbSerialIn
            // 
            this.tbSerialIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSerialIn.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSerialIn.Location = new System.Drawing.Point(0, 0);
            this.tbSerialIn.Multiline = true;
            this.tbSerialIn.Name = "tbSerialIn";
            this.tbSerialIn.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSerialIn.Size = new System.Drawing.Size(269, 256);
            this.tbSerialIn.TabIndex = 1;
            this.tbSerialIn.WordWrap = false;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Highlight;
            this.splitter1.Location = new System.Drawing.Point(277, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 256);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbSerialOut);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(277, 256);
            this.panel4.TabIndex = 0;
            // 
            // tbSerialOut
            // 
            this.tbSerialOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSerialOut.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSerialOut.Location = new System.Drawing.Point(0, 0);
            this.tbSerialOut.Multiline = true;
            this.tbSerialOut.Name = "tbSerialOut";
            this.tbSerialOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSerialOut.Size = new System.Drawing.Size(277, 256);
            this.tbSerialOut.TabIndex = 0;
            this.tbSerialOut.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnClearText);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 532);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(549, 37);
            this.panel3.TabIndex = 0;
            // 
            // btnClearText
            // 
            this.btnClearText.Location = new System.Drawing.Point(323, 6);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new System.Drawing.Size(75, 23);
            this.btnClearText.TabIndex = 11;
            this.btnClearText.Text = "Clear &Text";
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new System.EventHandler(this.OnClickClearText);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(462, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // ProgramEEPROMDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 569);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgramEEPROMDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Program EEPROM";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblVerify;
        private System.Windows.Forms.Label lblWrite;
        private System.Windows.Forms.Label lblRead;
        private System.Windows.Forms.Label lblCheckResult;
        private System.Windows.Forms.Label lblCheck;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbComPort;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbSerialIn;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbSerialOut;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnClearText;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblVerifyResult;
        private System.Windows.Forms.Label lblWriteResult;
        private System.Windows.Forms.Label lblReadResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
    }
}