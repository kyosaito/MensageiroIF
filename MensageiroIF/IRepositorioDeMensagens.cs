using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensageiroIF
{
    public interface IRepositorioDeMensagens
    {
        void Guardar(Mensagem mensagem);
        List<Mensagem> Listar();
    }
}
