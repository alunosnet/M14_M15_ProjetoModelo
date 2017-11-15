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
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        //ficheiro - sair
        private void sairToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }
        //editar - livros
        private void livrosToolStripMenuItem_Click(object sender, EventArgs e) {
            frLivros f = new frLivros();
            f.Show();
        }

        private void leitoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frLeitores f = new frLeitores();
            f.Show();
        }

        private void empréstimosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frEmprestimos f = new frEmprestimos();
            f.Show();
        }

        private void devoluçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frDevolve f = new frDevolve();
            f.Show();
        }

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e) {
            frConsultas f = new frConsultas();
            f.Show();
        }
    }
}
