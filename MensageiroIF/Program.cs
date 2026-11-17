using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====== AULA 04 ======");

            List<Mensagem> historico = new List<Mensagem>();
            //historico.Add(new Mensagem("Main", DateTime.Now, "Mensagem principal"));
            historico.Add(new MensagemTexto("Flavio", "Maria", DateTime.Now, "Mensagem de texto"));
            historico.Add(new MensagemSistema(DateTime.Now, "Mensagem do sistema", 5));

            foreach (Mensagem m in historico)
            {
                m.Exibir();
                if (m is MensagemSistema ms)
                {
                    Console.WriteLine($"[SISTEMA] Prioridade: {ms.Prioridade}");
                }
            }

            MensagemTexto CriarMensagem(string linha)
            {
                string[] partes = linha.Split('|');
                if (partes.Length < 3)
                {
                    throw new ProtocoloInvalidoException("Formato de mensagem inválido. Formato esperado: REMETENTE|DESTINATARIO|CORPO");
                }
                return new MensagemTexto(partes[0], partes[1], DateTime.Now, partes[1]);
            }

            try
            {
                MensagemTexto mensagem = CriarMensagem("Flavio|Olá! Tudo bem?");
                mensagem.Exibir();
                MensagemTexto mensagemInvalida = CriarMensagem("Flavio");
                mensagemInvalida.Exibir();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }


            Console.WriteLine("====== AULA 05 ======");

            RepositorioEmMemoria repositorio = new RepositorioEmMemoria();
            try
            {
                repositorio.Guardar(CriarMensagem("Flavio|Olá! Tudo bem?"));
                repositorio.Guardar(CriarMensagem("Maria|Oi! Tudo ótimo!"));
                repositorio.Guardar(CriarMensagem("João|E aí, pessoal?"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            Usuario remetente = new Usuario("Flavio", "Flávio Saito");
            Usuario destinatario = new Usuario("Maria", "Maria Silva");

            ServicoDeHistorico servicoMemoria = new ServicoDeHistorico(repositorio, remetente, destinatario);
            Console.WriteLine($"Resumo do Serviço Memória: {servicoMemoria.Resumo()}");

            ServicoDeHistorico servicoFalso = new ServicoDeHistorico(new RepositorioFalso(), remetente, destinatario);
            Console.WriteLine($"Resumo do Serviço Falso: {servicoFalso.Resumo()}");


            Console.WriteLine("====== AULA 06 ======");
            HashSet<Usuario> conjunto = new HashSet<Usuario>();
            conjunto.Add(new Usuario("Flavio", "Flávio Saito"));
            conjunto.Add(new Usuario("Flavio", "Flávio Saito"));

            Console.WriteLine($"Conjunto: {conjunto.Count}");


            Console.WriteLine("====== AULA 11 ======");
            string teste1 = "ENVIAR|ana|bruno|2026-10-14T09:12|Bom dia";
            AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();
            try
            {
                MensagemTexto mensagem = analisador.Analisar(teste1);
                mensagem.Exibir();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
            string teste2 = "ENVIAR|ana|bruno|2026-10-14T09:12|Bom | dia";
            try
            {
                MensagemTexto mensagem = analisador.Analisar(teste2);
                mensagem.Exibir();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
            string teste3 = "ENVIAR|ana|bruno|14/10/2026|Oi";
            try
            {
                MensagemTexto mensagem = analisador.Analisar(teste3);
                mensagem.Exibir();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }


            Console.WriteLine("====== AULA 12 ======");
            string teste4 = "ENVIAR|ana|bruno|2026-10-14T09:12|Bom dia";
            RepositorioEmArquivo repositorioArquivo = new RepositorioEmArquivo();
            try
            {
                repositorioArquivo.Guardar(analisador.Analisar(teste4));
                repositorioArquivo.Guardar(analisador.Analisar(teste4));
                repositorioArquivo.Guardar(analisador.Analisar(teste4));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            Console.WriteLine($"Arquivo criado e populado com mensagens.");

            Console.WriteLine("Mensagens no arquivo:");
            List<Mensagem> mensagens = repositorioArquivo.Listar();
            foreach (var msg in mensagens)
            {
                msg.Exibir();
            }


            Console.WriteLine("====== AULA 13 ======");
            ICanalDeComunicacao canalFlavio = new CanalArquivo(AppContext.BaseDirectory, "flavio");
            ICanalDeComunicacao canalServidor = new CanalArquivo(AppContext.BaseDirectory, "servidor");
            ICanalDeComunicacao canalMaria = new CanalArquivo(AppContext.BaseDirectory, "maria");

            //cliente flavio
            try
            {
                canalFlavio.Enviar("servidor", "ENVIAR|flavio|maria|2026-10-14T09:12|Bom dia, Maria!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            //servidor
            try
            {
                var caixaServidor = canalServidor.Receber();
                foreach (var linha in caixaServidor)
                {
                    canalServidor.Enviar("maria", linha);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            //cliente maria
            try
            {
                var caixaMaria = canalMaria.Receber();
                foreach (var linha in caixaMaria)
                {
                    Console.WriteLine(linha);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }


            Console.WriteLine("====== AULA 14 ======");
            RepositorioEmArquivo repositorioArquivo2 = new RepositorioEmArquivo();
            try
            {
                //repositorioArquivo2.Apagar();
                repositorioArquivo2.Guardar(CriarMensagem("Flavio|Maria|Olá! Tudo bem?"));
                repositorioArquivo2.Guardar(CriarMensagem("Maria|Flavio|Oi! Tudo ótimo!"));
                repositorioArquivo2.Guardar(CriarMensagem("João|Flavio|E aí, pessoal?"));
                repositorioArquivo2.Guardar(CriarMensagem("Flavio|Maria|Estudou para a prova?"));
                repositorioArquivo2.Guardar(CriarMensagem("Maria|Flavio|Que prova?"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
            var mensagensFiltradas = repositorioArquivo2.Listar(m => m.Remetente == "Flavio");
            Console.WriteLine($"Mensagens de Filtradas: {mensagensFiltradas.Count}");
        }
    }
}
