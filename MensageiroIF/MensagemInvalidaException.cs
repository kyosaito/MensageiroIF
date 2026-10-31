using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public class MensagemInvalidaException : Exception
    {
        public MensagemInvalidaException(string message) : base(message)
        {
        }
    }
}
