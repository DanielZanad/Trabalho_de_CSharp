using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace livrariafds
{
    public partial class CadastroLivro : Form
    {
        //Criando variaveis que represeanta o acesso desse formularios para outros
        CadastroUsuario cdUsuario = null;
        ListarLivros lLivros = null;

        Usuario usr = null;

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

                ModelProdutos model = new ModelProdutos();

                resultado = model.Salvar(prt);

                if (resultado["status"] == "200")
                {
                    MessageBox.Show("Cadastrado com sucesso");
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
    }
}
