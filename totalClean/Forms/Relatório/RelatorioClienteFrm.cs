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
    public partial class RelatorioClienteFrm : Form
    {
        public RelatorioClienteFrm()
        {
            InitializeComponent();
        }

        Conexao con = new Conexao();
        private void RelatorioClienteFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            InicialFrm n = new InicialFrm();
            n.Show();
            this.Visible = false;
        }

        private void RelatorioClienteFrm_Load(object sender, EventArgs e)
        {
            iniciaGrid();
            PreencheCmbServico();
            prencheCmbCliente();
            btnGerarRelatorio.Enabled = false;
        }
        private void iniciaGrid()
        {
            List<ServicoVenda> listVendasServicos = new List<ServicoVenda>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico");

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
                    sv.preco = reader.GetDouble(6);
                    sv.data = reader.GetDateTime(7);
                    sv.pago = reader.GetBoolean(8);

                    listVendasServicos.Add(sv);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvVendasClientes.DataSource = null;
            dgvVendasClientes.DataSource = listVendasServicos;

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

        private void btnPesquisar_Click_1(object sender, EventArgs e)
        {
            if (cmbCliente.Text != string.Empty)
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

                    int cliente = int.Parse(cmbCliente.SelectedValue.ToString());
                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}' AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");


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
                            servico.preco = reader.GetDouble(6);
                            servico.data = reader.GetDateTime(7);
                            servico.pago = reader.GetBoolean(8);

                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        dgvVendasClientes.DataSource = null;
                        dgvVendasClientes.DataSource = listServicoVenda;

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
                    
                    int cliente = int.Parse(cmbCliente.SelectedValue.ToString());

                    List<ServicoVenda> listServicoVenda = new List<ServicoVenda>();
                    con.conectar();

                    SqlDataReader reader;

                    reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}' AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}'");

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
                            servico.preco = reader.GetDouble(6);
                            servico.data = reader.GetDateTime(7);
                            servico.pago = reader.GetBoolean(8);

                            listServicoVenda.Add(servico);
                        }
                        reader.Close();
                        dgvVendasClientes.DataSource = null;
                        dgvVendasClientes.DataSource = listServicoVenda;

                    }

                    else
                    {
                        btnGerarRelatorio.Enabled = false;
                        MessageBox.Show("Não foi encontrado nenhum dado conforme a pesquisa feita ", "ERRO", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por Favor selecione um cliente!!!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciaGrid();
            limpaCampos();
            btnGerarRelatorio.Enabled = false;
        }

        private void limpaCampos()
        {
            cmbCliente.Text = "";
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
            xlWorkSheet.Cells[1, 2] = "Tipo";
            xlWorkSheet.Cells[1, 3] = "Cliente";
            xlWorkSheet.Cells[1, 4] = "Carro";
            xlWorkSheet.Cells[1, 5] = "Placa";
            xlWorkSheet.Cells[1, 6] = "Serviço";
            xlWorkSheet.Cells[1, 7] = "Preço";
            xlWorkSheet.Cells[1, 8] = "Data";
            xlWorkSheet.Cells[1, 9] = "Pagamento";
            

            Classes.VendasServicos vs = new Classes.VendasServicos();

            DateTime dataI = DtInicialVenda.Value;
            DateTime dataF = dtFinalVenda.Value;
            int cliente = int.Parse(cmbCliente.SelectedValue.ToString());

            SqlDataReader reader;

            if (cmbServico.Text != string.Empty)
            {
                vs.servico1 = int.Parse(cmbServico.SelectedValue.ToString());
                reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}' AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}' AND [VendasServicos].idServico = ('{vs.servico1}')");
            }
            else
            {
                reader = con.exeCliente($"SELECT [VendasServicos].[idVenda], [Cliente].[frotista], [Cliente].[nome] as 'Cliente', [Vendas].[carro], [Vendas].[placa], [Servicos].[nome] as 'Serviço', [Servicos].[preco], [Vendas].[data], [Vendas].[pago] FROM [VendasServicos] INNER JOIN Vendas ON ([VendasServicos].[idVenda] = [Vendas].[idVenda])INNER JOIN Cliente ON Vendas.idCliente = Cliente.idCliente INNER JOIN Servicos ON [VendasServicos].idServico = Servicos.idServico WHERE [Cliente].[idCliente] = '{cliente}' AND [Vendas].[data] BETWEEN '{dataI}' AND '{dataF}'");
            }


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
                    xlWorkSheet.Cells[i, 4] = reader.GetString(3);
                    xlWorkSheet.Cells[i, 5] = reader.GetString(4);
                    xlWorkSheet.Cells[i, 6] = reader.GetString(5);
                    xlWorkSheet.Cells[i, 7] = reader.GetDouble(6);
                    xlWorkSheet.Cells[i, 8] = reader.GetDateTime(7);
                   
                    Boolean pg = reader.GetBoolean(8);

                    if (pg == true)
                    {
                        xlWorkSheet.Cells[i, 9] = "PG";
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 9] = "EM ABERTO";
                    }
                    i++;
                }                
                int lastCell = i - 1;
                xlWorkSheet.Cells[i, 6] = "Total:";
                xlWorkSheet.Cells[i, 7] = "=SOMA(G2:G" + lastCell + ")";
                reader.Close();
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
