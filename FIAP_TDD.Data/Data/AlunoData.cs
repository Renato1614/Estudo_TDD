using FIAP_TDD.Data.DbAccess;
using FIAP_TDD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Data.Data
{
    public class AlunoData : IAlunoData
    {
        private readonly ISqlDataAccess _db;

        public AlunoData(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<AlunoModel?> BuscarPorId(int id)
        {
            var resultado = await _db.LoadData<AlunoModel, object>(
                "dbo.spAluno_BuscarPorId",
                new { Id = id },
                "Default");
            return resultado.FirstOrDefault();
        }

        public async Task<IEnumerable<AlunoModel>> BuscarTodos()
        {
            return await _db.LoadData<AlunoModel, object>(
                "dbo.spAluno_BuscarTodos",
                new { });
        }

        public async Task Deletar(int id)
        {
            await _db.SaveData(
                "dbo.spDeletarAluno",
                new { Id = id });
        }

        public async Task EditarAluno(AlunoModel aluno)
        {
            var data = new { aluno.Nome, aluno.Usuario, aluno.Id };
            await _db.SaveData(
                "dbo.spEditarAlunos", data);
        }

        public async Task GravarAluno(AlunoModel aluno)
        {
            var data = new
            {
                aluno.Nome,
                aluno.Usuario,
                aluno.Senha
            };
            await _db.SaveData("dbo.spGravarAlunos", data);
        }
    }
}
