using ShoppingCart.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace ShoppingCart.Models
{
    public class Producto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Por favor, introduce un nombre")]
        public string Nombre { get; set; }
        public string URLSlug { get; set; }

        [Required, MinLength(4, ErrorMessage = "Longitud mínima: 2")]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Por favor, introduce un precio")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Precio { get; set; }
        public long CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public string Imagen { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImagenUpload { get; set; }
    }
}
