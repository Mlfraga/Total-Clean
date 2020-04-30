namespace totalClean
{
    partial class PendenciaPCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendenciaPCliente));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPagamentosPendentes = new System.Windows.Forms.DataGridView();
            this.IdVenda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Frotista = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPFCNPJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Carro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Placa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pago = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.preco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValorACobrar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormaPagamento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPagamentoRealizado = new System.Windows.Forms.Button();
            this.bunifuSeparator6 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblServico1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbTransferencia = new System.Windows.Forms.RadioButton();
            this.rdbDebito = new System.Windows.Forms.RadioButton();
            this.rdbPermuta = new System.Windows.Forms.RadioButton();
            this.rdbCredito = new System.Windows.Forms.RadioButton();
            this.rdbBoleto = new System.Windows.Forms.RadioButton();
            this.rdbDinheiro = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagamentosPendentes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnPesquisar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPesquisar.FlatAppearance.BorderSize = 0;
            this.btnPesquisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPesquisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.Location = new System.Drawing.Point(351, 101);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(264, 56);
            this.btnPesquisar.TabIndex = 162;
            this.btnPesquisar.Text = "   Pesquisar Pendencias";
            this.btnPesquisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPagamentosPendentes);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(27, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1349, 550);
            this.groupBox2.TabIndex = 161;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pagamentos Pendentes";
            // 
            // dgvPagamentosPendentes
            // 
            this.dgvPagamentosPendentes.AllowUserToAddRows = false;
            this.dgvPagamentosPendentes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.dgvPagamentosPendentes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagamentosPendentes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdVenda,
            this.Frotista,
            this.Cliente,
            this.CPFCNPJ,
            this.Carro,
            this.Placa,
            this.Data,
            this.pago,
            this.preco,
            this.ValorACobrar,
            this.FormaPagamento});
            this.dgvPagamentosPendentes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.dgvPagamentosPendentes.Location = new System.Drawing.Point(16, 39);
            this.dgvPagamentosPendentes.Name = "dgvPagamentosPendentes";
            this.dgvPagamentosPendentes.ReadOnly = true;
            this.dgvPagamentosPendentes.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvPagamentosPendentes.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPagamentosPendentes.RowTemplate.Height = 24;
            this.dgvPagamentosPendentes.Size = new System.Drawing.Size(1322, 495);
            this.dgvPagamentosPendentes.TabIndex = 70;
            this.dgvPagamentosPendentes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPagamentosPendentes_CellClick);
            // 
            // IdVenda
            // 
            this.IdVenda.HeaderText = "IdVenda";
            this.IdVenda.MinimumWidth = 6;
            this.IdVenda.Name = "IdVenda";
            this.IdVenda.ReadOnly = true;
            this.IdVenda.Width = 125;
            // 
            // Frotista
            // 
            this.Frotista.HeaderText = "Frotista";
            this.Frotista.MinimumWidth = 6;
            this.Frotista.Name = "Frotista";
            this.Frotista.ReadOnly = true;
            this.Frotista.Width = 125;
            // 
            // Cliente
            // 
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.MinimumWidth = 6;
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Cliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cliente.Width = 125;
            // 
            // CPFCNPJ
            // 
            this.CPFCNPJ.HeaderText = "CPFCNPJ";
            this.CPFCNPJ.MinimumWidth = 6;
            this.CPFCNPJ.Name = "CPFCNPJ";
            this.CPFCNPJ.ReadOnly = true;
            this.CPFCNPJ.Width = 125;
            // 
            // Carro
            // 
            this.Carro.HeaderText = "Carro";
            this.Carro.MinimumWidth = 6;
            this.Carro.Name = "Carro";
            this.Carro.ReadOnly = true;
            this.Carro.Width = 125;
            // 
            // Placa
            // 
            this.Placa.HeaderText = "Placa";
            this.Placa.MinimumWidth = 6;
            this.Placa.Name = "Placa";
            this.Placa.ReadOnly = true;
            this.Placa.Width = 125;
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.MinimumWidth = 6;
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Width = 125;
            // 
            // pago
            // 
            this.pago.HeaderText = "pago";
            this.pago.MinimumWidth = 6;
            this.pago.Name = "pago";
            this.pago.ReadOnly = true;
            this.pago.Width = 125;
            // 
            // preco
            // 
            this.preco.HeaderText = "preco";
            this.preco.MinimumWidth = 6;
            this.preco.Name = "preco";
            this.preco.ReadOnly = true;
            this.preco.Width = 125;
            // 
            // ValorACobrar
            // 
            this.ValorACobrar.HeaderText = "Valor A Cobrar";
            this.ValorACobrar.MinimumWidth = 6;
            this.ValorACobrar.Name = "ValorACobrar";
            this.ValorACobrar.ReadOnly = true;
            this.ValorACobrar.Width = 125;
            // 
            // FormaPagamento
            // 
            this.FormaPagamento.HeaderText = "FormaPagamento";
            this.FormaPagamento.MinimumWidth = 6;
            this.FormaPagamento.Name = "FormaPagamento";
            this.FormaPagamento.ReadOnly = true;
            this.FormaPagamento.Width = 125;
            // 
            // btnPagamentoRealizado
            // 
            this.btnPagamentoRealizado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnPagamentoRealizado.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPagamentoRealizado.FlatAppearance.BorderSize = 0;
            this.btnPagamentoRealizado.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPagamentoRealizado.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnPagamentoRealizado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagamentoRealizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagamentoRealizado.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPagamentoRealizado.Image = ((System.Drawing.Image)(resources.GetObject("btnPagamentoRealizado.Image")));
            this.btnPagamentoRealizado.Location = new System.Drawing.Point(623, 101);
            this.btnPagamentoRealizado.Margin = new System.Windows.Forms.Padding(4);
            this.btnPagamentoRealizado.Name = "btnPagamentoRealizado";
            this.btnPagamentoRealizado.Size = new System.Drawing.Size(238, 56);
            this.btnPagamentoRealizado.TabIndex = 160;
            this.btnPagamentoRealizado.Text = "   Pagamento Realizado";
            this.btnPagamentoRealizado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPagamentoRealizado.UseVisualStyleBackColor = false;
            this.btnPagamentoRealizado.Click += new System.EventHandler(this.btnPagamentoRealizado_Click);
            // 
            // bunifuSeparator6
            // 
            this.bunifuSeparator6.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bunifuSeparator6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuSeparator6.LineThickness = 1;
            this.bunifuSeparator6.Location = new System.Drawing.Point(407, 81);
            this.bunifuSeparator6.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bunifuSeparator6.Name = "bunifuSeparator6";
            this.bunifuSeparator6.Size = new System.Drawing.Size(574, 10);
            this.bunifuSeparator6.TabIndex = 159;
            this.bunifuSeparator6.Transparency = 255;
            this.bunifuSeparator6.Vertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(429, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(525, 52);
            this.label1.TabIndex = 158;
            this.label1.Text = "Pagamentos Pendentes";
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnVoltar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnVoltar.FlatAppearance.BorderSize = 0;
            this.btnVoltar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnVoltar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnVoltar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnVoltar.Image = ((System.Drawing.Image)(resources.GetObject("btnVoltar.Image")));
            this.btnVoltar.Location = new System.Drawing.Point(19, 12);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(190, 58);
            this.btnVoltar.TabIndex = 157;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(869, 101);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(238, 56);
            this.btnCancelar.TabIndex = 163;
            this.btnCancelar.Text = "   Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cmbCliente
            // 
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(126, 119);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(208, 24);
            this.cmbCliente.TabIndex = 165;
            // 
            // lblServico1
            // 
            this.lblServico1.AutoSize = true;
            this.lblServico1.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServico1.ForeColor = System.Drawing.Color.White;
            this.lblServico1.Location = new System.Drawing.Point(124, 92);
            this.lblServico1.Name = "lblServico1";
            this.lblServico1.Size = new System.Drawing.Size(85, 23);
            this.lblServico1.TabIndex = 164;
            this.lblServico1.Text = "Cliente:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbTransferencia);
            this.groupBox1.Controls.Add(this.rdbDebito);
            this.groupBox1.Controls.Add(this.rdbPermuta);
            this.groupBox1.Controls.Add(this.rdbCredito);
            this.groupBox1.Controls.Add(this.rdbBoleto);
            this.groupBox1.Controls.Add(this.rdbDinheiro);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(1114, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 146);
            this.groupBox1.TabIndex = 209;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forma de Pagamento";
            // 
            // rdbTransferencia
            // 
            this.rdbTransferencia.AutoSize = true;
            this.rdbTransferencia.Location = new System.Drawing.Point(118, 108);
            this.rdbTransferencia.Name = "rdbTransferencia";
            this.rdbTransferencia.Size = new System.Drawing.Size(146, 28);
            this.rdbTransferencia.TabIndex = 210;
            this.rdbTransferencia.Text = "Transferência";
            this.rdbTransferencia.UseVisualStyleBackColor = true;
            // 
            // rdbDebito
            // 
            this.rdbDebito.AutoSize = true;
            this.rdbDebito.Location = new System.Drawing.Point(125, 33);
            this.rdbDebito.Name = "rdbDebito";
            this.rdbDebito.Size = new System.Drawing.Size(85, 28);
            this.rdbDebito.TabIndex = 4;
            this.rdbDebito.Text = "Débito";
            this.rdbDebito.UseVisualStyleBackColor = true;
            // 
            // rdbPermuta
            // 
            this.rdbPermuta.AutoSize = true;
            this.rdbPermuta.Location = new System.Drawing.Point(125, 67);
            this.rdbPermuta.Name = "rdbPermuta";
            this.rdbPermuta.Size = new System.Drawing.Size(101, 28);
            this.rdbPermuta.TabIndex = 3;
            this.rdbPermuta.Text = "Permuta";
            this.rdbPermuta.UseVisualStyleBackColor = true;
            // 
            // rdbCredito
            // 
            this.rdbCredito.AutoSize = true;
            this.rdbCredito.Location = new System.Drawing.Point(21, 108);
            this.rdbCredito.Name = "rdbCredito";
            this.rdbCredito.Size = new System.Drawing.Size(91, 28);
            this.rdbCredito.TabIndex = 2;
            this.rdbCredito.Text = "Crédito";
            this.rdbCredito.UseVisualStyleBackColor = true;
            // 
            // rdbBoleto
            // 
            this.rdbBoleto.AutoSize = true;
            this.rdbBoleto.Location = new System.Drawing.Point(21, 68);
            this.rdbBoleto.Name = "rdbBoleto";
            this.rdbBoleto.Size = new System.Drawing.Size(84, 28);
            this.rdbBoleto.TabIndex = 1;
            this.rdbBoleto.Text = "Boleto";
            this.rdbBoleto.UseVisualStyleBackColor = true;
            // 
            // rdbDinheiro
            // 
            this.rdbDinheiro.AutoSize = true;
            this.rdbDinheiro.Checked = true;
            this.rdbDinheiro.Location = new System.Drawing.Point(21, 33);
            this.rdbDinheiro.Name = "rdbDinheiro";
            this.rdbDinheiro.Size = new System.Drawing.Size(102, 28);
            this.rdbDinheiro.TabIndex = 0;
            this.rdbDinheiro.TabStop = true;
            this.rdbDinheiro.Text = "Dinheiro";
            this.rdbDinheiro.UseVisualStyleBackColor = true;
            // 
            // PendenciaPCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1394, 727);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbCliente);
            this.Controls.Add(this.lblServico1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnPagamentoRealizado);
            this.Controls.Add(this.bunifuSeparator6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVoltar);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PendenciaPCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pendencias Por Cliente";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PendenciaPCliente_FormClosed);
            this.Load += new System.EventHandler(this.PendenciaPCliente_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagamentosPendentes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPagamentosPendentes;
        private System.Windows.Forms.Button btnPagamentoRealizado;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblServico1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbDebito;
        private System.Windows.Forms.RadioButton rdbPermuta;
        private System.Windows.Forms.RadioButton rdbCredito;
        private System.Windows.Forms.RadioButton rdbBoleto;
        private System.Windows.Forms.RadioButton rdbDinheiro;
        private System.Windows.Forms.RadioButton rdbTransferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdVenda;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Frotista;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPFCNPJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Carro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Placa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewCheckBoxColumn pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn preco;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValorACobrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormaPagamento;
    }
}