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

namespace totalClean
{
    public partial class RelatorioDeCliente : Form
    {
        public RelatorioDeCliente()
        {
            InitializeComponent();
        }
        Conexao con = new Conexao();

        private void RelatorioDeCliente_Load(object sender, EventArgs e)
        {
            btnGerarRelatorio.Enabled = false;
            iniciarGrid();
        }

        private void iniciarGrid()
        {
            List<RelatorioCliente> listCliente = new List<RelatorioCliente>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select nome, telefone, endereco, pfpj from Cliente order by idCliente DESC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    RelatorioCliente cliente = new RelatorioCliente();

                    cliente.nome = reader.GetString(0);
                    cliente.telefone = reader.GetString(1);
                    cliente.endereco = reader.GetString(2);

                    try
                    {
                        cliente.cpf = reader.GetString(3);
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

                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj FROM Cliente WHERE Nome LIKE ('%{nome}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.nome = reader.GetString(0);
                            cliente.telefone = reader.GetString(1);
                            cliente.endereco = reader.GetString(2);

                            try
                            {
                                cliente.cpf = reader.GetString(3);
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
                    reader = con.exeCliente($"SELECT nome, telefone, endereco, pfpj FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.nome = reader.GetString(0);
                            cliente.telefone = reader.GetString(1);
                            cliente.endereco = reader.GetString(2);

                            try
                            {
                                cliente.cpf = reader.GetString(3);
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
                    reader = con.exeCliente($"SELECT  nome, telefone, endereco, pfpj FROM Cliente WHERE pfpj LIKE ('%{pfpj}%') AND Nome LIKE ('%{nome}%') order by idCliente DESC");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            RelatorioCliente cliente = new RelatorioCliente();

                            cliente.nome = reader.GetString(0);
                            cliente.telefone = reader.GetString(1);
                            cliente.endereco = reader.GetString(2);

                            try
                            {
                                cliente.cpf = reader.GetString(3);
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
                MessageBox.Show("Por favor insira dados para a pesquisa", "ERRO", MessageBoxButtons.OK);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnGerarRelatorio.Enabled = false;
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
    }
}