using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseDadosNS;
namespace M14_M15_ProjetoModelo {
    public partial class frLivros : Form {
        //nr de registos por página
        const int registosPorPagina = 5;

        public frLivros() {
            InitializeComponent();
            atualizaGrelha();
            atualizaNrPaginas();
        }
        public void atualizaGrelha() {
            if (comboBox1.SelectedIndex == -1)
                dataGridView1.DataSource = BaseDados.Instance.devolveConsulta("SELECT * FROM Livros");
            else {
                int pagina = comboBox1.SelectedIndex + 1;
                if (pagina <= 0) pagina = 1;
                int primeiroRegisto = (pagina - 1) * registosPorPagina + 1;
                string strSQL = $@"select nlivro as id,nome AS Nome from 
                                (select row_number() over (order by nome) as rownum, *
                                from Livros) as p
                                where rownum>={primeiroRegisto} and rownum<={(primeiroRegisto + registosPorPagina)}";
                dataGridView1.DataSource = BaseDados.Instance.devolveConsulta(strSQL);
            }
            
        }
        private void atualizaNrPaginas() {
            comboBox1.Items.Clear();
            DataTable dados = BaseDados.Instance.devolveConsulta("SELECT count(*) FROM Livros");
            int nRegistos = int.Parse(dados.Rows[0][0].ToString());
            int nPaginas = (int)Math.Ceiling(nRegistos / (float)registosPorPagina);
            for (int i = 1; i <= nPaginas; i++)
                comboBox1.Items.Add(i);
        }
        //adicionar registo
        private void button2_Click(object sender, EventArgs e) {

            //validar os dados do form
            string nome = textBox1.Text;
            int ano = int.Parse(textBox2.Text);
            DateTime data = dateTimePicker1.Value;
            decimal preco = decimal.Parse(textBox3.Text);
            //copiar imagem para uma pasta
            string nomeImagem = DateTime.Now.Ticks.ToString();
            string pastaImagens = Application.UserAppDataPath;
            if (Directory.Exists(pastaImagens) == false)
                Directory.CreateDirectory(pastaImagens);
            string[] extensao = lbCapa.Text.Split('.');
            string nomeCompleto = pastaImagens + "\\" + nomeImagem +"."+ extensao[extensao.Length-1];
            File.Copy(lbCapa.Text, nomeCompleto);
            //insert into
            string sql =$@"INSERT INTO livros(nome,ano,data_aquisicao,preco,capa,estado)
                        VALUES(@nome,@ano,@data_aquisicao,@preco,@capa,@estado)";
            //parâmetros
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=nome},
                new SqlParameter(){ParameterName="@ano",SqlDbType=SqlDbType.Int,Value=ano},
                new SqlParameter(){ParameterName="@data_aquisicao",SqlDbType=SqlDbType.Date,Value=data},
                new SqlParameter(){ParameterName="@preco",SqlDbType=SqlDbType.Decimal,Value=preco},
                new SqlParameter(){ParameterName="@capa",SqlDbType=SqlDbType.VarChar,Value=nomeCompleto},
                new SqlParameter(){ParameterName="@estado",SqlDbType=SqlDbType.Bit,Value=true},
            };
            BaseDados.Instance.executaSQL(sql,parametros);
            //todo: limpar o form
            //atualiza a grelha com os livros
            atualizaGrelha();
            atualizaNrPaginas();
        }
        //escolher uma capa
        private void button1_Click(object sender, EventArgs e) {
            OpenFileDialog cxDialogo = new OpenFileDialog();
            DialogResult resposta= cxDialogo.ShowDialog();
            if (resposta != DialogResult.OK) return;
            lbCapa.Text = cxDialogo.FileName;
            pictureBox1.Image = Image.FromFile(lbCapa.Text);
        }
        //remover livro
        private void button3_Click(object sender, EventArgs e) {
            //linha selecionada
            if (dataGridView1.CurrentCell == null)
                return;
            int linha = dataGridView1.CurrentCell.RowIndex;
            if(linha<0) {
                MessageBox.Show("Selecione o livro a remover.");
                return;
            }
            //nlivro
            int nlivro = int.Parse(dataGridView1.Rows[linha].Cells[0].Value.ToString());
            if (MessageBox.Show("Tem a certeza que pretende eliminar o livro nº " + nlivro, "Remover?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            //remover capa
            string capa = dataGridView1.Rows[linha].Cells[5].Value.ToString();
            //possível erro!
            if(File.Exists(capa))
                File.Delete(capa);
            //executar sql delete
            string sql = "DELETE FROM livros WHERE nlivro=" + nlivro;
            BaseDados.Instance.executaSQL(sql);
            //atualizagrelha
            atualizaGrelha();
        }
        //menu contexto da grid
        private void editarToolStripMenuItem_Click(object sender, EventArgs e) {
            //nlivro
            if (dataGridView1.CurrentCell == null) return;
            int linha = dataGridView1.CurrentCell.RowIndex;
            int nlivro = int.Parse(dataGridView1.Rows[linha].Cells[0].Value.ToString());
            //form editar livro
            frEditarLivro f = new frEditarLivro(nlivro);
            f.ShowDialog();
            atualizaGrelha();
        }
        //pesquisar
        private void button4_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Livros WHERE nome like @nome";
            string nome = "%" + textBox4.Text + "%";
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=nome},
            };
            dataGridView1.DataSource = BaseDados.Instance.devolveConsulta(sql,parametros);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            atualizaGrelha();
        }
    }
}
