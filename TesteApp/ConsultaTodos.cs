using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace TesteApp
{
    public partial class ConsultaTodos : Form
    {
        public ConsultaTodos()
        {
            InitializeComponent();
        }

        private void ConsultaTodos_Load(object sender, EventArgs e)
        {
            try
            {
                String conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\BOB\Desktop\BancoPecas.mdb";
                OleDbConnection conn = new OleDbConnection(conexao);
                conn.Open();

                String SQL = "select * from Pecas";

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, conn);

                DataSet DS = new DataSet();

                adapter.Fill(DS, "pecas");

                dgResultado.DataSource = DS.Tables["pecas"];



            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void alterarToolStripAlterar_Click(object sender, EventArgs e)
        {
            ConsultaCod frmConsultaCod = new ConsultaCod();

            frmConsultaCod.txtProduto.Enabled = true;
            frmConsultaCod.txtFator.Enabled = true;
            frmConsultaCod.txtPeso.Enabled = true;
            frmConsultaCod.btnExcluir.Enabled = true;
            

            frmConsultaCod.txtCodigo.Text = dgResultado.SelectedCells[0].Value.ToString();
            frmConsultaCod.txtProduto.Text = dgResultado.SelectedCells[1].Value.ToString();
            frmConsultaCod.txtFator.Text = dgResultado.SelectedCells[2].Value.ToString();
            frmConsultaCod.txtPeso.Text = dgResultado.SelectedCells[3].Value.ToString();
            frmConsultaCod.txtQtdePecas.Text = dgResultado.SelectedCells[4].Value.ToString();

            frmConsultaCod.ShowDialog();
        }
    }
}
