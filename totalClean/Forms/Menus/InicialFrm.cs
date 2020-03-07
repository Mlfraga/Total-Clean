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
            btnRelatorioCliente.Visible = false;
            btnRelatorioServico.Visible = false;
            btnRelatorioGastos.Visible = false;
            btnRelatorioGastosVendas.Visible = false;
        }

        private void btnCadClientes_Click(object sender, EventArgs e)
        {

            CadastroClientes f = new CadastroClientes();
            f.Show();
            this.Visible = false;

        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            RelatorioServico n = new RelatorioServico();
            n.Show();
            this.Visible = false;

        }

        private void btnRealizaVendas_Click(object sender, EventArgs e)
        {
            VendaFrm n = new VendaFrm();
            n.Show();
            this.Visible = false;
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
            RelatorioClienteFrm n = new RelatorioClienteFrm();
            n.Show();
            this.Visible = false;
        }

        private void InicialFrm_Resize(object sender, EventArgs e)
        {

        }

        private void btnPendencias_Click(object sender, EventArgs e)
        {
            Pendentes p = new Pendentes();
            p.Show();
            this.Visible = false;
        }

        private void btnCadastrarGastos_Click(object sender, EventArgs e)
        {
            CadastroGastos n = new CadastroGastos();
            n.Show();
            this.Visible = false;
        }

        private void btnGastoAbertos_Click(object sender, EventArgs e)
        {
            GastosAberto n = new GastosAberto();
            n.Show();
            this.Visible = false;

        }

        private void relatóriosVendasPServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelatorioServico n = new RelatorioServico();
            n.Show();
            this.Visible = false;
        }

        private void relatóriosVendasPClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelatorioClienteFrm n = new RelatorioClienteFrm();
            n.Show();
            this.Visible = false;
        }

        private void relatóriosGastosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelatorioGastos n = new RelatorioGastos();
            n.Show();
            this.Visible = false;
        }
    }
}
