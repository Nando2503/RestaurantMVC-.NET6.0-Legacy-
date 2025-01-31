using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.Migrations;
using RestaurantMVC.Models;
using RestaurantMVC.Repositories.Interfaces;





namespace RestaurantMVC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //gets the items in the customer's shopping cart
           List<Models.CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItems();
           _carrinhoCompra.CarrinhoCompraItems = items;

            //verifies if there's an items in the cart
            if(_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Looks like your cart is empty, what about we include some food?");

            }

            //calculating items and order totals
            foreach(var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            // atributing the recieved calculations to the order
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            //validade the order's data
            if (ModelState.IsValid)
            {
                // create order and details
                _pedidoRepository.CriarPedido(pedido);

                //defines messages to the customer
                ViewBag.CheckoutCompletoMensagem = "thanks for your order!";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                //clean the customer's cart
                _carrinhoCompra.LimparCarrinho();

                //returns the view with client's details and order
                
            }
            return View("~/Views/Pedido/CheckoutCompleto.cshtml");
        }
    }
}
