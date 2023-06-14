using Autofac.Extras.Moq;
using FIAP_TDD.Data.Data;
using FIAP_TDD.Services;
using FIAP_TDD.Testes.Factories;
using Microsoft.SqlServer.Server;
using Moq;

namespace FIAP_TDD.Testes.Service
{
    public class AlunoServiceTeste
    {
        [Fact]
        public async Task BuscarTodos_Deveria_RetornarOK()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoData>()
                .Setup(x => x.BuscarTodos())
                .Returns(AlunoTesteFactory.GerarListaDeAlunoModelParaTeste());

            var srv = mock.Create<AlunoService>();

            var retorno = await srv.BuscarTodos();

            Assert.NotNull(retorno);
        }

        [Fact]
        public async Task BuscarTodos_Deveria_RetornarNull()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoData>()
                .Setup(x => x.BuscarTodos())
                .Returns(AlunoTesteFactory.GerarListaNulaDeAlunosParaTeste());

            var srv = mock.Create<AlunoService>();

            var retorno = await srv.BuscarTodos();

            Assert.Null(retorno);
        }

        [Theory]
        [InlineData(1)]
        public async Task BuscarPorId_Deveria_RetornarOK(int id)
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoData>()
                .Setup(x => x.BuscarPorId(id))
                .Returns(AlunoTesteFactory.GerarAlunoModelParaTeste());

            var srv = mock.Create<AlunoService>();

            var retorno = await srv.BuscarPorId(id);

            Assert.NotNull(retorno);
            Assert.Equal(id, retorno.Id);
        }

        [Theory]
        [InlineData(1)]
        public async Task BuscarPorId_Deveria_RetornarNull(int id)
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoData>()
                .Setup(x => x.BuscarPorId(id))
                .Returns(AlunoTesteFactory.GerarAlunoNuloModelParaTeste());

            var srv = mock.Create<AlunoService>();

            var retorno = await srv.BuscarPorId(id);

            Assert.Null(retorno);
        }

        [Fact]
        public async Task CriarAluno_ComSenhaFraca_DeveriaProibir()
        {
            var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();
            aluno.Senha = "123";
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoData>()
                .Setup(x => x.GravarAluno(aluno));

            var svr = mock.Create<AlunoService>();
            var retorno = await svr.GravarAluno(aluno);

            Assert.False(retorno);
            mock.Mock<IAlunoData>()
                .Verify(x => x.GravarAluno(aluno), Times.Never());
        }

        [Fact]
        public async Task CriarAluno_ComSenhaForte_DeveriaPermitir()
        {
            var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();
            var mock = AutoMock.GetLoose();
            var svr = mock.Create<AlunoService>();

            var retorno = await svr.GravarAluno(aluno);

            Assert.True(retorno);
            mock.Mock<IAlunoData>()
                .Verify(x => x.GravarAluno(aluno), Times.Once());
        }

        [Fact]
        public async Task EditarAluno_Deveria_SerChamadoUmaVez()
        {
            var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();
            var mock = AutoMock.GetLoose();
            var svr = mock.Create<AlunoService>();
            await svr.EditarAluno(aluno);

            mock.Mock<IAlunoData>()
                .Verify(x => x.EditarAluno(aluno), Times.Once());
        }

        [Fact]
        public async Task DeletarAluno_Deveria_SerChamadoUmaVez()
        {
            var id = 1;
            var mock = AutoMock.GetLoose();
            var svr = mock.Create<AlunoService>();
            await svr.Deletar(id);

            mock.Mock<IAlunoData>()
                .Verify(x => x.Deletar(1), Times.Once());
        }
    }
}
