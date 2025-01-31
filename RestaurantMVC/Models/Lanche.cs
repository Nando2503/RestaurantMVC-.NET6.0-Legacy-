using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [Display(Name = "Name of the dish")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "{0} must have at least {1} and maxium of {2}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [Display(Name = "Short Description")]
        [MaxLength(200, ErrorMessage = "cannot exceed {1} characters")]
        [MinLength(20, ErrorMessage ="mus have at least {1} characters")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [Display(Name = "Detailed Description")]
        [MaxLength(200, ErrorMessage = "cannot exceed {1} characters")]
        [MinLength(20, ErrorMessage = "mus have at least {1} characters")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "this field is required")]
        [Display(Name = "Price")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1,999.99, ErrorMessage = "price must be between 1 and 999.99")]
        public decimal Preco {  get; set; }

        [Display(Name = "Image URL")]
        [StringLength(200, ErrorMessage = "{0} must have a maximum of {1} characters")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Image URL")]
        [StringLength(200, ErrorMessage = "{0} must have a maximum of {1} characters")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Favourite?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "In Stock?")]
        public bool EmEstoque {  get; set; }

        [Display(Name = "Category: ")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
