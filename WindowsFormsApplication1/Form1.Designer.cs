namespace LptPrnControl
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            LptPort.Dispose();
            if (disposing && (components != null))
            {                
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_closeport = new System.Windows.Forms.Button();
            this.button_openport = new System.Windows.Forms.Button();
            this.button_Clear = new System.Windows.Forms.Button();
            this.textBox_param = new System.Windows.Forms.TextBox();
            this.checkBox_hexParam = new System.Windows.Forms.CheckBox();
            this.checkBox_hexTerminal = new System.Windows.Forms.CheckBox();
            this.checkBox_autoscroll = new System.Windows.Forms.CheckBox();
            this.checkBox_hexCommand = new System.Windows.Forms.CheckBox();
            this.textBox_terminal = new System.Windows.Forms.TextBox();
            this.textBox_command = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.textBox_saveTo = new System.Windows.Forms.TextBox();
            this.checkBox_saveTo = new System.Windows.Forms.CheckBox();
            this.button_openFile = new System.Windows.Forms.Button();
            this.textBox_fileName = new System.Windows.Forms.TextBox();
            this.checkBox_hexFileOpen = new System.Windows.Forms.CheckBox();
            this.button_sendFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox_delay = new System.Windows.Forms.TextBox();
            this.textBox_sendNum = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_strDelay = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.radioButton_byString = new System.Windows.Forms.RadioButton();
            this.radioButton_byByte = new System.Windows.Forms.RadioButton();
            this.radioButton_stream = new System.Windows.Forms.RadioButton();
            this.label_LptPort = new System.Windows.Forms.Label();
            this.comboBox_portname1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_saveTime = new System.Windows.Forms.CheckBox();
            this.checkBox_saveOutput = new System.Windows.Forms.CheckBox();
            this.checkBox_saveInput = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_closeport
            // 
            this.button_closeport.Enabled = false;
            this.button_closeport.Location = new System.Drawing.Point(247, 8);
            this.button_closeport.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_closeport.Name = "button_closeport";
            this.button_closeport.Size = new System.Drawing.Size(70, 25);
            this.button_closeport.TabIndex = 8;
            this.button_closeport.Text = "Close";
            this.button_closeport.UseVisualStyleBackColor = true;
            this.button_closeport.Click += new System.EventHandler(this.button_closeport_Click);
            // 
            // button_openport
            // 
            this.button_openport.Location = new System.Drawing.Point(170, 8);
            this.button_openport.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_openport.Name = "button_openport";
            this.button_openport.Size = new System.Drawing.Size(70, 25);
            this.button_openport.TabIndex = 7;
            this.button_openport.Text = "Open";
            this.button_openport.UseVisualStyleBackColor = true;
            this.button_openport.Click += new System.EventHandler(this.button_openport_Click);
            // 
            // button_Clear
            // 
            this.button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Clear.Location = new System.Drawing.Point(486, 225);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(70, 25);
            this.button_Clear.TabIndex = 23;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // textBox_param
            // 
            this.textBox_param.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_param.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_param.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_param.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_param.Location = new System.Drawing.Point(186, 62);
            this.textBox_param.Name = "textBox_param";
            this.textBox_param.Size = new System.Drawing.Size(370, 20);
            this.textBox_param.TabIndex = 13;
            this.textBox_param.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_command_KeyUp);
            this.textBox_param.Leave += new System.EventHandler(this.textBox_param_Leave);
            // 
            // checkBox_hexParam
            // 
            this.checkBox_hexParam.AutoSize = true;
            this.checkBox_hexParam.Checked = true;
            this.checkBox_hexParam.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexParam.Location = new System.Drawing.Point(88, 64);
            this.checkBox_hexParam.Name = "checkBox_hexParam";
            this.checkBox_hexParam.Size = new System.Drawing.Size(93, 17);
            this.checkBox_hexParam.TabIndex = 12;
            this.checkBox_hexParam.Text = "hex parameter";
            this.checkBox_hexParam.UseVisualStyleBackColor = true;
            this.checkBox_hexParam.CheckedChanged += new System.EventHandler(this.checkBox_hexParam_CheckedChanged);
            // 
            // checkBox_hexTerminal
            // 
            this.checkBox_hexTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_hexTerminal.AutoSize = true;
            this.checkBox_hexTerminal.Checked = true;
            this.checkBox_hexTerminal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexTerminal.Location = new System.Drawing.Point(90, 233);
            this.checkBox_hexTerminal.Name = "checkBox_hexTerminal";
            this.checkBox_hexTerminal.Size = new System.Drawing.Size(45, 17);
            this.checkBox_hexTerminal.TabIndex = 20;
            this.checkBox_hexTerminal.Text = "Hex";
            this.checkBox_hexTerminal.UseVisualStyleBackColor = true;
            // 
            // checkBox_autoscroll
            // 
            this.checkBox_autoscroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_autoscroll.AutoSize = true;
            this.checkBox_autoscroll.Checked = true;
            this.checkBox_autoscroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_autoscroll.Location = new System.Drawing.Point(12, 233);
            this.checkBox_autoscroll.Name = "checkBox_autoscroll";
            this.checkBox_autoscroll.Size = new System.Drawing.Size(72, 17);
            this.checkBox_autoscroll.TabIndex = 19;
            this.checkBox_autoscroll.Text = "Autoscroll";
            this.checkBox_autoscroll.UseVisualStyleBackColor = true;
            // 
            // checkBox_hexCommand
            // 
            this.checkBox_hexCommand.AutoSize = true;
            this.checkBox_hexCommand.Checked = true;
            this.checkBox_hexCommand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexCommand.Location = new System.Drawing.Point(88, 37);
            this.checkBox_hexCommand.Name = "checkBox_hexCommand";
            this.checkBox_hexCommand.Size = new System.Drawing.Size(92, 17);
            this.checkBox_hexCommand.TabIndex = 10;
            this.checkBox_hexCommand.Text = "hex command";
            this.checkBox_hexCommand.UseVisualStyleBackColor = true;
            this.checkBox_hexCommand.CheckedChanged += new System.EventHandler(this.checkBox_hexCommand_CheckedChanged);
            // 
            // textBox_terminal
            // 
            this.textBox_terminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_terminal.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_terminal.Location = new System.Drawing.Point(12, 150);
            this.textBox_terminal.Multiline = true;
            this.textBox_terminal.Name = "textBox_terminal";
            this.textBox_terminal.ReadOnly = true;
            this.textBox_terminal.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_terminal.Size = new System.Drawing.Size(544, 69);
            this.textBox_terminal.TabIndex = 18;
            // 
            // textBox_command
            // 
            this.textBox_command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_command.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_command.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_command.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_command.Location = new System.Drawing.Point(186, 35);
            this.textBox_command.Name = "textBox_command";
            this.textBox_command.Size = new System.Drawing.Size(370, 20);
            this.textBox_command.TabIndex = 11;
            this.textBox_command.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_command_KeyUp);
            this.textBox_command.Leave += new System.EventHandler(this.textBox_command_Leave);
            // 
            // button_Send
            // 
            this.button_Send.Enabled = false;
            this.button_Send.Location = new System.Drawing.Point(12, 35);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(70, 47);
            this.button_Send.TabIndex = 87;
            this.button_Send.Text = "Send";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
            // 
            // textBox_saveTo
            // 
            this.textBox_saveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox_saveTo.Location = new System.Drawing.Point(408, 230);
            this.textBox_saveTo.Name = "textBox_saveTo";
            this.textBox_saveTo.Size = new System.Drawing.Size(70, 20);
            this.textBox_saveTo.TabIndex = 22;
            this.textBox_saveTo.Text = "lpt_rx.txt";
            // 
            // checkBox_saveTo
            // 
            this.checkBox_saveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_saveTo.AutoSize = true;
            this.checkBox_saveTo.Checked = true;
            this.checkBox_saveTo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_saveTo.Location = new System.Drawing.Point(338, 233);
            this.checkBox_saveTo.Name = "checkBox_saveTo";
            this.checkBox_saveTo.Size = new System.Drawing.Size(64, 17);
            this.checkBox_saveTo.TabIndex = 21;
            this.checkBox_saveTo.Text = "save to:";
            this.checkBox_saveTo.UseVisualStyleBackColor = true;
            this.checkBox_saveTo.CheckedChanged += new System.EventHandler(this.checkBox_saveTo_CheckedChanged);
            // 
            // button_openFile
            // 
            this.button_openFile.Location = new System.Drawing.Point(12, 88);
            this.button_openFile.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_openFile.Name = "button_openFile";
            this.button_openFile.Size = new System.Drawing.Size(70, 25);
            this.button_openFile.TabIndex = 16;
            this.button_openFile.Text = "Select file";
            this.button_openFile.UseVisualStyleBackColor = true;
            this.button_openFile.Click += new System.EventHandler(this.button_openFile_Click);
            // 
            // textBox_fileName
            // 
            this.textBox_fileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_fileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_fileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_fileName.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_fileName.Location = new System.Drawing.Point(186, 91);
            this.textBox_fileName.Name = "textBox_fileName";
            this.textBox_fileName.Size = new System.Drawing.Size(292, 20);
            this.textBox_fileName.TabIndex = 15;
            // 
            // checkBox_hexFileOpen
            // 
            this.checkBox_hexFileOpen.AutoSize = true;
            this.checkBox_hexFileOpen.Checked = true;
            this.checkBox_hexFileOpen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_hexFileOpen.Location = new System.Drawing.Point(88, 93);
            this.checkBox_hexFileOpen.Name = "checkBox_hexFileOpen";
            this.checkBox_hexFileOpen.Size = new System.Drawing.Size(87, 17);
            this.checkBox_hexFileOpen.TabIndex = 14;
            this.checkBox_hexFileOpen.Text = "hex text data";
            this.checkBox_hexFileOpen.UseVisualStyleBackColor = true;
            this.checkBox_hexFileOpen.CheckedChanged += new System.EventHandler(this.checkBox_hexFileOpen_CheckedChanged);
            // 
            // button_sendFile
            // 
            this.button_sendFile.Enabled = false;
            this.button_sendFile.Location = new System.Drawing.Point(12, 119);
            this.button_sendFile.MinimumSize = new System.Drawing.Size(70, 25);
            this.button_sendFile.Name = "button_sendFile";
            this.button_sendFile.Size = new System.Drawing.Size(70, 25);
            this.button_sendFile.TabIndex = 17;
            this.button_sendFile.Text = "Send file:";
            this.button_sendFile.UseVisualStyleBackColor = true;
            this.button_sendFile.Click += new System.EventHandler(this.button_sendFile_ClickAsync);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // textBox_delay
            // 
            this.textBox_delay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_delay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_delay.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_delay.Location = new System.Drawing.Point(460, 123);
            this.textBox_delay.MaxLength = 5;
            this.textBox_delay.Name = "textBox_delay";
            this.textBox_delay.Size = new System.Drawing.Size(55, 20);
            this.textBox_delay.TabIndex = 101;
            this.textBox_delay.Text = "1000";
            // 
            // textBox_sendNum
            // 
            this.textBox_sendNum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_sendNum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_sendNum.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_sendNum.Location = new System.Drawing.Point(382, 122);
            this.textBox_sendNum.MaxLength = 5;
            this.textBox_sendNum.Name = "textBox_sendNum";
            this.textBox_sendNum.Size = new System.Drawing.Size(32, 20);
            this.textBox_sendNum.TabIndex = 102;
            this.textBox_sendNum.Text = "001";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(521, 126);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(51, 13);
            this.label24.TabIndex = 103;
            this.label24.Text = "ms. delay";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(420, 125);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(34, 13);
            this.label23.TabIndex = 104;
            this.label23.Text = "times;";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(325, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 103;
            this.label1.Text = "ms. delay";
            // 
            // textBox_strDelay
            // 
            this.textBox_strDelay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox_strDelay.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_strDelay.Enabled = false;
            this.textBox_strDelay.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_strDelay.Location = new System.Drawing.Point(285, 122);
            this.textBox_strDelay.MaxLength = 5;
            this.textBox_strDelay.Name = "textBox_strDelay";
            this.textBox_strDelay.Size = new System.Drawing.Size(34, 20);
            this.textBox_strDelay.TabIndex = 101;
            this.textBox_strDelay.Text = "10";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(484, 91);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(72, 20);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 105;
            // 
            // radioButton_byString
            // 
            this.radioButton_byString.AutoSize = true;
            this.radioButton_byString.Location = new System.Drawing.Point(150, 122);
            this.radioButton_byString.Name = "radioButton_byString";
            this.radioButton_byString.Size = new System.Drawing.Size(64, 17);
            this.radioButton_byString.TabIndex = 106;
            this.radioButton_byString.TabStop = true;
            this.radioButton_byString.Text = "by string";
            this.radioButton_byString.UseVisualStyleBackColor = true;
            // 
            // radioButton_byByte
            // 
            this.radioButton_byByte.AutoSize = true;
            this.radioButton_byByte.Location = new System.Drawing.Point(220, 123);
            this.radioButton_byByte.Name = "radioButton_byByte";
            this.radioButton_byByte.Size = new System.Drawing.Size(59, 17);
            this.radioButton_byByte.TabIndex = 106;
            this.radioButton_byByte.TabStop = true;
            this.radioButton_byByte.Text = "by byte";
            this.radioButton_byByte.UseVisualStyleBackColor = true;
            // 
            // radioButton_stream
            // 
            this.radioButton_stream.AutoSize = true;
            this.radioButton_stream.Checked = true;
            this.radioButton_stream.Location = new System.Drawing.Point(88, 123);
            this.radioButton_stream.Name = "radioButton_stream";
            this.radioButton_stream.Size = new System.Drawing.Size(56, 17);
            this.radioButton_stream.TabIndex = 106;
            this.radioButton_stream.TabStop = true;
            this.radioButton_stream.Text = "stream";
            this.radioButton_stream.UseVisualStyleBackColor = true;
            this.radioButton_stream.CheckedChanged += new System.EventHandler(this.radioButton_stream_CheckedChanged);
            // 
            // label_LptPort
            // 
            this.label_LptPort.AutoSize = true;
            this.label_LptPort.Location = new System.Drawing.Point(12, 11);
            this.label_LptPort.Name = "label_LptPort";
            this.label_LptPort.Size = new System.Drawing.Size(49, 13);
            this.label_LptPort.TabIndex = 107;
            this.label_LptPort.Text = "LPT Port";
            // 
            // comboBox_portname1
            // 
            this.comboBox_portname1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox_portname1.FormattingEnabled = true;
            this.comboBox_portname1.Items.AddRange(new object[] {
            "LPT1",
            "LPT2",
            "LPT3",
            "LPT4"});
            this.comboBox_portname1.Location = new System.Drawing.Point(67, 8);
            this.comboBox_portname1.Name = "comboBox_portname1";
            this.comboBox_portname1.Size = new System.Drawing.Size(97, 21);
            this.comboBox_portname1.TabIndex = 108;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 125;
            this.label3.Text = "Save";
            // 
            // checkBox_saveTime
            // 
            this.checkBox_saveTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_saveTime.AutoSize = true;
            this.checkBox_saveTime.Checked = true;
            this.checkBox_saveTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_saveTime.Location = new System.Drawing.Point(170, 233);
            this.checkBox_saveTime.Name = "checkBox_saveTime";
            this.checkBox_saveTime.Size = new System.Drawing.Size(45, 17);
            this.checkBox_saveTime.TabIndex = 124;
            this.checkBox_saveTime.Text = "time";
            this.checkBox_saveTime.UseVisualStyleBackColor = true;
            // 
            // checkBox_saveOutput
            // 
            this.checkBox_saveOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_saveOutput.AutoSize = true;
            this.checkBox_saveOutput.Checked = true;
            this.checkBox_saveOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_saveOutput.Location = new System.Drawing.Point(276, 233);
            this.checkBox_saveOutput.Name = "checkBox_saveOutput";
            this.checkBox_saveOutput.Size = new System.Drawing.Size(56, 17);
            this.checkBox_saveOutput.TabIndex = 122;
            this.checkBox_saveOutput.Text = "output";
            this.checkBox_saveOutput.UseVisualStyleBackColor = true;
            // 
            // checkBox_saveInput
            // 
            this.checkBox_saveInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_saveInput.AutoSize = true;
            this.checkBox_saveInput.Enabled = false;
            this.checkBox_saveInput.Location = new System.Drawing.Point(221, 233);
            this.checkBox_saveInput.Name = "checkBox_saveInput";
            this.checkBox_saveInput.Size = new System.Drawing.Size(49, 17);
            this.checkBox_saveInput.TabIndex = 123;
            this.checkBox_saveInput.Text = "input";
            this.checkBox_saveInput.UseVisualStyleBackColor = true;
            this.checkBox_saveInput.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox_saveTime);
            this.Controls.Add(this.checkBox_saveOutput);
            this.Controls.Add(this.checkBox_saveInput);
            this.Controls.Add(this.label_LptPort);
            this.Controls.Add(this.comboBox_portname1);
            this.Controls.Add(this.radioButton_stream);
            this.Controls.Add(this.radioButton_byByte);
            this.Controls.Add(this.radioButton_byString);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBox_strDelay);
            this.Controls.Add(this.textBox_delay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_sendNum);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.button_openFile);
            this.Controls.Add(this.textBox_fileName);
            this.Controls.Add(this.checkBox_hexFileOpen);
            this.Controls.Add(this.button_sendFile);
            this.Controls.Add(this.textBox_saveTo);
            this.Controls.Add(this.checkBox_saveTo);
            this.Controls.Add(this.button_Clear);
            this.Controls.Add(this.textBox_param);
            this.Controls.Add(this.checkBox_hexParam);
            this.Controls.Add(this.checkBox_hexTerminal);
            this.Controls.Add(this.checkBox_autoscroll);
            this.Controls.Add(this.checkBox_hexCommand);
            this.Controls.Add(this.textBox_terminal);
            this.Controls.Add(this.textBox_command);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.button_closeport);
            this.Controls.Add(this.button_openport);
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "Form1";
            this.Text = "LptPrnControl (c) Andrey Kalugin (jekyll@mail.ru), 2016";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_closeport;
        private System.Windows.Forms.Button button_openport;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.TextBox textBox_param;
        private System.Windows.Forms.CheckBox checkBox_hexParam;
        private System.Windows.Forms.CheckBox checkBox_hexTerminal;
        private System.Windows.Forms.CheckBox checkBox_autoscroll;
        private System.Windows.Forms.CheckBox checkBox_hexCommand;
        private System.Windows.Forms.TextBox textBox_terminal;
        private System.Windows.Forms.TextBox textBox_command;
        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.TextBox textBox_saveTo;
        private System.Windows.Forms.CheckBox checkBox_saveTo;
        private System.Windows.Forms.Button button_openFile;
        private System.Windows.Forms.TextBox textBox_fileName;
        private System.Windows.Forms.CheckBox checkBox_hexFileOpen;
        private System.Windows.Forms.Button button_sendFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox_delay;
        private System.Windows.Forms.TextBox textBox_sendNum;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_strDelay;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RadioButton radioButton_byString;
        private System.Windows.Forms.RadioButton radioButton_byByte;
        private System.Windows.Forms.RadioButton radioButton_stream;
        private System.Windows.Forms.Label label_LptPort;
        private System.Windows.Forms.ComboBox comboBox_portname1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_saveTime;
        private System.Windows.Forms.CheckBox checkBox_saveOutput;
        private System.Windows.Forms.CheckBox checkBox_saveInput;
    }
}

