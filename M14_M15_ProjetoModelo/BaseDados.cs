using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace M14_M15_ProjetoModelo {
    class BaseDados {
        string strLigacao;
        SqlConnection ligacaoBD;

        public BaseDados() {
            strLigacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            ligacaoBD = new SqlConnection(strLigacao);
            ligacaoBD.Open();
        }
        ~BaseDados() {
            try {
                ligacaoBD.Close();
            } catch (Exception e) {
                Console.Write(e.Message);
            }
        }
        public void executaSQL(string sql) {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
    }
}
