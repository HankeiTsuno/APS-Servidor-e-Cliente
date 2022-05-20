﻿namespace TCPStreamer
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.GroupBoxClient = new System.Windows.Forms.GroupBox();
            this.txtBoxUsuario = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtboxMensagens = new System.Windows.Forms.TextBox();
            this.LabelIPAddressServer = new System.Windows.Forms.Label();
            this.TextBoxClientAddress = new System.Windows.Forms.TextBox();
            this.NumericUpDownJitterBufferClient = new System.Windows.Forms.NumericUpDown();
            this.btnConectarCliente = new System.Windows.Forms.Button();
            this.LabelJitterBufferClient = new System.Windows.Forms.Label();
            this.txtBoxPortaCliente = new System.Windows.Forms.TextBox();
            this.LabelPortServer = new System.Windows.Forms.Label();
            this.LabelClient = new System.Windows.Forms.Label();
            this.btncClienteAudio = new System.Windows.Forms.Button();
            this.LabelOutputSoundDeviceNameClient = new System.Windows.Forms.Label();
            this.ComboboxOutputSoundDeviceNameClient = new System.Windows.Forms.ComboBox();
            this.LabelIPAddressClient = new System.Windows.Forms.Label();
            this.txtIpAdress = new System.Windows.Forms.TextBox();
            this.TextBoxServerPort = new System.Windows.Forms.TextBox();
            this.LabelServerPort = new System.Windows.Forms.Label();
            this.GroupBoxServer = new System.Windows.Forms.GroupBox();
            this.btnAbrirConexao = new System.Windows.Forms.Button();
            this.ComboboxSamplesPerSecondServer = new System.Windows.Forms.ComboBox();
            this.LabelJitterBufferServer = new System.Windows.Forms.Label();
            this.LabelSamplesPerSecondServer = new System.Windows.Forms.Label();
            this.NumericUpDownJitterBufferServer = new System.Windows.Forms.NumericUpDown();
            this.lblLogServer = new System.Windows.Forms.Label();
            this.lblEntradasSistema = new System.Windows.Forms.Label();
            this.ComboboxOutputSoundDeviceNameServer = new System.Windows.Forms.ComboBox();
            this.GroupBoxSound = new System.Windows.Forms.GroupBox();
            this.btnClienteCamera = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ProgressBarPlayingClient = new System.Windows.Forms.ProgressBar();
            this.btnClienteMic = new System.Windows.Forms.Button();
            this.ComboboxInputSoundDeviceNameClient = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenFileDialogMain = new System.Windows.Forms.OpenFileDialog();
            this.GroupBoxServerStart = new System.Windows.Forms.GroupBox();
            this.btnServidorAudio = new System.Windows.Forms.Button();
            this.btnServidorMic = new System.Windows.Forms.Button();
            this.ComboboxInputSoundDeviceNameServer = new System.Windows.Forms.ComboBox();
            this.lblMicrofoneSistema = new System.Windows.Forms.Label();
            this.FlowLayoutPanelServerClients = new System.Windows.Forms.FlowLayoutPanel();
            this.FlowLayoutPanelServerProgressBars = new System.Windows.Forms.FlowLayoutPanel();
            this.GroupBoxServerClients = new System.Windows.Forms.GroupBox();
            this.FlowLayoutPanelServerSpeak = new System.Windows.Forms.FlowLayoutPanel();
            this.FlowLayoutPanelServerListen = new System.Windows.Forms.FlowLayoutPanel();
            this.TabControlMain = new System.Windows.Forms.TabControl();
            this.TabPageServer = new System.Windows.Forms.TabPage();
            this.TabPageClient = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnEnviarMsg = new System.Windows.Forms.Button();
            this.txtBoxMsg = new System.Windows.Forms.TextBox();
            this.GroupBoxClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownJitterBufferClient)).BeginInit();
            this.GroupBoxServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownJitterBufferServer)).BeginInit();
            this.GroupBoxSound.SuspendLayout();
            this.GroupBoxServerStart.SuspendLayout();
            this.GroupBoxServerClients.SuspendLayout();
            this.TabControlMain.SuspendLayout();
            this.TabPageServer.SuspendLayout();
            this.TabPageClient.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxClient
            // 
            this.GroupBoxClient.Controls.Add(this.txtBoxUsuario);
            this.GroupBoxClient.Controls.Add(this.lblNome);
            this.GroupBoxClient.Controls.Add(this.txtboxMensagens);
            this.GroupBoxClient.Controls.Add(this.LabelIPAddressServer);
            this.GroupBoxClient.Controls.Add(this.TextBoxClientAddress);
            this.GroupBoxClient.Controls.Add(this.NumericUpDownJitterBufferClient);
            this.GroupBoxClient.Controls.Add(this.btnConectarCliente);
            this.GroupBoxClient.Controls.Add(this.LabelJitterBufferClient);
            this.GroupBoxClient.Controls.Add(this.txtBoxPortaCliente);
            this.GroupBoxClient.Controls.Add(this.LabelPortServer);
            this.GroupBoxClient.Controls.Add(this.LabelClient);
            this.GroupBoxClient.Location = new System.Drawing.Point(8, 6);
            this.GroupBoxClient.Name = "GroupBoxClient";
            this.GroupBoxClient.Size = new System.Drawing.Size(432, 417);
            this.GroupBoxClient.TabIndex = 22;
            this.GroupBoxClient.TabStop = false;
            this.GroupBoxClient.Text = "Client";
            // 
            // txtBoxUsuario
            // 
            this.txtBoxUsuario.BackColor = System.Drawing.Color.White;
            this.txtBoxUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxUsuario.ForeColor = System.Drawing.Color.DimGray;
            this.txtBoxUsuario.Location = new System.Drawing.Point(108, 13);
            this.txtBoxUsuario.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxUsuario.Name = "txtBoxUsuario";
            this.txtBoxUsuario.Size = new System.Drawing.Size(192, 20);
            this.txtBoxUsuario.TabIndex = 39;
            this.txtBoxUsuario.Text = "Anonimo";
            this.txtBoxUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(9, 16);
            this.lblNome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(95, 13);
            this.lblNome.TabIndex = 38;
            this.lblNome.Text = "Nome do Usuário :";
            // 
            // txtboxMensagens
            // 
            this.txtboxMensagens.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtboxMensagens.Enabled = false;
            this.txtboxMensagens.Location = new System.Drawing.Point(8, 195);
            this.txtboxMensagens.Multiline = true;
            this.txtboxMensagens.Name = "txtboxMensagens";
            this.txtboxMensagens.Size = new System.Drawing.Size(414, 215);
            this.txtboxMensagens.TabIndex = 37;
            // 
            // LabelIPAddressServer
            // 
            this.LabelIPAddressServer.AutoSize = true;
            this.LabelIPAddressServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPAddressServer.Location = new System.Drawing.Point(7, 49);
            this.LabelIPAddressServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelIPAddressServer.Name = "LabelIPAddressServer";
            this.LabelIPAddressServer.Size = new System.Drawing.Size(58, 13);
            this.LabelIPAddressServer.TabIndex = 19;
            this.LabelIPAddressServer.Text = "IP Address";
            // 
            // TextBoxClientAddress
            // 
            this.TextBoxClientAddress.BackColor = System.Drawing.Color.White;
            this.TextBoxClientAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxClientAddress.ForeColor = System.Drawing.Color.DimGray;
            this.TextBoxClientAddress.Location = new System.Drawing.Point(66, 46);
            this.TextBoxClientAddress.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxClientAddress.Name = "TextBoxClientAddress";
            this.TextBoxClientAddress.Size = new System.Drawing.Size(132, 20);
            this.TextBoxClientAddress.TabIndex = 13;
            this.TextBoxClientAddress.Text = "192.168.0.101";
            this.TextBoxClientAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NumericUpDownJitterBufferClient
            // 
            this.NumericUpDownJitterBufferClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumericUpDownJitterBufferClient.Location = new System.Drawing.Point(350, 45);
            this.NumericUpDownJitterBufferClient.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumericUpDownJitterBufferClient.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NumericUpDownJitterBufferClient.Name = "NumericUpDownJitterBufferClient";
            this.NumericUpDownJitterBufferClient.Size = new System.Drawing.Size(75, 21);
            this.NumericUpDownJitterBufferClient.TabIndex = 35;
            this.NumericUpDownJitterBufferClient.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NumericUpDownJitterBufferClient.ValueChanged += new System.EventHandler(this.NumericUpDownJitterBufferClient_ValueChanged);
            // 
            // btnConectarCliente
            // 
            this.btnConectarCliente.BackColor = System.Drawing.Color.Gainsboro;
            this.btnConectarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectarCliente.ForeColor = System.Drawing.Color.Black;
            this.btnConectarCliente.Location = new System.Drawing.Point(8, 75);
            this.btnConectarCliente.Margin = new System.Windows.Forms.Padding(2);
            this.btnConectarCliente.Name = "btnConectarCliente";
            this.btnConectarCliente.Size = new System.Drawing.Size(415, 45);
            this.btnConectarCliente.TabIndex = 0;
            this.btnConectarCliente.Text = "Conectar";
            this.btnConectarCliente.UseVisualStyleBackColor = false;
            this.btnConectarCliente.Click += new System.EventHandler(this.ButtonClient_Click);
            // 
            // LabelJitterBufferClient
            // 
            this.LabelJitterBufferClient.AutoSize = true;
            this.LabelJitterBufferClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelJitterBufferClient.Location = new System.Drawing.Point(300, 47);
            this.LabelJitterBufferClient.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelJitterBufferClient.Name = "LabelJitterBufferClient";
            this.LabelJitterBufferClient.Size = new System.Drawing.Size(45, 17);
            this.LabelJitterBufferClient.TabIndex = 36;
            this.LabelJitterBufferClient.Text = "Jitter";
            // 
            // txtBoxPortaCliente
            // 
            this.txtBoxPortaCliente.BackColor = System.Drawing.Color.White;
            this.txtBoxPortaCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxPortaCliente.ForeColor = System.Drawing.Color.DimGray;
            this.txtBoxPortaCliente.Location = new System.Drawing.Point(232, 46);
            this.txtBoxPortaCliente.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxPortaCliente.Name = "txtBoxPortaCliente";
            this.txtBoxPortaCliente.Size = new System.Drawing.Size(59, 20);
            this.txtBoxPortaCliente.TabIndex = 14;
            this.txtBoxPortaCliente.Text = "7000";
            this.txtBoxPortaCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelPortServer
            // 
            this.LabelPortServer.AutoSize = true;
            this.LabelPortServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPortServer.Location = new System.Drawing.Point(202, 49);
            this.LabelPortServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelPortServer.Name = "LabelPortServer";
            this.LabelPortServer.Size = new System.Drawing.Size(26, 13);
            this.LabelPortServer.TabIndex = 15;
            this.LabelPortServer.Text = "Port";
            // 
            // LabelClient
            // 
            this.LabelClient.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.LabelClient.Location = new System.Drawing.Point(8, 134);
            this.LabelClient.Name = "LabelClient";
            this.LabelClient.Size = new System.Drawing.Size(414, 58);
            this.LabelClient.TabIndex = 26;
            // 
            // btncClienteAudio
            // 
            this.btncClienteAudio.BackColor = System.Drawing.Color.Gainsboro;
            this.btncClienteAudio.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncClienteAudio.BackgroundImage")));
            this.btncClienteAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncClienteAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncClienteAudio.Location = new System.Drawing.Point(125, 56);
            this.btncClienteAudio.Margin = new System.Windows.Forms.Padding(2);
            this.btncClienteAudio.Name = "btncClienteAudio";
            this.btncClienteAudio.Size = new System.Drawing.Size(32, 32);
            this.btncClienteAudio.TabIndex = 45;
            this.btncClienteAudio.UseVisualStyleBackColor = false;
            this.btncClienteAudio.Click += new System.EventHandler(this.ButtonClientListen_Click);
            // 
            // LabelOutputSoundDeviceNameClient
            // 
            this.LabelOutputSoundDeviceNameClient.AutoSize = true;
            this.LabelOutputSoundDeviceNameClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelOutputSoundDeviceNameClient.Location = new System.Drawing.Point(8, 64);
            this.LabelOutputSoundDeviceNameClient.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelOutputSoundDeviceNameClient.Name = "LabelOutputSoundDeviceNameClient";
            this.LabelOutputSoundDeviceNameClient.Size = new System.Drawing.Size(111, 17);
            this.LabelOutputSoundDeviceNameClient.TabIndex = 11;
            this.LabelOutputSoundDeviceNameClient.Text = "Saida de áudio :";
            // 
            // ComboboxOutputSoundDeviceNameClient
            // 
            this.ComboboxOutputSoundDeviceNameClient.BackColor = System.Drawing.Color.White;
            this.ComboboxOutputSoundDeviceNameClient.DropDownHeight = 800;
            this.ComboboxOutputSoundDeviceNameClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboboxOutputSoundDeviceNameClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboboxOutputSoundDeviceNameClient.ForeColor = System.Drawing.Color.Black;
            this.ComboboxOutputSoundDeviceNameClient.FormattingEnabled = true;
            this.ComboboxOutputSoundDeviceNameClient.IntegralHeight = false;
            this.ComboboxOutputSoundDeviceNameClient.Location = new System.Drawing.Point(162, 61);
            this.ComboboxOutputSoundDeviceNameClient.Margin = new System.Windows.Forms.Padding(2);
            this.ComboboxOutputSoundDeviceNameClient.Name = "ComboboxOutputSoundDeviceNameClient";
            this.ComboboxOutputSoundDeviceNameClient.Size = new System.Drawing.Size(259, 24);
            this.ComboboxOutputSoundDeviceNameClient.TabIndex = 12;
            // 
            // LabelIPAddressClient
            // 
            this.LabelIPAddressClient.AutoSize = true;
            this.LabelIPAddressClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPAddressClient.Location = new System.Drawing.Point(5, 25);
            this.LabelIPAddressClient.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelIPAddressClient.Name = "LabelIPAddressClient";
            this.LabelIPAddressClient.Size = new System.Drawing.Size(58, 13);
            this.LabelIPAddressClient.TabIndex = 19;
            this.LabelIPAddressClient.Text = "IP Address";
            // 
            // txtIpAdress
            // 
            this.txtIpAdress.BackColor = System.Drawing.Color.White;
            this.txtIpAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpAdress.ForeColor = System.Drawing.Color.DimGray;
            this.txtIpAdress.Location = new System.Drawing.Point(68, 22);
            this.txtIpAdress.Margin = new System.Windows.Forms.Padding(2);
            this.txtIpAdress.Name = "txtIpAdress";
            this.txtIpAdress.Size = new System.Drawing.Size(167, 20);
            this.txtIpAdress.TabIndex = 13;
            this.txtIpAdress.Text = "192.168.0.101";
            this.txtIpAdress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextBoxServerPort
            // 
            this.TextBoxServerPort.BackColor = System.Drawing.Color.White;
            this.TextBoxServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxServerPort.ForeColor = System.Drawing.Color.DimGray;
            this.TextBoxServerPort.Location = new System.Drawing.Point(279, 22);
            this.TextBoxServerPort.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxServerPort.Name = "TextBoxServerPort";
            this.TextBoxServerPort.Size = new System.Drawing.Size(59, 20);
            this.TextBoxServerPort.TabIndex = 14;
            this.TextBoxServerPort.Text = "7000";
            this.TextBoxServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelServerPort
            // 
            this.LabelServerPort.AutoSize = true;
            this.LabelServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelServerPort.Location = new System.Drawing.Point(249, 25);
            this.LabelServerPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelServerPort.Name = "LabelServerPort";
            this.LabelServerPort.Size = new System.Drawing.Size(26, 13);
            this.LabelServerPort.TabIndex = 15;
            this.LabelServerPort.Text = "Port";
            // 
            // GroupBoxServer
            // 
            this.GroupBoxServer.Controls.Add(this.LabelIPAddressClient);
            this.GroupBoxServer.Controls.Add(this.txtIpAdress);
            this.GroupBoxServer.Controls.Add(this.btnAbrirConexao);
            this.GroupBoxServer.Controls.Add(this.LabelServerPort);
            this.GroupBoxServer.Controls.Add(this.ComboboxSamplesPerSecondServer);
            this.GroupBoxServer.Controls.Add(this.LabelJitterBufferServer);
            this.GroupBoxServer.Controls.Add(this.LabelSamplesPerSecondServer);
            this.GroupBoxServer.Controls.Add(this.TextBoxServerPort);
            this.GroupBoxServer.Controls.Add(this.NumericUpDownJitterBufferServer);
            this.GroupBoxServer.Controls.Add(this.lblLogServer);
            this.GroupBoxServer.Location = new System.Drawing.Point(6, 59);
            this.GroupBoxServer.Name = "GroupBoxServer";
            this.GroupBoxServer.Size = new System.Drawing.Size(432, 157);
            this.GroupBoxServer.TabIndex = 27;
            this.GroupBoxServer.TabStop = false;
            this.GroupBoxServer.Text = "Server";
            // 
            // btnAbrirConexao
            // 
            this.btnAbrirConexao.BackColor = System.Drawing.Color.Green;
            this.btnAbrirConexao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirConexao.ForeColor = System.Drawing.Color.White;
            this.btnAbrirConexao.Location = new System.Drawing.Point(346, 18);
            this.btnAbrirConexao.Margin = new System.Windows.Forms.Padding(2);
            this.btnAbrirConexao.Name = "btnAbrirConexao";
            this.btnAbrirConexao.Size = new System.Drawing.Size(75, 27);
            this.btnAbrirConexao.TabIndex = 0;
            this.btnAbrirConexao.Text = "Abrir";
            this.btnAbrirConexao.UseVisualStyleBackColor = false;
            this.btnAbrirConexao.Click += new System.EventHandler(this.ButtonServer_Click);
            // 
            // ComboboxSamplesPerSecondServer
            // 
            this.ComboboxSamplesPerSecondServer.BackColor = System.Drawing.Color.White;
            this.ComboboxSamplesPerSecondServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboboxSamplesPerSecondServer.FormattingEnabled = true;
            this.ComboboxSamplesPerSecondServer.Items.AddRange(new object[] {
            "5000",
            "8000",
            "22050",
            "44100"});
            this.ComboboxSamplesPerSecondServer.Location = new System.Drawing.Point(114, 56);
            this.ComboboxSamplesPerSecondServer.Margin = new System.Windows.Forms.Padding(2);
            this.ComboboxSamplesPerSecondServer.Name = "ComboboxSamplesPerSecondServer";
            this.ComboboxSamplesPerSecondServer.Size = new System.Drawing.Size(71, 21);
            this.ComboboxSamplesPerSecondServer.TabIndex = 40;
            // 
            // LabelJitterBufferServer
            // 
            this.LabelJitterBufferServer.AutoSize = true;
            this.LabelJitterBufferServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelJitterBufferServer.Location = new System.Drawing.Point(293, 56);
            this.LabelJitterBufferServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelJitterBufferServer.Name = "LabelJitterBufferServer";
            this.LabelJitterBufferServer.Size = new System.Drawing.Size(45, 17);
            this.LabelJitterBufferServer.TabIndex = 34;
            this.LabelJitterBufferServer.Text = "Jitter";
            // 
            // LabelSamplesPerSecondServer
            // 
            this.LabelSamplesPerSecondServer.AutoSize = true;
            this.LabelSamplesPerSecondServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSamplesPerSecondServer.Location = new System.Drawing.Point(5, 59);
            this.LabelSamplesPerSecondServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelSamplesPerSecondServer.Name = "LabelSamplesPerSecondServer";
            this.LabelSamplesPerSecondServer.Size = new System.Drawing.Size(105, 13);
            this.LabelSamplesPerSecondServer.TabIndex = 37;
            this.LabelSamplesPerSecondServer.Text = "Samples per Second";
            // 
            // NumericUpDownJitterBufferServer
            // 
            this.NumericUpDownJitterBufferServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumericUpDownJitterBufferServer.Location = new System.Drawing.Point(346, 55);
            this.NumericUpDownJitterBufferServer.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumericUpDownJitterBufferServer.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NumericUpDownJitterBufferServer.Name = "NumericUpDownJitterBufferServer";
            this.NumericUpDownJitterBufferServer.Size = new System.Drawing.Size(75, 21);
            this.NumericUpDownJitterBufferServer.TabIndex = 33;
            this.NumericUpDownJitterBufferServer.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NumericUpDownJitterBufferServer.ValueChanged += new System.EventHandler(this.NumericUpDownJitterBufferServer_ValueChanged);
            // 
            // lblLogServer
            // 
            this.lblLogServer.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.lblLogServer.Location = new System.Drawing.Point(9, 85);
            this.lblLogServer.Name = "lblLogServer";
            this.lblLogServer.Size = new System.Drawing.Size(412, 59);
            this.lblLogServer.TabIndex = 28;
            // 
            // lblEntradasSistema
            // 
            this.lblEntradasSistema.AutoSize = true;
            this.lblEntradasSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntradasSistema.Location = new System.Drawing.Point(-1, 61);
            this.lblEntradasSistema.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEntradasSistema.Name = "lblEntradasSistema";
            this.lblEntradasSistema.Size = new System.Drawing.Size(44, 17);
            this.lblEntradasSistema.TabIndex = 11;
            this.lblEntradasSistema.Text = "Audio";
            // 
            // ComboboxOutputSoundDeviceNameServer
            // 
            this.ComboboxOutputSoundDeviceNameServer.BackColor = System.Drawing.Color.White;
            this.ComboboxOutputSoundDeviceNameServer.DropDownHeight = 800;
            this.ComboboxOutputSoundDeviceNameServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboboxOutputSoundDeviceNameServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboboxOutputSoundDeviceNameServer.ForeColor = System.Drawing.Color.Black;
            this.ComboboxOutputSoundDeviceNameServer.FormattingEnabled = true;
            this.ComboboxOutputSoundDeviceNameServer.IntegralHeight = false;
            this.ComboboxOutputSoundDeviceNameServer.Location = new System.Drawing.Point(117, 58);
            this.ComboboxOutputSoundDeviceNameServer.Margin = new System.Windows.Forms.Padding(2);
            this.ComboboxOutputSoundDeviceNameServer.Name = "ComboboxOutputSoundDeviceNameServer";
            this.ComboboxOutputSoundDeviceNameServer.Size = new System.Drawing.Size(304, 24);
            this.ComboboxOutputSoundDeviceNameServer.TabIndex = 12;
            // 
            // GroupBoxSound
            // 
            this.GroupBoxSound.Controls.Add(this.btnClienteCamera);
            this.GroupBoxSound.Controls.Add(this.label2);
            this.GroupBoxSound.Controls.Add(this.ProgressBarPlayingClient);
            this.GroupBoxSound.Controls.Add(this.btnClienteMic);
            this.GroupBoxSound.Controls.Add(this.ComboboxInputSoundDeviceNameClient);
            this.GroupBoxSound.Controls.Add(this.btncClienteAudio);
            this.GroupBoxSound.Controls.Add(this.label1);
            this.GroupBoxSound.Controls.Add(this.ComboboxOutputSoundDeviceNameClient);
            this.GroupBoxSound.Controls.Add(this.LabelOutputSoundDeviceNameClient);
            this.GroupBoxSound.Location = new System.Drawing.Point(6, 475);
            this.GroupBoxSound.Name = "GroupBoxSound";
            this.GroupBoxSound.Size = new System.Drawing.Size(434, 139);
            this.GroupBoxSound.TabIndex = 30;
            this.GroupBoxSound.TabStop = false;
            // 
            // btnClienteCamera
            // 
            this.btnClienteCamera.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClienteCamera.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClienteCamera.BackgroundImage")));
            this.btnClienteCamera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClienteCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClienteCamera.Location = new System.Drawing.Point(125, 93);
            this.btnClienteCamera.Margin = new System.Windows.Forms.Padding(2);
            this.btnClienteCamera.Name = "btnClienteCamera";
            this.btnClienteCamera.Size = new System.Drawing.Size(32, 32);
            this.btnClienteCamera.TabIndex = 47;
            this.btnClienteCamera.UseVisualStyleBackColor = false;
            this.btnClienteCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 46;
            this.label2.Text = "Webcam :";
            // 
            // ProgressBarPlayingClient
            // 
            this.ProgressBarPlayingClient.Location = new System.Drawing.Point(162, 61);
            this.ProgressBarPlayingClient.Name = "ProgressBarPlayingClient";
            this.ProgressBarPlayingClient.Size = new System.Drawing.Size(259, 24);
            this.ProgressBarPlayingClient.TabIndex = 37;
            this.ProgressBarPlayingClient.Visible = false;
            // 
            // btnClienteMic
            // 
            this.btnClienteMic.BackColor = System.Drawing.Color.Gainsboro;
            this.btnClienteMic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClienteMic.BackgroundImage")));
            this.btnClienteMic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClienteMic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClienteMic.Location = new System.Drawing.Point(126, 20);
            this.btnClienteMic.Margin = new System.Windows.Forms.Padding(2);
            this.btnClienteMic.Name = "btnClienteMic";
            this.btnClienteMic.Size = new System.Drawing.Size(32, 32);
            this.btnClienteMic.TabIndex = 44;
            this.btnClienteMic.UseVisualStyleBackColor = false;
            this.btnClienteMic.Click += new System.EventHandler(this.ButtonClientSpeak_Click);
            // 
            // ComboboxInputSoundDeviceNameClient
            // 
            this.ComboboxInputSoundDeviceNameClient.BackColor = System.Drawing.Color.White;
            this.ComboboxInputSoundDeviceNameClient.DropDownHeight = 800;
            this.ComboboxInputSoundDeviceNameClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboboxInputSoundDeviceNameClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboboxInputSoundDeviceNameClient.ForeColor = System.Drawing.Color.Black;
            this.ComboboxInputSoundDeviceNameClient.FormattingEnabled = true;
            this.ComboboxInputSoundDeviceNameClient.IntegralHeight = false;
            this.ComboboxInputSoundDeviceNameClient.Location = new System.Drawing.Point(162, 22);
            this.ComboboxInputSoundDeviceNameClient.Margin = new System.Windows.Forms.Padding(2);
            this.ComboboxInputSoundDeviceNameClient.Name = "ComboboxInputSoundDeviceNameClient";
            this.ComboboxInputSoundDeviceNameClient.Size = new System.Drawing.Size(259, 24);
            this.ComboboxInputSoundDeviceNameClient.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Entrada de áudio :";
            // 
            // OpenFileDialogMain
            // 
            this.OpenFileDialogMain.FileName = "MyRecord.wav";
            this.OpenFileDialogMain.Filter = "Wave Dateien (*.wav)|*.wav|Alle Dateien (*.*)|*.*";
            // 
            // GroupBoxServerStart
            // 
            this.GroupBoxServerStart.Controls.Add(this.btnServidorAudio);
            this.GroupBoxServerStart.Controls.Add(this.btnServidorMic);
            this.GroupBoxServerStart.Controls.Add(this.ComboboxInputSoundDeviceNameServer);
            this.GroupBoxServerStart.Controls.Add(this.lblMicrofoneSistema);
            this.GroupBoxServerStart.Controls.Add(this.lblEntradasSistema);
            this.GroupBoxServerStart.Controls.Add(this.ComboboxOutputSoundDeviceNameServer);
            this.GroupBoxServerStart.Location = new System.Drawing.Point(6, 222);
            this.GroupBoxServerStart.Name = "GroupBoxServerStart";
            this.GroupBoxServerStart.Size = new System.Drawing.Size(432, 92);
            this.GroupBoxServerStart.TabIndex = 35;
            this.GroupBoxServerStart.TabStop = false;
            // 
            // btnServidorAudio
            // 
            this.btnServidorAudio.BackColor = System.Drawing.Color.Gainsboro;
            this.btnServidorAudio.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnServidorAudio.BackgroundImage")));
            this.btnServidorAudio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnServidorAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServidorAudio.Location = new System.Drawing.Point(68, 52);
            this.btnServidorAudio.Margin = new System.Windows.Forms.Padding(2);
            this.btnServidorAudio.Name = "btnServidorAudio";
            this.btnServidorAudio.Size = new System.Drawing.Size(32, 32);
            this.btnServidorAudio.TabIndex = 44;
            this.btnServidorAudio.UseVisualStyleBackColor = false;
            this.btnServidorAudio.Click += new System.EventHandler(this.ButtonServerListen_Click);
            // 
            // btnServidorMic
            // 
            this.btnServidorMic.BackColor = System.Drawing.Color.Gainsboro;
            this.btnServidorMic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnServidorMic.BackgroundImage")));
            this.btnServidorMic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnServidorMic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServidorMic.Location = new System.Drawing.Point(68, 16);
            this.btnServidorMic.Margin = new System.Windows.Forms.Padding(2);
            this.btnServidorMic.Name = "btnServidorMic";
            this.btnServidorMic.Size = new System.Drawing.Size(32, 32);
            this.btnServidorMic.TabIndex = 43;
            this.btnServidorMic.UseVisualStyleBackColor = false;
            this.btnServidorMic.Click += new System.EventHandler(this.ButtonServerSpeak_Click);
            // 
            // ComboboxInputSoundDeviceNameServer
            // 
            this.ComboboxInputSoundDeviceNameServer.BackColor = System.Drawing.Color.White;
            this.ComboboxInputSoundDeviceNameServer.DropDownHeight = 800;
            this.ComboboxInputSoundDeviceNameServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboboxInputSoundDeviceNameServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboboxInputSoundDeviceNameServer.ForeColor = System.Drawing.Color.Black;
            this.ComboboxInputSoundDeviceNameServer.FormattingEnabled = true;
            this.ComboboxInputSoundDeviceNameServer.IntegralHeight = false;
            this.ComboboxInputSoundDeviceNameServer.Location = new System.Drawing.Point(117, 19);
            this.ComboboxInputSoundDeviceNameServer.Margin = new System.Windows.Forms.Padding(2);
            this.ComboboxInputSoundDeviceNameServer.Name = "ComboboxInputSoundDeviceNameServer";
            this.ComboboxInputSoundDeviceNameServer.Size = new System.Drawing.Size(304, 24);
            this.ComboboxInputSoundDeviceNameServer.TabIndex = 42;
            // 
            // lblMicrofoneSistema
            // 
            this.lblMicrofoneSistema.AutoSize = true;
            this.lblMicrofoneSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMicrofoneSistema.Location = new System.Drawing.Point(-1, 22);
            this.lblMicrofoneSistema.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMicrofoneSistema.Name = "lblMicrofoneSistema";
            this.lblMicrofoneSistema.Size = new System.Drawing.Size(70, 17);
            this.lblMicrofoneSistema.TabIndex = 41;
            this.lblMicrofoneSistema.Text = "Microfone";
            // 
            // FlowLayoutPanelServerClients
            // 
            this.FlowLayoutPanelServerClients.BackColor = System.Drawing.Color.DimGray;
            this.FlowLayoutPanelServerClients.Location = new System.Drawing.Point(8, 21);
            this.FlowLayoutPanelServerClients.Name = "FlowLayoutPanelServerClients";
            this.FlowLayoutPanelServerClients.Size = new System.Drawing.Size(151, 206);
            this.FlowLayoutPanelServerClients.TabIndex = 36;
            // 
            // FlowLayoutPanelServerProgressBars
            // 
            this.FlowLayoutPanelServerProgressBars.BackColor = System.Drawing.Color.DimGray;
            this.FlowLayoutPanelServerProgressBars.Location = new System.Drawing.Point(158, 21);
            this.FlowLayoutPanelServerProgressBars.Name = "FlowLayoutPanelServerProgressBars";
            this.FlowLayoutPanelServerProgressBars.Size = new System.Drawing.Size(210, 206);
            this.FlowLayoutPanelServerProgressBars.TabIndex = 37;
            // 
            // GroupBoxServerClients
            // 
            this.GroupBoxServerClients.Controls.Add(this.FlowLayoutPanelServerSpeak);
            this.GroupBoxServerClients.Controls.Add(this.FlowLayoutPanelServerListen);
            this.GroupBoxServerClients.Controls.Add(this.FlowLayoutPanelServerClients);
            this.GroupBoxServerClients.Controls.Add(this.FlowLayoutPanelServerProgressBars);
            this.GroupBoxServerClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBoxServerClients.Location = new System.Drawing.Point(6, 320);
            this.GroupBoxServerClients.Name = "GroupBoxServerClients";
            this.GroupBoxServerClients.Size = new System.Drawing.Size(432, 233);
            this.GroupBoxServerClients.TabIndex = 38;
            this.GroupBoxServerClients.TabStop = false;
            this.GroupBoxServerClients.Text = "Clients";
            // 
            // FlowLayoutPanelServerSpeak
            // 
            this.FlowLayoutPanelServerSpeak.BackColor = System.Drawing.Color.DimGray;
            this.FlowLayoutPanelServerSpeak.Location = new System.Drawing.Point(367, 21);
            this.FlowLayoutPanelServerSpeak.Name = "FlowLayoutPanelServerSpeak";
            this.FlowLayoutPanelServerSpeak.Size = new System.Drawing.Size(30, 206);
            this.FlowLayoutPanelServerSpeak.TabIndex = 39;
            // 
            // FlowLayoutPanelServerListen
            // 
            this.FlowLayoutPanelServerListen.BackColor = System.Drawing.Color.DimGray;
            this.FlowLayoutPanelServerListen.Location = new System.Drawing.Point(396, 21);
            this.FlowLayoutPanelServerListen.Name = "FlowLayoutPanelServerListen";
            this.FlowLayoutPanelServerListen.Size = new System.Drawing.Size(30, 206);
            this.FlowLayoutPanelServerListen.TabIndex = 38;
            // 
            // TabControlMain
            // 
            this.TabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControlMain.Controls.Add(this.TabPageServer);
            this.TabControlMain.Controls.Add(this.TabPageClient);
            this.TabControlMain.Location = new System.Drawing.Point(0, 2);
            this.TabControlMain.Name = "TabControlMain";
            this.TabControlMain.SelectedIndex = 0;
            this.TabControlMain.Size = new System.Drawing.Size(457, 648);
            this.TabControlMain.TabIndex = 40;
            // 
            // TabPageServer
            // 
            this.TabPageServer.BackColor = System.Drawing.Color.Transparent;
            this.TabPageServer.Controls.Add(this.GroupBoxServerClients);
            this.TabPageServer.Controls.Add(this.GroupBoxServer);
            this.TabPageServer.Controls.Add(this.GroupBoxServerStart);
            this.TabPageServer.Location = new System.Drawing.Point(4, 22);
            this.TabPageServer.Name = "TabPageServer";
            this.TabPageServer.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageServer.Size = new System.Drawing.Size(449, 622);
            this.TabPageServer.TabIndex = 1;
            this.TabPageServer.Text = "Server";
            // 
            // TabPageClient
            // 
            this.TabPageClient.Controls.Add(this.groupBox1);
            this.TabPageClient.Controls.Add(this.GroupBoxSound);
            this.TabPageClient.Controls.Add(this.GroupBoxClient);
            this.TabPageClient.Location = new System.Drawing.Point(4, 22);
            this.TabPageClient.Name = "TabPageClient";
            this.TabPageClient.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageClient.Size = new System.Drawing.Size(449, 622);
            this.TabPageClient.TabIndex = 0;
            this.TabPageClient.Text = "Client";
            this.TabPageClient.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnEnviarMsg);
            this.groupBox1.Controls.Add(this.txtBoxMsg);
            this.groupBox1.Location = new System.Drawing.Point(6, 420);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 70);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // btnEnviarMsg
            // 
            this.btnEnviarMsg.BackColor = System.Drawing.Color.Gainsboro;
            this.btnEnviarMsg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEnviarMsg.BackgroundImage")));
            this.btnEnviarMsg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnEnviarMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarMsg.Location = new System.Drawing.Point(352, 22);
            this.btnEnviarMsg.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnviarMsg.Name = "btnEnviarMsg";
            this.btnEnviarMsg.Size = new System.Drawing.Size(69, 32);
            this.btnEnviarMsg.TabIndex = 48;
            this.btnEnviarMsg.UseVisualStyleBackColor = false;
            this.btnEnviarMsg.Click += new System.EventHandler(this.btnEnviarMsg_Click);
            // 
            // txtBoxMsg
            // 
            this.txtBoxMsg.Location = new System.Drawing.Point(14, 29);
            this.txtBoxMsg.Name = "txtBoxMsg";
            this.txtBoxMsg.Size = new System.Drawing.Size(333, 20);
            this.txtBoxMsg.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AcceptButton = this.btnEnviarMsg;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 650);
            this.Controls.Add(this.TabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.GroupBoxClient.ResumeLayout(false);
            this.GroupBoxClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownJitterBufferClient)).EndInit();
            this.GroupBoxServer.ResumeLayout(false);
            this.GroupBoxServer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownJitterBufferServer)).EndInit();
            this.GroupBoxSound.ResumeLayout(false);
            this.GroupBoxSound.PerformLayout();
            this.GroupBoxServerStart.ResumeLayout(false);
            this.GroupBoxServerStart.PerformLayout();
            this.GroupBoxServerClients.ResumeLayout(false);
            this.TabControlMain.ResumeLayout(false);
            this.TabPageServer.ResumeLayout(false);
            this.TabPageClient.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxClient;
        private System.Windows.Forms.Label LabelIPAddressClient;
        private System.Windows.Forms.TextBox txtIpAdress;
        private System.Windows.Forms.Label LabelOutputSoundDeviceNameClient;
        private System.Windows.Forms.ComboBox ComboboxOutputSoundDeviceNameClient;
        private System.Windows.Forms.TextBox TextBoxServerPort;
        private System.Windows.Forms.Label LabelServerPort;
        private System.Windows.Forms.Button btnConectarCliente;
        private System.Windows.Forms.Label LabelClient;
        private System.Windows.Forms.GroupBox GroupBoxServer;
        private System.Windows.Forms.Label LabelIPAddressServer;
        private System.Windows.Forms.TextBox TextBoxClientAddress;
        private System.Windows.Forms.Label lblEntradasSistema;
        private System.Windows.Forms.ComboBox ComboboxOutputSoundDeviceNameServer;
        private System.Windows.Forms.TextBox txtBoxPortaCliente;
        private System.Windows.Forms.Label LabelPortServer;
        private System.Windows.Forms.Button btnAbrirConexao;
        private System.Windows.Forms.Label lblLogServer;
				private System.Windows.Forms.GroupBox GroupBoxSound;
        private System.Windows.Forms.Label LabelJitterBufferServer;
        private System.Windows.Forms.NumericUpDown NumericUpDownJitterBufferServer;
        private System.Windows.Forms.OpenFileDialog OpenFileDialogMain;
        private System.Windows.Forms.GroupBox GroupBoxServerStart;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelServerClients;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelServerProgressBars;
        private System.Windows.Forms.GroupBox GroupBoxServerClients;
        private System.Windows.Forms.TabControl TabControlMain;
        private System.Windows.Forms.TabPage TabPageClient;
        private System.Windows.Forms.TabPage TabPageServer;
        private System.Windows.Forms.Label LabelSamplesPerSecondServer;
        private System.Windows.Forms.ComboBox ComboboxSamplesPerSecondServer;
        private System.Windows.Forms.ComboBox ComboboxInputSoundDeviceNameClient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboboxInputSoundDeviceNameServer;
        private System.Windows.Forms.Label lblMicrofoneSistema;
        private System.Windows.Forms.Label LabelJitterBufferClient;
        private System.Windows.Forms.NumericUpDown NumericUpDownJitterBufferClient;
        private System.Windows.Forms.Button btnServidorMic;
        private System.Windows.Forms.Button btnServidorAudio;
        private System.Windows.Forms.Button btncClienteAudio;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelServerListen;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanelServerSpeak;
        private System.Windows.Forms.Button btnClienteMic;
        private System.Windows.Forms.ProgressBar ProgressBarPlayingClient;
        private System.Windows.Forms.Button btnClienteCamera;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnEnviarMsg;
        private System.Windows.Forms.TextBox txtBoxMsg;
        private System.Windows.Forms.TextBox txtboxMensagens;
        private System.Windows.Forms.TextBox txtBoxUsuario;
        private System.Windows.Forms.Label lblNome;
    }
}

