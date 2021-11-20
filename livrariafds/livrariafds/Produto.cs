using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace livrariafds
{
    public class Produto
    {
        private int codigo;
        private string nome;
        private string genero;
        private string editora;
        private string autor;

        public int getCodigo()
        {
            return this.codigo;
        }
        public void setCodigo(int codigo)
        {
            this.codigo = codigo;
        }

        public string getNome()
        {
            return this.nome;
        }
        public void setNome(string nome )
        {
            this.nome = nome;
        }

        public string getGenero()
        {
            return this.genero;
        }
        public void setGenero(string genero)
        {
            this.genero = genero;
        }

        public string getEditora()
        {
            return this.editora;
        }
        public void setEditora(string editora)
        {
            this.editora = editora;
        }

        public string getAutor()
        {
            return this.autor;
        }
        public void setAutor(string autor)
        {
            this.autor = autor;
        }
    }
}
