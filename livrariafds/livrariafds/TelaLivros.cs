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
    public partial class TelaLivros : Form
    {
        // Criando variaveis que vao representar o acesso desse formularios ao outros
        Form1 fr1 = new Form1();

        Usuario usr = new Usuario();

        public void setFr1(Form1 fr1)
        {
            this.fr1 = fr1;
        }
        public void setUsr(Usuario usr)
        {
            this.usr = usr;
        }


        public TelaLivros()
        {
            InitializeComponent();
        }

        private void Sair(object sender, EventArgs e)
        {
            // Mostra o formualario de login e fecha o de cadastro
            fr1.Show();
            this.Close();
        }

        private void TelaLivros_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Logado com: " + usr.getNome();

        }
    }
}
