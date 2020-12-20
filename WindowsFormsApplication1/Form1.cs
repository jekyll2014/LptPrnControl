using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LptPrnControl.Properties;

namespace LptPrnControl
{
    public partial class Form1 : Form
    {
        private int _sendComing;
        private int _logLinesLimit = 100;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, uint dwShareMode,
            IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        private bool _isConnected;
        private SafeFileHandle _port;
        private FileStream _lptPort;

        private bool LptOpen(string lptP)
        {
            try
            {
                _port = CreateFile(lptP, FileAccess.ReadWrite, 0, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
                if (_port.IsInvalid)
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                    _isConnected = false;
                    return _isConnected;
                }

                _isConnected = true;
                _lptPort = new FileStream(_port, FileAccess.ReadWrite);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return _isConnected;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _port = CreateFile(textBox_saveTo.Text, FileAccess.ReadWrite, 0, IntPtr.Zero, FileMode.OpenOrCreate, 0,
                IntPtr.Zero);
            _lptPort = new FileStream(_port, FileAccess.Read);
            _lptPort.Close();
            _port = null;

            comboBox_portname1.Items.Clear();
            try
            {
                var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_ParallelPort");
                foreach (ManagementObject queryObj in searcher.Get()) comboBox_portname1.Items.Add(queryObj["Name"]);
            }
            catch (ManagementException)
            {
                MessageBox.Show("An error occurred while querying for WMI data");
            }

            if (comboBox_portname1.Items.Count > 0) comboBox_portname1.SelectedIndex = 0;
            else comboBox_portname1.Items.Add("No LPT ports found");


            checkBox_hexCommand.Checked = Settings.Default.checkBox_hexCommand;
            textBox_command.Text = Settings.Default.textBox_command;
            checkBox_hexParam.Checked = Settings.Default.checkBox_hexParam;
            textBox_param.Text = Settings.Default.textBox_param;
            _logLinesLimit = Settings.Default.LogLinesLimit;
        }

        private void CheckBox_hexCommand_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.ConvertStringToHex(textBox_command.Text);
            else textBox_command.Text = Accessory.ConvertHexToString(textBox_command.Text);
        }

        private void CheckBox_hexParam_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.ConvertStringToHex(textBox_param.Text);
            else textBox_param.Text = Accessory.ConvertHexToString(textBox_param.Text);
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            textBox_terminal.Clear();
        }

