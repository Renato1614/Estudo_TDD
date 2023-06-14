using FIAP_TDD.Data.Models;

namespace FIAP_TDD.Data.Data
{
    public interface IAlunoData
    {
        Task<AlunoModel?> BuscarPorId(int id);
        Task<IEnumerable<AlunoModel>> BuscarTodos();
        Task Deletar(int id);
        Task EditarAluno(AlunoModel aluno);
        Task GravarAluno(AlunoModel aluno);
    }
}