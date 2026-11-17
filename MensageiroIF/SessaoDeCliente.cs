using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public enum ResultadoDeLogin
    {
        SemResposta,
        Aceito,
        Recusado
    }

    public class SessaoDeCliente
    {
        public ICanalDeComunicacao Canal { get; private set; }
        AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();

        public static Usuario UsuarioLogado { get; private set; }

        public string Apelido { get; private set; }
        public ErroDoServidor MotivoDaRecusa { get; private set; }

        public SessaoDeCliente(ICanalDeComunicacao canal, string apelido)
        {
            Apelido = apelido;
            Canal = canal;
        }

        public static SessaoDeCliente Criar(string diretorio, string apelido)
        {
            Usuario.ConferirApelido(apelido); //valida nulo ou vazio
            AnalisadorDeProtocolo.ConferirApelido(apelido); //valida separador
            CanalArquivo.ConferirApelidoDeUsuario(apelido); //valida termo servidor

            UsuarioLogado = new Usuario(apelido); //usuario é criado AQUI!
            ICanalDeComunicacao canal = new CanalArquivo(diretorio, UsuarioLogado.Apelido);
            SessaoDeCliente sessao = new SessaoDeCliente(canal, UsuarioLogado.Apelido);

            return sessao;
        }

        //Pedido para o SERVIDOR: LOGIN|USUARIO
        public void PedirLogin()
        {
            Canal.Receber();
            Canal.Enviar(CanalArquivo.SERVIDOR, AnalisadorDeProtocolo.LOGIN + AnalisadorDeProtocolo.SEPARADOR + Apelido);
        }

        public ResultadoDeLogin TentarEntrar()
        {
            RespostaDoServidor resposta = Receber();

            if (resposta.Erros.Count > 0)
            {
                MotivoDaRecusa = resposta.Erros[0];
                return ResultadoDeLogin.Recusado;
            }

            if (resposta.Confirmou(AnalisadorDeProtocolo.LOGIN))
                return ResultadoDeLogin.Aceito;

            return ResultadoDeLogin.SemResposta;
        }

        //É aqui que as linhas são interpretadas e viram objetos 
        public RespostaDoServidor Receber()
        {
            RespostaDoServidor resposta = new RespostaDoServidor();
            List<string> linhas = Canal.Receber();

            foreach(string linha in linhas)
            {
                string comando = analisador.Comando(linha);

                //ENVIAR|REMETENTE|DESTINATARIO|DATA|CORPO
                //HISTORICO|REMETENTE|DESTINATARIO|DATA|CORPO
                if (comando == AnalisadorDeProtocolo.ENVIAR || comando == AnalisadorDeProtocolo.HISTORICO)
                {
                    try
                    {
                        resposta.Mensagens.Add(analisador.Analisar(linha));
                    }
                    catch (ProtocoloInvalidoException)
                    {
                        resposta.Ignoradas.Add(linha);
                    }
                }
                //LISTA|USUARIO_1|USUARIO_2|...
                else if (comando == AnalisadorDeProtocolo.LISTA)
                {
                    resposta.Contatos = ExtrairContatos(linha);
                }
                //ERRO|CODIGO|DETALHE
                else if (comando == AnalisadorDeProtocolo.ERRO)
                {
                    string[] partes = linha.Split(AnalisadorDeProtocolo.SEPARADOR);

                    resposta.Erros.Add(new ErroDoServidor(
                        partes.Length > 1 ? partes[1] : "",
                        partes.Length > 2 ? partes[2] : ""
                        ));
                }
                //OK|COMANDO
                else if (comando == AnalisadorDeProtocolo.OK)
                {
                    string[] partes = linha.Split(AnalisadorDeProtocolo.SEPARADOR);

                    if (partes.Length > 1)
                        resposta.Confirmacoes.Add(partes[1]);
                }
                else
                {
                    resposta.Ignoradas.Add(linha);
                }
            }

            return resposta;
        }

        List<string> ExtrairContatos(string linha)
        {
            List<string> contatos = new List<string>();
            string[] partes = linha.Split(AnalisadorDeProtocolo.SEPARADOR);

            foreach(string parte in partes)
            {
                string apelido = parte.Trim();
                if (apelido != "" && apelido != Apelido && apelido != AnalisadorDeProtocolo.LISTA)
                    contatos.Add(apelido);
            }

            return contatos;
        }

        //Pedido para o SERVIDOR: LISTAR|USUARIO
        public void PedirLista()
        {
            Canal.Enviar(CanalArquivo.SERVIDOR, AnalisadorDeProtocolo.LISTAR + AnalisadorDeProtocolo.SEPARADOR + Apelido);
        }

        //Pedido para o SERVIDOR: HISTORICO|USUARIO|OUTRO_USUARIO
        public void PedirHistorico(string outro)
        {
            Canal.Enviar(CanalArquivo.SERVIDOR, AnalisadorDeProtocolo.HISTORICO + AnalisadorDeProtocolo.SEPARADOR +
                Apelido + AnalisadorDeProtocolo.SEPARADOR + outro);
        }

        //Pedido para o SERVIDOR: SAIR|USUARIO
        public void Sair()
        {
            Canal.Enviar(CanalArquivo.SERVIDOR, AnalisadorDeProtocolo.SAIR + AnalisadorDeProtocolo.SEPARADOR + Apelido);
        }

        //Envia mensagem para o SERVIDOR
        public MensagemTexto Enviar(string destino, string corpo)
        {
            AnalisadorDeProtocolo.ConferirApelido(destino);
            MensagemTexto.ConferirCorpo(corpo);

            MensagemTexto mensagem = new MensagemTexto(Apelido, destino, DateTime.Now, corpo);

            Canal.Enviar(CanalArquivo.SERVIDOR, mensagem.GetLinha());

            return mensagem;
        }
    }
}
