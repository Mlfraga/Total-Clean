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
    public partial class SelecionaCarro : Form
    {
        Conexao con = new Conexao();
        public String carro;
        public String placa;
        public String index;

        int idCliente;

        public String carroCancel;
        public String placaCancel;
        public String indexCancel;


        public SelecionaCarro()
        {
            InitializeComponent();
        }
        public SelecionaCarro(string cliente)
        {
            InitializeComponent();
            gpboxCliente.Text = "Carros do cliente " + cliente;
            txtBoxFlag.Text = cliente;
        }

        private void SelecionaCarro_Load(object sender, EventArgs e)
        {
            modoSelecao();
            txtBoxFlag.Visible = false;
            btnCancelarSelecao.Enabled = true;
            btnSelecionar.Enabled = false;
            iniciarGrid();
        }
        private void modoSelecao()
        {
            btnNova.Enabled = true;
            btnAdiciona.Visible = false;
            lblCarro.Visible = false;
            lblPlaca.Visible = false;
            txtCarro.Visible = false;
            txtPlaca.Visible = false;
            bnfCarro3.Visible = false;
            bnfPlaca3.Visible = false;

            btnCancelarSelecao.Visible = true;
            btnCancelarAdic.Visible = false;
            btnSelecionar.Visible = true;
        }
        private void modoAdiciona()
        {
            btnAdiciona.Visible = true;
            lblCarro.Visible = true;
            lblPlaca.Visible = true;
            txtCarro.Visible = true;
            txtPlaca.Visible = true;
            bnfCarro3.Visible = true;
            bnfPlaca3.Visible = true;

            btnCancelarSelecao.Visible = false;
            btnCancelarAdic.Visible = true;
            btnNova.Enabled = false;
            btnSelecionar.Visible = false;
        }
        private void iniciarGrid()
        {
            string nomeCliente = txtBoxFlag.Text;

            List<Carros> listCarros = new List<Carros>();


            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente($"SELECT carro, placa, [Cliente].[nome] FROM CarrosClientes INNER JOIN Cliente ON ([CarrosClientes].[idCliente] = [Cliente].[idCliente]) WHERE [Cliente].[nome] LIKE ('{nomeCliente}') AND ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Carros carro = new Carros();
                    carro.carro = reader.GetString(0).Trim();
                    carro.placa = reader.GetString(1).Trim();
                    carro.cliente = reader.GetString(2).Trim();

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            VendaFrm n = new VendaFrm(carroCancel, placaCancel, txtBoxFlag.Text);
            n.Show();
            this.Visible = false;
        }


        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            VendaFrm n = new VendaFrm(carro, placa, txtBoxFlag.Text);
            n.Show();
            this.Visible = false;
        }

        private void dgvCarros_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSelecionar.Enabled = true;
            btnCancelarSelecao.Enabled = true;

            carro = dgvCarros.CurrentRow.Cells[0].Value.ToString();
            placa = dgvCarros.CurrentRow.Cells[1].Value.ToString();
        }

        private void SelecionaCarro_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            VendaFrm n = new VendaFrm(carroCancel, placaCancel, txtBoxFlag.Text);
            n.Show();
            this.Visible = false;
        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            modoAdiciona();
        }

        private void btnCancelarAdic_Click(object sender, EventArgs e)
        {
            modoSelecao();
            iniciarGrid();
            btnSelecionar.Enabled = false;
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
                        modoSelecao();
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
    }
}

