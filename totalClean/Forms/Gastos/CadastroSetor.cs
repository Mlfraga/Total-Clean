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
    public partial class CadastroSetor : Form
    {
        Conexao conexao = new Conexao();
        public int flag = 0;
        public CadastroSetor()
        {
            InitializeComponent();
        }

        private void CadastroSetor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void CadastroSetor_Load(object sender, EventArgs e)
        {
            txtNome.ReadOnly = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            flag = 0;

            conexao.conectar();

            String nomeSetor = txtNome.Text;

            SqlDataReader readerC;

            readerC = conexao.exeCliente("select nome from SetorGastos");



            if (readerC.HasRows)
            {
                String nometesta;

                while (readerC.Read())
                {

                    nometesta = readerC.GetString(0).Trim();

                    if (nomeSetor == nometesta)
                    {
                        flag = 1;
                    }


                }
                readerC.Close();
                conexao.desconectar();
            }


            if (flag == 1)
            {
                MessageBox.Show("Já existe um setor com esse nome.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {

                conexao.conectar();

                int linhas = conexao.executar($"INSERT INTO SetorGastos (nome) VALUES ('{nomeSetor}')");

                conexao.desconectar();
                MessageBox.Show("Dados Salvos com sucesso", "Confirmação",MessageBoxButtons.OK,MessageBoxIcon.Information);

                txtNome.Text = "";
                txtNome.ReadOnly = true;
                btnSalvar.Enabled = false;
                btnCancelar.Enabled = false;
                btnNovo.Enabled = true;
            }

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            CadastroGastos n = new CadastroGastos();
            n.Show();
            this.Visible = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            txtNome.ReadOnly = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            btnNovo.Enabled = false;
            txtNome.ReadOnly = false;
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
        }
    }
}
