using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace livrariafds
{
    class Model
    {
        // Criando objeto de conexao
        MySqlConnection conexao = new MySqlConnection(
            "server=localhost;uid=root;pwd='';database=livraria;SslMode=none;pooling = false; convert zero datetime=True");

        public IDictionary<string,string> Salvar(Usuario usr)
        {
            IDictionary<string, string> resultado = new Dictionary<string, string>();
            string senha = $"md5('{usr.getSenha()}')";
            string sql = $"INSERT INTO usuario(nome,dataNasc,email,senha)" +
                $" VALUES('{usr.getNome()}', '{usr.getDataNasc()}', '{usr.getEmail()}', {senha})";
           
            // Passando o coamndo e a conexao como parametro
            MySqlCommand comando = new MySqlCommand(sql,conexao);
            try
            {
                // teste
                conexao.Open();

                // Executando o comando
                comando.ExecuteNonQuery();

                resultado["status"] = "200";
                resultado["msg"] = "Cadastro realizado com sucesso";
                return resultado;


            }
            catch(Exception ex)
            {
                resultado["status"] = "400";
                resultado["msg"] = ex.ToString();
                return resultado;
            }
            finally
            {
                // Fechando conexao e limpando o objeto comando
                conexao.Close();
                comando.Dispose();
            }
           
        }

        public IDictionary<string, dynamic> Login(Usuario usr)
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();

            // Criando query
            string sql = $"SELECT * FROM usuario WHERE email = '{usr.getEmail()}' AND senha = md5('{usr.getSenha()}')";
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            // Abrindo conexao
            conexao.Open();

            // Executando o comando e o resultado sendo armazenando em um objeto do tipo MySqlDataReader
            MySqlDataReader dr = comando.ExecuteReader();

            // Verificando se ha registros no resultado
            if (dr.Read())
            {
                Usuario usrResultado = new Usuario();
                usrResultado.setNome(Convert.ToString(dr[1]));
                usrResultado.setEmail(Convert.ToString(dr[3]));

                resultado["status"] = 200;
                resultado["usuario"] = usrResultado;
                resultado["msg"] = "Login feito com sucesso";
                return resultado;
            }
            else
            {
                resultado["status"] = 400;
                resultado["msg"] = "Email ou senha incorreto";
                return resultado;
            }
        }



    }
}
