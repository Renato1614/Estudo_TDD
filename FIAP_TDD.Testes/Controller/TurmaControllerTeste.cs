using Autofac.Extras.Moq;
using FIAP_TDD.Controllers;
using FIAP_TDD.Data.Models;
using FIAP_TDD.Services;
using FIAP_TDD.Testes.Factories;
using FIAP_TDD.Testes.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Controller
{
    public class TurmaControllerTeste
    {
        [Fact]
        public async Task Index_Deveria_Retornar_UmaView_ComResultados()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.BuscarTodas())
                .Returns(TurmaTesteFactory.GerarListaDeTurmas());

            var controller = mock.Create<TurmaController>();
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TurmaModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Index_Deveria_Retornar_UmaView_SemResultados()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.BuscarTodas())
                .Returns(TurmaTesteFactory.GerarListaVaziaDeTurmas());

            var controller = mock.Create<TurmaController>();
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public void CriarGet_Deveria_RetornarUmaView()
        {
            var mock = AutoMock.GetLoose();
            var controller = mock.Create<TurmaController>();

            var result = controller.Criar();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CriarPost_SeTrue_Deveria_Retornar_RedirectToAction()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.Gravar(turma))
                .Returns(RetornoDeValoresTask.RetornaValorBool(true));

            var controller = mock.Create<TurmaController>();
            var result = await controller.Criar(turma);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);

        }

        [Fact]
        public async Task CriarPost_SeFalse_Deveria_Retornar_View()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.Gravar(turma))
                .Returns(RetornoDeValoresTask.RetornaValorBool(false));

            var controller = mock.Create<TurmaController>();
            var result = await controller.Criar(turma);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TurmaModel>(viewResult.ViewData.Model);
            Assert.Equal(turma.Turma, model.Turma);
            Assert.Equal(turma.Ano, model.Ano);
            Assert.Equal(turma.Curso_Id, model.Curso_Id);
        }

        [Fact]
        public async Task EditarGet_SeTiverID_Deveria_RetornarView()
        {
            var turma = TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.BuscarPorId(1))
                .Returns(turma);
            var controller = mock.Create<TurmaController>();
            var result = await controller.Editar(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TurmaModel>(viewResult.ViewData.Model);
            Assert.Equal(turma.Result.Turma, model.Turma);
            Assert.Equal(turma.Result.Id, model.Id);
            Assert.Equal(turma.Result.Ano, model.Ano);
            Assert.Equal(turma.Result.Curso_Id, model.Curso_Id);
        }

        [Fact]
        public async Task EditarGet_SeNaoTiverID_Deveria_RetornarRedirectToAction()
        {
            var turma = TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();

            var controller = mock.Create<TurmaController>();
            var result = await controller.Editar((int?)null);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
            Assert.Null(viewResult.ControllerName);
        }

        [Fact]
        public async Task EditarGet_SeNaoAchar_Deveria_RetornarRedirectToAction()
        {
            var turma = TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.BuscarPorId(1))
                .Returns(TurmaTesteFactory.GerarTurmaVazia());

            var controller = mock.Create<TurmaController>();
            var result = await controller.Editar(1);

            var viewResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", viewResult.ActionName);
            Assert.NotNull(viewResult.ControllerName);
        }

        [Fact]
        public async Task Deletar_SeNaoTiverId_RetornaFalse()
        {
            var mock = AutoMock.GetLoose();
            
            var controller = mock.Create<TurmaController>();
            var retorno = await controller.Deletar(null);

            Assert.False(retorno);
        }

        [Fact]
        public async Task Deletar_SeTiverId_RetornaTrue()
        {
            var mock = AutoMock.GetLoose();

            var controller = mock.Create<TurmaController>();
            var retorno = await controller.Deletar(1);

            Assert.True(retorno);
        }

        [Fact]
        public async Task EditarPost_SeTrue_RetornaRedirectToAction()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.Editar(turma))
                .Returns(RetornoDeValoresTask.RetornaValorBool(true));

            var controller = mock.Create<TurmaController>();
            var retorno = await controller.Editar(turma);

            var viewResult = Assert.IsType<RedirectToActionResult>(retorno);
            Assert.Equal("Index", viewResult.ActionName);


        }

        [Fact]
        public async Task EditarPost_SeFalse_RetornaView()
        {
            var turma = await TurmaTesteFactory.GerarTurma();
            var mock = AutoMock.GetLoose();
            mock.Mock<ITurmaService>()
                .Setup(x => x.Editar(turma))
                .Returns(RetornoDeValoresTask.RetornaValorBool(false));
            var controller = mock.Create<TurmaController>();
            var retorno = await controller.Editar(turma);

            var viewResult = Assert.IsType<ViewResult>(retorno);
            var model = Assert.IsAssignableFrom<TurmaModel>(viewResult.Model);
            Assert.Equal(turma.Turma, model.Turma);
            Assert.Equal(turma.Id, model.Id);
            Assert.Equal(turma.Ano, model.Ano);
            Assert.Equal(turma.Curso_Id, model.Curso_Id);
        }
    }
}
