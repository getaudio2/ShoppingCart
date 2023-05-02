using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Infrastructure.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var pathExtension = Path.GetExtension(file.FileName);

                string[] extensiones = { "jpg", "png" };
                bool resultado = extensiones.Any(x => pathExtension.EndsWith(x));

                if(!resultado)
                {
                    return new ValidationResult("Solo se permiten las extensiones jpg y png");
                }
            }
            return ValidationResult.Success;
        }
    }
}
