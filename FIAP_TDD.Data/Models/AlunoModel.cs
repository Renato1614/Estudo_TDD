using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Data.Models
{
    public class AlunoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Usuario { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
