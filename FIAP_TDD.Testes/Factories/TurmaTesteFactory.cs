using FIAP_TDD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Factories
{
    public static class TurmaTesteFactory
    {
        public async static Task<IEnumerable<TurmaModel>> GerarListaDeTurmas()
        {
            return new List<TurmaModel>()
            {
                new TurmaModel
                {
                    Id = 1,
                    Curso_Id = 1,
                    Turma="Matematica",
                    Ano=2023
                },
                new TurmaModel
                {
                    Id = 2,
                    Curso_Id = 2,
                    Turma="Historia",
                    Ano=2018
                }
            };
        }

        public async static Task<IEnumerable<TurmaModel>> GerarListaVaziaDeTurmas() => null;

        public async static Task<TurmaModel> GerarTurma()
            => new TurmaModel
            {
                Id = 1,
                Curso_Id = 1,
                Turma = "Matematica",
                Ano = 2023
            };

        public async static Task<TurmaModel> GerarTurmaVazia()
        {
            return null;
        }
    }
}
