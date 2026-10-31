using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    internal class RepositorioFalso : IRepositorioDeMensagens
    {
        int quantidade = 0;
        List<Mensagem> mensagens = new List<Mensagem>();

        public RepositorioFalso()
        {
            mensagens.Add(new MensagemTexto("João", "Maria", DateTime.Now, "Olá!"));
            mensagens.Add(new MensagemSistema(DateTime.Now, "Oi!", 1));
        }

        public void Guardar(Mensagem mensagem)
        {
            quantidade++;
        }

        public List<Mensagem> Listar()
        {
            return mensagens;
        }
    }
}
