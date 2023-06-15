
using Autofac.Extras.Moq;
using FIAP_TDD.Data.Data;
using FIAP_TDD.Data.DbAccess;
using FIAP_TDD.Data.Models;
using FIAP_TDD.Testes.Factories;
using Moq;

namespace FIAP_TDD.Testes.Data;

public class AlunoDataTest
{
    [Fact]
    public async Task BuscarPorId_Deveria_RetornarUmObjeto()
    {
        using var mock = AutoMock.GetLoose();
        var alunos = await AlunoTesteFactory.GerarListaDeAlunoModelParaTeste();
        var aluno = alunos.FirstOrDefault();
        int id = 1;

        mock.Mock<ISqlDataAccess>()
            .Setup(x => x.LoadData<AlunoModel, object>(
                "dbo.spAluno_BuscarPorId",
                It.IsAny<object>(),
                "Default"))
            .ReturnsAsync(alunos);

        var alunoData = mock.Create<AlunoData>();
        var result = await alunoData.BuscarPorId(id);


        Assert.NotNull(result);
        Assert.Equal(aluno.Id, result.Id);
        Assert.Equal(aluno.Nome, result.Nome);
        Assert.Equal(aluno.Usuario, result.Usuario);
        mock.Mock<ISqlDataAccess>()
            .Verify(x => x.LoadData<AlunoModel, object>(
                "dbo.spAluno_BuscarPorId",
                It.IsAny<object>(),
                "Default"), Times.Once());
    }

    [Fact]
    public async Task BuscarPorId_Deveria_RetornarNull()
    {
        using var mock = AutoMock.GetLoose();
        var alunos = AlunoTesteFactory.GerarListaDeAlunoModelParaTeste();
        var aluno = alunos.Result.FirstOrDefault();
        int id = 1;

        mock.Mock<ISqlDataAccess>()
            .Setup(x => x.LoadData<AlunoModel, object>(
                "dbo.spAluno_BuscarPorId",
                new { Id = id },
                "Default"))
            .Returns(AlunoTesteFactory.GerarListaVaziaDeAlunosParaTeste());

        var alunoData = mock.Create<AlunoData>();
        var result = await alunoData.BuscarPorId(id);


        Assert.Null(result);
        mock.Mock<ISqlDataAccess>()
            .Verify(x => x.LoadData<AlunoModel, object>(
                "dbo.spAluno_BuscarPorId",
                It.IsAny<object>(),
                "Default"), Times.Once());
    }

    [Fact]
    public async Task BuscarTodos_Deveria_RetornarOsMesmosObjetos()
    {
        using var mock = AutoMock.GetLoose();
        var alunos = AlunoTesteFactory.GerarListaDeAlunoModelParaTeste();

        mock.Mock<ISqlDataAccess>()
            .Setup(x => x.LoadData<AlunoModel, object>(
                "dbo.spAluno_BuscarTodos",
                It.IsAny<object>(),
                "Default"))
            .Returns(AlunoTesteFactory.GerarListaDeAlunoModelParaTeste());

        var alunoData = mock.Create<AlunoData>();
        var result = await alunoData.BuscarTodos();

        Assert.NotNull(result);
        var resultadoArray = result.ToArray();
        var esperado = alunos.Result.ToArray();
        for (int i = 0; i < result.Count(); i++)
        {
            Assert.Equal(esperado[i].Id, resultadoArray[i].Id);
            Assert.Equal(esperado[i].Nome, resultadoArray[i].Nome);
            Assert.Equal(esperado[i].Usuario, resultadoArray[i].Usuario);
            Assert.Equal(esperado[i].Senha, resultadoArray[i].Senha);
        }
    }

    [Fact]
    public async Task Deletar_Deveria_SerChamadoSoUmaVez()
    {
        using var mock = AutoMock.GetLoose();
        var id = 1;

        mock.Mock<ISqlDataAccess>()
            .Setup(x => x.SaveData(
                "dbo.spDeletarAluno",
                It.IsAny<object>(),
                It.IsAny<string>()))
            .Returns(Task.CompletedTask);

        var alunoData = mock.Create<AlunoData>();

        await alunoData.Deletar(id);

        mock.Mock<ISqlDataAccess>()
            .Verify(x => x.SaveData(
                "dbo.spDeletarAluno",
                It.IsAny<object>(),
                It.IsAny<string>()), Times.Once());
    }

    [Fact]
    public async Task EditarAluno_Deveria_SerChamadoSoUmaVez()
    {
        using var mock = AutoMock.GetLoose();
        var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();

        mock.Mock<ISqlDataAccess>()
            .Setup(x => x.SaveData(
                "dbo.spEditarAlunos",
                It.IsAny<object>(),
                It.IsAny<string>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var alunoData = mock.Create<AlunoData>();

        await alunoData.EditarAluno(aluno);

        mock.Mock<ISqlDataAccess>()
            .Verify(x => x.SaveData(
                "dbo.spEditarAlunos",
                It.IsAny<object>(),
                It.IsAny<string>()), Times.Once());
    }

    [Fact]
    public async Task GravarAluno_Deveria_SerChamadoSoUmaVez()
    {
        using var mock = AutoMock.GetLoose();
        var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();

        mock.Mock<ISqlDataAccess>()
            .Setup(x => x.SaveData(
                "dbo.spGravarAlunos",
                It.IsAny<object>(),
                It.IsAny<string>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var alunoData = mock.Create<AlunoData>();

        await alunoData.GravarAluno(aluno);

        mock.Mock<ISqlDataAccess>()
            .Verify(x => x.SaveData(
                "dbo.spGravarAlunos",
                It.IsAny<object>(),
                It.IsAny<string>()), Times.Once());
    }

}

