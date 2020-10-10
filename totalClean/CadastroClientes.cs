using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace totalClean
{
    public partial class CadastroClientes : Form
    {
        public static int idSelected { get; set; }
        public static String nomeSelected { get; set; }
        public static String telefoneSelected { get; set; }
        public static String enderecoSelected { get; set; }
        public static Boolean frotistaSelected { get; set; }
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
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            EdicaoFrm s = new EdicaoFrm();
            s.Show();

        }
                    
        public void voltaAlteracao()
        {
            txtId.Text = idSelected.ToString();
            txtNome.Text = nomeSelected;
            txtTelefone.Text = telefoneSelected;
            txtEndereco.Text = enderecoSelected;
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
                    Conexao conexao = new Conexao();
                    conexao.conectar();
                    
                    int linhas = conexao.executar($"INSERT INTO Cliente (nome, telefone, endereco, frotista) VALUES ('{c.nome}','{c.telefone}','{c.endereco}',{statusCliente})");
                    limparCampos();
                    MessageBox.Show("Dados salvos com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            limparCampos();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CadastroClientes_Load(object sender, EventArgs e)
        {
            txtId.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtTelefone.ReadOnly = true;
            txtEndereco.ReadOnly = true;
            rdbFrotista.Enabled = false;
            rdbFrotista.Checked = false;
            rdbParticular.Checked = true;
        }
        private void limparCampos()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtTelefone.Text = "";
            txtId.Text = "";
        }

    }
}
