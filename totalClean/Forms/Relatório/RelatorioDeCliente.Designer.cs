namespace totalClean
{
    partial class RelatorioDeCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelatorioDeCliente));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnGerarRelatorio = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.bunifuSeparator6 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.bnfspNome = new Bunifu.Framework.UI.BunifuSeparator();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblCpf = new System.Windows.Forms.Label();
            this.bnfspCpf = new Bunifu.Framework.UI.BunifuSeparator();
            this.txtCpf = new System.Windows.Forms.TextBox();
            this.chkPesquisaEspecifica = new Bunifu.Framework.UI.BunifuCheckbox();
            this.label2 = new System.Windows.Forms.Label();
            this.gpboxCliente = new System.Windows.Forms.GroupBox();
            this.rdbAmbos = new System.Windows.Forms.RadioButton();
            this.rdbFrotistas = new System.Windows.Forms.RadioButton();
            this.rdbParticulares = new System.Windows.Forms.RadioButton();
            this.btnGeraPdf = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.gpboxCliente.SuspendLayout();
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
            this.btnSair.Location = new System.Drawing.Point(13, 13);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(190, 56);
            this.btnSair.TabIndex = 169;
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
            this.btnGerarRelatorio.Location = new System.Drawing.Point(701, 94);
            this.btnGerarRelatorio.Margin = new System.Windows.Forms.Padding(4);
            this.btnGerarRelatorio.Name = "btnGerarRelatorio";
            this.btnGerarRelatorio.Size = new System.Drawing.Size(264, 56);
            this.btnGerarRelatorio.TabIndex = 168;
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
            this.btnCancelar.Location = new System.Drawing.Point(429, 94);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(264, 56);
            this.btnCancelar.TabIndex = 167;
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
            this.btnPesquisar.Location = new System.Drawing.Point(157, 94);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(264, 56);
            this.btnPesquisar.TabIndex = 166;
            this.btnPesquisar.Text = "   Pesquisar";
            this.btnPesquisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvClientes);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(50, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1298, 435);
            this.groupBox1.TabIndex = 165;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados de Clientes";
            // 
            // dgvClientes
            // 
            this.dgvClientes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.dgvClientes.Location = new System.Drawing.Point(41, 38);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersWidth = 51;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvClientes.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClientes.RowTemplate.Height = 24;
            this.dgvClientes.Size = new System.Drawing.Size(1210, 379);
            this.dgvClientes.TabIndex = 70;
            // 
            // bunifuSeparator6
            // 
            this.bunifuSeparator6.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bunifuSeparator6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuSeparator6.LineThickness = 1;
            this.bunifuSeparator6.Location = new System.Drawing.Point(452, 75);
            this.bunifuSeparator6.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bunifuSeparator6.Name = "bunifuSeparator6";
            this.bunifuSeparator6.Size = new System.Drawing.Size(501, 10);
            this.bunifuSeparator6.TabIndex = 156;
            this.bunifuSeparator6.Transparency = 255;
            this.bunifuSeparator6.Vertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(471, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(470, 52);
            this.label1.TabIndex = 155;
            this.label1.Text = "Relatório de Clientes";
            // 
            // bnfspNome
            // 
            this.bnfspNome.BackColor = System.Drawing.Color.Transparent;
            this.bnfspNome.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bnfspNome.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bnfspNome.LineThickness = 1;
            this.bnfspNome.Location = new System.Drawing.Point(354, 250);
            this.bnfspNome.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bnfspNome.Name = "bnfspNome";
            this.bnfspNome.Size = new System.Drawing.Size(315, 10);
            this.bnfspNome.TabIndex = 172;
            this.bnfspNome.Transparency = 255;
            this.bnfspNome.Vertical = false;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.ForeColor = System.Drawing.Color.White;
            this.lblNome.Location = new System.Drawing.Point(350, 197);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(74, 23);
            this.lblNome.TabIndex = 171;
            this.lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            this.txtNome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.txtNome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNome.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNome.ForeColor = System.Drawing.SystemColors.Info;
            this.txtNome.HideSelection = false;
            this.txtNome.Location = new System.Drawing.Point(354, 224);
            this.txtNome.Margin = new System.Windows.Forms.Padding(4);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(315, 25);
            this.txtNome.TabIndex = 170;
            // 
            // lblCpf
            // 
            this.lblCpf.AutoSize = true;
            this.lblCpf.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCpf.ForeColor = System.Drawing.Color.White;
            this.lblCpf.Location = new System.Drawing.Point(696, 197);
            this.lblCpf.Name = "lblCpf";
            this.lblCpf.Size = new System.Drawing.Size(133, 23);
            this.lblCpf.TabIndex = 175;
            this.lblCpf.Text = "Cpf ou Cnpj:";
            // 
            // bnfspCpf
            // 
            this.bnfspCpf.BackColor = System.Drawing.Color.Transparent;
            this.bnfspCpf.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bnfspCpf.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bnfspCpf.LineThickness = 1;
            this.bnfspCpf.Location = new System.Drawing.Point(700, 250);
            this.bnfspCpf.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bnfspCpf.Name = "bnfspCpf";
            this.bnfspCpf.Size = new System.Drawing.Size(315, 10);
            this.bnfspCpf.TabIndex = 174;
            this.bnfspCpf.Transparency = 255;
            this.bnfspCpf.Vertical = false;
            // 
            // txtCpf
            // 
            this.txtCpf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.txtCpf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCpf.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCpf.ForeColor = System.Drawing.SystemColors.Info;
            this.txtCpf.HideSelection = false;
            this.txtCpf.Location = new System.Drawing.Point(700, 224);
            this.txtCpf.Margin = new System.Windows.Forms.Padding(4);
            this.txtCpf.Name = "txtCpf";
            this.txtCpf.Size = new System.Drawing.Size(315, 25);
            this.txtCpf.TabIndex = 173;
            // 
            // chkPesquisaEspecifica
            // 
            this.chkPesquisaEspecifica.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(205)))), ((int)(((byte)(117)))));
            this.chkPesquisaEspecifica.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(135)))), ((int)(((byte)(140)))));
            this.chkPesquisaEspecifica.Checked = true;
            this.chkPesquisaEspecifica.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(205)))), ((int)(((byte)(117)))));
            this.chkPesquisaEspecifica.ForeColor = System.Drawing.Color.White;
            this.chkPesquisaEspecifica.Location = new System.Drawing.Point(79, 220);
            this.chkPesquisaEspecifica.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPesquisaEspecifica.Name = "chkPesquisaEspecifica";
            this.chkPesquisaEspecifica.Size = new System.Drawing.Size(20, 20);
            this.chkPesquisaEspecifica.TabIndex = 189;
            this.chkPesquisaEspecifica.OnChange += new System.EventHandler(this.chkPesquisaEspecifica_OnChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(106, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 23);
            this.label2.TabIndex = 190;
            this.label2.Text = "Pesquisa específica";
            // 
            // gpboxCliente
            // 
            this.gpboxCliente.Controls.Add(this.rdbAmbos);
            this.gpboxCliente.Controls.Add(this.rdbFrotistas);
            this.gpboxCliente.Controls.Add(this.rdbParticulares);
            this.gpboxCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.gpboxCliente.ForeColor = System.Drawing.Color.White;
            this.gpboxCliente.Location = new System.Drawing.Point(488, 194);
            this.gpboxCliente.Name = "gpboxCliente";
            this.gpboxCliente.Size = new System.Drawing.Size(353, 64);
            this.gpboxCliente.TabIndex = 197;
            this.gpboxCliente.TabStop = false;
            this.gpboxCliente.Text = "Tipo de cliente";
            // 
            // rdbAmbos
            // 
            this.rdbAmbos.AutoSize = true;
            this.rdbAmbos.Checked = true;
            this.rdbAmbos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbAmbos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbAmbos.Location = new System.Drawing.Point(250, 27);
            this.rdbAmbos.Name = "rdbAmbos";
            this.rdbAmbos.Size = new System.Drawing.Size(85, 28);
            this.rdbAmbos.TabIndex = 164;
            this.rdbAmbos.TabStop = true;
            this.rdbAmbos.Text = "Todos";
            this.rdbAmbos.UseVisualStyleBackColor = true;
            // 
            // rdbFrotistas
            // 
            this.rdbFrotistas.AutoSize = true;
            this.rdbFrotistas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbFrotistas.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbFrotistas.Location = new System.Drawing.Point(144, 27);
            this.rdbFrotistas.Name = "rdbFrotistas";
            this.rdbFrotistas.Size = new System.Drawing.Size(100, 28);
            this.rdbFrotistas.TabIndex = 163;
            this.rdbFrotistas.Text = "Frotistas";
            this.rdbFrotistas.UseVisualStyleBackColor = true;
            // 
            // rdbParticulares
            // 
            this.rdbParticulares.AutoSize = true;
            this.rdbParticulares.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.rdbParticulares.ForeColor = System.Drawing.Color.White;
            this.rdbParticulares.Location = new System.Drawing.Point(10, 26);
            this.rdbParticulares.Name = "rdbParticulares";
            this.rdbParticulares.Size = new System.Drawing.Size(128, 28);
            this.rdbParticulares.TabIndex = 162;
            this.rdbParticulares.Text = "Particulares";
            this.rdbParticulares.UseVisualStyleBackColor = true;
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
            this.btnGeraPdf.Location = new System.Drawing.Point(973, 94);
            this.btnGeraPdf.Margin = new System.Windows.Forms.Padding(4);
            this.btnGeraPdf.Name = "btnGeraPdf";
            this.btnGeraPdf.Size = new System.Drawing.Size(264, 56);
            this.btnGeraPdf.TabIndex = 198;
            this.btnGeraPdf.Text = "  Gerar PDF";
            this.btnGeraPdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGeraPdf.UseVisualStyleBackColor = false;
            this.btnGeraPdf.Click += new System.EventHandler(this.btnGeraPdf_Click);
            // 
            // RelatorioDeCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1394, 727);
            this.Controls.Add(this.btnGeraPdf);
            this.Controls.Add(this.gpboxCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkPesquisaEspecifica);
            this.Controls.Add(this.lblCpf);
            this.Controls.Add(this.bnfspCpf);
            this.Controls.Add(this.txtCpf);
            this.Controls.Add(this.bnfspNome);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnGerarRelatorio);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bunifuSeparator6);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RelatorioDeCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio de Cliente";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RelatorioDeCliente_FormClosed);
            this.Load += new System.EventHandler(this.RelatorioDeCliente_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.gpboxCliente.ResumeLayout(false);
            this.gpboxCliente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnGerarRelatorio;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvClientes;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator6;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuSeparator bnfspNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblCpf;
        private Bunifu.Framework.UI.BunifuSeparator bnfspCpf;
        private System.Windows.Forms.TextBox txtCpf;
        private Bunifu.Framework.UI.BunifuCheckbox chkPesquisaEspecifica;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gpboxCliente;
        private System.Windows.Forms.RadioButton rdbAmbos;
        private System.Windows.Forms.RadioButton rdbFrotistas;
        private System.Windows.Forms.RadioButton rdbParticulares;
        private System.Windows.Forms.Button btnGeraPdf;
    }
}