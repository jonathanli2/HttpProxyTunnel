using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net;
using System.Runtime.InteropServices;


namespace WinProxy
{
	public class WinTunnel 
	{
        public String m_strListeningPort,  m_strForwardAddress,  m_strLogFileLocation, m_strForwardPort;
        private bool m_bLogToFile;
        public IPEndPoint m_localEP;
        public IPEndPoint m_serverEP;
  
        public delegate void WriteToConsole(string message);
        private static WriteToConsole m_delWriteToConsole;
        private static WriteToConsole m_delWriteToLog;

		public static ConnectionManager connMgr;
        ProxyClientListenerTask task;

		public WinTunnel(String strListeningPort, string strForwardAddress, string strForwardPort, string strLogFileLocation, 
            WriteToConsole delWriteToConsole, WriteToConsole delWriteToLog, bool bLogToFile )
		{
            m_strListeningPort = strListeningPort;
            m_strForwardAddress = strForwardAddress;
            m_strForwardPort = strForwardPort;
            m_strLogFileLocation = strLogFileLocation;
            m_delWriteToConsole = delWriteToConsole;
            m_delWriteToLog = delWriteToLog;
            m_bLogToFile = bLogToFile;

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
  
            //Load configuration and startup
            loadConfiguration(m_strListeningPort, m_strForwardAddress, m_strForwardPort);
           
            connMgr = new ConnectionManager();


            task = new ProxyClientListenerTask(m_localEP, m_serverEP);
            Thread t = new Thread(task.run);
            t.Start();

        }

        public void loadConfiguration(string strListeningPort, string strForwardAddress, string strForwardPort)
        {

            try
            {             
                String listenPort;
                String targetPort;
                String targetIP;

                listenPort = strListeningPort;
                m_localEP = new IPEndPoint(IPAddress.Any, Int32.Parse(listenPort));

                targetIP = strForwardAddress;
                targetPort = strForwardPort;
                
                IPAddress ip;
                if (!IPAddress.TryParse(targetIP, out ip))
                {
                    IPHostEntry hostEntry;

                    hostEntry = Dns.GetHostEntry(targetIP);
                    ip = hostEntry.AddressList[0];
                }

                m_serverEP = new IPEndPoint(ip, Int32.Parse(targetPort));
            }
            catch (Exception e)
            {
                WinTunnel.WriteTextToConsole(string.Format("The exception is {0}.", e));
            }
        }
	}
}
