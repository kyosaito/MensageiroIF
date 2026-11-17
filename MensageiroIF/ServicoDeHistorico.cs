using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class ServicoDeHistorico
    {
        public IRepositorioDeMensagens Repositorio { get; private set; }
        public Usuario Remetente { get; private set; }
        public Usuario Destinatario { get; private set; }

        public ServicoDeHistorico(IRepositorioDeMensagens repositorio, Usuario remetente, Usuario destinatario)
        {
            Repositorio = repositorio;
            Remetente = remetente;
            Destinatario = destinatario;
        }

        public int Resumo()
        {
            return Repositorio.Listar().Count;
        }

        //public void Registrar(string corpo)
        //{
        //    if (string.IsNullOrWhiteSpace(corpo))
        //        throw new MensagemInvalidaException("A mensagem está vazia.");
        //    if (corpo.Length > 250)
        //        throw new MensagemInvalidaException("A mensagem excede o limite de 250 caracteres.");

        //    Repositorio.Guardar(new MensagemTexto(Remetente.Apelido, Destinatario.Apelido, DateTime.Now, corpo));
        //}

        public MensagemTexto Registrar(string corpo)
        {
            if (string.IsNullOrWhiteSpace(corpo))
                throw new MensagemInvalidaException("A mensagem está vazia.");
            if (corpo.Length > 250)
                throw new MensagemInvalidaException("A mensagem excede o limite de 250 caracteres.");

            MensagemTexto mensagem = new MensagemTexto(Remetente.Apelido, Destinatario.Apelido, DateTime.Now, corpo);
            Repositorio.Guardar(mensagem);
            return mensagem;
        }

        public List<Mensagem> ObterHistorico()
        {
            //ObterHistorico foi alterado para retornar apenas mensagem de texto
            return Repositorio.Listar()
                .OfType<MensagemTexto>()
                .Where(EhDestaConversa)
                .OrderBy(m => m.Data)
                .Cast<Mensagem>()
                .ToList();
        }

        //Esse método é especifico para MensagemTexto
        public void Arquivar(MensagemTexto mensagem)
        {
            if (mensagem == null)
                throw new MensagemInvalidaException("A mensagem está nula.");

            if (!EhDestaConversa(mensagem))
                throw new MensagemInvalidaException("A mensagem não pertence a esta conversa.");

            string linha = mensagem.GetLinha();

            if (ObterHistorico().OfType<MensagemTexto>().Any(m => m.GetLinha() == linha))
                return;

            Repositorio.Guardar(mensagem);
        }

        public bool EhDestaConversa(MensagemTexto mensagem)
        {
            return (mensagem.Remetente == Remetente.Apelido && mensagem.Destinatario == Destinatario.Apelido)
                || (mensagem.Remetente == Destinatario.Apelido && mensagem.Destinatario == Remetente.Apelido);
        }
    }
}
