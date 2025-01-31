using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage ="Invalid length, write only up to 100 characters")]
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name")]
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "Invalid length, write only up to 200 characters")]
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Description")]
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; }

    }
}
