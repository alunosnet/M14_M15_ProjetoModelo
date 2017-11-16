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
    public partial class frLeitores : Form
    {
        public frLeitores()
        {
            InitializeComponent();
            atualizaGrelha();
        }

        public void atualizaGrelha()
        {
            dataGridView1.DataSource = BaseDados.Instance.devolveConsulta("select * from leitores");
        }
        //escolher
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog cxDialogo = new OpenFileDialog();
            DialogResult resposta = cxDialogo.ShowDialog();
            if (resposta != DialogResult.OK) return;
            lbFoto.Text = cxDialogo.FileName;
            pictureBox1.Image = Image.FromFile(lbFoto.Text);
        }
        //adicionar
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] imagem = Utils.ImagemParaVetor(lbFoto.Text);
            string sql = @"INSERT INTO Leitores(nome,data_nasc,fotografia,ativo) VALUES
                            (@nome,@data_nasc,@fotografia,@ativo)";
            List<SqlParameter> parametros = new List<SqlParameter>() {
                new SqlParameter(){ParameterName="@nome",SqlDbType=SqlDbType.VarChar,Value=textBox1.Text},
                new SqlParameter(){ParameterName="@data_nasc",SqlDbType=SqlDbType.Date,Value=dateTimePicker1.Value},
                new SqlParameter(){ParameterName="@fotografia",SqlDbType=SqlDbType.Image,Value=imagem},
                new SqlParameter(){ParameterName="@ativo",SqlDbType=SqlDbType.Bit,Value=true},
            };
            BaseDados.Instance.executaSQL(sql, parametros);
            atualizaGrelha();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //limpar picturebox
            pictureBox1.Image = null;
            //gc collect
            GC.Collect();
            //linha clicada
            int linha = dataGridView1.CurrentCell.RowIndex;
            //nleitor
            int nleitor = int.Parse(dataGridView1.Rows[linha].Cells[0].Value.ToString());
            //consulta
            string sql = "SELECT * FROM Leitores WHERE nleitor=" + nleitor;
            DataTable dados = BaseDados.Instance.devolveConsulta(sql);
            //criar o ficheiro com a fotografia
            byte[] imagem =(byte[]) dados.Rows[0][3];
            Utils.VetorParaImagem(imagem, "imagem_temp.jpg");
            //mostrar os dados do leitor
            //nome
            textBox1.Text = dados.Rows[0][1].ToString();
            //data nascimento
            dateTimePicker1.Text= dados.Rows[0][2].ToString();
            //fotografia
            pictureBox1.Image = Image.FromFile("imagem_temp.jpg");
            dados.Dispose();
        }
    }
}
