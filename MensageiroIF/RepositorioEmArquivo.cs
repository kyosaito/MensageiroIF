using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    internal class RepositorioEmArquivo : IRepositorioDeMensagens
    {
        private List<Mensagem> mensagens = new List<Mensagem>();

        private const string CAMINHO = "historico.txt";
        Stream arquivo = null;
        StreamWriter escritor = null;
        StreamReader leitor = null;

        AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();

        public void Guardar(Mensagem mensagem)
        {
            using (arquivo = File.Open("historico.txt", FileMode.Append))
            using (escritor = new StreamWriter(arquivo))
            {
                escritor.WriteLine(mensagem.GetLinha());
            }

        }
        public List<Mensagem> Listar()
        {
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
        }
    }
}
