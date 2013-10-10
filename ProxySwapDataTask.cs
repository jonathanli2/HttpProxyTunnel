using System;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using System.Security.Authentication;

namespace WinProxy
{
    /// <summary>
    /// Summary description for ProxyDataTask.
    /// </summary>
    public class ProxySwapDataTask 
    {
        ProxyConnection m_conn;

        public ProxySwapDataTask(ProxyConnection conn)
        {
            m_conn = conn;
        }

        public static void connectForwardServerCallBack(IAsyncResult ar)
        {
            ProxyConnection conn = (ProxyConnection)ar.AsyncState;
            conn.serverSocket.EndConnect(ar);
            ProxySwapDataTask proxy = new ProxySwapDataTask(conn);       

            //now we have both server socket and client socket, we can pass the data back and forth for both side
            System.Diagnostics.Debug.Assert(true == conn.clientSocket.Connected);
            System.Diagnostics.Debug.Assert(true == conn.serverSocket.Connected);
              
            WinTunnel.WriteTextToConsole(string.Format("ProxyConnection#{0}-- client socket or server socket start receiving data....",
                    conn.connNumber));
            WinTunnel.connMgr.AddConnection(conn);

            Stream stream = conn.clientSocket.GetStream();
            if (conn.m_bHttpsClient)
            {
                SslStream sslStream = new SslStream(conn.clientSocket.GetStream(), false);
                sslStream.AuthenticateAsServer(WinTunnel.CertificateForClientConnection, false, SslProtocols.Tls, true);
            }
            stream.BeginRead(conn.clientReadBuffer, 0, ProxyConnection.BUFFER_SIZE, new AsyncCallback(clientReadCallBack), conn);

            NetworkStream serverStream = conn.serverSocket.GetStream();
                //Read data from the server socket
            serverStream.BeginRead(conn.serverReadBuffer, 0, ProxyConnection.BUFFER_SIZE, new AsyncCallback(serverReadCallBack), conn);

        }

     
        //the client socket gets data from client side, need to forward it to server socket, and then
        //start to read from client socket again
        private static void clientReadCallBack(IAsyncResult ar)
        {
            ProxyConnection conn = (ProxyConnection)ar.AsyncState;
            if (!conn.isShutdown)
            {
                WinTunnel.WriteTextToConsole("client socket receives data callback called");

                int numBytes = 0;
                try
                {
                    if (conn.clientSocket != null)
                    {
                        numBytes = conn.clientSocket.GetStream().EndRead(ar);
                        WinTunnel.WriteTextToConsole(string.Format("client socket receives data: {0}", numBytes));

                        if (numBytes > 0) //write to the server side socket
                        {
                            //copy the bytes to the server send buffer and call send
                            Array.Copy(conn.clientReadBuffer, 0, conn.serverSendBuffer, 0, numBytes);

                            //log the client request
                            Logger.log(conn.clientReadBuffer, 0, numBytes, "C{0}:----------------Client Request----------------: ",
                                conn.connNumber);

                            conn.serverNumBytes += numBytes;
                            conn.serverSocket.GetStream().Write(conn.serverSendBuffer, 0, numBytes);
                           
                            //continue to read
                            conn.clientSocket.GetStream().BeginRead(conn.clientReadBuffer, 0, ProxyConnection.BUFFER_SIZE, 
                                new AsyncCallback(clientReadCallBack), conn);
                        }
                        else
                        {
                            //do not close socket even if no data to read, as we still need write to it? need test to confirm this

                            WinTunnel.WriteTextToConsole(string.Format("ProxyConnection#{0}: Detected Client Socket disconnect via read.",
                                conn.connNumber));
                            //conn.Release();
                        }
                    }
                    else
                    {
                        WinTunnel.connMgr.Release(conn);
                    }
                }
                catch (SocketException se)
                {
                    if (!conn.isShutdown)
                    {
                        WinTunnel.WriteTextToConsole(string.Format("ProxyConnection#{0}: Socket Error occurred when reading data from the client socket.  Error Code is: {1}.",
                            conn.connNumber, se.ErrorCode));
                        WinTunnel.connMgr.Release(conn);
                    }
                }
                catch (Exception e)
                {
                    if (!conn.isShutdown)
                    {
                        WinTunnel.WriteTextToConsole(string.Format("ProxyConnection#{0}:  Error occurred when reading data from the client socket.  The error is: {1}.",
                            conn.connNumber, e));
                        WinTunnel.connMgr.Release(conn);
                    }
                }
            }
          
        }

        //The server socket reads some data from server, needs to forward the data to client side, and then starts to 
        //read data again
        private static void serverReadCallBack(IAsyncResult ar)
        {
            WinTunnel.WriteTextToConsole("server socket receives data callback called");

            ProxyConnection conn = (ProxyConnection)ar.AsyncState;
            int numBytes = 0;
            if (!conn.isShutdown)
            {
                try
                {
                    numBytes = conn.serverSocket.GetStream().EndRead(ar);
                    WinTunnel.WriteTextToConsole(string.Format("server socket receives data: {0}", numBytes));

                    if (numBytes > 0) //write to the client side socket
                    {
                        //copy the bytes to the client send buffer and call send
                        Array.Copy(conn.serverReadBuffer, 0, conn.clientSendBuffer, 0, numBytes);

                        //log the server response request
                        Logger.log(conn.serverReadBuffer, 0, numBytes, "C{0}**************Server response**************::",
                             conn.connNumber);

                        conn.clientNumBytes += numBytes;
                        conn.clientSocket.GetStream().Write(conn.clientSendBuffer, 0, numBytes);
                        
                        conn.serverSocket.GetStream().BeginRead(conn.serverReadBuffer, 0, ProxyConnection.BUFFER_SIZE,
                            new AsyncCallback(serverReadCallBack), conn);

                    }
                    else
                    {
                        //Server must have disconnected the socket
                        WinTunnel.WriteTextToConsole(string.Format("ProxyConnection#{0}: Detected Server Socket disconnect via read.",
                            conn.connNumber));
                        WinTunnel.connMgr.Release(conn);
                    }
                }
                catch (SocketException se)
                {
                    if (!conn.isShutdown)
                    {
                        WinTunnel.WriteTextToConsole(string.Format("ProxyConnection#{0}: Socket Error occurred when reading data from the server socket.  Error Code is: {1}.",
                            conn.connNumber, se.ErrorCode));
                        WinTunnel.connMgr.Release(conn);
                    }
                }
                catch (Exception e)
                {
                    if (!conn.isShutdown)
                    {
                        WinTunnel.WriteTextToConsole(string.Format("ProxyConnection#{0}:  Error occurred when reading data from the server socket.  The error is: {1}.",
                            conn.connNumber, e));
                        WinTunnel.connMgr.Release(conn);
                    }
                }
            }
          
        }
      
    }
}

