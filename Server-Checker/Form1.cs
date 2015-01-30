using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Server_Checker
{
    public partial class Form1 : Form
    {
        private Checker serverCheck;
        private System.Timers.Timer timeCheck;
        private Boolean status = false;

        public Form1()
        {
            InitializeComponent();

            serverCheck = new Checker(IPAddress.Parse(ipInput.Text), Convert.ToInt16(portInput.Text));
            timeCheck = new System.Timers.Timer();
            timeCheck.Interval = 100;
            timeCheck.Elapsed += new ElapsedEventHandler(CheckStatus);

            setOnline(serverCheck.Check());
        }

        private void CheckStatus(object sender, ElapsedEventArgs e)
        {
            if (autoButton.Checked) //Safety check
                this.BeginInvoke((MethodInvoker)delegate { setOnline(serverCheck.Check()); });
        }

        private void autoButton_CheckedChanged(object sender, EventArgs e)
        {
            ipInput.Enabled = !autoButton.Checked;
            portInput.Enabled = !autoButton.Checked;

            serverCheck.setIP(IPAddress.Parse(ipInput.Text));
            serverCheck.setPort(Convert.ToInt16(portInput.Text));

            timeCheck.Enabled = autoButton.Checked;
        }

        private void setOnline(bool online)
        {
            Console.WriteLine(online + " != " + status);
            if (online != status)
            {
                if (online)
                {
                    statusLabel.Text = "Online";
                    statusLabel.ForeColor = Color.Green;
                    status = online;
                    timeCheck.Interval = 5000;
                }
                else
                {
                    statusLabel.Text = "Offline";
                    statusLabel.ForeColor = Color.Red;
                    status = online;
                    timeCheck.Interval = 100;
                }
            }
        }
    }
}
