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
    public partial class VendasAFinalizar : Form
    {

        Conexao con = new Conexao();
        public String msg;
        public String telefone;
        public int idVenda;
        public VendasAFinalizar()
        {
            InitializeComponent();
        }

        private void VendasAFinalizar_Load(object sender, EventArgs e)
        {
            iniciaGrid();
            prencheCmbCliente();
            bloqueaBtns();
        }
        private void bloqueaBtns()
        {
            btnEnviaMsg.Enabled = false;
            btnCancelar.Enabled = false;
        }
        private void desbloqueaBtns()
        {
            btnEnviaMsg.Enabled = true;
            btnCancelar.Enabled = true;
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
        private void iniciaGrid()
        {

            List<Cliente> listCliente = new List<Cliente>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select * from Cliente order by idCliente DESC");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.id = reader.GetInt32(0);
                    cliente.nome = reader.GetString(1);
                    cliente.telefone = reader.GetString(2);
                    cliente.endereco = reader.GetString(3);
                    cliente.frotista = reader.GetBoolean(4);


                    try
                    {
                        cliente.cpf = reader.GetString(5);
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            InicialFrm n = new InicialFrm();
            n.Show();
            this.Visible = false;
        }

        private void VendasAFinalizar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnEnviaMsg_Click(object sender, EventArgs e)
        {

            List<Cliente> listCliente = new List<Cliente>();

            con.conectar();

            SqlDataReader reader;



            reader = con.exeCliente("SELECT mensagemWpp FROM Configuracao ");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    msg = reader.GetString(0);

                }
                reader.Close();
                con.desconectar();
            }

            System.Diagnostics.Process.Start("chrome.exe", $"https://web.whatsapp.com/send?phone=55'{telefone}'&text={msg.Replace(" ", "%20")}");



            bloqueaBtns();
            iniciaGrid();
            cmbCliente.Text = "";

        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bloqueaBtns();
            iniciaGrid();
            cmbCliente.Text = "";
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
            List<Cliente> listCliente = new List<Cliente>();
            Cliente pesquisa = new Cliente();
            try
            {
                pesquisa.id = int.Parse(cmbCliente.SelectedValue.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                cmbCliente.Text = "";
                return;
            }
            con.conectar();
            SqlDataReader reader;

            reader = con.exeCliente($"select * from Cliente WHERE idCliente = '{pesquisa.id}' order by idCliente DESC");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.id = reader.GetInt32(0);
                    cliente.nome = reader.GetString(1);
                    cliente.telefone = reader.GetString(2);
                    cliente.endereco = reader.GetString(3);
                    cliente.frotista = reader.GetBoolean(4);


                    try
                    {
                        cliente.cpf = reader.GetString(5);
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

        private void dgvVendas_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            idVenda = int.Parse(dgvClientes.CurrentRow.Cells[0].Value.ToString());
            telefone = dgvClientes.CurrentRow.Cells[2].Value.ToString();
            cmbCliente.SelectedValue = idVenda;
            desbloqueaBtns();

        }

        private void btnEditaMsg_Click(object sender, EventArgs e)
        {
            MessageEdit n = new MessageEdit();
            n.Show();
        }
    }
}
