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
        int flagPreencheCmb2, flagPreencheCmb3, flagPreencheCmb4, flagPreencheCmb5, flagPreencheCmb6, flagPreencheCmb7, flagPreencheCmb8;
        int flagLimpaCampos;
        int FlagPreencheCmbCliente;
        int FlagPreencheCmbServico;

        int contPendencias = 0;
        double valorTotalaCobrar;

        int i;
        public int flagCarro = 0;
        Conexao con = new Conexao();

        float descontoPServico;

        double precoAtt;

        public String getCarro;
        public String getPlaca;
        public String getCliente;

        public VendaFrm()
        {
            InitializeComponent();
        }
        public VendaFrm(String carro, String placa, String cliente)
        {
            InitializeComponent();

            getCarro = carro;
            getPlaca = placa;
            getCliente = cliente;
        }



        private void VendaFrm_Load(object sender, EventArgs e)
        {
            /*flagLimpaCampos = 0;
            flagPreencheCmb2 = 0;
            flagPreencheCmb3 = 0;
            flagPreencheCmb4 = 0;
            flagPreencheCmb5 = 0;
            flagPreencheCmb6 = 0;
            flagPreencheCmb7 = 0;
            flagPreencheCmb8 = 0;
            FlagPreencheCmbCliente = 0;
            FlagPreencheCmbServico = 0;*/

            limpaCampos();
            ocultaCmbs();

            btnConcluido.Enabled = false;
            btnCancelar.Enabled = false;
            btnSelecionarCarro.Visible = false;

            bloqueiaCampos();

            prencheCmbCliente();
            PreencheCmb1();
            lblPendencias.Text = "";

            int index = cmbCliente.FindString(getCliente);
            cmbCliente.SelectedIndex = index;

            if (cmbCliente.Text != string.Empty)
            {
                txtCarro.Text = getCarro;
                txtPlaca.Text = getPlaca;
                btnSelecionarCarro.Visible = true;

                desbloqueiaCampos();
                btnConcluido.Enabled = true;
                btnCancelar.Enabled = true;
                btnNova.Enabled = false;
            }


            txtDesconto.Text = "0";
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
            lblTotal.Text = "0";
            lblSubTotal.Text = "0";
            bloqueiaCampos();

            lblPendencias.Text = "";
        }
        private void btnVoltar_Click(object sender, EventArgs e)
        {
            InicialFrm n = new InicialFrm();
            n.Show();
            this.Visible = false;
        }
        private void btnConcluido_Click(object sender, EventArgs e)
        {

            if (cmbCliente.Text != string.Empty && txtCarro.Text != string.Empty && txtPlaca.Text != string.Empty && cmbQtd1.Text != string.Empty && cmbServico1.Text != string.Empty)
            {
                int tamMax = 7;
                if (txtPlaca.Text.Length <= tamMax)
                {

                    Classes.VendasServicos vs = new Classes.VendasServicos();
                    Venda venda = new Venda();
                    try
                    {
                        venda.idCliente = int.Parse(cmbCliente.SelectedValue.ToString());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Favor selcionar um cliente já cadastrado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    venda.carro = txtCarro.Text;
                    venda.placa = txtPlaca.Text;
                    venda.data = DtVenda.Value;


                    if (rdbTransferencia.Checked == true)
                    {
                        venda.formaPagamento = "Transferência Bancária";
                    }

                    if (rdbBoleto.Checked == true)
                    {
                        venda.formaPagamento = "Boleto";
                    }

                    if (rdbCredito.Checked == true)
                    {
                        venda.formaPagamento = "Crédito";
                    }

                    if (rdbDebito.Checked == true)
                    {
                        venda.formaPagamento = "Débito";
                    }

                    if (rdbDinheiro.Checked == true)
                    {
                        venda.formaPagamento = "Dinheiro";
                    }

                    if (rdbPermuta.Checked == true)
                    {
                        venda.formaPagamento = "Permuta";
                    }


                    // PEGAR PREÇO DA SERVIÇO
                    ValorVenda valorVenda = new ValorVenda();

                    con.conectar();

                    SqlDataReader readerPreco;

                    readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico1.SelectedValue}')");
                    if (readerPreco.HasRows)
                    {
                        while (readerPreco.Read())
                        {
                            valorVenda.precoServico = readerPreco.GetDouble(0);
                            valorVenda.qtd = double.Parse(cmbQtd1.Text);
                        }
                        readerPreco.Close();
                        con.desconectar();
                    }

                    double preco = valorVenda.precoServico * valorVenda.qtd;



                    if (cmbServico2.Text != string.Empty && cmbQtd2.Text != string.Empty)
                    {
                        ValorVenda valorVenda2 = new ValorVenda();

                        con.conectar();

                        readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico2.SelectedValue}')");
                        if (readerPreco.HasRows)
                        {
                            while (readerPreco.Read())
                            {
                                valorVenda.precoServico = readerPreco.GetDouble(0);
                                valorVenda.qtd = double.Parse(cmbQtd2.Text);
                            }

                            readerPreco.Close();
                            con.desconectar();
                        }
                        preco += valorVenda.precoServico * valorVenda.qtd;

                    }
                    if (cmbServico3.Text != string.Empty && cmbQtd3.Text != string.Empty)
                    {
                        ValorVenda valorVenda3 = new ValorVenda();

                        con.conectar();

                        readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico3.SelectedValue}')");
                        if (readerPreco.HasRows)
                        {
                            while (readerPreco.Read())
                            {
                                valorVenda.precoServico = readerPreco.GetDouble(0);
                                valorVenda.qtd = double.Parse(cmbQtd3.Text);
                            }

                            readerPreco.Close();
                            con.desconectar();
                        }
                        preco += valorVenda.precoServico * valorVenda.qtd;

                    }
                    if (cmbServico4.Text != string.Empty && cmbQtd4.Text != string.Empty)
                    {
                        ValorVenda valorVenda4 = new ValorVenda();

                        con.conectar();

                        readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico4.SelectedValue}')");
                        if (readerPreco.HasRows)
                        {
                            while (readerPreco.Read())
                            {
                                valorVenda.precoServico = readerPreco.GetDouble(0);
                                valorVenda.qtd = double.Parse(cmbQtd4.Text);
                            }

                            readerPreco.Close();
                            con.desconectar();
                        }
                        preco += valorVenda.precoServico * valorVenda.qtd;

                    }
                    if (cmbServico5.Text != string.Empty && cmbQtd5.Text != string.Empty)
                    {
                        ValorVenda valorVenda5 = new ValorVenda();

                        con.conectar();

                        readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico5.SelectedValue}')");
                        if (readerPreco.HasRows)
                        {
                            while (readerPreco.Read())
                            {
                                valorVenda.precoServico = readerPreco.GetDouble(0);
                                valorVenda.qtd = double.Parse(cmbQtd5.Text);
                            }

                            readerPreco.Close();
                            con.desconectar();
                        }
                        preco += valorVenda.precoServico * valorVenda.qtd;

                    }
                    if (cmbServico6.Text != string.Empty && cmbQtd6.Text != string.Empty)
                    {
                        ValorVenda valorVenda6 = new ValorVenda();

                        con.conectar();

                        readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico6.SelectedValue}')");
                        if (readerPreco.HasRows)
                        {
                            while (readerPreco.Read())
                            {
                                valorVenda.precoServico = readerPreco.GetDouble(0);
                                valorVenda.qtd = double.Parse(cmbQtd6.Text);
                            }

                            readerPreco.Close();
                            con.desconectar();
                        }
                        preco += valorVenda.precoServico * valorVenda.qtd;

                    }
                    if (cmbServico7.Text != string.Empty && cmbQtd7.Text != string.Empty)
                    {
                        ValorVenda valorVenda7 = new ValorVenda();

                        con.conectar();

                        readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico7.SelectedValue}')");
                        if (readerPreco.HasRows)
                        {
                            while (readerPreco.Read())
                            {
                                valorVenda.precoServico = readerPreco.GetDouble(0);
                                valorVenda.qtd = double.Parse(cmbQtd7.Text);
                            }

                            readerPreco.Close();
                            con.desconectar();
                        }
                        preco += valorVenda.precoServico * valorVenda.qtd;

                    }
                    if (cmbServico8.Text != string.Empty && cmbQtd8.Text != string.Empty)
                    {
                        ValorVenda valorVenda3 = new ValorVenda();

                        con.conectar();

                        readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico8.SelectedValue}')");
                        if (readerPreco.HasRows)
                        {
                            while (readerPreco.Read())
                            {
                                valorVenda.precoServico = readerPreco.GetDouble(0);
                                valorVenda.qtd = double.Parse(cmbQtd8.Text);
                            }

                            readerPreco.Close();
                            con.desconectar();
                        }
                        preco += valorVenda.precoServico * valorVenda.qtd;

                    }

                    decimal numero;
                    float desconto;

                    if (txtDesconto.Text == string.Empty)
                    {
                        txtDesconto.Text = "0";
                    }

                    if (decimal.TryParse(txtDesconto.Text, out numero))
                    {
                        desconto = float.Parse(txtDesconto.Text);
                        if (txtDesconto.Text == string.Empty)
                        {
                            preco = preco - 0;
                        }
                        else
                        {
                            preco -= desconto;
                        }



                        Conexao conexao = new Conexao();


                        var escolha = MessageBox.Show("O cliente efetuou o pagamento de R$" + preco + ",00 ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (escolha == DialogResult.Yes)
                        {
                            conexao.conectar();
                            int insere = conexao.executar($"INSERT INTO Vendas(data, carro, placa, idCliente, pago, formaPagamento, valorCobrado) VALUES('{venda.data}','{venda.carro}','{venda.placa}','{venda.idCliente}', 1, '{venda.formaPagamento}', '{preco}')  ");
                            conexao.desconectar();

                        }
                        else
                        {
                            conexao.conectar();
                            int insere2 = conexao.executar($"INSERT INTO Vendas(data, carro, placa, idCliente, pago, formaPagamento, valorCobrado) VALUES('{venda.data}','{venda.carro}','{venda.placa}','{venda.idCliente}', 0, '{venda.formaPagamento}', '{preco}')  ");
                            conexao.desconectar();

                        }


                        // Vincular carro nao cadastrado ao cliente

                        con.conectar();
                        SqlDataReader readerTestaCarro;
                        flagCarro = 0;
                        readerTestaCarro = con.exeCliente($"select carro, placa from CarrosClientes WHERE idCliente = ('{venda.idCliente}')");
                        if (readerTestaCarro.HasRows)
                        {
                            while (readerTestaCarro.Read())
                            {
                                String carro = readerTestaCarro.GetString(0).Trim();
                                String placa = readerTestaCarro.GetString(1).Trim();
                                if (venda.carro == carro && venda.placa == placa)
                                {
                                    flagCarro = 1;
                                }
                            }

                            readerTestaCarro.Close();
                            con.desconectar();
                        }

                        if (flagCarro == 0)
                        {
                            var salvaCarro = MessageBox.Show("Esse veículo ainda não foi cadastrado no sistema, deseja cadastrá-lo agora?", "Cadastra de veículo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (salvaCarro == DialogResult.Yes)
                            {
                                conexao.conectar();
                                int insereCarro = conexao.executar($"INSERT INTO CarrosClientes(idCliente, carro, placa, ativo) VALUES('{venda.idCliente}','{venda.carro}','{venda.placa}', 1)");
                                conexao.desconectar();
                            }
                            else
                            {

                            }
                        }

                        if (cmbServico1.Text != string.Empty && cmbServico2.Text != string.Empty && cmbServico3.Text != string.Empty && cmbServico4.Text != string.Empty && cmbServico5.Text != string.Empty && cmbServico6.Text != string.Empty && cmbServico7.Text != string.Empty && cmbServico8.Text != string.Empty)
                        {
                            descontoPServico = desconto / 8;
                        }
                        if (cmbServico1.Text != string.Empty && cmbServico2.Text != string.Empty && cmbServico3.Text != string.Empty && cmbServico4.Text != string.Empty && cmbServico5.Text != string.Empty && cmbServico6.Text != string.Empty && cmbServico7.Text != string.Empty && cmbServico8.Text == string.Empty)
                        {
                            descontoPServico = desconto / 7;
                        }
                        if (cmbServico1.Text != string.Empty && cmbServico2.Text != string.Empty && cmbServico3.Text != string.Empty && cmbServico4.Text != string.Empty && cmbServico5.Text != string.Empty && cmbServico6.Text != string.Empty && cmbServico7.Text == string.Empty && cmbServico8.Text == string.Empty)
                        {
                            descontoPServico = desconto / 6;
                        }
                        if (cmbServico1.Text != string.Empty && cmbServico2.Text != string.Empty && cmbServico3.Text != string.Empty && cmbServico4.Text != string.Empty && cmbServico5.Text != string.Empty && cmbServico6.Text == string.Empty && cmbServico7.Text == string.Empty && cmbServico8.Text == string.Empty)
                        {
                            descontoPServico = desconto / 5;
                        }
                        if (cmbServico1.Text != string.Empty && cmbServico2.Text != string.Empty && cmbServico3.Text != string.Empty && cmbServico4.Text != string.Empty && cmbServico5.Text == string.Empty && cmbServico6.Text == string.Empty && cmbServico7.Text == string.Empty && cmbServico8.Text == string.Empty)
                        {
                            descontoPServico = desconto / 4;
                        }
                        if (cmbServico1.Text != string.Empty && cmbServico2.Text != string.Empty && cmbServico3.Text != string.Empty && cmbServico4.Text == string.Empty && cmbServico5.Text == string.Empty && cmbServico6.Text == string.Empty && cmbServico7.Text == string.Empty && cmbServico8.Text == string.Empty)
                        {
                            descontoPServico = desconto / 3;
                        }
                        if (cmbServico1.Text != string.Empty && cmbServico2.Text != string.Empty && cmbServico3.Text == string.Empty && cmbServico4.Text == string.Empty && cmbServico5.Text == string.Empty && cmbServico6.Text == string.Empty && cmbServico7.Text == string.Empty && cmbServico8.Text == string.Empty)
                        {
                            descontoPServico = desconto / 2;
                        }
                        if (cmbServico1.Text != string.Empty && cmbServico2.Text == string.Empty && cmbServico3.Text == string.Empty && cmbServico4.Text == string.Empty && cmbServico5.Text == string.Empty && cmbServico6.Text == string.Empty && cmbServico7.Text == string.Empty && cmbServico8.Text == string.Empty)
                        {
                            descontoPServico = desconto;
                        }




                        /// Pegar ultimo id Venda
                        con.conectar();

                        SqlDataReader readerIdVenda;

                        readerIdVenda = con.exeCliente("select * from Vendas");

                        if (readerIdVenda.HasRows)
                        {
                            while (readerIdVenda.Read())
                            {
                                venda.idVenda = readerIdVenda.GetInt32(0);
                                venda.data = readerIdVenda.GetDateTime(1);
                                venda.carro = readerIdVenda.GetString(2);
                                venda.placa = readerIdVenda.GetString(3);
                                venda.idCliente = readerIdVenda.GetInt32(4);

                            }
                            readerIdVenda.Close();
                            con.desconectar();
                        }


                        int idVenda = venda.idVenda;
                        vs.qtd1 = int.Parse(cmbQtd1.Text);
                        vs.servico1 = int.Parse(cmbServico1.SelectedValue.ToString());

                        // Pega o preco do servico

                        con.conectar();

                        PrecoVendaServico precoServicoVenda = new PrecoVendaServico();
                        SqlDataReader readerPreco1;

                        readerPreco1 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico1.SelectedValue}')");
                        if (readerPreco1.HasRows)
                        {
                            while (readerPreco1.Read())
                            {
                                precoServicoVenda.valor = readerPreco1.GetDouble(0);
                            }

                            readerPreco1.Close();
                            con.desconectar();
                        }
                        double valorCobrado;

                        valorCobrado = precoServicoVenda.valor - descontoPServico;


                        for (int i = 1; i <= vs.qtd1; i++)
                        {
                            conexao.conectar();
                            int linhas1 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico1}','{idVenda}', '{valorCobrado}') ");
                            conexao.desconectar();
                        }



                        if (cmbServico2.Text != string.Empty && cmbQtd2.Text != string.Empty)
                        {

                            // Pega o preco do servico

                            con.conectar();

                            PrecoVendaServico precoServicoVenda2 = new PrecoVendaServico();
                            SqlDataReader readerPreco2;

                            readerPreco2 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico2.SelectedValue}')");
                            if (readerPreco2.HasRows)
                            {
                                while (readerPreco2.Read())
                                {
                                    precoServicoVenda2.valor = readerPreco2.GetDouble(0);
                                }

                                readerPreco2.Close();
                                con.desconectar();
                            }
                            double valorCobrado2;

                            valorCobrado2 = precoServicoVenda2.valor - descontoPServico;

                            vs.qtd2 = int.Parse(cmbQtd2.Text);
                            vs.servico2 = int.Parse(cmbServico2.SelectedValue.ToString());

                            conexao.conectar();

                            for (int i = 1; i <= vs.qtd2; i++)
                            {
                                int linhas2 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico2}','{idVenda}', '{valorCobrado2}') ");
                            }
                            conexao.desconectar();
                        }
                        else
                        {

                        }


                        if (cmbServico3.Text != string.Empty && cmbQtd3.Text != string.Empty)
                        {
                            con.conectar();

                            PrecoVendaServico precoServicoVenda3 = new PrecoVendaServico();
                            SqlDataReader readerPreco3;

                            readerPreco3 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico3.SelectedValue}')");
                            if (readerPreco3.HasRows)
                            {
                                while (readerPreco3.Read())
                                {
                                    precoServicoVenda3.valor = readerPreco3.GetDouble(0);
                                }

                                readerPreco3.Close();
                                con.desconectar();
                            }
                            double valorCobrado3;

                            valorCobrado3 = precoServicoVenda3.valor - descontoPServico;


                            vs.qtd3 = int.Parse(cmbQtd3.Text);
                            vs.servico3 = int.Parse(cmbServico3.SelectedValue.ToString());

                            conexao.conectar();

                            for (int i = 1; i <= vs.qtd3; i++)
                            {
                                int linhas2 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico3}','{idVenda}', '{valorCobrado3}') ");
                            }
                            conexao.desconectar();
                        }
                        else
                        {

                        }


                        if (cmbServico4.Text != string.Empty && cmbQtd4.Text != string.Empty)
                        {
                            con.conectar();

                            PrecoVendaServico precoServicoVenda4 = new PrecoVendaServico();
                            SqlDataReader readerPreco4;

                            readerPreco4 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico4.SelectedValue}')");
                            if (readerPreco4.HasRows)
                            {
                                while (readerPreco4.Read())
                                {
                                    precoServicoVenda4.valor = readerPreco4.GetDouble(0);
                                }

                                readerPreco4.Close();
                                con.desconectar();
                            }
                            double valorCobrado4;

                            valorCobrado4 = precoServicoVenda4.valor - descontoPServico;

                            vs.qtd4 = int.Parse(cmbQtd4.Text);
                            vs.servico4 = int.Parse(cmbServico4.SelectedValue.ToString());
                            conexao.conectar();
                            for (int i = 1; i <= vs.qtd4; i++)
                            {
                                int linhas2 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico4}','{idVenda}', '{valorCobrado4}') ");
                            }
                            conexao.desconectar();
                        }
                        else
                        {

                        }


                        if (cmbServico5.Text != string.Empty && cmbQtd5.Text != string.Empty)
                        {
                            con.conectar();

                            PrecoVendaServico precoServicoVenda5 = new PrecoVendaServico();
                            SqlDataReader readerPreco5;

                            readerPreco5 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico5.SelectedValue}')");
                            if (readerPreco5.HasRows)
                            {
                                while (readerPreco5.Read())
                                {
                                    precoServicoVenda5.valor = readerPreco5.GetDouble(0);
                                }

                                readerPreco5.Close();
                                con.desconectar();
                            }
                            double valorCobrado5;

                            valorCobrado5 = precoServicoVenda5.valor - descontoPServico;

                            vs.qtd5 = int.Parse(cmbQtd5.Text);
                            vs.servico5 = int.Parse(cmbServico5.SelectedValue.ToString());
                            conexao.conectar();
                            for (int i = 1; i <= vs.qtd5; i++)
                            {
                                int linhas2 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico5}','{idVenda}', '{valorCobrado5}') ");
                            }
                            conexao.desconectar();
                        }
                        else
                        {

                        }


                        if (cmbServico6.Text != string.Empty && cmbQtd6.Text != string.Empty)
                        {
                            con.conectar();

                            PrecoVendaServico precoServicoVenda6 = new PrecoVendaServico();
                            SqlDataReader readerPreco6;

                            readerPreco6 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico6.SelectedValue}')");
                            if (readerPreco6.HasRows)
                            {
                                while (readerPreco6.Read())
                                {
                                    precoServicoVenda6.valor = readerPreco6.GetDouble(0);
                                }

                                readerPreco6.Close();
                                con.desconectar();
                            }
                            double valorCobrado6;

                            valorCobrado6 = precoServicoVenda6.valor - descontoPServico;

                            vs.qtd6 = int.Parse(cmbQtd6.Text);
                            vs.servico6 = int.Parse(cmbServico6.SelectedValue.ToString());
                            conexao.conectar();
                            for (int i = 1; i <= vs.qtd6; i++)
                            {
                                int linhas2 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico6}','{idVenda}', '{valorCobrado6}') ");
                            }
                            conexao.desconectar();
                        }
                        else
                        {

                        }


                        if (cmbServico7.Text != string.Empty && cmbQtd7.Text != string.Empty)
                        {
                            con.conectar();

                            PrecoVendaServico precoServicoVenda7 = new PrecoVendaServico();
                            SqlDataReader readerPreco7;

                            readerPreco7 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico7.SelectedValue}')");
                            if (readerPreco7.HasRows)
                            {
                                while (readerPreco7.Read())
                                {
                                    precoServicoVenda7.valor = readerPreco7.GetDouble(0);
                                }

                                readerPreco7.Close();
                                con.desconectar();
                            }
                            double valorCobrado7;

                            valorCobrado7 = precoServicoVenda7.valor - descontoPServico;

                            vs.qtd7 = int.Parse(cmbQtd7.Text);
                            vs.servico7 = int.Parse(cmbServico7.SelectedValue.ToString());
                            conexao.conectar();
                            for (int i = 1; i <= vs.qtd7; i++)
                            {
                                int linhas2 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico7}','{idVenda}', '{valorCobrado7}') ");
                            }
                            conexao.desconectar();
                        }
                        else
                        {

                        }


                        if (cmbServico8.Text != string.Empty && cmbQtd8.Text != string.Empty)
                        {
                            con.conectar();

                            PrecoVendaServico precoServicoVenda8 = new PrecoVendaServico();
                            SqlDataReader readerPreco8;

                            readerPreco8 = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico8.SelectedValue}')");
                            if (readerPreco8.HasRows)
                            {
                                while (readerPreco8.Read())
                                {
                                    precoServicoVenda8.valor = readerPreco8.GetDouble(0);
                                }

                                readerPreco8.Close();
                                con.desconectar();
                            }
                            double valorCobrado8;

                            valorCobrado8 = precoServicoVenda8.valor - descontoPServico;

                            vs.qtd8 = int.Parse(cmbQtd8.Text);
                            vs.servico8 = int.Parse(cmbServico8.SelectedValue.ToString());
                            conexao.conectar();
                            for (int i = 1; i <= vs.qtd8; i++)
                            {
                                int linhas2 = conexao.executar($"INSERT INTO VendasServicos(idServico, idVenda, valorCobrado) VALUES('{vs.servico8}','{idVenda}', '{valorCobrado8}') ");
                            }
                            conexao.desconectar();
                        }
                        else
                        {

                        }
                        MessageBox.Show("Prestação de Serviço Registrada com Sucesso", "Confirmção", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblPendencias.Text = "";
                        limpaCampos();
                        bloqueiaCampos();
                        ocultaCmbs();
                        btnNova.Enabled = true;
                        btnCancelar.Enabled = false;
                        btnConcluido.Enabled = false;
                        txtDesconto.Text = "0";
                    }

                    else
                    {
                        MessageBox.Show("Campo desconto só aceita números", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Campo placa com mais de 7 caracteres", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("É necessário preencher todos os dados!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void VendaFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void limpaCampos()
        {
            flagLimpaCampos = 1;
            txtPlaca.Text = "";
            txtCarro.Text = "";
            txtDesconto.Text = "0";

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
            flagLimpaCampos = 0;
        }
        private void bloqueiaCampos()
        {
            txtDesconto.ReadOnly = true;
            cmbCliente.Enabled = false;
            txtCarro.Enabled = false;
            txtPlaca.Enabled = false;
            DtVenda.Enabled = false;
            cmbServico1.Enabled = false;
            cmbQtd1.Enabled = false;
            btnAdd1.Enabled = false;

            rdbBoleto.Enabled = false;
            rdbCredito.Enabled = false;
            rdbDebito.Enabled = false;
            rdbDinheiro.Enabled = false;
            rdbPermuta.Enabled = false;
            rdbTransferencia.Enabled = false;
        }
        private void desbloqueiaCampos()
        {
            txtDesconto.ReadOnly = false;
            cmbCliente.Enabled = true;
            txtCarro.Enabled = true;
            txtPlaca.Enabled = true;
            DtVenda.Enabled = true;
            cmbServico1.Enabled = true;
            cmbQtd1.Enabled = true;
            btnAdd1.Enabled = true;


            rdbBoleto.Enabled = true;
            rdbCredito.Enabled = true;
            rdbDebito.Enabled = true;
            rdbDinheiro.Enabled = true;
            rdbPermuta.Enabled = true;
            rdbTransferencia.Enabled = true;
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

            btnSelecionarCarro.Visible = false;

        }
        private void prencheCmbCliente()
        {
            Cliente primeiroValor = new Cliente();
            List<Cliente> listCliente = new List<Cliente>();

            primeiroValor.nome = "";
            listCliente.Add(primeiroValor);

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("select idCliente, nome from CLiente order by nome ASC");

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
                con.desconectar();

            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbCliente.DataSource = listCliente;
            cmbCliente.ValueMember = "id";
            cmbCliente.DisplayMember = "nome";
            cmbCliente.Text = "";
            FlagPreencheCmbCliente = 1;


        }
        private void PreencheCmb1()
        {
            FlagPreencheCmbServico = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico1.DataSource = listServico;
            cmbServico1.ValueMember = "id";
            cmbServico1.DisplayMember = "nome";
            cmbServico1.Text = "";
            FlagPreencheCmbServico = 1;
        }
        private void PreencheCmb2()
        {
            flagPreencheCmb2 = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico2.DataSource = listServico;
            cmbServico2.ValueMember = "id";
            cmbServico2.DisplayMember = "nome";
            cmbServico2.Text = "";
            flagPreencheCmb2 = 1;
        }
        private void PreencheCmb3()
        {
            flagPreencheCmb3 = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico3.DataSource = listServico;
            cmbServico3.ValueMember = "id";
            cmbServico3.DisplayMember = "nome";
            cmbServico3.Text = "";
            flagPreencheCmb3 = 1;
        }
        private void PreencheCmb4()
        {
            flagPreencheCmb4 = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico4.DataSource = listServico;
            cmbServico4.ValueMember = "id";
            cmbServico4.DisplayMember = "nome";
            cmbServico4.Text = "";
            flagPreencheCmb4 = 1;
        }
        private void PreencheCmb5()
        {
            flagPreencheCmb5 = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico5.DataSource = listServico;
            cmbServico5.ValueMember = "id";
            cmbServico5.DisplayMember = "nome";
            cmbServico5.Text = "";
            flagPreencheCmb5 = 1;
        }
        private void PreencheCmb6()
        {
            flagPreencheCmb6 = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico6.DataSource = listServico;
            cmbServico6.ValueMember = "id";
            cmbServico6.DisplayMember = "nome";
            cmbServico6.Text = "";
            flagPreencheCmb6 = 1;
        }
        private void PreencheCmb7()
        {
            flagPreencheCmb7 = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico7.DataSource = listServico;
            cmbServico7.ValueMember = "id";
            cmbServico7.DisplayMember = "nome";
            cmbServico7.Text = "";
            flagPreencheCmb7 = 1;
        }
        private void PreencheCmb8()
        {
            flagPreencheCmb8 = 0;
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
                con.desconectar();
            }
            else
            {
                MessageBox.Show("Não foram encontrado dados.", "", MessageBoxButtons.OK);
            }
            cmbServico8.DataSource = listServico;
            cmbServico8.ValueMember = "id";
            cmbServico8.DisplayMember = "nome";
            cmbServico8.Text = "";
            flagPreencheCmb8 = 1;
        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            if (cmbQtd1.Text != string.Empty && cmbServico1.Text != string.Empty)
            {
                if (flagPreencheCmb2 == 0)
                {
                    PreencheCmb2();
                }
                lblQtd2.Visible = true;
                lblServico2.Visible = true;
                cmbServico2.Visible = true;
                btnAdd2.Visible = true;
                cmbQtd2.Visible = true;
            }
            else
            {
                MessageBox.Show("Não é possível adicionar um novo serviço sem ter preenchido o atual", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd2_Click(object sender, EventArgs e)
        {
            if (cmbQtd2.Text != string.Empty && cmbServico2.Text != string.Empty)
            {
                if (flagPreencheCmb3 == 0)
                {
                    PreencheCmb3();
                }
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
                if (flagPreencheCmb4 == 0)
                {
                    PreencheCmb4();
                }
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
                if (flagPreencheCmb5 == 0)
                {
                    PreencheCmb5();
                }
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
                if (flagPreencheCmb6 == 0)
                {
                    PreencheCmb6();
                }
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
                if (flagPreencheCmb7 == 0)
                {
                    PreencheCmb7();
                }
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
            if (flagPreencheCmb8 == 0)
            {
                PreencheCmb8();
            }
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

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            ConsultaVendaServiço n = new ConsultaVendaServiço();
            n.Show();
            this.Visible = false;
        }

        private void btnNovoCliente_Click(object sender, EventArgs e)
        {
            NovoClienteFrm n = new NovoClienteFrm();
            n.Show();
            this.Visible = false;

        }

        private void cmbCliente_TextUpdate(object sender, EventArgs e)
        {

        }

        private void cmbCliente_TextChanged(object sender, EventArgs e)
        {
            if (cmbCliente.Text == string.Empty)
            {
                txtCarro.Text = "";
                txtPlaca.Text = "";

            }
            else
            {
                btnSelecionarCarro.Visible = true;

                String nomeTesta;

                txtCarro.Text = "";
                txtPlaca.Text = "";

                con.conectar();

                SqlDataReader reader;

                reader = con.exeCliente("SELECT carro, placa, [Cliente].[nome] FROM CarrosClientes INNER JOIN Cliente ON ([CarrosClientes].[idCliente] = [Cliente].[idCliente]) WHERE ativo = 1 ");

                if (reader.HasRows)
                {
                    i = 0;
                    while (reader.Read())
                    {
                        nomeTesta = reader.GetString(2);
                        if (cmbCliente.Text == nomeTesta)
                        {
                            txtCarro.Text = reader.GetString(0).Trim();
                            txtPlaca.Text = reader.GetString(1).Trim();
                            i++;
                        }

                    }

                    reader.Close();
                    con.desconectar();
                }

                if (FlagPreencheCmbCliente == 0)
                {
                    return;
                }
                else
                {
                    con.conectar();

                    SqlDataReader readerPendencia;

                    readerPendencia = con.exeCliente($"SELECT valorCobrado from Vendas INNER JOIN Cliente ON ([Vendas].[idCliente] = [Cliente].[idCliente]) WHERE [Vendas].[pago] = 0 AND [Cliente].[nome] LIKE ('{cmbCliente.Text}')");

                    if (readerPendencia.HasRows)
                    {
                        valorTotalaCobrar = 0;
                        contPendencias = 0;
                        while (readerPendencia.Read())
                        {
                            valorTotalaCobrar += readerPendencia.GetDouble(0);
                            contPendencias++;
                        }

                        lblPendencias.Text = $"{cmbCliente.Text} tem {contPendencias} pendências no valor de {valorTotalaCobrar} reais";
                    }
                    else
                    {
                        lblPendencias.Text = $"{cmbCliente.Text} não tem nenhuma pendência ";
                    }
                    readerPendencia.Close();
                    con.desconectar();

                }
            }
        }

        private void btnSelecionarCarro_Click(object sender, EventArgs e)
        {
            SelecionaCarro selecionaCarro = new SelecionaCarro(cmbCliente.Text);
            selecionaCarro.Show();
            this.Visible = false;
        }

        private void atualizaTotalESubTotal()
        {
            if (FlagPreencheCmbServico == 0)
            {
                return;
            }

            precoAtt = 0;
            ValorVenda valorVenda = new ValorVenda();

            con.conectar();

            SqlDataReader readerPreco;


            if (cmbServico1.Text != string.Empty && cmbQtd1.Text != string.Empty)
            {
                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico1.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd1.Text);
                    }
                    readerPreco.Close();
                    con.desconectar();
                }

                precoAtt = valorVenda.precoServico * valorVenda.qtd;

            }

            if (cmbServico2.Text != string.Empty && cmbQtd2.Text != string.Empty)
            {
                ValorVenda valorVenda2 = new ValorVenda();

                con.conectar();

                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico2.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd2.Text);
                    }

                    readerPreco.Close();
                    con.desconectar();
                }

                precoAtt += valorVenda.precoServico * valorVenda.qtd;
            }
            if (cmbServico3.Text != string.Empty && cmbQtd3.Text != string.Empty)
            {
                ValorVenda valorVenda3 = new ValorVenda();

                con.conectar();

                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico3.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd3.Text);
                    }

                    readerPreco.Close();
                    con.desconectar();
                }
                precoAtt += valorVenda.precoServico * valorVenda.qtd;

            }
            if (cmbServico4.Text != string.Empty && cmbQtd4.Text != string.Empty)
            {
                ValorVenda valorVenda4 = new ValorVenda();

                con.conectar();

                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico4.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd4.Text);
                    }

                    readerPreco.Close();
                    con.desconectar();
                }
                precoAtt += valorVenda.precoServico * valorVenda.qtd;

            }
            if (cmbServico5.Text != string.Empty && cmbQtd5.Text != string.Empty)
            {
                ValorVenda valorVenda5 = new ValorVenda();

                con.conectar();

                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico5.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd5.Text);
                    }

                    readerPreco.Close();
                    con.desconectar();
                }
                precoAtt += valorVenda.precoServico * valorVenda.qtd;

            }
            if (cmbServico6.Text != string.Empty && cmbQtd6.Text != string.Empty)
            {
                ValorVenda valorVenda6 = new ValorVenda();

                con.conectar();

                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico6.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd6.Text);
                    }

                    readerPreco.Close();
                    con.desconectar();
                }
                precoAtt += valorVenda.precoServico * valorVenda.qtd;

            }
            if (cmbServico7.Text != string.Empty && cmbQtd7.Text != string.Empty)
            {
                ValorVenda valorVenda7 = new ValorVenda();

                con.conectar();

                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico7.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd7.Text);
                    }

                    readerPreco.Close();
                    con.desconectar();
                }
                precoAtt += valorVenda.precoServico * valorVenda.qtd;

            }
            if (cmbServico8.Text != string.Empty && cmbQtd8.Text != string.Empty)
            {
                ValorVenda valorVenda3 = new ValorVenda();

                con.conectar();

                readerPreco = con.exeCliente($"select preco from Servicos WHERE idServico = ('{cmbServico8.SelectedValue}')");
                if (readerPreco.HasRows)
                {
                    while (readerPreco.Read())
                    {
                        valorVenda.precoServico = readerPreco.GetDouble(0);
                        valorVenda.qtd = double.Parse(cmbQtd8.Text);
                    }

                    readerPreco.Close();
                    con.desconectar();
                }
                precoAtt += valorVenda.precoServico * valorVenda.qtd;

            }
            lblSubTotal.Text = precoAtt.ToString("C");

            if (txtDesconto.Text != string.Empty)
            {
                double desconto = double.Parse(txtDesconto.Text);
                double precoTotal = precoAtt - desconto;
                lblTotal.Text = precoTotal.ToString("C");
            }
            else
            {
                lblTotal.Text = precoAtt.ToString("C");
            }
        }

        private void cmbServico1_TextChanged(object sender, EventArgs e)
        {
            if (FlagPreencheCmbServico == 1 && flagLimpaCampos == 0)
            {
                atualizaTotalESubTotal();
            }
            else
            {
                return;
            }
        }

        private void cmbQtd1_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();

        }

        private void cmbServico2_TextChanged(object sender, EventArgs e)
        {
            if (flagPreencheCmb2 == 0 || flagLimpaCampos == 1)
            {
                return;
            }
            else
            {
                atualizaTotalESubTotal();
            }
        }

        private void cmbQtd2_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();
        }

        private void cmbServico3_TextChanged(object sender, EventArgs e)
        {
            if (flagPreencheCmb3 == 0 || flagLimpaCampos == 1)
            {
                return;
            }
            else
            {
                atualizaTotalESubTotal();
            }
        }

        private void cmbQtd3_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();
        }

        private void cmbServico4_TextChanged(object sender, EventArgs e)
        {
            if (flagPreencheCmb4 == 0 || flagLimpaCampos == 1)
            {
                return;
            }
            else
            {
                atualizaTotalESubTotal();
            }
        }

        private void cmbQtd4_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();
        }

        private void cmbServico5_TextChanged(object sender, EventArgs e)
        {
            if (flagPreencheCmb5 == 0 || flagLimpaCampos == 1)
            {
                return;
            }
            else
            {
                atualizaTotalESubTotal();
            }
        }

        private void cmbQtd5_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();
        }

        private void cmbServico6_TextChanged(object sender, EventArgs e)
        {
            if (flagPreencheCmb6 == 0 || flagLimpaCampos == 1)
            {
                return;
            }
            else
            {
                atualizaTotalESubTotal();
            }
        }

        private void cmbQtd6_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();
        }

        private void cmbServico7_TextChanged(object sender, EventArgs e)
        {
            if (flagPreencheCmb7 == 0 || flagLimpaCampos == 1)
            {
                return;
            }
            else
            {
                atualizaTotalESubTotal();
            }
        }

        private void cmbQtd7_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();
        }

        private void cmbServico8_TextChanged(object sender, EventArgs e)
        {
            if (flagPreencheCmb8 == 0 || flagLimpaCampos == 1)
            {
                return;
            }
            else
            {
                atualizaTotalESubTotal();
            }
        }

        private void cmbQtd8_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();

        }

        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            if (flagLimpaCampos == 1)
            {
                return;
            }
            atualizaTotalESubTotal();

        }

        private void cmbServico1_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}
