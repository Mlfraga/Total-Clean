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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;

namespace totalClean
{
    public partial class RelatorioGastos : Form
    {
        Conexao con = new Conexao();
        int lastCell;
        String situacaoGasto;
        int pagamento;
        public RelatorioGastos()
        {
            InitializeComponent();
        }

        private void RelatorioGastos_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            InicialFrm m = new InicialFrm();
            m.Show();
            this.Visible = false;
        }

        private void RelatorioGastos_Load(object sender, EventArgs e)
        {
            preencheCmbSetor();
            iniciarGrid();
            btnGerarRelatorio.Enabled = false;
            btnGeraPdf.Enabled = false;
        }

        private void iniciarGrid()
        {
            List<Gastos> listGastos = new List<Gastos>();
            con.conectar();

            SqlDataReader reader;

            if (rdbOdrdenarPId.Checked == true)
            {
                reader = con.exeCliente("SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] ORDER BY idGasto DESC ");
            }
            else
            {
                reader = con.exeCliente("SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] ORDER BY [Gastos].[data] DESC ");
            }

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

        private void preencheCmbSetor()
        {
            List<Setor> listSetor = new List<Setor>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select * from SetorGastos order by nome ASC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Setor setor = new Setor();
                    setor.id = reader.GetInt32(0);
                    setor.nome = reader.GetString(1);

                    listSetor.Add(setor);

                }
                reader.Close();
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foi encontrado nenhum setor");
            }
            cmbSetor.DataSource = listSetor;
            cmbSetor.ValueMember = "id";
            cmbSetor.DisplayMember = "nome";
            cmbSetor.Text = "";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            iniciarGrid();
            limpaCampos();
            btnGeraPdf.Enabled = false;
            btnGerarRelatorio.Enabled = false;
        }
        private void limpaCampos()
        {
            cmbSetor.Text = "";
            DtInicialVencimento.Value = DateTime.Now;
            DtInicialVencimento.Value = DateTime.Now;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnGerarRelatorio.Enabled = true;
            btnGeraPdf.Enabled = true;
            if (rdbAmbas.Checked == true)
            {
                if (cmbSetor.Text != string.Empty)
                {
                    Gastos edicaoGastos = new Gastos();

                    try
                    {
                        edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor selcionar um setor já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSetor.Text = " ";
                        return;
                    }
                    String dataI = DtInicialVencimento.Value.ToString("MM/dd/yyyy");
                    String dataF = dtFinalVencimento.Value.ToString("MM/dd/yyyy");

                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    SqlDataReader reader;
                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'  ORDER BY idGasto DESC");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'  ORDER BY [Gastos].[data] DESC");
                    }
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Gastos gasto = new Gastos();
                            gasto.id = reader.GetInt32(0);
                            gasto.nome = reader.GetString(1);
                            gasto.descricao = reader.GetString(2);
                            gasto.dataVencimento = reader.GetDateTime(3).ToString("dd/MM/yyy");
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
                        iniciarGrid();
                        cmbSetor.Text = "";
                    }

                }
                else
                {
                    EdicaoGastos edicaoGastos = new EdicaoGastos();


                    String dataI = DtInicialVencimento.Value.ToString("MM/dd/yyyy");
                    String dataF = dtFinalVencimento.Value.ToString("MM/dd/yyyy");

                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    SqlDataReader reader;
                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'  ORDER BY idGasto DESC");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'  ORDER BY [Gastos].[data] DESC");
                    }
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
                        iniciarGrid();
                        cmbSetor.Text = "";
                    }
                }
            }
            else
            {
                if (rdbAberto.Checked == true)
                {
                    pagamento = 0;
                }
                if (rdbPago.Checked == true)
                {
                    pagamento = 1;
                }

                if (cmbSetor.Text != string.Empty)
                {
                    Gastos edicaoGastos = new Gastos();

                    try
                    {
                        edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor selcionar um setor já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSetor.Text = " ";
                        return;
                    }
                    String dataI = DtInicialVencimento.Value.ToString("MM/dd/yyyy");
                    String dataF = dtFinalVencimento.Value.ToString("MM/dd/yyyy");

                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    SqlDataReader reader;
                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY idGasto DESC");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY [Gastos].[data] DESC");
                    }
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Gastos gasto = new Gastos();
                            gasto.id = reader.GetInt32(0);
                            gasto.nome = reader.GetString(1);
                            gasto.descricao = reader.GetString(2);
                            gasto.dataVencimento = reader.GetDateTime(3).ToString("dd/MM/yyy");
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
                        iniciarGrid();
                        cmbSetor.Text = "";
                    }

                }
                else
                {
                    EdicaoGastos edicaoGastos = new EdicaoGastos();

                    if (rdbAberto.Checked == true)
                    {
                        pagamento = 0;
                    }
                    if (rdbPago.Checked == true)
                    {
                        pagamento = 1;
                    }

                    String dataI = DtInicialVencimento.Value.ToString("MM/dd/yyyy");
                    String dataF = dtFinalVencimento.Value.ToString("MM/dd/yyyy");

                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    SqlDataReader reader;
                    if (rdbOdrdenarPId.Checked == true)
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY idGasto DESC");
                    }
                    else
                    {
                        reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY [Gastos].[data] DESC");
                    }
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
                        iniciarGrid();
                        cmbSetor.Text = "";
                    }
                }

            }
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            String dataI = DtInicialVencimento.Value.ToString("MM/dd/yyyy");
            String dataF = dtFinalVencimento.Value.ToString("MM/dd/yyyy");

            Gastos edicaoGastos = new Gastos();
            edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());


            SqlDataReader reader;

            if (rdbAmbas.Checked == true)
            {
                if (cmbSetor.Text == String.Empty)
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'");
                    construtor(reader);

                    con.desconectar();
                }
                else
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[idSetor] = '{edicaoGastos.id}'");
                    construtor(reader);

                    con.desconectar();
                }
            }
            else
            {
                if (rdbAberto.Checked == true)
                {
                    pagamento = 0;
                }
                if (rdbPago.Checked == true)
                {
                    pagamento = 1;
                }

                if (cmbSetor.Text != string.Empty)
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY idGasto DESC");
                    construtor(reader);

                    con.desconectar();
                }
                else
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY idGasto DESC");
                    construtor(reader);

                    con.desconectar();
                }
            }

        }

        private void rdbOrdenarPData_CheckedChanged(object sender, EventArgs e)
        {
            iniciarGrid();
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

            //incluindo dados
            xlWorkSheet.Cells[1, 1] = "Id";
            xlWorkSheet.Cells[1, 2] = "Setor";
            xlWorkSheet.Cells[1, 3] = "Descrição";
            xlWorkSheet.Cells[1, 4] = "Data Vencimento";
            xlWorkSheet.Cells[1, 5] = "Valor";
            xlWorkSheet.Cells[1, 6] = "Forma de Pagamento";
            xlWorkSheet.Cells[1, 7] = "Pago";




            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    xlWorkSheet.Cells[i, 1] = reader.GetInt32(0);
                    xlWorkSheet.Cells[i, 2] = reader.GetString(1);
                    xlWorkSheet.Cells[i, 3] = reader.GetString(2);
                    xlWorkSheet.Cells[i, 4] = reader.GetDateTime(3);
                    xlWorkSheet.Cells[i, 5] = reader.GetDouble(4);
                    xlWorkSheet.Cells[i, 6] = reader.GetString(5);
                    Boolean pg = reader.GetBoolean(6);
                    if (pg == true)
                    {
                        xlWorkSheet.Cells[i, 7] = "PG";
                    }
                    else
                    {
                        xlWorkSheet.Cells[i, 7] = "EM ABERTO";
                    }

                    i++;
                }

                //Atribuindo valores do total
                lastCell = i - 1;
                xlWorkSheet.Cells[i, 4] = "Total:";
                xlWorkSheet.Cells[i, 4].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                xlWorkSheet.Cells[i, 5].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                xlWorkSheet.Cells[i, 5].Formula = "=SUM(E2:E" + lastCell + ")";
                xlWorkSheet.Calculate();
                xlWorkSheet.Cells[i, 5].NumberFormat = "R$#,##0.00";
                xlWorkSheet.Cells[i, 5].Columns.AutoFit();

                reader.Close();
                con.desconectar();

            }

            //Colocando borda 
            rangeTabela = xlWorkSheet.get_Range("A1", "G" + lastCell);
            rangeTabela.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

            //Colocando cores nas células de identificação 
            rangeTitulo = xlWorkSheet.get_Range("A1", "L1");
            //rangeTitulo.Interior.Color = ColorTranslator.FromHtml("#9ea7aa");
            rangeTitulo.Font.Bold = true;

            // Colocando R$
            rangeValores = xlWorkSheet.get_Range("E2", "E" + i);
            rangeValores.NumberFormat = "R$#,##0.00";

            rangeTabela.Columns.AutoFit();
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

        private void btnGeraPdf_Click(object sender, EventArgs e)
        {
            String dataI = DtInicialVencimento.Value.ToString("MM/dd/yyyy");
            String dataF = dtFinalVencimento.Value.ToString("MM/dd/yyyy");

            Gastos edicaoGastos = new Gastos();
            edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());


            SqlDataReader reader;

            if (rdbAmbas.Checked == true)
            {
                if (cmbSetor.Text == String.Empty)
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'");
                    construtorPdf(reader);

                    con.desconectar();
                }
                else
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[idSetor] = '{edicaoGastos.id}'");
                    construtorPdf(reader);

                    con.desconectar();
                }
            }
            else
            {
                if (rdbAberto.Checked == true)
                {
                    pagamento = 0;
                }
                if (rdbPago.Checked == true)
                {
                    pagamento = 1;
                }

                if (cmbSetor.Text != string.Empty)
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [SetorGastos].[idSetor] = '{edicaoGastos.id}' AND [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY idGasto DESC");
                    construtorPdf(reader);

                    con.desconectar();
                }
                else
                {
                    List<Gastos> listGastos = new List<Gastos>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[pago] = '{pagamento}' ORDER BY idGasto DESC");
                    construtorPdf(reader);

                    con.desconectar();
                }
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

            if (answer == string.Empty)
            {
                return;
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


            BaseColor preto = new BaseColor(0, 0, 0);
            iTextSharp.text.Font font = FontFactory.GetFont("Roboto", 8, iTextSharp.text.Font.NORMAL, preto);
            iTextSharp.text.Font nomeRelatorio = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);
            iTextSharp.text.Font titulo = FontFactory.GetFont("Roboto", 12, iTextSharp.text.Font.BOLD, preto);
            float[] sizes = new float[] { 1f, 3f, 1f };

            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = doc.PageSize.Width - (doc.LeftMargin + doc.RightMargin);
            table.SetWidths(sizes);

            #region Logo Empresa
            iTextSharp.text.Image foot;

            foot = iTextSharp.text.Image.GetInstance(@"C:\Program Files (x86)\Total Clean\Total Clean\favicon_tc.png");

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
            cell = new PdfPCell(new Phrase("Relátórios de gastos", nomeRelatorio));
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
            cell = new PdfPCell(new Phrase("Data emissão: " + DateTime.Today.ToString("dd/MM/yyyy"), font));
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

            #region Filtros
            iTextSharp.text.Font fontPeriodo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);

            Paragraph periodo = new Paragraph((new Phrase("\nPeríodo de tempo pesquisado: \n" + DtInicialVencimento.Value.ToString("dd/MM/yyyy") + " a " + dtFinalVencimento.Value.ToString("dd/MM/yyyy") + "\n", fontPeriodo)));
            periodo.Alignment = Element.ALIGN_LEFT;
            //   doc.Add(periodo);


            Paragraph tpSetor = new Paragraph((new Phrase("\nGastos pesquisados pelo setor: \n" + cmbSetor.Text + "\n", fontPeriodo)));
            tpSetor.Alignment = Element.ALIGN_LEFT;
            //    doc.Add(tpCliente);



            if (rdbAberto.Checked == true)
            {
                situacaoGasto = "em aberto";
            }
            if (rdbAmbas.Checked == true)
            {
                situacaoGasto = "em aberto e pagos";
            }
            if (rdbPago.Checked == true)
            {
                situacaoGasto = "pagos";
            }
            Paragraph situacao = new Paragraph((new Phrase("\nPesquisando por gastos\n" + situacaoGasto + "\n", fontPeriodo)));
            situacao.Alignment = Element.ALIGN_LEFT;




            Paragraph filtros = new Paragraph((new Phrase("\n\n\nFiltros:", fontPeriodo)));
            filtros.Alignment = Element.ALIGN_LEFT;
            doc.Add(filtros);

            PdfPTable tableFiltros = new PdfPTable(3);
            float[] colsTableFiltros = { 15, 15, 15 };
            tableFiltros.SetWidths(colsTableFiltros);

            tableFiltros.WidthPercentage = 100f;

            tableFiltros.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            tableFiltros.AddCell(periodo);
            tableFiltros.AddCell(tpSetor);
            tableFiltros.AddCell(situacao);

            doc.Add(tableFiltros);

            #endregion


            construtorCorpoDados(reader, doc);

            doc.AddCreator("Matheus Fraga");


            doc.Close();
            System.Diagnostics.Process.Start(caminho);
        }

        public void construtorCorpoDados(SqlDataReader reader, Document doc)
        {
            double somaPGasto = 0;
            int lastId = 0;
            int cont = 1;
            BaseColor preto = new BaseColor(0, 0, 0);
            iTextSharp.text.Font font = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.NORMAL, preto);
            iTextSharp.text.Font titulo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);
            iTextSharp.text.Font fontPeriodo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);

            BaseColor branco = new BaseColor(255, 255, 255);


            double totalSomaTotal = 0;
            double totalSomaSubTotal = 0;


            Gastos gastos = new Gastos();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    gastos.id = reader.GetInt32(0);

                    if (gastos.id != lastId)
                    {
                        if (cont != 1)
                        {
                            PdfPTable tableTotal = new PdfPTable(3);
                            float[] colstabletableTotal = { 25, 5, 10 };
                            tableTotal.SetWidths(colstabletableTotal);
                            tableTotal.WidthPercentage = 100f;

                            tableTotal.DefaultCell.Border = PdfPCell.NO_BORDER;

                            tableTotal.AddCell(getNewCell("Total:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                            tableTotal.AddCell(getNewCell(somaPGasto.ToString("c"), titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                            tableTotal.AddCell(getNewCell("", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                            somaPGasto = 0;

                            doc.Add(tableTotal);
                        }
                    }

                    gastos.nome = reader.GetString(1);

                    gastos.descricao = reader.GetString(2);

                    gastos.dataVencimento = reader.GetDateTime(3).ToShortDateString();

                    gastos.valor = reader.GetDouble(4);

                    gastos.formaPagamento = reader.GetString(5);

                    gastos.pago = reader.GetBoolean(6);

                    String situacao;

                    if (gastos.pago == true)
                    {
                        situacao = "pago";
                    }
                    else
                    {
                        situacao = "em aberto";
                    }

                    PdfPTable tableTitulos = new PdfPTable(4);
                    BaseColor fundo = new BaseColor(200, 200, 200);


                    float[] colsW = { 5, 15, 20, 10 };
                    tableTitulos.SetWidths(colsW);
                    tableTitulos.WidthPercentage = 100f;

                    tableTitulos.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
                    tableTitulos.DefaultCell.BorderColor = preto;
                    tableTitulos.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);

                    tableTitulos.KeepTogether = true;
                    tableTitulos.KeepRowsTogether(0);


                    tableTitulos.AddCell(getNewCell("Id:", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell("Setor:", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell("Data Vencimento", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell("Situação", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));

                    tableTitulos.AddCell(getNewCell(gastos.id.ToString(), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell(gastos.nome, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell(gastos.dataVencimento, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell(situacao, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));


                    PdfPTable tableDados = new PdfPTable(3);
                    float[] colstabletableDados = { 25, 5, 10 };
                    tableDados.SetWidths(colstabletableDados);
                    tableDados.WidthPercentage = 100f;

                    tableDados.DefaultCell.Border = PdfPCell.NO_BORDER;

                    Paragraph espaco = new Paragraph((new Phrase("\n")));
                    doc.Add(espaco);

                    tableDados.AddCell(getNewCell("Descrição:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell("Valor:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell("Forma de pagamento:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));

                    tableDados.AddCell(getNewCell(gastos.descricao, font, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell(gastos.valor.ToString("c"), font, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell(gastos.formaPagamento, font, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));

                    somaPGasto += gastos.valor;
                    totalSomaTotal += gastos.valor;

                    Console.WriteLine("teste");

                    tableDados.KeepTogether = true;
                    tableDados.KeepRowsTogether(0);

                    doc.Add(tableTitulos);
                    doc.Add(tableDados);
                    cont++;






                }

                PdfPTable tableUltimoTotal = new PdfPTable(3);
                float[] colstableUltimoTotal = { 25, 5, 10 };
                tableUltimoTotal.SetWidths(colstableUltimoTotal);
                tableUltimoTotal.WidthPercentage = 100f;
                tableUltimoTotal.PaddingTop = 5;

                tableUltimoTotal.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;

                tableUltimoTotal.AddCell(getNewCell("Total:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                tableUltimoTotal.AddCell(getNewCell(somaPGasto.ToString("c"), titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                tableUltimoTotal.AddCell(getNewCell("", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));

                tableUltimoTotal.KeepTogether = true;
                //E ou...
                tableUltimoTotal.KeepRowsTogether(0);

                doc.Add(tableUltimoTotal);


                Paragraph espaco2 = new Paragraph((new Phrase("\n\n\n")));
                doc.Add(espaco2);


                PdfPTable tableSomaFinal = new PdfPTable(2);
                float[] colsTableSomaFinal = { 10, 10 };
                tableSomaFinal.SetWidths(colsTableSomaFinal);
                tableSomaFinal.WidthPercentage = 100f;
                tableSomaFinal.PaddingTop = 5;

                tableSomaFinal.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;

                tableSomaFinal.AddCell(getNewCell("Soma Final:", titulo, Element.ALIGN_CENTER, 2, PdfPCell.TOP_BORDER, preto, branco));
                tableSomaFinal.AddCell(getNewCell("", font, Element.ALIGN_LEFT, 2, PdfPCell.TOP_BORDER, preto, branco));

                tableSomaFinal.AddCell(getNewCell("Total:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));
                tableSomaFinal.AddCell(getNewCell(totalSomaTotal.ToString("c"), font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, branco));

                tableSomaFinal.KeepTogether = true;
                tableSomaFinal.KeepRowsTogether(0);

                doc.Add(tableSomaFinal);
            }



        }
    }
}
