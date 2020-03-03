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
    public partial class Pendentes : Form
    {
        public Pendentes()
        {
            InitializeComponent();
        }
        Conexao con = new Conexao();
        private void Pendentes_Load(object sender, EventArgs e)
        {
            btnPagamentoRealizado.Enabled = false;
            iniciaGrid();

        }

        public double setPreco(int num)
        {
            

            con.conectar();
            SqlDataReader reader;

            reader = con.exeCliente($"SELECT Servicos.preco from VendasServicos INNER JOIN Servicos ON [VendasServicos].[idServico] = Servicos.idServico WHERE idVenda =  " + num);


            if (reader.HasRows)
            {
                double preco = 0;

                while (reader.Read())
                {

                    preco = reader.GetDouble(0) + preco;

                }
                return preco;               
            }
            return 0 ;

        }
        private void iniciaGrid()
        {

            List<VendaPendente> listVendasServicos = new List<VendaPendente>();
            con.conectar();

            SqlDataReader reader;
            dgvPagamentosPendentes.Rows.Clear();

            reader = con.exeCliente("SELECT [Vendas].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Vendas].[data], [Vendas].[pago] FROM [Vendas] INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente WHERE pago = 0");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    VendaPendente sv = new VendaPendente();

                    sv.idVenda = reader.GetInt32(0);
                    sv.frotista = reader.GetBoolean(1);
                    sv.cliente = reader.GetString(2);
                    sv.carro = reader.GetString(3);
                    sv.placa = reader.GetString(4);
                    sv.data = reader.GetDateTime(5);
                    sv.pago = reader.GetBoolean(6);

                    
                    dgvPagamentosPendentes.DataSource = null;
                    dgvPagamentosPendentes.Rows.Add( sv.idVenda, sv.frotista, sv.cliente, sv.carro, sv.placa, sv.data.ToShortDateString(), sv.pago, setPreco(sv.idVenda));
                }                
                reader.Close();
            }
            else
            {
                
                Console.WriteLine("Não retornou dados");
            }


        }



        private void btnPagamentoRealizado_Click(object sender, EventArgs e)
        {
            string id = dgvPagamentosPendentes.CurrentRow.Cells[0].Value.ToString();
            string nome = dgvPagamentosPendentes.CurrentRow.Cells[2].Value.ToString();

            con.conectar();
            SqlDataReader reader;

            reader = con.exeCliente($"SELECT Servicos.preco from VendasServicos INNER JOIN Servicos ON [VendasServicos].[idServico] = Servicos.idServico WHERE idVenda =  " + id);


            if (reader.HasRows)
            {
                double preco = 0;

                while (reader.Read())
                {

                    preco = reader.GetDouble(0) + preco;

                }
                reader.Close();
                var choice = MessageBox.Show("O cliente efetuou o pagamento de R$" + preco + ",00 ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice == DialogResult.Yes)
                {
                    int att = con.executar($"UPDATE [dbo].[Vendas] set pago = 1 WHERE idVenda = " + id);
                    iniciaGrid();
                    
                }
                else
                {

                }
                
            }


            
            btnPagamentoRealizado.Enabled = false;
        }
     
        private void Pendentes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            InicialFrm m = new InicialFrm();
            m.Show();
            this.Visible = false;
        }

        private void dgvVendasClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPagamentosPendentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnPagamentoRealizado.Enabled = true;
        }

        private void dgvPagamentosPendentes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
