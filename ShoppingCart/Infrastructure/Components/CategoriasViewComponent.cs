using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Infrastructure.Components
{
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
