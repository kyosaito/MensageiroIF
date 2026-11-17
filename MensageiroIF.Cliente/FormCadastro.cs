using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MensageiroIF.Cliente
{
    public partial class FormCadastro : Form
    {
        const int TENTATIVAS = 20;
        const int ESPERA_MS = 200;
        readonly string diretorio;

        public SessaoDeCliente Sessao { get; private set; }

        
        public FormCadastro()
        {
            InitializeComponent();
        }
        public FormCadastro(string diretorio)
        {
            InitializeComponent();

            this.diretorio = diretorio;
            lblDiretorio.Text = $"Diretório: {diretorio}";
        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string apelido = txtApelido.Text.Trim().ToLower();

            SessaoDeCliente sessao;

            try
            {
                sessao = SessaoDeCliente.Criar(diretorio, apelido);
            }
            catch (ApelidoInvalidoException erro)
            {
                Recusar(erro.Message);
                return;
            }

            btnEntrar.Enabled = false;
            lblStatus.ForeColor = Color.Black;
            lblStatus.Text = "Entrando...";
            Application.DoEvents();

            try
            {
                sessao.PedirLogin();
            }
            catch(Exception erro)
            {
                Falhar($"Não foi possível acessar o diretório compartilhado. {erro.Message}");
                return;
            }

            EsperarResposta(sessao);

        }

        void EsperarResposta(SessaoDeCliente sessao)
        {
            for (int tentativa = 1; tentativa <= TENTATIVAS; tentativa++)
            {
                ResultadoDeLogin resultado;

                try
                {
                    resultado = sessao.TentarEntrar();
                }
                catch(Exception erro)
                {
                    Falhar($"Falha ao ler a resposta do servidor. {erro.Message}");
                    return;
                }

                if (resultado == ResultadoDeLogin.Aceito)
                {
                    Sessao = sessao;
                    DialogResult = DialogResult.OK;
                    return;
                }

                if (resultado == ResultadoDeLogin.Recusado)
                {
                    Falhar(DescreverErro(sessao.MotivoDaRecusa));
                    return;
                }

                Thread.Sleep(ESPERA_MS);
                Application.DoEvents();
            }

            Falhar($"O servidor não respondeu em {TENTATIVAS * ESPERA_MS / 1000} segundos. Ele está aberto?");
        }

        string DescreverErro(ErroDoServidor erro)
        {
            if (erro == null)
                return "O servidor recusou o login.";

            if (erro.Codigo == AnalisadorDeProtocolo.APELIDO_EM_USO)
                return $"O apelido \"{erro.Detalhe}\" já esta em uso. Escolha outro.";

            if (erro.Codigo == AnalisadorDeProtocolo.PROTOCOLO_INVALIDO)
                return $"O servidor não entendeu a mensagem de login.";

            return $"O servidor recusou: {erro.Codigo}";
        }

        private void Recusar(string motivo)
        {
            lblStatus.ForeColor = Color.Firebrick;
            lblStatus.Text = $"Status: {motivo}";
            txtApelido.Focus();
            txtApelido.SelectAll();
        }

        private void Falhar(string motivo)
        {
            Recusar(motivo);
            btnEntrar.Enabled = true;
        }


        // Limpa o aviso assim que a pessoa comeca a corrigir o campo.
        private void txtApelido_TextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
