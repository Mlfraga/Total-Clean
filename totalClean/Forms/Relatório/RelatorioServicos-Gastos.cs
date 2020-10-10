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
    public partial class RelatorioServicos_Gastos : Form
    {
        Conexao con = new Conexao();
        int lastCell;
        int lastCellVendas;
        public RelatorioServicos_Gastos()
        {
            InitializeComponent();
        }

        private void RelatorioServicos_Gastos_Load(object sender, EventArgs e)
        {
            btnGerarRelatorio.Enabled = false;
            btnCancelar.Enabled = false;
            iniciaGridGastos();
            iniciaGridVendas();
        }
        private void iniciaGridGastos()
        {
            List<Gastos> listGastos = new List<Gastos>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] ORDER BY idGasto DESC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Gastos gasto = new Gastos();
                    gasto.id = reader.GetInt32(0);
                    gasto.nome = reader.GetString(1);
                    gasto.descricao = reader.GetString(2);
                    gasto.dataVencimento = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    gasto.valor = reader.GetDouble(4);
                    gasto.formaPagamento = reader.GetString(5);
                    gasto.pago = reader.GetBoolean(6);

                    listGastos.Add(gasto);

                }
                reader.Close();
                con.desconectar();
                dgvGastos.DataSource = null;
                dgvGastos.DataSource = listGastos;

            }
        }
        private void iniciaGridVendas()
        {
            List<ServicoVenda> listVendasServicos = new List<ServicoVenda>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].pfpj, [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico ORDER BY idVenda DESC");

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
                    sv.data = reader.GetDateTime(7).ToString("dd/MM/yyyy");
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

        private void RelatorioServicos_Gastos_FormClosed(object sender, FormClosedEventArgs e)
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
            pesquisaGastos();
            pesquisaVendas();
            btnGerarRelatorio.Enabled = true;
            btnCancelar.Enabled = true;

        }
        private void pesquisaGastos()
        {
            String dataI = DtInicial.Value.ToString("MM/dd/yyyy");
            String dataF = dtFinal.Value.ToString("MM/dd/yyyy");

            List<Gastos> listGastos = new List<Gastos>();
            con.conectar();

   
            SqlDataReader reader;

            reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY idGasto DESC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Gastos gasto = new Gastos();
                    gasto.id = reader.GetInt32(0);
                    gasto.nome = reader.GetString(1);
                    gasto.descricao = reader.GetString(2);
                    gasto.dataVencimento = reader.GetDateTime(3).ToString("dd/MM/yyyy");
                    gasto.valor = reader.GetDouble(4);
                    gasto.formaPagamento = reader.GetString(5);
                    gasto.pago = reader.GetBoolean(6);

                    listGastos.Add(gasto);

                }
                reader.Close();
                con.desconectar();
                dgvGastos.DataSource = null;
                dgvGastos.DataSource = listGastos;

            }
            else
            {
                MessageBox.Show("Nenhum dado encontrado", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iniciaGridGastos();

            }
        }
        private void pesquisaVendas()
        {
            String dataI = DtInicial.Value.ToString("MM/dd/yyyy");
            String dataF = dtFinal.Value.ToString("MM/dd/yyyy");



            List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco], [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' ORDER BY idVenda DESC");

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
                    servico.placa = reader.GetString(5);
                    servico.servico = reader.GetString(6);
                    servico.data = reader.GetDateTime(7).ToString("dd/MM/yyyy");
                    servico.preco = reader.GetDouble(8);

                    try
                    {
                        servico.valorCobrado = reader.GetDouble(9);
                    }
                    catch (Exception)
                    {
                        servico.valorCobrado = servico.preco;
                    }

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnGerarRelatorio.Enabled = false;
            iniciaGridGastos();
            iniciaGridVendas();
            dtFinal.Value = DateTime.Now;
            DtInicial.Value = DateTime.Now;
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            int i = 3;

            String answer = Interaction.InputBox("Digite o nome do arquivo de relatorio", "", null, -1, -1);

            while (answer == null)
            {
                answer = Interaction.InputBox("Digite o nome do arquivo de relatorio", "", null, -1, -1);
            }



            // Inicia o componente Excel
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;

            Excel.Range rangeTituloVendas;
            Excel.Range rangeValoresVendas;
            Excel.Range rangeTabelaVendas;

            Excel.Range rangeTituloGastos;
            Excel.Range rangeValoresGastos;
            Excel.Range rangeTabelaGastos;

            object misValue = System.Reflection.Missing.Value;

            //Cria uma planilha temporária na memória do computador
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //incluindo dados
            xlWorkSheet.Cells[1, 1] = "Tabela Vendas";
            xlWorkSheet.Cells[2, 1] = "Id Venda";
            xlWorkSheet.Cells[2, 2] = "Classificação";
            xlWorkSheet.Cells[2, 3] = "Cliente";
            xlWorkSheet.Cells[2, 4] = "Cpf/Cnpj";
            xlWorkSheet.Cells[2, 5] = "Carro";
            xlWorkSheet.Cells[2, 6] = "Placa";
            xlWorkSheet.Cells[2, 7] = "Serviço";
            xlWorkSheet.Cells[2, 8] = "Data";
            xlWorkSheet.Cells[2, 9] = "Preço";
            xlWorkSheet.Cells[2, 10] = "Valor Cobrado";
            xlWorkSheet.Cells[2, 11] = "Pagamento";
            xlWorkSheet.Cells[2, 12] = "Forma Pagamento";

            xlWorkSheet.Cells[1, 14] = "Tabela Gastos";
            xlWorkSheet.Cells[2, 14] = "Id";
            xlWorkSheet.Cells[2, 15] = "Setor";
            xlWorkSheet.Cells[2, 16] = "Descrição";
            xlWorkSheet.Cells[2, 17] = "Data Vencimento";
            xlWorkSheet.Cells[2, 18] = "Valor";
            xlWorkSheet.Cells[2, 19] = "Pagamento";
            xlWorkSheet.Cells[2, 20] = "Forma de Pagamento";


            //inclui dados de gastos
            String dataI = DtInicial.Value.ToString("MM/dd/yyyy");
            String dataF = dtFinal.Value.ToString("MM/dd/yyyy");

            List<Gastos> listGastos = new List<Gastos>();
            con.conectar();

            SqlDataReader readerGasto;

            readerGasto = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[pago], [Gastos].[formaPagamento] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'");

            if (readerGasto.HasRows)
            {
                while (readerGasto.Read())
                {
                    xlWorkSheet.Cells[i, 14] = readerGasto.GetInt32(0);
                    xlWorkSheet.Cells[i, 15] = readerGasto.GetString(1);
                    xlWorkSheet.Cells[i, 16] = readerGasto.GetString(2);
                    xlWorkSheet.Cells[i, 17] = readerGasto.GetDateTime(3);
                    xlWorkSheet.Cells[i, 18] = readerGasto.GetDouble(4);
                    xlWorkSheet.Cells[i, 20] = readerGasto.GetString(6);
                    Boolean pg = readerGasto.GetBoolean(5);
                    if (pg == true)
                    {
                        xlWorkSheet.Cells[i, 19] = "PG";
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 19] = "EM ABERTO";
                    }

                    i++;
                }
                lastCell = i - 1;
                xlWorkSheet.Cells[i, 17] = "Total Gastos:";
                xlWorkSheet.Cells[i, 17].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;


                xlWorkSheet.Cells[i, 18].Formula = "=SUM(R2:R" + lastCell + ")";
                xlWorkSheet.Cells[i, 18].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Cells[i, 18].NumberFormat = "R$#,##0.00";
                xlWorkSheet.Calculate();

                readerGasto.Close();
                con.desconectar();

            }

            //Colocando borda 
            rangeTabelaGastos = xlWorkSheet.get_Range("N1", "T" + lastCell);
            rangeTabelaGastos.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            // Alinha colunas
            rangeTabelaGastos.Columns.AutoFit();
            //Colocando negrito nas células de identificação 
            rangeTituloGastos = xlWorkSheet.get_Range("N1", "T2");
            rangeTituloGastos.Font.Bold = true;


            //xlWorkSheet.Range[xlWorkSheet.Cells[13, 1], xlWorkSheet.Cells[19, 1]].Merge();
            xlWorkSheet.get_Range("N1", "T1").Merge();
            xlWorkSheet.get_Range("N1", "T1").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;


            // Colocando R$
            rangeValoresGastos = xlWorkSheet.get_Range("R2", "R" + i);
            rangeValoresGastos.NumberFormat = "R$#,##0.00";
            rangeValoresGastos.Columns.AutoFit();

            i = 3;

            con.conectar();
            SqlDataReader readerVenda;

            readerVenda = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Cliente].[pfpj], [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Vendas].[data], [Servicos].[preco],  [VendasServicos].[valorCobrado], [Vendas].[pago], [Vendas].[formaPagamento] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}'");


            if (readerVenda.HasRows)
            {
                while (readerVenda.Read())
                {


                    xlWorkSheet.Cells[i, 1] = readerVenda.GetInt32(0);

                    Boolean tipo = readerVenda.GetBoolean(1);

                    if (tipo == true)
                    {
                        xlWorkSheet.Cells[i, 2] = "Frotista";
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 2] = "Particular";
                    }

                    xlWorkSheet.Cells[i, 3] = readerVenda.GetString(2).Trim();

                    try
                    {
                        xlWorkSheet.Cells[i, 4] = readerVenda.GetString(3).Trim();
                    }
                    catch
                    {

                    }
                    xlWorkSheet.Cells[i, 5] = readerVenda.GetString(4);
                    xlWorkSheet.Cells[i, 6] = readerVenda.GetString(5);
                    xlWorkSheet.Cells[i, 7] = readerVenda.GetString(6);
                    /*DATA*/
                    xlWorkSheet.Cells[i, 8] = readerVenda.GetDateTime(7);
                    /*PRECO */
                    xlWorkSheet.Cells[i, 9] = readerVenda.GetDouble(8);
                    try
                    {
                        /*VALORcOBRADO */
                        xlWorkSheet.Cells[i, 10] = readerVenda.GetDouble(9);
                    }
                    catch
                    {
                        xlWorkSheet.Cells[i, 10] = readerVenda.GetDouble(8);
                    }

                    Boolean pg = readerVenda.GetBoolean(10);

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
                        xlWorkSheet.Cells[i, 12] = readerVenda.GetString(11);
                    }
                    catch
                    {

                    }
                    i++;
                }
                lastCellVendas = i - 1;
                xlWorkSheet.Cells[i, 8] = "Total:";
                xlWorkSheet.Cells[i, 8].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                xlWorkSheet.Cells[i, 9].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Cells[i, 9].Formula = "=SUM(I2:I" + lastCellVendas + ")";
                xlWorkSheet.Cells[i, 9].NumberFormat = "R$#,##0.00";
                xlWorkSheet.Calculate();

                xlWorkSheet.Cells[i, 10].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Cells[i, 10].Formula = "=SUM(J2:J" + lastCellVendas + ")";
                xlWorkSheet.Cells[i, 10].NumberFormat = "R$#,##0.00";
                xlWorkSheet.Calculate();

                readerVenda.Close();
                con.desconectar();
            }

            //Colocando borda 
            rangeTabelaVendas = xlWorkSheet.get_Range("A1", "L" + lastCellVendas);
            rangeTabelaVendas.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            // Alinha colunas
            rangeTabelaVendas.Columns.AutoFit();

            //Colocando negrito nos titulos
            rangeTituloVendas = xlWorkSheet.get_Range("A1", "L2");
            rangeTituloVendas.Font.Bold = true;

            //Mesclando e centralizando
            xlWorkSheet.get_Range("A1", "L1").Merge();
            xlWorkSheet.get_Range("A1", "L1").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;


            rangeValoresVendas = xlWorkSheet.get_Range("I2", "J" + i);
            rangeValoresVendas.NumberFormat = "R$#,##0.00";
            rangeValoresVendas.Columns.AutoFit();

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

