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
    public partial class TelaLivros : Form
    {
        // Criando variaveis que vao representar o acesso desse formularios ao outros
        Form1 fr1 = new Form1();

        Usuario usr = new Usuario();
        ModelProdutos model = new ModelProdutos();

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

            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
            resultado = model.Listar();
            int c = 0;
            MySqlDataReader lista = resultado["resultado"];
            if(resultado["status"] == 200)
            {
                while (lista.Read())
                {
                    switch (c)
                    {
                        case 0:
                            lblLivro1.Text = Convert.ToString(lista[1]);
                            pctLivro1.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;
                        case 1:
                            lblLivro2.Text = Convert.ToString(lista[1]);
                            pctLivro2.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;
                        case 2:
                            lblLivro3.Text = Convert.ToString(lista[1]);
                            pctLivro3.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;
                        case 3:
                            lblLivro4.Text = Convert.ToString(lista[1]);
                            pctLivro4.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;
                        case 4:
                            lblLivro5.Text = Convert.ToString(lista[1]);
                            pctLivro5.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;
                        case 5:
                            lblLivro6.Text = Convert.ToString(lista[1]);
                            pctLivro6.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;
                        case 6:
                            lblLivro7.Text = Convert.ToString(lista[1]);
                            pctLivro7.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;
                        case 7:
                            lblLivro8.Text = Convert.ToString(lista[1]);
                            pctLivro8.Image = Image.FromFile(Convert.ToString(lista[5]));
                            c = c + 1;
                            break;

                    }



                }
            }


        }
    }
}
