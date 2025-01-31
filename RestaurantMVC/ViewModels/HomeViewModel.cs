using RestaurantMVC.Models;

namespace RestaurantMVC.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
