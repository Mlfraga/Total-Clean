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

namespace totalClean
{
    public partial class PendenciasPSetor : Form
    {
        public PendenciasPSetor()
        {
            InitializeComponent();
        }
        Conexao con = new Conexao();

        private void PendenciasPSetor_Load(object sender, EventArgs e)
        {
            iniciarGrid();
            preencheCmbSetor();
            btnPagamentoRealizado.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void iniciarGrid()
        {
            List<Gastos> listGastos = new List<Gastos>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[pago] = 0");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Gastos gasto = new Gastos();
                    gasto.id = reader.GetInt32(0);
                    gasto.nome = reader.GetString(1);
                    gasto.descricao = reader.GetString(2);
                    gasto.dataVencimento = reader.GetDateTime(3);
                    gasto.valor = reader.GetDouble(4);
                    gasto.formaPagamento = reader.GetString(5);
                    gasto.pago = reader.GetBoolean(6);

                    listGastos.Add(gasto);

                }
                reader.Close();
                dgvGastos.DataSource = null;
                dgvGastos.DataSource = listGastos;

            }
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
        private void PendenciasPSetor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;

            if (chkPesquisarData.Checked == true)
            {

                if (cmbSetor.Text != string.Empty)
                {
                    Gastos edicaoGastos = new Gastos();

                    try
                    {
                        edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor selcionar um setor já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSetor.Text = "";
                        return;
                    }

                    edicaoGastos.dataVencimento = DateTime.Parse(DtGasto.Value.ToString());

                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    SqlDataReader reader;

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[pago] = 0 AND [Gastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[data] = '{edicaoGastos.dataVencimento}'");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Gastos gasto = new Gastos();
                            gasto.id = reader.GetInt32(0);
                            gasto.nome = reader.GetString(1);
                            gasto.descricao = reader.GetString(2);
                            gasto.dataVencimento = reader.GetDateTime(3);
                            gasto.valor = reader.GetDouble(4);
                            gasto.formaPagamento = reader.GetString(5);
                            gasto.pago = reader.GetBoolean(6);

                            listGastos.Add(gasto);

                        }
                        reader.Close();
                        dgvGastos.DataSource = null;
                        dgvGastos.DataSource = listGastos;


                    }
                    else
                    {
                        MessageBox.Show("Nenhum Gasto Em Aberto Encontrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        iniciarGrid();
                    }

                }
                else
                {
                    Gastos edicaoGastos = new Gastos();

                    edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());
                    edicaoGastos.dataVencimento = DateTime.Parse(DtGasto.Value.ToString());

                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    SqlDataReader reader;

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[pago] = 0 AND [Gastos].[data] = '{edicaoGastos.dataVencimento}'");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Gastos gasto = new Gastos();
                            gasto.id = reader.GetInt32(0);
                            gasto.nome = reader.GetString(1);
                            gasto.descricao = reader.GetString(2);
                            gasto.dataVencimento = reader.GetDateTime(3);
                            gasto.valor = reader.GetDouble(4);
                            gasto.formaPagamento = reader.GetString(5);
                            gasto.pago = reader.GetBoolean(6);

                            listGastos.Add(gasto);

                        }
                        reader.Close();
                        dgvGastos.DataSource = null;
                        dgvGastos.DataSource = listGastos;

                    }
                    else
                    {
                        MessageBox.Show("Nenhum Gasto Em Aberto Encontrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        iniciarGrid();
                        cmbSetor.Text = "";
                        DtGasto.Value = DateTime.Now;
                        btnCancelar.Enabled = false;
                    }
                }

            }
            if (chkPesquisarData.Checked == false)
            {
                if (cmbSetor.Text != string.Empty)
                {
                    Gastos edicaoGastos1 = new Gastos();

                    edicaoGastos1.id = int.Parse(cmbSetor.SelectedValue.ToString());
                    edicaoGastos1.dataVencimento = DateTime.Parse(DtGasto.Value.ToString());

                    List<Gastos> listGastos1 = new List<Gastos>();
                    con.conectar();

                    SqlDataReader reader1;

                    reader1 = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[pago] = 0 AND [Gastos].[idSetor] = '{edicaoGastos1.id}'");

                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            Gastos gasto = new Gastos();
                            gasto.id = reader1.GetInt32(0);
                            gasto.nome = reader1.GetString(1);
                            gasto.descricao = reader1.GetString(2);
                            gasto.dataVencimento = reader1.GetDateTime(3);
                            gasto.valor = reader1.GetDouble(4);
                            gasto.formaPagamento = reader1.GetString(5);
                            gasto.pago = reader1.GetBoolean(6);

                            listGastos1.Add(gasto);

                        }
                        reader1.Close();
                        dgvGastos.DataSource = null;
                        dgvGastos.DataSource = listGastos1;


                    }
                    else
                    {
                        MessageBox.Show("Nenhum Gasto Em Aberto Encontrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        iniciarGrid();
                        cmbSetor.Text = "";
                        DtGasto.Value = DateTime.Now;
                        btnCancelar.Enabled = false;
                    }

                }
                else
                {
                    MessageBox.Show("Por Favor Insira Um Setor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    iniciarGrid();
                    cmbSetor.Text = "";
                    DtGasto.Value = DateTime.Now;
                    btnCancelar.Enabled = false;

                }
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            GastosAberto n = new GastosAberto();
            n.Show();
            this.Visible = false;
        }



        private void chkPesquisarData_OnChange(object sender, EventArgs e)
        {
            if (chkPesquisarData.Checked == true)
            {
                DtGasto.Enabled = true;
            }
            else
            {
                DtGasto.Enabled = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciarGrid();
            btnPesquisar.Enabled = true;
            btnPagamentoRealizado.Enabled = false;
            cmbSetor.Text = "";
            DtGasto.Value = DateTime.Now;
            btnCancelar.Enabled = false;
        }

        private void btnPagamentoRealizado_Click(object sender, EventArgs e)
        {
            string id = dgvGastos.CurrentRow.Cells[0].Value.ToString();

            con.conectar();
            SqlDataReader reader;

            reader = con.exeCliente($"SELECT [Gastos].[valor] FROM[dbo].[Gastos] INNER JOIN SetorGastos ON[Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE[Gastos].[idGasto] = '{id}'");

            if (reader.HasRows)
            {
                Double valor = 0;
                while (reader.Read())
                {
                    valor = reader.GetDouble(0);
                }
                reader.Close();
                var choice = MessageBox.Show("O pagamento de R$" + valor + " foi realizado ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice == DialogResult.Yes)
                {
                    int att = con.executar($"UPDATE [dbo].[Gastos] set pago = 1 WHERE idGasto= " + id);
                    dgvGastos.DataSource = null;
                    iniciarGrid();
                    cmbSetor.Text = "";
                    DtGasto.Value = DateTime.Now;

                }
                else
                {

                }
                btnPagamentoRealizado.Enabled = false;
            }
        }

        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnPagamentoRealizado.Enabled = true;
            btnPesquisar.Enabled = false;
            btnCancelar.Enabled = true;
            Gastos n = new Gastos();
            cmbSetor.Text = dgvGastos.CurrentRow.Cells[1].Value.ToString();
            DtGasto.Value = DateTime.Parse(dgvGastos.CurrentRow.Cells[3].Value.ToString());


        }
    }
}
