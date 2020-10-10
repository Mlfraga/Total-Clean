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
using Microsoft.Win32.SafeHandles;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace totalClean
{
    public partial class RelatorioDeCliente : Form
    {
        public RelatorioDeCliente()
        {
            InitializeComponent();
        }
        Conexao con = new Conexao();
        int tpCliente;
        String tpClientepdf;
        private void RelatorioDeCliente_Load(object sender, EventArgs e)
        {
            chkPesquisaEspecifica.Checked = false;
            txtNome.Visible = false;
            txtCpf.Visible = false;
            lblNome.Visible = false;
            lblCpf.Visible = false;
            bnfspCpf.Visible = false;
            bnfspNome.Visible = false;

            btnGerarRelatorio.Enabled = false;
            btnGeraPdf.Enabled = false;
            iniciarGrid();
        }

        private void iniciarGrid()
        {
            List<RelatorioCliente> listCliente = new List<RelatorioCliente>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select frotista, nome, telefone, endereco, pfpj from Cliente order by idCliente DESC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RelatorioCliente cliente = new RelatorioCliente();

                    cliente.frotista = reader.GetBoolean(0);
                    cliente.nome = reader.GetString(1);
                    cliente.telefone = reader.GetString(2);
                    cliente.endereco = reader.GetString(3);

                    try
                    {
                        cliente.cpf = reader.GetString(4);
                    }
                    catch (Exception)
                    {

                    }



                    listCliente.Add(cliente);
                }
                reader.Close();
                con.desconectar();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = listCliente;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
            btnGeraPdf.Enabled = true;
            if (txtCpf.Text != string.Empty || txtNome.Text != string.Empty)
            {
                btnGerarRelatorio.Enabled = true;

                List<RelatorioCliente> listCliente = new List<RelatorioCliente>();
                con.conectar();

                SqlDataReader reader;

                String pfpj = txtCpf.Text;
                String nome = txtNome.Text;

                if (txtCpf.Text == string.Empty && txtNome.Text != string.Empty)
                {

                    reader = con.exeCliente($"SELECT frotista, nome, telefone, endereco, pfpj FROM Cliente WHERE Nome LIKE ('%{nome}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.frotista = reader.GetBoolean(0);
                            cliente.nome = reader.GetString(1);
                            cliente.telefone = reader.GetString(2);
                            cliente.endereco = reader.GetString(3);

                            try
                            {
                                cliente.cpf = reader.GetString(4);
                            }
                            catch
                            {

                            }


                            listCliente.Add(cliente);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvClientes.DataSource = null;
                        dgvClientes.DataSource = listCliente;
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum cliente com nome parecido com " + nome, "ERRO", MessageBoxButtons.OK);
                    }
                }

                if (txtCpf.Text != string.Empty && txtNome.Text == string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT frotista nome, telefone, endereco, pfpj FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.frotista = reader.GetBoolean(0);
                            cliente.nome = reader.GetString(1);
                            cliente.telefone = reader.GetString(2);
                            cliente.endereco = reader.GetString(3);

                            try
                            {
                                cliente.cpf = reader.GetString(4);
                            }
                            catch
                            {

                            }


                            listCliente.Add(cliente);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvClientes.DataSource = null;
                        dgvClientes.DataSource = listCliente;
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum cliente com cpf parecido com " + pfpj, "ERRO", MessageBoxButtons.OK);
                    }
                }

                if (txtCpf.Text != string.Empty && txtNome.Text != string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT frotista, nome, telefone, endereco, pfpj FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') AND Nome LIKE ('%{nome}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.frotista = reader.GetBoolean(0);
                            cliente.nome = reader.GetString(1);
                            cliente.telefone = reader.GetString(2);
                            cliente.endereco = reader.GetString(3);

                            try
                            {
                                cliente.cpf = reader.GetString(4);
                            }
                            catch
                            {

                            }


                            listCliente.Add(cliente);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvClientes.DataSource = null;
                        dgvClientes.DataSource = listCliente;
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum cliente com os dados pesquisados", "ERRO", MessageBoxButtons.OK);
                        return;
                    }
                }
            }

            else
            {
                if (rdbAmbos.Checked == true)
                {
                    btnGerarRelatorio.Enabled = true;

                    List<RelatorioCliente> listCliente = new List<RelatorioCliente>();
                    con.conectar();

                    SqlDataReader reader;

                    reader = con.exeCliente($"SELECT frotista, nome, telefone, endereco, pfpj FROM Cliente order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.frotista = reader.GetBoolean(0);
                            cliente.nome = reader.GetString(1);
                            cliente.telefone = reader.GetString(2);
                            cliente.endereco = reader.GetString(3);

                            try
                            {
                                cliente.cpf = reader.GetString(4);
                            }
                            catch
                            {

                            }


                            listCliente.Add(cliente);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvClientes.DataSource = null;
                        dgvClientes.DataSource = listCliente;
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum cliente", "ERRO", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (rdbFrotistas.Checked == true)
                    {
                        tpCliente = 1;
                    }

                    if (rdbParticulares.Checked == true)
                    {
                        tpCliente = 0;
                    }

                    btnGerarRelatorio.Enabled = true;

                    List<RelatorioCliente> listCliente = new List<RelatorioCliente>();
                    con.conectar();

                    SqlDataReader reader;

                    reader = con.exeCliente($"SELECT frotista, nome, telefone, endereco, pfpj FROM Cliente WHERE frotista = {tpCliente} order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.frotista = reader.GetBoolean(0);
                            cliente.nome = reader.GetString(1);
                            cliente.telefone = reader.GetString(2);
                            cliente.endereco = reader.GetString(3);

                            try
                            {
                                cliente.cpf = reader.GetString(4);
                            }
                            catch
                            {

                            }


                            listCliente.Add(cliente);
                        }
                        reader.Close();
                        con.desconectar();
                        dgvClientes.DataSource = null;
                        dgvClientes.DataSource = listCliente;
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum cliente ", "ERRO", MessageBoxButtons.OK);
                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnGerarRelatorio.Enabled = false;
            btnGeraPdf.Enabled = false;
            iniciarGrid();
            limpaCampos();
        }

        private void limpaCampos()
        {
            txtCpf.Text = "";
            txtNome.Text = "";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            InicialFrm n = new InicialFrm();
            n.Show();
            this.Visible = false;
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
            Excel.Range rangeTitulo;
            Excel.Range rangeValores;
            Excel.Range rangeTabela;
            object misValue = System.Reflection.Missing.Value;

            //Cria uma planilha temporária na memória do computador
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //incluindo dados
            xlWorkSheet.Cells[1, 1] = "Lista de Clientes";
            xlWorkSheet.Cells[2, 1] = "Nome";
            xlWorkSheet.Cells[2, 2] = "Telefone";
            xlWorkSheet.Cells[2, 3] = "Endereço";
            xlWorkSheet.Cells[2, 4] = "Cpf ou Cnpj";

            con.conectar();
            SqlDataReader reader;

            String pfpj = txtCpf.Text;
            String nome = txtNome.Text;

            if (txtCpf.Text != string.Empty || txtNome.Text != string.Empty)
            {

                if (txtCpf.Text == string.Empty && txtNome.Text != string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj FROM Cliente WHERE Nome LIKE ('%{nome}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            xlWorkSheet.Cells[i, 1] = reader.GetString(0);
                            xlWorkSheet.Cells[i, 2] = reader.GetString(1).Trim();
                            try
                            {
                                xlWorkSheet.Cells[i, 3] = reader.GetString(2).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 3] = "";
                            }

                            try
                            {
                                xlWorkSheet.Cells[i, 4] = reader.GetString(3).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 4] = "";
                            }
                            i++;
                        }
                    }
                    con.desconectar();
                }

                if (txtCpf.Text != string.Empty && txtNome.Text == string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            xlWorkSheet.Cells[i, 1] = reader.GetString(0);
                            xlWorkSheet.Cells[i, 2] = reader.GetString(1).Trim();
                            try
                            {
                                xlWorkSheet.Cells[i, 3] = reader.GetString(2).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 3] = "";
                            }

                            try
                            {
                                xlWorkSheet.Cells[i, 4] = reader.GetString(3).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 4] = "";
                            }
                            i++;
                        }
                    }
                    con.desconectar();
                }

                if (txtCpf.Text != string.Empty && txtNome.Text != string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT  nome, telefone, endereco, pfpj FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') AND Nome LIKE ('%{nome}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            xlWorkSheet.Cells[i, 1] = reader.GetString(0);
                            xlWorkSheet.Cells[i, 2] = reader.GetString(1).Trim();

                            try
                            {
                                xlWorkSheet.Cells[i, 3] = reader.GetString(2).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 3] = "";
                            }

                            try
                            {
                                xlWorkSheet.Cells[i, 4] = reader.GetString(3).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 4] = "";
                            }

                            i++;
                        }
                    }
                    con.desconectar();
                }
            }

            else
            {
                Console.WriteLine("teste");
                if (rdbAmbos.Checked == true)
                {
                    btnGerarRelatorio.Enabled = true;

                    List<RelatorioCliente> listCliente = new List<RelatorioCliente>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj FROM Cliente order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            xlWorkSheet.Cells[i, 1] = reader.GetString(0);
                            xlWorkSheet.Cells[i, 2] = reader.GetString(1).Trim();

                            try
                            {
                                xlWorkSheet.Cells[i, 3] = reader.GetString(2).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 3] = "";
                            }

                            try
                            {
                                xlWorkSheet.Cells[i, 4] = reader.GetString(3).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 4] = "";
                            }

                            i++;
                        }
                        reader.Close();
                        con.desconectar();
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum cliente", "ERRO", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (rdbFrotistas.Checked == true)
                    {
                        tpCliente = 1;
                    }

                    if (rdbParticulares.Checked == true)
                    {
                        tpCliente = 0;
                    }

                    btnGerarRelatorio.Enabled = true;

                    List<RelatorioCliente> listCliente = new List<RelatorioCliente>();
                    con.conectar();

                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj FROM Cliente WHERE frotista = {tpCliente} order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            xlWorkSheet.Cells[i, 1] = reader.GetString(0);
                            xlWorkSheet.Cells[i, 2] = reader.GetString(1).Trim();

                            try
                            {
                                xlWorkSheet.Cells[i, 3] = reader.GetString(2).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 3] = "";
                            }

                            try
                            {
                                xlWorkSheet.Cells[i, 4] = reader.GetString(3).Trim();
                            }
                            catch
                            {
                                xlWorkSheet.Cells[i, 4] = "";
                            }

                            i++;
                        }
                        reader.Close();
                        con.desconectar();

                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum cliente ", "ERRO", MessageBoxButtons.OK);
                    }
                }
            }
            rangeTitulo = xlWorkSheet.get_Range("A1", "D2");
            rangeTitulo.Font.Bold = true;

            rangeTabela = xlWorkSheet.get_Range("A1", "D" + (i - 1));
            rangeTabela.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            rangeTabela.Columns.AutoFit();

            xlWorkSheet.get_Range("A1", "D1").Merge();
            xlWorkSheet.get_Range("A1", "D1").HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;

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



        private void RelatorioDeCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void chkPesquisaEspecifica_OnChange(object sender, EventArgs e)
        {
            if (chkPesquisaEspecifica.Checked == true)
            {
                gpboxCliente.Visible = false;

                rdbParticulares.Visible = false;
                txtNome.Visible = true;
                txtCpf.Visible = true;
                lblNome.Visible = true;
                lblCpf.Visible = true;
                bnfspCpf.Visible = true;
                bnfspNome.Visible = true;
            }
            else
            {
                gpboxCliente.Visible = true;

                rdbParticulares.Visible = true;
                txtNome.Visible = false;
                txtCpf.Visible = false;
                lblNome.Visible = false;
                lblCpf.Visible = false;
                bnfspCpf.Visible = false;
                bnfspNome.Visible = false;
                txtNome.Text = "";
                txtCpf.Text = "";

            }
        }

        private void btnGeraPdf_Click(object sender, EventArgs e)
        {
            con.conectar();
            SqlDataReader reader;

            String pfpj = txtCpf.Text;
            String nome = txtNome.Text;

            if (txtCpf.Text != string.Empty || txtNome.Text != string.Empty)
            {

                if (txtCpf.Text == string.Empty && txtNome.Text != string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj, frotista FROM Cliente WHERE Nome LIKE ('%{nome}%') order by idCliente DESC");
                    construtorPdf(reader);
                    con.desconectar();
                }
                if (txtCpf.Text != string.Empty && txtNome.Text == string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj, frotista FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') order by idCliente DESC");
                    construtorPdf(reader);
                    con.desconectar();
                }
                if (txtCpf.Text != string.Empty && txtNome.Text != string.Empty)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT  nome, telefone, endereco, pfpj, frotista FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') AND Nome LIKE ('%{nome}%') order by idCliente DESC");
                    construtorPdf(reader);
                    con.desconectar();
                }
            }
            else
            {
                if (rdbAmbos.Checked == true)
                {
                    con.conectar();
                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj, frotista FROM Cliente order by idCliente DESC");
                    construtorPdf(reader);
                    con.desconectar();
                }
                else
                {
                    if (rdbFrotistas.Checked == true)
                    {
                        tpCliente = 1;
                    }

                    if (rdbParticulares.Checked == true)
                    {
                        tpCliente = 0;
                    }

                    con.conectar();

                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj, frotista FROM Cliente WHERE frotista = {tpCliente} order by idCliente DESC");
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
            cell = new PdfPCell(new Phrase("Relátórios de clientes", nomeRelatorio));
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



            if (rdbParticulares.Checked == true)
            {
                tpClientepdf = "particulares";
            }
            if (rdbFrotistas.Checked == true)
            {
                tpClientepdf = "frotistas";
            }
            if (rdbAmbos.Checked == true)
            {
                tpClientepdf = "todos";
            }


            Paragraph tpCliente = new Paragraph((new Phrase("\nTipo de cliente pesquisado: \n" + tpClientepdf + "\n", fontPeriodo)));
            tpCliente.Alignment = Element.ALIGN_LEFT;
            //   doc.Add(periodo);


            Paragraph nome = new Paragraph((new Phrase("\nClientes pesquisados por nomes parecido com: \n" + txtNome.Text + "\n", fontPeriodo)));
            nome.Alignment = Element.ALIGN_LEFT;
            //    doc.Add(tpCliente);


            Paragraph cpf = new Paragraph((new Phrase("\nClientes pesquisados por cpf's parecido com: \n" + txtCpf.Text + "\n", fontPeriodo)));
            cpf.Alignment = Element.ALIGN_LEFT;

            Paragraph filtros = new Paragraph((new Phrase("\n\n\nFiltros:", fontPeriodo)));
            filtros.Alignment = Element.ALIGN_LEFT;
            doc.Add(filtros);

            PdfPTable tableFiltros = new PdfPTable(3);
            float[] colsTableFiltros = { 15, 15, 15 };
            tableFiltros.SetWidths(colsTableFiltros);

            tableFiltros.WidthPercentage = 100f;

            tableFiltros.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
            tableFiltros.AddCell(tpCliente);
            tableFiltros.AddCell(nome);
            tableFiltros.AddCell(cpf);

            doc.Add(tableFiltros);

            #endregion


            construtorCorpoDados(reader, doc);

            doc.AddCreator("Matheus Fraga");


            doc.Close();
            System.Diagnostics.Process.Start(caminho);
        }

        public void construtorCorpoDados(SqlDataReader reader, Document doc)
        {
            BaseColor preto = new BaseColor(0, 0, 0);
            iTextSharp.text.Font font = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.NORMAL, preto);
            iTextSharp.text.Font titulo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);
            iTextSharp.text.Font fontPeriodo = FontFactory.GetFont("Roboto", 10, iTextSharp.text.Font.BOLD, preto);

            BaseColor branco = new BaseColor(255, 255, 255);



            RelatorioCliente clientes = new RelatorioCliente();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    clientes.nome = reader.GetString(0);
                    clientes.telefone = reader.GetString(1);
                    clientes.endereco = reader.GetString(2);
                 
                    try
                    {
                        clientes.cpf = reader.GetString(3);
                    }
                    catch (Exception)
                    {
                        clientes.cpf = "";
                    }

                    clientes.frotista = reader.GetBoolean(4);

                    String categoria;

                    if (clientes.frotista == true)
                    {
                        categoria = "FROTISTA";
                    }
                    else
                    {
                        categoria = "PARTICULAR";
                    }

                    PdfPTable tableTitulos = new PdfPTable(2);
                    BaseColor fundo = new BaseColor(200, 200, 200);


                    float[] colsW = { 40, 10 };
                    tableTitulos.SetWidths(colsW);
                    tableTitulos.WidthPercentage = 100f;

                    tableTitulos.DefaultCell.Border = PdfPCell.BOTTOM_BORDER;
                    tableTitulos.DefaultCell.BorderColor = preto;
                    tableTitulos.DefaultCell.BorderColorBottom = new BaseColor(255, 255, 255);

                    tableTitulos.KeepTogether = true;
                    tableTitulos.KeepRowsTogether(2);


                    tableTitulos.AddCell(getNewCell("Nome: ", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell("Categoria: ", fontPeriodo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, fundo));

                    tableTitulos.AddCell(getNewCell(clientes.nome, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));
                    tableTitulos.AddCell(getNewCell(categoria, font, Element.ALIGN_LEFT, 2, PdfPCell.BOTTOM_BORDER, preto, fundo));


                    PdfPTable tableDados = new PdfPTable(2);
                    float[] colstabletableDados = { 10, 15 };
                    tableDados.SetWidths(colstabletableDados);
                    tableDados.WidthPercentage = 100f;

                    tableDados.DefaultCell.Border = PdfPCell.NO_BORDER;

                    Paragraph espaco = new Paragraph((new Phrase("\n")));
                    doc.Add(espaco);

                    tableDados.AddCell(getNewCell("Endereço:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell(clientes.endereco, font, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell("Cpf/Cnpj:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell(clientes.cpf, font, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell("Telefone:", titulo, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));
                    tableDados.AddCell(getNewCell(clientes.telefone, font, Element.ALIGN_LEFT, 2, PdfPCell.NO_BORDER, preto, branco));

                    tableDados.KeepTogether = true;
                    tableDados.KeepRowsTogether(10);

                    doc.Add(tableTitulos);
                    doc.Add(tableDados);

                }

            }



        }

    }
}