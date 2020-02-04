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
    public partial class VendaFrm : Form
    {
        Conexao con = new Conexao();
        public VendaFrm()
        {
            InitializeComponent();
        }


        private void VendaFrm_Load(object sender, EventArgs e)
        {
            ocultaCmbs();
            prencheCmbCliente();
            PreencheCmb1();
            PreencheCmb2();
            PreencheCmb3();
            PreencheCmb4();
            PreencheCmb5();
            PreencheCmb6();
            PreencheCmb7();
            PreencheCmb8();
            btnConcluido.Enabled = false;
            btnCancelar.Enabled = false;
            bloqueiaCampos();
        }
        private void btnConsulta_Click(object sender, EventArgs e)
        {

        }

        private void btnNova_Click(object sender, EventArgs e)
        {
            desbloqueiaCampos();
            btnConcluido.Enabled = true;
            btnCancelar.Enabled = true;
            btnNova.Enabled = false;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ocultaCmbs();
            limpaCampos();
            btnCancelar.Enabled = false;
            btnConcluido.Enabled = false;
            btnNova.Enabled = true;
            bloqueiaCampos();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            InicialFrm n = new InicialFrm();
            n.Show();
            this.Visible = false;
        }

        private void VendaFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void limpaCampos()
        {
            txtPlaca.Text = "";
            txtCarro.Text = "";

            cmbCliente.Text = "";

            cmbServico1.Text = "";
            cmbQtd1.Text = "";

            cmbServico2.Text = "";
            cmbQtd2.Text = "";

            cmbServico3.Text = "";
            cmbQtd3.Text = "";

            cmbServico4.Text = "";
            cmbQtd4.Text = "";

            cmbServico5.Text = "";
            cmbQtd5.Text = "";

            cmbServico6.Text = "";
            cmbQtd6.Text = "";

            cmbServico7.Text = "";
            cmbQtd7.Text = "";

            cmbServico8.Text = "";
            cmbQtd8.Text = "";
        }
        private void bloqueiaCampos()
        {
            cmbCliente.Enabled = false;
            txtCarro.Enabled = false;
            txtPlaca.Enabled = false;
            DtVenda.Enabled = false;
            cmbServico1.Enabled = false;
            cmbQtd1.Enabled = false;
            btnAdd1.Enabled = false;
        }
        private void desbloqueiaCampos()
        {
            cmbCliente.Enabled = true;
            txtCarro.Enabled = true;
            txtPlaca.Enabled = true;
            DtVenda.Enabled = true;
            cmbServico1.Enabled = true;
            cmbQtd1.Enabled = true;
            btnAdd1.Enabled = true;
        }
        private void ocultaCmbs()
        {
            lblQtd2.Visible = false;
            lblServico2.Visible = false;
            cmbServico2.Visible = false;
            btnAdd2.Visible = false;
            cmbQtd2.Visible = false;

            lblQtd3.Visible = false;
            lblServico3.Visible = false;
            cmbServico3.Visible = false;
            btnAdd3.Visible = false;
            cmbQtd3.Visible = false;

            lblQtd4.Visible = false;
            lblServico4.Visible = false;
            cmbServico4.Visible = false;
            btnAdd4.Visible = false;
            cmbQtd4.Visible = false;

            lblQtd4.Visible = false;
            lblServico4.Visible = false;
            cmbServico5.Visible = false;
            btnAdd5.Visible = false;
            cmbQtd5.Visible = false;

            lblQtd5.Visible = false;
            lblServico5.Visible = false;
            cmbServico5.Visible = false;
            btnAdd5.Visible = false;
            cmbQtd5.Visible = false;

            lblQtd6.Visible = false;
            lblServico6.Visible = false;
            cmbServico6.Visible = false;
            btnAdd6.Visible = false;
            cmbQtd6.Visible = false;

            lblQtd7.Visible = false;
            lblServico7.Visible = false;
            cmbServico7.Visible = false;
            btnAdd7.Visible = false;
            cmbQtd7.Visible = false;

            lblQtd8.Visible = false;
            lblServico8.Visible = false;
            cmbServico8.Visible = false;
            cmbQtd8.Visible = false;

        }
        private void prencheCmbCliente()
        {
            List<Cliente> listCliente = new List<Cliente>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idCliente, nome from CLiente");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.id = reader.GetInt32(0);
                    cliente.nome = reader.GetString(1);

                    listCliente.Add(cliente);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbCliente.DataSource = listCliente;
            cmbCliente.ValueMember = "id";
            cmbCliente.DisplayMember = "nome";
            cmbCliente.Text = "";
        }
        private void PreencheCmb1()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico1.DataSource = listServico;
            cmbServico1.ValueMember = "id";
            cmbServico1.DisplayMember = "nome";
            cmbServico1.Text = "";
        }
        private void PreencheCmb2()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico2.DataSource = listServico;
            cmbServico2.ValueMember = "id";
            cmbServico2.DisplayMember = "nome";
            cmbServico2.Text = "";
        }
        private void PreencheCmb3()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico3.DataSource = listServico;
            cmbServico3.ValueMember = "id";
            cmbServico3.DisplayMember = "nome";
            cmbServico3.Text = "";
        }
        private void PreencheCmb4()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico4.DataSource = listServico;
            cmbServico4.ValueMember = "id";
            cmbServico4.DisplayMember = "nome";
            cmbServico4.Text = "";
        }
        private void PreencheCmb5()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico5.DataSource = listServico;
            cmbServico5.ValueMember = "id";
            cmbServico5.DisplayMember = "nome";
            cmbServico5.Text = "";
        }
        private void PreencheCmb6()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico6.DataSource = listServico;
            cmbServico6.ValueMember = "id";
            cmbServico6.DisplayMember = "nome";
            cmbServico6.Text = "";
        }
        private void PreencheCmb7()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico7.DataSource = listServico;
            cmbServico7.ValueMember = "id";
            cmbServico7.DisplayMember = "nome";
            cmbServico7.Text = "";
        }
        private void PreencheCmb8()
        {

            List<Servico> listServico = new List<Servico>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idServico, nome from Servicos WHERE ativo = 1");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Servico servico = new Servico();
                    servico.id = reader.GetInt32(0);
                    servico.nome = reader.GetString(1);

                    listServico.Add(servico);

                }
                reader.Close();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico8.DataSource = listServico;
            cmbServico8.ValueMember = "id";
            cmbServico8.DisplayMember = "nome";
            cmbServico8.Text = "";
        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            if (cmbQtd1.Text != string.Empty && cmbServico1.Text != string.Empty)
            {
                lblQtd2.Visible = true;
                lblServico2.Visible = true;
                cmbServico2.Visible = true;
                btnAdd2.Visible = true;
                cmbQtd2.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual","ERRO",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            if (cmbQtd2.Text != string.Empty && cmbServico2.Text != string.Empty)
            {
                lblQtd3.Visible = true;
                lblServico3.Visible = true;
                cmbServico3.Visible = true;
                btnAdd3.Visible = true;
                cmbQtd3.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnAdd3_Click(object sender, EventArgs e)
        {
            if (cmbQtd3.Text != string.Empty && cmbServico3.Text != string.Empty)
            {
                lblQtd4.Visible = true;
                lblServico4.Visible = true;
                cmbServico4.Visible = true;
                btnAdd4.Visible = true;
                cmbQtd4.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd4_Click(object sender, EventArgs e)
        {
            if (cmbQtd4.Text != string.Empty && cmbServico4.Text != string.Empty)
            {
                lblQtd5.Visible = true;
                lblServico5.Visible = true;
                cmbServico5.Visible = true;
                btnAdd5.Visible = true;
                cmbQtd5.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnAdd5_Click(object sender, EventArgs e)
        {
            if (cmbQtd5.Text != string.Empty && cmbServico5.Text != string.Empty)
            {
                lblQtd6.Visible = true;
                lblServico6.Visible = true;
                cmbServico6.Visible = true;
                btnAdd6.Visible = true;
                cmbQtd6.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd6_Click(object sender, EventArgs e)
        {
            if (cmbQtd6.Text != string.Empty && cmbServico6.Text != string.Empty)
            {
                lblQtd7.Visible = true;
                lblServico7.Visible = true;
                cmbServico7.Visible = true;
                btnAdd7.Visible = true;
                cmbQtd7.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd7_Click(object sender, EventArgs e)
        {

            if (cmbQtd7.Text != string.Empty && cmbServico7.Text != string.Empty)
            {
                lblQtd8.Visible = true;
                lblServico8.Visible = true;
                cmbServico8.Visible = true;
                cmbQtd8.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


    }
}
