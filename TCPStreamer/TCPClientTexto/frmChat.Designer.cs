
namespace NF
{
    partial class frmChat
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBoxClient = new System.Windows.Forms.GroupBox();
            this.LabelIPAddressServer = new System.Windows.Forms.Label();
            this.TextBoxClientAddress = new System.Windows.Forms.TextBox();
            this.NumericUpDownJitterBufferClient = new System.Windows.Forms.NumericUpDown();
            this.btnConectar = new System.Windows.Forms.Button();
            this.LabelJitterBufferClient = new System.Windows.Forms.Label();
            this.TextBoxClientPort = new System.Windows.Forms.TextBox();
            this.LabelPortServer = new System.Windows.Forms.Label();
            this.LabelClient = new System.Windows.Forms.Label();
            this.GroupBoxSound = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnEntrarChatVoz = new System.Windows.Forms.Button();
            this.btnAbrirCamera = new System.Windows.Forms.Button();
            this.GroupBoxClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownJitterBufferClient)).BeginInit();
            this.GroupBoxSound.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxClient
            // 
            this.GroupBoxClient.Controls.Add(this.LabelIPAddressServer);
            this.GroupBoxClient.Controls.Add(this.TextBoxClientAddress);
            this.GroupBoxClient.Controls.Add(this.NumericUpDownJitterBufferClient);
            this.GroupBoxClient.Controls.Add(this.btnConectar);
            this.GroupBoxClient.Controls.Add(this.LabelJitterBufferClient);
            this.GroupBoxClient.Controls.Add(this.TextBoxClientPort);
            this.GroupBoxClient.Controls.Add(this.LabelPortServer);
            this.GroupBoxClient.Controls.Add(this.LabelClient);
            this.GroupBoxClient.Location = new System.Drawing.Point(12, 12);
            this.GroupBoxClient.Name = "GroupBoxClient";
            this.GroupBoxClient.Size = new System.Drawing.Size(432, 378);
            this.GroupBoxClient.TabIndex = 23;
            this.GroupBoxClient.TabStop = false;
            this.GroupBoxClient.Text = "Client";
            // 
            // LabelIPAddressServer
            // 
            this.LabelIPAddressServer.AutoSize = true;
            this.LabelIPAddressServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPAddressServer.Location = new System.Drawing.Point(2, 22);
            this.LabelIPAddressServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelIPAddressServer.Name = "LabelIPAddressServer";
            this.LabelIPAddressServer.Size = new System.Drawing.Size(58, 13);
            this.LabelIPAddressServer.TabIndex = 41;
            this.LabelIPAddressServer.Text = "IP Address";
            // 
            // TextBoxClientAddress
            // 
            this.TextBoxClientAddress.BackColor = System.Drawing.Color.White;
            this.TextBoxClientAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxClientAddress.ForeColor = System.Drawing.Color.DimGray;
            this.TextBoxClientAddress.Location = new System.Drawing.Point(60, 18);
            this.TextBoxClientAddress.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxClientAddress.Name = "TextBoxClientAddress";
            this.TextBoxClientAddress.Size = new System.Drawing.Size(132, 20);
            this.TextBoxClientAddress.TabIndex = 38;
            this.TextBoxClientAddress.Text = "192.168.0.101";
            this.TextBoxClientAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NumericUpDownJitterBufferClient
            // 
            this.NumericUpDownJitterBufferClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NumericUpDownJitterBufferClient.Location = new System.Drawing.Point(345, 17);
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
            this.NumericUpDownJitterBufferClient.TabIndex = 42;
            this.NumericUpDownJitterBufferClient.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // btnConectar
            // 
            this.btnConectar.BackColor = System.Drawing.Color.Gainsboro;
            this.btnConectar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConectar.ForeColor = System.Drawing.Color.Black;
            this.btnConectar.Location = new System.Drawing.Point(5, 45);
            this.btnConectar.Margin = new System.Windows.Forms.Padding(2);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(415, 45);
            this.btnConectar.TabIndex = 37;
            this.btnConectar.Text = "Connect";
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // LabelJitterBufferClient
            // 
            this.LabelJitterBufferClient.AutoSize = true;
            this.LabelJitterBufferClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelJitterBufferClient.Location = new System.Drawing.Point(292, 18);
            this.LabelJitterBufferClient.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelJitterBufferClient.Name = "LabelJitterBufferClient";
            this.LabelJitterBufferClient.Size = new System.Drawing.Size(45, 17);
            this.LabelJitterBufferClient.TabIndex = 43;
            this.LabelJitterBufferClient.Text = "Jitter";
            // 
            // TextBoxClientPort
            // 
            this.TextBoxClientPort.BackColor = System.Drawing.Color.White;
            this.TextBoxClientPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxClientPort.ForeColor = System.Drawing.Color.DimGray;
            this.TextBoxClientPort.Location = new System.Drawing.Point(226, 18);
            this.TextBoxClientPort.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxClientPort.Name = "TextBoxClientPort";
            this.TextBoxClientPort.Size = new System.Drawing.Size(59, 20);
            this.TextBoxClientPort.TabIndex = 39;
            this.TextBoxClientPort.Text = "7000";
            this.TextBoxClientPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LabelPortServer
            // 
            this.LabelPortServer.AutoSize = true;
            this.LabelPortServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPortServer.Location = new System.Drawing.Point(197, 21);
            this.LabelPortServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelPortServer.Name = "LabelPortServer";
            this.LabelPortServer.Size = new System.Drawing.Size(26, 13);
            this.LabelPortServer.TabIndex = 40;
            this.LabelPortServer.Text = "Port";
            // 
            // LabelClient
            // 
            this.LabelClient.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.LabelClient.Location = new System.Drawing.Point(9, 103);
            this.LabelClient.Name = "LabelClient";
            this.LabelClient.Size = new System.Drawing.Size(414, 260);
            this.LabelClient.TabIndex = 26;
            // 
            // GroupBoxSound
            // 
            this.GroupBoxSound.Controls.Add(this.button1);
            this.GroupBoxSound.Controls.Add(this.textBox1);
            this.GroupBoxSound.Controls.Add(this.btnEntrarChatVoz);
            this.GroupBoxSound.Controls.Add(this.btnAbrirCamera);
            this.GroupBoxSound.Location = new System.Drawing.Point(12, 414);
            this.GroupBoxSound.Name = "GroupBoxSound";
            this.GroupBoxSound.Size = new System.Drawing.Size(432, 108);
            this.GroupBoxSound.TabIndex = 31;
            this.GroupBoxSound.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gainsboro;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(360, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 32);
            this.button1.TabIndex = 47;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(330, 20);
            this.textBox1.TabIndex = 46;
            // 
            // btnEntrarChatVoz
            // 
            this.btnEntrarChatVoz.BackColor = System.Drawing.Color.Gainsboro;
            this.btnEntrarChatVoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrarChatVoz.Location = new System.Drawing.Point(12, 56);
            this.btnEntrarChatVoz.Margin = new System.Windows.Forms.Padding(2);
            this.btnEntrarChatVoz.Name = "btnEntrarChatVoz";
            this.btnEntrarChatVoz.Size = new System.Drawing.Size(32, 32);
            this.btnEntrarChatVoz.TabIndex = 44;
            this.btnEntrarChatVoz.UseVisualStyleBackColor = false;
            // 
            // btnAbrirCamera
            // 
            this.btnAbrirCamera.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAbrirCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCamera.Location = new System.Drawing.Point(57, 56);
            this.btnAbrirCamera.Margin = new System.Windows.Forms.Padding(2);
            this.btnAbrirCamera.Name = "btnAbrirCamera";
            this.btnAbrirCamera.Size = new System.Drawing.Size(32, 32);
            this.btnAbrirCamera.TabIndex = 45;
            this.btnAbrirCamera.UseVisualStyleBackColor = false;
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 534);
            this.Controls.Add(this.GroupBoxSound);
            this.Controls.Add(this.GroupBoxClient);
            this.Name = "frmChat";
            this.Text = "Chat de Voz";
            this.GroupBoxClient.ResumeLayout(false);
            this.GroupBoxClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownJitterBufferClient)).EndInit();
            this.GroupBoxSound.ResumeLayout(false);
            this.GroupBoxSound.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxClient;
        private System.Windows.Forms.Label LabelClient;
        private System.Windows.Forms.GroupBox GroupBoxSound;
        private System.Windows.Forms.Button btnEntrarChatVoz;
        private System.Windows.Forms.Button btnAbrirCamera;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label LabelIPAddressServer;
        private System.Windows.Forms.TextBox TextBoxClientAddress;
        private System.Windows.Forms.NumericUpDown NumericUpDownJitterBufferClient;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label LabelJitterBufferClient;
        private System.Windows.Forms.TextBox TextBoxClientPort;
        private System.Windows.Forms.Label LabelPortServer;
    }
}

