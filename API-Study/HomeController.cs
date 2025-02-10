using Microsoft.AspNetCore.Mvc;

namespace MeuProjeto.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }
    }
}
