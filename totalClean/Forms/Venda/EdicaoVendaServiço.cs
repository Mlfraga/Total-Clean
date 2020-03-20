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

            reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].pfpj, [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ServicoVenda sv = new ServicoVenda();

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
                    sv.servico = reader.GetString(6);
                    sv.data = reader.GetDateTime(7);
                    sv.preco = reader.GetDouble(8);

                    sv.pago = reader.GetBoolean(9);



                    try
                    {
                        sv.formaPagamento = reader.GetString(10);
                    }
                    catch (Exception)
                    {
                        sv.formaPagamento = "";
                    }


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
            PreencheCmbServico();
            prencheCmbCliente();
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
                Classes.VendasServicos vs = new Classes.VendasServicos();

                int cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                String carro = txtCarro.Text;
                String placa = txtPlaca.Text;
                DateTime dataI = DtInicialVenda.Value;
                DateTime dataF = dtFinalVenda.Value;
                int idVenda = int.Parse(txtIdVenda.Text);

                List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                con.conectar();

                SqlDataReader reader;

                if (cmbServico.Text != string.Empty)
                {

                    if (chkFiltroData.Checked == true)
                    {
                        vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Servicos].[preco], [Vendas].[pago], [Vendas].[formaPagamento]  FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}' AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");
                    }
                    else
                    {
                        vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Servicos].[preco], [Vendas].[pago], [Vendas].[formaPagamento]  FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].[idVenda] = {idVenda} and [Cliente].[idCliente] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [VendasServicos].idServico = ('{vs.servico1}') ");
                    }
                }
                else
                {
                    if (chkFiltroData.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Servicos].[preco], [Vendas].[pago], [Vendas].[formaPagamento]  FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}'");
                    }
                    else
                    {

                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Servicos].[preco], [Vendas].[pago], [Vendas].[formaPagamento]  FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].[idVenda] = {idVenda} and [Cliente].[nome] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%')");
                    }
                }


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ServicoVenda sv = new ServicoVenda();


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
                        sv.servico = reader.GetString(6);
                        sv.data = reader.GetDateTime(7);
                        sv.preco = reader.GetDouble(8);

                        sv.pago = reader.GetBoolean(9);



                        try
                        {
                            sv.formaPagamento = reader.GetString(10);
                        }
                        catch (Exception)
                        {
                            sv.formaPagamento = "";
                        }


                        listServicoVenda.Add(sv);
                    }
                    reader.Close();
                    dgvVendas.DataSource = null;
                    dgvVendas.DataSource = listServicoVenda;
                }

                else
                {
                    MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                    iniciaGrid();
                    limpaCampos();
                }

                dtFinalVenda.Enabled = false;
                DtInicialVenda.Enabled = false;

            }
            else
            {

                Classes.VendasServicos vs = new Classes.VendasServicos();




                List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                con.conectar();

                SqlDataReader reader;

                if (cmbServico.Text != string.Empty)
                {

                    if (chkFiltroData.Checked == true)
                    {

                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        DateTime dataI = DtInicialVenda.Value;
                        DateTime dataF = dtFinalVenda.Value;
                        vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());

                        if (cmbCliente.Text == string.Empty)
                        {
                            cmbCliente.SelectedValue = 0;
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[carro] LIKE ('%{carro}%') AND [Vendas].[placa] LIKE ('%{placa}%') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");
                        }
                        else
                        {
                            int cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}'  AND [Vendas].[carro] LIKE ('%{carro}%') AND [Vendas].[placa] LIKE ('%{placa}%') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");
                        }


                    }
                    else
                    {

                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                        if (cmbCliente.Text == string.Empty)
                        {
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE  [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [VendasServicos].idServico = ('{vs.servico1}') ");
                        }
                        else
                        {
                            int cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [VendasServicos].idServico = ('{vs.servico1}') ");
                        }

                    }
                }
                else
                {
                    if (chkFiltroData.Checked == true)
                    {

                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        DateTime dataI = DtInicialVenda.Value;
                        DateTime dataF = dtFinalVenda.Value;
                        if (cmbCliente.Text == string.Empty)
                        {
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}'");
                        }
                        else
                        {
                            int cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and  [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}'");
                        }

                    }
                    else
                    {
                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        if (cmbCliente.Text == string.Empty)
                        {
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE   [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') ");
                        }
                        else
                        {
                            int cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and  [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') ");
                        }


                    }
                }


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ServicoVenda sv = new ServicoVenda();

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
                        sv.servico = reader.GetString(6);
                        sv.preco = reader.GetDouble(7);
                        sv.data = reader.GetDateTime(8);


                        sv.pago = reader.GetBoolean(9);



                        try
                        {
                            sv.formaPagamento = reader.GetString(10);
                        }
                        catch (Exception)
                        {
                            sv.formaPagamento = "";
                        }





                        listServicoVenda.Add(sv);
                    }
                    reader.Close();
                    dgvVendas.DataSource = null;
                    dgvVendas.DataSource = listServicoVenda;
                }
                else
                {
                    MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                    iniciaGrid();
                    limpaCampos();

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



        private void dgvVendas_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            btnCancelar.Enabled = true;
            btnPesquisar.Enabled = false;
            btnExcluir.Enabled = true;

            chkFiltroData.Enabled = false;
            dtFinalVenda.Enabled = false;
            DtInicialVenda.Enabled = false;

            txtIdVenda.Text = dgvVendas.CurrentRow.Cells[0].Value.ToString();
            cmbCliente.Text = dgvVendas.CurrentRow.Cells[2].Value.ToString();
            txtCarro.Text = dgvVendas.CurrentRow.Cells[3].Value.ToString();
            txtPlaca.Text = dgvVendas.CurrentRow.Cells[4].Value.ToString();
            cmbServico.Text = dgvVendas.CurrentRow.Cells[5].Value.ToString();

            txtIdVenda.ReadOnly = true;
            cmbCliente.Enabled = true;
            txtCarro.ReadOnly = true;
            txtPlaca.ReadOnly = true;
            cmbServico.Enabled = false;
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
            cmbCliente.Text = "";
            txtIdVenda.Text = "";
            txtPlaca.Text = "";
            cmbServico.Text = "";
            dtFinalVenda.Value = DateTime.Now;
            DtInicialVenda.Value = DateTime.Now;
            chkFiltroData.Checked = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;

            txtIdVenda.ReadOnly = true;
            cmbCliente.Enabled = true;
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
                cmbCliente.Enabled = true;
                txtCarro.ReadOnly = false;
                txtPlaca.ReadOnly = false;

            }
            else
            {
                MessageBox.Show("Por favor selecione algum serviço no quadro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            BloqueaBtns();
            btnPesquisar.Enabled = true;
            limpaCampos();
            iniciaGrid();
        }
    }
}
