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
    public partial class TelaInicialFrm : Form
    {
        public TelaInicialFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
        }

        private void btnCadClientes_Click(object sender, EventArgs e)
        {
            TelaCadastrClienteFrm f = new TelaCadastrClienteFrm();
            TelaInicialFrm j = new TelaInicialFrm();
            f.Show();
            j.Close();
            
        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {

        }

        private void btnRealizaVendas_Click(object sender, EventArgs e)
        {

        }

        private void btnCadServ_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
