using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LanchesMac.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;    //não está no projeto


        /// <summary>
        /// Tbm não está no projeto, estou injetando o contexto para poder executar uma action e colocar alguns produtos fakes no banco
        /// </summary>
        /// <param name="context"></param>
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            TempData["Nome"] = "Leonardo";
            return View();
        }

        /// <summary>
        /// Inserindo produtos fictícios no banco
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> FakeSeed()
        {
            var rnd = new Random();
            
            List<Categoria> list = new List<Categoria>();

            for (int i = 0; i < 4; i++)
            {
                var categoria = new Categoria
                {
                    CategoriaNome = "Categoria " + i,
                    Descricao = "Descrição " + i
                };
                list.Add(categoria); //adicionando na lista para usar depois
                _context.Categorias.Add(categoria);
            }
            //await _context.SaveChangesAsync();
            for (int i = 0; i < 10; i++)
            {
                var lanche = new Lanche
                {
                    Nome = "Lanche " + i,
                    DescricaoCurta = "Descrição curta " + i,
                    DescricaoDetalhada = "Descrição det",
                    ImagemThumbnailUrl = "https://via.placeholder.com/150",
                    ImagemUrl = "http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg",
                    Categoria = list[rnd.Next(0,4)], //fiz um aleario para chamar o indice da lista
                };
                _context.Lanches.Add(lanche);   
            }
            await _context.SaveChangesAsync();
            
            return Content("Seed tosca realizada com sucesso");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}