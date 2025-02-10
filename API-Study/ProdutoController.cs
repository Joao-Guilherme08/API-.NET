using Microsoft.AspNetCore.Mvc;
using MeuProjeto.Models;

namespace MeuProjeto.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Lista()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Notebook", Preco = 4500 },
                new Produto { Id = 2, Nome = "Smartphone", Preco = 2800 }
            };

            return View(produtos);
        }

        public IActionResult Detalhes(int id)
        {
            var produto = new Produto { Id = id, Nome = "Notebook", Preco = 4500 };
            return View(produto);
        }
    }
}
