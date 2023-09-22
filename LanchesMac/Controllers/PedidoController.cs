using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            carrinhoCompra = _carrinhoCompra;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Checkout(Pedido pedido)
        {
            return View();
        }
    }
}
