using System;
using System.Net.Sockets;
using System.Threading;
using  System.Net;
namespace WinProxy
{
    /// <summary>
    /// Summary description for SocketListenerTask.
    /// </summary>
    public class ProxyClientListenerTask
    {
      
        //public Socket listenSocket = null;
        public TcpListener tcpListener = null;

        private bool m_bContinue;
        private ManualResetEvent allDone;
        IPEndPoint m_local;
        private string m_serverName;
        private int m_serverPort;
        public static bool m_bHttpsClient;
        public static bool m_bHttpsServer;
   
        public ProxyClientListenerTask(IPEndPoint local, string serverName, int serverPort, bool bHttpsClient, bool bHttpsServer)
        {
            Console.WriteLine("ProxyClientListenerTask {0} created.", this);
            m_local = local;
            m_serverName = serverName;
            m_serverPort = serverPort;
            m_bHttpsClient = bHttpsClient;
            m_bHttpsServer = bHttpsServer;
        }

        #region ITask Members

        public void run()
        {
            try
            {
                m_bContinue = true;
                allDone = new ManualResetEvent(false);
                tcpListener = new TcpListener(m_local);
                tcpListener.Start();
            
                while (m_bContinue)
                {
                    WinTunnel.WriteTextToConsole(String.Format("Waiting for client connection at {0}...", m_local.ToString()));

                    allDone.Reset();
                    tcpListener.BeginAcceptTcpClient( new AsyncCallback(DoAcceptTcpClientCallback), this);
                    allDone.WaitOne();
                }
             }
            finally
            {
                tcpListener.Stop();
                WinTunnel.WriteTextToConsole(String.Format("client connection loop end {0}...", m_local.ToString()));
            }
        }

        public void stop()
        {
            m_bContinue = false;
            allDone.Set();
        }

        #endregion

        //Call back when the server listener socket has connected to a client request
        public static void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            ProxyConnection conn = null;

            try
            {

                ProxyClientListenerTask listener = (ProxyClientListenerTask)ar.AsyncState;
                if (listener.m_bContinue)
                {
                    listener.allDone.Set();

                    //create a new task for connecting to the server side.

                    conn = new ProxyConnection(m_bHttpsClient, m_bHttpsServer);

                    conn.clientSocket = listener.tcpListener.EndAcceptTcpClient(ar); //accept the client connection

                    WinTunnel.WriteTextToConsole(string.Format("Conn#{0} Accepted new connection. Local: {1}, Remote: {2}.",
                        conn.connNumber,
                        conn.clientSocket.Client.LocalEndPoint.ToString(),
                        conn.clientSocket.Client.RemoteEndPoint.ToString()));

                    conn.serverName = listener.m_serverName;
                    conn.serverPort = listener.m_serverPort;
                    IPHostEntry hostInfo = Dns.GetHostEntry(conn.serverName);
                    // Get the DNS IP addresses associated with the host.
                    IPAddress[] IPaddresses = hostInfo.AddressList;

                    conn.serverSocket = new TcpClient(AddressFamily.InterNetwork);
                    conn.serverSocket.BeginConnect(IPaddresses[0], conn.serverPort, new AsyncCallback(ProxySwapDataTask.connectForwardServerCallBack), conn);
                }
            }
            catch (SocketException se)
            {
                WinTunnel.WriteTextToConsole(string.Format("Conn# {0} Socket Error occurred when accepting client socket. Error Code is: {1}",
                    conn.connNumber, se.ErrorCode));
                if (conn != null && WinTunnel.connMgr!= null)
                {
                    WinTunnel.connMgr.Release(conn);
                }
            }
            catch (Exception e)
            {
                WinTunnel.WriteTextToConsole(string.Format("Conn# {0} Error occurred when accepting client socket. Error is: {1}",
                     conn.connNumber, e));
                if (conn != null && WinTunnel.connMgr != null)
                {
                    WinTunnel.connMgr.Release(conn);
	
                }
            }
        }

    }
}
