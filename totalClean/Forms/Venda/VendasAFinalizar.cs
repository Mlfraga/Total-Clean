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
    public partial class VendasAFinalizar : Form
    {
        Conexao con = new Conexao();
        public String telefone;
        public int idVenda;
        public VendasAFinalizar()
        {
            InitializeComponent();
        }

        private void VendasAFinalizar_Load(object sender, EventArgs e)
        {
            iniciaGrid();
            prencheCmbCliente();
            bloqueaBtns();
        }
        private void bloqueaBtns()
        {
            btnEnviaMsg.Enabled = false;
            btnCancelar.Enabled = false;
        }
        private void desbloqueaBtns()
        {
            btnEnviaMsg.Enabled = true;
            btnCancelar.Enabled = true;
        }
        private void prencheCmbCliente()
        {
            List<Cliente> listCliente = new List<Cliente>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idCliente, nome from CLiente order by nome ASC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.id = reader.GetInt32(0);
                    cliente.nome = reader.GetString(1);

                    listCliente.Add(cliente);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbCliente.DataSource = listCliente;
            cmbCliente.ValueMember = "id";
            cmbCliente.DisplayMember = "nome";
            cmbCliente.Text = "";
        }
        private void iniciaGrid()
        {
            List<VendaFinalizada> listVendaFinalizada = new List<VendaFinalizada>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj] ,[Cliente].[telefone], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[finalizado] = 0");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    VendaFinalizada sv = new VendaFinalizada();

                    sv.idVenda = reader.GetInt32(0);
                    sv.frotista = reader.GetBoolean(1);
                    sv.cliente = reader.GetString(2);
                    try
                    {
                        sv.CpfCnpj = reader.GetString(3);
                    }
                    catch (Exception)
                    {

                    }
                    sv.telefone = reader.GetString(4);
                    sv.carro = reader.GetString(5);
                    sv.placa = reader.GetString(6);
                    sv.servico = reader.GetString(7);
                    sv.data = reader.GetDateTime(8);
                    sv.preco = reader.GetDouble(9);


                    listVendaFinalizada.Add(sv);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvVendas.DataSource = null;
            dgvVendas.DataSource = listVendaFinalizada;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            InicialFrm n = new InicialFrm();
            n.Show();
            this.Visible = false;
        }

        private void VendasAFinalizar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnEnviaMsg_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", $"https://web.whatsapp.com/send?phone=5531'{telefone}'&text=Olá,%20tudo%20bem?%20Você%20já%20pode%20buscar%20o%20seu%20carro%20na%20Total%20Clean!");


            int finalizado = con.executar("UPDATE[dbo].[Vendas] set finalizado = 1 WHERE idVenda = " + idVenda);
            bloqueaBtns();
            iniciaGrid();
            cmbCliente.Text = "";

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bloqueaBtns();
            iniciaGrid();
            cmbCliente.Text = "";
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
            List<VendaFinalizada> listVendaFinalizada = new List<VendaFinalizada>();
            Cliente pesquisa = new Cliente();
            pesquisa.id = int.Parse(cmbCliente.SelectedValue.ToString());

            SqlDataReader reader;

            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Cliente].[telefone], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[finalizado] = 0 and [Cliente].[idCliente] = '{pesquisa.id}'");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    VendaFinalizada sv = new VendaFinalizada();

                    sv.idVenda = reader.GetInt32(0);
                    sv.frotista = reader.GetBoolean(1);
                    sv.cliente = reader.GetString(2);
                    sv.CpfCnpj = reader.GetString(3);
                    try
                    {
                        sv.telefone = reader.GetString(4);
                    }
                    catch (Exception)
                    {

                    }
                    sv.carro = reader.GetString(5);
                    sv.placa = reader.GetString(6);
                    sv.servico = reader.GetString(7);
                    sv.data = reader.GetDateTime(8);
                    sv.preco = reader.GetDouble(9);


                    listVendaFinalizada.Add(sv);
                }

            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvVendas.DataSource = null;
            dgvVendas.DataSource = listVendaFinalizada;

            reader.Close();
        }

        private void dgvVendas_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            idVenda = int.Parse(dgvVendas.CurrentRow.Cells[0].Value.ToString());
            telefone = dgvVendas.CurrentRow.Cells[3].Value.ToString();
            desbloqueaBtns();

        }


    }
}
