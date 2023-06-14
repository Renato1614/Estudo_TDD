using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Helpers
{
    public static class RetornoDeValoresTask
    {
        public static async  Task<bool> RetornaValorBool(bool valorBool) =>
            valorBool;
    }
}
