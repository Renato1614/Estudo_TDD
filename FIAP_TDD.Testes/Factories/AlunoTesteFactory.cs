using FIAP_TDD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Factories
{
    public static class AlunoTesteFactory
    {
        public async static Task<IEnumerable<AlunoModel>> GerarListaDeAlunoModelParaTeste()
        {
            return new List<AlunoModel>()
            {
                new AlunoModel
                {
                    Id = 1,
                    Nome="Renato",
                    Usuario="Renato1614",
                    Senha="123123"
                },
                new AlunoModel
                {
                    Id = 2,
                    Nome="Renato",
                    Usuario="Renato1614",
                    Senha="123123"
                },
                new AlunoModel
                {
                    Id = 3,
                    Nome="Renato",
                    Usuario="Renato1614",
                    Senha="123123"
                }
            };
        }

        public async static Task<AlunoModel> GerarAlunoModelParaTeste()
        {
            return new AlunoModel
            {
                Id = 1,
                Nome = "Renato",
                Usuario = "Renato1614",
                Senha = "Re46753951!@"
            };
        }

        public async static Task<AlunoModel?> GerarAlunoNuloModelParaTeste()
        {
            return null;
        }

        public async static Task<IEnumerable<AlunoModel>> GerarListaNulaDeAlunosParaTeste()
        {
            return null;
        }
    }
}
