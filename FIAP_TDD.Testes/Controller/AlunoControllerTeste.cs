using Autofac.Extras.Moq;
using FIAP_TDD.Controllers;
using FIAP_TDD.Data.Models;
using FIAP_TDD.Models;
using FIAP_TDD.Services;
using FIAP_TDD.Testes.Factories;
using FIAP_TDD.Testes.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_TDD.Testes.Controller
{
    public class AlunoControllerTeste
    {
        [Fact]
        public async void Index_Deveria_RetornarOK_SemResultados()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.BuscarTodos())
                .Returns(AlunoTesteFactory.GerarListaNulaDeAlunosParaTeste());
            var controller = mock.Create<AlunoController>();

            var retorno = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(retorno);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async void Index_Deveria_RetornarOK_ComResultados()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.BuscarTodos())
                .Returns(AlunoTesteFactory.GerarListaDeAlunoModelParaTeste());
            var controller = mock.Create<AlunoController>();

            var retorno = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(retorno);
            var model = Assert.IsAssignableFrom<IEnumerable<AlunoModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public void CriarGet_Deveria_RetornarUmaView()
        {
            var mock = AutoMock.GetLoose();
            var controller = mock.Create<AlunoController>();

            var retorno = controller.Criar();

            Assert.IsType<ViewResult>(retorno);
        }

        [Fact]
        public async void CriarPost_ComSucesso_Deveria_RetornarUmaRedirectView()
        {
            var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.GravarAluno(aluno))
                .Returns(RetornoDeValoresTask.RetornaValorBool(true));
            var controller = mock.Create<AlunoController>();

            var retorno = await controller.Criar(aluno);

            Assert.IsType<RedirectToActionResult>(retorno);
            mock.Mock<IAlunoService>()
                .Verify(x => x.GravarAluno(aluno), Times.Once());

        }

        [Fact]
        public async void CriarPost_ComErro_Deveria_RetornarUmaView()
        {
            var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.GravarAluno(aluno))
                .Returns(RetornoDeValoresTask.RetornaValorBool(false));

            var controller = mock.Create<AlunoController>();
            var retorno = await controller.Criar(aluno);
            var viewResult = Assert.IsType<ViewResult>(retorno);

            var model = Assert.IsAssignableFrom<AlunoModel>(
                    viewResult.ViewData.Model);
            Assert.Equal(aluno.Usuario, model.Usuario);
            Assert.Equal(aluno.Nome, model.Nome);
            Assert.Equal(aluno.Senha, model.Senha);
        }

        [Fact]
        public async void EditarGet_Deveria_retornarUmaView()
        {
            var aluno = AlunoTesteFactory.GerarAlunoModelParaTeste();
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.BuscarPorId(1)).
                Returns(aluno);

            var controller = mock.Create<AlunoController>();
            var retorno = await controller.Editar(1);

            var viewResult = Assert.IsType<ViewResult>(retorno);
            var model = Assert.IsAssignableFrom<AlunoModel>(
                   viewResult.ViewData.Model);
            Assert.Equal(aluno.Result.Usuario, model.Usuario);
            Assert.Equal(aluno.Result.Nome, model.Nome);
            Assert.Equal(aluno.Result.Senha, model.Senha);
            mock.Mock<IAlunoService>()
                .Verify(x => x.BuscarPorId(1), Times.Once());
        }

        [Fact]
        public async void EditarPost_SeRetornarTrue_Deveria_RetornarUma_RedirectView()
        {
            var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.EditarAluno(aluno))
                .Returns(RetornoDeValoresTask.RetornaValorBool(true));

            var controller = mock.Create<AlunoController>();
            var retorno = await controller.Editar(aluno);

            var viewResult = Assert.IsType<RedirectToActionResult>(retorno);
            Assert.Equal("Index", viewResult.ActionName);
            Assert.Null(viewResult.ControllerName);
            mock.Mock<IAlunoService>()
                .Verify(x => x.EditarAluno(aluno), Times.Once());
        }


        [Fact]
        public async void EditarPost_SeRetornarFalse_Deveria_RetornarUma_View()
        {
            var aluno = await AlunoTesteFactory.GerarAlunoModelParaTeste();
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.EditarAluno(aluno))
                .Returns(RetornoDeValoresTask.RetornaValorBool(false));

            var controller = mock.Create<AlunoController>();
            var retorno = await controller.Editar(aluno);

            var viewResult = Assert.IsType<ViewResult>(retorno);
            var model = Assert.IsAssignableFrom<AlunoModel>(
                   viewResult.ViewData.Model);
            Assert.Equal(aluno.Usuario, model.Usuario);
            Assert.Equal(aluno.Nome, model.Nome);
            Assert.Equal(aluno.Senha, model.Senha);
            mock.Mock<IAlunoService>()
                .Verify(x => x.EditarAluno(aluno), Times.Once());
        }

        [Fact]
        public async void DeletarPost_SeID_DiferenteDeZero_DeveriaSerSucesso()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x=>x.Deletar(1))
                .Returns(RetornoDeValoresTask.RetornaValorBool(true));

            var controller = mock.Create<AlunoController>();
            var retorno = await controller.Deletar(1);

            Assert.True(retorno);
        }

        [Fact]
        public async void DeletarPost_SeID_IgualZero_DeveriaSerSucesso()
        {
            var mock = AutoMock.GetLoose();
            mock.Mock<IAlunoService>()
                .Setup(x => x.Deletar(1))
                .Returns(RetornoDeValoresTask.RetornaValorBool(false));

            var controller = mock.Create<AlunoController>();
            var retorno = await controller.Deletar(0);

            Assert.False(false);
            mock.Mock<IAlunoService>()
               .Verify(x => x.Deletar(1), Times.Never());
        }
    }

}
