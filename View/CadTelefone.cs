using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View.Entidades;
using View.Modelo;

namespace View
{
    public partial class CadTelefone : Form
    {
        public CadTelefone()
        {
            InitializeComponent();
        }

        UsuarioEnt objTabela = new UsuarioEnt();                //objTabela exibira oq tem dentro do UsuarioEnt / pega todos os componetes

        private void LimparCampos()
        {
            txtID.Clear();
            txtNome.Clear();
            txtTelefone.Clear();
            txtNome.Focus();

        }

        private void btnInserir_Click(object sender, EventArgs e)
        {

            objTabela.Nome = txtNome.Text;
            objTabela.Telefone = txtTelefone.Text;

            int x = UsuarioModelo.Inserir(objTabela);

            if(x > 0)
            {
                MessageBox.Show(string.Format("Usuário {0} foi inserido!", txtNome.Text));
            }
            else
            {
                MessageBox.Show(string.Format("Usuário não inserido!"));
            }

        }

        private void ListaGrid()
        {

            try
            {

                List<UsuarioEnt> lista = new List<UsuarioEnt>();
                lista = new UsuarioModelo().Lista();
                Grid.AutoGenerateColumns = false;
                Grid.DataSource = lista;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }

        }

        private void CadTelefone_Load(object sender, EventArgs e)
        {
            ListaGrid();
        }

        private void grid_cellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = Grid.CurrentRow.Cells[0].Value.ToString();                     //pega os valores das colunas (celulas/cell), converte pra string e mostra no campo txt
            txtNome.Text = Grid.CurrentRow.Cells[1].Value.ToString();
            txtTelefone.Text = Grid.CurrentRow.Cells[2].Value.ToString();
        }

        private void txtConsulta_TextChanged(object sender, EventArgs e)
        {
            if (txtConsulta.Text == "")
            {

                ListaGrid();
                return;

            }
            try
            {

                LimparCampos();
                txtConsulta.Focus();
                objTabela.Nome = txtConsulta.Text;
                List<UsuarioEnt> lista = new List<UsuarioEnt>();
                lista = new UsuarioModelo().Consulta(objTabela);

                Grid.AutoGenerateColumns = false;
                Grid.DataSource = lista;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro ao listar dados" + ex.Message);

            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            try
            {

                objTabela.Id = Convert.ToInt32(txtID.Text);
                objTabela.Nome = Convert.ToString(txtNome.Text);
                objTabela.Telefone = Convert.ToString(txtTelefone.Text);

                int x = UsuarioModelo.Alterar(objTabela);
                if (x > 0)
                {

                    MessageBox.Show(string.Format("Usuário {0} foi alterado!", txtNome.Text));

                }
                else
                {

                    MessageBox.Show("Não alterado!");

                }
                LimparCampos();
                ListaGrid();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o registro?", "Cuidado", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)   //DialogResult ja deixa selecionado a opção No
            {

                MessageBox.Show("Operação cancelada!");

            }

            else
            {

                try
                {

                    objTabela.Id = Convert.ToInt32(txtID.Text);
                    objTabela.Nome = Convert.ToString(txtNome.Text);
                    objTabela.Telefone = Convert.ToString(txtTelefone.Text);

                    int x = UsuarioModelo.Excluir(objTabela);
                    if (x > 0)
                    {

                        MessageBox.Show(string.Format("Usuário {0} foi excluido!", txtNome.Text));

                    }
                    else
                    {

                        MessageBox.Show("Não excluido!");

                    }
                    LimparCampos();
                    ListaGrid();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }

            }
        }
    }
}
