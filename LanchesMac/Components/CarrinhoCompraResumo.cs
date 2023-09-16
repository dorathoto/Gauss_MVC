using LanchesMac.Models;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;


//normalmente a pasta se chama ViewComponents
namespace LanchesMac.Components;

/// <summary>
/// Recomenda-se utilizar o sufixo ViewComponent no nome da classe
/// Neste caso deveria ser CarrinhoCompraResumoViewComponent
/// </summary>
public class CarrinhoCompraResumo : ViewComponent
{
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
    {
        _carrinhoCompra = carrinhoCompra;
    }

    public IViewComponentResult Invoke()
    {
        var itens = _carrinhoCompra.GetCarrinhoCompraItens();

        //var itens = new List<CarrinhoCompraItem>()
        //{
        //    new CarrinhoCompraItem(),
        //    new CarrinhoCompraItem()
        //};

        _carrinhoCompra.CarrinhoCompraItems = itens;

        var carrinhoCompraVM = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraVM);
    }
}
