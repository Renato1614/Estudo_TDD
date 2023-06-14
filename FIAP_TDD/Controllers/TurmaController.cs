using Microsoft.AspNetCore.Mvc;

namespace FIAP_TDD.Controllers
{
    public class TurmaController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
