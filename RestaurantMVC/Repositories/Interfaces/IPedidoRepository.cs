using RestaurantMVC.Models;

namespace RestaurantMVC.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
