using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class ApelidoInvalidoException : Exception
    {
        public ApelidoInvalidoException(string message) : base(message) { }

    }
}
