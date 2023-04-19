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
            List<ItemCarrito> carrito = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");

            CarroCompraViewModel carroCompraVM = new()
            {
                ItemsCarrito = carrito,
                GranTotal = carrito.Sum(x => x.Cantidad * x.Precio)
            };
            return View(carroCompraVM);
        }
    }
}
