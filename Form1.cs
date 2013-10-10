using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinProxy
{
    public partial class WinProxy : Form
    {

        public static String SERVICE_NAME = "WinTunnel";
        public static String SERVERICE_DISPLAY_NAME = "Windows TCP Tunnel";

         private WinTunnel tunnel = null;

        public WinProxy()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                tunnel = new WinTunnel(txtListeningPort.Text, txtForwardAddress.Text, txtForwardPort.Text, chxHttpsClient.Checked, chxHttpsServer.Checked,
                    txtLogFileLocation.Text, new WinTunnel.WriteToConsole(WriteToConsole), new WinTunnel.WriteToConsole(WriteToLog), chxLogToFile.Checked,
                    txtCertForClientConnection.Text);
                tunnel.Start();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.ToString(), "exception");
            }
        }

        
        public void WriteToConsole(string strConsoleText)
        {
            txtConsole.Invoke(new WinTunnel.WriteToConsole(AppendConsoleText), new[] { strConsoleText });
        }

        public void WriteToLog(string strLogText)
        {
            txtLog.Invoke(new WinTunnel.WriteToConsole(AppendLogText), new[] { strLogText });
        }
       
        public void AppendConsoleText(string strText)
        {
            txtConsole.Text += strText;
        }

        public void AppendLogText(string strLog)
        {
            txtLog.Text += strLog;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                tunnel.Stop();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "exception");
            }
        }

        private void txtClearConsole_Click(object sender, EventArgs e)
        {
            txtConsole.Text = "";
        }

        private void btnClearLogText_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
        }
    }
}
