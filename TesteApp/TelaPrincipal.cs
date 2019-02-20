using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteApp
{
    public partial class TelaPrincipal : Form
    {
        public TelaPrincipal()
        {
            InitializeComponent();
        }

        private void codigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaCod FormConsultaCod = new ConsultaCod();

            FormConsultaCod.MdiParent = this;

            FormConsultaCod.Show();
        }

        private void retiradaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TelaPrincipal_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void todosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaTodos frmConsultaTodos = new ConsultaTodos();

            frmConsultaTodos.MdiParent = this;

            frmConsultaTodos.Show();
        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultaEmp frmConsultaEmp = new ConsultaEmp();

            frmConsultaEmp.MdiParent = this;

            frmConsultaEmp.Show();
        }
    }
}
