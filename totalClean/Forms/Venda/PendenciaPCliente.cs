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
    public partial class PendenciaPCliente : Form
    {
        Conexao con = new Conexao();
        public PendenciaPCliente()
        {
            InitializeComponent();
        }

        private void PendenciaPCliente_Load(object sender, EventArgs e)
        {
            rdbTransferencia.Enabled = false;
            rdbBoleto.Enabled = false;
            rdbCredito.Enabled = false;
            rdbDebito.Enabled = false;
            rdbDinheiro.Enabled = false;
            rdbPermuta.Enabled = false;
            btnPagamentoRealizado.Enabled = false;
            prencheCmbCliente();
            iniciaGrid();
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
            return 0;
        }

        private void iniciaGrid()
        {


            List<VendaPendente> listVendasServicos = new List<VendaPendente>();
            con.conectar();

            SqlDataReader reader;
            dgvPagamentosPendentes.Rows.Clear();

            reader = con.exeCliente("SELECT [Vendas].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento], [Vendas].[ValorCobrado] FROM [Vendas] INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente WHERE pago = 0");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    VendaPendente sv = new VendaPendente();

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
                    sv.carro = reader.GetString(4);
                    sv.placa = reader.GetString(5);
                    sv.data = reader.GetDateTime(6);
                    sv.pago = reader.GetBoolean(7);

                    String formaPagamento;

                    try
                    {
                        formaPagamento = reader.GetString(8);
                    }
                    catch (Exception)
                    {
                        formaPagamento = "";
                    }
                    try
                    {
                        sv.valorACobrar = reader.GetDouble(9);
                    }
                    catch (Exception)
                    {
                        sv.valorACobrar = setPreco(sv.idVenda);
                    }

                    dgvPagamentosPendentes.DataSource = null;
                    dgvPagamentosPendentes.Rows.Add(sv.idVenda, sv.frotista, sv.cliente, sv.CpfCnpj, sv.carro, sv.placa, sv.data.ToShortDateString(), sv.pago, setPreco(sv.idVenda), sv.valorACobrar, formaPagamento);
                }
                reader.Close();
            }
            else
            {

                Console.WriteLine("Não retornou dados");
            }


        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Pendentes n = new Pendentes();
            n.Show();
            this.Visible = false;

        }

        private void btnPagamentoRealizado_Click(object sender, EventArgs e)
        {
            string id = dgvPagamentosPendentes.CurrentRow.Cells[0].Value.ToString();
            string nome = dgvPagamentosPendentes.CurrentRow.Cells[2].Value.ToString();

            con.conectar();
            SqlDataReader reader;

            reader = con.exeCliente($"SELECT [Servicos].[preco] , [VendasServicos].[valorCobrado] from VendasServicos INNER JOIN Servicos ON [VendasServicos].[idServico] = Servicos.idServico WHERE idVenda =  " + id);


            if (reader.HasRows)
            {
                double preco = 0;

                while (reader.Read())
                {
                    try
                    {
                        preco = reader.GetDouble(1) + preco;
                    }
                    catch
                    {
                        preco = reader.GetDouble(0) + preco;
                    }
                }
                reader.Close();


                var choice = MessageBox.Show("O cliente efetuou o pagamento de R$" + preco + ",00 ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (choice == DialogResult.Yes)
                {
                    Venda venda = new Venda();

                    if (rdbBoleto.Checked == true)
                    {
                        venda.formaPagamento = "Boleto";
                    }

                    if (rdbCredito.Checked == true)
                    {
                        venda.formaPagamento = "Crédito";
                    }

                    if (rdbDebito.Checked == true)
                    {
                        venda.formaPagamento = "Débito";
                    }

                    if (rdbDinheiro.Checked == true)
                    {
                        venda.formaPagamento = "Dinheiro";
                    }

                    if (rdbPermuta.Checked == true)
                    {
                        venda.formaPagamento = "Permuta";
                    }
                    if (rdbTransferencia.Checked == true)
                    {
                        venda.formaPagamento = "Transferência Bancária";
                    }

                    int attPG = con.executar($"UPDATE [dbo].[Vendas] set pago = 1 WHERE idVenda = " + id);
                    int att = con.executar($"UPDATE [dbo].[Vendas] set formaPagamento = '{venda.formaPagamento}' WHERE idVenda = " + id);
                    iniciaGrid();

                }
                else
                {

                }

            }


            rdbDinheiro.Checked = true;
            btnPagamentoRealizado.Enabled = false;
        }

        private void dgvPagamentosPendentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String formaPG;
            formaPG = dgvPagamentosPendentes.CurrentRow.Cells[10].Value.ToString().Trim();

            if (formaPG == "Dinheiro")
            {
                rdbDinheiro.Checked = true;
            }
            if (formaPG == "Boleto")
            {
                rdbBoleto.Checked = true;
            }
            if (formaPG == "Crédito")
            {
                rdbCredito.Checked = true;
            }
            if (formaPG == "Débito")
            {
                rdbDebito.Checked = true;
            }
            if (formaPG == "Permuta")
            {
                rdbPermuta.Checked = true;
            }
            if (formaPG == "Transferência Bancária")
            {
                rdbTransferencia.Checked = true;
            }

            rdbTransferencia.Enabled = true;
            rdbBoleto.Enabled = true;
            rdbCredito.Enabled = true;
            rdbDebito.Enabled = true;
            rdbDinheiro.Enabled = true;
            rdbPermuta.Enabled = true;
            btnPagamentoRealizado.Enabled = true;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (cmbCliente.Text != String.Empty)
            {
                con.conectar();

                SqlDataReader reader;
                dgvPagamentosPendentes.Rows.Clear();

                int cliente;
                try
                {
                    cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                }
                catch (Exception)
                {
                    MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                reader = con.exeCliente("SELECT [Vendas].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento], [Vendas].[ValorCobrado] FROM [Vendas] INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente WHERE [Vendas].[pago]  = 0 AND Cliente.idCliente = " + cliente);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        VendaPendente sv = new VendaPendente();

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
                        sv.carro = reader.GetString(4);
                        sv.placa = reader.GetString(5);
                        sv.data = reader.GetDateTime(6);
                        sv.pago = reader.GetBoolean(7);

                        String formaPagamento;

                        try
                        {
                            formaPagamento = reader.GetString(8);
                        }
                        catch (Exception)
                        {
                            formaPagamento = "";
                        }

                        try
                        {
                            sv.valorACobrar = reader.GetDouble(9);
                        }
                        catch (Exception)
                        {
                            sv.valorACobrar = setPreco(sv.idVenda);
                        }

                        dgvPagamentosPendentes.DataSource = null;
                        dgvPagamentosPendentes.Rows.Add(sv.idVenda, sv.frotista, sv.cliente, sv.CpfCnpj, sv.carro, sv.placa, sv.data.ToShortDateString(), sv.pago, setPreco(sv.idVenda), sv.valorACobrar, formaPagamento);
                    }
                    reader.Close();
                    con.desconectar();
                }
                else
                {
                    MessageBox.Show("Cliente não tem pagamentos pendentes", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    iniciaGrid();

                }
            }

            else
            {
                MessageBox.Show("Por Favor informe um cliente. ", "ERRO", MessageBoxButtons.OK);
            }

            cmbCliente.Text = "";
        }

        private void PendenciaPCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciaGrid();
        }


    }
}
