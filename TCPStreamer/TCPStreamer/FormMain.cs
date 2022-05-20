using ServCli;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using WinSound;

namespace TCPStreamer
{
    public partial class FormMain : Form
    {
        private delegate void AtualizaStatusCallback(string strMensagem);

        public FormMain()
        {
            InitializeComponent();
            Init();
        }

        private ServCli.TCPClientVoz m_Client;
        private ServCli.TCPServidorVoz m_Server;
        private Configuration m_Config = new Configuration();
        private String m_ConfigFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "config.xml");
        private int m_SoundBufferCount = 8;
        private WinSound.Protocol m_PrototolClient = new WinSound.Protocol(WinSound.ProtocolTypes.LH, Encoding.Default);
        private Dictionary<ServCli.ServerThread, ServerThreadData> m_DictionaryServerDatas = new Dictionary<ServCli.ServerThread, ServerThreadData>();
        private WinSound.Recorder m_Recorder_Client;
        private WinSound.Recorder m_Recorder_Server;
        private WinSound.Player m_PlayerClient;
        private uint m_RecorderFactor = 4;
        private WinSound.JitterBuffer m_JitterBufferClientRecording;
        private WinSound.JitterBuffer m_JitterBufferClientPlaying;
        private WinSound.JitterBuffer m_JitterBufferServerRecording;
        WinSound.WaveFileHeader m_FileHeader = new WinSound.WaveFileHeader();
        private bool m_IsFormMain = true;
        private long m_SequenceNumber = 4596;
        private long m_TimeStamp = 0;
        private int m_Version = 2;
        private bool m_Padding = false;
        private bool m_Extension = false;
        private int m_CSRCCount = 0;
        private bool m_Marker = false;
        private int m_PayloadType = 0;
        private uint m_SourceId = 0;
        private System.Windows.Forms.Timer m_TimerProgressBarFile = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_TimerProgressBarPlayingClient = new System.Windows.Forms.Timer();
        private WinSound.EventTimer m_TimerMixed = null;
        private Byte[] m_FilePayloadBuffer;
        private int m_RTPPartsLength = 0;
        private uint m_Milliseconds = 20;
        System.Windows.Forms.Timer m_TimerDrawProgressBar;
        private Object LockerDictionary = new Object();
        public static Dictionary<Object, Queue<List<Byte>>> DictionaryMixed = new Dictionary<Object, Queue<List<byte>>>();
        private Encoding m_Encoding = Encoding.GetEncoding(1252);
        private const int RecordingJitterBufferCount = 8;

        private StreamWriter stwEnviador;
        private StreamReader strReceptor;
        private Thread mensagemThread;

        private System.Net.Sockets.TcpClient tcpServidor;
        flyoutWebCam webCamPainel;

        private delegate void AtualizaLogCallBack(string strMensagem);

        private delegate void FechaConexaoCallBack(string strMotivo);


        private void Init()
        {
            try
            {
                CreateHandle();
                InitComboboxes();
                LoadConfig();
                InitJitterBufferClientRecording();
                InitJitterBufferClientPlaying();
                InitJitterBufferServerRecording();
                InitTimerShowProgressBarPlayingClient();
                InitProtocolClient();
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }

        private void InitProtocolClient()
        {
            if (m_PrototolClient != null)
            {
                m_PrototolClient.DataComplete += new WinSound.Protocol.DelegateDataComplete(OnProtocolClient_DataComplete);
            }
        }



        private void InitTimerShowProgressBarPlayingClient()
        {
            m_TimerProgressBarPlayingClient = new System.Windows.Forms.Timer();
            m_TimerProgressBarPlayingClient.Interval = 60;
            m_TimerProgressBarPlayingClient.Tick += new EventHandler(OnTimerProgressPlayingClient);
        }
        private void OnTimerProgressPlayingClient(Object obj, EventArgs e)
        {
            try
            {
                if (m_PlayerClient != null)
                {
                    ProgressBarPlayingClient.Value = Math.Min(m_JitterBufferClientPlaying.Length, ProgressBarPlayingClient.Maximum);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("FormMain.cs | OnTimerProgressPlayingClient() | {0}", ex.Message));
                m_TimerProgressBarPlayingClient.Stop();
            }
        }

        private void OnTimerSendMixedDataToAllClients()
        {
            try
            {
                Dictionary<Object, List<Byte>> dic = new Dictionary<object, List<byte>>();
                List<List<byte>> listlist = new List<List<byte>>();
                Dictionary<Object, Queue<List<Byte>>> copy = new Dictionary<object, Queue<List<byte>>>(FormMain.DictionaryMixed);
                {
                    Queue<List<byte>> q = null;
                    foreach (Object obj in copy.Keys)
                    {

                        q = copy[obj];

                        if (q.Count > 0)
                        {
                            dic[obj] = q.Dequeue();
                            listlist.Add(dic[obj]);
                        }
                    }
                }

                if (listlist.Count > 0)
                {
                    Byte[] mixedBytes = WinSound.Mixer.MixBytes(listlist, m_Config.BitsPerSampleServer).ToArray();
                    List<Byte> listMixed = new List<Byte>(mixedBytes);

                    foreach (ServCli.ServerThread client in m_Server.Clients)
                    {
                        if (client.IsMute == false)
                        {
                            Byte[] mixedBytesClient = mixedBytes;

                            if (dic.ContainsKey(client))
                            {
                                List<Byte> listClient = dic[client];

                                mixedBytesClient = WinSound.Mixer.SubsctractBytes_16Bit(listMixed, listClient).ToArray();
                            }

                            WinSound.RTPPacket rtp = ToRTPPacket(mixedBytesClient, m_Config.BitsPerSampleServer, m_Config.ChannelsServer);
                            Byte[] rtpBytes = rtp.ToBytes();

                            client.Send(m_PrototolClient.ToBytes(rtpBytes));
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(String.Format("FormMain.cs | OnTimerSendMixedDataToAllClients() | {0}", ex.Message));
                m_TimerProgressBarPlayingClient.Stop();
            }
        }
        private void InitJitterBufferClientRecording()
        {
            if (m_JitterBufferClientRecording != null)
            {
                m_JitterBufferClientRecording.DataAvailable -= new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferClientDataAvailableRecording);
            }

            m_JitterBufferClientRecording = new WinSound.JitterBuffer(null, RecordingJitterBufferCount, 20);
            m_JitterBufferClientRecording.DataAvailable += new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferClientDataAvailableRecording);
        }

        private void InitJitterBufferClientPlaying()
        {
            if (m_JitterBufferClientPlaying != null)
            {
                m_JitterBufferClientPlaying.DataAvailable -= new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferClientDataAvailablePlaying);
            }

            m_JitterBufferClientPlaying = new WinSound.JitterBuffer(null, m_Config.JitterBufferCountClient, 20);
            m_JitterBufferClientPlaying.DataAvailable += new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferClientDataAvailablePlaying);
        }

        private void InitJitterBufferServerRecording()
        {
            if (m_JitterBufferServerRecording != null)
            {
                m_JitterBufferServerRecording.DataAvailable -= new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferServerDataAvailable);
            }

