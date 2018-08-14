using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LptPrnControl
{
    public partial class Form1 : Form
    {
        int SendComing = 0, txtOutState = 0;
        long oldTicks = DateTime.Now.Ticks, limitTick = 200;
        int LogLinesLimit = 100;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);
        bool IsConnected = false;
        SafeFileHandle port;
        FileStream LptPort;
        public bool LptOpen(string lptP)
        {
            try
            {
                port = CreateFile(lptP, FileAccess.ReadWrite, 0, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
                if (port.IsInvalid)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    IsConnected = false;
                    return IsConnected;
                }
                else
                {
                    IsConnected = true;
                    LptPort = new FileStream(port, FileAccess.ReadWrite);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return IsConnected;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            port = CreateFile(textBox_saveTo.Text, FileAccess.ReadWrite, 0, IntPtr.Zero, FileMode.OpenOrCreate, 0, IntPtr.Zero);
            LptPort = new FileStream(port, FileAccess.Read);
            LptPort.Close();
            port = null;

            comboBox_portname1.Items.Clear();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ParallelPort");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    comboBox_portname1.Items.Add(queryObj["Name"]);
                }
            }
            catch (ManagementException)
            {
                MessageBox.Show("An error occurred while querying for WMI data");
            }

            if (comboBox_portname1.Items.Count > 0) comboBox_portname1.SelectedIndex = 0;
            else comboBox_portname1.Items.Add("No LPT ports found");


            checkBox_hexCommand.Checked = LptPrnControl.Properties.Settings.Default.checkBox_hexCommand;
            textBox_command.Text = LptPrnControl.Properties.Settings.Default.textBox_command;
            checkBox_hexParam.Checked = LptPrnControl.Properties.Settings.Default.checkBox_hexParam;
            textBox_param.Text = LptPrnControl.Properties.Settings.Default.textBox_param;
            limitTick = LptPrnControl.Properties.Settings.Default.LineBreakTimeout;
            limitTick *= 10000;
            LogLinesLimit = LptPrnControl.Properties.Settings.Default.LogLinesLimit;
        }

        private void checkBox_hexCommand_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.ConvertStringToHex(textBox_command.Text);
            else textBox_command.Text = Accessory.ConvertHexToString(textBox_command.Text);
        }

        private void checkBox_hexParam_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.ConvertStringToHex(textBox_param.Text);
            else textBox_param.Text = Accessory.ConvertHexToString(textBox_param.Text);
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_terminal.Clear();
        }

        private void textBox_command_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.CheckHexString(textBox_command.Text);
        }

        private void textBox_param_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.CheckHexString(textBox_param.Text);
        }

        private void button_openport_Click(object sender, EventArgs e)
        {
            if (comboBox_portname1.Items.Count > 0 && comboBox_portname1.SelectedItem.ToString() != "No LPT ports found")
            {
                if (!IsConnected) LptOpen(comboBox_portname1.SelectedItem.ToString());
                if (!IsConnected)
                {
                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem.ToString());
                    comboBox_portname1.Enabled = true;
                    return;
                }
                comboBox_portname1.Enabled = false;
                button_closeport.Enabled = true;
                button_openport.Enabled = false;
                button_Send.Enabled = true;
                button_sendFile.Enabled = true;
            }
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            byte[] buff = { };
            if (textBox_command.Text + textBox_command.Text != "")
            {
                string sendStrHex = "";
                if (checkBox_hexCommand.Checked) sendStrHex = textBox_command.Text;
                else sendStrHex = Accessory.ConvertStringToHex(textBox_command.Text);
                if (checkBox_hexParam.Checked) sendStrHex += textBox_param.Text;
                else sendStrHex += Accessory.ConvertStringToHex(textBox_param.Text);

                if (sendStrHex != "")
                {
                    LptOpen(comboBox_portname1.SelectedItem.ToString());
                    if (!IsConnected)
                    {
                        MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem.ToString());
                        comboBox_portname1.Enabled = true;
                        return;
                    }
                    else
                    {
                        if (checkBox_hexTerminal.Checked) SetText(">> " + sendStrHex + "\r\n");
                        else SetText(">> " + Accessory.ConvertHexToString(sendStrHex) + "\r\n");
                        textBox_command.AutoCompleteCustomSource.Add(textBox_command.Text);
                        textBox_param.AutoCompleteCustomSource.Add(textBox_param.Text);
                        try
                        {
                            LptPort.Write(Accessory.ConvertHexToByteArray(sendStrHex), 0, sendStrHex.Length / 3);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem.ToString() + ": " + ex.Message);
                        }
                    }
                }
            }
        }

        private void button_closeport_Click(object sender, EventArgs e)
        {
            if (IsConnected) LptPort.Close();
            IsConnected = false;
            comboBox_portname1.Enabled = true;
            button_Send.Enabled = false;
            button_sendFile.Enabled = false;
            button_openport.Enabled = true;
            button_closeport.Enabled = false;
        }

        private void checkBox_saveTo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveTo.Checked) textBox_saveTo.Enabled = false;
            else textBox_saveTo.Enabled = true;
        }

        private void button_openFile_Click(object sender, EventArgs e)
        {
            if (checkBox_hexFileOpen.Checked)
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Open file";
                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.Filter = "Text files|*.txt|HEX files|*.hex|All files|*.*";
                openFileDialog1.ShowDialog();
            }
            else
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Open file";
                openFileDialog1.DefaultExt = "bin";
                openFileDialog1.Filter = "BIN files|*.bin|PRN files|*.prn|All files|*.*";
                openFileDialog1.ShowDialog();
            }
        }

        private async void button_sendFile_ClickAsync(object sender, EventArgs e)
        {
            if (SendComing > 0)
            {
                SendComing++;
            }
            else if (SendComing == 0)
            {
                int repeat = 1, delay = 1, strDelay = 1;
                if (textBox_fileName.Text != "" && textBox_sendNum.Text != "" && Int32.TryParse(textBox_sendNum.Text, out repeat) && Int32.TryParse(textBox_delay.Text, out delay) && Int32.TryParse(textBox_strDelay.Text, out strDelay))
                {
                    SendComing = 1;
                    button_Send.Enabled = false;
                    button_closeport.Enabled = false;
                    button_openFile.Enabled = false;
                    button_sendFile.Text = "Stop";
                    textBox_fileName.Enabled = false;
                    textBox_sendNum.Enabled = false;
                    textBox_delay.Enabled = false;
                    textBox_strDelay.Enabled = false;
                    for (int n = 0; n < repeat; n++)
                    {
                        string outStr;
                        long length = 0;
                        try
                        {
                            length = new System.IO.FileInfo(textBox_fileName.Text).Length;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\r\nError opening file " + textBox_fileName.Text + ": " + ex.Message);
                        }

                        if (!checkBox_hexFileOpen.Checked)  //binary data read
                        {
                            if (radioButton_byByte.Checked) //byte-by-byte
                            {
                                byte[] tmpBuffer = new byte[length];
                                try
                                {
                                    tmpBuffer = System.IO.File.ReadAllBytes(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!IsConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem.ToString());
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }
                                else
                                {

                                    try
                                    {
                                        for (int m = 0; m < tmpBuffer.Length; m++)
                                        {
                                            LptPort.Write(tmpBuffer, m, 1);
                                            progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                            if (strDelay > 0) await TaskEx.Delay(strDelay);
                                            //ReadLPT();
                                            outStr = ">> ";
                                            if (checkBox_hexTerminal.Checked) outStr += Accessory.ConvertByteArrayToHex(tmpBuffer);
                                            else outStr += Accessory.ConvertHexToString(Accessory.ConvertByteArrayToHex(tmpBuffer));
                                            outStr += "\r\n";
                                            SetText(outStr);
                                            if (SendComing > 1) m = tmpBuffer.Length;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem.ToString() + ": " + ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                byte[] tmpBuffer = new byte[length];
                                try
                                {
                                    tmpBuffer = System.IO.File.ReadAllBytes(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!IsConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem.ToString());
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }
                                else
                                {

                                    try
                                    {
                                        LptPort.Write(tmpBuffer, 0, tmpBuffer.Length);
                                        //ReadLPT();
                                        outStr = ">> ";
                                        if (checkBox_hexTerminal.Checked) outStr += Accessory.ConvertByteArrayToHex(tmpBuffer);
                                        else outStr += Accessory.ConvertHexToString(Accessory.ConvertByteArrayToHex(tmpBuffer));
                                        outStr += "\r\n";
                                        SetText(outStr);
                                        progressBar1.Value = (n * 100) / (repeat * tmpBuffer.Length);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem.ToString() + ": " + ex.Message);
                                    }
                                }
                            }
                        }
                        else  //hex text read
                        {
                            if (radioButton_byString.Checked) //String-by-string
                            {
                                String[] tmpBuffer = { };
                                try
                                {
                                    length = new System.IO.FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = System.IO.File.ReadAllText(textBox_fileName.Text).Replace("\n", "").Split('\r');
                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!IsConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem.ToString());
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }
                                else
                                {

                                    try
                                    {
                                        for (int m = 0; m < tmpBuffer.Length; m++)
                                        {
                                            tmpBuffer[m] = Accessory.CheckHexString(tmpBuffer[m]);
                                            LptPort.Write(Accessory.ConvertHexToByteArray(tmpBuffer[m]), 0, tmpBuffer[m].Length / 3);
                                            outStr = ">> ";
                                            if (checkBox_hexTerminal.Checked) outStr += tmpBuffer[m];
                                            else outStr += Accessory.ConvertHexToString(tmpBuffer[m].ToString());
                                            outStr += "\r\n";
                                            if (strDelay > 0) await TaskEx.Delay(strDelay);
                                            //ReadLPT();
                                            if (SendComing > 1) m = tmpBuffer.Length;
                                            SetText(outStr);
                                            progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem.ToString() + ": " + ex.Message);
                                    }
                                }
                                if (repeat > 1) outStr = "\r\nSend cycle " + (n + 1).ToString() + "/" + repeat.ToString() + "\r\n";
                                else outStr = "";
                                //if (checkBox_hexTerminal.Checked) outStr += tmpBuffer;
                                //else outStr += ConvertHexToString(tmpBuffer.ToString());
                            }
                            else if (radioButton_byByte.Checked) //byte-by-byte
                            {
                                String tmpBuffer = "";
                                try
                                {
                                    length = new System.IO.FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = System.IO.File.ReadAllText(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }
                                tmpBuffer = Accessory.CheckHexString(tmpBuffer);

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!IsConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem.ToString());
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }
                                else
                                {
                                    try
                                    {
                                        for (int m = 0; m < tmpBuffer.Length; m += 3)
                                        {
                                            LptPort.Write(Accessory.ConvertHexToByteArray(tmpBuffer.Substring(m, 3)), 0, 1);
                                            outStr = ">> ";
                                            if (checkBox_hexTerminal.Checked) outStr += tmpBuffer.Substring(m, 3);
                                            else outStr += Accessory.ConvertHexToString(tmpBuffer.Substring(m, 3));
                                            outStr += "\r\n";
                                            if (strDelay > 0) await TaskEx.Delay(strDelay);
                                            //ReadLPT();
                                            progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                            if (SendComing > 1) m = tmpBuffer.Length;
                                            SetText(outStr);
                                            progressBar1.Value = (n * tmpBuffer.Length + m) * 100 / (repeat * tmpBuffer.Length);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem.ToString() + ": " + ex.Message);
                                    }
                                }
                                if (repeat > 1) outStr = " Send cycle " + (n + 1).ToString() + "/" + repeat.ToString() + ">> ";
                                else outStr = "";
                            }
                            else //raw stream
                            {
                                String tmpBuffer = "";
                                try
                                {
                                    length = new System.IO.FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = Accessory.CheckHexString(System.IO.File.ReadAllText(textBox_fileName.Text));
                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " + ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!IsConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem.ToString());
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }
                                else
                                {
                                    try
                                    {
                                        LptPort.Write(Accessory.ConvertHexToByteArray(tmpBuffer), 0, tmpBuffer.Length / 3);
                                        //ReadLPT();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem.ToString() + ": " + ex.Message);
                                    }
                                }
                                if (repeat > 1) outStr = " Send cycle " + (n + 1).ToString() + "/" + repeat.ToString() + ">> ";
                                else outStr = ">> ";
                                if (checkBox_hexTerminal.Checked) outStr += tmpBuffer;
                                else outStr += Accessory.ConvertHexToString(tmpBuffer);
                                outStr += "\r\n";
                                SetText(outStr);
                                progressBar1.Value = (n * 100) / (repeat * tmpBuffer.Length);
                            }
                        }
                        if (repeat > 1) await TaskEx.Delay(delay);
                        if (SendComing > 1) n = repeat;
                    }
                    button_Send.Enabled = true;
                    button_closeport.Enabled = true;
                    button_openFile.Enabled = true;
                    button_sendFile.Text = "Send file";
                    textBox_fileName.Enabled = true;
                    textBox_sendNum.Enabled = true;
                    textBox_delay.Enabled = true;
                    textBox_strDelay.Enabled = true;
                }
                SendComing = 0;
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBox_fileName.Text = openFileDialog1.FileName;
        }

        private void checkBox_hexFileOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_hexFileOpen.Checked)
            {
                radioButton_byString.Enabled = false;
                if (radioButton_byString.Checked) radioButton_byByte.Checked = true;
                checkBox_hexFileOpen.Text = "binary data";
            }
            else
            {
                radioButton_byString.Enabled = true;
                checkBox_hexFileOpen.Text = "hex text data";
            }
        }

        private void textBox_command_KeyUp(object sender, KeyEventArgs e)
        {
            if (button_Send.Enabled)
            {
                if (e.KeyData == Keys.Return)
                {
                    button_Send_Click(textBox_command, EventArgs.Empty);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LptPrnControl.Properties.Settings.Default.checkBox_hexCommand = checkBox_hexCommand.Checked;
            LptPrnControl.Properties.Settings.Default.textBox_command = textBox_command.Text;
            LptPrnControl.Properties.Settings.Default.checkBox_hexParam = checkBox_hexParam.Checked;
            LptPrnControl.Properties.Settings.Default.textBox_param = textBox_param.Text;
            LptPrnControl.Properties.Settings.Default.Save();
            //port.Dispose();
            //LptPort.Dispose();
        }

        private void radioButton_stream_CheckedChanged(object sender, EventArgs e)
        {
            textBox_strDelay.Enabled = !radioButton_stream.Checked;
        }

        delegate void SetTextCallback1(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textBox_terminal1.InvokeRequired)
            if (textBox_terminal.InvokeRequired)
            {
                SetTextCallback1 d = new SetTextCallback1(SetText);
                BeginInvoke(d, new object[] { text });
            }
            else
            {
                int pos = textBox_terminal.SelectionStart;
                textBox_terminal.AppendText(text);
                if (textBox_terminal.Lines.Length > LogLinesLimit)
                {
                    StringBuilder tmp = new StringBuilder();
                    for (int i = textBox_terminal.Lines.Length - LogLinesLimit; i < textBox_terminal.Lines.Length; i++)
                    {
                        tmp.Append(textBox_terminal.Lines[i]);
                        tmp.Append("\r\n");
                    }
                    textBox_terminal.Text = tmp.ToString();
                }
                if (checkBox_autoscroll.Checked)
                {
                    textBox_terminal.SelectionStart = textBox_terminal.Text.Length;
                    textBox_terminal.ScrollToCaret();
                }
                else
                {
                    textBox_terminal.SelectionStart = pos;
                    textBox_terminal.ScrollToCaret();
                }
            }
        }

        public const byte Port1DataIn = 11;
        public const byte Port1DataOut = 12;
        public const byte Port1SignalIn = 13;
        public const byte Port1SignalOut = 14;
        public const byte Port1Error = 15;

        private object threadLock = new object();
        public void collectBuffer(string tmpBuffer, int state)
        {
            if (tmpBuffer != "")
            {
                string time = DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond.ToString("D3");
                lock (threadLock)
                {
                    if (!(txtOutState == state && (DateTime.Now.Ticks - oldTicks) < limitTick && state != Port1DataOut))
                    {
                        if (state == Port1DataIn) tmpBuffer = "<< " + tmpBuffer;         //sending data
                        else if (state == Port1DataOut) tmpBuffer = ">> " + tmpBuffer;    //receiving data
                        else if (state == Port1SignalIn) tmpBuffer = "<< " + tmpBuffer;    //pin change received
                        else if (state == Port1SignalOut) tmpBuffer = ">> " + tmpBuffer;    //pin changed by user
                        else if (state == Port1Error) tmpBuffer = "!! " + tmpBuffer;    //error occured

                        if (checkBox_saveTime.Checked == true) tmpBuffer = time + " " + tmpBuffer;
                        tmpBuffer = "\r\n" + tmpBuffer;
                        txtOutState = state;
                    }
                    if ((checkBox_saveInput.Checked == true && state == Port1DataIn) || (checkBox_saveOutput.Checked == true && state == Port1DataOut))
                    {
                        try
                        {
                            File.AppendAllText(textBox_saveTo.Text, tmpBuffer, Encoding.GetEncoding(LptPrnControl.Properties.Settings.Default.CodePage));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\r\nError opening file " + textBox_saveTo.Text + ": " + ex.Message);
                        }
                    }
                    SetText(tmpBuffer);
                    oldTicks = DateTime.Now.Ticks;
                }
            }
        }
    }
}
