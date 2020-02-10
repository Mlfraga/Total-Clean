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
    public partial class RelatorioServico : Form
    {
        public RelatorioServico()
        {
            InitializeComponent();
        }

        Conexao con = new Conexao();

        private void RelatorioServiço_Load(object sender, EventArgs e)
        {
            iniciaGrid();
            PreencheCmbServico();
            btnGerarRelatorio.Enabled = false;


        }

        private void iniciaGrid()
        {
            List<ServicoVenda> listVendasServicos = new List<ServicoVenda>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ServicoVenda sv = new ServicoVenda();

                    sv.idVenda = reader.GetInt32(0);
                    sv.frotista = reader.GetBoolean(1);
                    sv.cliente = reader.GetString(2);
                    sv.carro = reader.GetString(3);
                    sv.placa = reader.GetString(4);
                    sv.servico = reader.GetString(5);
                    sv.data = reader.GetDateTime(6);

                    listVendasServicos.Add(sv);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvVendas.DataSource = null;
            dgvVendas.DataSource = listVendasServicos;

        }

        private void RelatorioServico_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            InicialFrm m = new InicialFrm();
            m.Show();
            this.Visible = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cmbServico.Text != string.Empty)
            {
                Classes.VendasServicos vs = new Classes.VendasServicos();

                btnGerarRelatorio.Enabled = true;

                DateTime dataI = DtInicialVenda.Value;
                DateTime dataF = dtFinalVenda.Value;
                vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());


                List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                con.conectar();

                SqlDataReader reader;

                reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ServicoVenda servico = new ServicoVenda();

                        servico.idVenda = reader.GetInt32(0);
                        servico.frotista = reader.GetBoolean(1);
                        servico.cliente = reader.GetString(2);
                        servico.carro = reader.GetString(3);
                        servico.placa = reader.GetString(4);
                        servico.servico = reader.GetString(5);
                        servico.data = reader.GetDateTime(6);

                        listServicoVenda.Add(servico);
                    }
                    reader.Close();
                    dgvVendas.DataSource = null;
                    dgvVendas.DataSource = listServicoVenda;

                }

                else
                {
                    MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                }


            }
            else
            {
               

                btnGerarRelatorio.Enabled = true;

                DateTime dataI = DtInicialVenda.Value;
                DateTime dataF = dtFinalVenda.Value;
               


                List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                con.conectar();

                SqlDataReader reader;

                reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ServicoVenda servico = new ServicoVenda();

                        servico.idVenda = reader.GetInt32(0);
                        servico.frotista = reader.GetBoolean(1);
                        servico.cliente = reader.GetString(2);
                        servico.carro = reader.GetString(3);
                        servico.placa = reader.GetString(4);
                        servico.servico = reader.GetString(5);
                        servico.data = reader.GetDateTime(6);

                        listServicoVenda.Add(servico);
                    }
                    reader.Close();
                    dgvVendas.DataSource = null;
                    dgvVendas.DataSource = listServicoVenda;

                }

                else
                {
                    MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                }
            }
        }
        private void PreencheCmbServico()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico.DataSource = listServico;
            cmbServico.ValueMember = "id";
            cmbServico.DisplayMember = "nome";
            cmbServico.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciaGrid();
            limpaCampos();
            btnGerarRelatorio.Enabled = false;
        }

        private void limpaCampos()
        {
            cmbServico.Text = "";
            dtFinalVenda.Value = DateTime.Now;
            DtInicialVenda.Value = DateTime.Now;
        }
    }
}
