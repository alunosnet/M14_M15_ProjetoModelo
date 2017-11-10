using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M14_M15_ProjetoModelo {
    public partial class frEditarLivro : Form {
        public frEditarLivro(int nlivro) {
            InitializeComponent();
            //consulta à bd
            string sql = "SELECT * FROM Livros WHERE nlivro=" + nlivro;
            DataTable livro= BaseDados.Instance.devolveConsulta(sql);
            //preencher o form
            lbNLivro.Text = nlivro.ToString(); 
            //nome
            textBox1.Text = livro.Rows[0][1].ToString();
            //ano
            textBox2.Text = livro.Rows[0][2].ToString();
            dateTimePicker1.Value = DateTime.Parse(livro.Rows[0][3].ToString());
            //preço
            textBox3.Text = livro.Rows[0][4].ToString();
            //capa
            if(File.Exists(livro.Rows[0][5].ToString()))
                pictureBox1.Image = Image.FromFile(livro.Rows[0][5].ToString());

        }
        //cancelar
        private void button2_Click(object sender, EventArgs e) {

        }
        //escolher
        private void button3_Click(object sender, EventArgs e) {

        }
        //guardar
        private void button1_Click(object sender, EventArgs e) {

        }
    }
}
