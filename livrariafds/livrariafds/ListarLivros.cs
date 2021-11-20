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
    public partial class ListarLivros : Form
    {
        // Criando variaveis que representam o acesso desse formulario aos outros
        CadastroLivro cLivro = null;

        public void setCLivro(CadastroLivro cLivro)
        {
            this.cLivro = cLivro;
        }
        

        public ListarLivros()
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
            DataTable dataTable = new DataTable();
            // Criar as colunas
            dataTable.Columns.Add("Codigo");
            dataTable.Columns.Add("Nome");
            dataTable.Columns.Add("Genero");
            dataTable.Columns.Add("Editora");
            dataTable.Columns.Add("Autor");

            ModelProdutos model = new ModelProdutos();
            resultado = model.Listar();
            

            MySqlDataReader lista = resultado["resultado"];
            if (resultado["status"] == 200)
            {
                while (lista.Read())
                {
                    // Criando um objeto linha no formato da variavel dt
                    DataRow dr = dataTable.NewRow();
                    // Adicionando dados na linha
                    dr[0] = lista["codigo"];
                    dr[1] = lista["nome"];
                    dr[2] = lista["genero"];
                    dr[3] = lista["editora"];
                    dr[4] = lista["autor"];

                    dataTable.Rows.Add(dr);
                }

                dgvProdutos.DataSource = dataTable;
                model.FecharConexao();
            }
            else
            {
                MessageBox.Show(resultado["msg"]);
            }

        }


        private void ListarLivros_Load(object sender, EventArgs e)
        {
            Atualizar();
        }
    }
}
