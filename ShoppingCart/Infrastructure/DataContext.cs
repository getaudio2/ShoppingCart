using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Infrastructure
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        // Data context que representa los productos y categorias que se pueden recuperar de la base de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
