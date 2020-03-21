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
        public int flag = 0;
        public CadastroClientes()
        {
            InitializeComponent();
        }
        private void bloqueiaCampos()
        {
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            txtId.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            txtCpf.ReadOnly = true;
            rdbFrotista.Enabled = false;
            rdbParticular.Enabled = false;
            rdbParticular.Checked = true;
        }
        private void desbloqueaCampos()
        {
            txtCpf.ReadOnly = false;
            txtNome.ReadOnly = false;
            txtTelefone.ReadOnly = false;
            txtEndereco.ReadOnly = false;
            rdbFrotista.Enabled = true;
            rdbParticular.Enabled = true;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;

            btnNovo.Enabled = false;
        }
        private void btnNovo_Click(object sender, EventArgs e)
        {
            desbloqueaCampos();
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
            flag = 0;

            con.conectar();

            SqlDataReader readerC;

            readerC = con.exeCliente("select nome from Cliente");

            String nome = txtNome.Text.Trim();


            if (readerC.HasRows)
            {
                Cliente cliente = new Cliente();

                while (readerC.Read())
                {

                    cliente.nome = readerC.GetString(0).Trim();

                    if (nome == cliente.nome)
                    {
                        flag = 1;
                    }


                }
                readerC.Close();
            }

            if (flag == 1)
            {
                MessageBox.Show("Ja existe um cliente com esse nome", "Dados inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
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

               
                if (txtCpf.Text != string.Empty)
                {
                    c.cpf = txtCpf.Text;
                }
                else
                {
                    
                }

                if (txtNome.Text != string.Empty && txtTelefone.Text != string.Empty)
                {
                    var escolha = MessageBox.Show("Você deseja mesmo salvar esses dados?", "Confirmção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (escolha == DialogResult.Yes)
                    {
                        int maxChar = 11;
                        int maxCpf = 14;

                        if (txtTelefone.Text.Length > maxChar || txtCpf.Text.Length > maxCpf)
                        {
                            MessageBox.Show("Campo de telefone ou cpf e cnpj com muitos caracteres", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            Cliente cliente = new Cliente();
                            Conexao conexao = new Conexao();
                            conexao.conectar();

                            int linhas = conexao.executar($"INSERT INTO Cliente (nome, telefone, endereco, frotista, pfpj) VALUES ('{c.nome}','{c.telefone}','{c.endereco}','{statusCliente}', '{c.cpf}')");
                            limparCampos();
                            MessageBox.Show("Dados salvos com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            // Codigo para armazenar ultimo codigo salvo
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

                                reader.Close();

                                btnCancelar.Enabled = false;
                                btnSalvar.Enabled = false;
                                btnNovo.Enabled = true;

                                bloqueiaCampos();
                            }

                        }
                    }



                    else
                    {

                    }

                }


                else
                {
                    MessageBox.Show("Um ou mais campos não foram preenchidos!!!", "Dados inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }




        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;

            bloqueiaCampos();
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
            bloqueiaCampos();

        }
        private void limparCampos()
        {
            txtCpf.Text = "";
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
