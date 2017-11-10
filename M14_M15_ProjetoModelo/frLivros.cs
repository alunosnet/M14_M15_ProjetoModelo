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

namespace M14_M15_ProjetoModelo {
    public partial class frLivros : Form {
        public frLivros() {
            InitializeComponent();
            atualizaGrelha();
        }
        public void atualizaGrelha() {
            dataGridView1.DataSource = BaseDados.Instance.devolveConsulta("SELECT * FROM Livros");
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
    }
}
