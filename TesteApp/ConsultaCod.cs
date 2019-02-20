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
    public partial class ConsultaCod : Form
    {
        public String Codigo;

        public ConsultaCod()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                String conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\BOB\Desktop\BancoPecas.mdb";
                OleDbConnection conn = new OleDbConnection(conexao);
                conn.Open();

                String SQL;

                double fator, peso ,qtdePecas;

                fator = Convert.ToDouble(txtFator.Text);
                peso = Convert.ToDouble(txtPeso.Text);
                
               
                qtdePecas = peso / fator;
               

                SQL = "Insert into Pecas (Codigo,Produto,Fator,Peso,QtdePecas) Values " +
                    "('" + txtCodigo.Text + "', '" + txtProduto.Text + "','" + txtFator.Text +"','"+txtPeso.Text+"','"+qtdePecas+"')";

                OleDbCommand cmd = new OleDbCommand(SQL, conn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Inserido com sucesso!");

                btnInserir.Enabled = false;
                btnConsultar.Enabled = true;

                txtCodigo.Clear();
                txtProduto.Clear();
                txtFator.Clear();
                txtPeso.Clear();

                conn.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if(ch == 46 && txtCodigo.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
            }

            if(!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            String conexao = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\BOB\Desktop\BancoPecas.mdb";
            OleDbConnection conn = new OleDbConnection(conexao);
            

            String SQL;

            SQL = "select * from Pecas where codigo ="+txtCodigo.Text;

            OleDbCommand cmd = new OleDbCommand(SQL, conn);

            try
            {
                conn.Open();
                OleDbDataReader adapter = cmd.ExecuteReader();
                if (adapter.HasRows)
                {
                    while (adapter.Read())
                    {
                        MessageBox.Show("Deu certo!");

                        txtProduto.Enabled = true;
                        txtFator.Enabled = true;
                        txtPeso.Enabled = true;

                        txtProduto.Text = adapter.GetValue(1).ToString();
                        txtFator.Text = adapter.GetValue(2).ToString();
                        txtPeso.Text = adapter.GetValue(3).ToString();
                        txtQtdePecas.Text = adapter.GetValue(4).ToString();

                        btnConsultar.Enabled = false;
                        btnAlterar.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnRetirar.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Deu errado!");
                    btnInserir.Enabled = true;
                    txtProduto.Enabled = true;
                    txtFator.Enabled = true;
                    txtPeso.Enabled = true;

                    btnConsultar.Enabled = false;
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

            conn.Close();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
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

                txtProduto.Enabled = false;
                txtFator.Enabled = false;
                txtPeso.Enabled = false;

                conn.Close();

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
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

                SQL = "update Pecas set Produto = '"+txtProduto.Text+"',";
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

                txtProduto.Enabled = false;
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

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            groupboxRetirar.Visible = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
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


               

                btnExcluir.Enabled = false;
                btnConsultar.Enabled = true;
                btnAlterar.Enabled = false;

                txtProduto.Enabled = false;
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
