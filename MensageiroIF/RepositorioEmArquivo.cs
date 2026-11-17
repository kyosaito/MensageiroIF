using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class RepositorioEmArquivo : IRepositorioDeMensagens
    {
        //private List<Mensagem> mensagens = new List<Mensagem>();

        private const string CAMINHO = "historico.txt";
        Stream arquivo = null;
        StreamWriter escritor = null;
        StreamReader leitor = null;

        AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();

        public void Guardar(Mensagem mensagem)
        {
            using (arquivo = File.Open(CAMINHO, FileMode.Append))
            using (escritor = new StreamWriter(arquivo))
            {
                escritor.WriteLine(mensagem.GetLinha());
            }

        }
        public List<Mensagem> Listar()
        {
            List<Mensagem> mensagens = new List<Mensagem>();

            if (File.Exists(CAMINHO))
            {
                using (arquivo = File.Open(CAMINHO, FileMode.Open))
                using (leitor = new StreamReader(arquivo))
                {
                    string linha = leitor.ReadLine();
                    while (linha != null)
                    {
                        if (linha.Trim() != "")
                        {
                            var mensagem = analisador.Analisar(linha);
                            mensagens.Add(mensagem);
                        }

                        linha = leitor.ReadLine();
                    }
                }
            }

            return mensagens;

            //Toda vez que Listar() for chamado ele vai ler o arquivo inteiro. Em um arquivo com mil linhas, ele vai travar.
            //O que pode ser feito aqui é criar um método que carrega o arquivo uma vez e armazena em memória. Assim, o Listar()
            //só lista as mensagens que estão em memória. Outra forma é fazer paginação, ou seja, o Listar() recebe um parâmetro
            //de quantas mensagens ele quer listar e ele retorna apenas essas mensagens.
        }

        public List<Mensagem> Listar(Func<Mensagem, bool> criterio)
        {
            List<Mensagem> mensagens = new List<Mensagem>();

            if (File.Exists(CAMINHO))
            {
                using (arquivo = File.Open(CAMINHO, FileMode.Open))
                using (leitor = new StreamReader(arquivo))
                {
                    string linha = leitor.ReadLine();
                    while (linha != null)
                    {
                        if (linha.Trim() != "")
                        {
                            var mensagem = analisador.Analisar(linha);
                            if (criterio(mensagem))
                                mensagens.Add(mensagem);
                        }

                        linha = leitor.ReadLine();
                    }
                }
            }

            return mensagens;
        }

        //public void Apagar()
        //{
        //    if (File.Exists(CAMINHO))
        //    {
        //        File.Delete(CAMINHO);
        //    }
        //    mensagens.Clear();
        //}
    }
}
