using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    internal class MensagemArquivo : MensagemTexto
    {
        string arquivo;

        public MensagemArquivo(string remetente, string destinatario, DateTime data, string arquivo) : base(remetente, destinatario, data, "")
        {
            this.arquivo = arquivo;
        }

        public override void Exibir()
        {
            Console.WriteLine($"{Remetente} enviou arquivo {arquivo} em {Data.ToString("dd/MM/yyyy HH:mm:ss")}.");
        }

        public override string GetLinha()
        {
            return $"ENVIAR|{Remetente}|{Destinatario}|{Data}|{arquivo}|{Corpo}";
        }

    }
}
