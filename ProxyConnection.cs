using System;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace WinProxy
{
	/// <summary>
	/// Summary description for ProxyConnection.
	/// </summary>
	public class ProxyConnection
	{
		public int connNumber;
        public bool m_bHttpsClient;
        public bool m_bHttpsServer;



		public bool isShutdown = false;

		public TcpClient clientSocket; //socket for communication with the client
        public Stream clientStream; //Networkstream or SslStream
		public string serverName;
        public int serverPort;

		public TcpClient serverSocket; //Socket for communication with the server
        public Stream serverStream;

		public const int BUFFER_SIZE = 8092;

		public byte[] clientReadBuffer = new byte[BUFFER_SIZE];
		public byte[] serverReadBuffer = new byte[BUFFER_SIZE];

		public int clientNumBytes;
		public byte[] clientSendBuffer = new byte[BUFFER_SIZE];
		public int serverNumBytes;
		public byte[] serverSendBuffer = new byte[BUFFER_SIZE];

        public ProxyConnection(bool bHttpsClient, bool bHttpsServer)
        {
            m_bHttpsClient = bHttpsClient;
            m_bHttpsServer = bHttpsServer;
        }

		public void disconnect()
		{
			isShutdown = true;
			try
			{
				if (serverSocket != null)
				{
					serverSocket.Close();
				}

				if (clientSocket != null)
				{
					clientSocket.Close();
				}
				
			}
			catch (SocketException se)
			{
				WinTunnel.WriteTextToConsole(string.Format("Socket Error occurred while shutting down sockets: {0}.", se));
			}
			catch (Exception e)
			{
				WinTunnel.WriteTextToConsole(string.Format("Error occurred while shutting down sockets: {0}.", e));
			}
			finally
			{
				serverSocket = null;
				clientSocket = null;
			
				clientReadBuffer = null;
				clientSendBuffer = null;

				serverReadBuffer = null;
				serverSendBuffer = null;
			}
		}
	}
}
