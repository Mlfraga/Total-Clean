using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;

namespace totalClean
{
    public partial class CadastroGastos : Form
    {
        Conexao con = new Conexao();
        public CadastroGastos()
        {
            InitializeComponent();
            menuStrip1.Renderer = new MyRenderer();
        }


        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.Salmon; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.Salmon; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.Salmon; }
            }
        }

        private void CadastroGastos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void bloqueaCampos()
        {
            txtValor.ReadOnly = true;
            txtDescricao.ReadOnly = true;
            cmbSetor.Enabled = false;
            DtGasto.Enabled = false;
            rdbBoleto.Enabled = false;
            rdbCartão.Enabled = false;
            rdbDinheiro.Enabled = false;
            rdbPermuta.Enabled = false;
            rdbPago.Enabled = false;
            rdbAberto.Enabled = false;

            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        private void desbloqueaCampos()
        {
            txtValor.ReadOnly = false;
            txtDescricao.ReadOnly = false;
            cmbSetor.Enabled = true;
            DtGasto.Enabled = true;
            rdbBoleto.Enabled = true;
            rdbCartão.Enabled = true;
            rdbDinheiro.Enabled = true;
            rdbPermuta.Enabled = true;
            rdbPago.Enabled = true;
            rdbAberto.Enabled = true;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        private void limpaCampos()
        {
            cmbSetor.Text = "";
            txtDescricao.Text = "";
            txtValor.Text = "";
            DtGasto.Value = DateTime.Now;
            rdbPago.Checked = true;
            rdbDinheiro.Checked = true;
        }

        private void CadastroGastos_Load(object sender, EventArgs e)
        {
            bloqueaCampos();
            preencheCmbSetor();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            desbloqueaCampos();
            btnNovo.Enabled = false;
            rdbDinheiro.Checked = true;
            rdbPago.Checked = true;
        }

        private void cadastrarSetorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadastroSetor n = new CadastroSetor();
            n.Show();
            this.Visible = false;
        }


        private void preencheCmbSetor()
        {
            List<Setor> listSetor = new List<Setor>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select * from SetorGastos order by nome ASC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Setor setor = new Setor();
                    setor.id = reader.GetInt32(0);
                    setor.nome = reader.GetString(1);

                    listSetor.Add(setor);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foi encontrado nenhum setor");
            }
            cmbSetor.DataSource = listSetor;
            cmbSetor.ValueMember = "id";
            cmbSetor.DisplayMember = "nome";
            cmbSetor.Text = "";
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            InicialFrm n = new InicialFrm();
            n.Show();
            this.Visible = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Conexao conexao = new Conexao(); 

            if (cmbSetor.Text == string.Empty && txtDescricao.Text == string.Empty && txtValor.Text == string.Empty)
            {
                MessageBox.Show("Por Favor insira os dados", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var choice = MessageBox.Show("Você deseja mesmo salvar esses dados", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice == DialogResult.No)
                {
                }
                else
                {

                    Gastos c = new Gastos();
                    c.id = int.Parse(cmbSetor.SelectedValue.ToString());
                    c.descricao = txtDescricao.Text;

                    if (rdbBoleto.Checked == true)
                    {
                        c.formaPagamento = "Boleto";
                    }
                    if (rdbDinheiro.Checked == true)
                    {
                        c.formaPagamento = "Dinheiro";
                    }
                    if (rdbPermuta.Checked == true)
                    {
                        c.formaPagamento = "Permuta";
                    }
                    if (rdbCartão.Checked == true)
                    {
                        c.formaPagamento = "Cartão";
                    }

                    c.dataVencimento = DtGasto.Value;
                    c.valor = Double.Parse(txtValor.Text);

                    if (rdbPago.Checked)
                    {
                        c.pago = true;
                    }
                    else
                    {
                        c.pago = false;
                    }
                    conexao.conectar();

                    int insere = conexao.executar($"INSERT INTO Gastos (idSetor, descricao, data, valor, formaPagamento, pago ) VALUES ('{c.id}','{c.descricao}','{c.dataVencimento}','{c.valor}','{c.formaPagamento}','{c.pago}')");

                    MessageBox.Show("Dados salvos com sucesso", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    bloqueaCampos();
                    limpaCampos();
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            ConsultaGastos n = new ConsultaGastos();
            n.Show();
            this.Visible = false;

        }
    }
}
