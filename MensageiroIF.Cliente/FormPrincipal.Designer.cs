namespace MensageiroIF.Cliente
{
    partial class FormPrincipal
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
            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tabConversa = new System.Windows.Forms.TabPage();
            this.tabContatos = new System.Windows.Forms.TabPage();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            this.lstContatos = new System.Windows.Forms.ListBox();
            this.btnAdicionarContato = new System.Windows.Forms.Button();
            this.tabPrincipal.SuspendLayout();
            this.tabConversa.SuspendLayout();
            this.tabContatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.SuspendLayout();
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Controls.Add(this.tabConversa);
            this.tabPrincipal.Controls.Add(this.tabContatos);
            this.tabPrincipal.Location = new System.Drawing.Point(5, 12);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(792, 436);
            this.tabPrincipal.TabIndex = 0;
            // 
            // tabConversa
            // 
            this.tabConversa.Controls.Add(this.dgvHistorico);
            this.tabConversa.Controls.Add(this.btnEnviar);
            this.tabConversa.Controls.Add(this.txtMensagem);
            this.tabConversa.Location = new System.Drawing.Point(4, 25);
            this.tabConversa.Name = "tabConversa";
            this.tabConversa.Padding = new System.Windows.Forms.Padding(3);
            this.tabConversa.Size = new System.Drawing.Size(784, 407);
            this.tabConversa.TabIndex = 0;
            this.tabConversa.Text = "Conversa";
            this.tabConversa.UseVisualStyleBackColor = true;
            // 
            // tabContatos
            // 
            this.tabContatos.Controls.Add(this.btnAdicionarContato);
            this.tabContatos.Controls.Add(this.lstContatos);
            this.tabContatos.Location = new System.Drawing.Point(4, 25);
            this.tabContatos.Name = "tabContatos";
            this.tabContatos.Padding = new System.Windows.Forms.Padding(3);
            this.tabContatos.Size = new System.Drawing.Size(784, 407);
            this.tabContatos.TabIndex = 1;
            this.tabContatos.Text = "Contatos";
            this.tabContatos.UseVisualStyleBackColor = true;
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(6, 6);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(772, 129);
            this.txtMensagem.TabIndex = 0;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(589, 141);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(189, 30);
            this.btnEnviar.TabIndex = 1;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorico.Location = new System.Drawing.Point(6, 177);
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.RowHeadersWidth = 51;
            this.dgvHistorico.RowTemplate.Height = 24;
            this.dgvHistorico.Size = new System.Drawing.Size(772, 227);
            this.dgvHistorico.TabIndex = 2;
            // 
            // lstContatos
            // 
            this.lstContatos.FormattingEnabled = true;
            this.lstContatos.ItemHeight = 16;
            this.lstContatos.Location = new System.Drawing.Point(6, 6);
            this.lstContatos.Name = "lstContatos";
            this.lstContatos.Size = new System.Drawing.Size(772, 356);
            this.lstContatos.TabIndex = 0;
            // 
            // btnAdicionarContato
            // 
            this.btnAdicionarContato.Location = new System.Drawing.Point(586, 369);
            this.btnAdicionarContato.Name = "btnAdicionarContato";
            this.btnAdicionarContato.Size = new System.Drawing.Size(192, 32);
            this.btnAdicionarContato.TabIndex = 1;
            this.btnAdicionarContato.Text = "Adicionar contato";
            this.btnAdicionarContato.UseVisualStyleBackColor = true;
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabPrincipal);
            this.Name = "FormPrincipal";
            this.Text = "FormPrincipal";
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.tabPrincipal.ResumeLayout(false);
            this.tabConversa.ResumeLayout(false);
            this.tabConversa.PerformLayout();
            this.tabContatos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.TabPage tabConversa;
        private System.Windows.Forms.TabPage tabContatos;
        private System.Windows.Forms.DataGridView dgvHistorico;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Button btnAdicionarContato;
        private System.Windows.Forms.ListBox lstContatos;
    }
}