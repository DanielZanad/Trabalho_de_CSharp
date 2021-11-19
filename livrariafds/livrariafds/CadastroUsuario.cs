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
        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private void CadastrarUsuario(object sender, EventArgs e)
        {
            if (txtSenha.Text.Equals(txtConfirmarSenha.Text))
            {
                IDictionary<string, string> resultado = new Dictionary<string, string>();
                Model model = new Model();
                Usuario usr = new Usuario();
                string dia  = Convert.ToString(dtpData.Value.Day);
                string mes = Convert.ToString(dtpData.Value.Month);
                string ano = Convert.ToString(dtpData.Value.Year);
                usr.setNome(txtNome.Text);
                usr.setEmail(txtEmail.Text);
                usr.setDataNasc($"{ano}-{mes}-{dia}");

                usr.setSenha(txtSenha.Text);
                resultado = model.Salvar(usr);
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
    }
}
