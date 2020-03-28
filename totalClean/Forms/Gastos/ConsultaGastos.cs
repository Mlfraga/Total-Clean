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
    public partial class ConsultaGastos : Form
    {
        Conexao con = new Conexao();
        public ConsultaGastos()
        {
            InitializeComponent();
        }

        private void ConsultaGastos_Load(object sender, EventArgs e)
        {
            iniciarGrid();
            bloqueaBtns();
            preencheCmbSetor();
            bloqueaCampos();
            txtIdGasto.Enabled = false;
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
        private void bloqueaBtns()
        {
            btnSalvar.Enabled = false; 
            btnCancelar.Enabled = false;

            rdbAberto.Enabled = false;
            rdbPago.Enabled = false;
            rdbDinheiro.Enabled = false;
            rdbCartão.Enabled = false;
            rdbPermuta.Enabled = false;
            rdbBoleto.Enabled = false;

        }
        private void desbloqueaBtns()
        {
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            
            rdbAberto.Enabled = true;
            rdbPago.Enabled = true;
            rdbDinheiro.Enabled = true;
            rdbCartão.Enabled = true;
            rdbPermuta.Enabled = true;
            rdbBoleto.Enabled = true;
        }


        private void bloqueaCampos()
        {
            txtDescricao.Enabled = false;
            txtValor.Enabled = false;
        
        }

        private void desbloqueaCampos()
        {
            txtDescricao.Enabled = true;
            txtValor.Enabled = true;
        }
        private void iniciarGrid()
        {
            List<Gastos> listGastos = new List<Gastos>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor]");

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

        private void ConsultaGastos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            CadastroGastos n = new CadastroGastos();
            n.Show();
            this.Visible = false;

        }

        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            desbloqueaBtns();
            desbloqueaCampos();
            Gastos n = new Gastos();

            txtIdGasto.Text = dgvGastos.CurrentRow.Cells[0].Value.ToString();
            cmbSetor.Text = dgvGastos.CurrentRow.Cells[1].Value.ToString();
            txtDescricao.Text = dgvGastos.CurrentRow.Cells[2].Value.ToString();
            DtGasto.Value = DateTime.Parse(dgvGastos.CurrentRow.Cells[3].Value.ToString());
            txtValor.Text = dgvGastos.CurrentRow.Cells[4].Value.ToString();
            n.formaPagamento = dgvGastos.CurrentRow.Cells[5].Value.ToString().Trim();

            if (n.formaPagamento == "Boleto")
            {
                rdbBoleto.Checked = true;
            }
            if (n.formaPagamento == "Dinheiro")
            {
                rdbDinheiro.Checked = true;
            }
            if (n.formaPagamento == "Permuta")
            {
                rdbPermuta.Checked = true;
            }
            if (n.formaPagamento == "Cartão")
            {
                rdbCartão.Checked = true;
            }


            if (dgvGastos.CurrentRow.Cells[6].Value.Equals(true))
            {
                rdbPago.Checked = true;
            }
            else
            {
                rdbAberto.Checked = true;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cmbSetor.Text != string.Empty && txtDescricao.Text != string.Empty)
            {
                Gastos edicaoGastos = new Gastos();

                edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());
                edicaoGastos.descricao = txtDescricao.Text;
                edicaoGastos.dataVencimento = DateTime.Parse(DtGasto.Value.ToString());

                List<Gastos> listGastos = new List<Gastos>();
                con.conectar();

                SqlDataReader reader;

                reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[descricao] LIKE '%{edicaoGastos.descricao}%' AND [Gastos].[data] = '{edicaoGastos.dataVencimento}'");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Gastos gasto = new Gastos();
                        gasto.id= reader.GetInt32(0);
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

            if (cmbSetor.Text != string.Empty)
            {
                Gastos edicaoGastos = new Gastos();
                try { 
                edicaoGastos.id= int.Parse(cmbSetor.SelectedValue.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("Favor selcionar um setor já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSetor.Text = "";
                    return;
                }
                edicaoGastos.descricao = txtDescricao.Text;
                edicaoGastos.dataVencimento = DateTime.Parse(DtGasto.Value.ToString());

                List<Gastos> listGastos = new List<Gastos>();
                con.conectar();

                SqlDataReader reader;

                reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND  [Gastos].[data] = '{edicaoGastos.dataVencimento}'");

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
                    MessageBox.Show("Nenhum dado encontrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    iniciarGrid();
                    bloqueaBtns();
                    bloqueaCampos();
                    txtValor.Text = "";
                    txtDescricao.Text = "";
                    cmbSetor.Text = "";
                }

            }
            else
            {
                Gastos edicaoGastos = new Gastos();

                edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());
                edicaoGastos.descricao = txtDescricao.Text;
                edicaoGastos.dataVencimento = DateTime.Parse(DtGasto.Value.ToString());

                List<Gastos> listGastos = new List<Gastos>();
                con.conectar();

                SqlDataReader reader;

                reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] = '{edicaoGastos.dataVencimento}'");

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
                    MessageBox.Show("Nenhum dado encontrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    iniciarGrid();
                    bloqueaBtns();
                    bloqueaCampos();
                    txtValor.Text = "";
                    txtDescricao.Text = "";
                    cmbSetor.Text = "";
                }
            }
        }

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            iniciarGrid();
            bloqueaBtns();
            bloqueaCampos();
            txtValor.Text = "";
            txtDescricao.Text = "";
            cmbSetor.Text = "";
            DtGasto.Value = DateTime.Now;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciarGrid();
            bloqueaBtns();
            bloqueaCampos();
            txtValor.Text = "";
            txtDescricao.Text = "";
            cmbSetor.Text = "";
            DtGasto.Value = DateTime.Now;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            EdicaoGastos c = new EdicaoGastos();

             c.id = int.Parse(txtIdGasto.Text.ToString());
            c.idSetor = int.Parse(cmbSetor.SelectedValue.ToString());
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


            List<EdicaoGastos> listGastos = new List<EdicaoGastos>();
            con.conectar();



            int alteraSetor = con.executar($"UPDATE [dbo].[Gastos] SET [idSetor] = '" + c.idSetor + "' WHERE idGasto = " + c.id);
            int alteraDescricao = con.executar($"UPDATE [dbo].[Gastos] SET [descricao] = '" + c.descricao + "' WHERE idGasto = " + c.id);
            int alteraFPagamento = con.executar($"UPDATE [dbo].[Gastos] SET [formaPagamento] = '" + c.formaPagamento + "' WHERE idGasto = " + c.id);
            int alteraData = con.executar($"UPDATE [dbo].[Gastos] SET [data] = '" + c.dataVencimento + "' WHERE idGasto = " + c.id);
            int alteraValor = con.executar($"UPDATE [dbo].[Gastos] SET [valor] = '" + c.valor + "' WHERE idGasto = " + c.id);
            int alteraPago = con.executar($"UPDATE [dbo].[Gastos] SET [pago] = '" + c.pago + "' WHERE idGasto = " + c.id);

            MessageBox.Show("Dados alterados com sucesso", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // ATUALIZA GRID COM GASTO ALTERADO

            List<Gastos> listGastos1 = new List<Gastos>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor]WHERE idGasto = '{c.id}'");

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

                    listGastos1.Add(gasto);


                }
                reader.Close();
                dgvGastos.DataSource = null;
                dgvGastos.DataSource = listGastos1;

            }

        }

    }
    }

