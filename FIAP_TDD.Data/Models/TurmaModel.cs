using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Data.Models
{
    public class TurmaModel
    {
        public int Id { get; set; }
        public int Curso_Id { get; set; }
        public string Turma { get; set; }
        public int Ano { get; set; }

    }
}
