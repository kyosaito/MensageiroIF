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
    public partial class FormPrincipal : Form
    {
        const int CICLOS_POR_LISTAGEM = 5;

        readonly SessaoDeCliente sessao;
        readonly Usuario usuarioLogado;

        //Um repositório para o cliente inteiro. Cada aba tem seu ServicoDeHistorico,
        //mas todos guardam no mesmo lugar - e cada serviço filtra a sua conversa na hora de ler
        readonly IRepositorioDeMensagens repositorio = new RepositorioEmMemoria();

        readonly Dictionary<string, PainelDeConversa> conversas = new Dictionary<string, PainelDeConversa>();
        readonly Dictionary<string, TabPage> abas = new Dictionary<string, TabPage>();

        int ciclos;

        // Fica true enquanto a lista de contatos esta sendo repovoada. Sem isso, o
        // Clear() dispara o SelectedIndexChanged, que limpa a conversa e pede o
        // historico de novo - a cada 10 segundos, sozinho.
        bool repovoando;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        public FormPrincipal(SessaoDeCliente sessao)
        {
            this.sessao = sessao;

            InitializeComponent();

            usuarioLogado = SessaoDeCliente.UsuarioLogado;

            Text = $"Mensageiro IF - {sessao.Apelido}";
            lblUsuario.Text = $"Você está logado como: {sessao.Apelido}";
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            PedirLista();
            timerCiclo.Start();
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerCiclo.Stop();

            // Avisa o servidor que saiu. Se a pasta sumiu, nao adianta impedir a
            // janela de fechar por causa disso.
            try
            {
                sessao.Sair();
            }
            catch (Exception)
            {
            }
        }

        void PedirLista()
        {
            try
            {
                sessao.Canal.Enviar(CanalArquivo.SERVIDOR, 
                    $"{AnalisadorDeProtocolo.LISTAR}{AnalisadorDeProtocolo.SEPARADOR}{sessao.Apelido}");
            }
            catch (Exception ex)
            {
                Avisar($"Falha de comunicação com o servidor: {ex.Message}", true);
            }
        }

        void Avisar(string texto, bool erro)
        {
            lblStatus.ForeColor = erro ? Color.Firebrick : Color.DimGray;
            lblStatus.Text = texto;
        }

        private void timerCiclo_Tick(object sender, EventArgs e)
        {
            RespostaDoServidor resposta;

            try
            {
                resposta = sessao.Receber();
            }
            catch (Exception erro)
            {
                Avisar($"Falha ao ler a caixa: {erro.Message}", true);
                return;
            }

            if (resposta.Contatos != null)
                AtualizarContatos(resposta.Contatos);

            foreach (MensagemTexto mensagem in resposta.Mensagens)
                MostrarMensagem(mensagem);

            foreach (ErroDoServidor erro in resposta.Erros)
                Avisar(DescreverErro(erro), true);

            ciclos++;
            if (ciclos % CICLOS_POR_LISTAGEM == 0)
                PedirLista();
        }

        void AtualizarContatos(List<string> contatos)
        {
            string selecionado = lstContatos.SelectedItem == null ? "" : lstContatos.SelectedItem.ToString();

            repovoando = true;
            lstContatos.BeginUpdate();
            lstContatos.Items.Clear();

            foreach (string apelido in contatos)
                lstContatos.Items.Add(apelido);

            if (selecionado != "")
            {
                int posicao = lstContatos.Items.IndexOf(selecionado);
                if (posicao >= 0)
                    lstContatos.SelectedIndex = posicao;
            }

            lstContatos.EndUpdate();
            repovoando = false;
        }


        private void MostrarMensagem(MensagemTexto mensagem)
        {
            string outro = OutroLadoDa(mensagem);
            PainelDeConversa painel = AbrirConversa(outro);
            
            if (painel == null)
                return;

            painel.Arquivar(mensagem);
            AtualizarTituloDaAba(outro);
        }

        private string OutroLadoDa(MensagemTexto mensagem)
        {
            if (mensagem.Remetente == sessao.Apelido)
                return mensagem.Destinatario;

            return mensagem.Remetente;
        }



        PainelDeConversa AbrirConversa(string contato)
        {
            if (string.IsNullOrWhiteSpace(contato))
                return null;

            contato = contato.Trim().ToLower();

            if (contato == sessao.Apelido || contato == CanalArquivo.SERVIDOR)
                return null;

            if (conversas.ContainsKey(contato))
                return conversas[contato];

            // Cada aba tem o SEU serviço, com o SEU destinatário. O repositório
            // é o mesmo para todas; quem separa as conversas é o serviço.
            ServicoDeHistorico servico = new ServicoDeHistorico(repositorio, usuarioLogado, new Usuario(contato));

            PainelDeConversa painel = new PainelDeConversa(sessao, servico);
            painel.Dock = DockStyle.Fill;
            painel.FecharPedido += Painel_FecharPedido;

            TabPage aba = new TabPage(contato);
            aba.Padding = new Padding(3);
            aba.UseVisualStyleBackColor = true;
            aba.Controls.Add(painel);

            tabPrincipal.TabPages.Add(aba);

            conversas[contato] = painel;
            abas[contato] = aba;

            try
            {
                sessao.PedirHistorico(contato);
            }
            catch (Exception erro)
            {
                Avisar($"Não foi possível solicitar o histórico: {erro.Message}", true);
            }

            return painel;
        }

        void Painel_FecharPedido(object sender, EventArgs e)
        {
            PainelDeConversa painel = sender as PainelDeConversa;

            if (painel == null)
                return;

            FecharConversa(painel.Contato);
        }

        void FecharConversa(string contato)
        {
            if (!abas.ContainsKey(contato))
                return;

            TabPage aba = abas[contato];
            tabPrincipal.TabPages.Remove(aba);
            aba.Dispose();

            abas.Remove(contato);
            conversas.Remove(contato);

            // Fechar a aba não apaga o histórico: ele continua no repositório,
            // e reabrir a conversa traz tudo de volta.
            Avisar($"Conversa com {contato} fechada. O histórico foi mantido.", false);
        }

        void AtualizarTituloDaAba(string contato)
        {
            if (!abas.ContainsKey(contato) || !conversas.ContainsKey(contato))
                return;

            int naoLidas = conversas[contato].NaoLidas;
            abas[contato].Text = naoLidas > 0 ? $"{contato} ({naoLidas})" : contato;
        }

        string DescreverErro(ErroDoServidor erro)
        {
            if (erro.Codigo == AnalisadorDeProtocolo.USUARIO_DESCONHECIDO)
                return $"O servidor não conhece \"{erro.Detalhe}\". Ele está on-line?";

            if (erro.Codigo == AnalisadorDeProtocolo.PROTOCOLO_INVALIDO)
                return "O servidor não entendeu a linha enviada.";

            if (erro.Codigo == AnalisadorDeProtocolo.NAO_AUTENTICADO)
                return "O servidor não reconhece esta sessão. Feche e entre de novo.";

            if (erro.Codigo == AnalisadorDeProtocolo.APELIDO_EM_USO)
                return $"O apelido \"{erro.Detalhe}\" já está em uso.";

            return $"O servidor recusou: {erro.Codigo}";
        }

        private void tabPrincipal_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage aba = tabPrincipal.SelectedTab;

            if (aba == null)
                return;

            foreach (string contato in new List<string>(abas.Keys))
            {
                if (abas[contato] == aba)
                {
                    conversas[contato].MarcarComoLida();
                    AtualizarTituloDaAba(contato);
                    conversas[contato].Recarregar();
                }
            }
        }

        private void lstContatos_DoubleClick(object sender, EventArgs e)
        {
            if (repovoando || lstContatos.SelectedItem == null)
                return;

            AbrirESelecionar(lstContatos.SelectedItem.ToString());
        }

        void AbrirESelecionar(string contato)
        {
            bool jaExistia = conversas.ContainsKey(contato.Trim().ToLower());

            PainelDeConversa painel = AbrirConversa(contato);

            if (painel == null)
            {
                Avisar($"Não é possível abrir uma conversa com \"{contato}\".", true);
                return;
            }

            SelecionarConversa(painel.Contato);
            Avisar(jaExistia ? "" : $"Conversa com {painel.Contato} aberta.", false);
        }

        void SelecionarConversa(string contato)
        {
            if (abas.ContainsKey(contato))
                tabPrincipal.SelectedTab = abas[contato];
        }

        private void btnAdicionarContato_Click(object sender, EventArgs e)
        {
            string contato = txtNovoContato.Text.Trim().ToLower();

            try
            {
                Usuario.ConferirApelido(contato);
            }
            catch(Exception)
            {
                Avisar("Digite o apelido de quem você quer chamar.", true);
                txtNovoContato.Focus();
                return;
            }
            
            try
            {
                AnalisadorDeProtocolo.ConferirApelido(contato);
            }
            catch(Exception)
            {
                Avisar("O apelido não pode conter o caractere | .", true);
                txtNovoContato.Focus();
                return;
            }


            if (contato == sessao.Apelido)
            {
                Avisar("Esse é o seu próprio apelido.", true);
                txtNovoContato.Focus();
                return;
            }

            txtNovoContato.Clear();
            AbrirESelecionar(contato);
        }

    }
}
