using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;

namespace totalClean
{
    public partial class RelatorioServico : Form
    {
        int lastCell;
        String tpClientePesquisado;
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
            btnGeraPdf.Enabled = false;


        }

        private void iniciaGrid()
        {
            List<ServicoVenda> listVendasServicos = new List<ServicoVenda>();
            con.conectar();

            SqlDataReader reader;
            if (rdbOdrdenarPId.Checked == true)
            {
                reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico  ORDER BY idVenda DESC");
            }
            else
            {
                reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico  ORDER BY [Vendas].[data] DESC");
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
                    try
                    {
                        sv.valorCobrado = reader.GetDouble(8);
                    }
                    catch (Exception)
                    {
                        sv.valorCobrado = sv.preco;
                    }

                    sv.data = reader.GetDateTime(9);
                    sv.pago = reader.GetBoolean(10);
                    try
                    {
                        sv.formaPagamento = reader.GetString(11);
                    }
                    catch (Exception)
                    {
                        sv.formaPagamento = "";
                    }


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

        private void RelatorioServico_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            InicialFrm m = new InicialFrm();
            m.Show();
            this.Visible = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnGeraPdf.Enabled = true;
            if (rdbAmbos.Checked == true)
            {
                if (cmbServico.Text != string.Empty)
                {
                    Classes.VendasServicos vs = new Classes.VendasServicos();

                    btnGerarRelatorio.Enabled = true;

                    DateTime dataI = DtInicialVenda.Value;
                    DateTime dataF = dtFinalVenda.Value;
                    try
                    {
                        vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor selcionar um serviço já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbServico.Text = "";
                        return;
                    }

                    List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                    con.conectar();

                    SqlDataReader reader;
                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')  ORDER BY idVenda DESC");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')  ORDER BY [Vendas].[data] DESC");
                    }
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ServicoVenda servico = new ServicoVenda();

                            servico.idVenda = reader.GetInt32(0);
                            servico.frotista = reader.GetBoolean(1);
                            servico.cliente = reader.GetString(2);

                            try
                            {
                                servico.CpfCnpj = reader.GetString(3);
                            }
                            catch (Exception)
                            {

                            }

                            servico.carro = reader.GetString(4);
                            servico.carro = reader.GetString(4);
                            servico.placa = reader.GetString(5);
                            servico.servico = reader.GetString(6);
                            servico.preco = reader.GetDouble(7);
                            try
                            {
                                servico.valorCobrado = reader.GetDouble(8);
                            }
                            catch (Exception)
                            {
                                servico.valorCobrado = servico.preco;
                            }

                            servico.data = reader.GetDateTime(9);
                            servico.pago = reader.GetBoolean(10);
                            try
                            {
                                servico.formaPagamento = reader.GetString(11);
                            }
                            catch (Exception)
                            {
                                servico.formaPagamento = "";
                            }
                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvVendas.DataSource = null;
                        dgvVendas.DataSource = listServicoVenda;

                    }

                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                        btnGerarRelatorio.Enabled = false;
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
                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY idVenda DESC ");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY [Vendas].[data] DESC ");
                    }
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ServicoVenda servico = new ServicoVenda();

                            servico.idVenda = reader.GetInt32(0);
                            servico.frotista = reader.GetBoolean(1);
                            servico.cliente = reader.GetString(2);

                            try
                            {
                                servico.CpfCnpj = reader.GetString(3);
                            }
                            catch (Exception)
                            {

                            }

                            servico.carro = reader.GetString(4);
                            servico.carro = reader.GetString(4);
                            servico.placa = reader.GetString(5);
                            servico.servico = reader.GetString(6);
                            servico.preco = reader.GetDouble(7);
                            try
                            {
                                servico.valorCobrado = reader.GetDouble(8);
                            }
                            catch (Exception)
                            {
                                servico.valorCobrado = servico.preco;
                            }

                            servico.data = reader.GetDateTime(9);
                            servico.pago = reader.GetBoolean(10);
                            try
                            {
                                servico.formaPagamento = reader.GetString(11);
                            }
                            catch (Exception)
                            {
                                servico.formaPagamento = "";
                            }

                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvVendas.DataSource = null;
                        dgvVendas.DataSource = listServicoVenda;

                    }

                    else
                    {
                        btnGerarRelatorio.Enabled = false;
                        MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                    }
                }
            }


            if (rdbFrotistas.Checked == true)
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

                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}') ORDER BY idVenda DESC");
                    }

                    else
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}') ORDER BY [Vendas].[data] DESC");
                    }

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ServicoVenda servico = new ServicoVenda();

                            servico.idVenda = reader.GetInt32(0);
                            servico.frotista = reader.GetBoolean(1);
                            servico.cliente = reader.GetString(2);

                            try
                            {
                                servico.CpfCnpj = reader.GetString(3);
                            }
                            catch (Exception)
                            {

                            }

                            servico.carro = reader.GetString(4);
                            servico.carro = reader.GetString(4);
                            servico.placa = reader.GetString(5);
                            servico.servico = reader.GetString(6);
                            servico.preco = reader.GetDouble(7);
                            try
                            {
                                servico.valorCobrado = reader.GetDouble(8);
                            }
                            catch (Exception)
                            {
                                servico.valorCobrado = servico.preco;
                            }

                            servico.data = reader.GetDateTime(9);
                            servico.pago = reader.GetBoolean(10);
                            try
                            {
                                servico.formaPagamento = reader.GetString(11);
                            }
                            catch (Exception)
                            {
                                servico.formaPagamento = "";
                            }

                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvVendas.DataSource = null;
                        dgvVendas.DataSource = listServicoVenda;

                    }

                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                        btnGerarRelatorio.Enabled = false;
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

                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY idVenda DESC ");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY [Vendas].[data] DESC ");
                    }

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ServicoVenda servico = new ServicoVenda();

                            servico.idVenda = reader.GetInt32(0);
                            servico.frotista = reader.GetBoolean(1);
                            servico.cliente = reader.GetString(2);

                            try
                            {
                                servico.CpfCnpj = reader.GetString(3);
                            }
                            catch (Exception)
                            {

                            }

                            servico.carro = reader.GetString(4);
                            servico.carro = reader.GetString(4);
                            servico.placa = reader.GetString(5);
                            servico.servico = reader.GetString(6);
                            servico.preco = reader.GetDouble(7);
                            try
                            {
                                servico.valorCobrado = reader.GetDouble(8);
                            }
                            catch (Exception)
                            {
                                servico.valorCobrado = servico.preco;
                            }

                            servico.data = reader.GetDateTime(9);
                            servico.pago = reader.GetBoolean(10);
                            try
                            {
                                servico.formaPagamento = reader.GetString(11);
                            }
                            catch (Exception)
                            {
                                servico.formaPagamento = "";
                            }

                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvVendas.DataSource = null;
                        dgvVendas.DataSource = listServicoVenda;

                    }

                    else
                    {
                        btnGerarRelatorio.Enabled = false;
                        MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                    }
                }
            }

            if (rdbParticulares.Checked == true)
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

                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}') ORDER BY idVenda DESC");
                    }

                    else
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}') ORDER BY [Vendas].[data] DESC");
                    }

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ServicoVenda servico = new ServicoVenda();

                            servico.idVenda = reader.GetInt32(0);
                            servico.frotista = reader.GetBoolean(1);
                            servico.cliente = reader.GetString(2);

                            try
                            {
                                servico.CpfCnpj = reader.GetString(3);
                            }
                            catch (Exception)
                            {

                            }

                            servico.carro = reader.GetString(4);
                            servico.carro = reader.GetString(4);
                            servico.placa = reader.GetString(5);
                            servico.servico = reader.GetString(6);
                            servico.preco = reader.GetDouble(7);
                            try
                            {
                                servico.valorCobrado = reader.GetDouble(8);
                            }
                            catch (Exception)
                            {
                                servico.valorCobrado = servico.preco;
                            }

                            servico.data = reader.GetDateTime(9);
                            servico.pago = reader.GetBoolean(10);
                            try
                            {
                                servico.formaPagamento = reader.GetString(11);
                            }
                            catch (Exception)
                            {
                                servico.formaPagamento = "";
                            }

                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvVendas.DataSource = null;
                        dgvVendas.DataSource = listServicoVenda;

                    }

                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                        btnGerarRelatorio.Enabled = false;
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

                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj],[Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY idVenda DESC ");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj],[Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY [Vendas].[data] DESC ");
                    }
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ServicoVenda servico = new ServicoVenda();

                            servico.idVenda = reader.GetInt32(0);
                            servico.frotista = reader.GetBoolean(1);
                            servico.cliente = reader.GetString(2);

                            try
                            {
                                servico.CpfCnpj = reader.GetString(3);
                            }
                            catch (Exception)
                            {

                            }

                            servico.carro = reader.GetString(4);
                            servico.carro = reader.GetString(4);
                            servico.placa = reader.GetString(5);
                            servico.servico = reader.GetString(6);
                            servico.preco = reader.GetDouble(7);
                            try
                            {
                                servico.valorCobrado = reader.GetDouble(8);
                            }
                            catch (Exception)
                            {
                                servico.valorCobrado = servico.preco;
                            }

                            servico.data = reader.GetDateTime(9);
                            servico.pago = reader.GetBoolean(10);
                            try
                            {
                                servico.formaPagamento = reader.GetString(11);
                            }
                            catch (Exception)
                            {
                                servico.formaPagamento = "";
                            }

                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvVendas.DataSource = null;
                        dgvVendas.DataSource = listServicoVenda;

                    }

                    else
                    {
                        btnGerarRelatorio.Enabled = false;
                        MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                    }
                }
            }
        }
        private void PreencheCmbServico()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1 ORDER BY nome ASC");

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

        private void rdbOrdenarPData_CheckedChanged(object sender, EventArgs e)
        {
            iniciaGrid();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciaGrid();
            limpaCampos();
            btnGeraPdf.Enabled = false;
            btnGerarRelatorio.Enabled = false;
            rdbAmbos.Checked = true;
        }

        private void limpaCampos()
        {
            cmbServico.Text = "";
            dtFinalVenda.Value = DateTime.Now;
            DtInicialVenda.Value = DateTime.Now;
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {

            // Incluindo os dados das linhas

            Classes.VendasServicos vs = new Classes.VendasServicos();

            DateTime dataI = DtInicialVenda.Value;
            DateTime dataF = dtFinalVenda.Value;
            vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());

            con.conectar();
            SqlDataReader reader;
            if (rdbAmbos.Checked == true)
            {

                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtor(reader);
                }
                else
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtor(reader);
                }

            }
            if (rdbFrotistas.Checked == true)
            {
                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtor(reader);
                }
                else
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtor(reader);
                }
            }
            if (rdbParticulares.Checked == true)
            {
                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtor(reader);
                }
                else
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtor(reader);
                }
            }

        }


        public void construtor(SqlDataReader reader)
        {
            int i = 2;

            String answer = Interaction.InputBox("Digite o nome do arquivo de relatorio", "", null, -1, -1);

            while (answer == null)
            {
                answer = Interaction.InputBox("Digite o nome do arquivo de relatorio", "", null, -1, -1);
            }



            // Inicia o componente Excel
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range rangeTitulo;
            Excel.Range rangeValores;
            Excel.Range rangeTabela;
            object misValue = System.Reflection.Missing.Value;

            //Cria uma planilha temporária na memória do computador
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            //Auto size columns
            xlWorkSheet.Columns.AutoFit();


            //incluindo os títulos de cada coluna
            xlWorkSheet.Cells[1, 1] = "Id Venda";
            xlWorkSheet.Cells[1, 2] = "Classificação";
            xlWorkSheet.Cells[1, 3] = "Cliente";
            xlWorkSheet.Cells[1, 4] = "Cpf/Cnpj";
            xlWorkSheet.Cells[1, 5] = "Carro";
            xlWorkSheet.Cells[1, 6] = "Placa";
            xlWorkSheet.Cells[1, 7] = "Serviço";
            xlWorkSheet.Cells[1, 8] = "Preço";
            xlWorkSheet.Cells[1, 9] = "Valor Cobrado";
            xlWorkSheet.Cells[1, 10] = "Data";
            xlWorkSheet.Cells[1, 11] = "Pagamento";
            xlWorkSheet.Cells[1, 12] = "Forma de Pagamento";


            if (reader.HasRows)
            {


                while (reader.Read())
                {
                    xlWorkSheet.Cells[i, 1] = reader.GetInt32(0);
                    Boolean tipo = reader.GetBoolean(1);

                    if (tipo == true)
                    {
                        xlWorkSheet.Cells[i, 2] = "Frotista";
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 2] = "Particular";
                    }


                    xlWorkSheet.Cells[i, 3] = reader.GetString(2).Trim();


                    try
                    {
                        xlWorkSheet.Cells[i, 4] = reader.GetString(3).Trim();

                    }
                    catch
                    {
                        xlWorkSheet.Cells[i, 4] = "";

                    }

                    xlWorkSheet.Cells[i, 5] = reader.GetString(4);

                    xlWorkSheet.Cells[i, 6] = reader.GetString(5);

                    xlWorkSheet.Cells[i, 7] = reader.GetString(6);

                    xlWorkSheet.Cells[i, 8] = reader.GetDouble(7);

                    try
                    {
                        xlWorkSheet.Cells[i, 9] = reader.GetDouble(8);

                    }
                    catch
                    {
                        xlWorkSheet.Cells[i, 9] = reader.GetDouble(7);
                    }

                    xlWorkSheet.Cells[i, 10] = reader.GetDateTime(9);

                    Boolean pg = reader.GetBoolean(10);

                    if (pg == true)
                    {
                        xlWorkSheet.Cells[i, 11] = "PG";
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 11] = "EM ABERTO";
                    }
                    try
                    {
                        xlWorkSheet.Cells[i, 12] = reader.GetString(11);
                    }
                    catch (Exception)
                    {

                    }
                    i++;
                }

                //Atribuindo valore de subtotal e total
                lastCell = i - 1;
                xlWorkSheet.Cells[i, 7] = "Total:";
                //xlWorkSheet.Cells[i, 7].Interior.Color = ColorTranslator.FromHtml("#9ea7aa");
                xlWorkSheet.Cells[i, 7].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // xlWorkSheet.Cells[i, 9].Interior.Color = ColorTranslator.FromHtml("#cfd8dc");
                xlWorkSheet.Cells[i, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Cells[i, 9].Formula = "=SUM(I2:I" + lastCell + ")";
                xlWorkSheet.Cells[i, 9].NumberFormat = "R$#,##0.00";
                xlWorkSheet.Calculate();

                xlWorkSheet.Cells[i, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                //   xlWorkSheet.Cells[i, 8].Interior.Color = ColorTranslator.FromHtml("#cfd8dc");
                xlWorkSheet.Cells[i, 8].Formula = "=SUM(H2:H" + lastCell + ")";
                xlWorkSheet.Cells[i, 8].NumberFormat = "R$#,##0.00";
                xlWorkSheet.Calculate();

                reader.Close();
                con.desconectar();
            }

            //Colocando borda 
            rangeTabela = xlWorkSheet.get_Range("A1", "L" + lastCell);
            rangeTabela.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            // Alinha colunas
            rangeTabela.Columns.AutoFit();

            //Colocando cor nas linhas de dados
            // rangeTabela.Interior.Color = ColorTranslator.FromHtml("#cfd8dc");


            //Colocando cores nas células de identificação 
            rangeTitulo = xlWorkSheet.get_Range("A1", "L1");

            //rangeTitulo.Interior.Color = ColorTranslator.FromHtml("#9ea7aa");
            rangeTitulo.Font.Bold = true;

            // Colocando R$
            rangeValores = xlWorkSheet.get_Range("H2", "I" + i);
            rangeValores.NumberFormat = "R$#,##0.00";
            rangeValores.Columns.AutoFit();

            //Salva o arquivo de acordo com a documentação do Excel.
            try
            {

                xlWorkBook.SaveAs(answer, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                //o arquivo foi salvo na pasta Meus Documentos.
                string caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                try
                {
                    FileInfo fi = new FileInfo($@"{caminho}/{answer}.xls");
                    if (fi.Exists)
                    {
                        System.Diagnostics.Process.Start($@"{caminho}/{answer}.xls");
                    }
                    else
                    {
                        MessageBox.Show("Arquivo não encontrado");
                    }
                }
                catch
                {
                    MessageBox.Show("Test");
                }


            }
            catch
            {
                MessageBox.Show("Não foi possivel salvar");
            }

        }
        protected PdfPCell getNewCell(string Texto, iTextSharp.text.Font Fonte, int Alinhamento, float Espacamento, int Borda, BaseColor CorBorda, BaseColor CorFundo)
        {
            var cell = new PdfPCell(new Phrase(Texto, Fonte));
            cell.HorizontalAlignment = Alinhamento;
            cell.Padding = Espacamento;
            cell.Border = Borda;
            cell.BorderColor = CorBorda;
            cell.BackgroundColor = CorFundo;

            return cell;
        }


        public void construtorPdf(SqlDataReader reader)
        {



            String answer = Interaction.InputBox("Digite o nome do arquivo de relatorio", "", null, -1, -1);

            while (answer == null)
            {
                answer = Interaction.InputBox("Digite o nome do arquivo de relatorio", "", null, -1, -1);
            }

            //caminho onde sera criado o pdf + nome desejado
            string caminho = @"C:\Users\Public\Documents\" + answer + ".pdf";


            Document doc = new Document(PageSize.A4); // Criando e estipulando o tipo de folha usada            
            doc.SetMargins(40, 40, 40, 80); // Estipulando margens
            doc.AddCreationDate(); // Adicionando configurações

            //criando o arquivo pdf embranco
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(caminho, FileMode.Create));

            doc.Open();

            #region Cabeçalho
            //-------------------------------------------CABEÇALHO---------------------------------------------

            BaseColor preto = new BaseColor(0, 0, 0);
            iTextSharp.text.Font font = FontFactory.GetFont("Roboto", 8, iTextSharp.text.Font.NORMAL, preto);
            iTextSharp.text.Font titulo = FontFactory.GetFont("Roboto", 12, iTextSharp.text.Font.BOLD, preto);
            float[] sizes = new float[] { 1f, 3f, 1f };

            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin);
            table.SetWidths(sizes);

            #region Logo Empresa
            iTextSharp.text.Image foot;

            foot = iTextSharp.text.Image.GetInstance(@"F:\Matheus\Projeto-TotalClean\favicon_tc.png");

            foot.ScalePercent(60);

            PdfPCell cell = new PdfPCell(foot);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            cell.BorderWidthTop = 1.5f;
            cell.BorderWidthBottom = 1.5f;
            cell.PaddingTop = 10f;
            cell.PaddingBottom = 10f;
            table.AddCell(cell);

            PdfPTable micros = new PdfPTable(1);
            cell = new PdfPCell(new Phrase("Total Clean", titulo));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            micros.AddCell(cell);
            cell = new PdfPCell(new Phrase("Relátórios de compra por data", font));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            micros.AddCell(cell);

            cell = new PdfPCell(micros);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.BorderWidthTop = 1.5f;
            cell.BorderWidthBottom = 1.5f;
            cell.PaddingTop = 10f;
            table.AddCell(cell);
            #endregion

            #region Data Emissão
            micros = new PdfPTable(1);
            cell = new PdfPCell(new Phrase("Data emissão: " + DateTime.Now.ToShortDateString(), font));
            cell.Border = 0;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            micros.AddCell(cell);

            cell = new PdfPCell(micros);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.BorderWidthTop = 1.5f;
            cell.BorderWidthBottom = 1.5f;
            cell.PaddingTop = 10f;
            table.AddCell(cell);
            #endregion

            table.WriteSelectedRows(0, -1, doc.LeftMargin, (doc.PageSize.Height - 10), writer.DirectContent);

            #endregion

            #region Período
            iTextSharp.text.Font fontPeriodo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);

            Paragraph periodo = new Paragraph((new Phrase("\nPeríodo de tempo pesquisado: \n" + DtInicialVenda.Value.ToShortDateString() + " a " + dtFinalVenda.Value.ToShortDateString() + "\n", fontPeriodo)));
            periodo.Alignment = Element.ALIGN_LEFT;
         //   doc.Add(periodo);

            
            if (rdbParticulares.Checked == true)
            {
                tpClientePesquisado = "particulares";
            }
            if(rdbFrotistas.Checked == true)
            {
                tpClientePesquisado = "frotistas";
            }
            if(rdbAmbos.Checked == true)
            {
                tpClientePesquisado = "ambos";
            }

            Paragraph tpCliente = new Paragraph((new Phrase(" \nTipo de cliente pesquisado: \n" + tpClientePesquisado + "\n", fontPeriodo)));
            tpCliente.Alignment = Element.ALIGN_LEFT;
        //    doc.Add(tpCliente);

            String servicoTexto = cmbServico.Text;
            if(cmbServico.Text == string.Empty)
            {
                servicoTexto = "---";
            }

            Paragraph servico = new Paragraph((new Phrase("\nVendas pesquisadas por serviço: \n" + servicoTexto+ "\n", fontPeriodo)));
            servico.Alignment = Element.ALIGN_LEFT;
            

            Paragraph filtros = new Paragraph((new Phrase("\n\n\nFiltros:", fontPeriodo)));
            filtros.Alignment = Element.ALIGN_LEFT;
            doc.Add(filtros);

            PdfPTable tableFiltros = new PdfPTable(3);
            float[] colsTableFiltros = { 15, 15, 15 };
            tableFiltros.SetWidths(colsTableFiltros);

            tableFiltros.WidthPercentage = 100f;

            tableFiltros.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            tableFiltros.AddCell(periodo);
            tableFiltros.AddCell(tpCliente);
            tableFiltros.AddCell(servico);

            doc.Add(tableFiltros);

            #endregion

            PdfPTable tableTitulos = new PdfPTable(5);
            construtorPDF(reader, doc);


            doc.Close();
            System.Diagnostics.Process.Start(caminho);
        }

        public void construtorPDF(SqlDataReader reader, Document doc)
        {
            BaseColor preto = new BaseColor(0, 0, 0);
            iTextSharp.text.Font font = FontFactory.GetFont("Roboto", 8, iTextSharp.text.Font.NORMAL, preto);
            iTextSharp.text.Font titulo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);
            iTextSharp.text.Font fontPeriodo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);

            BaseColor branco = new BaseColor(255, 255, 255);

            int idVendaOld = 0;
            double somaSubTotal = 0;
            double somaTotal = 0;
            int contador = 0;

            ServicoVenda servico = new ServicoVenda();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    servico.idVenda = reader.GetInt32(0);

                    if (servico.idVenda != idVendaOld)
                    {
                        PdfPTable tableTotal = new PdfPTable(3);
                        float[] colsTableTotal = { 14, 3, 20 };
                        tableTotal.SetWidths(colsTableTotal);
                        tableTotal.WidthPercentage = 100f;
                        tableTotal.PaddingTop = 5;

                        tableTotal.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;


                        tableTotal.AddCell(getNewCell("Total:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));
                        tableTotal.AddCell(getNewCell(somaSubTotal.ToString("c"), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));
                        tableTotal.AddCell(getNewCell(somaTotal.ToString("C"), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));

                        somaTotal = 0;
                        somaSubTotal = 0;

                        if (contador != 0)
                        {
                            doc.Add(tableTotal);
                        }

                        PdfPTable tableTitulos = new PdfPTable(5);
                        BaseColor fundo = new BaseColor(200, 200, 200);


                        float[] colsW = { 5, 15, 10, 10, 10 };
                        tableTitulos.SetWidths(colsW);
                        tableTitulos.WidthPercentage = 100f;

                        tableTitulos.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
                        tableTitulos.DefaultCell.BorderColor = preto;
                        tableTitulos.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);

                        servico.cliente = reader.GetString(1);
                        servico.carro = reader.GetString(2);
                        servico.placa = reader.GetString(3);
                        servico.data = reader.GetDateTime(7);


                        tableTitulos.AddCell(getNewCell("OS:", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell("Cliente:", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell("Carro:", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell("Placa:", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell("Data:", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));


                        tableTitulos.AddCell(getNewCell(servico.idVenda.ToString(), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell(servico.cliente, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell(servico.carro, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell(servico.placa, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                        tableTitulos.AddCell(getNewCell(servico.data.ToShortDateString(), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));

                        PdfPTable tableTitulosDados = new PdfPTable(5);
                        float[] colstableTitulosDados = { 25, 5, 10, 10, 15 };
                        tableTitulosDados.SetWidths(colstableTitulosDados);
                        tableTitulosDados.WidthPercentage = 100f;

                        tableTitulosDados.DefaultCell.Border = PdfPCell.NO_BORDER;

                        Paragraph espaco = new Paragraph((new Phrase("\n")));
                        doc.Add(espaco);

                        tableTitulosDados.AddCell(getNewCell("Serviços:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                        tableTitulosDados.AddCell(getNewCell("Preço:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                        tableTitulosDados.AddCell(getNewCell("Valor Cobrado:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                        tableTitulosDados.AddCell(getNewCell("Situação:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                        tableTitulosDados.AddCell(getNewCell("Forma de Pagamento:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));

                        doc.Add(tableTitulos);
                        doc.Add(tableTitulosDados);

                        idVendaOld = servico.idVenda;
                        contador += 1;
                    }


                    servico.servico = reader.GetString(4);
                    servico.preco = reader.GetDouble(5);
                    servico.valorCobrado = reader.GetDouble(6);
                    servico.formaPagamento = reader.GetString(9);

                    PdfPTable tableDados = new PdfPTable(5);
                    float[] colsTableDados = { 25, 5, 10, 10, 15 };
                    tableDados.SetWidths(colsTableDados);
                    tableDados.WidthPercentage = 100f;

                    tableDados.DefaultCell.Border = PdfPCell.TOP_BORDER;
                    tableDados.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;

                    tableDados.AddCell(getNewCell(servico.servico, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell(servico.preco.ToString("C"), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell(servico.valorCobrado.ToString("C"), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));

                    if (servico.pago == true)
                    {
                        tableDados.AddCell(getNewCell("PAGO", font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));
                    }
                    else
                    {
                        tableDados.AddCell(getNewCell("EM ABERTO", font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));
                    }


                    tableDados.AddCell(getNewCell(servico.formaPagamento.ToString(), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));

                    somaSubTotal += servico.preco;
                    somaTotal += servico.valorCobrado;

                    doc.Add(tableDados);


                }

            }



        }
        private void btnGeraPdf_Click(object sender, EventArgs e)
        {

            Classes.VendasServicos vs = new Classes.VendasServicos();

            DateTime dataI = DtInicialVenda.Value;
            DateTime dataF = dtFinalVenda.Value;
            vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());

            con.conectar();
            SqlDataReader reader;
            if (rdbAmbos.Checked == true)
            {

                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtorPdf(reader);
                }
                else
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtorPdf(reader);
                }

            }
            if (rdbFrotistas.Checked == true)
            {
                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtorPdf(reader);
                }
                else
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtorPdf(reader);
                }
            }
            if (rdbParticulares.Checked == true)
            {
                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtorPdf(reader);
                }
                else
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
                    construtorPdf(reader);
                }
            }

     
        }
    }
}