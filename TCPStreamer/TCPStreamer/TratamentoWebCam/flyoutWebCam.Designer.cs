
namespace TCPStreamer
{
    partial class flyoutWebCam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picWebCam = new System.Windows.Forms.PictureBox();
            this.btnFoto = new System.Windows.Forms.Button();
            this.lblPastaDestino = new System.Windows.Forms.Label();
            this.btnProcurarPasta = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.picImagemSalva = new System.Windows.Forms.PictureBox();
            this.btnSalvarImagem = new System.Windows.Forms.Button();
            this.lblStatusImagem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagemSalva)).BeginInit();
            this.SuspendLayout();
            // 
            // picWebCam
            // 
            this.picWebCam.Location = new System.Drawing.Point(12, 43);
            this.picWebCam.Name = "picWebCam";
            this.picWebCam.Size = new System.Drawing.Size(309, 323);
            this.picWebCam.TabIndex = 0;
            this.picWebCam.TabStop = false;
            // 
            // btnFoto
            // 
            this.btnFoto.Location = new System.Drawing.Point(17, 387);
            this.btnFoto.Name = "btnFoto";
            this.btnFoto.Size = new System.Drawing.Size(190, 42);
            this.btnFoto.TabIndex = 1;
            this.btnFoto.Text = "Tirar Foto";
            this.btnFoto.UseVisualStyleBackColor = true;
            this.btnFoto.Click += new System.EventHandler(this.btnFoto_Click);
            // 
            // lblPastaDestino
            // 
            this.lblPastaDestino.AutoSize = true;
            this.lblPastaDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastaDestino.Location = new System.Drawing.Point(13, 13);
            this.lblPastaDestino.Name = "lblPastaDestino";
            this.lblPastaDestino.Size = new System.Drawing.Size(144, 20);
            this.lblPastaDestino.TabIndex = 3;
            this.lblPastaDestino.Text = "Selecione a pasta :";
            // 
            // btnProcurarPasta
            // 
            this.btnProcurarPasta.Location = new System.Drawing.Point(572, 8);
            this.btnProcurarPasta.Name = "btnProcurarPasta";
            this.btnProcurarPasta.Size = new System.Drawing.Size(73, 32);
            this.btnProcurarPasta.TabIndex = 4;
            this.btnProcurarPasta.Text = "Procurar";
            this.btnProcurarPasta.UseVisualStyleBackColor = true;
            this.btnProcurarPasta.Click += new System.EventHandler(this.btnProcurarPasta_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.Location = new System.Drawing.Point(409, 387);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(68, 42);
            this.btnFechar.TabIndex = 5;
            this.btnFechar.Text = "Fechar";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // picImagemSalva
            // 
            this.picImagemSalva.Location = new System.Drawing.Point(336, 43);
            this.picImagemSalva.Name = "picImagemSalva";
            this.picImagemSalva.Size = new System.Drawing.Size(309, 323);
            this.picImagemSalva.TabIndex = 7;
            this.picImagemSalva.TabStop = false;
            // 
            // btnSalvarImagem
            // 
            this.btnSalvarImagem.Location = new System.Drawing.Point(213, 387);
            this.btnSalvarImagem.Name = "btnSalvarImagem";
            this.btnSalvarImagem.Size = new System.Drawing.Size(190, 42);
            this.btnSalvarImagem.TabIndex = 8;
            this.btnSalvarImagem.Text = "Salvar Imagem";
            this.btnSalvarImagem.UseVisualStyleBackColor = true;
            this.btnSalvarImagem.Click += new System.EventHandler(this.btnSalvarImagem_Click);
            // 
            // lblStatusImagem
            // 
            this.lblStatusImagem.AutoSize = true;
            this.lblStatusImagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusImagem.Location = new System.Drawing.Point(503, 416);
            this.lblStatusImagem.Name = "lblStatusImagem";
            this.lblStatusImagem.Size = new System.Drawing.Size(0, 13);
            this.lblStatusImagem.TabIndex = 6;
            // 
            // flyoutWebCam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 450);
            this.Controls.Add(this.btnSalvarImagem);
            this.Controls.Add(this.picImagemSalva);
            this.Controls.Add(this.lblStatusImagem);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnProcurarPasta);
            this.Controls.Add(this.lblPastaDestino);
            this.Controls.Add(this.btnFoto);
            this.Controls.Add(this.picWebCam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "flyoutWebCam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Chat de Video";
            this.Load += new System.EventHandler(this.flyoutWebCam_Load);
            this.Leave += new System.EventHandler(this.flyoutWebCam_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.picWebCam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImagemSalva)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picWebCam;
        private System.Windows.Forms.Button btnFoto;
        private System.Windows.Forms.Label lblPastaDestino;
        private System.Windows.Forms.Button btnProcurarPasta;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.PictureBox picImagemSalva;
        private System.Windows.Forms.Button btnSalvarImagem;
        private System.Windows.Forms.Label lblStatusImagem;
    }
}