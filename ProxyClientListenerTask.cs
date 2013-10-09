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
      
        public Socket listenSocket = null;

        private bool m_bContinue;
        private ManualResetEvent allDone;
        IPEndPoint m_local;
        IPEndPoint m_server;

   
        public ProxyClientListenerTask(IPEndPoint local, IPEndPoint server)
        {
            Console.WriteLine("ProxyClientListenerTask {0} created.", this);
            m_local = local;
            m_server = server;
        }

        #region ITask Members

        public void run()
        {
            try
            {
                m_bContinue = true;
                allDone = new ManualResetEvent(false);

                listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.Bind(m_local);
                listenSocket.Listen(100); //allow up to 100 pending connections

                while (m_bContinue)
                {
                    WinTunnel.WriteTextToConsole(String.Format("Waiting for client connection at {0}...", m_local.ToString()));

                    allDone.Reset();
                    listenSocket.BeginAccept(new AsyncCallback(ProxyClientListenerTask.acceptCallBack), this);
                    allDone.WaitOne();
                }
             }
            finally
            {
                listenSocket.Close();
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
        public static void acceptCallBack(IAsyncResult ar)
        {
            ProxyConnection conn = null;

            try
            {

                ProxyClientListenerTask listener = (ProxyClientListenerTask)ar.AsyncState;
                if (listener.m_bContinue)
                {
                    listener.allDone.Set();

                    //create a new task for connecting to the server side.

                    conn = new ProxyConnection();

                    conn.clientSocket = listener.listenSocket.EndAccept(ar); //accept the client connection

                    WinTunnel.WriteTextToConsole(string.Format("Conn#{0} Accepted new connection. Local: {1}, Remote: {2}.",
                        conn.connNumber,
                        conn.clientSocket.LocalEndPoint.ToString(),
                        conn.clientSocket.RemoteEndPoint.ToString()));

                    conn.serverEP = listener.m_server;

                    conn.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    conn.serverSocket.BeginConnect(conn.serverEP, new AsyncCallback(ProxySwapDataTask.connectForwardServerCallBack), conn);
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
