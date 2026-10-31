using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public abstract class Mensagem
    {
        public string Remetente { get; protected set; }
        public DateTime Data { get; protected set; }
        public string Corpo { get; protected set; }

        public Mensagem()
        {
      
        }
        public Mensagem(string remetente, DateTime data, string corpo)
        {
            Remetente = remetente;
            Data = data;
            Corpo = corpo;
        }

        public abstract void Exibir();

        public override string ToString()
        {
            return $"{Data.ToString("dd/MM/yyyy HH:mm:ss")} - {Remetente}: {Corpo}";
        }

        public abstract string GetLinha();
    }
}
