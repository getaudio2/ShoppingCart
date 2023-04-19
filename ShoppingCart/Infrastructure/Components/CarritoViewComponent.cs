using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Infrastructure.Components
{
    public class CarritoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<ItemCarrito> carrito = HttpContext.Session.GetJson<List<ItemCarrito>>("Carrito");
            CarritoViewModel carritoVM;

            if (carrito == null || carrito.Count == 0)
            {
                carritoVM = null;
            }
            else 
            {
                carritoVM = new()
                {
                    CantidadItems = carrito.Sum(x => x.Cantidad),
                    PrecioTotal = carrito.Sum(x => x.Cantidad * x.Precio)
                };
            }
            
            return View(carritoVM);
        }
    }
}