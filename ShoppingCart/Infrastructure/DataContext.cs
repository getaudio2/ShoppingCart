using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using System.Security.Cryptography.X509Certificates;

namespace ShoppingCart.Infrastructure
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
