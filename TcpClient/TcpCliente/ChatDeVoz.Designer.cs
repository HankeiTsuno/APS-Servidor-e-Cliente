
namespace TcpCliente
{
    partial class ChatDeVoz
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
            this.ButtonClient = new System.Windows.Forms.Button();
            this.LabelClient = new System.Windows.Forms.Label();
            this.GroupBoxSound = new System.Windows.Forms.GroupBox();
            this.ProgressBarPlayingClient = new System.Windows.Forms.ProgressBar();
            this.ButtonClientSpeak = new System.Windows.Forms.Button();
            this.ComboboxInputSoundDeviceNameClient = new System.Windows.Forms.ComboBox();
            this.ButtonClientListen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ComboboxOutputSoundDeviceNameClient = new System.Windows.Forms.ComboBox();
            this.LabelOutputSoundDeviceNameClient = new System.Windows.Forms.Label();
            this.GroupBoxClient.SuspendLayout();
            this.GroupBoxSound.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxClient
            // 
            this.GroupBoxClient.Controls.Add(this.ButtonClient);
            this.GroupBoxClient.Controls.Add(this.LabelClient);
            this.GroupBoxClient.Location = new System.Drawing.Point(12, 25);
            this.GroupBoxClient.Name = "GroupBoxClient";
            this.GroupBoxClient.Size = new System.Drawing.Size(432, 378);
            this.GroupBoxClient.TabIndex = 23;
            this.GroupBoxClient.TabStop = false;
            this.GroupBoxClient.Text = "Client";
            // 
            // ButtonClient
            // 
            this.ButtonClient.BackColor = System.Drawing.Color.Gainsboro;
            this.ButtonClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonClient.ForeColor = System.Drawing.Color.Black;
            this.ButtonClient.Location = new System.Drawing.Point(8, 18);
            this.ButtonClient.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonClient.Name = "ButtonClient";
            this.ButtonClient.Size = new System.Drawing.Size(415, 45);
            this.ButtonClient.TabIndex = 0;
            this.ButtonClient.Text = "Connect";
            this.ButtonClient.UseVisualStyleBackColor = false;
            // 
            // LabelClient
            // 
            this.LabelClient.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.LabelClient.Location = new System.Drawing.Point(9, 82);
            this.LabelClient.Name = "LabelClient";
            this.LabelClient.Size = new System.Drawing.Size(414, 281);
            this.LabelClient.TabIndex = 26;
            // 
            // GroupBoxSound
            // 
            this.GroupBoxSound.Controls.Add(this.ProgressBarPlayingClient);
            this.GroupBoxSound.Controls.Add(this.ButtonClientSpeak);
            this.GroupBoxSound.Controls.Add(this.ComboboxInputSoundDeviceNameClient);
            this.GroupBoxSound.Controls.Add(this.ButtonClientListen);
            this.GroupBoxSound.Controls.Add(this.label1);
            this.GroupBoxSound.Controls.Add(this.ComboboxOutputSoundDeviceNameClient);
            this.GroupBoxSound.Controls.Add(this.LabelOutputSoundDeviceNameClient);
            this.GroupBoxSound.Location = new System.Drawing.Point(12, 414);
            this.GroupBoxSound.Name = "GroupBoxSound";
            this.GroupBoxSound.Size = new System.Drawing.Size(436, 108);
            this.GroupBoxSound.TabIndex = 31;
            this.GroupBoxSound.TabStop = false;
            // 
            // ProgressBarPlayingClient
            // 
            this.ProgressBarPlayingClient.Location = new System.Drawing.Point(112, 61);
            this.ProgressBarPlayingClient.Name = "ProgressBarPlayingClient";
            this.ProgressBarPlayingClient.Size = new System.Drawing.Size(309, 24);
            this.ProgressBarPlayingClient.TabIndex = 37;
            this.ProgressBarPlayingClient.Visible = false;
            // 
            // ButtonClientSpeak
            // 
            this.ButtonClientSpeak.BackColor = System.Drawing.Color.Gainsboro;
            this.ButtonClientSpeak.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonClientSpeak.Location = new System.Drawing.Point(59, 17);
            this.ButtonClientSpeak.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonClientSpeak.Name = "ButtonClientSpeak";
            this.ButtonClientSpeak.Size = new System.Drawing.Size(32, 32);
            this.ButtonClientSpeak.TabIndex = 44;
            this.ButtonClientSpeak.UseVisualStyleBackColor = false;
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
            this.ComboboxInputSoundDeviceNameClient.Location = new System.Drawing.Point(112, 22);
            this.ComboboxInputSoundDeviceNameClient.Margin = new System.Windows.Forms.Padding(2);
            this.ComboboxInputSoundDeviceNameClient.Name = "ComboboxInputSoundDeviceNameClient";
            this.ComboboxInputSoundDeviceNameClient.Size = new System.Drawing.Size(309, 24);
            this.ComboboxInputSoundDeviceNameClient.TabIndex = 35;
            // 
            // ButtonClientListen
            // 
            this.ButtonClientListen.BackColor = System.Drawing.Color.Gainsboro;
            this.ButtonClientListen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonClientListen.Location = new System.Drawing.Point(58, 56);
            this.ButtonClientListen.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonClientListen.Name = "ButtonClientListen";
            this.ButtonClientListen.Size = new System.Drawing.Size(32, 32);
            this.ButtonClientListen.TabIndex = 45;
            this.ButtonClientListen.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Speak";
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
            this.ComboboxOutputSoundDeviceNameClient.Location = new System.Drawing.Point(112, 61);
            this.ComboboxOutputSoundDeviceNameClient.Margin = new System.Windows.Forms.Padding(2);
            this.ComboboxOutputSoundDeviceNameClient.Name = "ComboboxOutputSoundDeviceNameClient";
            this.ComboboxOutputSoundDeviceNameClient.Size = new System.Drawing.Size(309, 24);
            this.ComboboxOutputSoundDeviceNameClient.TabIndex = 12;
            // 
            // LabelOutputSoundDeviceNameClient
            // 
            this.LabelOutputSoundDeviceNameClient.AutoSize = true;
            this.LabelOutputSoundDeviceNameClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelOutputSoundDeviceNameClient.Location = new System.Drawing.Point(8, 64);
            this.LabelOutputSoundDeviceNameClient.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelOutputSoundDeviceNameClient.Name = "LabelOutputSoundDeviceNameClient";
            this.LabelOutputSoundDeviceNameClient.Size = new System.Drawing.Size(46, 17);
            this.LabelOutputSoundDeviceNameClient.TabIndex = 11;
            this.LabelOutputSoundDeviceNameClient.Text = "Listen";
            // 
            // ChatDeVoz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 534);
            this.Controls.Add(this.GroupBoxSound);
            this.Controls.Add(this.GroupBoxClient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ChatDeVoz";
            this.Text = "Chat de Voz";
            this.GroupBoxClient.ResumeLayout(false);
            this.GroupBoxSound.ResumeLayout(false);
            this.GroupBoxSound.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxClient;
        private System.Windows.Forms.Button ButtonClient;
        private System.Windows.Forms.Label LabelClient;
        private System.Windows.Forms.GroupBox GroupBoxSound;
        private System.Windows.Forms.ProgressBar ProgressBarPlayingClient;
        private System.Windows.Forms.Button ButtonClientSpeak;
        private System.Windows.Forms.ComboBox ComboboxInputSoundDeviceNameClient;
        private System.Windows.Forms.Button ButtonClientListen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComboboxOutputSoundDeviceNameClient;
        private System.Windows.Forms.Label LabelOutputSoundDeviceNameClient;
    }
}

