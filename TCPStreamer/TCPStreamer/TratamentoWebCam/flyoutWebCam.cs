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

        //Quando carregar o form, da start na classe para carregar a camera.
        private void flyoutWebCam_Load(object sender, EventArgs e)
        {
            cameraLigada = true;

            //Verificar se a camera esta ligada, se não liga ela.
            if (!cameraLigada)
                CaptureInfo.Start();

            //Instancia a classe do DirectX para ter acesso aos filtros.
            CamContainer = new DirectX.Capture.Filters();
            try
            {
                //Cria uma lista para carregar todos os dispositivos de video.
                int no_of_cam = CamContainer.VideoInputDevices.Count;

                //Pra cada dispotivo ele abre uma camera
                for (int i = 0; i < no_of_cam; i++)
                {
                    // obtém o dispositivo de entrada do vídeo
                    Camera = CamContainer.VideoInputDevices[i];

                    // inicializa a Captura usando o dispositivo
                    CaptureInfo = new DirectX.Capture.Capture(Camera, null);

                    // Define a janela de visualização do vídeo
                    CaptureInfo.PreviewWindow = this.picWebCam;

                    //Atualiza a imagem da camera
                    CaptureInfo.FrameCaptureComplete += AtualizaImagem;

                    // Captura o frame do dispositivo
                    CaptureInfo.CaptureFrame();

                    TratamentoDeMensagens("Camera aberta e funcionando...", Color.Blue);

                }
            }
            catch (Exception ex)
            {
                TratamentoDeMensagens("Erro ao iniciar: " + ex.Message, Color.Red);
            }

        }
        private void AtualizaImagem(PictureBox frame)
        {
            try
            {
                capturaImagem = frame.Image;
                this.picImagemSalvar.Image = capturaImagem;
            }
            catch (Exception ex)
            {
                TratamentoDeMensagens("Erro " + ex.Message, Color.Red);
            }
        }

        //Clique do botão de Salvar Imagem.
        private void btnFoto_Click(object sender, EventArgs e)
        {
            try
            {
                //Metodo para tirar a foto
                CaptureInfo.CaptureFrame();
                //Se o caminho estiver vazio, ele da erro e nao salva.
                if (string.IsNullOrEmpty(lblPastaDestino.Text))
                {
                    TratamentoDeMensagens("Selecione uma pasta para salvar a Imagem...", Color.Red);
                    return;
                }
                //Passo primeiro o caminho, depois o nome, data e hora
                caminhoImagemSalva = lblPastaDestino.Text + @"\" + @"ImagemWebCam" + DateTime.Now.Day.ToString() 
                                                                                   + DateTime.Now.Month.ToString() 
                                                                                   + DateTime.Now.Year.ToString() 
                                                                                   + DateTime.Now.Millisecond.ToString() 
                                                                                   + ".jpg";
                //Salva a imagem
                picImagemSalvar.Image.Save(caminhoImagemSalva, ImageFormat.Jpeg);
 
                TratamentoDeMensagens("Imagem salva com sucesso...", Color.Green);

            }
            catch (Exception ex)
            {
                TratamentoDeMensagens("Erro : " + ex.Message, Color.Red);
            }

        }
        //Chama layout para selecionar a pasta
        private void btnProcurarPasta_Click(object sender, EventArgs e)
        {
            //Chama classe padrão windows.Forms
            FolderBrowserDialog fdlARquivos = new FolderBrowserDialog();
            //Se apertar confirmar, label destino se tornao path.
            if (fdlARquivos.ShowDialog() == DialogResult.OK)
                lblPastaDestino.Text = fdlARquivos.SelectedPath;
        }

        //Fecha o form;
        private void btnFechar_Click(object sender, EventArgs e)
        {
            FecharWebCam();
        }

        private void flyoutWebCam_Leave(object sender, EventArgs e)
        {
            FecharWebCam();
        }

        //Para evitar que fique aberto consumindo dados, dar stop e dispose na camera.
        private void FecharWebCam()
        {
            this.Close();
            this.Dispose();
            CaptureInfo.Stop();
            CaptureInfo.Dispose();
            CaptureInfo.DisposeCapture();

            cameraLigada = false;
        }

        //Classe especifica para tratamento dos status.
        private void TratamentoDeMensagens(string sMensagem, Color oCor)
        {
            lblStatusImagem.Text = sMensagem;
            lblStatusImagem.ForeColor = oCor;
        }
    }
}