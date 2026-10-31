using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class MensagemSistema : Mensagem
    {
        public int Prioridade { get; protected set; }

        public MensagemSistema(DateTime data, string corpo, int prioridade) : base("SISTEMA", data, corpo)
        {
            Prioridade = prioridade;
        }

        public override void Exibir()
        {
            Console.WriteLine($"[SISTEMA] {Corpo}");
        }

        public override string GetLinha()
        {
            return $"SISTEMA|{Data}|{Prioridade}|{Corpo}"; //a definir
        }
    }
}
