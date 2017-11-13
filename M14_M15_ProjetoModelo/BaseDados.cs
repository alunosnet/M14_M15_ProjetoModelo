using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace M14_M15_ProjetoModelo {
    class BaseDados {
        private static BaseDados instance;
        public static BaseDados Instance {
            get {
                if (instance == null)
                    instance = new BaseDados();
                return instance;
            }
        }
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
        //iniciar uma transação
        public SqlTransaction iniciarTransacao()
        {
            return ligacaoBD.BeginTransaction();
        }
        /// <summary>
        /// Recebe um comando SQL e executa-o na base de dados
        /// </summary>
        /// <param name="sql">Comando SQL</param>
        public void executaSQL(string sql) {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        /// <summary>
        /// Recebe um comando SQL e a lista de parâmetros e executa-o na base de dados
        /// </summary>
        /// <param name="sql">Comando SQL</param>
        /// <param name="parametros">Lista de parâmetros</param>
        public void executaSQL(string sql,List<SqlParameter> parametros) {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        public void executaSQL(string sql, List<SqlParameter> parametros,SqlTransaction transacao)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            comando.Transaction = transacao;
            comando.ExecuteNonQuery();
            comando.Dispose();
            comando = null;
        }
        public DataTable devolveConsulta(string sql) {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            return registos;
        }
        public DataTable devolveConsulta(string sql, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoBD);
            comando.Parameters.AddRange(parametros.ToArray());
            DataTable registos = new DataTable();
            SqlDataReader dados = comando.ExecuteReader();
            registos.Load(dados);
            dados = null;
            comando.Dispose();
            return registos;
        }
    }
}
