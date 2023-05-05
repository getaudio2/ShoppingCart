using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class Usuario
    {
        public string Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Longitud mínima: 2")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Longitud mínima: 4")]
        public string Password { get; set; }
    }
}