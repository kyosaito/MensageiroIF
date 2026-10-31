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
    public partial class FormEnvio : Form
    {
        private readonly ServicoDeHistorico servico;

        public FormEnvio()
        {
            InitializeComponent();
        }

        public FormEnvio(ServicoDeHistorico servico)
        {
            InitializeComponent();
            this.servico = servico;
        }

        private void btnEvniar_Click(object sender, EventArgs e)
        {

            try
            {
                servico.Registrar(txtMensagem.Text);
                AtualizarHistorico();
                txtMensagem.Clear();
            }
            catch (MensagemInvalidaException ex)
            {
                MessageBox.Show(ex.Message, "Nao foi possivel enviar");
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Formato de mensagem inválido.");
            }
        }

        public void AtualizarHistorico()
        {
            lstHistorico.Items.Clear();
            foreach (Mensagem m in servico.ObterHistorico())
            {
                lstHistorico.Items.Add(m.ToString());
            }
        }
    }
}
