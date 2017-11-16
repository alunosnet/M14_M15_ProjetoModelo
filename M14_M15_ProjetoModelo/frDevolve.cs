using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseDadosNS;

namespace M14_M15_ProjetoModelo
{
    public partial class frDevolve : Form
    {
        public frDevolve()
        {
            InitializeComponent();
            preencheCBLivros();
        }
        public void preencheCBLivros()
        {
            string sql = "SELECT nlivro,nome FROM Livros WHERE estado=0 ORDER BY nome";
            DataTable dados = BaseDados.Instance.devolveConsulta(sql);
            cbLivros.Items.Clear();
            foreach (DataRow linha in dados.Rows)
            {
                cbLivros.Items.Add(new Elemento()
                {
                    id = int.Parse(linha[0].ToString()),
                    texto = linha[1].ToString()
                });
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbLivros.SelectedIndex < 0 )
            {
                MessageBox.Show("Selecione o livro primeiro.");
                return;
            }
            //adicionar à tabela de empréstimos
            int nlivro = ((Elemento)cbLivros.SelectedItem).id;
            SqlTransaction transacao = BaseDados.Instance.iniciarTransacao();
            try
            {
                string sql = "UPDATE Livros SET estado=@estado WHERE nlivro=@nlivro";
                List<SqlParameter> parametros = new List<SqlParameter>() {
                    new SqlParameter(){ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=true},
                    new SqlParameter(){ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro},
                };
                BaseDados.Instance.executaSQL(sql, parametros, transacao);
                 sql = "UPDATE Emprestimos SET estado=@estado WHERE nlivro=@nlivro";
                parametros = new List<SqlParameter>() {
                    new SqlParameter(){ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=false},
                    new SqlParameter(){ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro},
                };
                BaseDados.Instance.executaSQL(sql, parametros, transacao);
            }
            catch (Exception erro)
            {
                transacao.Rollback();
            }
            transacao.Commit();
            preencheCBLivros();
        }
    }
}
