using FIAP_TDD.Data.Models;
using FIAP_TDD.Services;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_TDD.Controllers
{
    public class TurmaController : Controller
    {
        private readonly ITurmaService _turma;

        public TurmaController(ITurmaService turma)
        {
            _turma = turma;
        }

        public async Task<IActionResult> Index()
        {
            var turmas = await _turma.BuscarTodas();
            return View(turmas);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(TurmaModel turmaModel)
        {
            if (await _turma.Gravar(turmaModel))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(turmaModel);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id != null)
            {
                TurmaModel? turma = await _turma.BuscarPorId(id.Value);
                if (turma == null) return RedirectToAction(nameof(Index));
                return View(turma);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Editar(TurmaModel turmaModel)
        {
            if (await _turma.Editar(turmaModel))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(turmaModel);
        }

        [HttpPost]
        public async Task<bool> Deletar(int? id)
        {
            if (id != null)
            {
                try
                {
                    await _turma.Deletar(id.Value);
                    return true;
                }
                catch (Exception)
                {
                    return false;                 
                }
            }
            return false;
        }
    }
}
