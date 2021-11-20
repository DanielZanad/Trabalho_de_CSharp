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
    public partial class CadastroUsuario : Form
    {
        // Criando uma variavel que representa o acesso desse formulario as outras telas
        Form1 fr1 = null;
        CadastroLivro cLivro = null;
        ListarUsuarios lUsuarios = null;

        // Criando usuario para que fique armazenado os dados do usuario
        Usuario usr = new Usuario();

        public CadastroUsuario()
        {
            InitializeComponent();
        }

        public void setFr1(Form1 fr1)
        {
            this.fr1 = fr1;
        }
        public void setUsuario(Usuario usr)
        {
            this.usr = usr;
        }

        private void CadastrarUsuario(object sender, EventArgs e)
        {
            if (txtSenha.Text.Equals(txtConfirmarSenha.Text))
            {
                // Criando "hash table" que vai armazenar o resultado do model
                IDictionary<string, string> resultado = new Dictionary<string, string>();
                // Instanciando o objeto model
                Model model = new Model();
                // Instanciando o objeto usuario
                Usuario usr = new Usuario();

                // Inserindo dados que vem do formulario e atribuindo ao objeto
                string dia  = Convert.ToString(dtpData.Value.Day);
                string mes = Convert.ToString(dtpData.Value.Month);
                string ano = Convert.ToString(dtpData.Value.Year);
                usr.setNome(txtNome.Text);
                usr.setEmail(txtEmail.Text);
                usr.setDataNasc($"{ano}-{mes}-{dia}");
                usr.setSenha(txtSenha.Text);
                
                // Chamando a funcao que vai salvar os dados no banco de dados
                resultado = model.Salvar(usr);

                // Verificando se deu tudo certo
                if(resultado["status"] == "200")
                {
                    MessageBox.Show("Cadastrado com sucesso");
                }
                else
                {
                    MessageBox.Show(resultado["msg"]);
                }


            }
            else
            {
                MessageBox.Show("As Senhas estão incorretas");
            }
            


        }

        private void Sair(object sender, EventArgs e)
        {
            // Mostra o formualario de login e fecha o de cadastro
            fr1.Show();
            this.Close();
        }

        private void CadastroUsuario_Load(object sender, EventArgs e)
        {
            lblNome.Text = "Logado com: " + usr.getNome();
        }

        private void ListarUsuarios(object sender, EventArgs e)
        {
            lUsuarios = new ListarUsuarios();
            lUsuarios.setCLivro(this);
            lUsuarios.Show();
        }
    }
}
