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
    }
}
