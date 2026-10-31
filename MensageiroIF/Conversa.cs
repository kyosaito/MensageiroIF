using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    internal class Conversa
    {
        List<Usuario> participantes;
        List<Mensagem> historico = new List<Mensagem>();

        public Conversa(List<Usuario> participantes)
        {
            this.participantes = participantes;
        }
    }
}
