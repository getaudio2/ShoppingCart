using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IWebHostEnvironment _environment;

        public ProductosController(DataContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
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

        public IActionResult Crear(int p = 1)
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);

            if (ModelState.IsValid)
            {
                producto.URLSlug = producto.Nombre.ToLower().Replace(" ", "-");

                var URLSlug = await _context.Productos.FirstOrDefaultAsync(p => p.URLSlug == producto.URLSlug);
                if (URLSlug != null) 
                {
                    ModelState.AddModelError("", "El producto ya existe");
                    return View(producto);
                }
            }

            return View();
        }
    }
}
