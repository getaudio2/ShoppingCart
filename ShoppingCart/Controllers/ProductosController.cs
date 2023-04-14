using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class ProductosController : Controller
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string categoriaSlug = "", int p = 1)
        {
            int pageSize = 3;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.CategoriaSlug = categoriaSlug;

            if (categoriaSlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Productos.Count() / pageSize);

                return View(await _context.Productos.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
            }

            Categoria categoria = await _context.Categorias.Where(c => c.URLSlug == categoriaSlug).FirstOrDefaultAsync();
            if (categoria == null) return RedirectToAction("Index");

            var productosPorCategoria = _context.Productos.Where(p => p.CategoriaId == categoria.Id);
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productosPorCategoria.Count() / pageSize);

            return View(await productosPorCategoria.OrderByDescending(p => p.Id).Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }
}