        private void TextBox_command_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexCommand.Checked) textBox_command.Text = Accessory.CheckHexString(textBox_command.Text);
        }

        private void TextBox_param_Leave(object sender, EventArgs e)
        {
            if (checkBox_hexParam.Checked) textBox_param.Text = Accessory.CheckHexString(textBox_param.Text);
        }

        private void Button_openport_Click(object sender, EventArgs e)
        {
            if (comboBox_portname1.Items.Count > 0 &&
                comboBox_portname1.SelectedItem.ToString() != "No LPT ports found")
            {
                if (!_isConnected) LptOpen(comboBox_portname1.SelectedItem.ToString());
                if (!_isConnected)
                {
                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem);
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

        private void Button_Send_Click(object sender, EventArgs e)
        {
            byte[] buff = { };
            if (textBox_command.Text + textBox_command.Text != "")
            {
                var sendStrHex = "";
                if (checkBox_hexCommand.Checked) sendStrHex = textBox_command.Text;
                else sendStrHex = Accessory.ConvertStringToHex(textBox_command.Text);
                if (checkBox_hexParam.Checked) sendStrHex += textBox_param.Text;
                else sendStrHex += Accessory.ConvertStringToHex(textBox_param.Text);

                if (sendStrHex != "")
                {
                    LptOpen(comboBox_portname1.SelectedItem.ToString());
                    if (!_isConnected)
                    {
                        MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem);
                        comboBox_portname1.Enabled = true;
                    }
                    else
                    {
                        if (checkBox_hexTerminal.Checked) SetText(">> " + sendStrHex + "\r\n");
                        else SetText(">> " + Accessory.ConvertHexToString(sendStrHex) + "\r\n");
                        textBox_command.AutoCompleteCustomSource.Add(textBox_command.Text);
                        textBox_param.AutoCompleteCustomSource.Add(textBox_param.Text);
                        try
                        {
                            _lptPort.Write(Accessory.ConvertHexToByteArray(sendStrHex), 0, sendStrHex.Length / 3);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem + ": " +
                                            ex.Message);
                        }
                    }
                }
            }
        }

        private void Button_closeport_Click(object sender, EventArgs e)
        {
            if (_isConnected) _lptPort.Close();
            _isConnected = false;
            comboBox_portname1.Enabled = true;
            button_Send.Enabled = false;
            button_sendFile.Enabled = false;
            button_openport.Enabled = true;
            button_closeport.Enabled = false;
        }

        private void CheckBox_saveTo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_saveTo.Checked) textBox_saveTo.Enabled = false;
            else textBox_saveTo.Enabled = true;
        }

        private void Button_openFile_Click(object sender, EventArgs e)
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

        private async void Button_sendFile_ClickAsync(object sender, EventArgs e)
        {
            if (_sendComing > 0)
            {
                _sendComing++;
            }
            else if (_sendComing == 0)
            {
                int repeat = 1, delay = 1, strDelay = 1;
                if (textBox_fileName.Text != "" && textBox_sendNum.Text != "" &&
                    int.TryParse(textBox_sendNum.Text, out repeat) && int.TryParse(textBox_delay.Text, out delay) &&
                    int.TryParse(textBox_strDelay.Text, out strDelay))
                {
                    _sendComing = 1;
                    button_Send.Enabled = false;
                    button_closeport.Enabled = false;
                    button_openFile.Enabled = false;
                    button_sendFile.Text = "Stop";
                    textBox_fileName.Enabled = false;
                    textBox_sendNum.Enabled = false;
                    textBox_delay.Enabled = false;
                    textBox_strDelay.Enabled = false;
                    for (var n = 0; n < repeat; n++)
                    {
                        string outStr;
                        long length = 0;
                        try
                        {
                            length = new FileInfo(textBox_fileName.Text).Length;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("\r\nError opening file " + textBox_fileName.Text + ": " + ex.Message);
                        }

                        if (!checkBox_hexFileOpen.Checked) //binary data read
                        {
                            if (radioButton_byByte.Checked) //byte-by-byte
                            {
                                var tmpBuffer = new byte[length];
                                try
                                {
                                    tmpBuffer = File.ReadAllBytes(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " +
                                                    ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!_isConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem);
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }

                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        _lptPort.Write(tmpBuffer, m, 1);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 /
                                                             (repeat * tmpBuffer.Length);
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        //ReadLPT();
                                        outStr = ">> ";
                                        if (checkBox_hexTerminal.Checked)
                                            outStr += Accessory.ConvertByteArrayToHex(tmpBuffer);
                                        else
                                            outStr += Accessory.ConvertHexToString(
                                                Accessory.ConvertByteArrayToHex(tmpBuffer));
                                        outStr += "\r\n";
                                        SetText(outStr);
                                        if (_sendComing > 1) m = tmpBuffer.Length;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem + ": " +
                                                    ex.Message);
                                }
                            }
                            else
                            {
                                var tmpBuffer = new byte[length];
                                try
                                {
                                    tmpBuffer = File.ReadAllBytes(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " +
                                                    ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!_isConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem);
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }

                                try
                                {
                                    _lptPort.Write(tmpBuffer, 0, tmpBuffer.Length);
                                    //ReadLPT();
                                    outStr = ">> ";
                                    if (checkBox_hexTerminal.Checked)
                                        outStr += Accessory.ConvertByteArrayToHex(tmpBuffer);
                                    else
                                        outStr += Accessory.ConvertHexToString(
                                            Accessory.ConvertByteArrayToHex(tmpBuffer));
                                    outStr += "\r\n";
                                    SetText(outStr);
                                    progressBar1.Value = n * 100 / (repeat * tmpBuffer.Length);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem + ": " +
                                                    ex.Message);
                                }
                            }
                        }
                        else //hex text read
                        {
                            if (radioButton_byString.Checked) //String-by-string
                            {
                                string[] tmpBuffer = { };
                                try
                                {
                                    length = new FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = File.ReadAllText(textBox_fileName.Text).Replace("\n", "").Split('\r');
                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " +
                                                    ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!_isConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem);
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }

                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m++)
                                    {
                                        tmpBuffer[m] = Accessory.CheckHexString(tmpBuffer[m]);
                                        _lptPort.Write(Accessory.ConvertHexToByteArray(tmpBuffer[m]), 0,
                                            tmpBuffer[m].Length / 3);
                                        outStr = ">> ";
                                        if (checkBox_hexTerminal.Checked) outStr += tmpBuffer[m];
                                        else outStr += Accessory.ConvertHexToString(tmpBuffer[m]);
                                        outStr += "\r\n";
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        //ReadLPT();
                                        if (_sendComing > 1) m = tmpBuffer.Length;
                                        SetText(outStr);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 /
                                                             (repeat * tmpBuffer.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem + ": " +
                                                    ex.Message);
                                }

                                if (repeat > 1) outStr = "\r\nSend cycle " + (n + 1) + "/" + repeat + "\r\n";
                                else outStr = "";
                                //if (checkBox_hexTerminal.Checked) outStr += tmpBuffer;
                                //else outStr += ConvertHexToString(tmpBuffer.ToString());
                            }
                            else if (radioButton_byByte.Checked) //byte-by-byte
                            {
                                var tmpBuffer = "";
                                try
                                {
                                    length = new FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = File.ReadAllText(textBox_fileName.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " +
                                                    ex.Message);
                                }

                                tmpBuffer = Accessory.CheckHexString(tmpBuffer);

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!_isConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem);
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }

                                try
                                {
                                    for (var m = 0; m < tmpBuffer.Length; m += 3)
                                    {
                                        _lptPort.Write(Accessory.ConvertHexToByteArray(tmpBuffer.Substring(m, 3)), 0, 1);
                                        outStr = ">> ";
                                        if (checkBox_hexTerminal.Checked) outStr += tmpBuffer.Substring(m, 3);
                                        else outStr += Accessory.ConvertHexToString(tmpBuffer.Substring(m, 3));
                                        outStr += "\r\n";
                                        if (strDelay > 0) await TaskEx.Delay(strDelay);
                                        //ReadLPT();
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 /
                                                             (repeat * tmpBuffer.Length);
                                        if (_sendComing > 1) m = tmpBuffer.Length;
                                        SetText(outStr);
                                        progressBar1.Value = (n * tmpBuffer.Length + m) * 100 /
                                                             (repeat * tmpBuffer.Length);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem + ": " +
                                                    ex.Message);
                                }

                                if (repeat > 1) outStr = " Send cycle " + (n + 1) + "/" + repeat + ">> ";
                                else outStr = "";
                            }
                            else //raw stream
                            {
                                var tmpBuffer = "";
                                try
                                {
                                    length = new FileInfo(textBox_fileName.Text).Length;
                                    tmpBuffer = Accessory.CheckHexString(File.ReadAllText(textBox_fileName.Text));
                                }

                                catch (Exception ex)
                                {
                                    MessageBox.Show("\r\nError reading file " + textBox_fileName.Text + ": " +
                                                    ex.Message);
                                }

                                LptOpen(comboBox_portname1.SelectedItem.ToString());
                                if (!_isConnected)
                                {
                                    MessageBox.Show("Error opening port " + comboBox_portname1.SelectedItem);
                                    comboBox_portname1.Enabled = true;
                                    return;
                                }

                                try
                                {
                                    _lptPort.Write(Accessory.ConvertHexToByteArray(tmpBuffer), 0, tmpBuffer.Length / 3);
                                    //ReadLPT();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error sending to port " + comboBox_portname1.SelectedItem + ": " +
                                                    ex.Message);
                                }

                                if (repeat > 1) outStr = " Send cycle " + (n + 1) + "/" + repeat + ">> ";
                                else outStr = ">> ";
                                if (checkBox_hexTerminal.Checked) outStr += tmpBuffer;
                                else outStr += Accessory.ConvertHexToString(tmpBuffer);
                                outStr += "\r\n";
                                SetText(outStr);
                                progressBar1.Value = n * 100 / (repeat * tmpBuffer.Length);
                            }
                        }

                        if (repeat > 1) await TaskEx.Delay(delay);
                        if (_sendComing > 1) n = repeat;
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

                _sendComing = 0;
            }
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox_fileName.Text = openFileDialog1.FileName;
        }

        private void CheckBox_hexFileOpen_CheckedChanged(object sender, EventArgs e)
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

        private void TextBox_command_KeyUp(object sender, KeyEventArgs e)
        {
            if (button_Send.Enabled)
                if (e.KeyData == Keys.Return)
                    Button_Send_Click(textBox_command, EventArgs.Empty);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.checkBox_hexCommand = checkBox_hexCommand.Checked;
            Settings.Default.textBox_command = textBox_command.Text;
            Settings.Default.checkBox_hexParam = checkBox_hexParam.Checked;
            Settings.Default.textBox_param = textBox_param.Text;
            Settings.Default.Save();
            //port.Dispose();
            //LptPort.Dispose();
        }

        private void RadioButton_stream_CheckedChanged(object sender, EventArgs e)
        {
            textBox_strDelay.Enabled = !radioButton_stream.Checked;
        }

        private delegate void SetTextCallback1(string text);

        private void SetText(string text)
        {
            text = Accessory.FilterZeroChar(text);
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            //if (this.textBox_terminal1.InvokeRequired)
            if (textBox_terminal.InvokeRequired)
            {
                var d = new SetTextCallback1(SetText);
                BeginInvoke(d, text);
            }
            else
            {
                var pos = textBox_terminal.SelectionStart;
                textBox_terminal.AppendText(text);
                if (textBox_terminal.Lines.Length > _logLinesLimit)
                {
                    var tmp = new StringBuilder();
                    for (var i = textBox_terminal.Lines.Length - _logLinesLimit; i < textBox_terminal.Lines.Length; i++)
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
    }
}
