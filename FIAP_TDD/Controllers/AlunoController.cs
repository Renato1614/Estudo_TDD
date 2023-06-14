using FIAP_TDD.Data.Models;
using FIAP_TDD.Services;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_TDD.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoService _aluno;

        public AlunoController(IAlunoService aluno)
        {
            _aluno = aluno;
        }
        public async Task<IActionResult> Index()
        {
            var alunos = await _aluno.BuscarTodos();
            return View(alunos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(AlunoModel aluno)
        {
            if (await _aluno.GravarAluno(aluno))
            {
                return RedirectToAction(nameof(Index));

            }
            return View(aluno);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var aluno = await _aluno.BuscarPorId(id);
            return View(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(AlunoModel aluno)
        {
            await _aluno.EditarAluno(aluno);
            return RedirectToAction(nameof(Index));
        }
    }
}
