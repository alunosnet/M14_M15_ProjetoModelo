using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseDadosNS;
namespace M14_M15_ProjetoModelo {
    public partial class frConsultas : Form {
        public frConsultas() {
            InitializeComponent();
        }

        private void livrosEmprestadosToolStripMenuItem_Click(object sender, EventArgs e) {
            //listar nome dos livros emprestados
            string sql = "select nome from livros where estado=0;";
            dataGridView1.DataSource = BaseDados.Instance.devolveConsulta(sql);
        }
    }
}
