using Autofac.Extras.Moq;
using FIAP_TDD.Data.Data;
using FIAP_TDD.Data.DbAccess;
using FIAP_TDD.Data.Models;
using FIAP_TDD.Testes.Factories;
using FIAP_TDD.Testes.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Data
{
    public class TurmaDataTeste
    {
        [Fact]
        public async Task BuscarPorId_Deveria_RetornarOMesmoObjeto()
        {
            using var mock = AutoMock.GetLoose();
            var turmas = await TurmaTesteFactory.GerarListaDeTurmas();
            var turmaEsperada = turmas.First();
            mock.Mock<ISqlDataAccess>()
                .Setup(x => x.LoadData<TurmaModel, dynamic>(
                "dbo.SpTurmaBuscarPorId",
                It.IsAny<object>(),
                It.IsAny<string>()))
                .ReturnsAsync(turmas);

            var turmaData = mock.Create<TurmaData>();
            var result = await turmaData.BuscarPorId(1);

            Assert.NotNull(result);
            Assert.Equal(result.Id, turmaEsperada.Id);
            Assert.Equal(result.Ano, turmaEsperada.Ano);
            Assert.Equal(result.Turma, turmaEsperada.Turma);
        }

        [Fact]
        public async Task BuscarPorId_RetornoIguaANull()
        {
            // Arrange
            using var mock = AutoMock.GetLoose();
            int id = 1;

            // Simular o retorno de uma lista de turmas
            var turmas = new List<TurmaModel>();

            mock.Mock<ISqlDataAccess>()
                .Setup(x => x.LoadData<TurmaModel, object>(
                    "dbo.SpTurmaBuscarPorId",
                    It.IsAny<object>(),
                    It.IsAny<string>()))
                .ReturnsAsync(turmas);

            var turmaData = mock.Create<TurmaData>();

            // Act
            var result = await turmaData.BuscarPorId(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task BuscarPorNome_Deveria_RetornarObjetosComMesmoNome()
        {
            using var mock = AutoMock.GetLoose();
            var turmas = await TurmaTesteFactory.GerarListaDeTurmas();
            mock.Mock<ISqlDataAccess>()
                .Setup(x => x.LoadData<TurmaModel, object>("dbo.SpTurmaBuscarPorNome"
                , It.IsAny<object>(),
                It.IsAny<string>()))
                .ReturnsAsync(turmas);

            var turmaData = mock.Create<TurmaData>();
            var retorno = await turmaData.BuscarTurmasPorNome(turmas.First().Turma,
                                                  turmas.First().Id);
            var esperado = ConvertHelper<TurmaModel>.List_To_array(turmas);
            var arrayTurmasRetornadas = ConvertHelper<TurmaModel>.List_To_array(retorno);

            Assert.NotNull(retorno);
            for (int i = 0; i < retorno.Count(); i++)
            {
                Assert.Equal(esperado[i].Turma, arrayTurmasRetornadas[i].Turma);
                Assert.Equal(esperado[i].Ano, arrayTurmasRetornadas[i].Ano);
                Assert.Equal(esperado[i].Curso_Id, arrayTurmasRetornadas[i].Curso_Id);
            }
        }

        [Fact]
        public async Task Editar_Deveria_SerChamadoUmaVez()
        {
            using var mock = AutoMock.GetLoose();
            var turma = await TurmaTesteFactory.GerarTurma();
            mock.Mock<ISqlDataAccess>()
                .Setup(x => x.SaveData("dbo.spTurmaEditar",
                                     turma,
                                     It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var turmaData = mock.Create<TurmaData>();
            await turmaData.Editar(turma);

            mock.Mock<ISqlDataAccess>()
                    .Verify(x => x.SaveData("dbo.spTurmaEditar",
                                                 turma,
                                                 It.IsAny<string>()),
                                                 Times.Once());
        }

        [Fact]
        public async Task Deletar_Deveria_SerChamadoUmaVez()
        {
            using var mock = AutoMock.GetLoose();
            var turma = await TurmaTesteFactory.GerarTurma();
            mock.Mock<ISqlDataAccess>()
                .Setup(x => x.SaveData("dbo.spTurmaEditar",
                                      turma,
                                      It.IsAny<string>()))
                .Returns((Task)Task.CompletedTask);

            var turmaData = mock.Create<TurmaData>();
            await turmaData.Editar(turma);

            mock.Mock<ISqlDataAccess>()
                .Verify(x => x.SaveData("dbo.spTurmaEditar",
                                      turma,
                                      It.IsAny<string>())
                , Times.Once());
        }

        [Fact]
        public async Task Gravar_Deveria_SerChamadaUmaVez()
        {
            using var mock = AutoMock.GetLoose();
            var turma = await TurmaTesteFactory.GerarTurma();
            mock.Mock<ISqlDataAccess>()
                .Setup(x => x.SaveData("dbo.spTurmaGravar",
                                      It.IsAny<object>(),
                                      It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var turmaData = mock.Create<TurmaData>();
            await turmaData.Gravar(turma);

            mock.Mock<ISqlDataAccess>()
                .Verify(x => x.SaveData("dbo.spTurmaGravar",
                                      It.IsAny<object>(),
                                      It.IsAny<string>())
                , Times.Once());
        }
    }
}
