using FIAP_TDD.Data.DbAccess;
using FIAP_TDD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Data.Data
{
    public class TurmaData : ITurmaData
    {
        private readonly ISqlDataAccess _db;

        public TurmaData(ISqlDataAccess _db)
        {
            this._db = _db;
        }

        public async Task<TurmaModel?> BuscarPorId(int id)
        {
            var turmas = await _db.LoadData<TurmaModel, dynamic>(
                "dbo.SpTurmaBuscarPorId",
                new { Id = id });
            return turmas.FirstOrDefault();
        }

        public async Task<IEnumerable<TurmaModel>> BuscarTurmasPorNome(string nome, int? id)
        {
            var data = new
            {
                Nome = nome,
                Id = id == null ? 0 : id.Value
            };
            return await _db.LoadData<TurmaModel, dynamic>
                ("dbo.SpTurmaBuscarPorNome"
                , data);
        }


        public async Task<IEnumerable<TurmaModel>> BuscarTodas()
        {
            return await _db.LoadData<TurmaModel, dynamic>("dbo.SpTurmaBuscarTodas", new { });

        }

        public async Task Deletar(int id)
        {
            await _db.SaveData("dbo.spTurmaDeletar", new { Id = id });
        }

        public async Task Editar(TurmaModel turma)
        {
            await _db.SaveData("dbo.spTurmaEditar", turma);
        }

        public async Task Gravar(TurmaModel turma)
        {
            var data = new
            {
                turma.Curso_Id,
                turma.Turma,
                turma.Ano
            };
            await _db.SaveData("dbo.spTurmaGravar", data);
        }
    }
}
