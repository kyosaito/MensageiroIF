using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace MensageiroIF
{
    public class CanalArquivo : ICanalDeComunicacao
    {
        public const string SERVIDOR = "servidor";
        readonly string caminho, caixaDoUsuario;

        const int TENTATIVAS = 10;
        const int ESPERA_MS = 50;


        string CaixaDe(string nome)
        {
            return Path.Combine(caminho, $"caixa_{nome}.txt");
        }

        public CanalArquivo(string caminho, string apelidoDoUsuario)
        {
            this.caminho = caminho;
            caixaDoUsuario = CaixaDe(apelidoDoUsuario.Trim().ToLower());
        }
        public void Enviar(string destino, string linha)
        {
            //escreve a linha no arquivo do canal
            var caixaDoDestino = CaixaDe(destino.Trim().ToLower());

            Stream arquivo = AbrirComEspera(caixaDoDestino, FileMode.Append, FileAccess.Write);

            if (arquivo == null)
            {
                throw new IOException($"Não foi possível abrir o arquivo {caixaDoDestino} para escrita.");
            }

            using (arquivo)
            using (StreamWriter escritor = new StreamWriter(arquivo))
            {
                escritor.WriteLine(linha);
            }
        }

        public List<string> Receber()
        {
            List<string> linhas = new List<string>();

            Stream arquivo;

            //lê a linha nova do arquivo do canal (caixa de entrada)
            if (File.Exists(caixaDoUsuario))
            {
                arquivo = AbrirComEspera(caixaDoUsuario, FileMode.Open, FileAccess.ReadWrite);
                //o acesso é ReadWrite, pois para esvaziar é necessário ter premissão para escrever

                if (arquivo != null)
                {
                    using (arquivo)
                    using (StreamReader leitor = new StreamReader(arquivo))
                    {
                        string linha = leitor.ReadLine();

                        while (linha != null)
                        {
                            if (linha.Trim() != "")
                            {
                                linhas.Add(linha.Trim());
                            }

                            linha = leitor.ReadLine();
                        }

                        //File.Delete(caixaDoUsuario);
                        //aqui estava lançando exceção, o programa não conseguia deletar, pois para deletar
                        //ele precisa abrir o arquivo. Como o arquivo estava em uso (dentro do using), não
                        //conseguia abrir para deletar
                        arquivo.SetLength(0); //aqui estamos apenas esvaziando o arquivo
                    }
                }
            }

            return linhas;
        }

        public static void ConferirApelidoDeUsuario(string apelido)
        {
            AnalisadorDeProtocolo.ConferirApelido(apelido);

            if (apelido.Trim().ToLower() == SERVIDOR)
            {
                throw new ApelidoInvalidoException($"O apelido {SERVIDOR} é reservado. Escolha outro apelido.");
            }
        }


        Stream AbrirComEspera(string caminho, FileMode modo, FileAccess acesso)
        {
            for (int tentativa = 1; tentativa <= TENTATIVAS; tentativa++)
            {
                try
                {
                    return File.Open(caminho, modo, acesso, FileShare.None);
                }
                catch (IOException)
                {
                    Thread.Sleep(ESPERA_MS);
                }
            }
            return null;    
        }
    }
}
