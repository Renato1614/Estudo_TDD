using FIAP_TDD.Data.Data;
using FIAP_TDD.Data.Models;

namespace FIAP_TDD.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaData _turmaData;

        public TurmaService(ITurmaData turmaData)
        {
            _turmaData = turmaData;
        }

        public async Task<TurmaModel?> BuscarPorId(int id)
        {
            return await _turmaData.BuscarPorId(id);
        }

        public async Task<IEnumerable<TurmaModel>> BuscarTodas()
        {
            return await _turmaData.BuscarTodas();
        }

        public async Task Deletar(int id)
        {
            await _turmaData.Deletar(id);
        }

        public async Task<bool> Editar(TurmaModel turma)
        {
            try
            {
                if (VerificaSeAnoEMenorQueOAtual(turma.Ano)) return false;
                if (await VerificaSeJaExisteTurmaComMesmoNome(turma.Turma, turma.Id)) return false;
                await _turmaData.Editar(turma);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Gravar(TurmaModel turma)
        {
            try
            {
                if (VerificaSeAnoEMenorQueOAtual(turma.Ano)) return false;
                if(await VerificaSeJaExisteTurmaComMesmoNome(turma.Turma,null)) return false;
                await _turmaData.Gravar(turma);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool VerificaSeAnoEMenorQueOAtual(int anoDaTurma)
        {
            var anoAtual = DateTime.Now.Year;
            if (anoAtual > anoDaTurma) return true;
            return false;

        }

        private async Task<bool> VerificaSeJaExisteTurmaComMesmoNome(string nome, int? id)
        {
            var turmaExistente = await _turmaData.BuscarTurmasPorNome(nome,id);
            if (turmaExistente.Any()) return true;
            return false;
        }
    }
}
