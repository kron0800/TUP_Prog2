using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Act01.Domain
{
    public class DetalleFactura
    {
        public int IdDetalleFactura { get; set; }
        public int IdFactura { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }

        public DetalleFactura() { }

        public DetalleFactura(int idDetalleFactura, int idFactura, Articulo articulo, int cantidad)
        {
            IdDetalleFactura = idDetalleFactura;
            IdFactura = idFactura;
            Articulo = articulo;
            Cantidad = cantidad;
        }

        public DetalleFactura(DataRow row)
        {
            IdDetalleFactura = Convert.ToInt32(row["id_detalle_factura"]);
            IdFactura = Convert.ToInt32(row["id_factura"]);
            Articulo = new Articulo(
                Convert.ToInt32(row["id_articulo"]),
                row["nombre"].ToString(),
                Convert.ToDecimal(row["precio_unitario"])
                );
            Cantidad = Convert.ToInt32(row["cantidad"]);
        }

        public override string ToString()
        {
            return $"Articulo: {Articulo.Nombre} - Precio: {Articulo.PrecioUnitario} - Cantidad: {Cantidad}";
        }
    }
}
