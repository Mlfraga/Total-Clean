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
    public partial class NovoClienteFrm : Form
    {
        Conexao con = new Conexao();
        public int flag = 0;
        public NovoClienteFrm()
        {
            InitializeComponent();
        }
        private void bloqueiaCampos()
        {
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            txtCpf.ReadOnly = true;
            txtId.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;
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
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            VendaFrm n = new VendaFrm();
            n.Show();
            this.Visible = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            desbloqueaCampos();
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
                            if (txtTelefone.Text.Length < maxChar)
                            {
                                var choice2 = MessageBox.Show("O campo de telefone não está com ddd você deseja salvar mesmo assim?", "Confirmção", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                if (choice2 != DialogResult.Yes)
                                {
                                    return;
                                }
                            }

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
            VendaFrm n = new VendaFrm();
            n.Show();
            this.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;

            bloqueiaCampos();
            limparCampos();
        }
        private void limparCampos()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            txtId.Text = "";
            txtCpf.Text = "";
        }

        private void NovoClienteFrm_Load(object sender, EventArgs e)
        {
            bloqueiaCampos();
        }

        private void NovoClienteFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VendaFrm n = new VendaFrm();
            n.Show();
            this.Visible = false;
        }
    }
}
