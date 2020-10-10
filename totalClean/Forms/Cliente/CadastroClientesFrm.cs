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
        int lastIdCliente;
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

            txtCarro1.Visible = false;
            txtPlaca1.Visible = false;
            label8.Visible = false;
            bunifuSeparator7.Visible = false;
            label9.Visible = false;
            bunifuSeparator8.Visible = false;
            btnAdd1.Visible = false;

            txtCarro2.Visible = false;
            txtPlaca2.Visible = false;
            lblCarro2.Visible = false;
            lblPlaca2.Visible = false;
            bnfCarro2.Visible = false;
            bnfPlaca2.Visible = false;
            btnAdd2.Visible = false;

            txtCarro3.Visible = false;
            txtPlaca3.Visible = false;
            lblCarro3.Visible = false;
            lblPlaca3.Visible = false;
            bnfCarro3.Visible = false;
            bnfPlaca3.Visible = false;
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
                con.desconectar();
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
                            MessageBox.Show("Campo de telefone com mais de 11 caracteres ou de Cpf e Cnpj com mais de 14", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            if (txtTelefone.Text.Length < maxChar)
                            {
                                var choice2 = MessageBox.Show("O campo de telefone aparentemente não está com ddd, você deseja salvar mesmo assim?", "Confirmção", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                                if (choice2 != DialogResult.Yes)
                                {
                                    return;
                                }
                            }

                            Cliente cliente = new Cliente();
                            Conexao conexao = new Conexao();
                            conexao.conectar();

                            int linhas = conexao.executar($"INSERT INTO Cliente (nome, telefone, endereco, frotista, pfpj) VALUES ('{c.nome}','{c.telefone}','{c.endereco}','{statusCliente}', '{c.cpf}')");
                            conexao.desconectar();



                            // Codigo para armazenar ultimo codigo salvo         
                            con.conectar();

                            SqlDataReader reader;

                            reader = con.exeCliente("select idCliente from Cliente");


                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lastIdCliente = reader.GetInt32(0);
                                }
                                reader.Close();
                                con.desconectar();
                            }




                            int tamMax = 7;

                            if (txtCarro1.Text != string.Empty && txtPlaca1.Text != string.Empty)
                            {
                                if (txtPlaca1.Text.Length <= tamMax)
                                {
                                    Conexao conexaoCarro = new Conexao();
                                    conexao.conectar();

                                    int linhasCarro1 = conexao.executar($"INSERT INTO CarrosClientes (idCliente, carro, placa, ativo) VALUES ('{lastIdCliente}','{txtCarro1.Text}','{txtPlaca1.Text}', 1)");

                                    conexao.desconectar();
                                }
                                else
                                {
                                    MessageBox.Show("Campo placa com mais de 7 caracteres no 1° carro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (txtCarro2.Text != string.Empty && txtPlaca2.Text != string.Empty)
                            {
                                if (txtPlaca1.Text.Length <= tamMax)
                                {
                                    Conexao conexaoCarro = new Conexao();
                                    conexao.conectar();

                                    int linhasCarro2 = conexao.executar($"INSERT INTO CarrosClientes (idCliente, carro, placa, ativo) VALUES ('{lastIdCliente}','{txtCarro2.Text}','{txtPlaca2.Text}', 1)");

                                    conexao.desconectar();
                                }
                                else
                                {
                                    MessageBox.Show("Campo placa com mais de 7 caracteres no 2° carro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (txtCarro3.Text != string.Empty && txtPlaca3.Text != string.Empty)
                            {
                                if (txtPlaca1.Text.Length <= tamMax)
                                {
                                    Conexao conexaoCarro = new Conexao();
                                    conexao.conectar();

                                    int linhaCarro3s = conexao.executar($"INSERT INTO CarrosClientes (idCliente, carro, placa, ativo) VALUES ('{lastIdCliente}','{txtCarro3.Text}','{txtPlaca3.Text}', 1)");

                                    conexao.desconectar();
                                }
                                else
                                {
                                    MessageBox.Show("Campo placa com mais de 7 caracteres no 3° carro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            MessageBox.Show("Dados salvos com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCancelar.Enabled = false;
                            btnSalvar.Enabled = false;
                            btnNovo.Enabled = true;

                            limparCampos();
                            bloqueiaCampos();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("os campos de nome e telefone são obrigatórios", "Dados inválidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtCarro1.Text = "";
            txtCarro2.Text = "";
            txtCarro3.Text = "";
            txtPlaca1.Text = "";
            txtPlaca2.Text = "";
            txtPlaca3.Text = "";
        }

        private void CadastroClientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnAddCarros_Click(object sender, EventArgs e)
        {
            txtCarro1.Visible = true;
            txtPlaca1.Visible = true;
            label8.Visible = true;
            bunifuSeparator7.Visible = true;
            label9.Visible = true;
            bunifuSeparator8.Visible = true;
            btnAdd1.Visible = true;

        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            txtCarro2.Visible = true;
            txtPlaca2.Visible = true;
            lblCarro2.Visible = true;
            lblPlaca2.Visible = true;
            bnfCarro2.Visible = true;
            bnfPlaca2.Visible = true;
            btnAdd2.Visible = true;
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            txtCarro3.Visible = true;
            txtPlaca3.Visible = true;
            lblCarro3.Visible = true;
            lblPlaca3.Visible = true;
            bnfCarro3.Visible = true;
            bnfPlaca3.Visible = true;

        }
    }
}
