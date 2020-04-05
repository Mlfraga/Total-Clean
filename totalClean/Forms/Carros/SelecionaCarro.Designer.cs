namespace totalClean
{
    partial class SelecionaCarro
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelecionaCarro));
            this.bunifuSeparator6 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.gpboxCliente = new System.Windows.Forms.GroupBox();
            this.dgvCarros = new System.Windows.Forms.DataGridView();
            this.btnCancelarSelecao = new System.Windows.Forms.Button();
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.txtBoxFlag = new System.Windows.Forms.TextBox();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnNova = new System.Windows.Forms.Button();
            this.btnAdiciona = new System.Windows.Forms.Button();
            this.bnfPlaca3 = new Bunifu.Framework.UI.BunifuSeparator();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.bnfCarro3 = new Bunifu.Framework.UI.BunifuSeparator();
            this.txtCarro = new System.Windows.Forms.TextBox();
            this.lblCarro = new System.Windows.Forms.Label();
            this.btnCancelarAdic = new System.Windows.Forms.Button();
            this.gpboxCliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarros)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuSeparator6
            // 
            this.bunifuSeparator6.BackColor = System.Drawing.Color.Transparent;
            this.bunifuSeparator6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bunifuSeparator6.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuSeparator6.LineThickness = 1;
            this.bunifuSeparator6.Location = new System.Drawing.Point(223, 67);
            this.bunifuSeparator6.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bunifuSeparator6.Name = "bunifuSeparator6";
            this.bunifuSeparator6.Size = new System.Drawing.Size(445, 10);
            this.bunifuSeparator6.TabIndex = 61;
            this.bunifuSeparator6.Transparency = 255;
            this.bunifuSeparator6.Vertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(249, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 52);
            this.label1.TabIndex = 60;
            this.label1.Text = "Selecionar Carro";
            // 
            // gpboxCliente
            // 
            this.gpboxCliente.Controls.Add(this.dgvCarros);
            this.gpboxCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpboxCliente.ForeColor = System.Drawing.Color.White;
            this.gpboxCliente.Location = new System.Drawing.Point(75, 250);
            this.gpboxCliente.Name = "gpboxCliente";
            this.gpboxCliente.Size = new System.Drawing.Size(742, 337);
            this.gpboxCliente.TabIndex = 202;
            this.gpboxCliente.TabStop = false;
            this.gpboxCliente.Text = "Carros do cliente";
            // 
            // dgvCarros
            // 
            this.dgvCarros.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.dgvCarros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCarros.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.dgvCarros.Location = new System.Drawing.Point(29, 34);
            this.dgvCarros.Name = "dgvCarros";
            this.dgvCarros.ReadOnly = true;
            this.dgvCarros.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvCarros.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCarros.RowTemplate.Height = 24;
            this.dgvCarros.Size = new System.Drawing.Size(693, 284);
            this.dgvCarros.TabIndex = 70;
            this.dgvCarros.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCarros_CellClick);
            // 
            // btnCancelarSelecao
            // 
            this.btnCancelarSelecao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnCancelarSelecao.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancelarSelecao.FlatAppearance.BorderSize = 0;
            this.btnCancelarSelecao.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancelarSelecao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCancelarSelecao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarSelecao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarSelecao.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancelarSelecao.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarSelecao.Image")));
            this.btnCancelarSelecao.Location = new System.Drawing.Point(547, 96);
            this.btnCancelarSelecao.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelarSelecao.Name = "btnCancelarSelecao";
            this.btnCancelarSelecao.Size = new System.Drawing.Size(217, 56);
            this.btnCancelarSelecao.TabIndex = 203;
            this.btnCancelarSelecao.Text = "   Cancelar";
            this.btnCancelarSelecao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelarSelecao.UseVisualStyleBackColor = false;
            this.btnCancelarSelecao.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnSelecionar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSelecionar.FlatAppearance.BorderSize = 0;
            this.btnSelecionar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSelecionar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnSelecionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelecionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelecionar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSelecionar.Image = ((System.Drawing.Image)(resources.GetObject("btnSelecionar.Image")));
            this.btnSelecionar.Location = new System.Drawing.Point(322, 96);
            this.btnSelecionar.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(217, 56);
            this.btnSelecionar.TabIndex = 204;
            this.btnSelecionar.Text = "   Selecionar";
            this.btnSelecionar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelecionar.UseVisualStyleBackColor = false;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // txtBoxFlag
            // 
            this.txtBoxFlag.Location = new System.Drawing.Point(739, 36);
            this.txtBoxFlag.Name = "txtBoxFlag";
            this.txtBoxFlag.Size = new System.Drawing.Size(100, 22);
            this.txtBoxFlag.TabIndex = 205;
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
            this.btnVoltar.Location = new System.Drawing.Point(13, 9);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(156, 58);
            this.btnVoltar.TabIndex = 206;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnNova
            // 
            this.btnNova.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnNova.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNova.FlatAppearance.BorderSize = 0;
            this.btnNova.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnNova.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnNova.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNova.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNova.ForeColor = System.Drawing.SystemColors.Control;
            this.btnNova.Image = ((System.Drawing.Image)(resources.GetObject("btnNova.Image")));
            this.btnNova.Location = new System.Drawing.Point(124, 96);
            this.btnNova.Margin = new System.Windows.Forms.Padding(4);
            this.btnNova.Name = "btnNova";
            this.btnNova.Size = new System.Drawing.Size(190, 56);
            this.btnNova.TabIndex = 207;
            this.btnNova.Text = "   Novo Carro";
            this.btnNova.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNova.UseVisualStyleBackColor = false;
            this.btnNova.Click += new System.EventHandler(this.btnNova_Click);
            // 
            // btnAdiciona
            // 
            this.btnAdiciona.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnAdiciona.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnAdiciona.FlatAppearance.BorderSize = 0;
            this.btnAdiciona.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAdiciona.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnAdiciona.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdiciona.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdiciona.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAdiciona.Image = ((System.Drawing.Image)(resources.GetObject("btnAdiciona.Image")));
            this.btnAdiciona.Location = new System.Drawing.Point(322, 96);
            this.btnAdiciona.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdiciona.Name = "btnAdiciona";
            this.btnAdiciona.Size = new System.Drawing.Size(190, 56);
            this.btnAdiciona.TabIndex = 209;
            this.btnAdiciona.Text = "   Adicionar";
            this.btnAdiciona.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdiciona.UseVisualStyleBackColor = false;
            this.btnAdiciona.Click += new System.EventHandler(this.btnAdiciona_Click);
            // 
            // bnfPlaca3
            // 
            this.bnfPlaca3.BackColor = System.Drawing.Color.Transparent;
            this.bnfPlaca3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bnfPlaca3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bnfPlaca3.LineThickness = 1;
            this.bnfPlaca3.Location = new System.Drawing.Point(474, 215);
            this.bnfPlaca3.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bnfPlaca3.Name = "bnfPlaca3";
            this.bnfPlaca3.Size = new System.Drawing.Size(284, 10);
            this.bnfPlaca3.TabIndex = 215;
            this.bnfPlaca3.Transparency = 255;
            this.bnfPlaca3.Vertical = false;
            // 
            // txtPlaca
            // 
            this.txtPlaca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.txtPlaca.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlaca.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaca.ForeColor = System.Drawing.SystemColors.Info;
            this.txtPlaca.HideSelection = false;
            this.txtPlaca.Location = new System.Drawing.Point(474, 196);
            this.txtPlaca.Margin = new System.Windows.Forms.Padding(4);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(280, 25);
            this.txtPlaca.TabIndex = 214;
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaca.ForeColor = System.Drawing.Color.White;
            this.lblPlaca.Location = new System.Drawing.Point(474, 169);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(68, 23);
            this.lblPlaca.TabIndex = 213;
            this.lblPlaca.Text = "Placa:";
            // 
            // bnfCarro3
            // 
            this.bnfCarro3.BackColor = System.Drawing.Color.Transparent;
            this.bnfCarro3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bnfCarro3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bnfCarro3.LineThickness = 1;
            this.bnfCarro3.Location = new System.Drawing.Point(162, 215);
            this.bnfCarro3.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bnfCarro3.Name = "bnfCarro3";
            this.bnfCarro3.Size = new System.Drawing.Size(266, 10);
            this.bnfCarro3.TabIndex = 212;
            this.bnfCarro3.Transparency = 255;
            this.bnfCarro3.Vertical = false;
            // 
            // txtCarro
            // 
            this.txtCarro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.txtCarro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCarro.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCarro.ForeColor = System.Drawing.SystemColors.Info;
            this.txtCarro.HideSelection = false;
            this.txtCarro.Location = new System.Drawing.Point(162, 196);
            this.txtCarro.Margin = new System.Windows.Forms.Padding(4);
            this.txtCarro.Name = "txtCarro";
            this.txtCarro.Size = new System.Drawing.Size(266, 25);
            this.txtCarro.TabIndex = 211;
            // 
            // lblCarro
            // 
            this.lblCarro.AutoSize = true;
            this.lblCarro.Font = new System.Drawing.Font("Verdana", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarro.ForeColor = System.Drawing.Color.White;
            this.lblCarro.Location = new System.Drawing.Point(158, 169);
            this.lblCarro.Name = "lblCarro";
            this.lblCarro.Size = new System.Drawing.Size(71, 23);
            this.lblCarro.TabIndex = 210;
            this.lblCarro.Text = "Carro:";
            // 
            // btnCancelarAdic
            // 
            this.btnCancelarAdic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnCancelarAdic.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancelarAdic.FlatAppearance.BorderSize = 0;
            this.btnCancelarAdic.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnCancelarAdic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCancelarAdic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarAdic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarAdic.ForeColor = System.Drawing.SystemColors.Control;
            this.btnCancelarAdic.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarAdic.Image")));
            this.btnCancelarAdic.Location = new System.Drawing.Point(547, 96);
            this.btnCancelarAdic.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelarAdic.Name = "btnCancelarAdic";
            this.btnCancelarAdic.Size = new System.Drawing.Size(217, 56);
            this.btnCancelarAdic.TabIndex = 216;
            this.btnCancelarAdic.Text = "   Cancelar";
            this.btnCancelarAdic.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelarAdic.UseVisualStyleBackColor = false;
            this.btnCancelarAdic.Click += new System.EventHandler(this.btnCancelarAdic_Click);
            // 
            // SelecionaCarro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(904, 610);
            this.Controls.Add(this.btnCancelarAdic);
            this.Controls.Add(this.bnfPlaca3);
            this.Controls.Add(this.txtPlaca);
            this.Controls.Add(this.lblPlaca);
            this.Controls.Add(this.bnfCarro3);
            this.Controls.Add(this.txtCarro);
            this.Controls.Add(this.lblCarro);
            this.Controls.Add(this.btnAdiciona);
            this.Controls.Add(this.btnNova);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.txtBoxFlag);
            this.Controls.Add(this.btnSelecionar);
            this.Controls.Add(this.btnCancelarSelecao);
            this.Controls.Add(this.gpboxCliente);
            this.Controls.Add(this.bunifuSeparator6);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelecionaCarro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecionar Carro";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelecionaCarro_FormClosed);
            this.Load += new System.EventHandler(this.SelecionaCarro_Load);
            this.gpboxCliente.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCarros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gpboxCliente;
        private System.Windows.Forms.DataGridView dgvCarros;
        private System.Windows.Forms.Button btnCancelarSelecao;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.TextBox txtBoxFlag;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnNova;
        private System.Windows.Forms.Button btnAdiciona;
        private Bunifu.Framework.UI.BunifuSeparator bnfPlaca3;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label lblPlaca;
        private Bunifu.Framework.UI.BunifuSeparator bnfCarro3;
        private System.Windows.Forms.TextBox txtCarro;
        private System.Windows.Forms.Label lblCarro;
        private System.Windows.Forms.Button btnCancelarAdic;
    }
}