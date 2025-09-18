using Prog2_Act03.Models;

namespace Prog2_Act03.DTOs
{
    public class DetalleFacturaDto
    {
        public int IdDetalleFactura { get; set; }
        public ArticuloDto Articulo { get; set; }
        public int Cantidad { get; set; }

        public static DetalleFacturaDto FromEntity(DetalleFactura d) => new DetalleFacturaDto
        {
            IdDetalleFactura = d.IdDetalleFactura,
            Articulo = ArticuloDto.FromEntity(d.IdArticuloNavigation),
            Cantidad = d.Cantidad
        };

        public static DetalleFactura ToEntity(DetalleFacturaDto dto) => new DetalleFactura
        {
            IdDetalleFactura = dto.IdDetalleFactura,
            // Asume que el IdFactura se setea en el contexto de la Factura
            IdArticulo = dto.Articulo.IdArticulo,
            Cantidad = dto.Cantidad
        };

    }
}
