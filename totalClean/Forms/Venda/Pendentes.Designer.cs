namespace totalClean
{
    partial class Pendentes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pendentes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.bunifuSeparator6 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPagamentoRealizado = new System.Windows.Forms.Button();
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
            this.btnPendenciasPCliente = new System.Windows.Forms.Button();
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
            this.btnVoltar.Location = new System.Drawing.Point(13, 13);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(190, 58);
            this.btnVoltar.TabIndex = 46;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // bunifuSeparator6
            // 
            this.bunifuSeparator6.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bunifuSeparator6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuSeparator6.LineThickness = 1;
            this.bunifuSeparator6.Location = new System.Drawing.Point(488, 82);
            this.bunifuSeparator6.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bunifuSeparator6.Name = "bunifuSeparator6";
            this.bunifuSeparator6.Size = new System.Drawing.Size(467, 10);
            this.bunifuSeparator6.TabIndex = 73;
            this.bunifuSeparator6.Transparency = 255;
            this.bunifuSeparator6.Vertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(513, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(420, 52);
            this.label1.TabIndex = 72;
            this.label1.Text = "Vendas Em Aberto";
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
            this.btnPagamentoRealizado.Location = new System.Drawing.Point(771, 102);
            this.btnPagamentoRealizado.Margin = new System.Windows.Forms.Padding(4);
            this.btnPagamentoRealizado.Name = "btnPagamentoRealizado";
            this.btnPagamentoRealizado.Size = new System.Drawing.Size(264, 56);
            this.btnPagamentoRealizado.TabIndex = 153;
            this.btnPagamentoRealizado.Text = "   Pagamento Realizado";
            this.btnPagamentoRealizado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPagamentoRealizado.UseVisualStyleBackColor = false;
            this.btnPagamentoRealizado.Click += new System.EventHandler(this.btnPagamentoRealizado_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPagamentosPendentes);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(21, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1349, 550);
            this.groupBox2.TabIndex = 155;
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
            this.ValorACobrar.HeaderText = "Valor a cobrar";
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
            // btnPendenciasPCliente
            // 
            this.btnPendenciasPCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnPendenciasPCliente.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPendenciasPCliente.FlatAppearance.BorderSize = 0;
            this.btnPendenciasPCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPendenciasPCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnPendenciasPCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPendenciasPCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPendenciasPCliente.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPendenciasPCliente.Image = ((System.Drawing.Image)(resources.GetObject("btnPendenciasPCliente.Image")));
            this.btnPendenciasPCliente.Location = new System.Drawing.Point(1106, 102);
            this.btnPendenciasPCliente.Margin = new System.Windows.Forms.Padding(4);
            this.btnPendenciasPCliente.Name = "btnPendenciasPCliente";
            this.btnPendenciasPCliente.Size = new System.Drawing.Size(264, 56);
            this.btnPendenciasPCliente.TabIndex = 156;
            this.btnPendenciasPCliente.Text = "   Filtrar Por Cliente";
            this.btnPendenciasPCliente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPendenciasPCliente.UseVisualStyleBackColor = false;
            this.btnPendenciasPCliente.Click += new System.EventHandler(this.btnPendenciasPCliente_Click);
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
            this.groupBox1.Location = new System.Drawing.Point(13, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(739, 68);
            this.groupBox1.TabIndex = 208;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forma de Pagamento";
            // 
            // rdbTransferencia
            // 
            this.rdbTransferencia.AutoSize = true;
            this.rdbTransferencia.Location = new System.Drawing.Point(576, 27);
            this.rdbTransferencia.Name = "rdbTransferencia";
            this.rdbTransferencia.Size = new System.Drawing.Size(146, 28);
            this.rdbTransferencia.TabIndex = 209;
            this.rdbTransferencia.Text = "Transferência";
            this.rdbTransferencia.UseVisualStyleBackColor = true;
            // 
            // rdbDebito
            // 
            this.rdbDebito.AutoSize = true;
            this.rdbDebito.Location = new System.Drawing.Point(149, 27);
            this.rdbDebito.Name = "rdbDebito";
            this.rdbDebito.Size = new System.Drawing.Size(85, 28);
            this.rdbDebito.TabIndex = 4;
            this.rdbDebito.Text = "Débito";
            this.rdbDebito.UseVisualStyleBackColor = true;
            // 
            // rdbPermuta
            // 
            this.rdbPermuta.AutoSize = true;
            this.rdbPermuta.Location = new System.Drawing.Point(358, 27);
            this.rdbPermuta.Name = "rdbPermuta";
            this.rdbPermuta.Size = new System.Drawing.Size(101, 28);
            this.rdbPermuta.TabIndex = 3;
            this.rdbPermuta.Text = "Permuta";
            this.rdbPermuta.UseVisualStyleBackColor = true;
            // 
            // rdbCredito
            // 
            this.rdbCredito.AutoSize = true;
            this.rdbCredito.Location = new System.Drawing.Point(479, 27);
            this.rdbCredito.Name = "rdbCredito";
            this.rdbCredito.Size = new System.Drawing.Size(91, 28);
            this.rdbCredito.TabIndex = 2;
            this.rdbCredito.Text = "Crédito";
            this.rdbCredito.UseVisualStyleBackColor = true;
            // 
            // rdbBoleto
            // 
            this.rdbBoleto.AutoSize = true;
            this.rdbBoleto.Location = new System.Drawing.Point(253, 27);
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
            this.rdbDinheiro.Location = new System.Drawing.Point(24, 27);
            this.rdbDinheiro.Name = "rdbDinheiro";
            this.rdbDinheiro.Size = new System.Drawing.Size(102, 28);
            this.rdbDinheiro.TabIndex = 0;
            this.rdbDinheiro.TabStop = true;
            this.rdbDinheiro.Text = "Dinheiro";
            this.rdbDinheiro.UseVisualStyleBackColor = true;
            // 
            // Pendentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1394, 727);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPendenciasPCliente);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnPagamentoRealizado);
            this.Controls.Add(this.bunifuSeparator6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVoltar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pendentes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pendentes";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Pendentes_FormClosed);
            this.Load += new System.EventHandler(this.Pendentes_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagamentosPendentes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnVoltar;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPagamentoRealizado;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPagamentosPendentes;
        private System.Windows.Forms.Button btnPendenciasPCliente;
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