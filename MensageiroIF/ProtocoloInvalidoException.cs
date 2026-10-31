using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    internal class ProtocoloInvalidoException : Exception
    {
        public ProtocoloInvalidoException(string message) : base(message) { }
    }
}
