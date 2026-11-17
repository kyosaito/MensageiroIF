using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MensageiroIF.Cliente
{
    public partial class PainelDeConversa : UserControl
    {
        readonly ServicoDeHistorico servico;
        readonly SessaoDeCliente sessao;

        public string Contato { get { return servico == null ? "" : servico.Destinatario.Apelido; } }

        public int NaoLidas { get; private set; }

        //evento disparado quando a pessoa clica em "Fechar conversa"
        public event EventHandler FecharPedido;

        public PainelDeConversa()
        {
            InitializeComponent();
        }

        public PainelDeConversa(SessaoDeCliente sessao, ServicoDeHistorico servico)
        {
            InitializeComponent();

            this.sessao = sessao;
            this.servico = servico;

            lblConversa.Text = $"Conversa com {servico.Destinatario.Apelido}";

            ConfigurarColunas();
            Recarregar();
        }

        void ConfigurarColunas()
        {
            dgvHistorico.AutoGenerateColumns = false;
            dgvHistorico.Columns.Clear();

            dgvHistorico.Columns.Add(new DataGridViewButtonColumn
            {
                DataPropertyName = "Data",
                HeaderText = "Quando",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM HH:mm:ss" }
            });

            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Remetente",
                HeaderText = "De",
                Width = 110
            });

            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Corpo",
                HeaderText = "Mensagem",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        public void Recarregar()
        {
            if (servico == null)
                return;

            dgvHistorico.DataSource = null;
            dgvHistorico.DataSource = servico.ObterHistorico();

            if (dgvHistorico.Rows.Count > 0)
                dgvHistorico.FirstDisplayedScrollingRowIndex = dgvHistorico.Rows.Count - 1;
        }

        public void Arquivar(MensagemTexto mensagem)
        {
            servico.Arquivar(mensagem);

            if (!EstaVisivel())
                NaoLidas++;

            Recarregar();
        }
        bool EstaVisivel()
        {
            //verfica se o TabPage está aberta
            return Visible && ParentForm != null && ParentForm.WindowState != FormWindowState.Minimized;
        }

        public void MarcarComoLida()
        {
            NaoLidas = 0;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            MensagemTexto mensagem;

            try
            {
                mensagem = servico.Registrar(txtMensagem.Text);
            }
            catch (MensagemInvalidaException ex)
            {
                Avisar(ex.Message, true);
                txtMensagem.Focus();
                return;
            }

            try
            {
                sessao.Canal.Enviar(CanalArquivo.SERVIDOR, mensagem.GetLinha());
                Avisar("", false);
            }
            catch (Exception ex)
            {
                Avisar($"A mensagem não saiu: {ex.Message}", true);
            }

            txtMensagem.Clear();
            Recarregar();

            txtMensagem.Focus();
        }
        void Avisar(string texto, bool erro)
        {
            lblStatus.ForeColor = erro ? Color.Firebrick : Color.DimGray;
            lblStatus.Text = texto;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            if (FecharPedido != null)
                FecharPedido(this, EventArgs.Empty);
        }

        void txtMensagem_TextChanged(object sender, EventArgs e)
        {
            lblContador.Text = $"{250 - txtMensagem.Text.Length} caracteres restantes";
            lblContador.ForeColor = txtMensagem.Text.Length > 250 ? Color.Firebrick : Color.DimGray;
        }
    }
}
