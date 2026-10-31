using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace MensageiroIF
{    
    public class AnalisadorDeProtocolo
    {
        const string PADRAO_ENVIAR = @"^ENVIAR\|[^|]+\|[^|]+\|\d\d\d\d-\d\d-\d\dT\d\d:\d\d\|.*$";

        public MensagemTexto Analisar(string linha)
        {
            if (linha == null)
            {
                throw new ProtocoloInvalidoException("A mensagem está nula.");
            }

            linha = linha.Trim();
            if (!Regex.IsMatch(linha, PADRAO_ENVIAR))
            {
                throw new ProtocoloInvalidoException("Mensagem enviada fora do padrão.");
            }
            string[] partes = linha.Split(new[] { '|' }, 5);
            MensagemTexto mensagem = new MensagemTexto(partes[1], partes[2], DateTime.Parse(partes[3]), partes[4]);
            return mensagem;
        }
    }
}
