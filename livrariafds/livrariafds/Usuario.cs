using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livrariafds
{
    class Usuario
    {
        private int id;
        private string nome;
        private string dataNasc;
        private string email; 
        private string senha;
        


        public void setId(int id)
        {
            this.id = id;
        }
        public int getId()
        {
            return this.id;
        }

        public void setNome(string nome)
        {
            this.nome = nome;
        }
        public string getNome()
        {
            return this.nome;
        }

        public void setDataNasc(string dataNasc)
        {
            this.dataNasc = dataNasc;
        }
        public string getDataNasc()
        {
            return this.dataNasc;
        }

        public void setEmail(string email)
        {
            this.email = email;
        }
        public string getEmail()
        {
            return this.email;
        }

        public void setSenha(string senha)
        {
            this.senha = senha;
        }
        public string getSenha()
        {
            return this.senha;
        }

        



    }
}
