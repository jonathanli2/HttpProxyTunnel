using System;
using System.Collections;

namespace WinProxy
{
	/// <summary>
	/// Summary description for ConnectionManager.
	/// </summary>
	public class ConnectionManager
	{
		private ArrayList m_connections= null;

		private  int m_connCount = 0;

	
		public ConnectionManager()
		{
			m_connections = ArrayList.Synchronized( new ArrayList());
		}


		public ProxyConnection AddConnection( ProxyConnection conn)
		{	
			lock(this)
			{
                WinTunnel.WriteTextToConsole(string.Format("Allocating ProxyConnection#{0} for new connection.", m_connCount));
				m_connections.Add(conn);
				conn.connNumber = m_connCount++;
			}
			return conn;
		}

		public bool Release(ProxyConnection conn)
		{
            lock (this)
            {
        
                if (conn.clientSocket != null)
                {
                    WinTunnel.WriteTextToConsole(String.Format(" Releasing ProxyConnection#{0}: Client {1}, Server {2}.",
                        conn.connNumber,
                        conn.clientSocket.Client.RemoteEndPoint.ToString(),
                        conn.serverEP.ToString()));
                }
                else
                {
                    WinTunnel.WriteTextToConsole(string.Format("Releasing ProxyConnection#{0}: Server {1}.",
                        conn.connNumber,
                        conn.serverEP));
                }

                conn.disconnect();
                m_connections.Remove(conn);

            }
			return true;
		}

		public bool shutdown()
		{
            WinTunnel.WriteTextToConsole(string.Format("There are {0} connections in the Connection List.", m_connections.Count));

			foreach (ProxyConnection conn in m_connections)
			{
				WinTunnel.WriteTextToConsole(string.Format("Disconnecting conn#{0}...", conn.connNumber));
				conn.disconnect();
			}
			m_connections.Clear(); //remove from the array list
			return true;
		}
	}
}
