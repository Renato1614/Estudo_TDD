using Autofac.Extras.Moq;
using FIAP_TDD.Data.Data;
using FIAP_TDD.Data.Models;
using FIAP_TDD.Services;
using FIAP_TDD.Testes.Factories;
using FIAP_TDD.Testes.Helpers;

namespace FIAP_TDD.Testes.Service
{
    public class TurmaServiceTeste
    {
        [Fact]
        public void BuscarTodos_Deveria_RetornarAMesmaQuantidade()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaData>()
                .Setup(x => x.BuscarTodas())
                .Returns(TurmaTesteFactory.GerarListaDeTurmas());
            var service = mock.Create<TurmaService>();

            var retorno = service.BuscarTodas();
            Assert.NotNull(retorno);
            Assert.Equal(2, retorno.Result.Count());
        }

        [Fact]
        public async Task CriarTurma_Deveria_RetornarTrue()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaData>()
                .Setup(x => x.Gravar(turma))
                .Returns(TurmaTesteFactory.GerarTurma());
            var service = mock.Create<TurmaService>();
            
            var retorno = await service.Gravar(turma);

            Assert.True(retorno);
        }

        [Fact]
        public async Task CriarTurma_Deveria_RetornarFalse_SeAnoForMenorQueAtual()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            turma.Ano = 2018;
            var mock = AutoMock.GetLoose();
            
            var service = mock.Create<TurmaService>();
            var retorno = await service.Gravar(turma);

            Assert.False(retorno);
        }

        [Fact]
        public async Task CriarTurma_Deveria_RetornarFalse_SeJAExistirTurmaComMesmoNome()
        {
            var turma =  await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaData>()
                .Setup(x => x.BuscarTurmasPorNome(turma.Turma,null))
                .Returns(TurmaTesteFactory.GerarListaDeTurmas());
            var service = mock.Create<TurmaService>();
            var retorno = await service.Gravar(turma);

            Assert.False(retorno);
        }

        [Fact]
        public async Task EditarTurma_Deveria_RetornarTrue_SeEstiverTudoCerto()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaData>()
                .Setup(x => x.Editar(turma))
                .Returns(RetornoDeValoresTask.RetornaValorBool(true));

            var service = mock.Create<TurmaService>();
            var retorno = await service.Editar(turma);

            Assert.True(retorno);
        }

        [Fact]
        public async Task EditarTurma_Deveria_RetornarFalse_SeAnoForMenorQueAtual()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            turma.Ano = 2018;
            var mock = AutoMock.GetLoose();

            var service = mock.Create<TurmaService>();
            var retorno = await service.Editar(turma);

            Assert.False(retorno);
        }

        [Fact]
        public async Task EditarTurma_Deveria_RetornarFalse_SeJaExistirTurmaComMesmoNome()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaData>()
                .Setup(x => x.BuscarTurmasPorNome(turma.Turma, 1))
                .Returns(TurmaTesteFactory.GerarListaDeTurmas());

            var service = mock.Create<TurmaService>();
            var retorno = await service.Editar(turma);

            Assert.False(retorno);
        }
    }
}
