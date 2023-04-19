using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Controllers
{
    public class CarroCompraController : Controller
    {
        private readonly DataContext _context;

        public CarroCompraController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ItemCarrito> carrito = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito") ?? new List<ItemCarrito>();

            CarroCompraViewModel carroCompraVM = new()
            {
                ItemsCarrito = carrito,
                GranTotal = carrito.Sum(x => x.Cantidad * x.Precio)
            };
            return View(carroCompraVM);
        }
        public async Task<IActionResult> Añadir(long id)
        {
            Producto producto = await _context.Productos.FindAsync(id);

            List<ItemCarrito> carrito = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito") ?? new List<ItemCarrito>();

            ItemCarrito itemCarrito = carrito.Where(c => c.ProductoId == id).FirstOrDefault();

            if (itemCarrito == null)
            {
                carrito.Add(new ItemCarrito(producto));
            }
            else
            {
                itemCarrito.Cantidad += 1;
            }

            HttpContext.Session.SetJson("Carrito", carrito);

            TempData["Success"] = "Producto añadido con éxito!";

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
