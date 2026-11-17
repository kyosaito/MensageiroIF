using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class ErroDoServidor
    {
        public string Codigo { get; private set; }
        public string Detalhe { get; private set; }
        public ErroDoServidor(string codigo, string detalhe)
        {
            Codigo = codigo;
            Detalhe = detalhe;
        }
    }
}
