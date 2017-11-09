using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M14_M15_ProjetoModelo {
    public partial class frLivros : Form {
        public frLivros() {
            InitializeComponent();

        }
        //adicionar registo
        private void button2_Click(object sender, EventArgs e) {

            //validar os dados do form
            string nome = textBox1.Text;
            int ano = int.Parse(textBox2.Text);
            DateTime data = dateTimePicker1.Value;
            decimal preco = decimal.Parse(textBox3.Text);
            //copiar imagem para uma pasta

            //insert into
            string sql =$@"INSERT INTO livros(nome,ano,data_aquisicao,preco)
                        VALUES('{nome}',{ano},'{data}',{preco})";
            BaseDados bd = new BaseDados();
            bd.executaSQL(sql);
        }
    }
}
