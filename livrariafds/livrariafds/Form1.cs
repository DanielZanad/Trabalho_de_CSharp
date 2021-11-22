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
    public partial class Form1 : Form
    {
        // Criando uma variavel que representa o acesso desse formulario as outras telas
        CadastroUsuario cdUsuario = null;
        TelaLivros tlivro = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Entrar(object sender, EventArgs e)
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
            ModelUsuario model = new ModelUsuario();
            Usuario usr = new Usuario();
            usr.setEmail(txtLogin.Text);
            usr.setSenha(txtSenha.Text);


            resultado = model.Login(usr);

            // Colocar o redirecionamento se o usuario nao for admin
            if (resultado["status"] == 200)
            {

                if (resultado["usuario"].getNome().Equals("admin"))
                {
                    cdUsuario = new CadastroUsuario();
                    cdUsuario.setFr1(this);
                    cdUsuario.setUsuario(resultado["usuario"]);
                    txtLogin.Text = "";
                    txtSenha.Text = "";
                    cdUsuario.Show();
                    this.Hide();
                }
                else
                {
                    tlivro = new TelaLivros();
                    tlivro.setFr1(this);
                    tlivro.setUsr(resultado["usuario"]);
                    tlivro.Show();
                    txtLogin.Text = "";
                    txtSenha.Text = "";
                    this.Hide();
                }
                
            }
            else
            {
                MessageBox.Show("Usuario ou Senha incorreta");
            }



            
        }
    }
}
