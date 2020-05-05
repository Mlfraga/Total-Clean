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
    public partial class RelatorioGastos : Form
    {
        Conexao con = new Conexao();
        int lastCell;
        public RelatorioGastos()
        {
            InitializeComponent();
        }

        private void RelatorioGastos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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
                    gasto.dataVencimento = reader.GetDateTime(3);
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
                DateTime dataI = DtInicialVencimento.Value;
                DateTime dataF = dtFinalVencimento.Value;

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
                        gasto.dataVencimento = reader.GetDateTime(3);
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


                DateTime dataI = DtInicialVencimento.Value;
                DateTime dataF = dtFinalVencimento.Value;

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
                        gasto.dataVencimento = reader.GetDateTime(3);
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

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            DateTime dataI = DtInicialVencimento.Value;
            DateTime dataF = dtFinalVencimento.Value;

            Gastos edicaoGastos = new Gastos();
            edicaoGastos.id = int.Parse(cmbSetor.SelectedValue.ToString());


            SqlDataReader reader;

            if (cmbSetor.Text == String.Empty)
            {
                List<Gastos> listGastos = new List<Gastos>();
                con.conectar();

                reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}'");

            }
            else
            {               
                List<Gastos> listGastos = new List<Gastos>();
                con.conectar();

                reader = con.exeCliente($"SELECT [Gastos].[idGasto], [SetorGastos].[nome] as 'Setor Gasto', [Gastos].[descricao] as 'Descriçao', [Gastos].[data], [Gastos].[valor], [Gastos].[formaPagamento], [Gastos].[pago] FROM [dbo].[Gastos] INNER JOIN SetorGastos ON [Gastos].[idSetor] = [SetorGastos].[idSetor] WHERE [Gastos].[data] BETWEEN '{dataI}' AND '{dataF}' AND [Gastos].[idSetor] = '{edicaoGastos.id}'");
            }

            construtor(reader);
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
    }
}
