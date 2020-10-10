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
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic;

namespace totalClean
{
    public partial class EditaCarros : Form
    {
        Conexao con = new Conexao();
        public String carro;
        public String placa;
        public String index;

        int idCliente;

        public String carroCancel;
        public String placaCancel;
        public String indexCancel;

        public EditaCarros(string cliente)
        {
            InitializeComponent();
            gpboxCliente.Text = "Carros do cliente " + cliente;
            txtBoxFlag.Text = cliente;
        }


        private void EditaCarros_Load(object sender, EventArgs e)
        {
            txtBoxFlagId.Visible = false;
            txtBoxFlag.Visible = false;
            iniciarGrid();
            bloqueaBtns();
        }
        private void iniciarGrid()
        {
            string nomeCliente = txtBoxFlag.Text;

            List<edicaoCarros> listCarros = new List<edicaoCarros>();


            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente($"SELECT idCarro, carro, placa, [Cliente].[nome], ativo FROM CarrosClientes INNER JOIN Cliente ON ([CarrosClientes].[idCliente] = [Cliente].[idCliente]) WHERE [Cliente].[nome] LIKE ('{nomeCliente}')");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    edicaoCarros carro = new edicaoCarros();

                    carro.idCarro = reader.GetInt32(0);
                    carro.carro = reader.GetString(1).Trim();
                    carro.placa = reader.GetString(2).Trim();
                    carro.cliente = reader.GetString(3).Trim();
                    carro.ativo = reader.GetBoolean(4);

                    carroCancel = carro.carro;
                    placaCancel = carro.placa;

                    listCarros.Add(carro);
                }
                reader.Close();
                con.desconectar();

            }

            dgvCarros.DataSource = null;
            dgvCarros.DataSource = listCarros;
        }
        private void bloqueaBtns()
        {
            btnAltera.Enabled = false;
            btnCancelar.Enabled = false;
            btnNova.Enabled = true;
            btnAdiciona.Visible = false;
        }
        private void desbloqueaBtns()
        {
            btnAltera.Enabled = true;
            btnCancelar.Enabled = true;
            btnNova.Enabled = false;
        }
        private void modoAdicionar()
        {
            btnAltera.Enabled = true;
            btnCancelar.Enabled = true;
            btnNova.Enabled = false;

            btnAdiciona.Visible = true;
            dgvCarros.Enabled = false;
            txtCarro.Text = "";
            txtPlaca.Text = "";
            rdbAtivo.Enabled = false;
            rdbInativo.Enabled = false;
        }
        private void modoNeutro()
        {
            btnAltera.Enabled = false;
            btnCancelar.Enabled = false;
            btnNova.Enabled = true;

            btnAdiciona.Visible = false;
            dgvCarros.Enabled = true;
            txtCarro.Text = "";
            txtPlaca.Text = "";
            rdbAtivo.Enabled = true;
            rdbInativo.Enabled = true;
        }

        private void dgvCarros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            desbloqueaBtns();
            txtBoxFlagId.Text = dgvCarros.CurrentRow.Cells[0].Value.ToString();
            txtCarro.Text = dgvCarros.CurrentRow.Cells[1].Value.ToString();
            txtPlaca.Text = dgvCarros.CurrentRow.Cells[2].Value.ToString();

            if (dgvCarros.CurrentRow.Cells[3].Value.Equals(true))
            {
                rdbAtivo.Checked = true;
            }
            else
            {
                rdbInativo.Checked = true;
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            EdicaoFrm n = new EdicaoFrm();
            n.Show();
            this.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bloqueaBtns();
            modoNeutro();
            txtCarro.Text = "";
            txtPlaca.Text = "";
        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            modoAdicionar();

        }

        private void btnAdiciona_Click(object sender, EventArgs e)
        {
            con.conectar();

            int maxPlaca = 7;

            String nomeCliente = txtBoxFlag.Text;
            SqlDataReader reader;


            reader = con.exeCliente($"SELECT idCliente FROM Cliente WHERE nome LIKE ('{nomeCliente}')");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idCliente = reader.GetInt32(0);
                }
            }

            if (txtPlaca.Text != string.Empty)
            {
                if (txtPlaca.Text.Length > maxPlaca)
                {
                    MessageBox.Show("O campo de placa não pode ter mais que 7 caracteres.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (txtCarro.Text != string.Empty)
                    {
                        con.conectar();
                        int insere = con.executar($"INSERT INTO CarrosClientes(idCliente, carro, placa, ativo) VALUES ('{idCliente}', '{txtCarro.Text}', '{txtPlaca.Text}', 1)");
                        con.desconectar();
                        iniciarGrid();
                        modoNeutro();
                    }
                    else
                    {
                        MessageBox.Show("O campo de carro é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("O campo de placa é obrigatório.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAltera_Click(object sender, EventArgs e)
        {
            edicaoCarros editarCarro = new edicaoCarros();

            editarCarro.idCarro = int.Parse(txtBoxFlagId.Text);
            editarCarro.carro = txtCarro.Text;
            editarCarro.placa = txtPlaca.Text;

            if (rdbAtivo.Checked == true)
            {
                editarCarro.ativo = true;
            }

            if (rdbInativo.Checked == true)
            {
                editarCarro.ativo = false;
            }

            int maxPlaca = 7;

            if (editarCarro.placa.Length <= maxPlaca)
            {
                con.conectar();
                int aCarro = con.executar($"UPDATE CarrosClientes SET carro = '{editarCarro.carro}' WHERE idCarro = '{editarCarro.idCarro}'");
                int aPlaca = con.executar($"UPDATE CarrosClientes SET placa = '{editarCarro.placa}' WHERE idCarro = '{editarCarro.idCarro}'");
                int aAtivo = con.executar($"UPDATE CarrosClientes SET ativo = '{editarCarro.ativo}' WHERE idCarro = '{editarCarro.idCarro}'");
                con.desconectar();
                MessageBox.Show("Dados alterados com sucesso", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPlaca.Text = "";
                txtCarro.Text = "";
                iniciarGrid();
                modoNeutro();
            }
            else
            {
                MessageBox.Show("Campo placa com mais de 7 caracteres.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditaCarros_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
