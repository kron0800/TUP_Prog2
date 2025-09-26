using Entrega_Act4.Models;

namespace Entrega_Act4.DTOs
{
    public class DetalleEnvioDTO
    {
        public int Id { get; set; }
        public ProductoDTO Producto { get; set; }
        public int Cantidad { get; set; }
        public string Comentario { get; set; }

        public static DetalleEnvioDTO ToDTO(DetalleEnvio detalleEnvio)
        {
            return new DetalleEnvioDTO
            {
                Id = detalleEnvio.Id,
                Producto = ProductoDTO.ToDTO(detalleEnvio.ProductoNavegation),
                Cantidad = detalleEnvio.Cantidad,
                Comentario = detalleEnvio.Comentario
            };
        }
    }
}
