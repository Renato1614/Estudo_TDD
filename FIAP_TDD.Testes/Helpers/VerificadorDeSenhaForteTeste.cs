using FIAP_TDD.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Helpers
{
    public class VerificadorDeSenhaForteTeste
    {
        [Fact]
        public void SenhaFraca_Deveria_RetornarFalse()
        {
            var senha = "123!@";
            var retorno = VerificadorDeSenhaForte.VerificarSenhaForte(senha);
            Assert.False(retorno);
        }

        [Fact]
        public void SenhaForte_Deveria_RetornarTrue()
        {
            var senha = "Re46753951!@";
            var retorno = VerificadorDeSenhaForte.VerificarSenhaForte(senha);
            Assert.True(retorno);
        }
    }
}
