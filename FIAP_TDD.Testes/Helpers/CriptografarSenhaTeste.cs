using FIAP_TDD.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Helpers
{
    public class CriptografarSenhaTeste
    {
        [Fact]
        public void CriptografiaDeSenha_Deveria_RetornarSenhaDiferente()
        {
            var senha = "Re46753951!@";
            var senhaRetorno = CriptografarSenha.EncriptografarSenha(senha);

            Assert.NotEqual(senha, senhaRetorno);
        }
    }
}
