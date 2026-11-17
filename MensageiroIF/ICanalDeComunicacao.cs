using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public interface ICanalDeComunicacao
    {
        void Enviar(string destino, string linha);
        List<string> Receber();
    }
}