            m_JitterBufferServerRecording = new WinSound.JitterBuffer(null, RecordingJitterBufferCount, 20);
            m_JitterBufferServerRecording.DataAvailable += new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferServerDataAvailable);
        }

        private bool UseJitterBufferServer
        {
            get
            {
                return m_Config.JitterBufferCountServer >= 2;
            }
        }

        private bool UseJitterBufferClientRecording
        {
            get
            {
                return m_Config.UseJitterBufferClientRecording;
            }
        }

        private bool UseJitterBufferServerRecording
        {
            get
            {
                return m_Config.UseJitterBufferServerRecording;
            }
        }

        private void StartRecordingFromSounddevice_Client()
        {
            try
            {
                if (IsRecorderFromSounddeviceStarted_Client == false)
                {
                    int bufferSize = 0;
                    if (UseJitterBufferClientRecording)
                    {
                        bufferSize = WinSound.Utils.GetBytesPerInterval((uint)m_Config.SamplesPerSecondClient, m_Config.BitsPerSampleClient, m_Config.ChannelsClient) * (int)m_RecorderFactor;
                    }
                    else
                    {
                        bufferSize = WinSound.Utils.GetBytesPerInterval((uint)m_Config.SamplesPerSecondClient, m_Config.BitsPerSampleClient, m_Config.ChannelsClient);
                    }

                    if (bufferSize > 0)
                    {
                        m_Recorder_Client = new WinSound.Recorder();

                        m_Recorder_Client.DataRecorded += new WinSound.Recorder.DelegateDataRecorded(OnDataReceivedFromSoundcard_Client);
                        m_Recorder_Client.RecordingStopped += new WinSound.Recorder.DelegateStopped(OnRecordingStopped_Client);

                        if (m_Recorder_Client.Start(m_Config.SoundInputDeviceNameClient, m_Config.SamplesPerSecondClient, m_Config.BitsPerSampleClient, m_Config.ChannelsClient, m_SoundBufferCount, bufferSize))
                        {
                            ShowStreamingFromSounddeviceStarted_Client();

                            if (UseJitterBufferClientRecording)
                            {
                                m_JitterBufferClientRecording.Start();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void StartRecordingFromSounddevice_Server()
        {
            try
            {
                if (IsRecorderFromSounddeviceStarted_Server == false)
                {
                    int bufferSize = 0;
                    if (UseJitterBufferServerRecording)
                    {
                        bufferSize = WinSound.Utils.GetBytesPerInterval((uint)m_Config.SamplesPerSecondServer, m_Config.BitsPerSampleServer, m_Config.ChannelsServer) * (int)m_RecorderFactor;
                    }
                    else
                    {
                        bufferSize = WinSound.Utils.GetBytesPerInterval((uint)m_Config.SamplesPerSecondServer, m_Config.BitsPerSampleServer, m_Config.ChannelsServer);
                    }

                    if (bufferSize > 0)
                    {
                        m_Recorder_Server = new WinSound.Recorder();

                        m_Recorder_Server.DataRecorded += new WinSound.Recorder.DelegateDataRecorded(OnDataReceivedFromSoundcard_Server);
                        m_Recorder_Server.RecordingStopped += new WinSound.Recorder.DelegateStopped(OnRecordingStopped_Server);

                        if (m_Recorder_Server.Start(m_Config.SoundInputDeviceNameServer, m_Config.SamplesPerSecondServer, m_Config.BitsPerSampleServer, m_Config.ChannelsServer, m_SoundBufferCount, bufferSize))
                        {
                            ShowStreamingFromSounddeviceStarted_Server();

                            FormMain.DictionaryMixed[this] = new Queue<List<byte>>();

                            m_JitterBufferServerRecording.Start();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void StopRecordingFromSounddevice_Client()
        {
            try
            {
                if (IsRecorderFromSounddeviceStarted_Client)
                {
                    m_Recorder_Client.Stop();

                    m_Recorder_Client.DataRecorded -= new WinSound.Recorder.DelegateDataRecorded(OnDataReceivedFromSoundcard_Client);
                    m_Recorder_Client.RecordingStopped -= new WinSound.Recorder.DelegateStopped(OnRecordingStopped_Client);
                    m_Recorder_Client = null;

                    if (UseJitterBufferClientRecording)
                    {
                        m_JitterBufferClientRecording.Stop();
                    }

                    ShowStreamingFromSounddeviceStopped_Client();
                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void StopRecordingFromSounddevice_Server()
        {
            try
            {
                if (IsRecorderFromSounddeviceStarted_Server)
                {
                    m_Recorder_Server.Stop();

                    m_Recorder_Server.DataRecorded -= new WinSound.Recorder.DelegateDataRecorded(OnDataReceivedFromSoundcard_Server);
                    m_Recorder_Server.RecordingStopped -= new WinSound.Recorder.DelegateStopped(OnRecordingStopped_Server);
                    m_Recorder_Server = null;

                    m_JitterBufferServerRecording.Stop();

                    ShowStreamingFromSounddeviceStopped_Server();
                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void OnRecordingStopped_Client()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ShowStreamingFromSounddeviceStopped_Client();

                }));
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void OnRecordingStopped_Server()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    ShowStreamingFromSounddeviceStopped_Server();

                }));
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void OnDataReceivedFromSoundcard_Client(Byte[] data)
        {
            try
            {
                lock (this)
                {
                    if (IsClientConnected)
                    {
                        if (m_Config.ClientNoSpeakAll == false)
                        {

                            int bytesPerInterval = WinSound.Utils.GetBytesPerInterval((uint)m_Config.SamplesPerSecondClient, m_Config.BitsPerSampleClient, m_Config.ChannelsClient);
                            int count = data.Length / bytesPerInterval;
                            int currentPos = 0;
                            for (int i = 0; i < count; i++)
                            {
                                Byte[] partBytes = new Byte[bytesPerInterval];
                                Array.Copy(data, currentPos, partBytes, 0, bytesPerInterval);
                                currentPos += bytesPerInterval;
                                WinSound.RTPPacket rtp = ToRTPPacket(partBytes, m_Config.BitsPerSampleClient, m_Config.ChannelsClient);

                                if (UseJitterBufferClientRecording)
                                {
                                    m_JitterBufferClientRecording.AddData(rtp);
                                }
                                else
                                {
                                    Byte[] rtpBytes = ToRTPData(data, m_Config.BitsPerSampleClient, m_Config.ChannelsClient);
                                    m_Client.Send(m_PrototolClient.ToBytes(rtpBytes));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void OnDataReceivedFromSoundcard_Server(Byte[] data)
        {
            try
            {
                lock (this)
                {
                    if (IsServerRunning)
                    {
                        if (m_IsFormMain)
                        {
                            if (m_Config.ServerNoSpeakAll == false)
                            {
                                int bytesPerInterval = WinSound.Utils.GetBytesPerInterval((uint)m_Config.SamplesPerSecondServer, m_Config.BitsPerSampleServer, m_Config.ChannelsServer);
                                int count = data.Length / bytesPerInterval;
                                int currentPos = 0;
                                for (int i = 0; i < count; i++)
                                {
                                    Byte[] partBytes = new Byte[bytesPerInterval];
                                    Array.Copy(data, currentPos, partBytes, 0, bytesPerInterval);
                                    currentPos += bytesPerInterval;
                                    Queue<List<Byte>> q = FormMain.DictionaryMixed[this];
                                    if (q.Count < 10)
                                    {
                                        q.Enqueue(new List<Byte>(partBytes));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void OnJitterBufferClientDataAvailableRecording(Object sender, WinSound.RTPPacket rtp)
        {
            try
            {
                if (rtp != null && m_Client != null && rtp.Data != null && rtp.Data.Length > 0)
                {
                    if (IsClientConnected)
                    {
                        if (m_IsFormMain)
                        {

                            Byte[] rtpBytes = rtp.ToBytes();
                            m_Client.Send(m_PrototolClient.ToBytes(rtpBytes));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackFrame sf = new System.Diagnostics.StackFrame(true);
                ShowError(LabelClient, String.Format("Exception: {0} StackTrace: {1}. FileName: {2} Method: {3} Line: {4}", ex.Message, ex.StackTrace, sf.GetFileName(), sf.GetMethod(), sf.GetFileLineNumber()));
            }
        }
        private void OnJitterBufferClientDataAvailablePlaying(Object sender, WinSound.RTPPacket rtp)
        {
            try
            {
                if (m_PlayerClient != null)
                {
                    if (m_PlayerClient.Opened)
                    {
                        if (m_IsFormMain)
                        {
                            if (m_Config.MuteClientPlaying == false)
                            {
                                Byte[] linearBytes = WinSound.Utils.MuLawToLinear(rtp.Data, m_Config.BitsPerSampleClient, m_Config.ChannelsClient);

                                m_PlayerClient.PlayData(linearBytes, false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackFrame sf = new System.Diagnostics.StackFrame(true);
                ShowError(LabelClient, String.Format("Exception: {0} StackTrace: {1}. FileName: {2} Method: {3} Line: {4}", ex.Message, ex.StackTrace, sf.GetFileName(), sf.GetMethod(), sf.GetFileLineNumber()));
            }
        }
        private void OnJitterBufferServerDataAvailable(Object sender, WinSound.RTPPacket rtp)
        {
            try
            {
                if (IsServerRunning)
                {
                    if (m_IsFormMain)
                    {
                        Byte[] rtpBytes = rtp.ToBytes();

                        List<ServCli.ServerThread> list = new List<ServCli.ServerThread>(m_Server.Clients);
                        foreach (ServCli.ServerThread client in list)
                        {
                            if (client.IsMute == false)
                            {
                                try
                                {
                                    client.Send(m_PrototolClient.ToBytes(rtpBytes));
                                }
                                catch (Exception)
                                {
                                    RemoveControlInAllFlowLayoutPanelsByServerThread(client);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackFrame sf = new System.Diagnostics.StackFrame(true);
                ShowError(lblLogServer, String.Format("Exception: {0} StackTrace: {1}. FileName: {2} Method: {3} Line: {4}", ex.Message, ex.StackTrace, sf.GetFileName(), sf.GetMethod(), sf.GetFileLineNumber()));
            }
        }
        private Byte[] ToRTPData(Byte[] data, int bitsPerSample, int channels)
        {
            WinSound.RTPPacket rtp = ToRTPPacket(data, bitsPerSample, channels);

            Byte[] rtpBytes = rtp.ToBytes();

            return rtpBytes;
        }
        private WinSound.RTPPacket ToRTPPacket(Byte[] linearData, int bitsPerSample, int channels)
        {
            Byte[] mulaws = WinSound.Utils.LinearToMulaw(linearData, bitsPerSample, channels);

            WinSound.RTPPacket rtp = new WinSound.RTPPacket();

            rtp.Data = mulaws;
            rtp.CSRCCount = m_CSRCCount;
            rtp.Extension = m_Extension;
            rtp.HeaderLength = WinSound.RTPPacket.MinHeaderLength;
            rtp.Marker = m_Marker;
            rtp.Padding = m_Padding;
            rtp.PayloadType = m_PayloadType;
            rtp.Version = m_Version;
            rtp.SourceId = m_SourceId;

            try
            {
                rtp.SequenceNumber = Convert.ToUInt16(m_SequenceNumber);
                m_SequenceNumber++;
            }
            catch (Exception)
            {
                m_SequenceNumber = 0;
            }
            try
            {
                rtp.Timestamp = Convert.ToUInt32(m_TimeStamp);
                m_TimeStamp += mulaws.Length;
            }
            catch (Exception)
            {
                m_TimeStamp = 0;
            }

            return rtp;
        }
        private bool IsRecorderFromSounddeviceStarted_Client
        {
            get
            {
                if (m_Recorder_Client != null)
                {
                    return m_Recorder_Client.Started;
                }
                return false;
            }
        }
        private bool IsRecorderFromSounddeviceStarted_Server
        {
            get
            {
                if (m_Recorder_Server != null)
                {
                    return m_Recorder_Server.Started;
                }
                return false;
            }
        }
        private void InitComboboxes()
        {
            InitComboboxesClient();
            InitComboboxesServer();
        }
        private void InitComboboxesClient()
        {
            ComboboxOutputSoundDeviceNameClient.Items.Clear();
            ComboboxInputSoundDeviceNameClient.Items.Clear();
            List<String> playbackNames = WinSound.WinSound.GetPlaybackNames();
            List<String> recordingNames = WinSound.WinSound.GetRecordingNames();

            ComboboxOutputSoundDeviceNameClient.Items.Add("None");
            foreach (String name in playbackNames.Where(x => x != null))
            {
                ComboboxOutputSoundDeviceNameClient.Items.Add(name);
            }
            foreach (String name in recordingNames.Where(x => x != null))
            {
                ComboboxInputSoundDeviceNameClient.Items.Add(name);
            }

            if (ComboboxOutputSoundDeviceNameClient.Items.Count > 0)
            {
                ComboboxOutputSoundDeviceNameClient.SelectedIndex = 0;
            }
            if (ComboboxInputSoundDeviceNameClient.Items.Count > 0)
            {
                ComboboxInputSoundDeviceNameClient.SelectedIndex = 0;
            }
        }

        private void InitComboboxesServer()
        {
            ComboboxOutputSoundDeviceNameServer.Items.Clear();
            ComboboxInputSoundDeviceNameServer.Items.Clear();
            List<String> playbackNames = WinSound.WinSound.GetPlaybackNames();
            List<String> recordingNames = WinSound.WinSound.GetRecordingNames();

            foreach (String name in playbackNames.Where(x => x != null))
            {
                ComboboxOutputSoundDeviceNameServer.Items.Add(name);
            }
            foreach (String name in recordingNames.Where(x => x != null))
            {
                ComboboxInputSoundDeviceNameServer.Items.Add(name);
            }

            if (ComboboxOutputSoundDeviceNameServer.Items.Count > 0)
            {
                ComboboxOutputSoundDeviceNameServer.SelectedIndex = 0;
            }
            if (ComboboxInputSoundDeviceNameServer.Items.Count > 0)
            {
                ComboboxInputSoundDeviceNameServer.SelectedIndex = 0;
            }
        }

        private void ConnectClient()
        {
            try
            {
                if (IsClientConnected == false)
                {
                    if (m_Config.IpAddressClient.Length > 0 && m_Config.PortClient > 0)
                    {
                        m_Client = new ServCli.TCPClientVoz(m_Config.IpAddressClient, m_Config.PortClient);
                        m_Client.ClientConnected += new ServCli.TCPClientVoz.DelegateConnection(OnClientConnected);
                        m_Client.ClientDisconnected += new ServCli.TCPClientVoz.DelegateConnection(OnClientDisconnected);
                        m_Client.ExceptionAppeared += new ServCli.TCPClientVoz.DelegateException(OnClientExceptionAppeared);
                        m_Client.DataReceived += new ServCli.TCPClientVoz.DelegateDataReceived(OnClientDataReceived);
                        m_Client.Connect();
                    }
                }
            }
            catch (Exception ex)
            {
                m_Client = null;
                ShowError(LabelClient, ex.Message);
            }
        }
        private void DisconnectClient()
        {
            try
            {
                StopRecordingFromSounddevice_Client();

                if (m_Client != null)
                {
                    m_Client.Disconnect();
                    m_Client.ClientConnected -= new ServCli.TCPClientVoz.DelegateConnection(OnClientConnected);
                    m_Client.ClientDisconnected -= new ServCli.TCPClientVoz.DelegateConnection(OnClientDisconnected);
                    m_Client.ExceptionAppeared -= new ServCli.TCPClientVoz.DelegateException(OnClientExceptionAppeared);
                    m_Client.DataReceived -= new ServCli.TCPClientVoz.DelegateDataReceived(OnClientDataReceived);
                    m_Client = null;
                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void StartServer()
        {
            try
            {
                if (IsServerRunning == false)
                {
                    if (m_Config.IPAddressServer.Length > 0 && m_Config.PortServer > 0)
                    {
                        m_Server = new ServCli.TCPServidorVoz();
                        m_Server.ClientConnected += new ServCli.TCPServidorVoz.DelegateClientConnected(OnServerClientConnected);
                        m_Server.ClientDisconnected += new ServCli.TCPServidorVoz.DelegateClientDisconnected(OnServerClientDisconnected);
                        m_Server.DataReceived += new ServCli.TCPServidorVoz.DelegateDataReceived(OnServerDataReceived);
                        m_Server.Start(m_Config.IPAddressServer, m_Config.PortServer);

                        //Je nach Server Status
                        if (m_Server.State == ServCli.TCPServidorVoz.ListenerState.Started)
                        {
                            ShowServerStarted();
                        }
                        else
                        {
                            ShowServerStopped();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(lblLogServer, ex.Message);
            }
        }
        private void StopServer()
        {
            try
            {
                if (IsServerRunning)
                {

                    DeleteAllServerThreadDatas();

                    m_Server.Stop();
                    m_Server.ClientConnected -= new ServCli.TCPServidorVoz.DelegateClientConnected(OnServerClientConnected);
                    m_Server.ClientDisconnected -= new ServCli.TCPServidorVoz.DelegateClientDisconnected(OnServerClientDisconnected);
                    m_Server.DataReceived -= new ServCli.TCPServidorVoz.DelegateDataReceived(OnServerDataReceived);
                }

                if (m_Server != null)
                {
                    if (m_Server.State == ServCli.TCPServidorVoz.ListenerState.Started)
                    {
                        ShowServerStarted();
                    }
                    else
                    {
                        ShowServerStopped();
                    }
                }

                m_Server = null;
            }
            catch (Exception ex)
            {
                ShowError(lblLogServer, ex.Message);
            }
        }
        private void OnClientConnected(ServCli.TCPClientVoz client, string info)
        {
            ShowMessage(LabelClient, String.Format("Client connected {0}", ""));
            ShowClientConnected();
        }
        private void OnClientDisconnected(ServCli.TCPClientVoz client, string info)
        {
            StopPlayingToSounddevice_Client();

            StopRecordingFromSounddevice_Client();

            if (m_Client != null)
            {
                m_Client.ClientConnected -= new ServCli.TCPClientVoz.DelegateConnection(OnClientConnected);
                m_Client.ClientDisconnected -= new ServCli.TCPClientVoz.DelegateConnection(OnClientDisconnected);
                m_Client.ExceptionAppeared -= new ServCli.TCPClientVoz.DelegateException(OnClientExceptionAppeared);
                m_Client.DataReceived -= new ServCli.TCPClientVoz.DelegateDataReceived(OnClientDataReceived);
                ShowMessage(LabelClient, String.Format("Client disconnected {0}", ""));
            }

            ShowClientDisconnected();
        }
        private void OnClientExceptionAppeared(ServCli.TCPClientVoz client, Exception ex)
        {
            DisconnectClient();
            ShowError(LabelClient, ex.Message);
        }
        private void OnClientDataReceived(ServCli.TCPClientVoz client, Byte[] bytes)
        {
            try
            {
                if (m_PrototolClient != null)
                {
                    m_PrototolClient.Receive_LH(client, bytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void OnProtocolClient_DataComplete(Object sender, Byte[] data)
        {
            try
            {
                if (m_PlayerClient != null)
                {
                    if (m_PlayerClient.Opened)
                    {
                        WinSound.RTPPacket rtp = new WinSound.RTPPacket(data);

                        if (rtp.Data != null)
                        {
                            if (m_JitterBufferClientPlaying != null)
                            {
                                m_JitterBufferClientPlaying.AddData(rtp);
                            }
                        }
                    }
                }
                else
                {
                    OnClientConfigReceived(sender, data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void OnClientConfigReceived(Object sender, Byte[] data)
        {
            try
            {
                String msg = m_Encoding.GetString(data);
                if (msg.Length > 0)
                {
                    String[] values = msg.Split(':');
                    String cmd = values[0];

                    switch (cmd.ToUpper())
                    {
                        case "SAMPLESPERSECOND":
                            int samplePerSecond = Convert.ToInt32(values[1]);
                            m_Config.SamplesPerSecondClient = samplePerSecond;

                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                StartPlayingToSounddevice_Client();
                                StartRecordingFromSounddevice_Client();
                            }));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void OnServerClientConnected(ServCli.ServerThread st)
        {
            try
            {
                ServerThreadData data = new ServerThreadData();

                data.Init(st, m_Config.SoundOutputDeviceNameServer, m_Config.SamplesPerSecondServer, m_Config.BitsPerSampleServer, m_Config.ChannelsServer, m_SoundBufferCount, m_Config.JitterBufferCountServer, m_Milliseconds);

                m_DictionaryServerDatas[st] = data;

                AddServerClientToFlowLayoutPanel_ServerClient(st);
                AddServerClientToFlowLayoutPanel_ServerProgressBars(data);
                AddServerClientToFlowLayoutPanel_ServerListenButtons(data);
                AddServerClientToFlowLayoutPanel_ServerSpeakButtons(data);

                SendConfigurationToClient(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void SendConfigurationToClient(ServerThreadData data)
        {
            Byte[] bytesConfig = m_Encoding.GetBytes(String.Format("SamplesPerSecond:{0}", m_Config.SamplesPerSecondServer));
            data.ServerThread.Send(m_PrototolClient.ToBytes(bytesConfig));
        }
        private void OnServerClientDisconnected(ServCli.ServerThread st, string info)
        {
            try
            {
                if (m_DictionaryServerDatas.ContainsKey(st))
                {
                    ServerThreadData data = m_DictionaryServerDatas[st];
                    data.Dispose();
                    lock (LockerDictionary)
                    {
                        m_DictionaryServerDatas.Remove(st);
                    }
                    RemoveServerClientToFlowLayoutPanel_ServerClient(st);
                    RemoveServerClientToFlowLayoutPanel_ServerProgressBar(data);
                    RemoveServerClientToFlowLayoutPanel_ButtonListen(data);
                    RemoveServerClientToFlowLayoutPanel_ButtonSpeak(data);
                }

                FormMain.DictionaryMixed.Remove(st);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void StartTimerMixed()
        {
            if (m_TimerMixed == null)
            {
                m_TimerMixed = new WinSound.EventTimer();
                m_TimerMixed.TimerTick += new WinSound.EventTimer.DelegateTimerTick(OnTimerSendMixedDataToAllClients);
                m_TimerMixed.Start(20, 0);
            }
        }
        private void StopTimerMixed()
        {
            if (m_TimerMixed != null)
            {
                m_TimerMixed.Stop();
                m_TimerMixed.TimerTick -= new WinSound.EventTimer.DelegateTimerTick(OnTimerSendMixedDataToAllClients);
                m_TimerMixed = null;
            }
        }
        private void StartTimerDrawProgressBar()
        {
            if (m_TimerDrawProgressBar == null)
            {
                m_TimerDrawProgressBar = new System.Windows.Forms.Timer();
                m_TimerDrawProgressBar.Tick += new EventHandler(OnTimerDrawServerClientsProgressBars);
                m_TimerDrawProgressBar.Interval = 100;
                m_TimerDrawProgressBar.Start();
            }
        }
        private void StopTimerDrawProgressBar()
        {
            try
            {
                if (m_TimerDrawProgressBar != null)
                {
                    m_TimerDrawProgressBar.Stop();
                    m_TimerDrawProgressBar = null;

                    foreach (ProgressBar prog in FlowLayoutPanelServerProgressBars.Controls)
                    {
                        if (prog.Tag != null)
                        {
                            ServerThreadData stData = (ServerThreadData)prog.Tag;

                            if (stData.JitterBuffer != null)
                            {
                                prog.Value = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnTimerDrawServerClientsProgressBars(Object obj, EventArgs e)
        {
            try
            {
                foreach (ProgressBar prog in FlowLayoutPanelServerProgressBars.Controls)
                {
                    if (prog.Tag != null)
                    {
                        ServerThreadData stData = (ServerThreadData)prog.Tag;

                        if (stData.JitterBuffer != null)
                        {
                            prog.Value = stData.JitterBuffer.Length;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void RemoveControlByTag(Control.ControlCollection controls, object tag)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                Control existing = null;
                foreach (Control ctrl in controls)
                {
                    if (ctrl.Tag == tag)
                    {
                        existing = ctrl;
                        break;
                    }
                }

                if (existing != null)
                {
                    controls.Remove(existing);
                }
            }));
        }

        private void RemoveControlInAllFlowLayoutPanelsByServerThread(ServCli.ServerThread st)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                Control ctrlLabel = null;
                foreach (Control ctrl in FlowLayoutPanelServerClients.Controls)
                {
                    ServCli.ServerThread thread = (ServCli.ServerThread)ctrl.Tag;
                    if (thread == st)
                    {
                        ctrlLabel = ctrl;
                        break;
                    }
                }
                if (ctrlLabel != null)
                {
                    FlowLayoutPanelServerClients.Controls.Remove(ctrlLabel);
                }

                Control ctrlProgress = null;
                foreach (Control ctrl in FlowLayoutPanelServerProgressBars.Controls)
                {
                    ServerThreadData data = (ServerThreadData)ctrl.Tag;
                    if (data.ServerThread == st)
                    {
                        ctrlProgress = ctrl;
                        break;
                    }
                }
                if (ctrlProgress != null)
                {
                    FlowLayoutPanelServerProgressBars.Controls.Remove(ctrlProgress);
                }

                Control ctrlListen = null;
                foreach (Control ctrl in FlowLayoutPanelServerListen.Controls)
                {
                    ServerThreadData data = (ServerThreadData)ctrl.Tag;
                    if (data.ServerThread == st)
                    {
                        ctrlListen = ctrl;
                        break;
                    }
                }
                if (ctrlListen != null)
                {
                    FlowLayoutPanelServerListen.Controls.Remove(ctrlListen);
                }

                Control ctrlSpeak = null;
                foreach (Control ctrl in FlowLayoutPanelServerSpeak.Controls)
                {
                    ServerThreadData data = (ServerThreadData)ctrl.Tag;
                    if (data.ServerThread == st)
                    {
                        ctrlSpeak = ctrl;
                        break;
                    }
                }
                if (ctrlSpeak != null)
                {
                    FlowLayoutPanelServerSpeak.Controls.Remove(ctrlSpeak);
                }

            }));
        }
        private void RemoveServerClientToFlowLayoutPanel_ServerClient(ServCli.ServerThread st)
        {
            try
            {
                FlowLayoutPanelServerClients.Invoke(new MethodInvoker(delegate ()
                {
                    //Label löschen
                    RemoveControlByTag(FlowLayoutPanelServerClients.Controls, st);

                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void RemoveServerClientToFlowLayoutPanel_ButtonListen(ServerThreadData data)
        {
            try
            {
                FlowLayoutPanelServerListen.Invoke(new MethodInvoker(delegate ()
                {
                    RemoveControlByTag(FlowLayoutPanelServerListen.Controls, data);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RemoveServerClientToFlowLayoutPanel_ButtonSpeak(ServerThreadData data)
        {
            try
            {
                FlowLayoutPanelServerSpeak.Invoke(new MethodInvoker(delegate ()
                {
                    RemoveControlByTag(FlowLayoutPanelServerSpeak.Controls, data);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddServerClientToFlowLayoutPanel_ServerClient(ServCli.ServerThread st)
        {
            try
            {
                FlowLayoutPanelServerClients.Invoke(new MethodInvoker(delegate ()
                {
                    Label lab = new Label();
                    lab.AutoSize = false;
                    lab.BackColor = Color.DimGray;
                    lab.ForeColor = Color.White;
                    lab.Font = new Font(lab.Font, FontStyle.Bold);
                    lab.Margin = new Padding(5, FlowLayoutPanelServerClients.Controls.Count > 0 ? 5 : 10, 0, 5);
                    lab.TextAlign = ContentAlignment.MiddleCenter;
                    lab.Width = FlowLayoutPanelServerClients.Width - 10;
                    lab.Text = txtBoxUsuario.Text + " : " + txtBoxPortaCliente.Text;
                    lab.Tag = st;

                    FlowLayoutPanelServerClients.Controls.Add(lab);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddServerClientToFlowLayoutPanel_ServerProgressBars(ServerThreadData stData)
        {
            try
            {
                FlowLayoutPanelServerProgressBars.Invoke(new MethodInvoker(delegate ()
                {
                    ProgressBar prog = new ProgressBar();
                    prog.AutoSize = false;
                    prog.Margin = new Padding(5, FlowLayoutPanelServerProgressBars.Controls.Count > 0 ? 5 : 10, 0, 5);
                    prog.Width = FlowLayoutPanelServerProgressBars.Width - 20;
                    prog.BackColor = Color.White;
                    prog.Maximum = (int)stData.JitterBuffer.Maximum;
                    prog.Tag = stData;

                    FlowLayoutPanelServerProgressBars.Controls.Add(prog);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddServerClientToFlowLayoutPanel_ServerListenButtons(ServerThreadData stData)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    Button btnListen = new Button();
                    btnListen.Width = 26;
                    btnListen.Height = 27;
                    btnListen.Margin = new Padding(0, FlowLayoutPanelServerListen.Controls.Count > 0 ? 3 : 8, 0, 3);
                    btnListen.Tag = stData;
                    btnListen.BackColor = Color.LightGray;
                    btnListen.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Listen_On.png");
                    btnListen.BackgroundImageLayout = ImageLayout.Zoom;
                    btnListen.Tag = stData;
                    btnListen.MouseClick += new MouseEventHandler(OnButtonServerThreadListenClick);

                    FlowLayoutPanelServerListen.Controls.Add(btnListen);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AddServerClientToFlowLayoutPanel_ServerSpeakButtons(ServerThreadData stData)
        {
            try
            {
                FlowLayoutPanelServerSpeak.Invoke(new MethodInvoker(delegate ()
                {
                    Button btnSpeak = new Button();
                    btnSpeak.Width = 26;
                    btnSpeak.Height = 27;
                    btnSpeak.Margin = new Padding(0, FlowLayoutPanelServerSpeak.Controls.Count > 0 ? 3 : 8, 0, 3);
                    btnSpeak.Tag = stData;
                    btnSpeak.ImageAlign = ContentAlignment.MiddleCenter;
                    btnSpeak.BackColor = Color.LightGray;
                    btnSpeak.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Speak_On.png");
                    btnSpeak.BackgroundImageLayout = ImageLayout.Zoom;
                    btnSpeak.Tag = stData;
                    btnSpeak.MouseClick += new MouseEventHandler(OnButtonServerThreadSpeakClick);

                    FlowLayoutPanelServerSpeak.Controls.Add(btnSpeak);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void OnButtonServerThreadListenClick(Object sender, MouseEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Tag != null)
                {
                    ServerThreadData data = (ServerThreadData)btn.Tag;

                    data.IsMute = !data.IsMute;

                    if (data.IsMute)
                    {
                        btn.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Listen_Off.png");
                        btn.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else
                    {
                        btn.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Listen_On.png");
                        btn.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(lblLogServer, ex.Message);
            }
        }
        private void OnButtonServerThreadSpeakClick(Object sender, MouseEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Tag != null)
                {
                    ServerThreadData data = (ServerThreadData)btn.Tag;

                    data.ServerThread.IsMute = !data.ServerThread.IsMute;

                    if (data.ServerThread.IsMute)
                    {
                        btn.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Speak_Off.png");
                        btn.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                    else
                    {
                        btn.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Speak_On.png");
                        btn.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(lblLogServer, ex.Message);
            }
        }

        private void RemoveServerClientToFlowLayoutPanel_ServerProgressBar(ServerThreadData data)
        {
            try
            {
                FlowLayoutPanelServerProgressBars.Invoke(new MethodInvoker(delegate ()
                {
                    RemoveControlByTag(FlowLayoutPanelServerProgressBars.Controls, data);
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnServerDataReceived(ServCli.ServerThread st, Byte[] data)
        {
            if (m_DictionaryServerDatas.ContainsKey(st))
            {
                ServerThreadData stData = m_DictionaryServerDatas[st];
                if (stData.Protocol != null)
                {
                    stData.Protocol.Receive_LH(st, data);
                }
            }
        }
        private void DeleteAllServerThreadDatas()
        {
            lock (LockerDictionary)
            {
                try
                {
                    foreach (ServerThreadData info in m_DictionaryServerDatas.Values)
                    {
                        info.Dispose();
                    }
                    m_DictionaryServerDatas.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private bool IsServerRunning
        {
            get
            {
                if (m_Server != null)
                {
                    return m_Server.State == ServCli.TCPServidorVoz.ListenerState.Started;
                }
                return false;
            }
        }
          private bool IsClientConnected
        {
            get
            {
                if (m_Client != null)
                {
                    return m_Client.Connected;
                }
                return false;
            }
        }
        private void ShowClientConnected()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    btnConectarCliente.Text = "Desconectar";
                    btnConectarCliente.BackColor = Color.Green;
                    btnConectarCliente.ForeColor = Color.White;
                    TextBoxClientAddress.Enabled = false;
                    txtBoxPortaCliente.Enabled = false;
                    NumericUpDownJitterBufferClient.Enabled = false;
                    ComboboxOutputSoundDeviceNameClient.Enabled = false;
                    ComboboxInputSoundDeviceNameClient.Enabled = false;
                    ProgressBarPlayingClient.Visible = true;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ShowClientDisconnected()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    btnConectarCliente.Text = "Conectar";
                    btnConectarCliente.BackColor = Color.Gray;
                    btnConectarCliente.ForeColor = Color.Black;
                    TextBoxClientAddress.Enabled = true;
                    txtBoxPortaCliente.Enabled = true;
                    NumericUpDownJitterBufferClient.Enabled = true;
                    ComboboxOutputSoundDeviceNameClient.Enabled = true;
                    ComboboxInputSoundDeviceNameClient.Enabled = true;
                    ProgressBarPlayingClient.Visible = false;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ShowServerStarted()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    btnAbrirConexao.BackColor = Color.Green;
                    btnAbrirConexao.ForeColor = Color.White;
                    NumericUpDownJitterBufferServer.Enabled = false;
                    ComboboxOutputSoundDeviceNameServer.Enabled = false;
                    ComboboxInputSoundDeviceNameServer.Enabled = false;
                    txtIpAdress.Enabled = false;
                    TextBoxServerPort.Enabled = false;
                    ComboboxSamplesPerSecondServer.Enabled = false;
                    StartTimerDrawProgressBar();
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ShowServerStopped()
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    btnAbrirConexao.BackColor = Color.Green;
                    btnAbrirConexao.ForeColor = Color.Black;
                    StopTimerDrawProgressBar();
                    FlowLayoutPanelServerClients.Controls.Clear();
                    FlowLayoutPanelServerProgressBars.Controls.Clear();
                    FlowLayoutPanelServerListen.Controls.Clear();
                    FlowLayoutPanelServerSpeak.Controls.Clear();
                    NumericUpDownJitterBufferServer.Enabled = true;
                    ComboboxOutputSoundDeviceNameServer.Enabled = true;
                    ComboboxInputSoundDeviceNameServer.Enabled = true;
                    txtIpAdress.Enabled = true;
                    TextBoxServerPort.Enabled = true;
                    ComboboxSamplesPerSecondServer.Enabled = true;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ShowStreamingFromSounddeviceStarted_Client()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        ShowStreamingFromSounddeviceStarted_Client();
                    }));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void ShowStreamingFromSounddeviceStopped_Client()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        ShowStreamingFromSounddeviceStopped_Client();
                    }));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void ShowStreamingFromSounddeviceStarted_Server()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        ShowStreamingFromSounddeviceStarted_Server();
                    }));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void ShowStreamingFromSounddeviceStopped_Server()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        ShowStreamingFromSounddeviceStopped_Server();
                    }));
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void ShowStreamingFromFileStarted()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        ShowStreamingFromFileStarted();
                    }));
                }
                else
                {
                    ComboboxInputSoundDeviceNameClient.Enabled = false;
                    ProgressBarPlayingClient.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void ShowStreamingFromFileStopped()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        ShowStreamingFromFileStopped();
                    }));
                }
                else
                {
                    ComboboxInputSoundDeviceNameClient.Enabled = true;
                    ProgressBarPlayingClient.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }
        private void ShowError(Label lb, string text)
        {
            try
            {
                lb.Invoke(new MethodInvoker(delegate ()
                {
                    lb.Text = text;
                    lb.ForeColor = Color.Red;

                    if (lb == LabelClient)
                    {
                        btnConectarCliente.BackColor = Color.Gray;
                    }
                    else if (lb == lblLogServer)
                    {
                        btnAbrirConexao.BackColor = Color.Gray;
                    }
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ShowMessage(Label lb, string text)
        {
            try
            {
                lb.Invoke(new MethodInvoker(delegate ()
                {
                    lb.Text = text;
                    lb.ForeColor = Color.Black;
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private bool FormToConfig()
        {
            try
            {
                m_Config.IpAddressClient = TextBoxClientAddress.Text;
                m_Config.IPAddressServer = txtIpAdress.Text;
                m_Config.PortClient = Convert.ToInt32(txtBoxPortaCliente.Text);
                m_Config.PortServer = Convert.ToInt32(TextBoxServerPort.Text);
                m_Config.SoundInputDeviceNameClient = ComboboxInputSoundDeviceNameClient.SelectedIndex >= 0 ? ComboboxInputSoundDeviceNameClient.SelectedItem.ToString() : "";
                m_Config.SoundOutputDeviceNameClient = ComboboxOutputSoundDeviceNameClient.SelectedIndex >= 0 ? ComboboxOutputSoundDeviceNameClient.SelectedItem.ToString() : "";
                m_Config.SoundInputDeviceNameServer = ComboboxInputSoundDeviceNameServer.SelectedIndex >= 0 ? ComboboxInputSoundDeviceNameServer.SelectedItem.ToString() : "";
                m_Config.SoundOutputDeviceNameServer = ComboboxOutputSoundDeviceNameServer.SelectedIndex >= 0 ? ComboboxOutputSoundDeviceNameServer.SelectedItem.ToString() : "";
                m_Config.JitterBufferCountServer = (uint)NumericUpDownJitterBufferServer.Value;
                m_Config.JitterBufferCountClient = (uint)NumericUpDownJitterBufferClient.Value;
                m_Config.SamplesPerSecondServer = ComboboxSamplesPerSecondServer.SelectedIndex >= 0 ? Convert.ToInt32(ComboboxSamplesPerSecondServer.SelectedItem.ToString()) : 8000;
                m_Config.BitsPerSampleServer = 16;
                m_Config.BitsPerSampleClient = 16;
                m_Config.ChannelsServer = 1;
                m_Config.ChannelsClient = 1;
                m_Config.UseJitterBufferClientRecording = true;
                m_Config.UseJitterBufferServerRecording = true;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler bei der Eingabe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool ConfigToForm()
        {
            try
            {
                TextBoxClientAddress.Text = m_Config.IpAddressClient.ToString();
                txtIpAdress.Text = m_Config.IPAddressServer.ToString();
                txtBoxPortaCliente.Text = m_Config.PortClient.ToString();
                TextBoxServerPort.Text = m_Config.PortServer.ToString();
                ComboboxInputSoundDeviceNameClient.SelectedIndex = ComboboxInputSoundDeviceNameClient.FindString(m_Config.SoundInputDeviceNameClient);
                ComboboxOutputSoundDeviceNameClient.SelectedIndex = ComboboxOutputSoundDeviceNameClient.FindString(m_Config.SoundOutputDeviceNameClient);
                ComboboxInputSoundDeviceNameServer.SelectedIndex = ComboboxInputSoundDeviceNameServer.FindString(m_Config.SoundInputDeviceNameServer);
                ComboboxOutputSoundDeviceNameServer.SelectedIndex = ComboboxOutputSoundDeviceNameServer.FindString(m_Config.SoundOutputDeviceNameServer);
                NumericUpDownJitterBufferServer.Value = m_Config.JitterBufferCountServer;
                NumericUpDownJitterBufferClient.Value = m_Config.JitterBufferCountClient;
                ComboboxSamplesPerSecondServer.SelectedIndex = ComboboxSamplesPerSecondServer.FindString(m_Config.SamplesPerSecondServer.ToString());

                ShowButtonServerSpeak();
                ShowButtonClientListen();
                ShowButtonServerListen();
                ShowButtonClientSpeak();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void SaveConfig()
        {
            try
            {
                FormToConfig();
                XmlSerializer ser = new XmlSerializer(typeof(Configuration));
                FileStream stream = new FileStream(m_ConfigFileName, FileMode.Create);
                ser.Serialize(stream, m_Config);
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadConfig()
        {
            try
            {
                if (File.Exists(m_ConfigFileName))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Configuration));
                    StreamReader sr = new StreamReader(m_ConfigFileName);
                    m_Config = (Configuration)ser.Deserialize(sr);
                    sr.Close();
                }

                ConfigToForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_IsFormMain = false;

                StopRecordingFromSounddevice_Server();

                StopRecordingFromSounddevice_Client();

                DisconnectClient();

                StopServer();

                SaveConfig();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ButtonClient_Click(object sender, EventArgs e)
        {
            try
            {
                FormToConfig();
                TrataComportamentoDesignCliente();
                if (IsClientConnected)
                {
                    DisconnectClient();
                    StopRecordingFromSounddevice_Client();
                }
                else
                {
                    ConnectClient();
                    AbrirConexaoParaTexto();
                }

                System.Threading.Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }

        private void TrataComportamentoDesignCliente()
        {
            if (!IsClientConnected)
            {
                btnConectarCliente.Text = "Conectar";
                btnConectarCliente.ForeColor = Color.Black;
                btnConectarCliente.BackColor = Color.Green;
            }
            else
            {
                btnConectarCliente.Text = "Desconectar";
                btnConectarCliente.ForeColor = Color.White;
                btnConectarCliente.BackColor = Color.Green;

            }
        }

        private void ButtonServer_Click(object sender, EventArgs e)
        {
            try
            {
                FormToConfig();
                TrataComportamentoDesign();

                if (IsServerRunning)
                {
                    StopServer();
                    StopRecordingFromSounddevice_Server();
                    StopTimerMixed();
                    lblLogServer.Text = "Conexão Fechada...\r\n";
                }
                else
                {
                    StartServer();
                    AbreConexaoChatTexto();

                    if (!m_Config.ServerNoSpeakAll)
                    {
                        StartRecordingFromSounddevice_Server();
                    }

                    StartTimerMixed();
                }
            }
            catch (Exception ex)
            {
                ShowError(lblLogServer, ex.Message);
            }
        }

        private void TrataComportamentoDesign()
        {
            if (!IsServerRunning)
            {
                btnAbrirConexao.Text = "Fechar";
                btnAbrirConexao.ForeColor = Color.Black;
                btnAbrirConexao.BackColor = Color.Green;
            }
            else
            {
                btnAbrirConexao.Text = "Abrir";
                btnAbrirConexao.ForeColor = Color.White;
                btnAbrirConexao.BackColor = Color.Green;

            }

        }

        private void NumericUpDownJitterBufferServer_ValueChanged(object sender, EventArgs e)
        {
            m_Config.JitterBufferCountServer = (uint)NumericUpDownJitterBufferServer.Value;
        }
        private void NumericUpDownJitterBufferClient_ValueChanged(object sender, EventArgs e)
        {
            m_Config.JitterBufferCountClient = (uint)NumericUpDownJitterBufferClient.Value;
        }

        private void AbreConexaoChatTexto()
        {
            // Analisa o endereço IP do servidor informado no textbox
            IPAddress enderecoIP = IPAddress.Parse(txtIpAdress.Text);

            // Cria uma nova instância do objeto ChatServidor
            TcpServidorChat mainServidor = new TcpServidorChat(enderecoIP);


            // Inicia o atendimento das conexões
            mainServidor.IniciaAtendimento();

            // Mostra que nos iniciamos o atendimento para conexões
            lblLogServer.Text = "Monitorando as conexões de Texto...\r\n";

        }

        public void mainServidor_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Chama o método que atualiza o formulário
            this.Invoke(new AtualizaStatusCallback(this.AtualizaStatus), new object[] { e.EventMessage });
        }

        private void AtualizaStatus(string strMensagem)
        {
            // Atualiza o logo com mensagens
            lblLogServer.Text = strMensagem + "\r\n";
        }

        private bool IsPlayingToSoundDeviceWanted
        {
            get
            {
                if (ComboboxOutputSoundDeviceNameClient.SelectedIndex >= 1)
                {
                    return true;
                }
                return false;
            }
        }
        private void StartPlayingToSounddevice_Client()
        {
            if (IsPlayingToSoundDeviceWanted)
            {
                if (m_JitterBufferClientPlaying != null)
                {
                    InitJitterBufferClientPlaying();
                    m_JitterBufferClientPlaying.Start();
                }

                if (m_PlayerClient == null)
                {
                    m_PlayerClient = new WinSound.Player();
                    m_PlayerClient.Open(m_Config.SoundOutputDeviceNameClient, m_Config.SamplesPerSecondClient, m_Config.BitsPerSampleClient, m_Config.ChannelsClient, (int)m_Config.JitterBufferCountClient);
                }

                m_TimerProgressBarPlayingClient.Start();

            }

            ComboboxOutputSoundDeviceNameClient.Invoke(new MethodInvoker(delegate ()
            {
                ComboboxOutputSoundDeviceNameClient.Enabled = false;
                NumericUpDownJitterBufferClient.Enabled = false;
                ProgressBarPlayingClient.Maximum = (int)m_JitterBufferClientPlaying.Maximum;
            }));

        }
        private void StopPlayingToSounddevice_Client()
        {
            if (m_PlayerClient != null)
            {
                m_PlayerClient.Close();
                m_PlayerClient = null;
            }

            if (m_JitterBufferClientPlaying != null)
            {
                m_JitterBufferClientPlaying.Stop();
            }

            m_TimerProgressBarPlayingClient.Stop();

            this.Invoke(new MethodInvoker(delegate ()
            {
                ComboboxOutputSoundDeviceNameClient.Enabled = true;
                NumericUpDownJitterBufferClient.Enabled = true;
                ProgressBarPlayingClient.Value = 0;
            }));
        }
        private void ButtonServerSpeak_Click(object sender, EventArgs e)
        {
            m_Config.ServerNoSpeakAll = !m_Config.ServerNoSpeakAll;

            if (m_Config.ServerNoSpeakAll)
            {
                StopRecordingFromSounddevice_Server();
            }
            else
            {
                StartRecordingFromSounddevice_Server();
            }

            ShowButtonServerSpeak();
        }
        private void ShowButtonServerSpeak()
        {
            if (m_Config.ServerNoSpeakAll)
            {
                btnServidorMic.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Speak_Off.png");
                btnServidorMic.BackgroundImageLayout = ImageLayout.Zoom;
            }
            else
            {
                btnServidorMic.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Speak_On.png");
                btnServidorMic.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }
        private void ShowButtonClientSpeak()
        {
            if (m_Config.ClientNoSpeakAll)
            {
                btnClienteMic.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Speak_Off.png");
                btnClienteMic.BackgroundImageLayout = ImageLayout.Zoom;
            }
            else
            {
                btnClienteMic.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Speak_On.png");
                btnClienteMic.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }
        private void ButtonClientListen_Click(object sender, EventArgs e)
        {
            m_Config.MuteClientPlaying = !m_Config.MuteClientPlaying;
            ShowButtonClientListen();
        }

        private void ShowButtonClientListen()
        {
            if (m_Config.MuteClientPlaying)
            {
                btncClienteAudio.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Listen_Off.png");
                btncClienteAudio.BackgroundImageLayout = ImageLayout.Zoom;
            }
            else
            {
                btncClienteAudio.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Listen_On.png");
                btncClienteAudio.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }
        private void ButtonServerListen_Click(object sender, EventArgs e)
        {

            m_Config.MuteServerListen = !m_Config.MuteServerListen;


            ShowButtonServerListen();
        }
        private void ShowButtonServerListen()
        {
            if (m_Config.MuteServerListen)
            {
                btnServidorAudio.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Listen_Off.png");
                btnServidorAudio.BackgroundImageLayout = ImageLayout.Zoom;
            }
            else
            {
                btnServidorAudio.BackgroundImage = Image.FromFile(@"D:\SALVAR ESTA PASTA\MATERIAS FACULDADE\APS\TcpClienteOficial\APS-Servidor-e-Cliente\TCPStreamer\TCPStreamer\Icones\Listen_On.png");
                btnServidorAudio.BackgroundImageLayout = ImageLayout.Zoom;
            }

            ServerThreadData.IsMuteAll = m_Config.MuteServerListen;
        }
        private void ButtonClientSpeak_Click(object sender, EventArgs e)
        {
            try
            {
                m_Config.ClientNoSpeakAll = !m_Config.ClientNoSpeakAll;

                ShowButtonClientSpeak();
            }
            catch (Exception ex)
            {
                ShowError(LabelClient, ex.Message);
            }
        }

        private void btnEnviarMsg_Click(object sender, EventArgs e)
        {
            EnviaMensagem();

        }

        private void EnviaMensagem()
        {
            if (txtBoxMsg.Lines.Length >= 1)
            {
                stwEnviador.WriteLine(txtBoxMsg.Text);
                stwEnviador.Flush();
                txtBoxMsg.Lines = null;
            }
            txtBoxMsg.Text = "";
        }

        private void AtualizaMensagens(string strMensagem)
        {
            txtboxMensagens.AppendText(strMensagem + "\r\n");
        }

        private void RecebeMensagens()
        {

            strReceptor = new StreamReader(tcpServidor.GetStream());
            string ConResposta = strReceptor.ReadLine();

            if (IsClientConnected)
            {
                // Atualiza o formulário para informar que esta conectado
                this.Invoke(new AtualizaLogCallBack(this.AtualizaMensagens), new object[] { "Conectado com sucesso!" });
            }
            else 
            {
                string Motivo = "Não Conectado: ";
                // Extrai o motivo da mensagem resposta. O motivo começa no 3o caractere
                Motivo += ConResposta.Substring(2, ConResposta.Length - 2);
                // Atualiza o formulário como o motivo da falha na conexão
                this.Invoke(new FechaConexaoCallBack(this.FechaConexao), new object[] { Motivo });
                // Sai do método
                return;
            }

            // Enquanto estiver conectado le as linhas que estão chegando do servidor
            while (IsClientConnected)
            {
                // exibe mensagems no Textbox
                this.Invoke(new AtualizaLogCallBack(this.AtualizaMensagens), new object[] { strReceptor.ReadLine() });
            }
        }

        private void FechaConexao(string Motivo)
        {
            // Mostra o motivo porque a conexão encerrou
            txtboxMensagens.AppendText(Motivo + "\r\n");
            // Habilita e desabilita os controles apropriados no formulario

            // Fecha os objetos

            stwEnviador.Close();
            strReceptor.Close();
            DisconnectClient();
        }

        private void AbrirConexaoParaTexto()
        {

            tcpServidor = new System.Net.Sockets.TcpClient();
            tcpServidor.Connect(TextBoxClientAddress.Text, 2502);

            stwEnviador = new StreamWriter(tcpServidor.GetStream());
            stwEnviador.WriteLine(txtBoxUsuario.Text);
            stwEnviador.Flush();

            mensagemThread = new Thread(new ThreadStart(RecebeMensagens));
            mensagemThread.Start();

            stwEnviador = new StreamWriter(tcpServidor.GetStream());
            stwEnviador.WriteLine(txtBoxUsuario.Text);
            stwEnviador.Flush();

        }

        private void btnCamera_Click(object sender, EventArgs e)
        {
            webCamPainel.ShowDialog();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            webCamPainel = new flyoutWebCam();
            webCamPainel.Visible = false;

        }


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnEnviarMsg.PerformClick();
            }
        }

        public class Configuration
        {
            public Configuration()
            {

            }

            public String IpAddressClient = "";
            public String IPAddressServer = "";
            public int PortClient = 0;
            public int PortServer = 0;
            public String SoundInputDeviceNameClient = "";
            public String SoundOutputDeviceNameClient = "";
            public String SoundInputDeviceNameServer = "";
            public String SoundOutputDeviceNameServer = "";
            public int SamplesPerSecondClient = 8000;
            public int BitsPerSampleClient = 16;
            public int ChannelsClient = 1;
            public int SamplesPerSecondServer = 8000;
            public int BitsPerSampleServer = 16;
            public int ChannelsServer = 1;
            public bool UseJitterBufferClientRecording = true;
            public bool UseJitterBufferServerRecording = true;
            public uint JitterBufferCountServer = 20;
            public uint JitterBufferCountClient = 20;
            public string FileName = "";
            public bool LoopFile = false;
            public bool MuteClientPlaying = false;
            public bool ServerNoSpeakAll = false;
            public bool ClientNoSpeakAll = false;
            public bool MuteServerListen = false;
        }

        public class ServerThreadData
        {
            public ServerThreadData()
            {

            }

            public ServCli.ServerThread ServerThread;
            public WinSound.Player Player;
            public WinSound.JitterBuffer JitterBuffer;
            public WinSound.Protocol Protocol;
            public int SamplesPerSecond = 8000;
            public int BitsPerSample = 16;
            public int SoundBufferCount = 8;
            public uint JitterBufferCount = 20;
            public uint JitterBufferMilliseconds = 20;
            public int Channels = 1;
            private bool IsInitialized = false;
            public bool IsMute = false;
            public static bool IsMuteAll = false;

               public void Init(ServCli.ServerThread st, string soundDeviceName, int samplesPerSecond, int bitsPerSample, int channels, int soundBufferCount, uint jitterBufferCount, uint jitterBufferMilliseconds)
            {
                this.ServerThread = st;
                this.SamplesPerSecond = samplesPerSecond;
                this.BitsPerSample = bitsPerSample;
                this.Channels = channels;
                this.SoundBufferCount = soundBufferCount;
                this.JitterBufferCount = jitterBufferCount;
                this.JitterBufferMilliseconds = jitterBufferMilliseconds;

                this.Player = new WinSound.Player();
                this.Player.Open(soundDeviceName, samplesPerSecond, bitsPerSample, channels, soundBufferCount);

                if (jitterBufferCount >= 2)
                {
                    this.JitterBuffer = new WinSound.JitterBuffer(st, jitterBufferCount, jitterBufferMilliseconds);
                    this.JitterBuffer.DataAvailable += new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferDataAvailable);
                    this.JitterBuffer.Start();
                }

                this.Protocol = new WinSound.Protocol(WinSound.ProtocolTypes.LH, Encoding.Default);
                this.Protocol.DataComplete += new WinSound.Protocol.DelegateDataComplete(OnProtocolDataComplete);

                FormMain.DictionaryMixed[st] = new Queue<List<byte>>();

                IsInitialized = true;
            }
            public void Dispose()
            {
                if (Protocol != null)
                {
                    this.Protocol.DataComplete -= new WinSound.Protocol.DelegateDataComplete(OnProtocolDataComplete);
                    this.Protocol = null;
                }

                if (JitterBuffer != null)
                {
                    JitterBuffer.Stop();
                    JitterBuffer.DataAvailable -= new WinSound.JitterBuffer.DelegateDataAvailable(OnJitterBufferDataAvailable);
                    this.JitterBuffer = null;
                }

                if (Player != null)
                {
                    Player.Close();
                    this.Player = null;
                }

                IsInitialized = false;
            }
            private void OnProtocolDataComplete(Object sender, Byte[] bytes)
            {
                if (IsInitialized)
                {
                    if (ServerThread != null && Player != null)
                    {
                        try
                        {
                            if (Player.Opened)
                            {

                                WinSound.RTPPacket rtp = new WinSound.RTPPacket(bytes);

                                if (rtp.Data != null)
                                {
                                    if (JitterBuffer != null && JitterBuffer.Maximum >= 2)
                                    {
                                        JitterBuffer.AddData(rtp);
                                    }
                                    else
                                    {
                                        if (IsMuteAll == false && IsMute == false)
                                        {
                                            Byte[] linearBytes = WinSound.Utils.MuLawToLinear(rtp.Data, this.BitsPerSample, this.Channels);

                                            Player.PlayData(linearBytes, false);
                                        }
                                    }
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            IsInitialized = false;
                        }
                    }
                }
            }
            private void OnJitterBufferDataAvailable(Object sender, WinSound.RTPPacket rtp)
            {
                try
                {
                    if (Player != null)
                    {
                        Byte[] linearBytes = WinSound.Utils.MuLawToLinear(rtp.Data, BitsPerSample, Channels);

                        if (IsMuteAll == false && IsMute == false)
                        {
                            Player.PlayData(linearBytes, false);
                        }

                        Queue<List<Byte>> q = FormMain.DictionaryMixed[sender];
                        if (q.Count < 10)
                        {
                            FormMain.DictionaryMixed[sender].Enqueue(new List<Byte>(linearBytes));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("FormMain.cs | OnJitterBufferDataAvailable() | {0}", ex.Message));
                }
            }
        }
    }
}