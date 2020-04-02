﻿using System;
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

            reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico");

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

                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");

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

                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");

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

                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");

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

                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");

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

                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");

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

                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj],[Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciaGrid();
            limpaCampos();
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
            object misValue = System.Reflection.Missing.Value;

            //Cria uma planilha temporária na memória do computador
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //incluindo dados
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

            Classes.VendasServicos vs = new Classes.VendasServicos();

            DateTime dataI = DtInicialVenda.Value;
            DateTime dataF = dtFinalVenda.Value;


            con.conectar();
            SqlDataReader reader;

            if (rdbAmbos.Checked == true)
            {
                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
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
                        int lastCell = i - 1;
                        xlWorkSheet.Cells[i, 8] = "Total:";
                        xlWorkSheet.Cells[i, 9] = "=SOMA(I2:I" + lastCell + ")";
                        reader.Close();
                        con.desconectar();
                    }



                    //Salva o arquivo de acordo com a documentação do Excel.
                    try
                    {

                        xlWorkBook.SaveAs(answer, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                        //o arquivo foi salvo na pasta Meus Documentos.
                        string caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //MessageBox.Show("Concluído. Verifique em " + caminho + "arquivo.xls");


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
                else
                {
                    vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
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
                        int lastCell = i - 1;
                        xlWorkSheet.Cells[i, 8] = "Total:";
                        xlWorkSheet.Cells[i, 9] = "=SOMA(I2:I" + lastCell + ")";
                        reader.Close();
                        con.desconectar();
                    }



                    //Salva o arquivo de acordo com a documentação do Excel.
                    try
                    {

                        xlWorkBook.SaveAs(answer, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                        //o arquivo foi salvo na pasta Meus Documentos.
                        string caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //MessageBox.Show("Concluído. Verifique em " + caminho + "arquivo.xls");


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
            }
            if (rdbFrotistas.Checked == true)
            {
                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
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
                        int lastCell = i - 1;
                        xlWorkSheet.Cells[i, 8] = "Total:";
                        xlWorkSheet.Cells[i, 9] = "=SOMA(I2:I" + lastCell + ")";
                        reader.Close();
                        con.desconectar();
                    }



                    //Salva o arquivo de acordo com a documentação do Excel.
                    try
                    {

                        xlWorkBook.SaveAs(answer, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                        //o arquivo foi salvo na pasta Meus Documentos.
                        string caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //MessageBox.Show("Concluído. Verifique em " + caminho + "arquivo.xls");


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
                else
                {
                    vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 1 AND [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
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
                        int lastCell = i - 1;
                        xlWorkSheet.Cells[i, 8] = "Total:";
                        xlWorkSheet.Cells[i, 9] = "=SOMA(I2:I" + lastCell + ")";
                        reader.Close();
                        con.desconectar();
                    }



                    //Salva o arquivo de acordo com a documentação do Excel.
                    try
                    {

                        xlWorkBook.SaveAs(answer, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                        //o arquivo foi salvo na pasta Meus Documentos.
                        string caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //MessageBox.Show("Concluído. Verifique em " + caminho + "arquivo.xls");


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
            }

            if (rdbParticulares.Checked == true)
            {
                if (cmbServico.Text == "")
                {
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
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
                        int lastCell = i - 1;
                        xlWorkSheet.Cells[i, 8] = "Total:";
                        xlWorkSheet.Cells[i, 9] = "=SOMA(I2:I" + lastCell + ")";
                        reader.Close();
                        con.desconectar();
                    }



                    //Salva o arquivo de acordo com a documentação do Excel.
                    try
                    {

                        xlWorkBook.SaveAs(answer, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                        //o arquivo foi salvo na pasta Meus Documentos.
                        string caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //MessageBox.Show("Concluído. Verifique em " + caminho + "arquivo.xls");


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
                else
                {
                    vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[data], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[frotista] = 0 AND [VendasServicos].idServico = ('{vs.servico1}') AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ");
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
                        int lastCell = i - 1;
                        xlWorkSheet.Cells[i, 8] = "Total:";
                        xlWorkSheet.Cells[i, 9] = "=SOMA(I2:I" + lastCell + ")";
                        reader.Close();
                        con.desconectar();
                    }



                    //Salva o arquivo de acordo com a documentação do Excel.
                    try
                    {

                        xlWorkBook.SaveAs(answer, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkBook.Close(true, misValue, misValue);
                        xlApp.Quit();
                        //o arquivo foi salvo na pasta Meus Documentos.
                        string caminho = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //MessageBox.Show("Concluído. Verifique em " + caminho + "arquivo.xls");


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
            }
        }

    }
}
