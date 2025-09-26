using Entrega_Act4.Models;

namespace Entrega_Act4.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }

        public static ProductoDTO ToDTO(Producto producto)
        {
            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio
            };
        }
    }
}
