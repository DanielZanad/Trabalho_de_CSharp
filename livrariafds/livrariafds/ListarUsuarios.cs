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

        private void Atualizar()
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();

            // Criar um objeto "tabela"
            DataTable dt = new DataTable();
            // Criar as colunas
            dt.Columns.Add("Id");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data de Nascimento");
            dt.Columns.Add("Email");

            ModelUsuario model = new ModelUsuario();
            resultado = model.ListarTodos();

            MySqlDataReader lista = resultado["resultado"];

            if (lista.Read())
            {
                while (lista.Read())
                {
                    // Criando um objeto linha no formato da variavel dt
                    DataRow dr = dt.NewRow();
                    // Adicionando dados na linha
                    dr[0] = lista["id"];
                    dr[1] = lista["nome"];
                    dr[2] = lista["dataNasc"];
                    dr[3] = lista["email"];

                    dt.Rows.Add(dr);
                }
                dgvUsuarios.DataSource = dt;
                model.FecharConexao();
            }
            else
            {
                MessageBox.Show(resultado["msg"]);
            }
        } 

        private void ListarUsuarios_Load(object sender, EventArgs e)
        {
            Atualizar();
            
        }

        private void ExcluirUsuario(object sender, EventArgs e)
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();

            int id = Convert.ToInt32(txtId.Text);
            ModelUsuario model = new ModelUsuario();
            resultado = model.Excluir(id);
            MessageBox.Show(resultado["msg"]);

            Atualizar();

        }
    }
}
