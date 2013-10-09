using System;
using System.Net.Sockets;
using System.Net;

namespace WinProxy
{
	/// <summary>
	/// Summary description for ProxyConnection.
	/// </summary>
	public class ProxyConnection
	{
		public int connNumber;

		public bool isShutdown = false;

		public Socket clientSocket; //socket for communication with the client
		
		public IPEndPoint serverEP;

		public Socket serverSocket; //Socket for communication with the server

		public const int BUFFER_SIZE = 8092;

		public byte[] clientReadBuffer = new byte[BUFFER_SIZE];
		public byte[] serverReadBuffer = new byte[BUFFER_SIZE];

		public int clientNumBytes;
		public byte[] clientSendBuffer = new byte[BUFFER_SIZE];
		public int serverNumBytes;
		public byte[] serverSendBuffer = new byte[BUFFER_SIZE];

		public void disconnect()
		{
			isShutdown = true;
			try
			{
				if (serverSocket != null)
				{
					if (serverSocket.Connected)	
                        serverSocket.Shutdown(SocketShutdown.Both);
					serverSocket.Close();
				}

				if (clientSocket != null)
				{
					if (clientSocket.Connected) 
                        clientSocket.Shutdown(SocketShutdown.Both);
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
				serverEP = null;

				clientReadBuffer = null;
				clientSendBuffer = null;

				serverReadBuffer = null;
				serverSendBuffer = null;
			}
		}
	}
}
