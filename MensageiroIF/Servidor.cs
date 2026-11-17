using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class Servidor
    {
        //tempo sem dar sinal de vida até o usuário sair da lista
        public static readonly TimeSpan SILENCIO_MAXIMO = TimeSpan.FromSeconds(60);

        readonly ICanalDeComunicacao canal;
        readonly IRepositorioDeMensagens repositorio;
        readonly AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();
        readonly RegistradorDeLog logs = new RegistradorDeLog();

        //Dicionario<apelido , ultima mensagem recebida>
        readonly Dictionary<string, DateTime> online = new Dictionary<string, DateTime>();
        
        public Servidor(ICanalDeComunicacao canal, IRepositorioDeMensagens repositorio)
        {
            this.canal = canal;
            this.repositorio = repositorio;
        }

        public void Processar()
        {
            List<string> linhas = canal.Receber();

            foreach(string linha in linhas)
            {
                try
                {
                    Tratar(linha);
                }
                catch(Exception erro)
                {
                    logs.Falha($"Falha ao tratar \"{linha}\": {erro.Message}");
                }
            }

            EsquecerQuemSumiu();
        }

        void Tratar(string linha)
        {
            string comando = analisador.Comando(linha);
            string[] partes = linha.Split(AnalisadorDeProtocolo.SEPARADOR);

            if (comando == AnalisadorDeProtocolo.LOGIN)
                TratarLogin(partes);
            else if (comando == AnalisadorDeProtocolo.LISTAR)
                TratarListar(partes);
            else if (comando == AnalisadorDeProtocolo.HISTORICO)
                TratarHistorico(partes);
            else if (comando == AnalisadorDeProtocolo.ENVIAR)
                TratarEnviar(linha);
            else if (comando == AnalisadorDeProtocolo.SAIR)
                TratarSair(partes);
            else
                logs.Falha($"Comando desconhecido: \"{linha}\"");
        }

        void TratarLogin(string[] partes)
        {
            if (partes.Length < 2)
                return;

            string apelido = partes[1].Trim().ToLower();

            try
            {
                CanalArquivo.ConferirApelidoDeUsuario(apelido);
            }
            catch (ApelidoInvalidoException)
            {
                // Sem caixa valida nao da nem para responder a quem pediu.
                logs.Falha($"Login recusado: apelido invalido \"{apelido}\".");
                return;
            }

            if (online.ContainsKey(apelido))
            {
                Responder(apelido, analisador.MontarErro(
                    AnalisadorDeProtocolo.APELIDO_EM_USO, apelido));
                logs.Falha($"Login recusado: \"{apelido}\" ja esta on-line.");
                return;
            }

            online[apelido] = DateTime.Now;
            Responder(apelido, analisador.MontarOK(AnalisadorDeProtocolo.LOGIN));
            logs.Registrar($"{apelido} entrou. On-line agora: {online.Count}.");

            // Quem acabou de entrar aparece para os outros, e os outros para ele.
            AvisarTodos();
        }

        void EsquecerQuemSumiu()
        {
            List<string> sumidos = new List<string>();

            foreach (KeyValuePair<string, DateTime> par in online)
            {
                if (DateTime.Now - par.Value > SILENCIO_MAXIMO)
                    sumidos.Add(par.Key);
            }

            foreach (string apelido in sumidos)
            {
                online.Remove(apelido);
                logs.Registrar($"{apelido} sumiu sem avisar e saiu da lista.");
            }

            if (sumidos.Count > 0)
                AvisarTodos();
        }

        void AvisarTodos()
        {
            string lista = MontarLista();

            foreach (string apelido in new List<string>(online.Keys))
                Responder(apelido, lista);
        }

        string MontarLista()
        {
            //StringBuilder é uma string mutável (consegue aumentar ou diminuir uma string dinamicamente
            //é util quando de tem laços de repetição grandes ou muitas concatenações
            StringBuilder linha = new StringBuilder(AnalisadorDeProtocolo.LISTA);

            foreach (string apelido in online.Keys)
                linha.Append(AnalisadorDeProtocolo.SEPARADOR).Append(apelido);

            return linha.ToString();
        }

        void Responder(string destino, string linha)
        {
            try
            {
                canal.Enviar(destino, linha);
            }
            catch (Exception erro)
            {
                logs.Falha($"Nao foi possivel responder a {destino}: {erro.Message}");
            }
        }


        void TratarListar(string[] partes)
        {
            if (partes.Length < 2)
                return;

            string apelido = partes[1].Trim().ToLower();

            if (!Autenticado(apelido))
                return;

            Responder(apelido, MontarLista());
        }

        bool Autenticado(string apelido)
        {
            if (!online.ContainsKey(apelido))
                return false;

            online[apelido] = DateTime.Now;
            return true;
        }


        void TratarHistorico(string[] partes)
        {
            if (partes.Length < 3)
                return;

            string quemPediu = partes[1].Trim().ToLower();
            string outro = partes[2].Trim().ToLower();

            if (!Autenticado(quemPediu))
                return;

            foreach (Mensagem mensagem in repositorio.Listar())
            {
                MensagemTexto texto = mensagem as MensagemTexto;

                if (texto == null)
                    continue;   // mensagem de sistema nao entra na conversa de ninguem

                if (EhDaConversa(texto, quemPediu, outro))
                {
                    // A linha gravada ja esta no formato de ENVIAR. Para devolver como
                    // historico, troca-se so a primeira palavra - os campos sao os mesmos.
                    Responder(quemPediu, analisador.ComComando(texto.GetLinha(),
                                                               AnalisadorDeProtocolo.HISTORICO));
                }
            }
        }

        bool EhDaConversa(MensagemTexto texto, string um, string outro)
        {
            string remetente = texto.Remetente.ToLower();
            string destinatario = texto.Destinatario.ToLower();

            return (remetente == um && destinatario == outro)
                || (remetente == outro && destinatario == um);
        }


        void TratarEnviar(string linha)
        {
            MensagemTexto mensagem;

            try
            {
                mensagem = analisador.Analisar(linha);
            }
            catch (ProtocoloInvalidoException erro)
            {
                logs.Falha($"Linha recusada: {erro.Message}");
                return;   // sem remetente confiavel, nao ha a quem responder
            }

            string remetente = mensagem.Remetente.Trim().ToLower();
            string destinatario = mensagem.Destinatario.Trim().ToLower();

            if (!Autenticado(remetente))
            {
                Responder(remetente, analisador.MontarErro(
                    AnalisadorDeProtocolo.NAO_AUTENTICADO, remetente));
                return;
            }

            if (!online.ContainsKey(destinatario))
            {
                Responder(remetente, analisador.MontarErro(
                    AnalisadorDeProtocolo.USUARIO_DESCONHECIDO, destinatario));
                return;
            }

            try
            {
                MensagemTexto.ConferirCorpo(mensagem.Corpo);
            }
            catch (MensagemInvalidaException erro)
            {
                Responder(remetente, analisador.MontarErro(
                    AnalisadorDeProtocolo.PROTOCOLO_INVALIDO, erro.Message));
                return;
            }

            // Grava primeiro, entrega depois: se a entrega falhar, a mensagem ainda
            // existe no historico e o destinatario a ve quando pedir a conversa.
            repositorio.Guardar(mensagem);

            Responder(destinatario, mensagem.GetLinha());
            Responder(remetente, analisador.MontarOK(AnalisadorDeProtocolo.ENVIAR));

            logs.Registrar($"{remetente} -> {destinatario}: {mensagem.Corpo}");
        }


        void TratarSair(string[] partes)
        {
            if (partes.Length < 2)
                return;

            string apelido = partes[1].Trim().ToLower();

            if (online.Remove(apelido))
            {
                logs.Registrar($"{apelido} saiu. On-line agora: {online.Count}.");
                AvisarTodos();
            }
        }

        //de tempos em tempos, lê a linha nova do aqruivo do canal, chama AnalisadorDeProtocolo (que transforma
        //a linha em uma MensagemTexto) e ServicoDeHistorico registra a MensagemTexto no repositório de mensagens
        //(historico.txt)

    }
}
