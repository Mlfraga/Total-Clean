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
    public partial class CadastroClientes : Form
    {
        Conexao con = new Conexao();
        public CadastroClientes()
        {
            InitializeComponent();
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtNome.ReadOnly = false;
            txtTelefone.ReadOnly = false;
            txtEndereco.ReadOnly = false;
            rdbFrotista.Enabled = true;
            rdbParticular.Enabled = true;

        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {

            btnNovo.Enabled = false;            
            EdicaoFrm s = new EdicaoFrm();
            s.Show();
            this.Visible = false;
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {

            Cliente c = new Cliente();
            c.nome = txtNome.Text;
            c.telefone = txtTelefone.Text;
            c.endereco = txtEndereco.Text;

            if (rdbFrotista.Checked == true)
            {
                c.frotista = true;
            }

            else
            {
                c.frotista = false;
            }

            int statusCliente = c.frotista ? 1 : 0;

            var escolha = MessageBox.Show("Você deseja mesmo salvar esses dados?", "Confirmção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (escolha == DialogResult.Yes)
            {

                if (txtNome.Text != string.Empty)
                {
                    Cliente cliente = new Cliente();
                    Conexao conexao = new Conexao();
                    conexao.conectar();

                    int linhas = conexao.executar($"INSERT INTO Cliente (nome, telefone, endereco, frotista) VALUES ('{c.nome}','{c.telefone}','{c.endereco}',{statusCliente})");
                    limparCampos();
                    MessageBox.Show("Dados salvos com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
                    List<Cliente> listCliente = new List<Cliente>();
                    con.conectar();

                    SqlDataReader reader;

                    reader = con.exeCliente("select * from Cliente");

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
                        MessageBox.Show("Id do Cliente: " + cliente.id.ToString(),"armazenar ultima id salva");
                        reader.Close();
                    }
                    else
                    {
                        MessageBox.Show("Campo Nome não preenchido", "Dados inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            limparCampos();

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            InicialFrm f = new InicialFrm();
            f.Show();
            this.Visible = false; 

        }

        private void CadastroClientes_Load(object sender, EventArgs e)
        {
            txtId.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            rdbFrotista.Enabled = false;
            rdbParticular.Enabled = false;
            rdbParticular.Checked = true;

        }
        private void limparCampos()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            txtId.Text = "";
        }

        private void CadastroClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
