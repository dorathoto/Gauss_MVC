using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public async Task<IActionResult> List()
        {
            ViewData["Titulo"] = "Todos os lanches";
            ViewData["Data"] = DateTime.Now;
            var lanchesListViewModel = new LancheListViewModel();
            lanchesListViewModel.Lanches = _lancheRepository.Lanches;
            lanchesListViewModel.CategoriaAtual = "Categoria Atual";


            var totalLanches = lanchesListViewModel.Lanches.Count();
            ViewBag.Total = "Total de lanches: " + totalLanches;

            
            return View(lanchesListViewModel);
        }
    }
}
