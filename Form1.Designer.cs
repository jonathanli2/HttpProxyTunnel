namespace WinProxy
{
    partial class WinProxy
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtListeningPort = new System.Windows.Forms.TextBox();
            this.txtForwardAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogFileLocation = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.btnClearLogText = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabConsole = new System.Windows.Forms.TabPage();
            this.txtClearConsole = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.chxLogToFile = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtForwardPort = new System.Windows.Forms.TextBox();
            this.chxHttpsClient = new System.Windows.Forms.CheckBox();
            this.txtCertForClientConnection = new System.Windows.Forms.TextBox();
            this.chxHttpsServer = new System.Windows.Forms.CheckBox();
            this.txtClientConnectionCertPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(533, 71);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(51, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(603, 71);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Listening Port:";
            // 
            // txtListeningPort
            // 
            this.txtListeningPort.Location = new System.Drawing.Point(119, 6);
            this.txtListeningPort.Name = "txtListeningPort";
            this.txtListeningPort.Size = new System.Drawing.Size(113, 20);
            this.txtListeningPort.TabIndex = 3;
            this.txtListeningPort.Text = "9000";
            // 
            // txtForwardAddress
            // 
            this.txtForwardAddress.Location = new System.Drawing.Point(355, 6);
            this.txtForwardAddress.Name = "txtForwardAddress";
            this.txtForwardAddress.Size = new System.Drawing.Size(191, 20);
            this.txtForwardAddress.TabIndex = 4;
            this.txtForwardAddress.Text = "torn00461340a.dhcp.tor2.sap.corp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Forward Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Log File Location:";
            // 
            // txtLogFileLocation
            // 
            this.txtLogFileLocation.Location = new System.Drawing.Point(119, 73);
            this.txtLogFileLocation.Name = "txtLogFileLocation";
            this.txtLogFileLocation.Size = new System.Drawing.Size(136, 20);
            this.txtLogFileLocation.TabIndex = 7;
            this.txtLogFileLocation.Text = "c:\\temp\\log.txt";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Controls.Add(this.tabConsole);
            this.tabControl1.Location = new System.Drawing.Point(12, 99);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(642, 460);
            this.tabControl1.TabIndex = 8;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.btnClearLogText);
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(634, 434);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Http Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // btnClearLogText
            // 
            this.btnClearLogText.Location = new System.Drawing.Point(463, 404);
            this.btnClearLogText.Name = "btnClearLogText";
            this.btnClearLogText.Size = new System.Drawing.Size(75, 23);
            this.btnClearLogText.TabIndex = 1;
            this.btnClearLogText.Text = "Clear";
            this.btnClearLogText.UseVisualStyleBackColor = true;
            this.btnClearLogText.Click += new System.EventHandler(this.btnClearLogText_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(3, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(625, 395);
            this.txtLog.TabIndex = 0;
            // 
            // tabConsole
            // 
            this.tabConsole.Controls.Add(this.txtClearConsole);
            this.tabConsole.Controls.Add(this.txtConsole);
            this.tabConsole.Location = new System.Drawing.Point(4, 22);
            this.tabConsole.Name = "tabConsole";
            this.tabConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tabConsole.Size = new System.Drawing.Size(634, 434);
            this.tabConsole.TabIndex = 0;
            this.tabConsole.Text = "Console Output";
            this.tabConsole.UseVisualStyleBackColor = true;
            // 
            // txtClearConsole
            // 
            this.txtClearConsole.Location = new System.Drawing.Point(463, 411);
            this.txtClearConsole.Name = "txtClearConsole";
            this.txtClearConsole.Size = new System.Drawing.Size(75, 23);
            this.txtClearConsole.TabIndex = 1;
            this.txtClearConsole.Text = "Clear";
            this.txtClearConsole.UseVisualStyleBackColor = true;
            this.txtClearConsole.Click += new System.EventHandler(this.txtClearConsole_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(0, 0);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(554, 404);
            this.txtConsole.TabIndex = 0;
            // 
            // chxLogToFile
            // 
            this.chxLogToFile.AutoSize = true;
            this.chxLogToFile.Location = new System.Drawing.Point(261, 76);
            this.chxLogToFile.Name = "chxLogToFile";
            this.chxLogToFile.Size = new System.Drawing.Size(73, 17);
            this.chxLogToFile.TabIndex = 9;
            this.chxLogToFile.Text = "LogToFile";
            this.chxLogToFile.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(568, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port:";
            // 
            // txtForwardPort
            // 
            this.txtForwardPort.Location = new System.Drawing.Point(603, 6);
            this.txtForwardPort.Name = "txtForwardPort";
            this.txtForwardPort.Size = new System.Drawing.Size(41, 20);
            this.txtForwardPort.TabIndex = 11;
            this.txtForwardPort.Text = "8081";
            // 
            // chxHttpsClient
            // 
            this.chxHttpsClient.AutoSize = true;
            this.chxHttpsClient.Checked = true;
            this.chxHttpsClient.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxHttpsClient.Location = new System.Drawing.Point(32, 42);
            this.chxHttpsClient.Name = "chxHttpsClient";
            this.chxHttpsClient.Size = new System.Drawing.Size(184, 17);
            this.chxHttpsClient.TabIndex = 13;
            this.chxHttpsClient.Text = "Enable https for client connection";
            this.chxHttpsClient.UseVisualStyleBackColor = true;
            // 
            // txtCertForClientConnection
            // 
            this.txtCertForClientConnection.Location = new System.Drawing.Point(263, 39);
            this.txtCertForClientConnection.Name = "txtCertForClientConnection";
            this.txtCertForClientConnection.Size = new System.Drawing.Size(254, 20);
            this.txtCertForClientConnection.TabIndex = 14;
            this.txtCertForClientConnection.Text = "C:\\SharedFolder\\SMP\\gen_cert\\mykeystore.p12";
            // 
            // chxHttpsServer
            // 
            this.chxHttpsServer.AutoSize = true;
            this.chxHttpsServer.Checked = true;
            this.chxHttpsServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxHttpsServer.Location = new System.Drawing.Point(355, 77);
            this.chxHttpsServer.Name = "chxHttpsServer";
            this.chxHttpsServer.Size = new System.Drawing.Size(172, 17);
            this.chxHttpsServer.TabIndex = 15;
            this.chxHttpsServer.Text = "use https for server connection";
            this.chxHttpsServer.UseVisualStyleBackColor = true;
            // 
            // txtClientConnectionCertPassword
            // 
            this.txtClientConnectionCertPassword.Location = new System.Drawing.Point(603, 40);
            this.txtClientConnectionCertPassword.Name = "txtClientConnectionCertPassword";
            this.txtClientConnectionCertPassword.Size = new System.Drawing.Size(56, 20);
            this.txtClientConnectionCertPassword.TabIndex = 16;
            this.txtClientConnectionCertPassword.Text = "s3pAdmin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Path:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(544, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Password:";
            // 
            // WinProxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 571);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtClientConnectionCertPassword);
            this.Controls.Add(this.chxHttpsServer);
            this.Controls.Add(this.txtCertForClientConnection);
            this.Controls.Add(this.chxHttpsClient);
            this.Controls.Add(this.txtForwardPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chxLogToFile);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtLogFileLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtForwardAddress);
            this.Controls.Add(this.txtListeningPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "WinProxy";
            this.Text = "WinProxy";
            this.tabControl1.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabConsole.ResumeLayout(false);
            this.tabConsole.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtListeningPort;
        private System.Windows.Forms.TextBox txtForwardAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogFileLocation;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.Button btnClearLogText;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TabPage tabConsole;
        private System.Windows.Forms.Button txtClearConsole;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.CheckBox chxLogToFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtForwardPort;
        private System.Windows.Forms.CheckBox chxHttpsClient;
        private System.Windows.Forms.TextBox txtCertForClientConnection;
        private System.Windows.Forms.CheckBox chxHttpsServer;
        private System.Windows.Forms.TextBox txtClientConnectionCertPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

