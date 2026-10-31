using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class Usuario
    {
        public string Apelido { get; protected set; }
        public string Nome { get; protected set; }
        List<Usuario> contatos = new List<Usuario>();

        public Usuario(string apelido, string nome)
        {
            this.Apelido = apelido;
            this.Nome = nome;
        }

        public override bool Equals(object obj)
        {
            if (obj is Usuario outro)
            {
                return Apelido.Equals(outro.Apelido);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Apelido.GetHashCode();
        }
    }
}
