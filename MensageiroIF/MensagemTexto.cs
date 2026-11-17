using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class MensagemTexto : Mensagem
    {
        public const int LIMITE = 250; //Tamanho máximo do corpo da mensagem
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
            return $"ENVIAR|{Remetente}|{Destinatario}|{Data.ToString("yyyy-MM-ddTHH:mm:ss")}|{Corpo}";
        }

        public static void ConferirCorpo(string corpo)
        {
            if (string.IsNullOrWhiteSpace(corpo))
                throw new MensagemInvalidaException($"A mensagem está vazia.");
            if (corpo.Length > LIMITE)
                throw new MensagemInvalidaException($"A mensagem excede o limite de {LIMITE} caracteres.");
        }
    }
}
