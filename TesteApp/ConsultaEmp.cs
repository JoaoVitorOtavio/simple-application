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
    public partial class ConsultaEmp : Form
    {
        public ConsultaEmp()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                String conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\BOB\Desktop\BancoPecas.mdb";
                OleDbConnection conn = new OleDbConnection(conexao);
                conn.Open();

                String SQL = "select * from Pecas where Produto like '%" + txtProduto.Text + "%'";

                OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, conn);

                DataSet DS = new DataSet();

                adapter.Fill(DS, "pecas");

                dataGridPesqEmp.DataSource = DS.Tables["pecas"];



            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtCodigo.Text=  dataGridPesqEmp.SelectedCells[0].Value.ToString();
            txtProduto.Text = dataGridPesqEmp.SelectedCells[1].Value.ToString();
            txtFator.Text = dataGridPesqEmp.SelectedCells[2].Value.ToString();
            txtPeso.Text = dataGridPesqEmp.SelectedCells[3].Value.ToString();
            txtQtdePecas.Text = dataGridPesqEmp.SelectedCells[4].Value.ToString();

            txtCodigo.Enabled = true;
            txtFator.Enabled = true;
            txtPeso.Enabled = true;


            btnConsultar.Enabled = false;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnRetirar.Enabled = true;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                String conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\BOB\Desktop\BancoPecas.mdb";
                OleDbConnection conn = new OleDbConnection(conexao);

                double fator, peso, qtdePecas;

                fator = Convert.ToDouble(txtFator.Text);
                peso = Convert.ToDouble(txtPeso.Text);


                qtdePecas = peso / fator;

                String SQL;

                SQL = "update Pecas set Produto = '" + txtProduto.Text + "',";
                SQL += "Fator = '" + txtFator.Text + "',";
                SQL += "Peso = '" + txtPeso.Text + "',";
                SQL += "QtdePecas ='" + qtdePecas + "' ";
                SQL += "where Codigo =" + txtCodigo.Text;


                OleDbCommand cmd = new OleDbCommand(SQL, conn);

                conn.Open();

                cmd.ExecuteNonQuery();

                MessageBox.Show("Alterado com sucesso!");

                btnExcluir.Enabled = false;
                btnConsultar.Enabled = true;
                btnAlterar.Enabled = false;

                txtCodigo.Enabled = false;
                txtFator.Enabled = false;
                txtPeso.Enabled = false;

                txtCodigo.Clear();
                txtProduto.Clear();
                txtFator.Clear();
                txtPeso.Clear();
                txtQtdePecas.Clear();


                dataGridPesqEmp.DataSource = null;
               
                conn.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                String conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\BOB\Desktop\BancoPecas.mdb";
                OleDbConnection conn = new OleDbConnection(conexao);

                String SQL;

                SQL = "delete * from Pecas where codigo =" + txtCodigo.Text;

                OleDbCommand cmd = new OleDbCommand(SQL, conn);

                conn.Open();

                cmd.ExecuteNonQuery();

                MessageBox.Show("Excluido com sucesso!");

                txtCodigo.Clear();
                txtProduto.Clear();
                txtFator.Clear();
                txtPeso.Clear();
                txtQtdePecas.Clear();

                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                btnConsultar.Enabled = true;

                txtCodigo.Enabled = false;
                txtFator.Enabled = false;
                txtPeso.Enabled = false;

                dataGridPesqEmp.DataSource = null;

                conn.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            groupboxRetirar.Visible = true;
            btnConsultar.Enabled = false;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;
            btnRetirar.Enabled = false;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                String conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\BOB\Desktop\BancoPecas.mdb";
                OleDbConnection conn = new OleDbConnection(conexao);

                double fator, peso, retirar, qtdePecas;

                retirar = Convert.ToDouble(txtQtdRetirar.Text);
                fator = Convert.ToDouble(txtFator.Text);
                peso = Convert.ToDouble(txtPeso.Text);

                if (retirar > peso)
                {
                    MessageBox.Show("Impossivel retirar essa quantidade!");
                    txtQtdRetirar.Clear();
                    groupboxRetirar.Visible = false;
                   

                }
                else
                {
                    peso = Convert.ToDouble(txtPeso.Text) - retirar;
                    qtdePecas = peso / fator;

                    String SQL;

                    SQL = "update Pecas set Produto = '" + txtProduto.Text + "',";
                    SQL += "Fator = '" + txtFator.Text + "',";
                    SQL += "Peso = '" + peso + "',";
                    SQL += "QtdePecas ='" + qtdePecas + "' ";
                    SQL += "where Codigo =" + txtCodigo.Text;


                    OleDbCommand cmd = new OleDbCommand(SQL, conn);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Retirado com sucesso!");

                    txtQtdRetirar.Clear();
                    groupboxRetirar.Visible = false;
                }

                dataGridPesqEmp.DataSource = null;


                btnExcluir.Enabled = false;
                btnConsultar.Enabled = true;
                btnAlterar.Enabled = false;
                btnRetirar.Enabled = false;

                txtCodigo.Enabled = false;
                txtFator.Enabled = false;
                txtPeso.Enabled = false;

                txtCodigo.Clear();
                txtProduto.Clear();
                txtFator.Clear();
                txtPeso.Clear();
                txtQtdePecas.Clear();

                conn.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }
    }
}
