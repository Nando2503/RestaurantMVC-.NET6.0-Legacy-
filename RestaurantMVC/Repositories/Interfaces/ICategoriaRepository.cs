using RestaurantMVC.Models;
namespace RestaurantMVC.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
