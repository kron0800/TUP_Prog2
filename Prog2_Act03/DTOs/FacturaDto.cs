using System.Text.Json.Serialization;
using Prog2_Act03.Models;

namespace Prog2_Act03.DTOs
{
    public class FacturaDto
    {
        public int IdFactura { get; set; }
        public int NroFactura { get; set; }
        public string Cliente { get; set; }
        public DateOnly Fecha { get; set; }
        public FormaPagoDto? FormaPago { get; set; }
        public List<DetalleFacturaDto> Detalles { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? IdFormaPago { get; set; }

        public static FacturaDto FromEntity(Factura f)
        {
            return new FacturaDto
            {
                IdFactura = f.IdFactura,
                NroFactura = f.NroFactura,
                Cliente = f.Cliente,
                Fecha = f.Fecha,
                FormaPago = FormaPagoDto.FromEntity(f.IdFormaPagoNavigation),
                Detalles = f.DetalleFacturas.Select(DetalleFacturaDto.FromEntity).ToList()
            };
        }

        public static Factura ToEntity(FacturaDto dto)
        {
            if (dto.IdFormaPago == null) {
                throw new ArgumentException("IdFormaPago cannot be null when converting to entity.");
            }
            return new Factura
            {
                IdFactura = dto.IdFactura,
                NroFactura = dto.NroFactura,
                Cliente = dto.Cliente,
                Fecha = dto.Fecha,
                IdFormaPago = (int)dto.IdFormaPago,
                DetalleFacturas = dto.Detalles?.Select(DetalleFacturaDto.ToEntity).ToList()
            };
        }

    }
}
