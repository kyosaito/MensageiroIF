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

        public Usuario(string apelido)
        {
            ConferirApelido(apelido);
            this.Apelido = apelido.Trim();
        }

        public Usuario(string apelido, string nome)
        {
            ConferirApelido(apelido);
            this.Apelido = apelido.Trim();
            this.Nome = string.IsNullOrWhiteSpace(nome) ? Apelido : nome.Trim();
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

        public override string ToString()
        {
            return Apelido;
        }

        public static void ConferirApelido(string apelido)
        {
            if (string.IsNullOrWhiteSpace(apelido))
            {
                throw new ApelidoInvalidoException("O apelido não pode ficar em branco.");
            }
        }
    }
}
