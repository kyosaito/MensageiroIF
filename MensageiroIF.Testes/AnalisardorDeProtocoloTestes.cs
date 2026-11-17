using MensageiroIF;
using MensageiroIF.Cliente;
using System.Runtime.CompilerServices;

namespace MensageiroIF.Testes
{
    [TestClass]
    public sealed class AnalisardorDeProtocoloTestes
    {
        [TestMethod]
        public void Analisar_LinhaValida_DevolveRemetenteCorreto()
        {
            AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();
            string linha = "ENVIAR|ana|bruno|2026-10-20T14:32|bom dia";

            Mensagem mensagem = analisador.Analisar(linha);

            Assert.AreEqual("ana", mensagem.Remetente);
        }

        [TestMethod]
        public void Analisar_LinhaComCampoFaltando_LancaProtocoloInvalidoException()
        {
            AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();
            
            try
            {
                analisador.Analisar("ENVIAR|ana|bruno");
                Assert.Fail("Esperava-se uma exceção ProtocoloInvalidoException, mas nenhuma foi lançada.");
            }
            catch (ProtocoloInvalidoException)
            {
                // Exceção lançada como esperado
            }
        }

        [TestMethod]
        public void Analisar_LinhaVazia_LancaProtocoloInvalidoException()
        {
            AnalisadorDeProtocolo analisador = new AnalisadorDeProtocolo();

            try
            {
                analisador.Analisar("");
                Assert.Fail("Esperava-se uma exceção ProtocoloInvalidoException, mas nenhuma foi lançada.");
            }
            catch (ProtocoloInvalidoException)
            {
                // Exceção lançada como esperado
            }
        }
    }
}
