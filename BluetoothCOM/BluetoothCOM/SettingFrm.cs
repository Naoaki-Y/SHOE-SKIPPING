using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BluetoothCOM
{
    public partial class SettingFrm : Form
    {
        public int BaudRate { get; set; }
        public bool IsRunning { get; private set; }
        private string recvMessage;

        SerialPort serialPort;
        public SettingFrm()
        {
            InitializeComponent();
            this.BaudRate = 9600;
            comboBoxPort.SelectedIndex = 0;
        }

        private bool OpenCom()
        {
            /*
            try {
                this.serialPort = new SerialPort(this.comboBoxPort.SelectedItem.ToString(), BaudRate, Parity.None, 8, StopBits.One);
                this.serialPort.DataReceived += SerialPort_DataReceived;
                this.serialPort.ErrorReceived += SerialPort_ErrorReceived;
                this.serialPort.Open();
                this.IsRunning = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serialPort = null;
                return false;
            }
            */

            return true;
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (IsRunning)
            {
                btnStart.PerformClick();
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string recv = this.serialPort.ReadLine();

            if(recv == recvMessage)
            {
                return;
            }


            try {
                using (StreamWriter write = new StreamWriter(Path.Combine()))
                {
                    write.WriteLine(recvMessage);
                }
                recvMessage = recv;

                this.Invoke(new MethodInvoker(() => textBoxLog.Text += String.Format("{0} {1}{2}", DateTime.Now.ToString("HH:mm.ss.fff"), recvMessage, Environment.NewLine)));
            }
            catch
            {
            }

        }


        private bool CloseCom()
        {
            if(this.serialPort == null || !this.serialPort.IsOpen){
                return true;
            }

            /*
            try {
                serialPort.Close();
            }
            catch(IOException e)
            {
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            */
            return true;
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxPath.Text;

            if(folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            textBoxPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void SettingFrm_Load(object sender, EventArgs e)
        {
            this.textBoxPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            notifyIcon.Visible = true;
            notifyIcon.Icon = SystemIcons.Application;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                if (CloseCom())
                {
                    btnPath.Enabled = true;
                    textBoxPath.Enabled = true;
                    comboBoxPort.Enabled = true;
                    btnStart.Text = "START";
                }
            }
            else
            {

                if(!Directory.Exists(textBoxPath.Text))
                {
                    MessageBox.Show("Not Found. Directory", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (OpenCom())
                {
                    btnPath.Enabled = false;
                    textBoxPath.Enabled = false;
                    comboBoxPort.Enabled = false;
                    btnStart.Text = "STOP";
                }

            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void SettingFrm_ResizeEnd(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        private void SettingFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsRunning)
            {
                CloseCom();
            }
        }
    }
}
