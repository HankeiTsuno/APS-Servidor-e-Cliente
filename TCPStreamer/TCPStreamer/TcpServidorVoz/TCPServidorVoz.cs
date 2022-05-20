using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using System.Collections;

namespace ServCli
{
    public class TCPServidorVoz
    {
        public TCPServidorVoz()
        {

        }

        private IPEndPoint m_endpoint;
        private TcpListener m_tcpip;
        private Thread m_ThreadMainServer;
        private ListenerState m_State;
        private List<ServerThread> m_threads = new List<ServerThread>();

        public delegate void DelegateClientConnected(ServerThread st);
        public delegate void DelegateClientDisconnected(ServerThread st, string info);
        public delegate void DelegateDataReceived(ServerThread st, Byte[] data);

        public event DelegateClientConnected ClientConnected;
        public event DelegateClientDisconnected ClientDisconnected;
        public event DelegateDataReceived DataReceived;

        public enum ListenerState
        {
            None,
            Started,
            Stopped,
            Error
        };
        public List<ServerThread> Clients
        {
            get
            {
                return m_threads;
            }
        }
        public ListenerState State
        {
            get
            {
                return m_State;
            }
        }
        public void Start(string strIPAdress, int Port)
        {
            m_endpoint = new IPEndPoint(IPAddress.Parse(strIPAdress), Port);
            m_tcpip = new TcpListener(m_endpoint);

            if (m_tcpip == null) return;

            try
            {
                m_tcpip.Start();

                m_ThreadMainServer = new Thread(new ThreadStart(Run));
                m_ThreadMainServer.Start();
                this.m_State = ListenerState.Started;
            }
            catch (Exception ex)
            {
                m_tcpip.Stop();
                this.m_State = ListenerState.Error;

                throw ex;
            }
        }
        private void Run()
        {
            while (true)
            {
                TcpClient client = m_tcpip.AcceptTcpClient();
                ServerThread st = new ServerThread(client);
                st.DataReceived += new ServerThread.DelegateDataReceived(OnDataReceived);
                st.ClientDisconnected += new ServerThread.DelegateClientDisconnected(OnClientDisconnected);

                OnClientConnected(st);

                try
                {
                    client.Client.BeginReceive(st.ReadBuffer, 0, st.ReadBuffer.Length, SocketFlags.None, st.Receive, client.Client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
         private void OnDataReceived(ServerThread st, Byte[] data)
        {
            if (DataReceived != null)
            {
                DataReceived(st, data);
            }
        }
        private void OnClientDisconnected(ServerThread st, string info)
        {
            m_threads.Remove(st);

            if (ClientDisconnected != null)
            {
                ClientDisconnected(st, info);
            }
        }
        private void OnClientConnected(ServerThread st)
        {
            if (!m_threads.Contains(st))
            {
                m_threads.Add(st);
            }

            if (ClientConnected != null)
            {
                ClientConnected(st);
            }
        }
        public void Stop()
        {
            try
            {
                if (m_ThreadMainServer != null)
                {
                    m_ThreadMainServer.Abort();
                    System.Threading.Thread.Sleep(100);
                }

                for (IEnumerator en = m_threads.GetEnumerator(); en.MoveNext(); )
                {
                    ServerThread st = (ServerThread)en.Current;
                    st.Stop();

                    if (ClientDisconnected != null)
                    {
                        ClientDisconnected(st, "Verbindung wurde beendet");
                    }
                }

                if (m_tcpip != null)
                {
                    m_tcpip.Stop();
                    m_tcpip.Server.Close();
                }

                m_threads.Clear();
        
                this.m_State = ListenerState.Stopped;

            }
            catch (Exception)
            {
                this.m_State = ListenerState.Error;
            }
        }
    }

    public class ServerThread
    {
        private bool m_IsStopped = false;

        private TcpClient m_Connection = null;

        public byte[] ReadBuffer = new byte[1024];

        public bool IsMute = false;

        public String Name = "";

        public delegate void DelegateDataReceived(ServerThread st, Byte[] data);
        public event DelegateDataReceived DataReceived;
        public delegate void DelegateClientDisconnected(ServerThread sv, string info);
        public event DelegateClientDisconnected ClientDisconnected;

        public TcpClient Client
        {
            get
            {
                return m_Connection;
            }
        }
        public bool IsStopped
        {
            get
            {
                return m_IsStopped;
            }
        }
        public ServerThread(TcpClient connection)
        {


            this.m_Connection = connection;
        }

        public void Receive(IAsyncResult ar)
        {
            try
            {

                if (this.m_Connection.Client.Connected == false)
                {
                    return;
                }

                if (ar.IsCompleted)
                {

                    int bytesRead = m_Connection.Client.EndReceive(ar);

                    if (bytesRead > 0)
                    {

                        Byte[] data = new byte[bytesRead];
                        System.Array.Copy(ReadBuffer, 0, data, 0, bytesRead);

                        DataReceived(this, data);

                        m_Connection.Client.BeginReceive(ReadBuffer, 0, ReadBuffer.Length, SocketFlags.None, Receive, m_Connection.Client);
                    }
                    else
                    {
                        HandleDisconnection("Verbindung wurde beendet");
                    }
                }
            }
            catch (Exception ex)
            {

                HandleDisconnection(ex.Message);
            }
        }

        public void HandleDisconnection(string reason)
        {

            m_IsStopped = true;

            if (ClientDisconnected != null)
            {
                ClientDisconnected(this, reason);
            }
        }

        public void Send(Byte[] data)
        {
            try
            {
                if (this.m_IsStopped == false)
                {
                    NetworkStream ns = this.m_Connection.GetStream();

                    lock (ns)
                    {
                        ns.Write(data, 0, data.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                this.m_Connection.Close();
                this.m_IsStopped = true;

                if (ClientDisconnected != null)
                {
                    ClientDisconnected(this, ex.Message);
                }

                throw ex;
            }
        }

        public void Stop()
        {
            if (m_Connection.Client.Connected == true)
            {
                m_Connection.Client.Disconnect(false);
            }

            this.m_IsStopped = true;
        }
    }
}
