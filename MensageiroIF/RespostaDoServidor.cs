using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class RespostaDoServidor
    {
        public List<MensagemTexto> Mensagens { get; private set; }
        public List<ErroDoServidor> Erros { get; private set; }
        public List<string> Confirmacoes { get; private set; }
        public List<string> Contatos { get; set; }
        public List<string> Ignoradas { get; private set; }

        public RespostaDoServidor()
        {
            Mensagens = new List<MensagemTexto>();
            Erros = new List<ErroDoServidor>();
            Confirmacoes = new List<string>();
            Contatos = null;
            Ignoradas = new List<string>();
        }

        public bool Confirmou(string comando)
        {
            return Confirmacoes.Contains(comando);
        }

        public bool TemAlgumaCoisa {
            get { return Mensagens.Count > 0 || Erros.Count > 0 || Confirmacoes.Count > 0 || Contatos != null || Ignoradas.Count > 0; }
        }
    }
}
