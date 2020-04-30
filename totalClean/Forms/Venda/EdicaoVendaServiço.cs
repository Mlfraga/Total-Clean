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
        double diferenca;

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

            reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].pfpj, [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico ORDER BY idVenda desc");

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

                    try
                    {
                        sv.valorCobrado = reader.GetDouble(9);
                    }
                    catch (Exception)
                    {
                        sv.valorCobrado = sv.preco;
                    }

                    sv.pago = reader.GetBoolean(10);



                    try
                    {
                        sv.formaPagamento = reader.GetString(11);
                    }
                    catch (Exception)
                    {
                        sv.formaPagamento = "";
                    }

                    sv.idVendasServicos = reader.GetInt32(12);

                    listVendasServicos.Add(sv);
                }
                reader.Close();
                con.desconectar();
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
            lblIdVendasServicos.Visible = false;

            lblData.Visible = false;
            dtpData.Visible = false;
            dtFinalVenda.Enabled = false;
            DtInicialVenda.Enabled = false;

            gpBoxPagamento.Visible = false;


        }

        private void ConsultaVendaServiço_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void BloqueaBtns()
        {
            btnCancelar.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = false;

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
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}' AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')  ORDER BY idVenda desc");
                    }
                    else
                    {
                        vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].[idVenda] = {idVenda} and [Cliente].[idCliente] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [VendasServicos].idServico = ('{vs.servico1}')  ORDER BY idVenda desc");
                    }
                }
                else
                {
                    if (chkFiltroData.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}'  ORDER BY idVenda desc");
                    }
                    else
                    {

                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].[idVenda] = {idVenda} and [Cliente].[nome] LIKE ('%{cliente}%') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%')  ORDER BY idVenda desc");
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

                        try
                        {
                            sv.valorCobrado = reader.GetDouble(9);
                        }
                        catch (Exception)
                        {
                            sv.valorCobrado = sv.preco;
                        }

                        sv.pago = reader.GetBoolean(10);



                        try
                        {
                            sv.formaPagamento = reader.GetString(11);
                        }
                        catch (Exception)
                        {
                            sv.formaPagamento = "";
                        }

                        sv.idVendasServicos = reader.GetInt32(12);



                        listServicoVenda.Add(sv);
                    }
                    reader.Close();
                    con.desconectar();
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
                        int cliente;
                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        DateTime dataI = DtInicialVenda.Value;
                        DateTime dataF = dtFinalVenda.Value;
                        try
                        {
                            vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Favor selcionar um serviço já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbServico.Text = " ";
                            return;
                        }

                        if (cmbCliente.Text == string.Empty)
                        {
                            cmbCliente.SelectedValue = 0;
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[carro] LIKE ('%{carro}%') AND [Vendas].[placa] LIKE ('%{placa}%') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}') ORDER BY idVenda desc");
                        }
                        else
                        {
                            try
                            {
                                cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return;
                            }
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}'  AND [Vendas].[carro] LIKE ('%{carro}%') AND [Vendas].[placa] LIKE ('%{placa}%') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}') ORDER BY idVenda desc");
                        }


                    }
                    else
                    {
                        int cliente;
                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        try
                        {
                            vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Favor selcionar um serviço já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbServico.Text = " ";
                            return;
                        }


                        if (cmbCliente.Text == string.Empty)
                        {
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE  [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [VendasServicos].idServico = ('{vs.servico1}') ORDER BY idVenda desc ");
                        }
                        else
                        {
                            try
                            {
                                cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cmbCliente.Text = " ";
                                return;
                            }
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') AND [VendasServicos].idServico = ('{vs.servico1}')  ORDER BY idVenda desc ");
                        }

                    }
                }
                else
                {
                    if (chkFiltroData.Checked == true)
                    {
                        int cliente;
                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        DateTime dataI = DtInicialVenda.Value;
                        DateTime dataF = dtFinalVenda.Value;
                        if (cmbCliente.Text == string.Empty)
                        {
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY idVenda desc");
                        }
                        else
                        {
                            try
                            {
                                cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cmbCliente.Text = " ";
                                return;
                            }
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and  [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') and [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY idVenda desc");
                        }

                    }
                    else
                    {
                        int cliente;
                        String carro = txtCarro.Text;
                        String placa = txtPlaca.Text;
                        if (cmbCliente.Text == string.Empty)
                        {
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE   [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%')  ORDER BY idVenda desc");
                        }
                        else
                        {
                            try
                            {
                                cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                cmbCliente.Text = " ";
                                return;
                            }
                            cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = ('{cliente}') and  [Vendas].[carro] LIKE ('%{carro}%') and [Vendas].[placa] LIKE ('%{placa}%') ORDER BY idVenda desc ");
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
                        sv.data = reader.GetDateTime(7);
                        sv.preco = reader.GetDouble(8);

                        try
                        {
                            sv.valorCobrado = reader.GetDouble(9);
                        }
                        catch (Exception)
                        {
                            sv.valorCobrado = sv.preco;
                        }

                        sv.pago = reader.GetBoolean(10);



                        try
                        {
                            sv.formaPagamento = reader.GetString(11);
                        }
                        catch (Exception)
                        {
                            sv.formaPagamento = "";
                        }

                        sv.idVendasServicos = reader.GetInt32(12);



                        listServicoVenda.Add(sv);
                    }
                    reader.Close();
                    con.desconectar();
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
            String formaPagamento;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnPesquisar.Enabled = false;
            btnExcluir.Enabled = true;

            txtIdVenda.Text = dgvVendas.CurrentRow.Cells[0].Value.ToString();
            cmbCliente.Text = dgvVendas.CurrentRow.Cells[2].Value.ToString();
            txtCarro.Text = dgvVendas.CurrentRow.Cells[4].Value.ToString();
            txtPlaca.Text = dgvVendas.CurrentRow.Cells[5].Value.ToString();
            cmbServico.Text = dgvVendas.CurrentRow.Cells[6].Value.ToString();
            dtpData.Value = DateTime.Parse(dgvVendas.CurrentRow.Cells[9].Value.ToString());
            formaPagamento = dgvVendas.CurrentRow.Cells[11].Value.ToString().Trim();

            if (formaPagamento == "Transferência Bancária")
            {
                rdbTransferencia.Checked = true;
            }

            if (formaPagamento == "Boleto")
            {
                rdbBoleto.Checked = true;
            }

            if (formaPagamento == "Crédito")
            {
                rdbCredito.Checked = true;
            }

            if (formaPagamento == "Débito")
            {
                rdbDebito.Checked = true;
            }

            if (formaPagamento == "Dinheiro")
            {
                rdbDinheiro.Checked = true;
            }

            if (formaPagamento == "Permuta")
            {
                rdbPermuta.Checked = true;
            }

            lblIdVendasServicos.Text = dgvVendas.CurrentRow.Cells[12].Value.ToString();

            gpBoxPagamento.Visible = true;
            lblData.Visible = true;
            dtpData.Visible = true;

            chkFiltroData.Visible = false;
            lblDtInicical.Visible = false;
            lblDtFinal.Visible = false;
            dtFinalVenda.Visible = false;
            DtInicialVenda.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciaGrid();

            btnSalvar.Enabled = false;
            chkFiltroData.Enabled = true;

            btnCancelar.Enabled = false;
            btnPesquisar.Enabled = true;
            btnExcluir.Enabled = false;

            gpBoxPagamento.Visible = false;
            lblData.Visible = false;
            dtpData.Visible = false;

            chkFiltroData.Visible = true;
            lblDtInicical.Visible = true;
            lblDtFinal.Visible = true;
            dtFinalVenda.Visible = true;
            DtInicialVenda.Visible = true;

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

            gpBoxPagamento.Visible = false;
            lblData.Visible = false;
            dtpData.Visible = false;

            chkFiltroData.Visible = true;
            lblDtInicical.Visible = true;
            lblDtFinal.Visible = true;
            dtFinalVenda.Visible = true;
            DtInicialVenda.Visible = true;
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

                con.desconectar();
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
                con.desconectar();
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
                con.desconectar();
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            int servico;
            int idVendasServicos;
            int idVenda;

            if (txtPlaca.Text.Length > 7)
            {
                MessageBox.Show("Campo placa com mais de 7 caracteres", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (cmbCliente.Text != string.Empty && txtCarro.Text != string.Empty && txtPlaca.Text != string.Empty && cmbServico.Text != string.Empty)
                {
                    Classes.VendasServicos vs = new Classes.VendasServicos();
                    Venda venda = new Venda();

                    try
                    {
                        venda.idCliente = int.Parse(cmbCliente.SelectedValue.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    venda.carro = txtCarro.Text;
                    venda.placa = txtPlaca.Text;
                    venda.data = dtpData.Value;

                    if (rdbTransferencia.Checked == true)
                    {
                        venda.formaPagamento = "Transferência Bancária";
                    }

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

                    try
                    {
                        servico = int.Parse(cmbServico.SelectedValue.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor selcionar um serviço já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    idVendasServicos = int.Parse(lblIdVendasServicos.Text);
                    idVenda = int.Parse(txtIdVenda.Text);

                    //Calcula Novo valor cobrado
                    List<ServicoVenda> listVendasServicosCalculaValorCobrado = new List<ServicoVenda>();
                    con.conectar();

                    double preco;
                    double valorCobrado;
                    double novoValorCobrado;


                    SqlDataReader readerCalculaValorCobrado;

                    readerCalculaValorCobrado = con.exeCliente("SELECT [Servicos].[preco], [VendasServicos].[valorCobrado] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE VendasServicos.idVendasServicos = " + idVendasServicos + " ORDER BY Vendas.idVenda desc ");

                    if (readerCalculaValorCobrado.HasRows)
                    {
                        while (readerCalculaValorCobrado.Read())
                        {

                            preco = readerCalculaValorCobrado.GetDouble(0);
                            valorCobrado = readerCalculaValorCobrado.GetDouble(1);

                            diferenca = preco - valorCobrado;
                        }
                    }


                    con.conectar();

                    PrecoVendaServico precoServicoVenda = new PrecoVendaServico();
                    SqlDataReader readerPreco1;

                    readerPreco1 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{servico}')");
                    if (readerPreco1.HasRows)
                    {
                        while (readerPreco1.Read())
                        {
                            precoServicoVenda.valor = readerPreco1.GetDouble(0);
                        }

                        readerPreco1.Close();
                        con.desconectar();
                    }

                    novoValorCobrado = precoServicoVenda.valor - diferenca;

                    con.conectar();

                    int alteraCliente = con.executar($"UPDATE [dbo].[Vendas] SET [idCliente] = '" + venda.idCliente + "' WHERE idVenda = " + idVenda);
                    int alteraCarro = con.executar($"UPDATE [dbo].[Vendas] SET [carro] = '" + venda.carro + "' WHERE idVenda = " + idVenda);
                    int alteraPlaca = con.executar($"UPDATE [dbo].[Vendas] SET [placa] = '" + venda.placa + "' WHERE idVenda = " + idVenda);
                    int alteraData = con.executar($"UPDATE [dbo].[Vendas] SET [data] = '" + venda.data + "' WHERE idVenda = " + idVenda);
                    int alteraFormaPagamento = con.executar($"UPDATE [dbo].[Vendas] SET [formaPagamento] = '" + venda.formaPagamento + "' WHERE idVenda = " + idVenda);

                    int alteraServico = con.executar($"UPDATE [dbo].[VendasServicos] SET [idServico] = '" + servico + "' WHERE idVendasServicos = " + idVendasServicos);
                    int alteraValorCobrado = con.executar($"UPDATE [dbo].[VendasServicos] SET [valorCobrado] = '" + novoValorCobrado + "' WHERE idVendasServicos = " + idVendasServicos);

                    MessageBox.Show("Dados alterados com sucesso", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.desconectar();

                    // ATUALIZA GRID COM GASTO ALTERADO
                    List<ServicoVenda> listVendasServicos = new List<ServicoVenda>();
                    con.conectar();

                    SqlDataReader reader;

                    reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].pfpj, [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento], [VendasServicos].[idVendasServicos] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE Vendas.idVenda = " + idVenda + " ORDER BY Vendas.idVenda desc ");

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

                            try
                            {
                                sv.valorCobrado = reader.GetDouble(9);
                            }
                            catch (Exception)
                            {
                                sv.valorCobrado = sv.preco;
                            }

                            sv.pago = reader.GetBoolean(10);



                            try
                            {
                                sv.formaPagamento = reader.GetString(11);
                            }
                            catch (Exception)
                            {
                                sv.formaPagamento = "";
                            }

                            sv.idVendasServicos = reader.GetInt32(12);

                            listVendasServicos.Add(sv);
                        }
                        reader.Close();
                        con.desconectar();
                    }
                    else
                    {
                        Console.WriteLine("Não retornou dados");
                    }
                    dgvVendas.DataSource = null;
                    dgvVendas.DataSource = listVendasServicos;
                }
            }
        }

    }
}
