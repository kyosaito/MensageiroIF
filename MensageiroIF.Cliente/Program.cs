using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MensageiroIF.Cliente
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormCadastro());
            var usuario1 = new Usuario("FSaito", "Flavio Saito");
            var usuario2 = new Usuario("LKiehn", "Luiz Kiehn");
            var repositorio = new RepositorioEmMemoria();
            var servico = new ServicoDeHistorico(repositorio, usuario1, usuario2);


            //Application.Run(new FormEnvio(servico));
            var mt = new MensagemTexto(usuario1.Apelido, usuario2.Apelido, DateTime.Now,"Olá, tudo bem?");
            var ms = new MensagemSistema(DateTime.Now,"Mensagem de sistema", 1);
            repositorio.Guardar(mt);
            repositorio.Guardar(ms);
            Application.Run(new FormPrincipal(servico));
        }
    }
}
