using Prog2_Act03.Models;

namespace Prog2_Act03.DTOs
{
    public class ArticuloDto
    {
        public int IdArticulo { get; set; }
        public string? Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }

        public static ArticuloDto FromEntity(Articulo a)
        {
            return new ArticuloDto
            {
                IdArticulo = a.IdArticulo,
                Nombre = a.Nombre,
                PrecioUnitario = a.PrecioUnitario
            };
        }

        public static Articulo ToEntity(ArticuloDto dto)
        {
            return new Articulo
            {
                IdArticulo = dto.IdArticulo,
                Nombre = dto.Nombre,
                PrecioUnitario = dto.PrecioUnitario
            };
        }
    }
}
