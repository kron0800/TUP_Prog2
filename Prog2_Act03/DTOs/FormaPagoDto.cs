using Prog2_Act03.Models;

namespace Prog2_Act03.DTOs
{
    public class FormaPagoDto
    {
        public int IdFormaPago { get; set; }
        public string? Nombre { get; set; }
        public static FormaPagoDto FromEntity(FormaPago fp)
        {
            return new FormaPagoDto
            {
                IdFormaPago = fp.IdFormaPago,
                Nombre = fp.Nombre
            };
        }
    }
}
