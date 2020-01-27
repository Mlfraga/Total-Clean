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
            iniciaGrid();

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {


            if (txtId.Text != string.Empty || txtNome.Text != string.Empty ) {

                string nome = txtNome.Text;

                List<Cliente> listCliente = new List<Cliente>();
                con.conectar();

                SqlDataReader reader;

                reader = con.exeCliente("SELECT [idCliente], [Nome], [telefone], [endereco], [frotista] FROM[dbo].[Cliente] WHERE Nome LIKE '%' " + nome );

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
    private void btnSelecionar_Click(object sender, EventArgs e)
        {

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

 
    }
}
