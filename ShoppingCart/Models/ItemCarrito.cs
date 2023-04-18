namespace ShoppingCart.Models
{
    public class ItemCarrito
    {
        public long ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total
        {
            get { return Cantidad * Precio; }
        }
        public string Imagen { get; set; }

        public ItemCarrito() { }

        public ItemCarrito(Producto producto)
        { 
            ProductoId = producto.Id;
            ProductoNombre = producto.Nombre;
            Precio = producto.Precio;
            Cantidad = 1;
            Imagen = producto.Imagen;
        }
    }
}
