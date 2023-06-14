using FIAP_TDD.Data.Models;

namespace FIAP_TDD.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<AlunoModel>> BuscarTodos();
        Task<AlunoModel> BuscarPorId(int id);
        Task<bool> GravarAluno(AlunoModel aluno);
        Task<bool> EditarAluno(AlunoModel aluno);
    }
}