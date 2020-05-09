namespace totalClean
{
    partial class RelatorioGastos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelatorioGastos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnGerarRelatorio = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.bunifuSeparator6 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSetor = new System.Windows.Forms.ComboBox();
            this.lblServico1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbOrdenarPData = new System.Windows.Forms.RadioButton();
            this.rdbOdrdenarPId = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvGastos = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.dtFinalVencimento = new System.Windows.Forms.DateTimePicker();
            this.DtInicialVencimento = new System.Windows.Forms.DateTimePicker();
            this.btnGeraPdf = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbAmbas = new System.Windows.Forms.RadioButton();
            this.rdbAberto = new System.Windows.Forms.RadioButton();
            this.rdbPago = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnSair.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSair.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSair.Image = ((System.Drawing.Image)(resources.GetObject("btnSair.Image")));
            this.btnSair.Location = new System.Drawing.Point(15, 14);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(190, 56);
            this.btnSair.TabIndex = 174;
            this.btnSair.Text = "   Voltar";
            this.btnSair.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnGerarRelatorio
            // 
            this.btnGerarRelatorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnGerarRelatorio.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGerarRelatorio.FlatAppearance.BorderSize = 0;
            this.btnGerarRelatorio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnGerarRelatorio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnGerarRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGerarRelatorio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGerarRelatorio.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGerarRelatorio.Image = ((System.Drawing.Image)(resources.GetObject("btnGerarRelatorio.Image")));
            this.btnGerarRelatorio.Location = new System.Drawing.Point(669, 97);
            this.btnGerarRelatorio.Margin = new System.Windows.Forms.Padding(4);
            this.btnGerarRelatorio.Name = "btnGerarRelatorio";
            this.btnGerarRelatorio.Size = new System.Drawing.Size(264, 56);
            this.btnGerarRelatorio.TabIndex = 173;
            this.btnGerarRelatorio.Text = "  Gerar Relatório ";
            this.btnGerarRelatorio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGerarRelatorio.UseVisualStyleBackColor = false;
            this.btnGerarRelatorio.Click += new System.EventHandler(this.btnGerarRelatorio_Click);
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
            this.btnCancelar.Location = new System.Drawing.Point(397, 97);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(264, 56);
            this.btnCancelar.TabIndex = 172;
            this.btnCancelar.Text = "   Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
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
            this.btnPesquisar.Location = new System.Drawing.Point(125, 97);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(264, 56);
            this.btnPesquisar.TabIndex = 171;
            this.btnPesquisar.Text = "   Pesquisar";
            this.btnPesquisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // bunifuSeparator6
            // 
            this.bunifuSeparator6.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bunifuSeparator6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuSeparator6.LineThickness = 1;
            this.bunifuSeparator6.Location = new System.Drawing.Point(418, 83);
            this.bunifuSeparator6.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bunifuSeparator6.Name = "bunifuSeparator6";
            this.bunifuSeparator6.Size = new System.Drawing.Size(495, 10);
            this.bunifuSeparator6.TabIndex = 170;
            this.bunifuSeparator6.Transparency = 255;
            this.bunifuSeparator6.Vertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(442, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(444, 52);
            this.label1.TabIndex = 169;
            this.label1.Text = "Relatório de Gastos";
            // 
            // cmbSetor
            // 
            this.cmbSetor.FormattingEnabled = true;
            this.cmbSetor.Location = new System.Drawing.Point(507, 201);
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.Size = new System.Drawing.Size(208, 24);
            this.cmbSetor.TabIndex = 190;
            // 
            // lblServico1
            // 
            this.lblServico1.AutoSize = true;
            this.lblServico1.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServico1.ForeColor = System.Drawing.Color.White;
            this.lblServico1.Location = new System.Drawing.Point(503, 174);
            this.lblServico1.Name = "lblServico1";
            this.lblServico1.Size = new System.Drawing.Size(70, 23);
            this.lblServico1.TabIndex = 189;
            this.lblServico1.Text = "Setor:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbOrdenarPData);
            this.groupBox2.Controls.Add(this.rdbOdrdenarPId);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dgvGastos);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(25, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1349, 460);
            this.groupBox2.TabIndex = 194;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gastos";
            // 
            // rdbOrdenarPData
            // 
            this.rdbOrdenarPData.AutoSize = true;
            this.rdbOrdenarPData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbOrdenarPData.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbOrdenarPData.Location = new System.Drawing.Point(218, 424);
            this.rdbOrdenarPData.Name = "rdbOrdenarPData";
            this.rdbOrdenarPData.Size = new System.Drawing.Size(68, 28);
            this.rdbOrdenarPData.TabIndex = 174;
            this.rdbOrdenarPData.Text = "Data";
            this.rdbOrdenarPData.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.rdbOrdenarPData.UseVisualStyleBackColor = true;
            this.rdbOrdenarPData.CheckedChanged += new System.EventHandler(this.rdbOrdenarPData_CheckedChanged);
            // 
            // rdbOdrdenarPId
            // 
            this.rdbOdrdenarPId.AutoSize = true;
            this.rdbOdrdenarPId.Checked = true;
            this.rdbOdrdenarPId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbOdrdenarPId.ForeColor = System.Drawing.Color.White;
            this.rdbOdrdenarPId.Location = new System.Drawing.Point(166, 424);
            this.rdbOdrdenarPId.Name = "rdbOdrdenarPId";
            this.rdbOdrdenarPId.Size = new System.Drawing.Size(46, 28);
            this.rdbOdrdenarPId.TabIndex = 173;
            this.rdbOdrdenarPId.TabStop = true;
            this.rdbOdrdenarPId.Text = "Id";
            this.rdbOdrdenarPId.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(25, 427);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 23);
            this.label3.TabIndex = 172;
            this.label3.Text = "Ordenar por:";
            // 
            // dgvGastos
            // 
            this.dgvGastos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.dgvGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGastos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.dgvGastos.Location = new System.Drawing.Point(13, 36);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.ReadOnly = true;
            this.dgvGastos.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvGastos.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGastos.RowTemplate.Height = 24;
            this.dgvGastos.Size = new System.Drawing.Size(1322, 382);
            this.dgvGastos.TabIndex = 164;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(836, 169);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(271, 23);
            this.label7.TabIndex = 171;
            this.label7.Text = "Data De Vencimento Entre:";
            // 
            // dtFinalVencimento
            // 
            this.dtFinalVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFinalVencimento.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dtFinalVencimento.Location = new System.Drawing.Point(985, 203);
            this.dtFinalVencimento.Name = "dtFinalVencimento";
            this.dtFinalVencimento.Size = new System.Drawing.Size(220, 22);
            this.dtFinalVencimento.TabIndex = 170;
            // 
            // DtInicialVencimento
            // 
            this.DtInicialVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtInicialVencimento.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DtInicialVencimento.Location = new System.Drawing.Point(744, 203);
            this.DtInicialVencimento.Name = "DtInicialVencimento";
            this.DtInicialVencimento.Size = new System.Drawing.Size(220, 22);
            this.DtInicialVencimento.TabIndex = 168;
            // 
            // btnGeraPdf
            // 
            this.btnGeraPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnGeraPdf.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnGeraPdf.FlatAppearance.BorderSize = 0;
            this.btnGeraPdf.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnGeraPdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnGeraPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeraPdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGeraPdf.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGeraPdf.Image = ((System.Drawing.Image)(resources.GetObject("btnGeraPdf.Image")));
            this.btnGeraPdf.Location = new System.Drawing.Point(941, 97);
            this.btnGeraPdf.Margin = new System.Windows.Forms.Padding(4);
            this.btnGeraPdf.Name = "btnGeraPdf";
            this.btnGeraPdf.Size = new System.Drawing.Size(264, 56);
            this.btnGeraPdf.TabIndex = 195;
            this.btnGeraPdf.Text = "  Gerar PDF";
            this.btnGeraPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGeraPdf.UseVisualStyleBackColor = false;
            this.btnGeraPdf.Click += new System.EventHandler(this.btnGeraPdf_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbAmbas);
            this.groupBox1.Controls.Add(this.rdbAberto);
            this.groupBox1.Controls.Add(this.rdbPago);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(92, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 64);
            this.groupBox1.TabIndex = 196;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Situação";
            // 
            // rdbAmbas
            // 
            this.rdbAmbas.AutoSize = true;
            this.rdbAmbas.Checked = true;
            this.rdbAmbas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbAmbas.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbAmbas.Location = new System.Drawing.Point(226, 26);
            this.rdbAmbas.Name = "rdbAmbas";
            this.rdbAmbas.Size = new System.Drawing.Size(90, 28);
            this.rdbAmbas.TabIndex = 164;
            this.rdbAmbas.TabStop = true;
            this.rdbAmbas.Text = "Ambas";
            this.rdbAmbas.UseVisualStyleBackColor = true;
            // 
            // rdbAberto
            // 
            this.rdbAberto.AutoSize = true;
            this.rdbAberto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbAberto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbAberto.Location = new System.Drawing.Point(99, 26);
            this.rdbAberto.Name = "rdbAberto";
            this.rdbAberto.Size = new System.Drawing.Size(121, 28);
            this.rdbAberto.TabIndex = 163;
            this.rdbAberto.Text = "Em Aberto";
            this.rdbAberto.UseVisualStyleBackColor = true;
            // 
            // rdbPago
            // 
            this.rdbPago.AutoSize = true;
            this.rdbPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbPago.ForeColor = System.Drawing.Color.White;
            this.rdbPago.Location = new System.Drawing.Point(18, 26);
            this.rdbPago.Name = "rdbPago";
            this.rdbPago.Size = new System.Drawing.Size(75, 28);
            this.rdbPago.TabIndex = 162;
            this.rdbPago.Text = "Pago";
            this.rdbPago.UseVisualStyleBackColor = true;
            // 
            // RelatorioGastos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1394, 727);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGeraPdf);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dtFinalVencimento);
            this.Controls.Add(this.cmbSetor);
            this.Controls.Add(this.DtInicialVencimento);
            this.Controls.Add(this.lblServico1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnGerarRelatorio);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.bunifuSeparator6);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RelatorioGastos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio de Gastos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RelatorioGastos_FormClosed);
            this.Load += new System.EventHandler(this.RelatorioGastos_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnGerarRelatorio;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnPesquisar;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.Label lblServico1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvGastos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtFinalVencimento;
        private System.Windows.Forms.DateTimePicker DtInicialVencimento;
        private System.Windows.Forms.RadioButton rdbOrdenarPData;
        private System.Windows.Forms.RadioButton rdbOdrdenarPId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGeraPdf;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbAmbas;
        private System.Windows.Forms.RadioButton rdbAberto;
        private System.Windows.Forms.RadioButton rdbPago;
    }
}