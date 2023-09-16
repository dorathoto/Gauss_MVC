using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


//normalmente a pasta se chama ViewComponents
namespace LanchesMac.Components;

/// <summary>
/// Recomenda-se utilizar o sufixo ViewComponent no nome da classe
/// Neste caso deveria ser CategoriaMenuViewComponent'
/// </summary>
public class CategoriaMenu : ViewComponent
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaMenu(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public IViewComponentResult Invoke()
    {
        var categorias = _categoriaRepository.Categorias.OrderBy(c => c.CategoriaNome);
        return View(categorias);
    }
}
