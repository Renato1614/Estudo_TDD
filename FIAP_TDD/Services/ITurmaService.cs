using FIAP_TDD.Data.Models;

namespace FIAP_TDD.Services
{
    public interface ITurmaService
    {
        Task<TurmaModel?> BuscarPorId(int value);
        Task<IEnumerable<TurmaModel>> BuscarTodas();
        Task Deletar(int value);
        Task<bool> Editar(TurmaModel turma);
        Task<bool> Gravar(TurmaModel model);
    }
}