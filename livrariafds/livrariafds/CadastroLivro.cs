using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace livrariafds
{
    public partial class CadastroLivro : Form
    {
        //Criando variaveis que represeanta o acesso desse formularios para outros
        CadastroUsuario cdUsuario = null;
        ListarLivros lLivros = null;

        Usuario usr = null;

        // Instanciando o model
        ModelProdutos model = new ModelProdutos();

        public void setCdUsuario(CadastroUsuario cdUsuario)
        {
            this.cdUsuario = cdUsuario;
        }
        public void setUsuario(Usuario usr)
        {
            this.usr = usr;
        }

        public CadastroLivro()
        {
            InitializeComponent();
        }

        private void CadastroLivro_Load(object sender, EventArgs e)
        {
            lblNome.Text = "Logado com: " + usr.getNome();
            btnAtualizar.Enabled = false;

        }

        private void Cadastrar(object sender, EventArgs e)
        {
            IDictionary<string, string> resultado = new Dictionary<string, string>();

            Produto prt = new Produto();
            if (txtCodigo.Text.Equals(""))
            {
                MessageBox.Show("Algum Campo esta vazio");
            }
            else
            {
                prt.setCodigo(Convert.ToInt32(txtCodigo.Text));
                prt.setNome(Convert.ToString(txtNome.Text));
                prt.setGenero(Convert.ToString(txtGenero.Text));
                prt.setEditora(Convert.ToString(txtEditora.Text));
                prt.setAutor(Convert.ToString(txtAutor.Text));
                prt.setDImagem(Convert.ToString("imagens/" + txtCodigo.Text + ".png"));
                System.IO.File.Copy(ofdFotos.FileName, "imagens/" + txtCodigo.Text + ".png");

                resultado = model.Salvar(prt);

                if (resultado["status"] == "200")
                {
                    MessageBox.Show("Cadastrado com sucesso");
                    Limpar();
                }
                else
                {
                    MessageBox.Show(resultado["msg"]);
                }
            }
            
        }

        private void Voltar(object sender, EventArgs e)
        {
            cdUsuario.Show();
            this.Close();
        }

        private void ListarProdutos(object sender, EventArgs e)
        {
            lLivros = new ListarLivros();
            lLivros.setCLivro(this);
            lLivros.Show();
            this.Hide();
        }

        private void BuscarProduto(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Campo Codigo esta vazio");
            }
            else
            {
                int codigo = Convert.ToInt32(txtCodigo.Text);

                IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
                resultado = model.ListarPorCodigo(codigo);
                MySqlDataReader dr = resultado["resultado"]; 


                if (dr.Read())
                {
                    if (resultado["status"] == 200)
                    {
                        txtNome.Text = Convert.ToString(dr[1]);
                        txtGenero.Text = Convert.ToString(dr[2]);
                        txtEditora.Text = Convert.ToString(dr[3]);
                        txtAutor.Text = Convert.ToString(dr[4]);
                        model.FecharConexao();
                        btnAtualizar.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(resultado["msg"]);
                        model.FecharConexao();
                    }
                }
                else
                {
                    MessageBox.Show("Produto não encontrado!");
                    model.FecharConexao();
                }
                
            }
            
        }

        private void AtualizarProduto(object sender, EventArgs e)
        {
            Produto prt = new Produto();
            prt.setCodigo(Convert.ToInt32(txtCodigo.Text));
            prt.setNome(txtNome.Text);
            prt.setGenero(txtGenero.Text);
            prt.setEditora(txtEditora.Text);
            prt.setAutor(txtAutor.Text);

            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
            resultado = model.Atualizar(prt);

            if(resultado["status"] == 200)
            {

                MessageBox.Show(resultado["msg"]);
                btnAtualizar.Enabled = false;
                Limpar();
            }
            else
            {
                MessageBox.Show(resultado["msg"]);
            }
        }

        public void Limpar()
        {
            txtNome.Text = "";
            txtAutor.Text = "";
            txtCodigo.Text = "";
            txtEditora.Text = "";
            txtGenero.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofdFotos.ShowDialog();
        }

        private void CarregarFotos(object sender, CancelEventArgs e)
        {
            pctFotos.Image = Image.FromFile(ofdFotos.FileName);
        }
    }
}
