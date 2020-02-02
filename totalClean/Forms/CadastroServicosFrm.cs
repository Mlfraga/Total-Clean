using System;
using System.Windows.Forms;

namespace totalClean
{
    public partial class CadastroServicosFrm : Form
    {
        public CadastroServicosFrm()
        {
            InitializeComponent();
        }
        private void CadastroServicosFrm_Load(object sender, EventArgs e)
        {
            bloqueiaCampos();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            btnNovo.Enabled = true;
        }

        private void CadastroServicosFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            InicialFrm i = new InicialFrm();
            i.Show();
            this.Visible = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            desbloqueaCampos();
            btnNovo.Enabled = false;
            

        }
        private void limparCampos()
        {
            txtNome.Text = "";
            txtPreco.Text = "";
            txtId.Text = "";
        }
        private void bloqueiaCampos()
        {
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;

            txtId.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtPreco.ReadOnly = true;
            rdbAtivo.Enabled = false;
            rdbDesativo.Enabled = false;
            rdbAtivo.Checked = true;
        }
        private void desbloqueaCampos()
        {
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;

            txtPreco.ReadOnly = false;
            txtNome.ReadOnly = false;
            rdbDesativo.Enabled = true;
            rdbAtivo.Enabled = true;
            rdbAtivo.Checked = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Servico servico = new Servico();

            servico.nome = txtNome.Text;
            servico.preco = float.Parse(txtPreco.Text);

            if (rdbAtivo.Checked == true)
            {
                servico.ativo = true;
            }
            else
            {
                servico.ativo = false;
            }

            int statusServico = servico.ativo ? 1 : 0;

            if (txtNome.Text != string.Empty && txtPreco.Text != string.Empty)
            {
                var escolha = MessageBox.Show("Você deseja mesmo salvar esses dados?", "Confirmção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(escolha == DialogResult.Yes)
                {
                    Conexao conexao = new Conexao();
                    conexao.conectar();

                    int linhas = conexao.executar($"INSERT INTO Servicos(nome, preco, ativo) VALUES ('{servico.nome}','{servico.preco}',{statusServico})");
                    limparCampos();
                    MessageBox.Show("Dados salvos com sucesso","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    
                    btnSalvar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnNovo.Enabled = true;
                    bloqueiaCampos();
                }
                else
                {

                }

            }
            else
            {
                MessageBox.Show("Um ou mais campos não foram preenchido!!!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Forms.EdicaoServico s = new Forms.EdicaoServico(); 
            s.Show();
            this.Visible = false;
            
            
            
        }
    }
}
