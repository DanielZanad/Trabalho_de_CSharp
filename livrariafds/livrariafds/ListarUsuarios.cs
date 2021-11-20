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

    public partial class ListarUsuarios : Form
    {
        CadastroUsuario cLivro = null;

        public void setCLivro(CadastroUsuario cLivro)
        {
            this.cLivro = cLivro;
        }

        public ListarUsuarios()
        {
            InitializeComponent();
        }

        private void Voltar(object sender, EventArgs e)
        {
            cLivro.Show();
            this.Close();
        }
    }
}
