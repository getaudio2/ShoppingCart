using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Controllers
{
    // Controlador de las funciones para botones en el carro de la compra
    public class CarroCompraController : Controller
    {
        // Recoger productos de la base de datos
        private readonly DataContext _context;

        public CarroCompraController(DataContext context)
        {
            _context = context;
        }

        // Función por defecto del Carro Compra
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

        // Función para el botón de aumentar cantidad del producto
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

        // Función para el botón de disminuir cantidad del producto
        public async Task<IActionResult> Quitar(long id)
        {
            List<ItemCarrito> carrito = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");

            ItemCarrito itemCarrito = carrito.Where(c => c.ProductoId == id).FirstOrDefault();

            if (itemCarrito.Cantidad > 1)
            {
                --itemCarrito.Cantidad;
            }
            else
            {
                carrito.RemoveAll(p => p.ProductoId == id);
            }

            if (carrito.Count == 0)
            {
                HttpContext.Session.Remove("Carrito");
            }
            else
            {
                HttpContext.Session.SetJson("Carrito", carrito);
            }

            TempData["Success"] = "Producto quitado con éxito!";

            return RedirectToAction("Index");
        }

        // Función para el botón de eliminar producto del Carro Compra
        public async Task<IActionResult> Eliminar(long id)
        {
            List<ItemCarrito> carrito = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");

            carrito.RemoveAll(p => p.ProductoId == id);

            if (carrito.Count == 0)
            {
                HttpContext.Session.Remove("Carrito");
            }
            else
            {
                HttpContext.Session.SetJson("Carrito", carrito);
            }

            TempData["Success"] = "Producto eliminado con éxito!";

            return RedirectToAction("Index");
        }

        // Función para el botón de vaciar el Carro Compra por completo
        public IActionResult Vaciar()
        {
            HttpContext.Session.Remove("Carrito");

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
