using RestaurantMVC.Repositories.Interfaces;
using RestaurantMVC.Context;
using RestaurantMVC.Models;



namespace RestaurantMVC.Repositories
{
    public class CategoryRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Categoria> Categorias => _context.Categorias;

        
    }
}
