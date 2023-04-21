using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Infrastructure.Components
{
    // ViewComponent de las Categorias.
    // Sirve para pasar las categorias desde la DDBB a la vista del html
    public class CategoriasViewComponent : ViewComponent
    {
        private readonly DataContext _context;

        public CategoriasViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync() => View(await _context.Categorias.ToListAsync());
    }
}