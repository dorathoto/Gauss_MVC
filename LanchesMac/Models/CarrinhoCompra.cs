using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{

    /// <summary>
    /// Eu não colocaria essa classe aqui na Models, normalmente utilizo a pasta models apenas para Models tipo anemicas, sem lógica de negócio.
    /// Aqui está mais para um serviço/repositório que uma simples Model, porém o C# permite fazer muitas coisas então vamos seguir o vídeo.
    /// </summary>
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //define uma sessão, má prática, mas é o que temos para hoje
            ISession session =
                services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //como estou em uma classe preciso de alguma maneira de injetar os serviços
            var context = services.GetService<AppDbContext>();

            //obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();  // se for null, ganha novo guid

            //atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        /// <summary>
        /// Não vou modificar, mas recomendaria ser Async
        /// </summary>
        /// <param name="lanche"></param>
        public void AdicionarAoCarrinho(Lanche lanche)
        {
            //SingleOrDefault - retorna apenas 1 se tiver mais de 1 retorna erro
            //FirstOrDefault = retorna o primeiro se tiver mais de 1 retorna o primeiro, por isso é mais seguro
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                     s => s.Lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId);

            //se fosse assync eu teria que fazer isso
            //var carrinhoCompraItem = await _context.CarrinhoCompraItens.FirstOrDefaultAsync(
            //         s => s.Lanche.LancheId == lanche.LancheId &&
            //         s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                //já faço o Add com o new CarrinhoCompraItem
                _context.CarrinhoCompraItens.Add(new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                });
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            //mesma coisa, faria async e firstOrDefault
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                   s => s.Lanche.LancheId == lanche.LancheId &&
                   s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        /// <summary>
        /// pequena refatorada ao código original (removi parentes desnecessários)
        /// </summary>
        /// <returns></returns>
        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItems ?? _context.CarrinhoCompraItens
                                            .Where(w => w.CarrinhoCompraId == CarrinhoCompraId)
                                            .Include(i => i.Lanche)
                                            .OrderBy(o => o.CarrinhoCompraId)//apenas para mostrar como ordenar
                                            .ToList();
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                 .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        //refatorei para colocar direto no return
        public decimal GetCarrinhoCompraTotal()
        {
            return _context.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Lanche.Preco * c.Quantidade).Sum();
            
        }
    }
}
