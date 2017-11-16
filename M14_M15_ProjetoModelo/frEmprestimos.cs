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
    public partial class frEmprestimos : Form
    {
        public frEmprestimos()
        {
            InitializeComponent();
            preencheCBLeitores();
            preencheCBLivros();
        }

        public void preencheCBLeitores()
        {
            string sql = "SELECT nleitor,nome FROM Leitores ORDER BY nome";
            DataTable dados = BaseDados.Instance.devolveConsulta(sql);
            cbLeitores.Items.Clear();
            foreach (DataRow linha in dados.Rows)
            {
                cbLeitores.Items.Add(new Elemento()
                {
                    id = int.Parse(linha[0].ToString()),
                    texto = linha[1].ToString()
                });
            }
        }
        public void preencheCBLivros()
        {
            string sql = "SELECT nlivro,nome FROM Livros WHERE estado=1 ORDER BY nome";
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
            //validar dados
            if (cbLivros.SelectedIndex < 0 || cbLeitores.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione o livro e o leitor primeiro.");
                return;
            }
            //adicionar à tabela de empréstimos
            int nlivro = ((Elemento)cbLivros.SelectedItem).id;
            int nleitor = ((Elemento)cbLeitores.SelectedItem).id;
            //iniciar uma transação
            SqlTransaction transacao = BaseDados.Instance.iniciarTransacao();
            try
            {
                string sql = @"INSERT INTO emprestimos(nlivro,nleitor,data_emprestimo,data_devolve,estado) 
                        VALUES(@nlivro,@nleitor,@data_emprestimo,@data_devolve,@estado)";
                List<SqlParameter> parametros = new List<SqlParameter>() {
                    new SqlParameter(){ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro},
                    new SqlParameter(){ParameterName="@nleitor",SqlDbType=SqlDbType.Int,Value=nleitor},
                    new SqlParameter(){ParameterName="@data_emprestimo",SqlDbType=SqlDbType.Date,Value=dateTimePicker1.Value},
                    new SqlParameter(){ParameterName="@data_devolve",SqlDbType=SqlDbType.Date,Value=dateTimePicker2.Value},
                    new SqlParameter(){ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=true},
                };
                BaseDados.Instance.executaSQL(sql, parametros, transacao);

                //alterar estado do livro
                sql = "UPDATE Livros SET estado=@estado WHERE nlivro=@nlivro";
                parametros = new List<SqlParameter>()
                {
                    new SqlParameter(){ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=false},
                    new SqlParameter(){ParameterName="@nlivro",SqlDbType=SqlDbType.Int,Value=nlivro},
                };
                BaseDados.Instance.executaSQL(sql, parametros, transacao);
            }
            catch (Exception erro)
            {
                transacao.Rollback();
                MessageBox.Show("Ocorreu um erro. Tente novamente. "+erro.Message);
                return;
            }
            //concluir a transação
            transacao.Commit();
            //atualizar cb livros
            preencheCBLivros();
        }
    }
}
