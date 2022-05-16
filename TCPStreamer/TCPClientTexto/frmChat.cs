using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Collections;


namespace NF
{
    public partial class frmChat : Form
    {
		private NF.TCPClient m_Client;
		private NF.TCPServer m_Server;
		private Configuration m_Config = new Configuration();
		private String m_ConfigFileName = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "config.xml");
		private int m_SoundBufferCount = 8;
		private Dictionary<NF.ServerThread, ServerThreadData> m_DictionaryServerDatas = new Dictionary<NF.ServerThread, ServerThreadData>();
		private uint m_RecorderFactor = 4;
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
		private Byte[] m_FilePayloadBuffer;
		private int m_RTPPartsLength = 0;
		private uint m_Milliseconds = 20;
		System.Windows.Forms.Timer m_TimerDrawProgressBar;
		private Object LockerDictionary = new Object();
		public static Dictionary<Object, Queue<List<Byte>>> DictionaryMixed = new Dictionary<Object, Queue<List<byte>>>();
		private Encoding m_Encoding = Encoding.GetEncoding(1252); 
		private const int RecordingJitterBufferCount = 8;

		public frmChat()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
			try
			{

				FormToConfig();

				if (IsClientConnected)
				{
					DisconnectClient();
					StopRecordingFromSounddevice_Client();
				}
				else
				{
					ConnectClient();
				}

				//Kurz warten
				System.Threading.Thread.Sleep(100);
			}
			catch (Exception ex)
			{
				ShowError(LabelClient, ex.Message);
			}
		}
    }
}
