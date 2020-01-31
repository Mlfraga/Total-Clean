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
    public partial class EdicaoFrm : Form
    {        
        Conexao con = new Conexao();
        public EdicaoFrm()
        {
            InitializeComponent();

        }


       
        private void EdicaoFrm_Load(object sender, EventArgs e)
        {
            txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            rdbFrotista.Enabled = false;
            rdbParticular.Enabled = false;
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            //  btnAlterear.Enabled = false;
            iniciaGrid();

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            CadastroClientes f = new CadastroClientes();
            f.Show();
            this.Visible = false;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            if (txtId.Text != string.Empty || txtNome.Text != string.Empty)
            {

                List<Cliente> listCliente = new List<Cliente>();
                con.conectar();

                SqlDataReader reader;

                if (txtNome.Text != string.Empty && txtId.Text == string.Empty)
                {
                    string nome = txtNome.Text;
                    reader = con.exeCliente($"SELECT * FROM Cliente WHERE Nome LIKE ('%{nome}%') ");
                }
                else
                {
                    int g = int.Parse(txtId.Text);
                    reader = con.exeCliente("SELECT * FROM Cliente WHERE idCliente = " + g);
                }
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

                        listCliente.Add(cliente);
                    }
                    reader.Close();
                }
                else
                {
                    Console.WriteLine("Não retornou dados");
                }
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = listCliente;
            }
            else
            {

            }
        }

        private void iniciaGrid()
        {
            List<Cliente> listCliente = new List<Cliente>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select * from Cliente");

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

                    listCliente.Add(cliente);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = listCliente;
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rdbParticular.Enabled = true;
            rdbFrotista.Enabled = true;
            // btnAlterear.Enabled = true;
            btnPesquisar.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            txtEndereco.ReadOnly = false;
            txtTelefone.ReadOnly = false;



            txtId.Text = dgvClientes.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
            txtTelefone.Text = dgvClientes.CurrentRow.Cells[2].Value.ToString();
            txtEndereco.Text = dgvClientes.CurrentRow.Cells[3].Value.ToString();

            if (dgvClientes.CurrentRow.Cells[4].Value.Equals(true))
            {
                rdbFrotista.Checked = true;
            }
            else
            {
                rdbParticular.Checked = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            iniciaGrid();
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnPesquisar.Enabled = true;
            txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;

        }

        private void limparCampos()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            txtId.Text = "";
            iniciaGrid();
        }

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnPesquisar.Enabled = true;
            txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;

            iniciaGrid();
            limparCampos();
        }




        private void btnSalvar_Click(object sender, EventArgs e)
        {

            Cliente cliente = new Cliente();
            cliente.id = int.Parse(txtId.Text);
            cliente.nome = txtNome.Text;
            cliente.telefone = txtTelefone.Text;
            cliente.endereco = txtEndereco.Text;

            if (rdbFrotista.Checked == true)
            {
                cliente.frotista = true;
            }
            else
            {
                cliente.frotista = false;
            }

            int aNome = con.executar($"UPDATE [dbo].[Cliente] set nome = '" + cliente.nome + "' WHERE idCliente = " + cliente.id);
            int atelefone = con.executar($"UPDATE [dbo].[Cliente] set telefone = '" + cliente.telefone + "' WHERE idCliente = " + cliente.id);
            int aEndereco = con.executar($"UPDATE [dbo].[Cliente] set endereco = '" + cliente.endereco + "' WHERE idCliente = " + cliente.id);
            int aTipo = con.executar($"UPDATE [dbo].[Cliente] set frotista = '" + cliente.frotista + "' WHERE idCliente = " + cliente.id);




            // Atualizar Grid com o cliente pesquisado
            List<Cliente> listCliente = new List<Cliente>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente($"SELECT * FROM Cliente WHERE idCliente = ('{cliente.id}') ");

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    cliente.id = reader.GetInt32(0);
                    cliente.nome = reader.GetString(1);
                    cliente.telefone = reader.GetString(2);
                    cliente.endereco = reader.GetString(3);
                    cliente.frotista = reader.GetBoolean(4);

                    listCliente.Add(cliente);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvClientes.DataSource = null;
            dgvClientes.DataSource = listCliente;
        }

        private void EdicaoFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}

