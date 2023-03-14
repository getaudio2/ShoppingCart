using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Infrastructure
{
    public class SeedData
    {
        public static void SeedDatabase(DataContext context)
        {
            context.Database.Migrate();

            if (!context.Productos.Any())
            {
                Categoria frutas = new Categoria { Nombre = "Frutas", URLSlug = "frutas" };
                Categoria camisas = new Categoria { Nombre = "Camisas", URLSlug = "camisas" };

                context.Productos.AddRange(
                        new Producto
                        {
                            Nombre = "Manzanas",
                            URLSlug = "manzanas",
                            Descripcion = "Manzanas dulces",
                            Precio = 1.50M,
                            Categoria = frutas,
                            Imagen = "manzanas.jpg"
                        },
                        new Producto
                        {
                            Nombre = "Platanos",
                            URLSlug = "platanos",
                            Descripcion = "Platanos frescos",
                            Precio = 3M,
                            Categoria = frutas,
                            Imagen = "platanos.jpg"
                        },
                        new Producto
                        {
                            Nombre = "Sandia",
                            URLSlug = "sandia",
                            Descripcion = "Sandia dulce",
                            Precio = 0.50M,
                            Categoria = frutas,
                            Imagen = "sandia.jpg"
                        },
                        new Producto
                        {
                            Nombre = "Uvas",
                            URLSlug = "uvas",
                            Descripcion = "Uvas dulces",
                            Precio = 2M,
                            Categoria = frutas,
                            Imagen = "uvas.jpg"
                        },
                        new Producto
                        {
                            Nombre = "Camisa blanca",
                            URLSlug = "camisa-blanca",
                            Descripcion = "Camisa blanca",
                            Precio = 5.99M,
                            Categoria = camisas,
                            Imagen = "camisa_blanca.jpg"
                        },
                        new Producto
                        {
                            Nombre = "Camisa negra",
                            URLSlug = "camisa-negra",
                            Descripcion = "Camisa negra",
                            Precio = 7.99M,
                            Categoria = camisas,
                            Imagen = "camisa_negra.jpg"
                        },
                        new Producto
                        {
                            Nombre = "Camisa amarilla",
                            URLSlug = "camisa-amarilla",
                            Descripcion = "Camisa amarilla",
                            Precio = 11.99M,
                            Categoria = camisas,
                            Imagen = "camisa_amarilla.jpg"
                        },
                        new Producto
                        {
                            Nombre = "Camisa gris",
                            URLSlug = "camisa-gris",
                            Descripcion = "Camisa gris",
                            Precio = 12.99M,
                            Categoria = camisas,
                            Imagen = "camisa_gris.jpg"
                        }
                );

                context.SaveChanges();
            }
        }
    }
}