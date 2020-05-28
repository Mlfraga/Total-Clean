using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace totalClean
{
    class Conexao
    {
        private String dadosCon = @"Server=tcp:total-clean-server.database.windows.net,1433;Initial Catalog=TESTE;Persist Security Info=False;User ID=AdminTotalCLean;Password=Mm884741;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private SqlConnection sqlCon;

        public void conectar()
        {
            sqlCon = new SqlConnection(dadosCon);
            try
            {
                sqlCon.Open();
                Console.WriteLine("Conectado!");

            }
            catch (SqlException sqlE)

            {
                System.Windows.Forms.MessageBox.Show("Erro de conexão: " + sqlE, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            }
        }

        public void desconectar()
        {
            sqlCon.Close();
            Console.WriteLine("desconectado");
        }

        public int executar(String sql)

        {

            SqlCommand sqlc = new SqlCommand(sql, sqlCon);
            return sqlc.ExecuteNonQuery();

        }
        public SqlDataReader exeCliente(String sql)

        {

            SqlCommand sqlc = new SqlCommand(sql, sqlCon);
            return sqlc.ExecuteReader();

        }
    }
}
