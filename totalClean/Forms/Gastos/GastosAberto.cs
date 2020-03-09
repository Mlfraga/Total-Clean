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
    public partial class GastosAberto : Form
    {
        public GastosAberto()
        {
            InitializeComponent();
        }
        Conexao con = new Conexao();
        private void GastosAberto_Load(object sender, EventArgs e)
        {
            btnPagamentoRealizado.Enabled = false;
            iniciarGrid();
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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            InicialFrm m = new InicialFrm();
            m.Show();
            this.Visible = false;
        }

        private void GastosAberto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnPagamentoRealizado.Enabled = true;

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
                var choice = MessageBox.Show("O pagamento de R$" + valor + " foi realizado?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice == DialogResult.Yes)
                {
                    int att = con.executar($"UPDATE [dbo].[Gastos] set pago = 1 WHERE idGasto= " + id);
                    dgvGastos.DataSource = null;
                    iniciarGrid();


                }
                else
                {

                }
            }


        }

        private void btnPendenciasPSetor_Click(object sender, EventArgs e)
        {
            PendenciasPSetor n = new PendenciasPSetor();
            n.Show();
            this.Visible = false;
        }
    }
}
