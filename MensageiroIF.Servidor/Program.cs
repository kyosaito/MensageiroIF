using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MensageiroIF.Terminal
{
    internal class Program
    {
        public const string DIRETORIO = @"C:\MensageiroIF";
        const int CICLO_MS = 1000; //de quanto em quanto tempo o servidor vai ler seu arquivo do canal
                
        static void Main(string[] args)
        {
            Console.Title = "Mensageiro IF - Servidor";
            Console.WriteLine("====== Mensageiro IF - Servidor ======");
            Console.WriteLine($"Diretório compartilhado: {DIRETORIO}");

            if (!Directory.Exists(DIRETORIO))
            {
                Console.WriteLine();
                Console.WriteLine("O diretório não existe ou não está acessível.");
                Console.WriteLine("Crie o diretório, ou ajuste a constante DIRETORIO no Program.cs.");
                Console.WriteLine();
                Console.Write("Pressione ENTER para sair. ");
                Console.ReadLine();
                return;
            }

            string historico = Path.Combine(DIRETORIO, "historico.txt");

            ICanalDeComunicacao canal = new CanalArquivo(DIRETORIO, CanalArquivo.SERVIDOR);
            IRepositorioDeMensagens repositorio = new RepositorioEmArquivo();

            Servidor servidor = new Servidor(canal, repositorio);

            Console.WriteLine($"Histórico: {historico}");
            Console.WriteLine($"Ciclo: {CICLO_MS} ms. CTRL+C encerra.");
            Console.WriteLine();

            while (true)
            {
                try
                {
                    servidor.Processar();
                }
                catch (Exception erro)
                {
                    Console.WriteLine($"[ciclo] {erro.GetType().Name}: {erro.Message}");
                }

                Thread.Sleep(CICLO_MS);
            }
        }
    }
}
