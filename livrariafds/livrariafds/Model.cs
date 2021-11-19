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

        public IDictionary<string,string> Salvar(Usuario user)
        {
            IDictionary<string, string> resultado = new Dictionary<string, string>();

            string sql = $"insert into usuario(nome,dataNasc,email,senha)" +
                $" values('{user.getNome()}', '{user.getDataNasc()}', '{user.getEmail()}', '{user.getSenha()}')";
           
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
           
        }



    }
}
