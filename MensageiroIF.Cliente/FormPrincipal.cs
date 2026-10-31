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
        ServicoDeHistorico servico;
        public FormPrincipal(ServicoDeHistorico servico)
        {
            this.servico = servico;
            InitializeComponent();
        }

        public void ConfigurarColunas()
        {
            dgvHistorico.AutoGenerateColumns = false;

            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Remetente", HeaderText = "De" });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Corpo", HeaderText = "Mensagem" });

            dgvHistorico.DataSource = servico.ObterHistorico(); //necessita obter ServicoDeHistorico pelo construtor
        }

        private void AtualizarHistorico()
        {
            dgvHistorico.DataSource = null; 
            dgvHistorico.DataSource = servico.ObterHistorico(); 
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            ConfigurarColunas();
            AtualizarHistorico();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                servico.Registrar(txtMensagem.Text);
                AtualizarHistorico();
                txtMensagem.Clear();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
