using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class MensagemTexto : Mensagem
    {
        public string Destinatario { get; private set; }
        
        public MensagemTexto(string remetente, string destinatario, DateTime data, string corpo) : base(remetente, data, corpo)
        {
            Destinatario = destinatario;
        }

        public override void Exibir()
        {
            Console.WriteLine($"{Remetente} para {Destinatario}: {Corpo}");
        }

        public override string GetLinha()
        {
            return $"ENVIAR|{Remetente}|{Destinatario}|{Data.ToString("yyyy-MM-ddTHH:mm")}|{Corpo}";
        }
    }
}
