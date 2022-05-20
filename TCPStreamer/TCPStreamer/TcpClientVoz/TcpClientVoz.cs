using System;
using System.Net.Sockets;

namespace ServCli
{
    public class TCPClientVoz
    {
        public TCPClientVoz(String server, int port)
        {
            this.m_Server = server;
            this.m_Port = port;

            this.ExceptionAppeared += new DelegateException(this.OnExceptionAppeared);
            this.ClientConnected += new DelegateConnection(this.OnConnected);
            this.ClientDisconnected += new DelegateConnection(this.OnDisconnected);

        }
        
        public TcpClient Client;
        NetworkStream m_NetStream;
        byte[] m_ByteBuffer;
        String m_Server;
        int m_Port;
        bool m_AutoConnect = false;
        private System.Threading.Timer m_TimerAutoConnect;
        private int m_AutoConnectInterval = 10;

        private class Locker_AutoConnectClass
        {
        }
        private Locker_AutoConnectClass Locker_AutoConnect = new Locker_AutoConnectClass();


        public delegate void DelegateDataReceived(ServCli.TCPClientVoz client, Byte[] bytes);
        public delegate void DelegateDataSend(ServCli.TCPClientVoz client, Byte[] bytes);
        public delegate void DelegateDataReceivedComplete(ServCli.TCPClientVoz client, String message);
        public delegate void DelegateConnection(ServCli.TCPClientVoz client, string Info);
        public delegate void DelegateException(ServCli.TCPClientVoz client, Exception ex);

        public event DelegateDataReceived DataReceived;

        public event DelegateDataSend DataSend;

        public event DelegateConnection ClientConnected;

        public event DelegateConnection ClientDisconnected;

        public event DelegateException ExceptionAppeared;

        private void InitTimerAutoConnect()
        {
            if(m_AutoConnect)
            {
                if(m_TimerAutoConnect == null)
                {
                    if(m_AutoConnectInterval > 0)
                    {
                        m_TimerAutoConnect = new System.Threading.Timer(
                            new System.Threading.TimerCallback(OnTimer_AutoConnect),
                            null,
                            m_AutoConnectInterval * 1000,
                            m_AutoConnectInterval * 1000);
                    }
                }
            }
        }

        public void Send(Byte[] data)
        {
            try
            {
                m_NetStream.Write(data, 0, data.Length);

                if (this.DataSend != null)
                {
                    this.DataSend(this, data);
                }
            } 
            catch (Exception ex)
            { 

                ExceptionAppeared(this, ex);
            }
        }

        private void StartReading()
        {
            try
            {
                m_ByteBuffer = new byte[1024];
                m_NetStream.BeginRead(
                    m_ByteBuffer,
                    0,
                    m_ByteBuffer.Length,
                    new AsyncCallback(OnDataReceived),
                    m_NetStream);
            } catch(Exception ex)
            {
                ExceptionAppeared(this, ex);
            }
        }

        private void OnDataReceived(IAsyncResult ar)
        {
            try
            {
                NetworkStream myNetworkStream = (NetworkStream)ar.AsyncState;


                if(myNetworkStream.CanRead)
                {
                    int numberOfBytesRead = myNetworkStream.EndRead(ar);

                    if(numberOfBytesRead > 0)
                    {
                        if(this.DataReceived != null)
                        {
                            Byte[] data = new byte[numberOfBytesRead];
                            System.Array.Copy(m_ByteBuffer, 0, data, 0, numberOfBytesRead);

                            this.DataReceived(this, data);
                        }
                    } else
                    {
                        if(this.ClientDisconnected != null)
                        {
                            this.ClientDisconnected(this, "FIN");
                        }

                        if(m_AutoConnect == false)
                        {
                            this.disconnect_intern();
                        } else
                        {
                            this.Disconnect_ButAutoConnect();
                        }

                        return;
                    }

                    myNetworkStream.BeginRead(
                        m_ByteBuffer,
                        0,
                        m_ByteBuffer.Length,
                        new AsyncCallback(OnDataReceived),
                        myNetworkStream);
                }
            } catch(Exception ex)
            {
                ExceptionAppeared(this, ex);
            }
        }

        public void Connect()
        {
            try
            {
                InitTimerAutoConnect();

                Client = new TcpClient(this.m_Server, this.m_Port);
                m_NetStream = Client.GetStream();

                this.StartReading();

                ClientConnected(this, String.Format("server: {0} port: {1}", this.m_Server, this.m_Port));
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            disconnect_intern();

            if(m_TimerAutoConnect != null)
            {
                m_TimerAutoConnect.Dispose();
                m_TimerAutoConnect = null;
            }

            if(this.ClientDisconnected != null)
            {
                this.ClientDisconnected(this, "Verbindung beendet");
            }
        }


        private void Disconnect_ButAutoConnect()
        {
            //disconnect_intern();
        }

        private void disconnect_intern()
        {
            if(Client != null)
            {
                Client.Close();
            }
            if(m_NetStream != null)
            {
                m_NetStream.Close();
            }
        }

        private void OnTimer_AutoConnect(Object ob)
        {
            try
            {
                lock(Locker_AutoConnect)
                {
                    if(m_AutoConnect)
                    {
                        if(Client == null || Client.Connected == false)
                        {
                            Client = new TcpClient(this.m_Server, this.m_Port);
                            m_NetStream = Client.GetStream();

                            this.StartReading();
                            ClientConnected(this, String.Format("server: {0} port: {1}", this.m_Server, this.m_Port));
                        }
                    } else
                    {
                        if(m_TimerAutoConnect != null)
                        {
                            m_TimerAutoConnect.Dispose();
                            m_TimerAutoConnect = null;
                        }
                    }
                }
            } catch(Exception ex)
            {
                ExceptionAppeared(this, ex);
            }
        }

        private void OnExceptionAppeared(ServCli.TCPClientVoz client, Exception ex)
        {
        }
        private void OnConnected(ServCli.TCPClientVoz client, string info)
        {
        }
        private void OnDisconnected(ServCli.TCPClientVoz client, string info)
        {
        }

        public bool Connected
        {
            get
            {
                if(this.Client != null)
                {
                    return this.Client.Connected;
                } else
                {
                    return false;
                }
            }
        }
    }
}
