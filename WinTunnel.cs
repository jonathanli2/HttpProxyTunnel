using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;


namespace WinProxy
{
	public class WinTunnel 
	{
        public String m_strListeningPort,  m_strForwardAddress,  m_strLogFileLocation, m_strForwardPort;
        private bool m_bLogToFile;
        public IPEndPoint m_localEP;
        public string m_serverName;
        public int m_serverPort;
        private bool m_bHttpsClient;
        private bool m_bHttpsServer;

        public static X509Certificate2 CertificateForClientConnection = null;
       
        public delegate void WriteToConsole(string message);
        private static WriteToConsole m_delWriteToConsole;
        private static WriteToConsole m_delWriteToLog;

		public static ConnectionManager connMgr;
        ProxyClientListenerTask task;

		public WinTunnel(String strListeningPort, string strForwardAddress, string strForwardPort, bool bHttpsClient, bool bHttpsServer,
            string strLogFileLocation, WriteToConsole delWriteToConsole, WriteToConsole delWriteToLog, bool bLogToFile,
            string strCertForClientConnection, string strClientConnectionCertPassword)
		{
            m_strListeningPort = strListeningPort;
            m_strForwardAddress = strForwardAddress;
            m_strForwardPort = strForwardPort;
            m_bHttpsClient = bHttpsClient;
            m_bHttpsServer = bHttpsServer;
            m_strLogFileLocation = strLogFileLocation;
            m_delWriteToConsole = delWriteToConsole;
            m_delWriteToLog = delWriteToLog;
            m_bLogToFile = bLogToFile;
            if (bHttpsClient)
            {
                CertificateForClientConnection = new X509Certificate2(strCertForClientConnection, strClientConnectionCertPassword);
            }
		}

        public static void WriteTextToConsole(string strConsoleText)
        {
            if (m_delWriteToConsole != null)
                m_delWriteToConsole(strConsoleText + "\r\n");
        }

        public static void WriteTextToLog(string strLogText)
        {
            if (m_delWriteToLog != null)
            {
                m_delWriteToLog(strLogText);
            }
        }

		/// <summary>
		/// Stop this service.
		/// </summary>
		public void Stop()
		{
			
            //close the main listen socket
            task.stop();

			//Shutdown the connection manager
			connMgr.shutdown();
            connMgr = null;
			WriteTextToConsole("WinTunnel stopped. ");
	
			Logger.close();
		}


        public void Start()
        {
            WriteTextToConsole("*** Starting up WinTunnel ****");
         
            WriteTextToConsole("Starting thread... ");
         
            Logger.initialize(m_strLogFileLocation, m_bLogToFile);
   
            m_localEP = new IPEndPoint(IPAddress.Any, Int32.Parse(m_strListeningPort));

            m_serverName = m_strForwardAddress;
            m_serverPort = Int32.Parse(m_strForwardPort);

            connMgr = new ConnectionManager();

            task = new ProxyClientListenerTask(m_localEP, m_serverName, m_serverPort, m_bHttpsClient, m_bHttpsServer);
            Thread t = new Thread(task.run);
            t.Start();

        }
	}
}
