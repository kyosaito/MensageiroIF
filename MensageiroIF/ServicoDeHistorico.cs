using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class ServicoDeHistorico
    {
        IRepositorioDeMensagens repositorio;
        Usuario remetente, destinatario;

        public ServicoDeHistorico(IRepositorioDeMensagens repositorio, Usuario remetente, Usuario destinatario)
        {
            this.repositorio = repositorio;
            this.remetente = remetente;
            this.destinatario = destinatario;
        }

        public int Resumo()
        {
            return repositorio.Listar().Count;
        }

        public void Registrar(string corpo)
        {
            if (string.IsNullOrWhiteSpace(corpo))
                throw new MensagemInvalidaException("A mensagem está vazia.");
            if (corpo.Length > 250)
                throw new MensagemInvalidaException("A mensagem excede o limite de 250 caracteres.");

            repositorio.Guardar(new MensagemTexto(remetente.Apelido, destinatario.Apelido, DateTime.Now, corpo));
        }

        public List<Mensagem> ObterHistorico()
        {
            return repositorio.Listar();
        }
    }
}
