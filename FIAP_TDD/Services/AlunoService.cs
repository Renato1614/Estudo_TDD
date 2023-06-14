using FIAP_TDD.Data.Data;
using FIAP_TDD.Data.Models;
using FIAP_TDD.Helper;

namespace FIAP_TDD.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoData _alunoDb;

        public AlunoService(IAlunoData aluno)
        {
            _alunoDb = aluno;
        }

        public async Task<AlunoModel?> BuscarPorId(int id)
        {
            try
            {
                AlunoModel? retorno = await _alunoDb.BuscarPorId(id);
                if (retorno != null)
                {
                    return retorno;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<AlunoModel>> BuscarTodos()
        {
            return await _alunoDb.BuscarTodos();
        }

        public async Task<bool> Deletar(int id)
        {
            try
            {
                await _alunoDb.Deletar(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditarAluno(AlunoModel aluno)
        {
            try
            {
                await _alunoDb.EditarAluno(aluno);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> GravarAluno(AlunoModel aluno)
        {
            try
            {
                if (!VerificadorDeSenhaForte.VerificarSenhaForte(aluno.Senha))
                {
                    return false;
                }
                aluno.Senha = CriptografarSenha.EncriptografarSenha(aluno.Senha);
                await _alunoDb.GravarAluno(aluno);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
