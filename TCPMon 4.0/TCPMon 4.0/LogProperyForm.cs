using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace TCPMon_3._1
{
    public partial class LogPropertyForm : Form
    {
        public LogPropertyForm()
        {
            InitializeComponent();
        }

        private void LogPropertyForm_Load(object sender, EventArgs e)
        {
            int PIDS = PublicClass.ProcessPID_TO_Properties;
            string Path="";
            try
            {
                if (!Process.GetProcessById(PIDS).HasExited)
                {
                    Path = Process.GetProcessById(PIDS).MainModule.FileName;
                    textBox1.Text = Path;
                    _PID.Text = PIDS.ToString();
                    _Company.Text = Process.GetProcessById(PIDS).MainModule.FileVersionInfo.CompanyName;
                    _Ver.Text = Process.GetProcessById(PIDS).MainModule.FileVersionInfo.FileVersion;
                    _StartTime.Text = Process.GetProcessById(PIDS).StartTime.ToString();                   
                    
                }
                if (File.Exists(Path))
                {
                    _FileName.Text = new FileInfo(Path).Name;
                    _Size.Text = new FileInfo(Path).Length.ToString() + " bytes";
                    Text = Text + " For " + _FileName.Text;                    
                }
            }
            catch (Exception err)
            {
                try
                {
                    Path = Process.GetProcessById(PIDS).ProcessName;
                    textBox1.Text = Path;
                    Text = Text + " For " + Path;  
                }
                catch (Exception err1)
                {

                    MessageBox.Show(null, err1.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                }
                
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}