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
    public partial class CadastroUsuario : Form
    {
        // Criando uma variavel que representa o acesso desse formulario as outras telas
        Form1 fr1 = null;
        CadastroLivro cLivro = null;
        ListarUsuarios lUsuarios = null;

        // Criando usuario para que fique armazenado os dados do usuario
        Usuario usr = new Usuario();

        // Instanciando o objeto model
        ModelUsuario model = new ModelUsuario();

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
                IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
                
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
                if(resultado["status"] == 200)
                {
                    MessageBox.Show("Cadastrado com sucesso");
                    Limpar();
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



        private void CadastroUsuario_Load(object sender, EventArgs e)
        {
            lblNome.Text = "Logado com: " + usr.getNome();
            txtId.ReadOnly = true;
            btnAtualizar.Enabled = false;
            btnBuscar.Enabled = false;

        }


        private void Editar(object sender, EventArgs e)
        {
            txtId.ReadOnly = false;
            btnBuscar.Enabled = true;
        }

        private void BuscarUsuario(object sender, EventArgs e)
        {
            if(txtId.Text == "")
            {
                MessageBox.Show("Campo ID esta vazio");
            }
            else
            {
                IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
                resultado = model.ListarPorId(Convert.ToInt32(txtId.Text));
                MySqlDataReader dr = resultado["resultado"];
                if (dr.Read())
                {
                    if (resultado["status"] == 200)
                    {
                        
                        string[] data2 = Convert.ToString(dr[2]).Split('/');
                        string anoFormat = data2[2];
                        int dia = Convert.ToInt32(data2[0]);
                        int mes = Convert.ToInt32(data2[1]);
                        int ano = Convert.ToInt32(anoFormat.Substring(0,4));

                        txtNome.Text = Convert.ToString(dr[1]);
                        dtpData.Value = new DateTime(ano, mes, dia);
                        txtEmail.Text = Convert.ToString(dr[3]);
                        txtSenha.Text = Convert.ToString(dr[4]);
                        btnAtualizar.Enabled = true;
                        model.FecharConexao();

                    }
                    else
                    {
                        MessageBox.Show(resultado["msg"]);
                        model.FecharConexao();
                    }

                }
                else
                {
                    MessageBox.Show("Usuário não encontrado!");
                    model.FecharConexao();
                }
            }
            

        }

        private void AtualizarUsuario(object sender, EventArgs e)
        {
            // Instanciando o objeto usuario
            Usuario usr = new Usuario();

            // Inserindo dados que vem do formulario e atribuindo ao objeto
            string dia = Convert.ToString(dtpData.Value.Day);
            string mes = Convert.ToString(dtpData.Value.Month);
            string ano = Convert.ToString(dtpData.Value.Year);
            usr.setId(Convert.ToInt32(txtId.Text));
            usr.setNome(txtNome.Text);
            usr.setEmail(txtEmail.Text);
            usr.setDataNasc($"{ano}-{mes}-{dia}");
            usr.setSenha(txtSenha.Text);


            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
            resultado = model.Atualizar(usr);
            if (resultado["status"] == 200)
            {
                MessageBox.Show(resultado["msg"]);
                btnAtualizar.Enabled = false;
                btnBuscar.Enabled = false;
                Limpar();
                txtId.Text = "";
            }
            else
            {
                MessageBox.Show(resultado["msg"]);
            }
        }

        private void ListarUsuarios(object sender, EventArgs e)
        {
            lUsuarios = new ListarUsuarios();
            lUsuarios.setCLivro(this);
            lUsuarios.Show();
            this.Hide();
        }

        private void CadastrarLivro(object sender, EventArgs e)
        {
            cLivro = new CadastroLivro();
            cLivro.setCdUsuario(this);
            cLivro.setUsuario(usr);
            cLivro.Show();
            this.Hide();
        }

        private void Sair(object sender, EventArgs e)
        {
            // Mostra o formualario de login e fecha o de cadastro
            fr1.Show();
            this.Close();
        }
        public void Limpar()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            txtConfirmarSenha.Text = "";
        }
    }
}
