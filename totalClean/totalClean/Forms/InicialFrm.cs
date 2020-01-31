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
    public partial class InicialFrm : Form
    {
        public InicialFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void btnCadClientes_Click(object sender, EventArgs e)
        {

            CadastroClientes f = new CadastroClientes();
            f.Show();
            this.Visible = false;

        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {

        }

        private void btnRealizaVendas_Click(object sender, EventArgs e)
        {

        }

        private void btnCadServ_Click(object sender, EventArgs e)
        {
            CadastroServicosFrm s = new CadastroServicosFrm();
            s.Show();
            this.Visible = false;
        }

        

        private void InicialFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnRelatorioCliente_Click(object sender, EventArgs e)
        {

        }
    }
}
