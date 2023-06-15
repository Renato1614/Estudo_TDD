using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Helpers
{
    public static class ConvertHelper<T>
    {
        public static T[] List_To_array(IEnumerable<T> list) => list.ToArray();
    }
}
