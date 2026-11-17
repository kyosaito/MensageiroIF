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
            this.components = new System.ComponentModel.Container();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.tabPrincipal = new System.Windows.Forms.TabControl();
            this.tabContatos = new System.Windows.Forms.TabPage();
            this.btnAdicionarContato = new System.Windows.Forms.Button();
            this.txtNovoContato = new System.Windows.Forms.TextBox();
            this.lstContatos = new System.Windows.Forms.ListBox();
            this.lblAjuda = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerCiclo = new System.Windows.Forms.Timer(this.components);
            this.tabPrincipal.SuspendLayout();
            this.tabContatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(12, 9);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(217, 20);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Você está logado como: ";
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPrincipal.Controls.Add(this.tabContatos);
            this.tabPrincipal.Location = new System.Drawing.Point(12, 32);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.SelectedIndex = 0;
            this.tabPrincipal.Size = new System.Drawing.Size(828, 450);
            this.tabPrincipal.TabIndex = 1;
            this.tabPrincipal.SelectedIndexChanged += new System.EventHandler(this.tabPrincipal_SelectedIndexChanged);
            // 
            // tabContatos
            // 
            this.tabContatos.Controls.Add(this.btnAdicionarContato);
            this.tabContatos.Controls.Add(this.txtNovoContato);
            this.tabContatos.Controls.Add(this.lstContatos);
            this.tabContatos.Controls.Add(this.lblAjuda);
            this.tabContatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabContatos.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tabContatos.Location = new System.Drawing.Point(4, 25);
            this.tabContatos.Name = "tabContatos";
            this.tabContatos.Padding = new System.Windows.Forms.Padding(3);
            this.tabContatos.Size = new System.Drawing.Size(820, 421);
            this.tabContatos.TabIndex = 0;
            this.tabContatos.Text = "Contatos";
            this.tabContatos.UseVisualStyleBackColor = true;
            // 
            // btnAdicionarContato
            // 
            this.btnAdicionarContato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdicionarContato.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdicionarContato.Location = new System.Drawing.Point(386, 379);
            this.btnAdicionarContato.Name = "btnAdicionarContato";
            this.btnAdicionarContato.Size = new System.Drawing.Size(231, 29);
            this.btnAdicionarContato.TabIndex = 3;
            this.btnAdicionarContato.Text = "Abrir conversa";
            this.btnAdicionarContato.UseVisualStyleBackColor = true;
            this.btnAdicionarContato.Click += new System.EventHandler(this.btnAdicionarContato_Click);
            // 
            // txtNovoContato
            // 
            this.txtNovoContato.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNovoContato.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNovoContato.Location = new System.Drawing.Point(6, 382);
            this.txtNovoContato.Name = "txtNovoContato";
            this.txtNovoContato.Size = new System.Drawing.Size(374, 24);
            this.txtNovoContato.TabIndex = 2;
            // 
            // lstContatos
            // 
            this.lstContatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstContatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstContatos.FormattingEnabled = true;
            this.lstContatos.ItemHeight = 18;
            this.lstContatos.Location = new System.Drawing.Point(6, 24);
            this.lstContatos.Name = "lstContatos";
            this.lstContatos.Size = new System.Drawing.Size(808, 346);
            this.lstContatos.TabIndex = 1;
            this.lstContatos.DoubleClick += new System.EventHandler(this.lstContatos_DoubleClick);
            // 
            // lblAjuda
            // 
            this.lblAjuda.AutoSize = true;
            this.lblAjuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAjuda.Location = new System.Drawing.Point(6, 3);
            this.lblAjuda.Name = "lblAjuda";
            this.lblAjuda.Size = new System.Drawing.Size(433, 18);
            this.lblAjuda.TabIndex = 0;
            this.lblAjuda.Text = "Dê um duplo clique num contato para abrir a conversa numa aba";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(16, 489);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(820, 23);
            this.lblStatus.TabIndex = 2;
            // 
            // timerCiclo
            // 
            this.timerCiclo.Tick += new System.EventHandler(this.timerCiclo_Tick);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 519);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.tabPrincipal);
            this.Controls.Add(this.lblUsuario);
            this.Name = "FormPrincipal";
            this.Text = "MensageiroIF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.tabPrincipal.ResumeLayout(false);
            this.tabContatos.ResumeLayout(false);
            this.tabContatos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TabControl tabPrincipal;
        private System.Windows.Forms.TabPage tabContatos;
        private System.Windows.Forms.Label lblAjuda;
        private System.Windows.Forms.TextBox txtNovoContato;
        private System.Windows.Forms.ListBox lstContatos;
        private System.Windows.Forms.Button btnAdicionarContato;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerCiclo;
    }
}