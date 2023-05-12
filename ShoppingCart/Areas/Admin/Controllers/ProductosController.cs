using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;

namespace ShoppingCart.Areas.Admin.Controllers
{

    // Controlador de productos para el area del Admin
    [Area("Admin")]
    [Authorize]
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

        // Crear Producto GET
        public IActionResult Crear(int p = 1)
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre");

            return View();
        }

        // Crear Producto POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Producto producto)
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);

            if (ModelState.IsValid)
            {
                producto.URLSlug = producto.Nombre.ToLower().Replace(" ", "-");

                // Comprobamos si la URL del producto ya existe en la DDBB
                var URLSlug = await _context.Productos.FirstOrDefaultAsync(p => p.URLSlug == producto.URLSlug);
                if (URLSlug != null)
                {
                    ModelState.AddModelError("", "El producto ya existe");
                    return View(producto);
                }

                // Comprobamos si se ha añadido una imagen
                if (producto.ImagenUpload != null)
                {
                    string uploadsDir = Path.Combine(_environment.WebRootPath, "media/productos");
                    string imagenNombre = Guid.NewGuid().ToString() + "_" + producto.ImagenUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imagenNombre);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await producto.ImagenUpload.CopyToAsync(fs);
                    fs.Close();

                    producto.Imagen = imagenNombre;
                }

                _context.Add(producto);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Producto creado con éxito!";

                return RedirectToAction("Index");
            }

            return View(producto);
        }
        // Editar Producto GET
        public async Task<IActionResult> Editar(long id)
        {
            // Buscamos el producto de la DDBB _context
            Producto producto = await _context.Productos.FindAsync(id);

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);

            return View(producto);
        }
        // Editar Producto POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Producto producto)
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);

            if (ModelState.IsValid)
            {
                producto.URLSlug = producto.Nombre.ToLower().Replace(" ", "-");

                // Comprobamos si la URL del producto ya existe en la DDBB
                /*var URLSlug = await _context.Productos.FirstOrDefaultAsync(p => p.URLSlug == producto.URLSlug);
                if (URLSlug != null)
                {
                    ModelState.AddModelError("", "El producto ya existe");
                    return View(producto);
                }*/

                // Comprobamos si se ha añadido una imagen
                if (producto.ImagenUpload != null)
                {
                    string uploadsDir = Path.Combine(_environment.WebRootPath, "media/productos");
                    string imagenNombre = Guid.NewGuid().ToString() + "_" + producto.ImagenUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imagenNombre);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await producto.ImagenUpload.CopyToAsync(fs);
                    fs.Close();

                    producto.Imagen = imagenNombre;
                }

                _context.Update(producto);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Producto editado con éxito!";
            }

            return RedirectToAction("Index");
            //return View(producto);
        }
        // Eliminar Producto
        public async Task<IActionResult> Eliminar(long id)
        {
            // Buscamos el producto de la DDBB _context
            Producto producto = await _context.Productos.FindAsync(id);

            // Comprobamos si tiene una imagen guardada
            if (!string.Equals(producto.Imagen, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_environment.WebRootPath, "media/productos");
                string oldImagePath = Path.Combine(uploadsDir, producto.Imagen);
                if (System.IO.File.Exists(oldImagePath))
                {
                    // Borramos la imagen guardada
                    System.IO.File.Delete(oldImagePath);
                }

            }

            // Borramos el producto de la base de datos
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            // Borramos el producto del carrito
            List<ItemCarrito> carrito = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");

            carrito.RemoveAll(p => p.ProductoId == producto.Id);

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
    }
}
