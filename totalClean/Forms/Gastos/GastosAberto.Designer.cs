namespace totalClean
{
    partial class GastosAberto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GastosAberto));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnPendenciasPSetor = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvGastos = new System.Windows.Forms.DataGridView();
            this.btnPagamentoRealizado = new System.Windows.Forms.Button();
            this.bunifuSeparator6 = new Bunifu.Framework.UI.BunifuSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPendenciasPSetor
            // 
            this.btnPendenciasPSetor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnPendenciasPSetor.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPendenciasPSetor.FlatAppearance.BorderSize = 0;
            this.btnPendenciasPSetor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPendenciasPSetor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnPendenciasPSetor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPendenciasPSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPendenciasPSetor.ForeColor = System.Drawing.SystemColors.Control;
            this.btnPendenciasPSetor.Image = ((System.Drawing.Image)(resources.GetObject("btnPendenciasPSetor.Image")));
            this.btnPendenciasPSetor.Location = new System.Drawing.Point(1109, 101);
            this.btnPendenciasPSetor.Margin = new System.Windows.Forms.Padding(4);
            this.btnPendenciasPSetor.Name = "btnPendenciasPSetor";
            this.btnPendenciasPSetor.Size = new System.Drawing.Size(264, 56);
            this.btnPendenciasPSetor.TabIndex = 162;
            this.btnPendenciasPSetor.Text = "   Filtrar Por Setor";
            this.btnPendenciasPSetor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPendenciasPSetor.UseVisualStyleBackColor = false;
            this.btnPendenciasPSetor.Click += new System.EventHandler(this.btnPendenciasPSetor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvGastos);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(24, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1349, 550);
            this.groupBox2.TabIndex = 161;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gastos Pendentes";
            // 
            // dgvGastos
            // 
            this.dgvGastos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.dgvGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGastos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.dgvGastos.Location = new System.Drawing.Point(13, 39);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvGastos.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGastos.RowTemplate.Height = 24;
            this.dgvGastos.Size = new System.Drawing.Size(1322, 495);
            this.dgvGastos.TabIndex = 163;
            this.dgvGastos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGastos_CellClick);
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
            this.btnPagamentoRealizado.Location = new System.Drawing.Point(571, 101);
            this.btnPagamentoRealizado.Margin = new System.Windows.Forms.Padding(4);
            this.btnPagamentoRealizado.Name = "btnPagamentoRealizado";
            this.btnPagamentoRealizado.Size = new System.Drawing.Size(264, 56);
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
            this.bunifuSeparator6.Location = new System.Drawing.Point(491, 81);
            this.bunifuSeparator6.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.bunifuSeparator6.Name = "bunifuSeparator6";
            this.bunifuSeparator6.Size = new System.Drawing.Size(467, 10);
            this.bunifuSeparator6.TabIndex = 159;
            this.bunifuSeparator6.Transparency = 255;
            this.bunifuSeparator6.Vertical = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(516, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(411, 52);
            this.label1.TabIndex = 158;
            this.label1.Text = "Gastos Em Aberto";
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
            this.btnVoltar.TabIndex = 157;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // GastosAberto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(1394, 727);
            this.Controls.Add(this.btnPendenciasPSetor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnPagamentoRealizado);
            this.Controls.Add(this.bunifuSeparator6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnVoltar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GastosAberto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gastos Em Aberto";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GastosAberto_FormClosed);
            this.Load += new System.EventHandler(this.GastosAberto_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPendenciasPSetor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPagamentoRealizado;
        private Bunifu.Framework.UI.BunifuSeparator bunifuSeparator6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.DataGridView dgvGastos;
    }
}