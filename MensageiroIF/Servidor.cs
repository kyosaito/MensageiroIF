using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    internal class Servidor
    {
        Dictionary<string, Usuario> usuarios = new Dictionary<string, Usuario>();
        RegistradorDeLog logs = new RegistradorDeLog();

        public Servidor(List<Usuario> usuarios)
        {
            
        }
    }
}
