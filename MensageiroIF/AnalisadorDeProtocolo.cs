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
        public const string ENVIAR = "ENVIAR";
        public const string HISTORICO = "HISTORICO";
        public const string LOGIN = "LOGIN";
        public const string LISTAR = "LISTAR";
        public const string LISTA = "LISTA";
        public const string SAIR = "SAIR";
        public const string OK = "OK";
        public const string ERRO = "ERRO";

        public const string PROTOCOLO_INVALIDO = "PROTOCOLO_INVALIDO";
        public const string USUARIO_DESCONHECIDO = "USUARO_DESCONHECIDO";
        public const string NAO_AUTENTICADO = "NAO_AUTENTICADO";
        public const string APELIDO_EM_USO = "APELIDO_EM_USO";

        public const char SEPARADOR = '|';

        const string PADRAO_ENVIAR = @"^(ENVIAR|HISTORICO)\|[^|]+\|[^|]+\|\d\d\d\d-\d\d-\d\dT\d\d:\d\d\:\d\d|.*$";
        
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
            string[] partes = linha.Split(new[] { SEPARADOR }, 5);
            MensagemTexto mensagem = new MensagemTexto(partes[1], partes[2], DateTime.Parse(partes[3]), partes[4]);
            return mensagem;
        }

        public static void ConferirApelido(string apelido)
        {
            if (apelido == null)
                throw new ArgumentException("apelido");

            if (apelido.Contains(SEPARADOR))
                throw new ApelidoInvalidoException($"O apelido não pode conter o caractere {SEPARADOR}");
        }

        //retorna qual o comando da string
        public string Comando(string linha)
        {
            if (string.IsNullOrWhiteSpace(linha))
                return "";

            return linha.Trim().Split(SEPARADOR)[0];
        }

        //troca o comando de uma linha: ENVIAR para HISTORICO e vice-versa
        public string ComComando(string linha, string comando)
        {
            int primeiro = linha.IndexOf(SEPARADOR);
            
            if (primeiro < 0)
                return comando;

            return comando + linha.Substring(primeiro);
        }

        public string MontarErro(string codigo, string detalhe)
        {
            return ERRO + SEPARADOR + codigo + SEPARADOR + detalhe;
        }

        public string MontarOK(string comando)
        {
            return OK + SEPARADOR + comando;
        }
    }
}
