using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class RepositorioEmMemoria : IRepositorioDeMensagens
    {
        private List<Mensagem> mensagens = new List<Mensagem>();
        public void Guardar(Mensagem mensagem)
        {
            mensagens.Add(mensagem);
        }
        public List<Mensagem> Listar()
        {
            return mensagens;
        }
    }
}
