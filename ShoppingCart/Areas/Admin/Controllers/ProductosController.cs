using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;

namespace ShoppingCart.Areas.Admin.Controllers
{

    // Controlador de productos para el area del Admin
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Productos.Count() / pageSize);

            return View(await _context.Productos.OrderByDescending(p => p.Id)
                .Include(p => p.Categoria)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync());
        }
    }
}
