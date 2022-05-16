using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TCPStreamer
{
    public partial class flyoutWebCam : Form
    {
        public DirectX.Capture.Filter Camera;
        public DirectX.Capture.Capture CaptureInfo;
        public DirectX.Capture.Filters CamContainer;
        Image capturaImagem;
        public string caminhoImagemSalva = null;

        public bool cameraLigada;

        public flyoutWebCam()
        {
            InitializeComponent();
        }

        private void flyoutWebCam_Load(object sender, EventArgs e)
        {
            cameraLigada = true;

            if(!cameraLigada)
                CaptureInfo.Start();
           
            CamContainer = new DirectX.Capture.Filters();
            try
            {
                int no_of_cam = CamContainer.VideoInputDevices.Count;

                for (int i = 0; i < no_of_cam; i++)
                {
                    // obtém o dispositivo de entrada do vídeo
                    Camera = CamContainer.VideoInputDevices[i];

                    // inicializa a Captura usando o dispositivo
                    CaptureInfo = new DirectX.Capture.Capture(Camera, null);

                    // Define a janela de visualização do vídeo
                    CaptureInfo.PreviewWindow = this.picWebCam;

                    CaptureInfo.FrameCaptureComplete += AtualizaImagem;

                    // Captura o frame do dispositivo
                    CaptureInfo.CaptureFrame();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }
        private void AtualizaImagem(PictureBox frame)
        {
            try
            {
                capturaImagem = frame.Image;
                this.picImagemSalva.Image = capturaImagem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }
        private void btnFoto_Click(object sender, EventArgs e)
        {
            try
            {
                CaptureInfo.CaptureFrame();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }               

        }
        private void btnProcurarPasta_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdlARquivos = new FolderBrowserDialog();

            if (fdlARquivos.ShowDialog() == DialogResult.OK)
                lblPastaDestino.Text = fdlARquivos.SelectedPath;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            FecharWebCam();
        }

        private void flyoutWebCam_Leave(object sender, EventArgs e)
        {
            FecharWebCam();
        }

        private void FecharWebCam()
        {
            this.Close();
            this.Dispose();
            CaptureInfo.Stop();
            CaptureInfo.Dispose();

            cameraLigada = false;
        }
        private void btnSalvarImagem_Click(object sender, EventArgs e)
        {
            try
            {
                caminhoImagemSalva = @"D:\SALVAR ESTA PASTA" + "ImagemWebCam" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".jpg";
                picImagemSalva.Image.Save(caminhoImagemSalva, ImageFormat.Jpeg);
                MessageBox.Show("Imagem salva com sucesso");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message);
            }
        }
    }
}




