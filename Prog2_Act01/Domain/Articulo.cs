using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog2_Act01.Domain
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }

        public Articulo() { }

        public Articulo(System.Data.DataRow row)
        {
            IdArticulo = Convert.ToInt32(row["id_articulo"]);
            Nombre = row["nombre"].ToString();
            PrecioUnitario = Convert.ToDecimal(row["precio_unitario"]);
        }

        public Articulo(int idArticulo, string nombre, decimal precioUnitario)
        {
            IdArticulo = idArticulo;
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
        }

        public override string ToString()
        {
            return $"Articulo: {Nombre} - Precio unitario: {PrecioUnitario}";
        }
    }
}
