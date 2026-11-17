using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class RegistradorDeLog
    {
        public void Registrar(string texto)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {texto}");
        }

        public void Falha(string texto)
        {
            ConsoleColor antes = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {texto}");
            Console.ForegroundColor = antes;
        }
    }
}
