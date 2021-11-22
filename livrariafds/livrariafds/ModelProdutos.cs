using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace livrariafds
{
    class ModelProdutos
    {
        // Criando objeto de conexao
        MySqlConnection conexao = new MySqlConnection(
            "server=localhost;uid=root;pwd='';database=livraria;SslMode=none");

        public IDictionary<string, string> Salvar(Produto prt)
        {
            IDictionary<string, string> resultado = new Dictionary<string, string>();
            string sql = $"INSERT INTO produto(codigo,nome,genero,editora, autor)" +
                $" VALUES('{prt.getCodigo()}', '{prt.getNome()}', '{prt.getGenero()}', '{prt.getEditora()}', '{prt.getAutor()}')";

            // Passando o coamndo e a conexao como parametro
            MySqlCommand comando = new MySqlCommand(sql, conexao);
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
            catch (Exception ex)
            {
                resultado["status"] = "400";
                resultado["msg"] = Convert.ToString(ex);
                return resultado;
            }
            finally
            {
                // Fechando conexao e limpando o objeto comando
                conexao.Close();
                comando.Dispose();
            }

        }

        public IDictionary<string, dynamic> Listar()
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();

            // Criando query
            string sql = "SELECT * FROM produto";
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            try
            {
                conexao.Open();
                // Executando o comando e o resultado sendo armazenando em um objeto do tipo MySqlDataReader
                MySqlDataReader dr = comando.ExecuteReader();
                resultado["status"] = 200;
                resultado["resultado"] = dr;
                return resultado;


            }
            catch (Exception ex)
            {
                resultado["status"] = 400;
                resultado["msg"] = Convert.ToString(ex);
                return resultado;
            }
            finally
            {
                comando.Dispose();
            }

        }
        public IDictionary<string, dynamic> ListarPorCodigo(int codigo)
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();

            // Criando query
            string sql = $"SELECT * FROM produto WHERE codigo ='{codigo}'";
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            try
            {
                conexao.Open();
                // Executando o comando e o resultado sendo armazenando em um objeto do tipo MySqlDataReader
                MySqlDataReader dr = comando.ExecuteReader();
                resultado["status"] = 200;
                resultado["resultado"] = dr;
                return resultado;


            }
            catch (Exception ex)
            {
                resultado["status"] = 400;
                resultado["msg"] = Convert.ToString(ex);
                return resultado;
            }
            finally
            {
                comando.Dispose();
            }

        }

        public IDictionary<string, dynamic> Atualizar(Produto prt)
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();
            
            string sql = $"UPDATE produto SET" +
                $" codigo ='{prt.getCodigo()}', nome = '{prt.getNome()}', genero = '{prt.getGenero()}', editora = '{prt.getEditora()}', autor = '{prt.getAutor()}' " +
                $"WHERE codigo = {prt.getCodigo()};";

            // Passando o coamndo e a conexao como parametro
            MySqlCommand comando = new MySqlCommand(sql, conexao);
            try
            {
                conexao.Open();

                // Executando o comando
                comando.ExecuteNonQuery();

                resultado["status"] = 200;
                resultado["msg"] = "Produto atulizado com sucesso";
                return resultado;


            }
            catch (Exception ex)
            {
                resultado["status"] = 400;
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


        public IDictionary<string, dynamic> Excluir(int codigo)
        {
            IDictionary<string, dynamic> resultado = new Dictionary<string, dynamic>();

            // Criando query
            string sql = $"DELETE FROM produto WHERE codigo = {codigo}";
            MySqlCommand comando = new MySqlCommand(sql, conexao);

            try
            {
                // Abrindo conexao
                conexao.Open();
                if (comando.ExecuteNonQuery() > 0)
                {
                    resultado["status"] = 200;
                    resultado["msg"] = "Produto excluído com sucesso";
                    return resultado;
                }
                else
                {
                    resultado["status"] = 400;
                    resultado["msg"] = "produto não existe no banco de dados";
                    return resultado;
                }


            }catch(Exception ex)
            {
                resultado["status"] = 400;
                resultado["msg"] = Convert.ToString(ex);
                return resultado;
            }
            finally
            {
                conexao.Close();
                comando.Dispose();
            }

        }


        public void FecharConexao()
        {
            conexao.Close();
        }


    }
}
