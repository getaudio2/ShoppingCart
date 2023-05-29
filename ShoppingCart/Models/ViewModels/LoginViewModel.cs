using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models.ViewModels
{
    // Modelo de vista que representa la información visualizada
    // específicamente en la vista de login
    public class LoginViewModel
    {
        [Required, MinLength(2, ErrorMessage = "Longitud mínima: 2")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Longitud mínima: 4")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}