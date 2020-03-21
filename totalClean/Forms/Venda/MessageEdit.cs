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
    public partial class MessageEdit : Form
    {
        public MessageEdit()
        {
            InitializeComponent();
        }
        Conexao con = new Conexao();
        private void MessageEdit_Load(object sender, EventArgs e)
        {                                    
            List<Cliente> listCliente = new List<Cliente>();

            con.conectar();

            SqlDataReader reader;

            reader = con.exeCliente("SELECT mensagemWpp FROM Configuracao ");
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    txtMensagem.Text = reader.GetString(0);
                }
                reader.Close();
            }
        }

        private void btnConcluido_Click(object sender, EventArgs e)
        {
            int aMmsg = con.executar($"UPDATE [dbo].[Configuracao] set mensagemWpp = '" + txtMensagem.Text + "' WHERE id = 1");

            MessageBox.Show("Mensagem alterada com sucesso", "Confirmação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
            
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
