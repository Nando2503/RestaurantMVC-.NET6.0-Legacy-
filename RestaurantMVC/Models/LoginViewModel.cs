using System.ComponentModel.DataAnnotations;

namespace RestaurantMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Inform Your Username")]
        [Display(Name = "User")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter a valid password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
