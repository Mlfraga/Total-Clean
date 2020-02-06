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
    public partial class ConsultaVendaServiço : Form
    {
        public ConsultaVendaServiço()
        {
            InitializeComponent();
        }

        Conexao con = new Conexao();

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
     

        private void ConsultaVendaServiço_Load(object sender, EventArgs e)
        {
            iniciaGrid();
            BloqueaBtns();
            dtFinalVenda.Enabled = false;
            DtInicialVenda.Enabled = false;
        }

        private void ConsultaVendaServiço_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void BloqueaBtns()
        {
            btnCancelar.Enabled = false;
            btnExcluir.Enabled = false;


        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            VendaFrm n = new VendaFrm();
            n.Show();
            this.Visible = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {


            if (txtIdVenda.Text != string.Empty)
            {
                String cliente = txtCliente.Text;
                String carro = txtCarro.Text;
                String placa = txtPlaca.Text;
                String serv = txtServ.Text;
                DateTime dataI = DtInicialVenda.Value;
                DateTime dataF = dtFinalVenda.Value;
                int idVenda = int.Parse(txtIdVenda.Text);

                List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                con.conectar();

                SqlDataReader reader;
                if (chkFiltroData.Checked == true)
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[nome] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Servicos].[nome] LIKE ('%{serv}%')");
                }
                else
                {
                    
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].[idVenda] = {idVenda} and [Cliente].[nome] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [Servicos].[nome] LIKE ('%{serv}%') ");
                }
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

                dtFinalVenda.Enabled = false;
                DtInicialVenda.Enabled = false;

            }
            else
            {
                String cliente = txtCliente.Text;
                String carro = txtCarro.Text;
                String placa = txtPlaca.Text;
                String serv = txtServ.Text;
                DateTime dataI = DtInicialVenda.Value;
                DateTime dataF = dtFinalVenda.Value;

                List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                con.conectar();

                SqlDataReader reader;

                if (chkFiltroData.Checked == true)
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[nome] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Servicos].[nome] LIKE ('%{serv}%')");
                }
                else
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[nome] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [Servicos].[nome] LIKE ('%{serv}%') ");
                }

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

        private void chkFiltroData_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFiltroData.Checked == true)
            {
                dtFinalVenda.Enabled = true;
                DtInicialVenda.Enabled = true;
            }
            else
            {

                dtFinalVenda.Enabled = false;
                DtInicialVenda.Enabled = false;
            }

        }

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            limpaCampos();
            iniciaGrid();
        }

        private void dgvVendas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            btnCancelar.Enabled = true;
            btnPesquisar.Enabled = false;
            btnExcluir.Enabled = true;

            chkFiltroData.Enabled = false;
            dtFinalVenda.Enabled = false;
            DtInicialVenda.Enabled = false;

            txtIdVenda.Text = dgvVendas.CurrentRow.Cells[0].Value.ToString();
            txtCliente.Text = dgvVendas.CurrentRow.Cells[2].Value.ToString();
            txtCarro.Text = dgvVendas.CurrentRow.Cells[3].Value.ToString();
            txtPlaca.Text = dgvVendas.CurrentRow.Cells[4].Value.ToString();
            txtServ.Text = dgvVendas.CurrentRow.Cells[5].Value.ToString();

            txtIdVenda.ReadOnly = true;
            txtCliente.ReadOnly = true;
            txtCarro.ReadOnly = true;
            txtPlaca.ReadOnly = true;
            txtServ.ReadOnly = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            chkFiltroData.Enabled = true;

            btnCancelar.Enabled = false;
            btnPesquisar.Enabled = true;
            btnExcluir.Enabled = false;

            limpaCampos();
        }
        private void limpaCampos()
        {
            txtCarro.Text = "";
            txtCliente.Text = "";
            txtIdVenda.Text = "";
            txtPlaca.Text = "";
            txtServ.Text = "";
            dtFinalVenda.Value = DateTime.Now;
            DtInicialVenda.Value = DateTime.Now;
            chkFiltroData.Checked = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;

            txtIdVenda.ReadOnly = true;
            txtCliente.ReadOnly = true;
            txtCarro.ReadOnly = true;
            txtPlaca.ReadOnly = true;

            if (txtIdVenda.Text != string.Empty)
            {
                ServicoVenda n = new ServicoVenda();
                n.idVenda = int.Parse(txtIdVenda.Text);
                con.conectar();

                int linhas = con.executar($"DELETE FROM VendasServicos WHERE idVenda = '{n.idVenda}'");
                int linhas1 = con.executar($"DELETE FROM Vendas WHERE idVenda = '{n.idVenda}'");

                MessageBox.Show("Dados deletados com sucesso", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                limpaCampos();
                iniciaGrid();
                btnCancelar.Enabled = false;
                btnExcluir.Enabled = false;
                btnPesquisar.Enabled = true;

                txtIdVenda.ReadOnly = false;
                txtCliente.ReadOnly = false;
                txtCarro.ReadOnly = false;
                txtPlaca.ReadOnly = false;

            }
            else
            {
                MessageBox.Show("Por favor selecione algum serviço no quadro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

      
    }
}
