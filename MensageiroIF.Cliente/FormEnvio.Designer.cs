namespace MensageiroIF.Cliente
{
    partial class FormEnvio
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
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.cboDestinatario = new System.Windows.Forms.ComboBox();
            this.chkUrgente = new System.Windows.Forms.CheckBox();
            this.btnEvniar = new System.Windows.Forms.Button();
            this.lstHistorico = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(12, 12);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(491, 81);
            this.txtMensagem.TabIndex = 0;
            // 
            // cboDestinatario
            // 
            this.cboDestinatario.FormattingEnabled = true;
            this.cboDestinatario.Location = new System.Drawing.Point(12, 99);
            this.cboDestinatario.Name = "cboDestinatario";
            this.cboDestinatario.Size = new System.Drawing.Size(408, 24);
            this.cboDestinatario.TabIndex = 1;
            // 
            // chkUrgente
            // 
            this.chkUrgente.AutoSize = true;
            this.chkUrgente.Location = new System.Drawing.Point(426, 101);
            this.chkUrgente.Name = "chkUrgente";
            this.chkUrgente.Size = new System.Drawing.Size(77, 20);
            this.chkUrgente.TabIndex = 2;
            this.chkUrgente.Text = "Urgente";
            this.chkUrgente.UseVisualStyleBackColor = true;
            // 
            // btnEvniar
            // 
            this.btnEvniar.Location = new System.Drawing.Point(358, 129);
            this.btnEvniar.Name = "btnEvniar";
            this.btnEvniar.Size = new System.Drawing.Size(145, 36);
            this.btnEvniar.TabIndex = 3;
            this.btnEvniar.Text = "Enviar";
            this.btnEvniar.UseVisualStyleBackColor = true;
            this.btnEvniar.Click += new System.EventHandler(this.btnEvniar_Click);
            // 
            // lstHistorico
            // 
            this.lstHistorico.FormattingEnabled = true;
            this.lstHistorico.ItemHeight = 16;
            this.lstHistorico.Location = new System.Drawing.Point(12, 188);
            this.lstHistorico.Name = "lstHistorico";
            this.lstHistorico.Size = new System.Drawing.Size(491, 196);
            this.lstHistorico.TabIndex = 4;
            // 
            // FormEnvio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 394);
            this.Controls.Add(this.lstHistorico);
            this.Controls.Add(this.btnEvniar);
            this.Controls.Add(this.chkUrgente);
            this.Controls.Add(this.cboDestinatario);
            this.Controls.Add(this.txtMensagem);
            this.Name = "FormEnvio";
            this.Text = "FormEnvio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.ComboBox cboDestinatario;
        private System.Windows.Forms.CheckBox chkUrgente;
        private System.Windows.Forms.Button btnEvniar;
        private System.Windows.Forms.ListBox lstHistorico;
    }
}