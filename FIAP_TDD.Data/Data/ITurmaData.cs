using FIAP_TDD.Data.Models;

namespace FIAP_TDD.Data.Data
{
    public interface ITurmaData
    {
        Task<TurmaModel?> BuscarPorId(int id);
        Task<IEnumerable<TurmaModel>> BuscarTurmasPorNome(string nome, int? id);
        Task<IEnumerable<TurmaModel>> BuscarTodas();
        Task Deletar(int id);
        Task Editar(TurmaModel turma);
        Task Gravar(TurmaModel turma);
    }
}