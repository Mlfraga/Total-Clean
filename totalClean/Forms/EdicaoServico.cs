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


namespace totalClean.Forms
{
    public partial class EdicaoServico : Form
    {
        Conexao con = new Conexao();
        public EdicaoServico()
        {
            InitializeComponent();
        }
        private void iniciaGrid()
        {
            List<Servico> listServico = new List<Servico>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select * from Servicos");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();

                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);
                    servico.preco = reader.GetDouble(2);
                    //(float)dataReader.GetDouble("Power");
                    servico.ativo = reader.GetBoolean(3);

                    listServico.Add(servico);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvServicos.DataSource = null;
            dgvServicos.DataSource = listServico;
        }
        private void EdicaoServico_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;

            rdbAtivo.Enabled = false;
            rdbDesativo.Enabled = false;

            txtPreco.ReadOnly = true;

            iniciaGrid();
        }

        private void EdicaoServico_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            CadastroServicosFrm n = new CadastroServicosFrm();
            n.Show();
            this.Visible = false;
        }

        private void dgvServicos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCancelar.Enabled = true;
            btnSalvar.Enabled = true;
            btnPesquisar.Enabled = false;

            txtPreco.ReadOnly = false;

            rdbAtivo.Enabled = true;
            rdbDesativo.Enabled = true;

            txtId.Text = dgvServicos.CurrentRow.Cells[0].Value.ToString();
            txtNome.Text = dgvServicos.CurrentRow.Cells[1].Value.ToString();
            txtPreco.Text = dgvServicos.CurrentRow.Cells[2].Value.ToString();

            if (dgvServicos.CurrentRow.Cells[3].Value.Equals(true))
            {
                rdbAtivo.Checked = true;
            }
            else
            {
                rdbDesativo.Checked = true;
            }

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != string.Empty || txtNome.Text != string.Empty)
            {

                List<Servico> listServico = new List<Servico>();
                con.conectar();

                SqlDataReader reader;


                string nome = txtNome.Text;
                if (txtId.Text != string.Empty)
                {
                    int g = int.Parse(txtId.Text);
                    
                    reader = con.exeCliente($"SELECT * FROM Servicos WHERE idServico = ('{g}') AND Nome LIKE ('%{nome}%') ");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Servico servico = new Servico();

                            servico.id = reader.GetInt32(0);
                            servico.nome = reader.GetString(1);
                            servico.preco = reader.GetDouble(2);
                            servico.ativo = reader.GetBoolean(3);

                            listServico.Add(servico);

                        }
                        reader.Close();
                        dgvServicos.DataSource = null;
                        dgvServicos.DataSource = listServico;
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum serviço com nome parecido com " + nome + " ou id igual a " + g, "ERRO", MessageBoxButtons.OK);
                    }
                }

                else
                {
                    reader = con.exeCliente($"SELECT * FROM Servicos WHERE Nome LIKE ('%{nome}%') ");

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Servico servico = new Servico();

                            servico.id = reader.GetInt32(0);
                            servico.nome = reader.GetString(1);
                            servico.preco = reader.GetDouble(2);
                            servico.ativo = reader.GetBoolean(3);

                            listServico.Add(servico);

                        }
                        reader.Close();
                        dgvServicos.DataSource = null;
                        dgvServicos.DataSource = listServico;
                    }
                    else
                    {
                        MessageBox.Show("Não foi encontrado nenhum serviço com nome parecido com " + nome, "ERRO", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nenhum dado inserido");
            }
        }
        private void limparCampos()
        {
            txtPreco.Text = "";
            txtId.Text = "";
            txtNome.Text = "";
            iniciaGrid();
        }

        private void btnLimpaCampos_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnPesquisar.Enabled = true;
            txtPreco.ReadOnly = true;
            rdbAtivo.Enabled = false;
            rdbDesativo.Enabled = false;

            iniciaGrid();
            limparCampos();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnPesquisar.Enabled = true;
            txtPreco.ReadOnly = true;
            rdbAtivo.Enabled = false;
            rdbDesativo.Enabled = false;

            iniciaGrid();
            limparCampos();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            Servico servico = new Servico();
            servico.id = int.Parse(txtId.Text);
            servico.nome = txtNome.Text;
            servico.preco = Double.Parse(txtPreco.Text);


            if (rdbAtivo.Checked == true)
            {
                servico.ativo = true;
            }
            else
            {
                servico.ativo = false;
            }

            int aNome = con.executar($"UPDATE [dbo].[Servicos] set nome = '" + servico.nome + "' WHERE idServico = " + servico.id);
            int apreco = con.executar($"UPDATE [dbo].[Servicos] set preco = '" + servico.preco + "' WHERE idServico = " + servico.id);
            int aEndereco = con.executar($"UPDATE [dbo].[Servicos] set ativo = '" + servico.ativo + "' WHERE idServico = " + servico.id);

            MessageBox.Show("Dados alterados com sucesso", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Atualizar Grid com o cliente salvo
            List<Servico> listServico = new List<Servico>();
            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente($"SELECT * FROM Servicos WHERE idServico = ('{servico.id}') ");

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);
                    servico.preco = reader.GetDouble(2);
                    servico.ativo = reader.GetBoolean(3);

                    listServico.Add(servico);
                }
                reader.Close();
            }
            else
            {
                Console.WriteLine("Não retornou dados");
            }
            dgvServicos.DataSource = null;
            dgvServicos.DataSource = listServico;
        }
    }
}
